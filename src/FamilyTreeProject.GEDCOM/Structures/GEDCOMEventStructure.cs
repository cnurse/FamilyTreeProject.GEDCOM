//******************************************
//  Copyright (C) 2014-2015 Charles Nurse  *
//                                         *
//  Licensed under MIT License             *
//  (see included LICENSE)                 *
//                                         *
// *****************************************

using System;
using System.Collections.Generic;
using FamilyTreeProject.Common;
using FamilyTreeProject.GEDCOM.Common;
using FamilyTreeProject.GEDCOM.Records;

namespace FamilyTreeProject.GEDCOM.Structures
{
    ///<summary>
    ///  The GEDCOMEventStructure Class models Genealogical Fact Records and
    ///  Attribute Records.
    ///</summary>
    ///<remarks>
    ///  <h2>GEDCOM 5.5 Fact Structure</h2>
    ///  n  [ BIRT | CHR ] [Y|<NULL>]                    {1:1} <br />
    ///    +1 <<EVENT_DETAIL>>                           {0:1} <i>see Below</i><br />
    ///    +1 FAMC @<XREF:FAM>@                          {0:1}
    ///  
    ///  n  [ DEAT | BURI | CREM ] [Y|<NULL>]            {1:1} <br />
    ///    +1 <<EVENT_DETAIL>>                           {0:1} <i>see Below</i><br />
    ///  
    ///  n  ADOP [Y|<NULL>]                              {1:1} <br />
    ///    +1 <<EVENT_DETAIL>>                           {0:1} <i>see Below</i><br />
    ///    +1 FAMC @<XREF:FAM>@                          {0:1}
    ///      +2 ADOP <ADOPTED_BY_WHICH_PARENT>           {0:1}
    ///    
    ///  n  [ BAPM | BARM | BASM | BLES ] [Y|<NULL>]     {1:1} <br />
    ///    +1 <<EVENT_DETAIL>>                           {0:1} <i>see Below</i><br />
    ///    
    ///  n  [ CHRA | CONF | FCOM | ORDN ] [Y|<NULL>]     {1:1} <br />
    ///    +1 <<EVENT_DETAIL>>                           {0:1} <i>see Below</i><br />
    ///    
    ///  n  [ NATU | EMIG | IMMI ] [Y|<NULL>]            {1:1} <br />
    ///    +1 <<EVENT_DETAIL>>                           {0:1} <i>see Below</i><br />
    ///    
    ///  n  [ CENS | PROB | WILL] [Y|<NULL>]             {1:1} <br />
    ///    +1 <<EVENT_DETAIL>>                           {0:1} <i>see Below</i><br />
    ///  
    ///  n  [ GRAD | RETI ] [Y|<NULL>]                   {1:1} <br />
    ///    +1 <<EVENT_DETAIL>>                           {0:1} <i>see Below</i><br />
    ///    
    ///  n  [ ANUL | CENS | DIV | DIVF ] [Y|<NULL>]      {1:1} <br />
    ///    +1 <<EVENT_DETAIL>>                           {0:1} <i>see Below</i><br />
    ///  
    ///  n  [ ENGA | MARR | MARB | MARC ] [Y|<NULL>]     {1:1} <br />
    ///    +1 <<EVENT_DETAIL>>                           {0:1} <i>see Below</i><br />
    ///  
    ///  n  [ MARL | MARS ] [Y|<NULL>]                   {1:1} <br />
    ///    +1 <<EVENT_DETAIL>>                           {0:1} <i>see Below</i><br />
    ///  
    ///  n  EVEN                                         {1:1} <br />
    ///    +1 <<EVENT_DETAIL>>                           {0:1} <i>see Below</i><br />
    ///
    ///  <h2>GEDCOM 5.5 Attribute Structure</h2>
    ///  n  CAST <CASTE_NAME>                            {1:1} <br />
    ///    +1 <<EVENT_DETAIL>>                           {0:1} <i>see Below</i><br />
    ///  
    ///  n  DSCR <PHYSICAL_DESCRIPTION>                  {1:1} <br />
    ///    +1 <<EVENT_DETAIL>>                           {0:1} <i>see Below</i><br />
    ///    
    ///  n  EDUC <SCHOLASTIC_ACHIEVEMENT>                {1:1} <br />
    ///    +1 <<EVENT_DETAIL>>                           {0:1} <i>see Below</i><br />
    ///  
    ///  n  IDNO <NATIONAL_ID_NUMBER>                    {1:1}*<br />
    ///    +1 <<EVENT_DETAIL>>                           {0:1} <i>see Below</i><br />
    ///    
    ///  n  NATI <NATIONAL_OR_TRIBAL_ORIGIN>             {1:1} <br />
    ///    +1 <<EVENT_DETAIL>>                           {0:1} <i>see Below</i><br />
    ///    
    ///  n  NCHI <COUNT_OF_CHILDREN>                     {1:1} <br />
    ///    +1 <<EVENT_DETAIL>>                           {0:1} <i>see Below</i><br />
    ///    
    ///  n  NMR <COUNT_OF_MARRIAGES>                     {1:1} <br />
    ///    +1 <<EVENT_DETAIL>>                           {0:1} <i>see Below</i><br />
    ///  
    ///  n  OCCU <OCCUPATION>                            {1:1} <br />
    ///    +1 <<EVENT_DETAIL>>                           {0:1} <i>see Below</i><br />
    ///    
    ///  n  PROP <POSSESSIONS>                           {1:1} <br />
    ///    +1 <<EVENT_DETAIL>>                           {0:1} <i>see Below</i><br />
    ///  
    ///  n  RELI <RELIGIOUS_AFFILIATION>                 {1:1} <br />
    ///    +1 <<EVENT_DETAIL>>                           {0:1} <i>see Below</i><br />
    ///  
    ///  n  RESI                                         {1:1} <br />
    ///    +1 <<EVENT_DETAIL>>                           {0:1} <i>see Below</i><br />
    ///  
    ///  n  SSN <SOCIAL_SECURITY_NUMBER>                 {0:1} <br />
    ///    +1 <<EVENT_DETAIL>>                           {0:1} <i>see Below</i><br />
    ///  
    ///  n  TITL <NOBILITY_TYPE_TITLE>                   {1:1} <br />
    ///    +1 <<EVENT_DETAIL>>                           {0:1} <i>see Below</i><br />
    ///
    ///  <h3>GEDCOM 5.5 Fact Detail</h3>
    ///  n  TYPE <EVENT_DESCRIPTOR>                      {0:1} - TypeDetail<br />
    ///  n  DATE <DATE_VALUE>                            {0:1} - Date<br />
    ///  n  <<PLACE_STRUCTURE>>                          {0:1} - Place<br />
    ///  n  <<ADDRESS_STRUCTURE>>                        {0:1} - Address/Phones<br />
    ///  n  AGE <AGE_AT_EVENT>                           {0:1} - Age<br />
    ///  n  AGNC <RESPONSIBLE_AGENCY>                    {0:1} - Agency<br />
    ///  n  CAUS <CAUSE_OF_EVENT>                        {0:1} - Cause<br />
    ///  n  <<SOURCE_CITATION>>                          {0:M} - <i>see GEDCOMStructure - SourceCitations</i><br />
    ///  n  <<MULTIMEDIA_LINK>>                          {0:M} - <i>see GEDCOMStructure - Multimedia</i><br />
    ///  n  <<NOTE_STRUCTURE>>                           {0:M} - <i>see GEDCOMStructure - Notes</i><br />
    ///</remarks>
    public class GEDCOMEventStructure : GEDCOMStructure
    {
        private readonly EventClass _eventClass = EventClass.Unknown;

