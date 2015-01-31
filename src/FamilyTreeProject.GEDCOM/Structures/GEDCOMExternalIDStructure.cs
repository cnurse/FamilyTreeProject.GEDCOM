//******************************************
//  Copyright (C) 2011-2013 Charles Nurse  *
//                                         *
//  Licensed under MIT License             *
//  (see included License.txt file)        *
//                                         *
// *****************************************

using FamilyTreeProject.GEDCOM.Common;
using FamilyTreeProject.GEDCOM.Records;

namespace FamilyTreeProject.GEDCOM.Structures
{
    /// <summary>
    ///   The ExternalID class provides a rich object to define
    ///   External IDs.
    /// </summary>
    public class GEDCOMExternalIDStructure : GEDCOMRecord
    {
        #region Constructors

        /// <summary>
        ///   Constructs a GEDCOMExternalIDStructure from a GEDCOMRecord
        /// </summary>
        /// <param name = "record">a GEDCOMRecord</param>
        public GEDCOMExternalIDStructure(GEDCOMRecord record) : base(record)
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets the External ID
        /// </summary>
        public string ExternalID
        {
            get { return Data; }
        }

        /// <summary>
        ///   Gets the External ID Type
        /// </summary>
        public ExternalIDType Type
        {
            get
            {
                ExternalIDType type = ExternalIDType.UserDefined;
                switch (TagName)
                {
                    case GEDCOMTag.AFN:
                        type = ExternalIDType.AncestralFileNo;
                        break;
                    case GEDCOMTag.RIN:
                        type = ExternalIDType.AutomatedRecord;
                        break;
                    case GEDCOMTag.RFN:
                        type = ExternalIDType.PermanentRecordFileNumber;
                        break;
                }
                return type;
            }
        }

        /// <summary>
        ///   Gets the External ID TypeDetail for User Defined types
        /// </summary>
        public string TypeDetail
        {
            get { return ChildRecords.GetRecordData(GEDCOMTag.TYPE); }
        }

        #endregion
    }
}