//******************************************
//  Copyright (C) 2014-2015 Charles Nurse  *
//                                         *
//  Licensed under MIT License             *
//  (see included LICENSE)                 *
//                                         *
// *****************************************

using System.Text;

using FamilyTreeProject.GEDCOM.Common;
using FamilyTreeProject.GEDCOM.Records;
using FamilyTreeProject.GEDCOM.Structures;

using NUnit.Framework;

namespace FamilyTreeProject.GEDCOM.Tests.Common
{
    public class GEDCOMAssert
    {
        public static void EventStructureIsEqual(GEDCOMEventStructure expectedEventStructure, GEDCOMEventStructure actualEventStructure)
        {
            Assert.AreEqual(expectedEventStructure.Date, actualEventStructure.Date);
            Assert.IsInstanceOf(typeof (GEDCOMPlaceStructure), actualEventStructure.Place);
            Assert.AreEqual(expectedEventStructure.Place.Place, actualEventStructure.Place.Place);
        }

        public static void FamilyIsEqual(GEDCOMFamilyRecord expectedRecord, GEDCOMFamilyRecord actualRecord)
        {
            Assert.AreEqual(expectedRecord.Id, actualRecord.Id);

            //Assert values for FamilyRecord
            Assert.AreEqual(expectedRecord.Husband, actualRecord.Husband);
            Assert.AreEqual(expectedRecord.Wife, actualRecord.Wife);
            Assert.AreEqual(expectedRecord.NoChildren, actualRecord.NoChildren);
            Assert.AreEqual(expectedRecord.Events.Count, actualRecord.Events.Count);

            for (int i = 0; i < expectedRecord.Events.Count; i++)
            {
                EventStructureIsEqual(expectedRecord.Events[i], actualRecord.Events[i]);
            }
        }

        public static void HeaderIsEqual(GEDCOMHeaderRecord expectedRecord, GEDCOMHeaderRecord actualRecord)
        {
            HeaderSourceStructureIsEqual(expectedRecord.Source, actualRecord.Source);
            Assert.AreEqual(expectedRecord.Destination, actualRecord.Destination);
            Assert.AreEqual(expectedRecord.TransmissionDate, actualRecord.TransmissionDate);
            Assert.AreEqual(expectedRecord.CharacterSet, actualRecord.CharacterSet);
            Assert.AreEqual(expectedRecord.FileName, actualRecord.FileName);
            Assert.AreEqual(expectedRecord.GEDCOMForm, actualRecord.GEDCOMForm);
            Assert.AreEqual(expectedRecord.GEDCOMVersion, actualRecord.GEDCOMVersion);
            Assert.AreEqual(expectedRecord.Submitter, actualRecord.Submitter);
        }

        public static void HeaderSourceStructureIsEqual(GEDCOMHeaderSourceStructure expectedSourceStructure, GEDCOMHeaderSourceStructure actualSourceStructure)
        {
            Assert.AreEqual(expectedSourceStructure.SystemId, actualSourceStructure.SystemId);
            Assert.AreEqual(expectedSourceStructure.Version, actualSourceStructure.Version);
            Assert.AreEqual(expectedSourceStructure.ProductName, actualSourceStructure.ProductName);
            Assert.AreEqual(expectedSourceStructure.Company, actualSourceStructure.Company);
            Assert.AreEqual(expectedSourceStructure.Address.Address, actualSourceStructure.Address.Address);
        }

        public static void IndividualIsEqual(GEDCOMIndividualRecord expectedRecord, GEDCOMIndividualRecord actualRecord)
        {
            Assert.AreEqual(expectedRecord.Id, actualRecord.Id);
            //Assert.IsTrue(record.HasChildren);

            //Assert values for IndividualRecord
            Assert.AreEqual(expectedRecord.Name.GivenName, actualRecord.Name.GivenName);
            Assert.AreEqual(expectedRecord.Name.LastName, actualRecord.Name.LastName);
            Assert.AreEqual(expectedRecord.Sex, actualRecord.Sex);
            Assert.AreEqual(expectedRecord.Events.Count, actualRecord.Events.Count);

            for (int i = 0; i < expectedRecord.Events.Count; i++)
            {
                EventStructureIsEqual(expectedRecord.Events[i], actualRecord.Events[i]);
            }
        }

        public static void IsValidHeader(GEDCOMHeaderRecord actualRecord)
        {
            Assert.AreEqual(GEDCOMTag.HEAD, actualRecord.TagName);
            Assert.AreEqual(0, actualRecord.Level);
            Assert.IsTrue(actualRecord.HasChildren);
        }

        public static void IsValidFamily(GEDCOMFamilyRecord actualRecord)
        {
            Assert.AreEqual(GEDCOMTag.FAM, actualRecord.TagName);
            Assert.AreEqual(0, actualRecord.Level);
        }

        public static void IsValidIndividual(GEDCOMIndividualRecord actualRecord)
        {
            Assert.AreEqual(GEDCOMTag.INDI, actualRecord.TagName);
            Assert.AreEqual(0, actualRecord.Level);
        }

        public static void IsValidRecord(GEDCOMRecord record, int level, string data, bool hasChildren, int childCount)
        {
            Assert.IsNotNull(record);
            Assert.AreEqual(level, record.Level);
            Assert.AreEqual(data, record.Data);

            if (hasChildren)
            {
                Assert.IsTrue(record.HasChildren);
                Assert.AreEqual(childCount, record.ChildRecords.Count);
            }
            else
            {
                Assert.IsFalse(record.HasChildren);
            }
        }

        public static void IsValidOutput(string expectedString, StringBuilder actualSb)
        {
            IsValidOutput(expectedString, actualSb.ToString());
        }

        public static void IsValidOutput(string expectedString, string actualString)
        {
            string[] actual = actualString.Split('\n');
            string[] expected = expectedString.Split('\n');

            Assert.AreEqual(expected.Length, actual.Length);

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }
    }
}