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
    ///   The GEDCOMSourceRepositoryCitationStructure class models the GEDCOM Source Repository Citation
    ///   Structure
    /// </summary>
    /// <remarks>
    ///   <h2>GEDCOM 5.5 Source Citation Structure</h2>
    ///   n REPO @XREF:REPO@                   {1:1} - <i>see GEDCOMRecord - XRefId</i><br />
    ///   +1 <<NOTE_STRUCTURE>>             {0:M} - <i>see GEDCOMBaseRecord - Notes</i><br />
    ///         +1 CALN <SOURCE_CALL_NUMBER>      {0:M} - CallNumbers<br />
    ///                   +2 MEDI <SOURCE_MEDIA_TYPE>   {0:1} - <br />
    /// </remarks>
    public class GEDCOMSourceRepositoryCitationStructure : GEDCOMStructure
    {
        #region Constructors

        /// <summary>
        ///   Constructs a GEDCOMSourceRepositoryCitationStructure from a GEDCOMRecord
        /// </summary>
        /// <param name = "record">a GEDCOMRecord</param>
        public GEDCOMSourceRepositoryCitationStructure(GEDCOMRecord record) : base(record)
        {
        }

        #endregion

        /// <summary>
        ///   Gets a List of Call Numbers
        /// </summary>
        public List<GEDCOMCallNumberStructure> CallNumbers
        {
            get { return ChildRecords.GetLinesByTag<GEDCOMCallNumberStructure>(GEDCOMTag.CALN); }
        }
    }
}