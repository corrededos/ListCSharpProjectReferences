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

        /// <summary>
        ///     Returns XML formatted string with all References and COMReferences for all .csproj files for a given folder path.
        /// </summary>
        /// <param name="filePath">Folder to search</param>
        /// <returns></returns>
        private string GetReferencesFromFile(string filePath)
        {

            //Get References
            //Get Com References
            //Dont get project references


            string output = "";
            XElement xEl = XElement.Load(filePath);

            //Reference
            output = GetReferencesOfType(filePath,xEl, Reference.ReferenceType.Reference);

            //COMReference
            output += GetReferencesOfType(filePath, xEl, Reference.ReferenceType.COMReference);

            return output;
        }

        /// <summary>
        /// Returns XML formatted string by reference type for all .csproj files for a given folder path.
        /// </summary>
        /// <param name="filePath">Folder to search</param>
        /// <param name="xEl">XElement that is loaded from .csproj file</param>
        /// <param name="referenceType">Reference type filter to search for</param>
        /// <returns></returns>
        private string GetReferencesOfType(string filePath, XElement xEl, Reference.ReferenceType referenceType)
        {
            string output = "";
            IEnumerable<XElement> elements;
            Reference refObject;

            //Get elements by ReferenceType
            elements = xEl.Descendants().Where(el => el.Name.LocalName == referenceType.ToString());
            refObject = new Reference();

            foreach (XElement element in elements)
            {
                refObject = LoadReferenceObject(element, filePath, referenceType);
                output += @"<Reference " + refObject.ReferenceInformation
                        + @" SpecificVersion=""" + refObject.SpecificVersion
                        + @""" HintPath=""" + refObject.HintPath
                        + @""" Private=""" + refObject.Private
                        + @""" ExtDep=""" + refObject.ExternalDependency
                        + @""" InOutputFolder=""" + refObject.InOutputFolder
                        + @""" InPkgFolder=""" + refObject.InPackagesFolder
                        + @""" InProject=""" + refObject.ProjectPath
                        + @""" ReferenceType=""" + referenceType.ToString()
                        + @"""></Reference>"
                        + System.Environment.NewLine;
            }
            return output;
        }

        /// <summary>
        /// Loads Reference Entity object
        /// </summary>
        /// <param name="reference">The reference element from the .csproj file, used to iterate its childs for additional information.</param>
        /// <param name="filePath">Used for tracking purposes</param>
        /// <param name="referenceType">Used to filter by specific reference type</param>
        /// <returns></returns>
        private Reference LoadReferenceObject(XElement reference, string filePath, Reference.ReferenceType referenceType)
        {
            //Assign value to InfoObject
            Reference refObj = new Reference {ProjectPath = filePath,
                                              RefType = referenceType,
                                              ReferenceInformation = reference.Attribute("Include").ToString()
                                              };
            
            
            
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
    public class Reference
    {


        public enum ReferenceType
        {
            Reference = 0,
            COMReference = 1,
            ProjectReference = 2

        }
        public string ProjectPath { get; set; }
        public string SpecificVersion { get; set; }
        public string HintPath { get; set; }
        public string Private { get; set; }
        public string ReferenceInformation { get; set; }
        public string AdditionalInformation { get; set; }
        public string ExternalDependency { get; set; }
        public string InOutputFolder { get; set; }
        public string InPackagesFolder { get; set; }
        public ReferenceType RefType { get; set; }

    }
}

