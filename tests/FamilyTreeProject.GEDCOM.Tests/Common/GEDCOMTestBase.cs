//******************************************
//  Copyright (C) 2014-2015 Charles Nurse  *
//                                         *
//  Licensed under MIT License             *
//  (see included LICENSE)                 *
//                                         *
// *****************************************

#nullable enable
using System;
using System.IO;

namespace FamilyTreeProject.GEDCOM.Tests.Common
{
    public abstract class GEDCOMTestBase
    {
        protected virtual string FilePath => Path.Combine("TestFiles");
        
        protected string GetFileString(string fileName)
        {
            string text = "";
            using (var reader = new StreamReader(GetFileStream(fileName)))
            {
                string line = "";
                while ((line = reader.ReadLine()) != null)
                {
                    text += String.Format("{0}\n", line);
                }
            }
            return text;
        }
        
        protected Stream? GetFileStream(string fileName)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            var fileFullPath = GetFileName(fileName);
            if (!File.Exists(fileFullPath))
            {
                return null;
            }

            return new FileStream(fileFullPath, FileMode.Open, FileAccess.Read);
        }
        
        private string GetFileName(string fileName)
        {
            return Path.ChangeExtension(Path.Combine(FilePath, fileName), ".ged");
        }
    }
}