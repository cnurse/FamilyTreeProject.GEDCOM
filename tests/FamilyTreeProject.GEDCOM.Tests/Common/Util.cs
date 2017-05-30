//******************************************
//  Copyright (C) 2014-2015 Charles Nurse  *
//                                         *
//  Licensed under MIT License             *
//  (see included LICENSE)                 *
//                                         *
// *****************************************

using System;

using FamilyTreeProject.Common;
using FamilyTreeProject.GEDCOM.Common;
using FamilyTreeProject.GEDCOM.Records;
using FamilyTreeProject.GEDCOM.Structures;
using NUnit.Framework.Constraints;

namespace FamilyTreeProject.GEDCOM.Tests.Common
{
    public class Util
    {
        public const string IND_FirstName = "Foo{0}";
        public const string IND_LastName = "Bar";
        public const string IND_AltLastName = "Car";

        public const string NOTE_text = "New Notes {0}";

        public const string NOTE_long_header_text =
            "== '''Augustin Ernest (Agustín Ernesto) Lemonnier:''' ==   Nació en Francia entre los años 1862, 1863 y 1864,  probablemente en alguna ciudad " +
            "de la región de Normandía, de donde es originario el apellido Lemonnier y donde han habitado y habitan la gran mayoría de personas con este apellido.  Llegó al puerto de " +
            "Chala (Caravelí, Arequipa, Perú) aproximadamente en 1883, según dice su acta de matrimonio.";        

        public static readonly DateTime NOTE_DATETIME = new DateTime(2017, 4, 19, 16, 45, 35);

        public static readonly string USER_Attributes = "Attributes = " +
        "<3c3f786d 6c207665 7273696f 6e3d2231 2e302220 656e636f 64696e67 3d225554 " +
        "462d3822 3f3e0a3c 21444f43 54595045 20706c69 73742050 55424c49 4320222d " +
        "2f2f4170 706c652f 2f445444 20504c49 53542031 2e302f2f 454e2220 22687474 " +
        "703a2f2f 7777772e 6170706c 652e636f 6d2f4454 44732f50 726f7065 7274794c " +
        "6973742d 312e302e 64746422 3e0a3c70 6c697374 20766572 73696f6e 3d22312e " +
        "30223e0a 3c646963 743e0a09 3c6b6579 3e42726f 77736572 20507269 6e74496e " +
        "666f3c2f 6b65793e 0a093c64 6174613e 0a094241 747a6448 4a6c5957 31306558 " +
        "426c5a49 486f4134 51425149 53456841 744f5531 42796157 35305357 356d6277 " +
        "47456841 684f5530 3969616d 566a6441 43466b6f 53450a09 68424e4f 55303131 " +
        "64474669 62475645 61574e30 61573975 59584a35 41495345 44453554 52476c6a " +
        "64476c76 626d4679 65514355 68414670 48704b45 68495149 546c4e54 0a096448 " +
        "4a70626d 63426c49 51424b77 6c4f5531 42796157 35305a58 4b476b6f 53456841 " +
        "6c4f5531 42796157 35305a58 49416c4a 4b456d5a 6b424949 61476b6f 535a6d52 " +
        "524f0a09 55315a6c 636e5270 59324673 5547466e 61573568 64476c76 626f6153 " +
        "68495345 43453554 546e5674 596d5679 41495345 42303554 566d4673 64575541 " +
        "6c495142 4b6f5358 0a096c77 43476b6f 535a6d52 5a4f5530 6876636d 6c366232 " +
        "35305957 78736555 4e6c626e 526c636d 566b6870 4b456e35 32456841 466a6e67 " +
        "47476b6f 535a6d52 524f5531 5a6c0a09 636e5270 59324673 62486c44 5a573530>";


        public static readonly string USER_Settings = "Settings = " + 
            "<3c3f786d 6c207665 7273696f 6e3d2231 2e302220 656e636f 64696e67 3d225554 " +
            "462d3822 3f3e0a3c 21444f43 54595045 20706c69 73742050 55424c49 4320222d " +
            "2f2f4170 706c652f 2f445444 20504c49 53542031 2e302f2f 454e2220 22687474 " +
            "703a2f2f 7777772e 6170706c 652e636f 6d2f4454 44732f50 726f7065 7274794c " +
            "6973742d 312e302e 64746422 3e0a3c70 6c697374 20766572 73696f6e 3d22312e " +
            "30223e0a 3c646963 743e0a09 3c6b6579 3e4a414e 53656c65 63746564 416c6275 " +
            "6d3c2f6b 65793e0a 093c7374 72696e67 3e46414d 3c2f7374 72696e67 3e0a3c2f " +
            "64696374 3e0a3c2f 706c6973 743e0a>";

