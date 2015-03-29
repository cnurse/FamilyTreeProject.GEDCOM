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
    ///  The GEDCOMEventStructure Class models Genealogical Event Records and
    ///  Attribute Records.
    ///</summary>
    ///<remarks>
    ///  <h2>GEDCOM 5.5 Event Structure</h2>
    ///  n  [ BIRT | CHR ] [Y|<NULL>]                    {1:1} <br />
    ///                         +1 <<EVENT_DETAIL>>                         {0:1} <i>see Below</i><br />
    ///                               +1 FAMC @<XREF:FAM>@                        {0:1}
    ///                                          n  [ DEAT | BURI | CREM ] [Y|<NULL>]            {1:1} <br />
    ///                                                                         +1 <<EVENT_DETAIL>>                         {0:1} <i>see Below</i><br />
    ///                                                                               n  ADOP [Y|<NULL>]                              {1:1} <br />
    ///                                                                                            +1 <<EVENT_DETAIL>>                         {0:1} <i>see Below</i><br />
    ///                                                                                                  +1 FAMC @<XREF:FAM>@                        {0:1}
    ///                                                                                                             +2 ADOP <ADOPTED_BY_WHICH_PARENT>       {0:1}
    ///                                                                                                                       n  [ BAPM | BARM | BASM | BLES ] [Y|<NULL>]     {1:1} <br />
    ///                                                                                                                                                             +1 <<EVENT_DETAIL>>                         {0:1} <i>see Below</i><br />
    ///                                                                                                                                                                   n  [ CHRA | CONF | FCOM | ORDN ] [Y|<NULL>]     {1:1} <br />
    ///                                                                                                                                                                                                         +1 <<EVENT_DETAIL>>                         {0:1} <i>see Below</i><br />
    ///                                                                                                                                                                                                               n  [ NATU | EMIG | IMMI ] [Y|<NULL>]            {1:1} <br />
    ///                                                                                                                                                                                                                                              +1 <<EVENT_DETAIL>>                         {0:1} <i>see Below</i><br />
    ///                                                                                                                                                                                                                                                    n  [ CENS | PROB | WILL] [Y|<NULL>]             {1:1} <br />
    ///                                                                                                                                                                                                                                                                                  +1 <<EVENT_DETAIL>>                         {0:1} <i>see Below</i><br />
    ///                                                                                                                                                                                                                                                                                        n  [ GRAD | RETI ] [Y|<NULL>]                   {1:1} <br />
    ///                                                                                                                                                                                                                                                                                                                +1 <<EVENT_DETAIL>>                         {0:1} <i>see Below</i><br />
    ///                                                                                                                                                                                                                                                                                                                      n  [ ANUL | CENS | DIV | DIVF ] [Y|<NULL>]      {1:1} <br />
    ///                                                                                                                                                                                                                                                                                                                                                           +1 <<EVENT_DETAIL>>                         {0:1} <i>see Below</i><br />
    ///                                                                                                                                                                                                                                                                                                                                                                 n  [ ENGA | MARR | MARB | MARC ] [Y|<NULL>]     {1:1} <br />
    ///                                                                                                                                                                                                                                                                                                                                                                                                       +1 <<EVENT_DETAIL>>                         {0:1} <i>see Below</i><br />
    ///                                                                                                                                                                                                                                                                                                                                                                                                             n  [ MARL | MARS ] [Y|<NULL>]                   {1:1} <br />
    ///                                                                                                                                                                                                                                                                                                                                                                                                                                     +1 <<EVENT_DETAIL>>                         {0:1} <i>see Below</i><br />
    ///                                                                                                                                                                                                                                                                                                                                                                                                                                           n  EVEN                                         {1:1} <br />
    ///                                                                                                                                                                                                                                                                                                                                                                                                                                           +1 <<EVENT_DETAIL>>                         {0:1} <i>see Below</i><br />
    ///
    ///                                                                                                                                                                                                                                                                                                                                                                                                                                                 <h2>GEDCOM 5.5 Attribute Structure</h2>
    ///                                                                                                                                                                                                                                                                                                                                                                                                                                                 n  CAST <CASTE_NAME>                  {1:1} <br />
    ///                                                                                                                                                                                                                                                                                                                                                                                                                                                           +1 <<EVENT_DETAIL>>               {0:1} <i>see Below</i><br />
    ///                                                                                                                                                                                                                                                                                                                                                                                                                                                                 n  DSCR <PHYSICAL_DESCRIPTION>        {1:1} <br />
    ///                                                                                                                                                                                                                                                                                                                                                                                                                                                                           +1 <<EVENT_DETAIL>>               {0:1} <i>see Below</i><br />
    ///                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 n  EDUC <SCHOLASTIC_ACHIEVEMENT>      {1:1} <br />
    ///                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           +1 <<EVENT_DETAIL>>               {0:1} <i>see Below</i><br />
    ///                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 n  IDNO <NATIONAL_ID_NUMBER>          {1:1}*<br />
    ///                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           +1 <<EVENT_DETAIL>>               {0:1} <i>see Below</i><br />
    ///                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 n  NATI <NATIONAL_OR_TRIBAL_ORIGIN>   {1:1} <br />
    ///                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           +1 <<EVENT_DETAIL>>               {0:1} <i>see Below</i><br />
    ///                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 n  NCHI <COUNT_OF_CHILDREN>           {1:1} <br />
    ///                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           +1 <<EVENT_DETAIL>>               {0:1} <i>see Below</i><br />
    ///                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 n  NMR <COUNT_OF_MARRIAGES>           {1:1} <br />
    ///                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          +1 <<EVENT_DETAIL>>               {0:1} <i>see Below</i><br />
    ///                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                n  OCCU <OCCUPATION>                  {1:1} <br />
    ///                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          +1 <<EVENT_DETAIL>>               {0:1} <i>see Below</i><br />
    ///                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                n  PROP <POSSESSIONS>                 {1:1} <br />
    ///                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          +1 <<EVENT_DETAIL>>               {0:1} <i>see Below</i><br />
    ///                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                n  RELI <RELIGIOUS_AFFILIATION>       {1:1} <br />
    ///                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          +1 <<EVENT_DETAIL>>               {0:1} <i>see Below</i><br />
    ///                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                n  RESI                               {1:1} <br />
    ///                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                +1 <<EVENT_DETAIL>>               {0:1} <i>see Below</i><br />
    ///                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      n  SSN <SOCIAL_SECURITY_NUMBER>       {0:1} <br />
    ///                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               +1 <<EVENT_DETAIL>>               {0:1} <i>see Below</i><br />
    ///                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     n  TITL <NOBILITY_TYPE_TITLE>         {1:1} <br />
    ///                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               +1 <<EVENT_DETAIL>>               {0:1} <i>see Below</i><br />
    ///
    ///                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     <h3>GEDCOM 5.5 Event Detail</h3>
    ///                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     n  TYPE <EVENT_DESCRIPTOR>          {0:1} - TypeDetail<br />
    ///                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               n  DATE <DATE_VALUE>                {0:1} - Date<br />
    ///                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         n  <<PLACE_STRUCTURE>>              {0:1} - Place<br />
    ///                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               n  <<ADDRESS_STRUCTURE>>            {0:1} - Address/Phones<br />
    ///                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     n  AGE <AGE_AT_EVENT>               {0:1} - Age<br />
    ///                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              n  AGNC <RESPONSIBLE_AGENCY>        {0:1} - Agency<br />
    ///                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        n  CAUS <CAUSE_OF_EVENT>            {0:1} - Cause<br />
    ///                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  n  <<SOURCE_CITATION>>              {0:M} - <i>see GEDCOMStructure - SourceCitations</i><br />
    ///                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        n  <<MULTIMEDIA_LINK>>              {0:M} - <i>see GEDCOMStructure - Multimedia</i><br />
    ///                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              n  <<NOTE_STRUCTURE>>               {0:M} - <i>see GEDCOMStructure - Notes</i><br />
    ///</remarks>
    public class GEDCOMEventStructure : GEDCOMStructure
    {
        private readonly EventClass eventClass = EventClass.Unknown;

        #region Constructors

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
            this.eventClass = eventClass;
        }

        public GEDCOMEventStructure(int level, string tag, string date, string place) : base(new GEDCOMRecord(level, "", "", tag, ""))
        {
            var dateRecord = new GEDCOMRecord(level + 1, "", "", "DATE", date);
            ChildRecords.Add(dateRecord);

            var placeRecord = new GEDCOMPlaceStructure(level + 1, place);
            ChildRecords.Add(placeRecord);
        }

        #endregion

        #region Public Properties

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
            get { return eventClass; }
        }

        /// <summary>
        ///   Gets the Type of the event
        /// </summary>
        public FamilyEventType FamilyEventType
        {
            get
            {
                if (EventClass == EventClass.Family)
                {
                    switch (TagName)
                    {
                        case GEDCOMTag.ANUL:
                            return FamilyEventType.Annulment;
                        case GEDCOMTag.DIV:
                            return FamilyEventType.Divorce;
                        case GEDCOMTag.DIVF:
                            return FamilyEventType.DivorceFiled;
                        case GEDCOMTag.ENGA:
                            return FamilyEventType.Engagement;
                        case GEDCOMTag.MARR:
                            return FamilyEventType.Marriage;
                        case GEDCOMTag.MARB:
                            return FamilyEventType.MarriageBann;
                        case GEDCOMTag.MARC:
                            return FamilyEventType.MarriageContract;
                        case GEDCOMTag.MARL:
                            return FamilyEventType.MarriageLicense;
                        case GEDCOMTag.MARS:
                            return FamilyEventType.MarriageSettlement;
                        case GEDCOMTag.EVEN:
                            return FamilyEventType.Other;
                        default:
                            return FamilyEventType.Unknown;
                    }
                }
                else
                {
                    return FamilyEventType.Unknown;
                }
            }
        }

        /// <summary>
        ///   Gets the Type of the event
        /// </summary>
        public IndividualAttributeType IndividualAttributeType
        {
            get
            {
                if (EventClass == EventClass.Attribute)
                {
                    switch (TagName)
                    {
                        case GEDCOMTag.CAST:
                            return IndividualAttributeType.Caste;
                        case GEDCOMTag.DSCR:
                            return IndividualAttributeType.Attribute;
                        case GEDCOMTag.EDUC:
                            return IndividualAttributeType.Education;
                        case GEDCOMTag.IDNO:
                            return IndividualAttributeType.NationalID;
                        case GEDCOMTag.NATI:
                            return IndividualAttributeType.Nationality;
                        case GEDCOMTag.NCHI:
                            return IndividualAttributeType.Children;
                        case GEDCOMTag.NMR:
                            return IndividualAttributeType.Marriages;
                        case GEDCOMTag.OCCU:
                            return IndividualAttributeType.Occupation;
                        case GEDCOMTag.PROP:
                            return IndividualAttributeType.Property;
                        case GEDCOMTag.RELI:
                            return IndividualAttributeType.Religion;
                        case GEDCOMTag.RESI:
                            return IndividualAttributeType.Residence;
                        case GEDCOMTag.SSN:
                            return IndividualAttributeType.SSN;
                        case GEDCOMTag.TITL:
                            return IndividualAttributeType.Title;
                        default:
                            return IndividualAttributeType.Unknown;
                    }
                }
                else
                {
                    return IndividualAttributeType.Unknown;
                }
            }
        }

        /// <summary>
        ///   Gets the Type of the event
        /// </summary>
        public IndividualEventType IndividualEventType
        {
            get
            {
                if (EventClass == EventClass.Individual)
                {
                    switch (TagName)
                    {
                        case GEDCOMTag.ADOP:
                            return IndividualEventType.Adoption;
                        case GEDCOMTag.BAPM:
                            return IndividualEventType.Baptism;
                        case GEDCOMTag.BARM:
                            return IndividualEventType.BarMitzvah;
                        case GEDCOMTag.BASM:
                            return IndividualEventType.BasMitzvah;
                        case GEDCOMTag.BIRT:
                            return IndividualEventType.Birth;
                        case GEDCOMTag.BLES:
                            return IndividualEventType.Blessing;
                        case GEDCOMTag.BURI:
                            return IndividualEventType.Burial;
                        case GEDCOMTag.CENS:
                            return IndividualEventType.Census;
                        case GEDCOMTag.CHR:
                            return IndividualEventType.Christening;
                        case GEDCOMTag.CHRA:
                            return IndividualEventType.AdultChristening;
                        case GEDCOMTag.CONF:
                            return IndividualEventType.Confirmation;
                        case GEDCOMTag.CREM:
                            return IndividualEventType.Cremation;
                        case GEDCOMTag.DEAT:
                            return IndividualEventType.Death;
                        case GEDCOMTag.EMIG:
                            return IndividualEventType.Emigration;
                        case GEDCOMTag.FCOM:
                            return IndividualEventType.FirstCommunion;
                        case GEDCOMTag.GRAD:
                            return IndividualEventType.Graduation;
                        case GEDCOMTag.IMMI:
                            return IndividualEventType.Immigration;
                        case GEDCOMTag.NATU:
                            return IndividualEventType.Naturalisation;
                        case GEDCOMTag.ORDN:
                            return IndividualEventType.Ordination;
                        case GEDCOMTag.PROB:
                            return IndividualEventType.Probate;
                        case GEDCOMTag.RETI:
                            return IndividualEventType.Retirement;
                        case GEDCOMTag.WILL:
                            return IndividualEventType.Will;
                        case GEDCOMTag.EVEN:
                            return IndividualEventType.Other;
                        default:
                            return IndividualEventType.Unknown;
                    }
                }
                else
                {
                    return IndividualEventType.Unknown;
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

        #endregion
    }
}