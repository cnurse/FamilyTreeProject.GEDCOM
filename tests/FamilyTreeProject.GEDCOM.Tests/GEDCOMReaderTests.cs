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
    public class GEDCOMReaderTests : GEDCOMTestBase
    {
        #region Protected Properties

        protected override string EmbeddedFilePath
        {
            get { return "FamilyTreeProject.GEDCOM.Tests.TestFiles.GEDCOMReaderTests"; }
        }

        #endregion

        #region Create

        [Test]
        public void GEDCOMReader_Create_Throws_Exception_If_Stream_Parameter_Is_Null()
        {
            Stream s = GetEmbeddedFileStream("InvalidFileName");
            Assert.Throws<ArgumentNullException>(() => GEDCOMReader.Create(s));
        }

        [Test]
        public void GEDCOMReader_Create_Throws_Exception_If_TextReader_Parameter_Is_Null()
        {
            StringReader reader = null;
            Assert.Throws<ArgumentNullException>(() => GEDCOMReader.Create(reader));
        }

        [Test]
        public void GEDCOMReader_Create_Throws_Exception_If_String_Parameter_Is_Null()
        {
            String text = null;
            Assert.Throws<ArgumentNullException>(() => GEDCOMReader.Create(text));
        }

        #endregion

        #region MoveToFamily

        [Test]
        public void GEDCOMReader_MoveToFamily_Returns_False_If_No_Family_Record()
        {
            GEDCOMReader reader;
            using (Stream s = GetEmbeddedFileStream("NoRecords"))
            {
                reader = GEDCOMReader.Create(s);
                bool moveResult = reader.MoveToFamily();

                //Assert that Move failed
                Assert.IsFalse(moveResult);
            }
        }

        [Test]
        public void GEDCOMReader_MoveToFamily_Returns_True_If_At_Least_One_Family_Record()
        {
            GEDCOMReader reader;
            using (Stream s = GetEmbeddedFileStream("OneFamily"))
            {
                reader = GEDCOMReader.Create(s);
                bool moveResult = reader.MoveToFamily();

                //Assert that Move was successful
                Assert.IsTrue(moveResult);
            }
        }

        [Test]
        public void GEDCOMReader_MoveToFamily_Returns_False_If_No_More_Family_Records()
        {
            GEDCOMReader reader;
            using (Stream s = GetEmbeddedFileStream("OneFamily"))
            {
                reader = GEDCOMReader.Create(s);
                bool moveResult = reader.MoveToFamily();

                reader.ReadRecord();

                moveResult = reader.MoveToFamily();

                //Assert that Move failed
                Assert.IsFalse(moveResult);
            }
        }

        #endregion

        #region MoveToHeader

        [Test]
        public void GEDCOMReader_MoveToHeader_Returns_False_If_No_Header_Record()
        {
            GEDCOMReader reader;
            using (Stream s = GetEmbeddedFileStream("Empty"))
            {
                reader = GEDCOMReader.Create(s);
                bool moveResult = reader.MoveToHeader();

                //Assert that Move failed
                Assert.IsFalse(moveResult);
            }
        }

        [Test]
        public void GEDCOMReader_MoveToHeader_Returns_True_If_At_Least_One_Header_Record()
        {
            GEDCOMReader reader;
            using (Stream s = GetEmbeddedFileStream("OneIndividual"))
            {
                reader = GEDCOMReader.Create(s);
                bool moveResult = reader.MoveToHeader();

                //Assert that Move was successful
                Assert.IsTrue(moveResult);
            }
        }

        [Test]
        public void GEDCOMReader_MoveToHeader_Returns_False_If_Past_Header()
        {
            GEDCOMReader reader;
            using (Stream s = GetEmbeddedFileStream("OneIndividual"))
            {
                reader = GEDCOMReader.Create(s);
                bool moveResult = reader.MoveToIndividual();

                reader.ReadRecord();

                moveResult = reader.MoveToHeader();

                //Assert that Move failed
                Assert.IsFalse(moveResult);
            }
        }

        #endregion

        #region MoveToIndividual

        [Test]
        public void GEDCOMReader_MoveToIndividual_Returns_False_If_No_Individual_Record()
        {
            GEDCOMReader reader;
            using (Stream s = GetEmbeddedFileStream("NoRecords"))
            {
                reader = GEDCOMReader.Create(s);
                bool moveResult = reader.MoveToIndividual();

                //Assert that Move failed
                Assert.IsFalse(moveResult);
            }
        }

        [Test]
        public void GEDCOMReader_MoveToIndividual_Returns_True_If_At_Least_One_Individual_Record()
        {
            GEDCOMReader reader;
            using (Stream s = GetEmbeddedFileStream("OneIndividual"))
            {
                reader = GEDCOMReader.Create(s);
                bool moveResult = reader.MoveToIndividual();

                //Assert that Move was successful
                Assert.IsTrue(moveResult);
            }
        }

        [Test]
        public void GEDCOMReader_MoveToIndividual_Returns_False_If_No_More_Individual_Records()
        {
            GEDCOMReader reader;
            using (Stream s = GetEmbeddedFileStream("OneIndividual"))
            {
                reader = GEDCOMReader.Create(s);
                bool moveResult = reader.MoveToIndividual();

                reader.ReadRecord();

                moveResult = reader.MoveToIndividual();

                //Assert that Move failed
                Assert.IsFalse(moveResult);
            }
        }

        #endregion

        #region MoveToRecord

        [Test]
        public void GEDCOMReader_MoveToRecord_Returns_False_If_No_Records()
        {
            GEDCOMReader reader;
            using (Stream s = GetEmbeddedFileStream("Empty"))
            {
                reader = GEDCOMReader.Create(s);
                bool moveResult = reader.MoveToRecord();

                //Assert that Move failed
                Assert.IsFalse(moveResult);
            }
        }

        [Test]
        public void GEDCOMReader_MoveToRecord_Returns_True_If_At_Least_One_Record()
        {
            GEDCOMReader reader;
            using (Stream s = GetEmbeddedFileStream("OneIndividual"))
            {
                reader = GEDCOMReader.Create(s);
                bool moveResult = reader.MoveToRecord();

                //Assert that Move was successful
                Assert.IsTrue(moveResult);
            }
        }

        [Test]
        public void GEDCOMReader_MoveToRecord_Returns_False_If_No_More_Records()
        {
            GEDCOMReader reader;
            GEDCOMRecord record;
            using (Stream s = GetEmbeddedFileStream("OneIndividual"))
            {
                reader = GEDCOMReader.Create(s);
                bool moveResult = reader.MoveToRecord();

                record = reader.ReadRecord(); //HEAD
                record = reader.ReadRecord(); //SUBM
                record = reader.ReadRecord(); //INDI
                record = reader.ReadRecord(); //TRLR

                moveResult = reader.MoveToRecord();

                //Assert that Move failed
                Assert.IsFalse(moveResult);
            }
        }

        #endregion

        #region Read

        [Test]
        [TestCase("Empty", 0)]
        [TestCase("NoRecords", 3)]
        [TestCase("OneIndividual", 4)]
        [TestCase("TwoIndividuals", 5)]
        public void GEDCOMReader_Read_Reads_Correct_Count_Of_Records_From_String(string fileName, int expectedCount)
        {
            string text = GetEmbeddedFileString(fileName);
            GEDCOMReader reader = GEDCOMReader.Create(text);
            Assert.AreEqual(expectedCount, reader.Read().Count);
        }

        [Test]
        [TestCase("Empty", 0)]
        [TestCase("NoRecords", 3)]
        [TestCase("OneIndividual", 4)]
        [TestCase("TwoIndividuals", 5)]
        public void GEDCOMReader_Read_Reads_Correct_Count_Of_Records_From_Stream(string fileName, int expectedCount)
        {
            GEDCOMReader reader;
            using (Stream s = GetEmbeddedFileStream(fileName))
            {
                reader = GEDCOMReader.Create(s);

                Assert.AreEqual(expectedCount, reader.Read().Count);
            }
        }

        [Test]
        [TestCase("Empty", 0)]
        [TestCase("NoRecords", 3)]
        [TestCase("OneIndividual", 4)]
        [TestCase("TwoIndividuals", 5)]
        public void GEDCOMReader_Read_Reads_Correct_Count_Of_Records_From_TextReader(string fileName, int expectedCount)
        {
            string text = GetEmbeddedFileString(fileName);
            GEDCOMReader reader;
            using (StringReader stringReader = new StringReader(text))
            {
                reader = GEDCOMReader.Create(stringReader);

                Assert.AreEqual(expectedCount, reader.Read().Count);
            }
        }

        [Test]
        [TestCase("NoRecords", 0, GEDCOMTag.HEAD)]
        [TestCase("NoRecords", 1, GEDCOMTag.SUBM)]
        [TestCase("NoRecords", 2, GEDCOMTag.TRLR)]
        [TestCase("OneIndividual", 0, GEDCOMTag.HEAD)]
        [TestCase("OneIndividual", 1, GEDCOMTag.SUBM)]
        [TestCase("OneIndividual", 2, GEDCOMTag.INDI)]
        [TestCase("OneIndividual", 3, GEDCOMTag.TRLR)]
        [TestCase("TwoIndividuals", 0, GEDCOMTag.HEAD)]
        [TestCase("TwoIndividuals", 1, GEDCOMTag.SUBM)]
        [TestCase("TwoIndividuals", 2, GEDCOMTag.INDI)]
        [TestCase("TwoIndividuals", 3, GEDCOMTag.INDI)]
        [TestCase("TwoIndividuals", 4, GEDCOMTag.TRLR)]
        public void GEDCOMReader_Read_Reads_Correct_Records(string fileName, int recordNo, GEDCOMTag tag)
        {
            GEDCOMReader reader;
            GEDCOMRecordList records;
            using (Stream s = GetEmbeddedFileStream(fileName))
            {
                reader = GEDCOMReader.Create(s);
                records = reader.Read();
            }
            Assert.AreEqual(tag, records[recordNo].TagName);
        }

        [Test]
        [TestCase("OneIndividual", 2, 4)]
        [TestCase("TwoIndividuals", 2, 4)]
        [TestCase("TwoIndividuals", 3, 3)]
        public void GEDCOMReader_Read_Reads_Correct_Count_Of_ChildRecords(string fileName, int recordNo, int expectedCount)
        {
            GEDCOMReader reader;
            GEDCOMRecordList records;
            using (Stream s = GetEmbeddedFileStream(fileName))
            {
                reader = GEDCOMReader.Create(s);
                records = reader.Read();
            }
            GEDCOMRecord record = records[recordNo];

            Assert.AreEqual(expectedCount, record.ChildRecords.Count);
        }

        [Test]
        [TestCase("OneIndividual", 2, 0, GEDCOMTag.NAME)]
        [TestCase("OneIndividual", 2, 1, GEDCOMTag.SEX)]
        [TestCase("OneIndividual", 2, 2, GEDCOMTag.BIRT)]
        [TestCase("OneIndividual", 2, 3, GEDCOMTag.DEAT)]
        [TestCase("TwoIndividuals", 2, 0, GEDCOMTag.NAME)]
        [TestCase("TwoIndividuals", 2, 1, GEDCOMTag.SEX)]
        [TestCase("TwoIndividuals", 2, 2, GEDCOMTag.BIRT)]
        [TestCase("TwoIndividuals", 2, 3, GEDCOMTag.DEAT)]
        [TestCase("TwoIndividuals", 3, 0, GEDCOMTag.NAME)]
        [TestCase("TwoIndividuals", 3, 1, GEDCOMTag.SEX)]
        [TestCase("TwoIndividuals", 3, 2, GEDCOMTag.BIRT)]
        public void GEDCOMReader_Read_Reads_Correct_ChildRecords(string fileName, int recordNo, int childRecordNo, GEDCOMTag tag)
        {
            GEDCOMReader reader;
            GEDCOMRecordList records;
            using (Stream s = GetEmbeddedFileStream(fileName))
            {
                reader = GEDCOMReader.Create(s);
                records = reader.Read();
            }
            GEDCOMRecord record = records[recordNo];

            Assert.AreEqual(tag, record.ChildRecords[childRecordNo].TagName);
        }

        [Test]
        public void GEDCOMReader_Read_Reads_Record_If_One_Individual_Record()
        {
            GEDCOMReader reader;
            GEDCOMRecordList records;
            using (Stream s = GetEmbeddedFileStream("TwoIndividuals"))
            {
                reader = GEDCOMReader.Create(s);
                records = reader.Read();
            }

            //Get first real record
            GEDCOMRecord record = records[2];

            //Assert that Name record is valid
            GEDCOMRecord nameRecord = record.ChildRecords.GetLineByTag(GEDCOMTag.NAME);
            GEDCOMAssert.IsValidRecord(nameRecord, 1, "John /Smith/", false, -1);

            //Assert that Sex record is valid
            GEDCOMRecord sexRecord = record.ChildRecords.GetLineByTag(GEDCOMTag.SEX);
            GEDCOMAssert.IsValidRecord(sexRecord, 1, "M", false, -1);

            //Assert that Birth record is valid
            GEDCOMRecord birthRecord = record.ChildRecords.GetLineByTag(GEDCOMTag.BIRT);
            GEDCOMAssert.IsValidRecord(birthRecord, 1, "", true, 2);
            Assert.AreEqual(GEDCOMTag.DATE, birthRecord.ChildRecords[0].TagName);
            Assert.AreEqual(GEDCOMTag.PLAC, birthRecord.ChildRecords[1].TagName);

            GEDCOMRecord dateRecord = birthRecord.ChildRecords.GetLineByTag(GEDCOMTag.DATE);
            GEDCOMAssert.IsValidRecord(dateRecord, 2, "10 Apr 1964", false, -1);

            GEDCOMRecord placeRecord = birthRecord.ChildRecords.GetLineByTag(GEDCOMTag.PLAC);
            GEDCOMAssert.IsValidRecord(placeRecord, 2, "AnyTown", false, -1);

            //Assert that Death record is valid
            GEDCOMRecord deathRecord = record.ChildRecords.GetLineByTag(GEDCOMTag.DEAT);
            GEDCOMAssert.IsValidRecord(deathRecord, 1, "", true, 2);
            Assert.AreEqual(GEDCOMTag.DATE, deathRecord.ChildRecords[0].TagName);
            Assert.AreEqual(GEDCOMTag.PLAC, deathRecord.ChildRecords[1].TagName);

            dateRecord = deathRecord.ChildRecords.GetLineByTag(GEDCOMTag.DATE);
            GEDCOMAssert.IsValidRecord(dateRecord, 2, "15 May 1998", false, -1);

            placeRecord = birthRecord.ChildRecords.GetLineByTag(GEDCOMTag.PLAC);
            GEDCOMAssert.IsValidRecord(placeRecord, 2, "AnyTown", false, -1);
        }

        #endregion

        #region ReadFamilies

        [Test]
        [TestCase("NoRecords", 0)]
        [TestCase("OneFamily", 1)]
        [TestCase("TwoFamilies", 2)]
        public void GEDCOMReader_ReadFamilies_Returns_A_List_Of_GEDCOMFamilyRecords(string fileName, int recordCount)
        {
            //Arrange
            GEDCOMReader reader;
            GEDCOMRecordList records;

            //Act
            using (Stream s = GetEmbeddedFileStream(fileName))
            {
                reader = GEDCOMReader.Create(s);
                records = reader.ReadFamilies();
            }

            //Assert
            Assert.AreEqual(recordCount, records.Count);

            for (int i = 0; i < records.Count; i++)
            {
                Assert.IsInstanceOf<GEDCOMFamilyRecord>(records[i]);
            }
        }

        #endregion

        #region ReadFamily

        [Test]
        public void GEDCOMReader_ReadFamily_Returns_Null_If_No_Family_Records()
        {
            GEDCOMReader reader;
            GEDCOMFamilyRecord record;
            using (Stream s = GetEmbeddedFileStream("NoRecords"))
            {
                reader = GEDCOMReader.Create(s);
                record = reader.ReadFamily();
            }
            //Assert base record values
            Assert.IsNull(record);
        }

        [Test]
        [TestCase("OneFamily", 2)]
        [TestCase("TwoFamilies", 3)]
        public void GEDCOMReader_ReadFamily_Returns_No_More_Individual_Records(string fileName, int recordNo)
        {
            GEDCOMReader reader;
            GEDCOMFamilyRecord record = null;
            using (Stream s = GetEmbeddedFileStream(fileName))
            {
                reader = GEDCOMReader.Create(s);
                for (int i = 0; i < recordNo; i++)
                {
                    record = reader.ReadFamily();
                }
            }
            //Assert base record values
            Assert.IsNull(record);
        }

        [Test]
        [TestCase("OneFamily", 1)]
        [TestCase("TwoFamilies", 1)]
        [TestCase("TwoFamilies", 2)]
        public void GEDCOMReader_ReadFamily_Returns_Correct_GEDCOMFamilyRecord(string fileName, int recordNo)
        {
            GEDCOMReader reader;
            GEDCOMFamilyRecord actualRecord = null;
            GEDCOMFamilyRecord expectedRecord = Util.CreateFamilyRecord(recordNo);
            using (Stream s = GetEmbeddedFileStream(fileName))
            {
                reader = GEDCOMReader.Create(s);
                for (int i = 0; i < recordNo; i++)
                {
                    actualRecord = reader.ReadFamily();
                }
            }

            GEDCOMAssert.IsValidFamily(actualRecord);
            GEDCOMAssert.FamilyIsEqual(expectedRecord, actualRecord);
        }

        #endregion

        #region ReadHeader

        [Test]
        public void GEDCOMReader_ReadHeader_Returns_Null_If_No_Header_Record()
        {
            GEDCOMReader reader;
            GEDCOMHeaderRecord record;
            using (Stream s = GetEmbeddedFileStream("Empty"))
            {
                reader = GEDCOMReader.Create(s);
                record = reader.ReadHeader();
            }
            //Assert base record values
            Assert.IsNull(record);
        }

        [Test]
        [TestCase("NoRecords")]
        [TestCase("OneIndividual")]
        [TestCase("TwoIndividuals")]
        public void GEDCOMReader_ReadHeader_Returns_GEDCOMHeaderRecord(string fileName)
        {
            GEDCOMReader reader;
            GEDCOMHeaderRecord actualRecord;
            GEDCOMHeaderRecord expectedRecord = Util.CreateHeaderRecord(fileName);

            using (Stream s = GetEmbeddedFileStream(fileName))
            {
                reader = GEDCOMReader.Create(s);
                actualRecord = reader.ReadHeader();
            }

            GEDCOMAssert.IsValidHeader(actualRecord);
            GEDCOMAssert.HeaderIsEqual(expectedRecord, actualRecord);
        }

        #endregion

        #region ReadIndividual

        [Test]
        public void GEDCOMReader_ReadIndividual_Returns_Null_If_No_Individual_Records()
        {
            GEDCOMReader reader;
            GEDCOMIndividualRecord record;
            using (Stream s = GetEmbeddedFileStream("NoRecords"))
            {
                reader = GEDCOMReader.Create(s);
                record = reader.ReadIndividual();
            }
            //Assert base record values
            Assert.IsNull(record);
        }

        [Test]
        [TestCase("OneIndividual", 2)]
        [TestCase("TwoIndividuals", 3)]
        public void GEDCOMReader_ReadIndividual_Returns_No_More_Individual_Records(string fileName, int recordNo)
        {
            GEDCOMReader reader;
            GEDCOMIndividualRecord record = null;
            using (Stream s = GetEmbeddedFileStream(fileName))
            {
                reader = GEDCOMReader.Create(s);
                for (int i = 0; i < recordNo; i++)
                {
                    record = reader.ReadIndividual();
                }
            }
            //Assert base record values
            Assert.IsNull(record);
        }

        [Test]
        [TestCase("OneIndividual", 1)]
        [TestCase("TwoIndividuals", 1)]
        [TestCase("TwoIndividuals", 2)]
        public void GEDCOMReader_ReadIndividual_Returns_Correct_GEDCOMIndividualRecord(string fileName, int recordNo)
        {
            GEDCOMReader reader;
            GEDCOMIndividualRecord actualRecord = null;
            GEDCOMIndividualRecord expectedRecord = Util.CreateIndividualRecord(recordNo);
            using (Stream s = GetEmbeddedFileStream(fileName))
            {
                reader = GEDCOMReader.Create(s);
                for (int i = 0; i < recordNo; i++)
                {
                    actualRecord = reader.ReadIndividual();
                }
            }

            GEDCOMAssert.IsValidIndividual(actualRecord);
            GEDCOMAssert.IndividualIsEqual(expectedRecord, actualRecord);
        }

        #endregion

        #region ReadIndividuals

        [Test]
        [TestCase("NoRecords", 0)]
        [TestCase("OneIndividual", 1)]
        [TestCase("TwoIndividuals", 2)]
        public void GEDCOMReader_ReadIndividual_Returns_A_List_Of_GEDCOMIndividualRecords(string fileName, int recordCount)
        {
            //Arrange
            GEDCOMReader reader;
            GEDCOMRecordList records;

            //Act
            using (Stream s = GetEmbeddedFileStream(fileName))
            {
                reader = GEDCOMReader.Create(s);
                records = reader.ReadIndividuals();
            }

            //Assert
            Assert.AreEqual(recordCount, records.Count);

            for (int i = 0; i < records.Count; i++)
            {
                Assert.IsInstanceOf<GEDCOMIndividualRecord>(records[i]);
            }
        }

        #endregion

        protected override Stream GetEmbeddedFileStream(string fileName)
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceStream(GetEmbeddedFileName(fileName));
        }

    }
}