        /// <summary>
        ///   Constructs a GEDCOMEventStructure from a GEDCOMRecord
        /// </summary>
        /// <param name = "record">a GEDCOMRecord</param>
        public GEDCOMEventStructure(GEDCOMRecord record) : base(record)
        {
        }

        /// <summary>
        ///   Constructs a GEDCOMEventStructure from a GEDCOMRecord
        /// </summary>
        /// <param name = "record">a GEDCOMRecord</param>
        /// <param name = "eventClass"></param>
        public GEDCOMEventStructure(GEDCOMRecord record, EventClass eventClass) : base(record)
        {
            _eventClass = eventClass;
        }

        public GEDCOMEventStructure(int level, string tag, string date, string place) : base(new GEDCOMRecord(level, "", "", tag, ""))
        {
            var dateRecord = new GEDCOMRecord(level + 1, "", "", "DATE", date);
            ChildRecords.Add(dateRecord);

            var placeRecord = new GEDCOMPlaceStructure(level + 1, place);
            ChildRecords.Add(placeRecord);
        }

        /// <summary>
        ///   Gets the Address connected to this event
        /// </summary>
        public GEDCOMAddressStructure Address
        {
            get { return ChildRecords.GetLineByTag<GEDCOMAddressStructure>(GEDCOMTag.ADDR); }
        }

