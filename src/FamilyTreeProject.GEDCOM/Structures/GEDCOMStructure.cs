//******************************************
//  Copyright (C) 2014-2015 Charles Nurse  *
//                                         *
//  Licensed under MIT License             *
//  (see included LICENSE)                 *
//                                         *
// *****************************************

using System.Collections.Generic;

using FamilyTreeProject.GEDCOM.Common;
using FamilyTreeProject.GEDCOM.Records;

namespace FamilyTreeProject.GEDCOM.Structures
{
    /// <summary>
    ///   The GEDCOMStructure class provides a base class for GEDCOM
    ///   Structures, by providing support for Collections of Notes
    ///   and collections of Source Citations.
    /// </summary>
    public class GEDCOMStructure : GEDCOMRecord
    {
        #region Constructors

        /// <summary>
        ///   Constructs a GEDCOMStructure
        /// </summary>
        public GEDCOMStructure()
        {
        }

        /// <summary>
        ///   Constructs a GEDCOMStructure from a GEDCOMRecord
        /// </summary>
        /// <param name = "record">a GEDCOMRecord</param>
        public GEDCOMStructure(GEDCOMRecord record) : base(record)
        {
        }

        /// <summary>
        ///   Constructs a GEDCOMStructure object
        /// </summary>
        /// <param name = "level">The level (or depth) of the GEDCOM Record</param>
        /// <param name = "id">the id of the record</param>
        /// <param name = "xRefId">An optional XrefId reference</param>
        /// <param name = "tag">The tag name of the GEDCOM Record</param>
        /// <param name = "data">The data part of the GEDCOM Record</param>
        public GEDCOMStructure(int level, string id, string xRefId, string tag, string data) : this(new GEDCOMRecord(level, id, xRefId, tag, data))
        {            
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets a List of Multimedia
        /// </summary>
        public List<GEDCOMMultimediaStructure> Multimedia
        {
            get { return ChildRecords.GetLinesByTag<GEDCOMMultimediaStructure>(GEDCOMTag.OBJE); }
        }

        /// <summary>
        ///   Gets a List of Notes
        /// </summary>
        public List<GEDCOMNoteStructure> Notes
        {
            get { return ChildRecords.GetLinesByTag<GEDCOMNoteStructure>(GEDCOMTag.NOTE); }
        }

        /// <summary>
        ///   Gets a List of Source Citations
        /// </summary>
        public List<GEDCOMSourceCitationStructure> SourceCitations
        {
            get { return ChildRecords.GetLinesByTag<GEDCOMSourceCitationStructure>(GEDCOMTag.SOUR); }
        }

        #endregion
    }
}