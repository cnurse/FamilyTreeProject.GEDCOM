//******************************************
//  Copyright (C) 2014-2015 Charles Nurse  *
//                                         *
//  Licensed under MIT License             *
//  (see included LICENSE)                 *
//                                         *
// *****************************************

using FamilyTreeProject.GEDCOM.Records;

namespace FamilyTreeProject.GEDCOM.Structures
{
    ///<summary>
    ///  The GEDCOMNoteStructure class models the GEDCOM Note Structure
    ///</summary>
    ///<remarks>
    ///  <h2>GEDCOM 5.5 Note Structure</h2>
    ///  n  NOTE @<XREF:NOTE>@                      {1:1} - NoteRecord<br />
    ///      +1 <<SOURCE_CITATION>>                 {0:M} - <i>see GEDCOMStructure - SourceCitations<br />
    ///
    ///  n  NOTE <SUBMITTER_TEXT> | <NULL>]         {1:1} - Text<br />
    ///      +1 [ CONC | CONT ] <SUBMITTER_TEXT>    {0:M} - <br />
    ///      +1 <<SOURCE_CITATION>>                 {0:M} - <i>see GEDCOMStructure - SourceCitations<br />
    ///</remarks>
    public class GEDCOMNoteStructure : GEDCOMStructure
    {
        #region Constructors

        /// <summary>
        ///   Constructs a GEDCOMNoteStructure from a GEDCOMRecord
        /// </summary>
        /// <param name = "record">a GEDCOMRecord</param>
        public GEDCOMNoteStructure(GEDCOMRecord record) : base(record)
        {
        }

        public GEDCOMNoteStructure(int level, string tag, string data) : base(new GEDCOMRecord(level, "", "", tag, data)) { }

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets the Id of the Linked Note Record
        /// </summary>
        public string NoteRecord
        {
            get { return XRefId; }
        }

        /// <summary>
        ///   Gets the Text for the Note
        /// </summary>
        public string Text
        {
            get { return Data; }
        }

        #endregion
    }
}