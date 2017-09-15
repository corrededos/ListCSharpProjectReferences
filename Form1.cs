using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace WindowsFormsApplication3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            readAllFiles();
        }

        private void readAllFiles() {

            string output = "";
            string filePaths = "";
            string[] files = Directory.GetFiles(txtSearchFolderPath.Text, "*.*csproj", SearchOption.AllDirectories);

            //GET REFERENCES
            foreach (var filePath in files)
            {
                filePaths += filePath + System.Environment.NewLine;
                output += GetReferencesFromFile(filePath); 
            }

            //OUTPUT
            txtReferences.Text = "<References>" + System.Environment.NewLine + output + System.Environment.NewLine + "</References>";
            txtFilesSearched.Text = "Searched in follwing files:" + System.Environment.NewLine + filePaths;

        }
        private string GetReferencesFromFile(string filePath) {


            //Get References
            //Get Com References
            //Dont get project references


            string output = "";
            XDocument xml = XDocument.Load(filePath);



            //----------------------------------------------------------------------------
            //----------------------------------------------------------------------------
            //----------------------------------------------------------------------------
            XElement xEl = XElement.Load(filePath);
            string testOut = "";

            IEnumerable<XElement> elements = xEl.Descendants().Where(el => el.Name.LocalName =="Reference");
            Reference refObject = new Reference();
            foreach (XElement element in elements)
            {
                refObject = LoadReferenceObject(element, filePath);
                testOut += @"<Reference " + refObject.ReferenceInformation
                        + @" SpecificVersion=""" + refObject.SpecificVersion
                        + @""" HintPath=""" + refObject.HintPath
                        + @""" Private=""" + refObject.Private
                        + @""" ExtDep=""" + refObject.ExternalDependency
                        + @""" InOutputFolder=""" + refObject.InOutputFolder
                        + @""" InPkgFolder=""" + refObject.InPackagesFolder
                        + @""" InProject=""" + refObject.ProjectPath
                        + @"""></Reference>"
                        + System.Environment.NewLine;
            }
            return testOut;
            //----------------------------------------------------------------------------
            //----------------------------------------------------------------------------
            //----------------------------------------------------------------------------
                        
        }

        private Reference LoadReferenceObject(XElement reference, string filePath)
        {
            //Assign value to InfoObject
            Reference refObj = new Reference();
            refObj.ReferenceInformation = reference.Attribute("Include").ToString();
            refObj.ProjectPath = filePath;
            foreach (XElement referenceChild in reference.Elements())
            {
                switch (referenceChild.Name.LocalName)
                {
                    case "HintPath":
                        refObj.HintPath = referenceChild.Value;

                        if (referenceChild.Value.ToString().Contains(@"\ExternalDep\"))
                        {
                            refObj.ExternalDependency = "True";
                        }
                        else
                        {
                            refObj.ExternalDependency = "False";
                        }

                        if (referenceChild.Value.ToString().Contains(@"\Output\"))
                        {
                            refObj.InOutputFolder = "True";
                        }
                        else
                        {
                            refObj.InOutputFolder = "False";
                        }
                        if (referenceChild.Value.ToString().Contains(@"\packages\"))
                        {
                            refObj.InPackagesFolder = "True";
                        }
                        else
                        {
                            refObj.InPackagesFolder = "False";
                        }


                        break;
                    case "SpecificVersion":
                        refObj.SpecificVersion = referenceChild.Value;
                        break;
                    case "Private":
                        refObj.Private = referenceChild.Value.ToString();
                        break;

                    default:
                        refObj.AdditionalInformation += referenceChild.Value;
                        break;
                }

            }
            return refObj;
        }
    }
}
public class Reference{

    public string ProjectPath { get; set; }

    public string SpecificVersion { get; set; }
    public string HintPath { get; set; }
    public string Private { get; set; }
    public string ReferenceInformation { get; set; }
    public string AdditionalInformation { get; set; }
    public string ExternalDependency { get; set; }
    public string InOutputFolder { get; set; }
    public string InPackagesFolder { get; set; }
}