        /// <summary>
        ///   Gets the Age of the Individual at the time of this event
        /// </summary>
        public string Age
        {
            get { return ChildRecords.GetRecordData(GEDCOMTag.AGE); }
        }

        /// <summary>
        ///   Gets the Responsible Agency for the event
        /// </summary>
        public string Agency
        {
            get { return ChildRecords.GetRecordData(GEDCOMTag.AGNC); }
        }

        /// <summary>
        ///   Gets the Age of the Husband at the time of this event
        /// </summary>
        public string AgeOfHusband
        {
            get
            {
                string age = String.Empty;
                GEDCOMRecord husband = ChildRecords.GetLineByTag(GEDCOMTag.HUSB);
                if (husband != null)
                {
                    age = husband.ChildRecords.GetRecordData(GEDCOMTag.AGE);
                }
                return age;
            }
        }

        /// <summary>
        ///   Gets the Age of the Wife at the time of this event
        /// </summary>
        public string AgeOfWife
        {
            get
            {
                string age = String.Empty;
                GEDCOMRecord wife = ChildRecords.GetLineByTag(GEDCOMTag.WIFE);
                if (wife != null)
                {
                    age = wife.ChildRecords.GetRecordData(GEDCOMTag.AGE);
                }
                return age;
            }
        }

        /// <summary>
        ///   Gets the Cuase of the event
        /// </summary>
        public string Cause
        {
            get { return ChildRecords.GetRecordData(GEDCOMTag.CAUS); }
        }

        /// <summary>
        ///   Gets the Date of the event
        /// </summary>
        public string Date
        {
            get { return ChildRecords.GetRecordData(GEDCOMTag.DATE); }
        }

        /// <summary>
        ///   Gets the Class of the event
        /// </summary>
        public EventClass EventClass
        {
            get { return _eventClass; }
        }

        /// <summary>
        ///   Gets the Type of the event
        /// </summary>
        public FactType FamilyEventType
        {
            get
            {
                if (EventClass == EventClass.Family)
                {
                    switch (TagName)
                    {
                        case GEDCOMTag.ANUL:
                            return FactType.Annulment;
                        case GEDCOMTag.DIV:
                            return FactType.Divorce;
                        case GEDCOMTag.DIVF:
                            return FactType.DivorceFiled;
                        case GEDCOMTag.ENGA:
                            return FactType.Engagement;
                        case GEDCOMTag.MARR:
                            return FactType.Marriage;
                        case GEDCOMTag.MARB:
                            return FactType.MarriageBann;
                        case GEDCOMTag.MARC:
                            return FactType.MarriageContract;
                        case GEDCOMTag.MARL:
                            return FactType.MarriageLicense;
                        case GEDCOMTag.MARS:
                            return FactType.MarriageSettlement;
                        case GEDCOMTag.EVEN:
                            return FactType.Other;
                        default:
                            return FactType.Unknown;
                    }
                }
                else
                {
                    return FactType.Unknown;
                }
            }
        }

