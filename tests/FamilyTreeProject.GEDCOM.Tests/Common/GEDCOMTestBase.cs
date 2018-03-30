using System;
using System.IO;
using System.Reflection;

namespace FamilyTreeProject.GEDCOM.Tests.Common
{
    public abstract class GEDCOMTestBase
    {
        protected virtual string EmbeddedFilePath
        {
            get { return String.Empty; }
        }

        protected string GetEmbeddedFileName(string fileName)
        {
            string fullName = String.Format("{0}.{1}", EmbeddedFilePath, fileName);
            if (!fullName.ToLower().EndsWith(".ged"))
            {
                fullName += ".ged";
            }

            return fullName;
        }

        protected Stream GetEmbeddedFileStream(string fileName)
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceStream(GetEmbeddedFileName(fileName));
        }

        protected string GetEmbeddedFileString(string fileName)
        {
            string text = "";
            using (var reader = new StreamReader(GetEmbeddedFileStream(fileName)))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    text += String.Format("{0}\n", line);
                }
            }
            return text;
        }
    }
}