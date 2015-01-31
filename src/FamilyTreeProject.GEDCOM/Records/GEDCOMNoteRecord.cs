//******************************************
//  Copyright (C) 2011-2013 Charles Nurse  *
//                                         *
//  Licensed under MIT License             *
//  (see included License.txt file)        *
//                                         *
// *****************************************

namespace FamilyTreeProject.GEDCOM.Records
{
    ///<summary>
    ///  The GEDCOMNoteRecord Class models a Genealogical Note Record.
    ///</summary>
    ///<remarks>
    ///  <h2>GEDCOM 5.5 Note Record</h2>
    ///  n @<XREF:NOTE>@ NOTE <SUBMITTER_TEXT>		{1:1} <i>see GEDCOMRecord</i><br />
    ///                         +1 [ CONC | CONT] <SUBMITTER_TEXT>		{0:M} - <br />
    ///                                             +1 <<SOURCE_CITATION>>					{0:M} - <i>see GEDCOMBaseRecord - SourceCitations<br />
    ///                                                                   					          +1 REFN <USER_REFERENCE_NUMBER>		{0:M} - <i>see GEDCOMBaseRecord - UserDefinedIDs</i><br />
    ///                                                                   					                    +2 TYPE <USER_REFERENCE_TYPE>		{0:1} - <br />
    ///                                                                   					                              +1 RIN <AUTOMATED_RECORD_ID>           {0:1} - <i>see GEDCOMBaseRecord - AutomatedRecordID</i><br />
    ///                                                                   					                                       +1 <<CHANGE_DATE>>                     {0:1} - <i>see GEDCOMBaseRecord - ChangeDate</i><br />
    ///                                                                   					                                             <br />
    ///                                                                   					                                             Note that most notes are usually included in GEDCOM files as <NOTE_STRUCTURE>
    ///                                                                   					                                                                                                            elements as children of other record types.
    ///</remarks>
    public class GEDCOMNoteRecord : GEDCOMBaseRecord
    {
        #region Constructors

        /// <summary>
        ///   Constructs a GEDCOMNoteRecord from a GEDCOMRecord
        /// </summary>
        /// <param name = "record">a GEDCOMRecord</param>
        public GEDCOMNoteRecord(GEDCOMRecord record) : base(record)
        {
        }

        #endregion
    }
}