using FamilyTreeProject.GEDCOM.Common;
using FamilyTreeProject.GEDCOM.Records;

namespace FamilyTreeProject.GEDCOM.Structures
{
    /// <summary>
    ///   The GEDCOMCallNumberStructure class provides a rich object to define
    ///   Call Numbers.
    /// </summary>
    public class GEDCOMCallNumberStructure : GEDCOMRecord
    {
        #region Constructors

        /// <summary>
        ///   Constructs a GEDCOMCallNumberStructure from a GEDCOMRecord
        /// </summary>
        /// <param name = "record">a GEDCOMRecord</param>
        public GEDCOMCallNumberStructure(GEDCOMRecord record) : base(record)
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets the CallNumber
        /// </summary>
        public string CallNumber
        {
            get { return Data; }
        }

        /// <summary>
        ///   Gets the SourceMedia
        /// </summary>
        public string SourceMedia
        {
            get { return ChildRecords.GetRecordData(GEDCOMTag.MEDI); }
        }

        #endregion
    }
}