        /// <summary>
        ///   Gets the Type of the event
        /// </summary>
        public FactType IndividualAttributeType
        {
            get
            {
                if (EventClass == EventClass.Attribute)
                {
                    switch (TagName)
                    {
                        case GEDCOMTag.CAST:
                            return FactType.Caste;
                        case GEDCOMTag.DSCR:
                            return FactType.Description;
                        case GEDCOMTag.EDUC:
                            return FactType.Education;
                        case GEDCOMTag.IDNO:
                            return FactType.IdNumber;
                        case GEDCOMTag.NATI:
                            return FactType.NationalOrTribalOrigin;
                        case GEDCOMTag.NCHI:
                            return FactType.NoOfChildren;
                        case GEDCOMTag.NMR:
                            return FactType.NoOfMarriages;
                        case GEDCOMTag.OCCU:
                            return FactType.Occupation;
                        case GEDCOMTag.PROP:
                            return FactType.Property;
                        case GEDCOMTag.RELI:
                            return FactType.Religion;
                        case GEDCOMTag.RESI:
                            return FactType.Residence;
                        case GEDCOMTag.SSN:
                            return FactType.SocialSecurityNumber;
                        case GEDCOMTag.TITL:
                            return FactType.Title;
                        default:
                            return FactType.Unknown;
                    }
                }
                else
                {
                    return FactType.Unknown;
                }
            }
        }

        /// <summary>
        ///   Gets the Type of the event
        /// </summary>
        public FactType IndividualEventType
        {
            get
            {
                if (EventClass == EventClass.Individual)
                {
                    switch (TagName)
                    {
                        case GEDCOMTag.ADOP:
                            return FactType.Adoption;
                        case GEDCOMTag.BAPM:
                            return FactType.Baptism;
                        case GEDCOMTag.BARM:
                            return FactType.BarMitzvah;
                        case GEDCOMTag.BASM:
                            return FactType.BasMitzvah;
                        case GEDCOMTag.BIRT:
                            return FactType.Birth;
                        case GEDCOMTag.BLES:
                            return FactType.Blessing;
                        case GEDCOMTag.BURI:
                            return FactType.Burial;
                        case GEDCOMTag.CENS:
                            return FactType.Census;
                        case GEDCOMTag.CHR:
                            return FactType.Christening;
                        case GEDCOMTag.CHRA:
                            return FactType.AdultChristening;
                        case GEDCOMTag.CONF:
                            return FactType.Confirmation;
                        case GEDCOMTag.CREM:
                            return FactType.Cremation;
                        case GEDCOMTag.DEAT:
                            return FactType.Death;
                        case GEDCOMTag.EMIG:
                            return FactType.Emigration;
                        case GEDCOMTag.FCOM:
                            return FactType.FirstCommunion;
                        case GEDCOMTag.GRAD:
                            return FactType.Graduation;
                        case GEDCOMTag.IMMI:
                            return FactType.Immigration;
                        case GEDCOMTag.NATU:
                            return FactType.Naturalisation;
                        case GEDCOMTag.ORDN:
                            return FactType.Ordination;
                        case GEDCOMTag.PROB:
                            return FactType.Probate;
                        case GEDCOMTag.RETI:
                            return FactType.Retirement;
                        case GEDCOMTag.WILL:
                            return FactType.Will;
                        case GEDCOMTag.EVEN:
                            return FactType.Other;
                        default:
                            return FactType.Unknown;
                    }
                }
                else
                {
                    return FactType.Unknown;
                }
            }
        }

        /// <summary>
        ///   Gets a List of PhoneNumbers
        /// </summary>
        public List<string> PhoneNumbers
        {
            get
            {
                List<GEDCOMRecord> phoneRecords = ChildRecords.GetLinesByTag<GEDCOMRecord>(GEDCOMTag.PHON);
                List<string> phoneNumbers = new List<string>();

                foreach (GEDCOMRecord phoneRecord in phoneRecords)
                {
                    if (!String.IsNullOrEmpty(phoneRecord.Data))
                    {
                        phoneNumbers.Add(phoneRecord.Data);
                    }
                }

                return phoneNumbers;
            }
        }

        /// <summary>
        ///   Gets the Place of the event
        /// </summary>
        public GEDCOMPlaceStructure Place
        {
            get { return ChildRecords.GetLineByTag<GEDCOMPlaceStructure>(GEDCOMTag.PLAC); }
        }

        /// <summary>
        ///   Gets the Type Detail of the event (if just defined as an EVEN)
        /// </summary>
        public string TypeDetail
        {
            get { return ChildRecords.GetRecordData(GEDCOMTag.TYPE); }
        }
    }
}