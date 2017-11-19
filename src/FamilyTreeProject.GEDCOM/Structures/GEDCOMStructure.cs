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
        ///   Constructs a GEDCOMStructure from a GEDCOMRecord
        /// </summary>
        /// <param name = "record">a GEDCOMRecord</param>
        public GEDCOMStructure(GEDCOMRecord record) : base(record)
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