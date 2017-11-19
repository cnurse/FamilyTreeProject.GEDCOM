using System;
using System.Linq;
using NUnit.Framework;

namespace FamilyTreeProject.GEDCOM.Tests
{
    /// <summary>
    ///   Summary description for GEDCOMTests
    /// </summary>
    public partial class GEDCOMDocumentTests
    {
        #region SelectChildsFamilyRecord

        [Test]
        [TestCase("OneFamily", null)]
        [TestCase("OneFamily", "")]
        public void GEDCOMDocument_SelectChildsFamilyRecord_Throws_On_Null_Or_Empty_ChildId(string fileName, string childId)
        {
            //Arrange
            var document = new GEDCOMDocument();
            document.LoadGEDCOM(GetEmbeddedFileString(fileName));

            //Act, Assert
            Assert.Throws<ArgumentNullException>(() => document.SelectChildsFamilyRecord(childId));
        }

        [Test]
        [TestCase("OneFamily", "@I3@", "@F1@")]
        [TestCase("TwoFamilies", "@I5@", "@F1@")]
        [TestCase("ThreeFamilies", "@I6@", "@F2@")]
        public void GEDCOMDocument_SelectChildsFamilyRecord_Returns_Family_When_Given_Valid_ChildId(string fileName, string childId, string familyId)
        {
            //Arrange
            var document = new GEDCOMDocument();
            document.LoadGEDCOM(GetEmbeddedFileString(fileName));

            //Act
            var record = document.SelectChildsFamilyRecord(childId);

            //Assert
            Assert.IsNotNull(record);
            Assert.AreEqual(record.Id, familyId);
        }

        [Test]
        [TestCase("OneFamily", "@I4@")]
        [TestCase("TwoFamilies", "@I6@")]
        [TestCase("ThreeFamilies", "@I7@")]
        public void GEDCOMDocument_SelectChildsFamilyRecord_Returns_Null_When_Given_InValid_ChildId(string fileName, string childId)
        {
            //Arrange
            var document = new GEDCOMDocument();
            document.LoadGEDCOM(GetEmbeddedFileString(fileName));

            //Act
            var record = document.SelectChildsFamilyRecord(childId);

            //Assert
            Assert.IsNull(record);
        }

        #endregion

        #region SelectFamilyRecord

        [Test]
        [TestCase("OneFamily", "@F1@")]
        [TestCase("TwoFamilies", "@F1@")]
        [TestCase("TwoFamilies", "@F2@")]
        public void GEDCOMDocument_SelectFamilyRecord_Returns_Family_When_Given_Valid_Id(string fileName, string familyId)
        {
            //Arrange
            var document = new GEDCOMDocument();
            document.LoadGEDCOM(GetEmbeddedFileString(fileName));

            //Act
            var record = document.SelectFamilyRecord(familyId);

            //Assert
            Assert.IsNotNull(record);
            Assert.AreEqual(record.Id, familyId);
        }

        [Test]
        [TestCase("OneFamily", "@F2@")]
        [TestCase("TwoFamilies", "@F3@")]
        public void GEDCOMDocument_SelectFamilyRecord_Returns_Null_When_Given_InValid_Id(string fileName, string familyId)
        {
            //Arrange
            var document = new GEDCOMDocument();
            document.LoadGEDCOM(GetEmbeddedFileString(fileName));

            //Act
            var record = document.SelectFamilyRecord(familyId);

            //Assert
            Assert.IsNull(record);
        }

        [Test]
        [TestCase("OneFamily", null, "@I2@")]
        public void GEDCOMDocument_SelectFamilyRecord_Throws_On_Null_HusbandId(string fileName, string husbandId, string wifeId)
        {
            //Arrange
            var document = new GEDCOMDocument();
            document.LoadGEDCOM(GetEmbeddedFileString(fileName));

            //Act, Assert
            Assert.Throws<ArgumentNullException>(() => document.SelectFamilyRecord(husbandId, wifeId));
        }

        [Test]
        [TestCase("OneFamily", "@I2@", null)]
        public void GEDCOMDocument_SelectFamilyRecord_Throws_On_Null_WifeId(string fileName, string husbandId, string wifeId)
        {
            //Arrange
            var document = new GEDCOMDocument();
            document.LoadGEDCOM(GetEmbeddedFileString(fileName));

            //Act, Assert
            Assert.Throws<ArgumentNullException>(() => document.SelectFamilyRecord(husbandId, wifeId));
        }

