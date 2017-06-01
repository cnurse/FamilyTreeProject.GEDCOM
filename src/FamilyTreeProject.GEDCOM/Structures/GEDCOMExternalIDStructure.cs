//******************************************
//  Copyright (C) 2014-2015 Charles Nurse  *
//                                         *
//  Licensed under MIT License             *
//  (see included LICENSE)                 *
//                                         *
// *****************************************

using System.IO.Compression;
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
        ///   Constructs a GEDCOMExternalIDStructure
        /// </summary>
        public GEDCOMExternalIDStructure()
        {            
        }

        /// <summary>
        ///   Constructs a GEDCOMExternalIDStructure from a GEDCOMRecord
        /// </summary>
        /// <param name = "record">a GEDCOMRecord</param>
        public GEDCOMExternalIDStructure(GEDCOMRecord record) : base(record)
        {
        }

        #endregion
        public static GEDCOMExternalIDStructure CreateUserReference(string number, string type, int level = 0)
        {
            var userReference = new GEDCOMExternalIDStructure(new GEDCOMRecord(level, "", "", "REFN", number));
            userReference.AddChildRecord(string.Empty, string.Empty, "TYPE", type, level + 1);

            return userReference;
        }

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