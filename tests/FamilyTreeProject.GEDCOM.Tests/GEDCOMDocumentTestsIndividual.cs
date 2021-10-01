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

using FamilyTreeProject.GEDCOM.Tests.Common;

using NUnit.Framework;

namespace FamilyTreeProject.GEDCOM.Tests
{
    /// <summary>
    ///   Summary description for GEDCOMTests
    /// </summary>
    [TestFixture]
    public class GEDCOMDocumentTestsIndividual : GEDCOMTestBase
    {
        protected override string FilePath => Path.Combine(base.FilePath, "GEDCOMDocumentTests");

        #region SelectIndividualRecord

        [Test]
        [TestCase("OneIndividual", 1)]
        [TestCase("TwoIndividuals", 1)]
        [TestCase("TwoIndividuals", 2)]
        public void GEDCOMDocument_SelectIndividualRecord_Returns_Individual_When_Given_Valid_Id(string fileName, int recordNo)
        {
            //Arrange
            var document = new GEDCOMDocument();
            document.LoadGEDCOM(GetFileString(fileName));
            string id = String.Format("@I{0}@", recordNo);

            //Act
            var record = document.SelectIndividualRecord(id);

            //Assert
            Assert.IsNotNull(record);
            Assert.AreEqual(record.Id, id);
        }

        [Test]
        [TestCase("OneIndividual", 2)]
        [TestCase("TwoIndividuals", 3)]
        public void GEDCOMDocument_SelectIndividualRecord_Returns_Null_When_Given_InValid_Id(string fileName, int recordNo)
        {
            //Arrange
            var document = new GEDCOMDocument();
            document.LoadGEDCOM(GetFileString(fileName));
            string id = String.Format("@I{0}@", recordNo);

            //Act
            var record = document.SelectIndividualRecord(id);

            //Assert
            Assert.IsNull(record);
        }

        #endregion

    }
}