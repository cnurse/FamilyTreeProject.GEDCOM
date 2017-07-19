//******************************************
//  Copyright (C) 2014-2015 Charles Nurse  *
//                                         *
//  Licensed under MIT License             *
//  (see included LICENSE)                 *
//                                         *
// *****************************************

using System;
using System.Collections.Generic;

using FamilyTreeProject.GEDCOM.Common;
using FamilyTreeProject.GEDCOM.Structures;

namespace FamilyTreeProject.GEDCOM.Records
{
    ///<summary>
    ///  The GEDCOMFamilyRecord Class models a Genealogical Family Record.
    ///</summary>
    ///<remarks>
    ///  <h2>GEDCOM 5.5 Family Record</h2>
    ///  n @{XREF:FAM}@   FAM					{1:1} <i>see GEDCOMRecord</i><br />
    ///    +1 <FAMILY_EVENT_STRUCTURE>			{0:M} - Facts<br />
    ///      +2 HUSB							{0:1} - <br />
    ///        +3 AGE <AGE_AT_EVENT>			{1:1} - <br />
    ///      +2 WIFE							{0:1} - <br />
    ///        +3 AGE <AGE_AT_EVENT>			{1:1} - <br />
    ///   +1 HUSB @{XREF:INDI}@					{0:1} - Husband<br />
    ///   +1 WIFE @{XREF:INDI}@					{0:1} - Wife<br />
    ///   +1 CHIL @{XREF:INDI}@					{0:M} - Children<br />
    ///   +1 NCHI {COUNT_OF_CHILDREN}			{0:1} - NoChildren<br />
    ///   +1 SUBM @{XREF:SUBM}@					{0:M} - Submitters<br />
    ///   +1 <<LDS_SPOUSE_SEALING>>				{0:M} - not implemented<br />
    ///   +1 <<SOURCE_CITATION>>				{0:M} - <i>see GEDCOMBaseRecord - SourceCitations</i><br />
    ///   +1 <<MULTIMEDIA_LINK>>				{0:M} - <i>see GEDCOMBaseRecord - Multimedia</i><br />
    ///   +1 <<NOTE_STRUCTURE>>					{0:M} - <i>see GEDCOMBaseRecord - Notes</i><br />
    ///   +1 REFN <USER_REFERENCE_NUMBER>		{0:M} - <i>see GEDCOMBaseRecord - UserDefinedIDs</i><br />
    ///     +2 TYPE <USER_REFERENCE_TYPE>		{0:1} - <br />
    ///   +1 RIN <AUTOMATED_RECORD_ID>			{0:1} - <i>see GEDCOMBaseRecord - AutomatedRecordID</i><br />
    ///   +1 <<CHANGE_DATE>>					{0:1} - <i>see GEDCOMBaseRecord - ChangeDate</i><br />
    ///</remarks>
    public class GEDCOMFamilyRecord : GEDCOMBaseRecord
    {
        /// <summary>
        ///   Constructs a GEDCOMIndividualRecord
        /// </summary>
        public GEDCOMFamilyRecord(string id) : this(new GEDCOMRecord(0, "@F" + id + "@", "", "FAM", ""))
        {
        }

        /// <summary>
        ///   Constructs a GEDCOMFamilyRecord from a GEDCOMRecord
        /// </summary>
        /// <param name = "record">a GEDCOMRecord</param>
        public GEDCOMFamilyRecord(GEDCOMRecord record) : base(record)
        {
        }

        /// <summary>
        ///   Gets the Children XRefIds for this Family
        /// </summary>
        public List<String> Children
        {
            get { return ChildRecords.GetXRefIDs(GEDCOMTag.CHIL); }
        }

        /// <summary>
        ///   Gets the Facts for this Family
        /// </summary>
        public List<GEDCOMEventStructure> Events
        {
            get { return ChildRecords.GetLinesByTags<GEDCOMEventStructure>(GEDCOMUtil.FamilyEventTags); }
        }

        /// <summary>
        ///   Gets the Id of the Husband
        /// </summary>
        public string Husband
        {
            get { return ChildRecords.GetXRefID(GEDCOMTag.HUSB); }
        }

        /// <summary>
        ///   Gets the No of Children
        /// </summary>
        public string NoChildren
        {
            get { return ChildRecords.GetRecordData(GEDCOMTag.NCHI); }
        }

        /// <summary>
        ///   Gets the Submitter XRefIds for this Family
        /// </summary>
        public List<String> Submitters
        {
            get { return ChildRecords.GetXRefIDs(GEDCOMTag.SUBM); }
        }

        /// <summary>
        ///   Gets the Id of the Wife
        /// </summary>
        public string Wife
        {
            get { return ChildRecords.GetXRefID(GEDCOMTag.WIFE); }
        }

        public void AddChild(string childId)
        {
            AddChildRecord("", childId, "CHIL", "");
        }

        public void AddHusband(string husbandId)
        {
            AddChildRecord("", husbandId, "HUSB", "");
        }

        public void AddWife(string wifeId)
        {
            AddChildRecord("", wifeId, "WIFE", "");
        }
    }
}