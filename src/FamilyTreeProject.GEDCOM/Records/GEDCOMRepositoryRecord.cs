//******************************************
//  Copyright (C) 2014-2015 Charles Nurse  *
//                                         *
//  Licensed under MIT License             *
//  (see included LICENSE)                 *
//                                         *
// *****************************************

using FamilyTreeProject.GEDCOM.Common;
using FamilyTreeProject.GEDCOM.Structures;

namespace FamilyTreeProject.GEDCOM.Records
{
    ///<summary>
    ///  The GEDCOMRepositoryRecord Class models a Genealogical Repository Record.
    ///</summary>
    ///<remarks>
    ///  <h2>GEDCOM 5.5 Repository Record</h2>
    ///  n  @<XREF:REPO>@ REPO                          {1:1} <i>see GEDCOMRecord</i><br />
    ///    +1 NAME <NAME_OF_REPOSITORY>                 {0:1} - Name<br />
    ///    +1 <<ADDRESS_STRUCTURE>>                     {0:1} - Address<br />
    ///    +1 <<NOTE_STRUCTURE>>                        {0:M} - <i>see GEDCOMBaseRecord - Notes</i><br />
    ///    +1 REFN <USER_REFERENCE_NUMBER>              {0:M} - <i>see GEDCOMBaseRecord - UserDefinedIDs</i><br />
    ///      +2 TYPE <USER_REFERENCE_TYPE>              {0:1} - <br />
    ///    +1 RIN <AUTOMATED_RECORD_ID>                 {0:1} - <i>see GEDCOMBaseRecord - AutomatedRecordID</i><br />
    ///    +1 <<CHANGE_DATE>>                           {0:1} - <i>see GEDCOMBaseRecord - ChangeDate</i><br />
    ///</remarks>
    public class GEDCOMRepositoryRecord : GEDCOMBaseRecord
    {
        /// <summary>
        ///   Constructs a GEDCOMRepositoryRecord from a GEDCOMRecord
        /// </summary>
        /// <param name = "record">a GEDCOMRecord</param>
        public GEDCOMRepositoryRecord(GEDCOMRecord record) : base(record)
        {
        }

        /// <summary>
        ///   Gets the Address of the Repository
        /// </summary>
        public GEDCOMAddressStructure Address
        {
            get { return ChildRecords.GetLineByTag<GEDCOMAddressStructure>(GEDCOMTag.ADDR); }
        }

        /// <summary>
        ///   Gets the Name of the Repository
        /// </summary>
        public string Name
        {
            get { return ChildRecords.GetRecordData(GEDCOMTag.NAME); }
        }
    }
}