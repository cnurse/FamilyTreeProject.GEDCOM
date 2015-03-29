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
using System.Text;

using FamilyTreeProject.GEDCOM.Common;
using FamilyTreeProject.GEDCOM.IO;
using FamilyTreeProject.GEDCOM.Records;
using FamilyTreeProject.GEDCOM.Tests.Common;

using NUnit.Framework;

namespace FamilyTreeProject.GEDCOM.Tests
{
    /// <summary>
    ///   Summary description for GEDCOMTests
    /// </summary>
    [TestFixture]
    public class GEDCOMWriterTests : GEDCOMTestBase
    {
        #region Protected Properties

        protected override string EmbeddedFilePath
        {
            get { return "FamilyTreeProject.GEDCOM.Tests.TestFiles.GEDCOMWriterTests"; }
        }

        #endregion

        #region Create

        [Test]
        public void GEDCOMWriter_Create_Throws_Exception_If_Stream_Parameter_Is_Null()
        {
            Stream s = GetEmbeddedFileStream("InvalidFileName");
            Assert.Throws<ArgumentNullException>(() => GEDCOMWriter.Create(s));
        }

        [Test]
        public void GEDCOMWriter_Create_Throws_Exception_If_TextWriter_Parameter_Is_Null()
        {
            StringWriter stringWriter = null;
            Assert.Throws<ArgumentNullException>(() => GEDCOMWriter.Create(stringWriter));
        }

        [Test]
        public void GEDCOMWriter_Create_Throws_Exception_If_StringBuilder_Parameter_Is_Null()
        {
            StringBuilder sb = null;
            Assert.Throws<ArgumentNullException>(() => GEDCOMWriter.Create(sb));
        }

        [Test]
        public void GEDCOMWriter_Create_Creates_Instance_Of_Writer_If_Stream_Parameter_Is_Valid()
        {
            Stream s = new MemoryStream();
            GEDCOMWriter writer = GEDCOMWriter.Create(s);

            Assert.IsInstanceOfType(typeof (GEDCOMWriter), writer);
        }

        [Test]
        public void GEDCOMWriter_Create_Creates_Instance_Of_Writer_If_TextWriter_Parameter_Is_Valid()
        {
            StringWriter stringWriter = new StringWriter(new StringBuilder());
            GEDCOMWriter writer = GEDCOMWriter.Create(stringWriter);

            Assert.IsInstanceOfType(typeof (GEDCOMWriter), writer);
        }

        [Test]
        public void GEDCOMWriter_Create_Creates_Instance_Of_Writer_If_StringBuilder_Parameter_Is_Valid()
        {
            StringBuilder sb = new StringBuilder();
            GEDCOMWriter writer = GEDCOMWriter.Create(sb);

            Assert.IsInstanceOfType(typeof (GEDCOMWriter), writer);
        }

        #endregion

        #region WriteData

        [Test]
        public void GEDCOMWriter_WriteData_Correctly_Writes_Provided_Text()
        {
            StringBuilder sb = new StringBuilder();
            GEDCOMWriter writer = GEDCOMWriter.Create(sb);

            writer.WriteData("Data");

            Assert.AreEqual("Data", sb.ToString());
        }

        #endregion

        #region WriteId

        [Test]
        public void GEDCOMWriter_WriteId_Correctly_Writes_Provided_Id()
        {
            StringBuilder sb = new StringBuilder();
            GEDCOMWriter writer = GEDCOMWriter.Create(sb);

            writer.WriteId("@I001@");

            Assert.AreEqual("@I001@", sb.ToString());
        }

        [Test]
        public void GEDCOMWriter_WriteId_Correctly_Writes_Provided_Id_And_Prefix()
        {
            StringBuilder sb = new StringBuilder();
            GEDCOMWriter writer = GEDCOMWriter.Create(sb);

            writer.WriteId(1, "I");

            Assert.AreEqual("@I1@", sb.ToString());
        }

        #endregion

        #region WriteLevel

        [Test]
        public void GEDCOMWriter_WriteLevel_Correctly_Writes_Provided_Level()
        {
            StringBuilder sb = new StringBuilder();
            GEDCOMWriter writer = GEDCOMWriter.Create(sb);

            writer.WriteLevel(2);

            Assert.AreEqual("2", sb.ToString());
        }

        #endregion

        #region WriteTag

        [Test]
        public void GEDCOMWriter_WriteTag_Correctly_Writes_Provided_Tag()
        {
            StringBuilder sb = new StringBuilder();
            GEDCOMWriter writer = GEDCOMWriter.Create(sb);

            writer.WriteTag("INDI");

            Assert.AreEqual("INDI", sb.ToString());
        }

        [Test]
        public void GEDCOMWriter_WriteTag_Correctly_Writes_Provided_TagName()
        {
            StringBuilder sb = new StringBuilder();
            GEDCOMWriter writer = GEDCOMWriter.Create(sb);

            writer.WriteTag(GEDCOMTag.INDI);

            Assert.AreEqual("INDI", sb.ToString());
        }

        #endregion

        #region WriteXRefId

        [Test]
        public void GEDCOMWriter_WriteXRefId_Correctly_Writes_Provided_Id()
        {
            StringBuilder sb = new StringBuilder();
            GEDCOMWriter writer = GEDCOMWriter.Create(sb);

            writer.WriteXRefId("@I001@");

            Assert.AreEqual("@I001@", sb.ToString());
        }

