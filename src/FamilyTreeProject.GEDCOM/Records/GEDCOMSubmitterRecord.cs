using System;
using System.Collections.Generic;

using FamilyTreeProject.GEDCOM.Common;
using FamilyTreeProject.GEDCOM.Structures;
#pragma warning disable 1570

namespace FamilyTreeProject.GEDCOM.Records
{
    /// <summary>
    ///   The GEDCOMSubmitterRecord Class models a GEDCOM Submitter Record.
    /// </summary>
    /// <remarks>
    ///   <h2>GEDCOM 5.5 Submitter Record</h2>
    ///   n @XREF:SUBN@ SUBM                               {1:1} <br />
    ///     +1 NAME <SUBMITTER_NAME>                       {1:1} - Name<br />
    ///     +1 <<ADDRESS_STRUCTURE>>                       {0:1} - Address<br />
    ///     +1 <<MULTIMEDIA_LINK>>                         {0:M} - <i>see GEDCOMBaseRecord - Multimedia</i><br />
    ///     +1 LANG <LANGUAGE_PREFERENCE>                  {0:3} - Languages<br />
    ///     +1 RFN <SUBMITTER_REGISTERED_RFN>              {0:1} - RegisteredID<br />
    ///     +1 RIN <AUTOMATED_RECORD_ID>                   {0:1} - <i>see GEDCOMBaseRecord - AutomatedRecordID</i><br />
    ///     +1 <<CHANGE_DATE>>                             {0:1} - <i>see GEDCOMBaseRecord - ChangeDate</i><br />
    /// </remarks>
    public class GEDCOMSubmitterRecord : GEDCOMBaseRecord
    {
        #region Constructors

        /// <summary>
        ///   Constructs a GEDCOMSubmitterRecord from a GEDCOMRecord
        /// </summary>
        /// <param name = "record">a GEDCOMRecord</param>
        public GEDCOMSubmitterRecord(GEDCOMRecord record) : base(record)
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets the Address of the submitter
        /// </summary>
        public GEDCOMAddressStructure Address
        {
            get { return ChildRecords.GetLineByTag<GEDCOMAddressStructure>(GEDCOMTag.ADDR); }
        }

        /// <summary>
        ///   Gets a List of Languages
        /// </summary>
        public List<string> Languages
        {
            get
            {
                List<GEDCOMRecord> languageRecords = ChildRecords.GetLinesByTag<GEDCOMRecord>(GEDCOMTag.LANG);
                List<string> languages = new List<string>();

                foreach (GEDCOMRecord languageRecord in languageRecords)
                {
                    if (!String.IsNullOrEmpty(languageRecord.Data))
                    {
                        languages.Add(languageRecord.Data);
                    }
                }

                return languages;
            }
        }

        /// <summary>
        ///   Gets the Name of the submitter
        /// </summary>
        public string Name
        {
            get { return ChildRecords.GetRecordData(GEDCOMTag.NAME); }
        }

        /// <summary>
        ///   Gets the Registered ID number of the submitter
        /// </summary>
        public GEDCOMExternalIDStructure RegisteredID
        {
            get { return ChildRecords.GetLineByTag<GEDCOMExternalIDStructure>(GEDCOMTag.RFN); }
        }

        #endregion
    }
}