using System;
using System.IO;
using System.Reflection;
using System.Text;

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
    public partial class GEDCOMDocumentTests : GEDCOMTestBase
    {
        #region Protected Properties

        protected override string EmbeddedFilePath
        {
            get { return "FamilyTreeProject.GEDCOM.Tests.TestFiles.GEDCOMDocumentTests"; }
        }

        #endregion

        #region AddRecord

        [Test]
        public void GEDCOMDocument_AddRecord_Throws_If_Record_IsNull()
        {
            var document = new GEDCOMDocument();

            //Assert
            Assert.Throws<ArgumentNullException>(() => document.AddRecord(null));
        }

        [Test]
        public void GEDCOMDocument_AddRecord_Adds_Record_To_Document()
        {
            //Arrange
            var document = new GEDCOMDocument();
            var record = new GEDCOMIndividualRecord("1");
            int count = document.Records.Count;

            //Act
            document.AddRecord(record);

            //Assert
            Assert.AreEqual(count + 1, document.Records.Count);
        }

        [Test]
        public void GEDCOMDocument_AddRecord_Adds_Record_To_Individuals_Collection()
        {
            //Arrange
            var document = new GEDCOMDocument();
            var record = new GEDCOMIndividualRecord("1");
            int count = document.IndividualRecords.Count;

            //Act
            document.AddRecord(record);

            //Assert
            Assert.AreEqual(count + 1, document.IndividualRecords.Count);
        }

        [Test]
        public void GEDCOMDocument_AddRecord_Adds_HeaderRecord()
        {
            //Arrange
            var document = new GEDCOMDocument();
            var record = new GEDCOMHeaderRecord();

            //Act
            document.AddRecord(record);

            //Assert
            Assert.IsNotNull(document.SelectHeader());
        }

        #endregion

        #region AddRecords

        [Test]
        public void GEDCOMDocument_AddRecords_Exception_If_RecordList_IsNull()
        {
            var document = new GEDCOMDocument();

            //Assert
            Assert.Throws<ArgumentNullException>(() => document.AddRecords(null));
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(5)]
        public void GEDCOMDocument_AddRecords_Add_Records_To_Document(int recordCount)
        {
            //Arrange
            var document = new GEDCOMDocument();
            var records = new GEDCOMRecordList();
            int count = document.Records.Count;

            for (int i = 1; i <= recordCount; i++)
            {
                records.Add(Util.CreateIndividualRecord(i));
            }

            //Act
            document.AddRecords(records);

            //Assert
            Assert.AreEqual(count + recordCount, document.Records.Count);
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(5)]
        public void GEDCOMDocument_AddRecords_Add_Records_To_Individuals_Collection(int recordCount)
        {
            //Arrange
            var document = new GEDCOMDocument();
            var records = new GEDCOMRecordList();
            int count = document.Records.Count;

            for (int i = 1; i <= recordCount; i++)
            {
                records.Add(Util.CreateIndividualRecord(i));
            }

            //Act
            document.AddRecords(records);

            //Assert
            Assert.AreEqual(count + recordCount, document.IndividualRecords.Count);
        }

        #endregion

        #region Load

        [Test]
        public void GEDCOMDocument_Load_Throws_If_Stream_Parameter_IsNull()
        {
            Stream s = GetEmbeddedFileStream("InvalidFileName");
            var document = new GEDCOMDocument();

            //Assert
            Assert.Throws<ArgumentNullException>(() => document.Load(s));
        }

        [Test]
        public void GEDCOMDocument_Load_Throws_If_TextReader_Parameter_IsNull()
        {
            TextReader reader = null;
            var document = new GEDCOMDocument();

            //Assert
            // ReSharper disable once ExpressionIsAlwaysNull
            Assert.Throws<ArgumentNullException>(() => document.Load(reader));
        }

        [Test]
        public void GEDCOMDocument_Load_Throws_If_GEDCOMReader_Parameter_IsNull()
        {
            GEDCOMReader reader = null;
            var document = new GEDCOMDocument();

            //Assert
            // ReSharper disable once ExpressionIsAlwaysNull
            Assert.Throws<ArgumentNullException>(() => document.Load(reader));
        }

        [Test]
        [TestCase("NoRecords", 0)]
        [TestCase("OneIndividual", 1)]
        [TestCase("TwoIndividuals", 2)]
        public void GEDCOMDocument_Load_Loads_Document_With_Individuals_Using_Stream(string fileName, int recordCount)
        {
            //Arrange
            var document = new GEDCOMDocument();

            //Act
            using (Stream s = GetEmbeddedFileStream(fileName))
            {
                document.Load(s);
            }

            //Assert
            Assert.AreEqual(recordCount, document.IndividualRecords.Count);
        }

        [Test]
        [TestCase("NoRecords", 0)]
        [TestCase("OneIndividual", 1)]
        [TestCase("TwoIndividuals", 2)]
        public void GEDCOMDocument_Load_Loads_Document_With_Individuals_Using_TextReader(string fileName, int recordCount)
        {
            //Arrange
            var document = new GEDCOMDocument();

            //Act
            using (Stream s = GetEmbeddedFileStream(fileName))
            {
                document.Load(new StreamReader(s));
            }

            //Assert
            Assert.AreEqual(recordCount, document.IndividualRecords.Count);
        }

        [Test]
        [TestCase("NoRecords")]
        public void GEDCOMDocument_Load_Loads_Document_With_Header_If_Document_Is_WellFormed(string fileName)
        {
            //Arrange
            var document = new GEDCOMDocument();

            //Act
            using (Stream s = GetEmbeddedFileStream(fileName))
            {
                document.Load(new StreamReader(s));
            }

            GEDCOMAssert.IsValidHeader(document.SelectHeader());
            GEDCOMAssert.HeaderIsEqual(Util.CreateHeaderRecord(fileName), document.SelectHeader());
        }

        #endregion

        #region LoadGEDCOM

        [Test]
        [TestCase("NoRecords", 0)]
        [TestCase("OneIndividual", 1)]
        [TestCase("TwoIndividuals", 2)]
        public void GEDCOMDocument_LoadGEDCOM_Loads_Document_With_Individuals(string fileName, int recordCount)
        {
            //Arrange
            var document = new GEDCOMDocument();

            //Act
            document.LoadGEDCOM(GetEmbeddedFileString(fileName));

            //Assert
            Assert.AreEqual(recordCount, document.IndividualRecords.Count);
        }

        #endregion

        #region RemoveRecord

        [Test]
        public void GEDCOMDocument_RemoveRecord_Throws_If_Record_IsNull()
        {
            var document = new GEDCOMDocument();

            //Assert
            Assert.Throws<ArgumentNullException>(() => document.RemoveRecord(null));
        }

        [Test]
        public void GEDCOMDocument_RemoveRecord_Removes_Record_From_Document()
        {
            //Arrange
            var document = new GEDCOMDocument();
            document.AddRecord(Util.CreateHeaderRecord("Header"));
            for (int i = 1; i <= 2; i++)
            {
                document.AddRecord(Util.CreateIndividualRecord(i));
            }

            var record = document.IndividualRecords[1] as GEDCOMIndividualRecord;

            //Act
            document.RemoveRecord(record);

            //Assert
            Assert.AreEqual(2, document.Records.Count);
        }

        [Test]
        public void GEDCOMDocument_RemoveRecord_Removes_Record_From_Individuals_Collection()
        {
            //Arrange
            var document = new GEDCOMDocument();
            document.AddRecord(Util.CreateHeaderRecord("Header"));
            for (int i = 1; i <= 2; i++)
            {
                document.AddRecord(Util.CreateIndividualRecord(i));
            }

            var record = document.IndividualRecords[1] as GEDCOMIndividualRecord;

            //Act
            document.RemoveRecord(record);

            //Assert
            Assert.AreEqual(1, document.IndividualRecords.Count);
        }

        [Test]
        public void GEDCOMDocument_RemoveRecord_Throws_If_Record_Not_Present()
        {
            //Arrange
            var document = new GEDCOMDocument();
            document.AddRecord(Util.CreateHeaderRecord("Header"));
            for (int i = 1; i <= 2; i++)
            {
                document.AddRecord(Util.CreateIndividualRecord(i));
            }

            var record = Util.CreateIndividualRecord(3);

            //Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => document.RemoveRecord(record));
        }

        #endregion

        #region Save

        [Test]
        public void GEDCOMDocument_Save_If_Stream_Parameter_IsNull()
        {
            var s = GetEmbeddedFileStream("InvalidFileName");
            var document = new GEDCOMDocument();

            //Assert
            Assert.Throws<ArgumentNullException>(() => document.Save(s));
        }

        [Test]
        public void GEDCOMDocument_Save_If_TextWriter_Parameter_IsNull()
        {
            TextWriter writer = null;
            var document = new GEDCOMDocument();

            //Assert
            // ReSharper disable once ExpressionIsAlwaysNull
            Assert.Throws<ArgumentNullException>(() => document.Save(writer));
        }

        [Test]
        public void GEDCOMDocument_Save_If_GEDCOMWriter_Parameter_IsNull()
        {
            GEDCOMWriter writer = null;
            var document = new GEDCOMDocument();

            //Assert
            // ReSharper disable once ExpressionIsAlwaysNull
            Assert.Throws<ArgumentNullException>(() => document.Save(writer));
        }

        [Test]
        [TestCase("NoRecordsSave", 0)]
        [TestCase("OneIndividualSave", 1)]
        [TestCase("TwoIndividualsSave", 2)]
        public void GEDCOMDocument_Save_Saves_Document_Using_GEDCOMWriter(string fileName, int recordCount)
        {
            //Arrange
            var document = new GEDCOMDocument();
            document.AddRecord(Util.CreateHeaderRecord(fileName));
            for (int i = 1; i <= recordCount; i++)
            {
                document.AddRecord(Util.CreateIndividualRecord(i));
            }

            var sb = new StringBuilder();
            var writer = GEDCOMWriter.Create(sb);
            writer.NewLine = "\n";

            //Act
            document.Save(writer);

            //Assert
            GEDCOMAssert.IsValidOutput(GetEmbeddedFileString(fileName), sb);
        }

        [Test]
        [TestCase("NoRecordsSave", 0)]
        [TestCase("OneIndividualSave", 1)]
        [TestCase("TwoIndividualsSave", 2)]
        public void GEDCOMDocument_Save_Saves_Document_Using_TextWriter(string fileName, int recordCount)
        {
            //Arrange
            var document = new GEDCOMDocument();
            document.AddRecord(Util.CreateHeaderRecord(fileName));
            for (int i = 1; i <= recordCount; i++)
            {
                document.AddRecord(Util.CreateIndividualRecord(i));
            }

            var sb = new StringBuilder();
            var writer = new StringWriter(sb);
            writer.NewLine = "\n";

            //Act
            document.Save(writer);

            //Assert
            GEDCOMAssert.IsValidOutput(GetEmbeddedFileString(fileName), sb);
        }

        [Test]
        [TestCase("NoRecordsSave", 0)]
        [TestCase("OneIndividualSave", 1)]
        [TestCase("TwoIndividualsSave", 2)]
        public void GEDCOMDocument_Save_Saves_Document_Regardless_Of_Record_Order_And_Type(string fileName, int recordCount)
        {
            //Arrange
            var document = new GEDCOMDocument();
            if (recordCount > 0)
            {
                document.AddRecord(Util.CreateIndividualRecord(1));
            }

            document.AddRecord(Util.CreateHeaderRecord(fileName));

            if (recordCount > 1)
            {
                document.AddRecord(Util.CreateIndividualRecord(2));
            }

            var sb = new StringBuilder();
            var writer = GEDCOMWriter.Create(sb);
            writer.NewLine = "\n";

            //Act
            document.Save(writer);

            //Assert
            GEDCOMAssert.IsValidOutput(GetEmbeddedFileString(fileName), sb);
        }

        #endregion

        #region SaveGEDCOM

        [Test]
        [TestCase("NoRecordsSave", 0, 0)]
        [TestCase("OneIndividualSave", 1, 0)]
        [TestCase("TwoIndividualsSave", 2, 0)]
        [TestCase("OneFamilySave", 2, 1)]
        [TestCase("TwoFamiliesSave", 3, 2)]
        public void GEDCOMDocument_SaveGEDCOM_Saves_Document(string fileName, int individualCount, int familyCount)
        {
            //Arrange
            var document = new GEDCOMDocument();
            document.AddRecord(Util.CreateHeaderRecord(fileName));
            for (int i = 1; i <= individualCount; i++)
            {
                document.AddRecord(Util.CreateIndividualRecord(i));
            }
            for (int i = 1; i <= familyCount; i++)
            {
                document.AddRecord(Util.CreateFamilyRecord(i));
            }

            //Assert
            GEDCOMAssert.IsValidOutput(GetEmbeddedFileString(fileName), document.SaveGEDCOM());
        }

        #endregion

        protected override Stream GetEmbeddedFileStream(string fileName)
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceStream(GetEmbeddedFileName(fileName));
        }


    }
}