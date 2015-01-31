//******************************************
//  Copyright (C) 2011-2013 Charles Nurse  *
//                                         *
//  Licensed under MIT License             *
//  (see included License.txt file)        *
//                                         *
// *****************************************

using System.Collections.Generic;

using FamilyTreeProject.GEDCOM.Common;
using FamilyTreeProject.GEDCOM.Structures;

namespace FamilyTreeProject.GEDCOM.Records
{
    /// <summary>
    ///   The GEDCOMBaseRecord class provides a base class for GEDCOM
    ///   Records, by providing support for Change Dates,  a Collection 
    ///   of Notes and a collection of Source Citations.
    /// </summary>
    /// <remarks>
    public class GEDCOMBaseRecord : GEDCOMRecord
    {
        #region Constructors

        /// <summary>
        ///   Constructs a GEDCOMBaseRecord from a GEDCOMRecord
        /// </summary>
        /// <param name = "record">a GEDCOMRecord</param>
        public GEDCOMBaseRecord(GEDCOMRecord record) : base(record)
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets the Automated Record ID
        /// </summary>
        public GEDCOMExternalIDStructure AutomatedRecordID
        {
            get { return ChildRecords.GetLineByTag<GEDCOMExternalIDStructure>(GEDCOMTag.RIN); }
        }

        /// <summary>
        ///   Gets a List of Notes
        /// </summary>
        public GEDCOMChangeDateStructure ChangeDate
        {
            get { return (GEDCOMChangeDateStructure) ChildRecords.GetLineByTag(GEDCOMTag.CHAN); }
        }

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

        /// <summary>
        ///   Gets the User Defined IDs
        /// </summary>
        public List<GEDCOMExternalIDStructure> UserDefinedIDs
        {
            get { return ChildRecords.GetLinesByTag<GEDCOMExternalIDStructure>(GEDCOMTag.REFN); }
        }

        #endregion
    }
}