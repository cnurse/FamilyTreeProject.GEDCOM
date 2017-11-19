using FamilyTreeProject.GEDCOM.Records;
#pragma warning disable 1570

namespace FamilyTreeProject.GEDCOM.Structures
{
    /// <summary>
    ///   The GEDCOMPlaceStructure class models the GEDCOM Place Structure
    /// </summary>
    /// <remarks>
    ///   <h2>GEDCOM 5.5 Place Structure</h2>
    ///   n PLAC <PLACE_VALUE>                {1:1} - <br />
    ///     +1 FORM <PLACE_HIERARCHY>       {0:1} - not implemented<br />
    ///     +1 <<SOURCE_CITATION>>          {0:M} - <i>see GEDCOMStructure - SourceCitations</i><br />
    ///     +1 <<NOTE_STRUCTURE>>           {0:M} - <i>see GEDCOMStructure - Notes</i><br />
    /// </remarks>
    public class GEDCOMPlaceStructure : GEDCOMStructure
    {
        #region Constructors

        /// <summary>
        ///   Constructs a GEDCOMPlaceStructure from a GEDCOMRecord
        /// </summary>
        /// <param name = "record">a GEDCOMRecord</param>
        public GEDCOMPlaceStructure(GEDCOMRecord record) : base(record)
        {
        }

        public GEDCOMPlaceStructure(int level, string place) : base(new GEDCOMRecord(level, "", "", "PLAC", place))
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets the Place
        /// </summary>
        public string Place
        {
            get { return Data; }
        }

        #endregion
    }
}