        public static GEDCOMFamilyRecord CreateFamilyRecord(int recordNo)
        {
            var family = new GEDCOMFamilyRecord(recordNo);
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
            var source = new GEDCOMHeaderSourceStructure("FTM");
            source.Company = "The Generations Network";
            source.ProductName = "Family Tree Maker for Windows";
            source.Version = "Family Tree Maker (17.0.0.416)";
            record.Source = source;

            var address = new GEDCOMAddressStructure(source.Level + 2);
            address.Address = "360 W 4800 N\nProvo, UT 84604";
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
            var individual = new GEDCOMIndividualRecord(recordNo);
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
                case 4:
                    name = new GEDCOMNameStructure("Augustin Ernest /Lemonnier/", individual.Level + 1);
                    individual.Name = name;
                    individual.Sex = Sex.Male;
                    birthEvent = new GEDCOMEventStructure(individual.Level + 1, "BIRT", "1863", "Francia");
                    deathEvent = new GEDCOMEventStructure(individual.Level + 1, "DEAT", "BET 1900 AND 1904", "Chile o Perú");
                    individual.ChildRecords.Add(birthEvent);
                    individual.ChildRecords.Add(deathEvent);
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

        public static GEDCOMUserDefinedStructure CreateUserDefinedStructure(int recordNo)
        {
            string tag = string.Empty;
            string data = string.Empty;

            switch (recordNo)
            {
                case 1:
                    tag = "NSDATA";
                    data = USER_Attributes;
                    break;
                case 2:
                    tag = "NSDATA";
                    data = USER_Settings;
                    break;
                case 3:
                    tag = "UID";
                    data = "40C34B57-FB82-4780-8940-3E3AE4D91668";
                    break;
                case 4:
                    tag = "UPD";
                    data = "22 SEP 2011 11:41:04 GMT-5";
                    break;
            }

            return new GEDCOMUserDefinedStructure(tag, 1, data);
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

        public static GEDCOMSourceRecord CreateSourceRecord(int recordNo, int? repoId = null, int level = 0)
        {            
            var source = new GEDCOMSourceRecord(level, recordNo, "New Source");
            source.ChildRecords.Add(GEDCOMExternalIDStructure.CreateUserReference(NOTE_DATETIME.ToString("dd MMM yyyy HH:mm:ss").ToUpper(), "Creation Date", level + 1));
            source.ChildRecords.Add(new GEDCOMStructure(level + 1, string.Empty, string.Empty, "_GCID", "AD13971A-678C-438B-996D-786258F0A96F"));

            if (repoId != null)
            {
                source.ChildRecords.Add(new GEDCOMAssociationStructure(level + 1, $"R{repoId}", "REPO"));
            }

            source.ChildRecords.Add(new GEDCOMChangeDateStructure(level + 1, NOTE_DATETIME));

            return source;
        }

        public static GEDCOMNoteRecord CreateNoteRecord(int recordNo, int? sourceId = null, int level = 0)
        {
            GEDCOMNoteRecord note;

            switch (recordNo)
            {
                case 1:
                    note = new GEDCOMNoteRecord(recordNo)
                    {
                        Level = level
                    };

                    note.ChildRecords.Add(new GEDCOMNoteStructure(level + 1, "CONC", string.Format(NOTE_text, recordNo)));
                    note.ChildRecords.Add(GEDCOMExternalIDStructure.CreateUserReference(NOTE_DATETIME.ToString("dd MMM yyyy HH:mm:ss").ToUpper(), "Creation Date", level + 1));
                    note.ChildRecords.Add(new GEDCOMStructure(level + 1, string.Empty, string.Empty, "_GCID", "05DB1416-C53E-429E-98FE-725E12DDED2D"));
                    note.ChildRecords.Add(new GEDCOMChangeDateStructure(level + 1, NOTE_DATETIME));

                    if (sourceId != null)
                    {
                        note.ChildRecords.Add(new GEDCOMAssociationStructure(level + 1, $"SR{sourceId}", "SOUR"));
                    }
                    break;
                case 2:
                    note = new GEDCOMNoteRecord(new GEDCOMRecord(level, null, null, "NOTE", NOTE_long_header_text));                    
                    break;
                default:
                    throw new ArgumentOutOfRangeException();                    
            }
            

            return note;
        }

        public static GEDCOMRepositoryRecord CreateRepoRecord(int recordNo, int level = 0)
        {
            var repo = new GEDCOMRepositoryRecord(level, recordNo, "New Repository", "123 Nowhere St");
            repo.ChildRecords.Add(GEDCOMExternalIDStructure.CreateUserReference(NOTE_DATETIME.ToString("dd MMM yyyy HH:mm:ss").ToUpper(), "Creation Date", level + 1));
            repo.ChildRecords.Add(new GEDCOMStructure(level + 1, string.Empty, string.Empty, "_GCID", "EFF36040-4598-4A8E-BFF0-10D4159B2458"));
            repo.ChildRecords.Add(new GEDCOMChangeDateStructure(level + 1, NOTE_DATETIME));

            return repo;
        }
    }
}
 