        [Test]
        public void GEDCOMWriter_WriteXRefId_Correctly_Writes_Provided_Id_And_Prefix()
        {
            StringBuilder sb = new StringBuilder();
            GEDCOMWriter writer = GEDCOMWriter.Create(sb);

            writer.WriteXRefId(1, "I");

            Assert.AreEqual("@I1@", sb.ToString());
        }

        #endregion

        #region WriteRecord

        [Test]
        public void GEDCOMWriter_WriteRecord_Correctly_Writes_Provided_Level_Id_And_Tag()
        {
            StringBuilder sb = new StringBuilder();
            GEDCOMWriter writer = GEDCOMWriter.Create(sb);
            writer.NewLine = "\n";

            writer.WriteRecord("@I001@", 0, "", "INDI", "");

            Assert.AreEqual("0 @I001@ INDI\n", sb.ToString());
        }

        [Test]
        public void GEDCOMWriter_WriteRecord_Correctly_Writes_Provided_Level_Tag_And_Data()
        {
            StringBuilder sb = new StringBuilder();
            GEDCOMWriter writer = GEDCOMWriter.Create(sb);
            writer.NewLine = "\n";

            writer.WriteRecord("", 1, "", "NAME", "John /Smith/");

            Assert.AreEqual("1 NAME John /Smith/\n", sb.ToString());
        }

        [Test]
        public void GEDCOMWriter_WriteRecord_Correctly_Writes_Provided_Level_Tag_And_XRefId()
        {
            StringBuilder sb = new StringBuilder();
            GEDCOMWriter writer = GEDCOMWriter.Create(sb);
            writer.NewLine = "\n";

            writer.WriteRecord("", 2, "@N002@", "NOTE", "");

            Assert.AreEqual("2 NOTE @N002@\n", sb.ToString());
        }

        [Test]
        public void GEDCOMWriter_WriterRecord_Correctly_Renders_Header_Record()
        {
            StringBuilder sb = new StringBuilder();
            GEDCOMWriter writer = GEDCOMWriter.Create(sb);
            writer.NewLine = "\n";

            GEDCOMHeaderRecord record = Util.CreateHeaderRecord("Header");

            //write Header
            writer.WriteRecord(record, false);

            Assert.AreEqual("0 HEAD\n", sb.ToString());
        }

        [Test]
        public void GEDCOMWriter_WriterRecord_Correctly_Renders_Family_Record_And_Children()
        {
            var sb = new StringBuilder();
            GEDCOMWriter writer = GEDCOMWriter.Create(sb);
            writer.NewLine = "\n";

            GEDCOMFamilyRecord record = Util.CreateFamilyRecord(1);

            //write Header
            writer.WriteRecord(record);

            //Assert
            GEDCOMAssert.IsValidOutput(GetEmbeddedFileString("OneFamily"), sb);
        }

        [Test]
        public void GEDCOMWriter_WriterRecord_Correctly_Renders_Header_Record_And_Children()
        {
            var sb = new StringBuilder();
            GEDCOMWriter writer = GEDCOMWriter.Create(sb);
            writer.NewLine = "\n";

            GEDCOMHeaderRecord record = Util.CreateHeaderRecord("Header");

            //write Header
            writer.WriteRecord(record);

            //Assert
            GEDCOMAssert.IsValidOutput(GetEmbeddedFileString("Header"), sb);
        }

        [Test]
        public void GEDCOMWriter_WriterRecord_Correctly_Renders_Individual_Record_And_Children()
        {
            var sb = new StringBuilder();
            GEDCOMWriter writer = GEDCOMWriter.Create(sb);
            writer.NewLine = "\n";

            GEDCOMIndividualRecord record = Util.CreateIndividualRecord(1);

            //write Header
            writer.WriteRecord(record);

            //Assert
            GEDCOMAssert.IsValidOutput(GetEmbeddedFileString("OneIndividual"), sb);
        }

        #endregion

        #region WriteRecords

        [Test]
        public void GEDCOMWriter_WriterRecords_Correctly_Renders_Two_Family_Records()
        {
            var sb = new StringBuilder();
            GEDCOMWriter writer = GEDCOMWriter.Create(sb);
            writer.NewLine = "\n";

            GEDCOMRecordList individuals = Util.CreateFamilyRecords();

            //write Individuals
            writer.WriteRecords(individuals);

            //Assert
            GEDCOMAssert.IsValidOutput(GetEmbeddedFileString("TwoFamilies"), sb);
        }

        [Test]
        public void GEDCOMWriter_WriterRecords_Correctly_Renders_Two_Individual_Records()
        {
            var sb = new StringBuilder();
            GEDCOMWriter writer = GEDCOMWriter.Create(sb);
            writer.NewLine = "\n";

            GEDCOMRecordList individuals = Util.CreateIndividualRecords();

            //write Individuals
            writer.WriteRecords(individuals);

            //Assert
            GEDCOMAssert.IsValidOutput(GetEmbeddedFileString("TwoIndividuals"), sb);
        }

        #endregion

        protected override Stream GetEmbeddedFileStream(string fileName)
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceStream(GetEmbeddedFileName(fileName));
        }

    }
}