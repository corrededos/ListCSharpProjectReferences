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

                 
            string output = "";
            XDocument xml = XDocument.Load(filePath);

            //1. Iterate all child elements of <Project> to find <ItemGroup>
            foreach (XElement projectChildElement in xml.Elements().First().Elements())
            {
                //2. If ItemGroup then look for entrys of <Reference> element
                if (projectChildElement.Name.LocalName == "ItemGroup") {
                    if (!projectChildElement.HasElements) break;
                    
                    
                    //=&= TODO: Finns riks för logiskt fel här, Reference behöver ej komma först eller vara default, det kan ligga andra element före och då missar man reference element ur en projektfil.
                    if (projectChildElement.Elements().FirstOrDefault().Name.LocalName == "Reference")
                    {


                        //3. When <Reference> child elements are found in <ItemGroup>, iterate and build up output.
                        IEnumerable<XElement>itemGroup = projectChildElement.Elements();
                        Reference refObj = new Reference();
                        
                        foreach (XElement referenceElement in itemGroup)
                        {
                            refObj = LoadReferenceObject(referenceElement, filePath);
                            output += @"<Reference " + refObj.ReferenceInformation 
                                    + @" SpecificVersion=""" + refObj.SpecificVersion 
                                    + @""" HintPath=""" + refObj.HintPath 
                                    + @""" Private=""" + refObj.Private 
                                    + @""" ExtDep=""" + refObj.ExternalDependency 
                                    + @""" InOutputFolder=""" + refObj.InOutputFolder 
                                    + @""" InPkgFolder=""" + refObj.InPackagesFolder 
                                    + @""" InProject=""" + refObj.ProjectPath 
                                    + @"""></Reference>" 
                                    + System.Environment.NewLine;

                        }
                        
                    }
                }
            }
            return output;
                        
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
