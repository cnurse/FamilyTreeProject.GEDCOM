using System;
using FamilyTreeProject.Core.Common;
using FamilyTreeProject.GEDCOM.Records;
using FamilyTreeProject.GEDCOM.Structures;
// ReSharper disable InconsistentNaming

namespace FamilyTreeProject.GEDCOM.Tests.Common
{
    public class Util
    {
        public const string IND_FirstName = "Foo{0}";
        public const string IND_LastName = "Bar";
        public const string IND_AltLastName = "Car";


        public static GEDCOMFamilyRecord CreateFamilyRecord(int recordNo)
        {
            var family = new GEDCOMFamilyRecord(recordNo.ToString());
            GEDCOMRecord childRecord;
            GEDCOMEventStructure marrEvent;
            switch (recordNo)
            {
                case 1:
                    childRecord = new GEDCOMRecord(family.Level + 1, "", "@I1@", "HUSB", "");
                    family.ChildRecords.Add(childRecord);
                    childRecord = new GEDCOMRecord(family.Level + 1, "", "@I2@", "WIFE", "");
                    family.ChildRecords.Add(childRecord);
                    marrEvent = new GEDCOMEventStructure(family.Level + 1, "MARR", "11 JUN 1988", "MyTown");
                    family.ChildRecords.Add(marrEvent);
                    break;
                case 2:
                    childRecord = new GEDCOMRecord(family.Level + 1, "", "@I3@", "HUSB", "");
                    family.ChildRecords.Add(childRecord);
                    childRecord = new GEDCOMRecord(family.Level + 1, "", "@I2@", "WIFE", "");
                    family.ChildRecords.Add(childRecord);
                    marrEvent = new GEDCOMEventStructure(family.Level + 1, "MARR", "09 APR 2001", "MyTown");
                    family.ChildRecords.Add(marrEvent);
                    break;
            }

            return family;
        }

        public static GEDCOMRecordList CreateFamilyRecords()
        {
            var families = new GEDCOMRecordList();

            var family = CreateFamilyRecord(1);
            families.Add(family);

            family = CreateFamilyRecord(2);
            families.Add(family);

            return families;
        }

        public static GEDCOMHeaderRecord CreateHeaderRecord(string fileName)
        {
            var record = new GEDCOMHeaderRecord();
            var source = new GEDCOMHeaderSourceStructure("FTM")
            {
                Company = "The Generations Network",
                ProductName = "Family Tree Maker for Windows",
                Version = "Family Tree Maker (17.0.0.416)"
            };
            record.Source = source;

            var address = new GEDCOMAddressStructure(source.Level + 2) {Address = "360 W 4800 N\nProvo, UT 84604"};
            record.Source.Address = address;

            record.Destination = "FTM";
            record.TransmissionDate = "13 December 2008";
            record.CharacterSet = "ANSI";
            record.FileName = String.Format(@"D:\My Projects\Family Tree Project\Tests\FamilyTreeProject.GEDCOM.Tests\TestFiles\{0}.ged", fileName);
            record.Submitter = "@SUBM@";
            record.GEDCOMVersion = "5.5";
            record.GEDCOMForm = "LINEAGE-LINKED";
            record.Submitter = "@SUBM@";

            return record;
        }

        public static GEDCOMIndividualRecord CreateIndividualRecord(int recordNo)
        {
            var individual = new GEDCOMIndividualRecord(recordNo.ToString());
            GEDCOMNameStructure name;
            GEDCOMEventStructure birthEvent;
            GEDCOMEventStructure deathEvent;
            switch (recordNo)
            {
                case 1:
                    name = new GEDCOMNameStructure("John /Smith/", individual.Level + 1);
                    individual.Name = name;
                    individual.Sex = Sex.Male;
                    birthEvent = new GEDCOMEventStructure(individual.Level + 1, "BIRT", "10 Apr 1964", "AnyTown");
                    individual.ChildRecords.Add(birthEvent);
                    deathEvent = new GEDCOMEventStructure(individual.Level + 1, "DEAT", "15 May 1998", "AnyTown");
                    individual.ChildRecords.Add(deathEvent);
                    break;
                case 2:
                    name = new GEDCOMNameStructure("Jane /Doe/", individual.Level + 1);
                    individual.Name = name;
                    individual.Sex = Sex.Female;
                    birthEvent = new GEDCOMEventStructure(individual.Level + 1, "BIRT", "25 May 1967", "MyTown");
                    individual.ChildRecords.Add(birthEvent);
                    break;
                case 3:
                    name = new GEDCOMNameStructure("William /Jones/", individual.Level + 1);
                    individual.Name = name;
                    individual.Sex = Sex.Male;
                    birthEvent = new GEDCOMEventStructure(individual.Level + 1, "BIRT", "31 Mar 1964", "MyTown");
                    individual.ChildRecords.Add(birthEvent);
                    break;
                default:
                    string firstName = String.Format(IND_FirstName, recordNo);
                    string lastName = (recordNo < 5) ? IND_LastName : IND_AltLastName;
                    name = new GEDCOMNameStructure(String.Format("{0} /{1}/", firstName, lastName), individual.Level + 1);
                    individual.Name = name;
                    break;
            }

            return individual;
        }

        public static GEDCOMRecordList CreateIndividualRecords()
        {
            var individuals = new GEDCOMRecordList();

            var individual = CreateIndividualRecord(1);
            individuals.Add(individual);

            individual = CreateIndividualRecord(2);
            individuals.Add(individual);

            return individuals;
        }
    }
}