        [Test]
        [TestCase("OneFamily", "@I1@", "@I2@", "@F1@")]
        [TestCase("TwoFamilies", "@I1@", "@I2@", "@F1@")]
        [TestCase("TwoFamilies", "@I3@", "@I4@", "@F2@")]
        public void GEDCOMDocument_SelectFamilyRecord_Returns_Family_When_Given_Valid_HusbandId_And_WifeId(string fileName, string husbandId, string wifeId, string familyId)
        {
            //Arrange
            var document = new GEDCOMDocument();
            document.LoadGEDCOM(GetEmbeddedFileString(fileName));

            //Act
            var record = document.SelectFamilyRecord(husbandId, wifeId);

            //Assert
            Assert.IsNotNull(record);
            Assert.AreEqual(record.Id, familyId);
        }

        [Test]
        [TestCase("OneFamily", "@I3@", "@I4@")]
        [TestCase("TwoFamilies", "@I3@", "@I2@")]
        public void GEDCOMDocument_SelectFamilyRecord_Returns_Null_When_Given_InValid_HusbandId_Or_WifeId(string fileName, string husbandId, string wifeId)
        {
            //Arrange
            var document = new GEDCOMDocument();
            document.LoadGEDCOM(GetEmbeddedFileString(fileName));

            //Act
            var record = document.SelectFamilyRecord(husbandId, wifeId);

            //Assert
            Assert.IsNull(record);
        }

        #endregion

        #region SelectFamilyRecords

        [Test]
        [TestCase("OneFamily", null)]
        [TestCase("OneFamily", "")]
        public void GEDCOMDocument_SelectFamilyRecords_Throws_On_Null_Or_Empty_IndividualId(string fileName, string individualId)
        {
            //Arrange
            var document = new GEDCOMDocument();
            document.LoadGEDCOM(GetEmbeddedFileString(fileName));

            //Act, Assert
            Assert.Throws<ArgumentNullException>(() => document.SelectFamilyRecords(individualId));
        }

        [Test]
        [TestCase("OneFamily", "@I1@", 1)]
        [TestCase("OneFamily", "@I2@", 1)]
        [TestCase("OneFamily", "@I3@", 0)]
        [TestCase("TwoFamilies", "@I3@", 1)]
        [TestCase("TwoFamilies", "@I6@", 0)]
        [TestCase("ThreeFamilies", "@I2@", 2)]
        public void GEDCOMDocument_SelectFamilyRecords_Returns_List_Of_Families(string fileName, string individualId, int recordCount)
        {
            //Arrange
            var document = new GEDCOMDocument();
            document.LoadGEDCOM(GetEmbeddedFileString(fileName));

            //Act
            var records = document.SelectFamilyRecords(individualId);

            //Assert
            Assert.AreEqual(recordCount, records.Count());
        }

        [Test]
        [TestCase("OneFamily", "@I1@", "@F1@")]
        [TestCase("TwoFamilies", "@I1@", "@F1@")]
        [TestCase("TwoFamilies", "@I3@", "@F2@")]
        public void GEDCOMDocument_SelectFamilyRecords_Returns_Family_When_Given_Valid_HusbandId(string fileName, string husbandId, string familyId)
        {
            //Arrange
            var document = new GEDCOMDocument();
            document.LoadGEDCOM(GetEmbeddedFileString(fileName));

            //Act
            var record = document.SelectFamilyRecords(husbandId).SingleOrDefault();

            //Assert
            Assert.IsNotNull(record);
            Assert.AreEqual(record.Id, familyId);
        }

        [Test]
        [TestCase("OneFamily", "@I2@", "@F1@")]
        [TestCase("TwoFamilies", "@I2@", "@F1@")]
        [TestCase("TwoFamilies", "@I4@", "@F2@")]
        public void GEDCOMDocument_SelectFamilyRecords_Returns_Family_When_Given_Valid_WifeId(string fileName, string wifeId, string familyId)
        {
            //Arrange
            var document = new GEDCOMDocument();
            document.LoadGEDCOM(GetEmbeddedFileString(fileName));

            //Act
            var record = document.SelectFamilyRecords(wifeId).SingleOrDefault();

            //Assert
            Assert.IsNotNull(record);
            Assert.AreEqual(record.Id, familyId);
        }

        #endregion
    }
}