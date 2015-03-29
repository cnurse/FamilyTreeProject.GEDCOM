//******************************************
//  Copyright (C) 2014-2015 Charles Nurse  *
//                                         *
//  Licensed under MIT License             *
//  (see included LICENSE)                 *
//                                         *
// *****************************************

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

        protected virtual string FilePath
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

        protected abstract Stream GetEmbeddedFileStream(string fileName);

        protected string GetEmbeddedFileString(string fileName)
        {
            string text = "";
            using (var reader = new StreamReader(GetEmbeddedFileStream(fileName)))
            {
                string line = "";
                while ((line = reader.ReadLine()) != null)
                {
                    text += String.Format("{0}\n", line);
                }
            }
            return text;
        }

        private string GetFileName(string fileName)
        {
            string fullName = String.Format("{0}\\{1}", FilePath, fileName);
            if (!fullName.ToLower().EndsWith(".ged"))
            {
                fullName += ".ged";
            }

            return fullName;
        }

        protected Stream GetFileStream(string fileName)
        {
            return new FileStream(GetFileName(fileName), FileMode.Open, FileAccess.Read);
        }

        protected string GetFileString(string fileName)
        {
            string text = "";
            using (StreamReader reader = new StreamReader(new FileStream(GetFileName(fileName), FileMode.Open, FileAccess.Read)))
            {
                string line = "";
                while ((line = reader.ReadLine()) != null)
                {
                    text += String.Format("{0}\n", line);
                }
            }
            return text;
        }
    }
}