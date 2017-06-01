//******************************************
//  Copyright (C) 2014-2015 Charles Nurse  *
//                                         *
//  Licensed under MIT License             *
//  (see included LICENSE)                 *
//                                         *
// *****************************************

using FamilyTreeProject.GEDCOM.Common;
using FamilyTreeProject.GEDCOM.Records;

namespace FamilyTreeProject.GEDCOM.Structures
{
    ///<summary>
    ///  The GEDCOMMultimediaStructure class models the GEDCOM MultiMedia Link Structure
    ///</summary>
    ///<remarks>
    ///  <h2>GEDCOM 5.5 MultiMedia Link Structure</h2>
    ///  n  OBJE @<XREF:OBJE>@                      {1:1} - MultimediaRecord<br />
    ///
    ///  n  OBJE                                    {1:1} - <br />
    ///    +1 FORM <MULTIMEDIA_FORMAT>              {1:1} - Format<br />
    ///    +1 TITL <DESCRIPTIVE_TITLE>              {0:1} - Title<br />
    ///    +1 FILE <MULTIMEDIA_FILE_REFERENCE>      {1:1} - FileReference<br />
    ///    +1 <<NOTE_STRUCTURE>>                    {0:M} - <i>see GEDCOMStructure - Notes<br />
    ///</remarks>
    public class GEDCOMMultimediaStructure : GEDCOMStructure
    {
        #region Constructors

        /// <summary>
        ///  Constructs a GEDCOMMultimediaStructure
        /// </summary>
        public GEDCOMMultimediaStructure() : base(1, "", "", "OBJE", "")
        {
            
        }

        /// <summary>
        ///   Constructs a GEDCOMMultimediaStructure from a GEDCOMRecord
        /// </summary>
        /// <param name = "record">a GEDCOMRecord</param>
        public GEDCOMMultimediaStructure(GEDCOMRecord record) : base(record)
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets the Id of the Linked Multimedia Record
        /// </summary>
        public string MultimediaRecord
        {
            get { return XRefId; }
        }

        /// <summary>
        ///   Gets the FileReference for the Multimedia Link
        /// </summary>
        public string FileReference
        {
            get { return ChildRecords.GetRecordData(GEDCOMTag.FILE); }
        }

        /// <summary>
        ///   Gets the Format for the Multimedia Link
        /// </summary>
        public string Format
        {
            get { return ChildRecords.GetRecordData(GEDCOMTag.FORM); }
        }

        /// <summary>
        ///   Gets the Title for the Multimedia Link
        /// </summary>
        public string Title
        {
            get { return ChildRecords.GetRecordData(GEDCOMTag.TITL); }
        }

        #endregion
    }
}