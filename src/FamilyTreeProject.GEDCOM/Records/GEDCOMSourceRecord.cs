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
    ///  The GEDCOMSourceRecord Class models a Genealogical Source Record.
    ///</summary>
    ///<remarks>
    ///  <h2>GEDCOM 5.5 Source Record</h2>
    ///  n  @<XREF:SOUR>@ SOUR                     {1:1} <i>see GEDCOMRecord</i><br />
    ///        +1 DATA                                 {0:1} <br />
    ///        +2 EVEN <EVENTS_RECORDED>             {0:M} - SourceEvents<br />
    ///                  +3 DATE <DATE_PERIOD>               {0:1} - <br />
    ///                            +3 PLAC <SOURCE_JURISDICTION_PLACE> {0:1} - <br />
    ///                                      +2 AGNC <RESPONSIBLE_AGENCY>          {0:1} - SourceAgency<br />
    ///                                                +2 <<NOTE_STRUCTURE>>                 {0:M} - SourceNotes<br />
    ///                                                      +1 AUTH <SOURCE_ORIGINATOR>             {0:1} - Author<br />
    ///                                                                +1 TITL <SOURCE_DESCRIPTIVE_TITLE>      {0:1} - Title<br />
    ///                                                                          +1 ABBR <SOURCE_FILED_BY_ENTRY>         {0:1} - AbbreviatedTitle<br />
    ///                                                                                    +1 PUBL <SOURCE_PUBLICATION_FACTS>      {0:1} - PublisherInfo<br />
    ///                                                                                              +1 TEXT <TEXT_FROM_SOURCE>              {0:1} - Text<br />
    ///                                                                                                        +1 <<SOURCE_REPOSITORY_CITATION>>       {0:1} - SourceRepositories<br />
    ///                                                                                                              +1 <<MULTIMEDIA_LINK>>                  {0:M} - <i>see GEDCOMBaseRecord - Multimedia</i><br />
    ///                                                                                                                    +1 <<NOTE_STRUCTURE>>                   {0:M} - <i>see GEDCOMBaseRecord - Notes</i><br />
    ///                                                                                                                          +1 REFN <USER_REFERENCE_NUMBER>         {0:M} - <i>see GEDCOMBaseRecord - UserDefinedIDs</i><br />
    ///                                                                                                                                    +2 TYPE <USER_REFERENCE_TYPE>         {0:1} - <br />
    ///                                                                                                                                              +1 RIN <AUTOMATED_RECORD_ID>            {0:1} - <i>see GEDCOMBaseRecord - AutomatedRecordID</i><br />
    ///                                                                                                                                                       +1 <<CHANGE_DATE>>                      {0:1} - <i>see GEDCOMBaseRecord - ChangeDate</i><br />
    ///</remarks>
    public class GEDCOMSourceRecord : GEDCOMBaseRecord
    {
        #region Constructors

        /// <summary>
        ///   Constructs a GEDCOMSourceRecord from a GEDCOMRecord
        /// </summary>
        /// <param name = "record">a GEDCOMRecord</param>
        public GEDCOMSourceRecord(GEDCOMRecord record) : base(record)
        {
        }

        #endregion

        protected GEDCOMRecord DataRecord
        {
            get { return ChildRecords.GetLineByTag<GEDCOMRecord>(GEDCOMTag.DATA); }
        }

        #region Public Properties

        /// <summary>
        ///   Gets the Abbreviated Title of the Source
        /// </summary>
        public string AbbreviatedTitle
        {
            get { return ChildRecords.GetRecordData(GEDCOMTag.ABBR); }
        }

        /// <summary>
        ///   Gets the Author of the Source
        /// </summary>
        public string Author
        {
            get { return ChildRecords.GetRecordData(GEDCOMTag.AUTH); }
        }

        /// <summary>
        ///   Gets the Publisher Information of the Source
        /// </summary>
        public string PublisherInfo
        {
            get { return ChildRecords.GetRecordData(GEDCOMTag.PUBL); }
        }

        /// <summary>
        ///   Gets the Source Agency
        /// </summary>
        public string SourceAgency
        {
            get
            {
                string agency = String.Empty;
                if (DataRecord != null)
                {
                    agency = DataRecord.ChildRecords.GetRecordData(GEDCOMTag.AGNC);
                }
                return agency;
            }
        }

        /// <summary>
        ///   Gets a List of SourceNotes
        /// </summary>
        public List<GEDCOMSourceEventStructure> SourceEvents
        {
            get
            {
                if (DataRecord != null)
                {
                    return DataRecord.ChildRecords.GetLinesByTag<GEDCOMSourceEventStructure>(GEDCOMTag.EVEN);
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        ///   Gets a List of SourceNotes
        /// </summary>
        public List<GEDCOMNoteStructure> SourceNotes
        {
            get
            {
                if (DataRecord != null)
                {
                    return DataRecord.ChildRecords.GetLinesByTag<GEDCOMNoteStructure>(GEDCOMTag.NOTE);
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        ///   Gets a List of Source Repositories
        /// </summary>
        public List<GEDCOMSourceRepositoryCitationStructure> SourceRepositories
        {
            get { return ChildRecords.GetLinesByTag<GEDCOMSourceRepositoryCitationStructure>(GEDCOMTag.REPO); }
        }

        /// <summary>
        ///   Gets the Text of the Source
        /// </summary>
        public string Text
        {
            get { return ChildRecords.GetRecordData(GEDCOMTag.TEXT); }
        }

        /// <summary>
        ///   Gets the Title of the Source
        /// </summary>
        public string Title
        {
            get { return ChildRecords.GetRecordData(GEDCOMTag.TITL); }
        }

        #endregion
    }
}