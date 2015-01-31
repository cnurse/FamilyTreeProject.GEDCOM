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
    ///   The GEDCOMHeaderSourceStructure class models the GEDCOM Header Records Source Structure
    /// </summary>
    /// <remarks>
    ///   +1 SOUR <APPROVED_SYSTEM_ID>                   {1:1} - Source<br />
    ///             +2 VERS <VERSION_NUMBER>                   {0:1} - Version<br />
    ///                       +2 NAME <NAME_OF_PRODUCT>                  {0:1} - ProductName<br />
    ///                                 +2 CORP <NAME_OF_BUSINESS>                 {0:1} - Company<br />
    ///                                           +3 <<ADDRESS_STRUCTURE>>               {0:1} - Address<br />
    ///                                                 +2 DATA <NAME_OF_SOURCE_DATA>              {0:1} - SourceData<br />
    ///                                                           +3 DATE <PUBLICATION_DATE>             {0:1} - PublicationDate<br />
    ///                                                                     +3 COPR <COPYRIGHT_SOURCE_DATA>        {0:1} - SourceCopyright<br />
    /// </remarks>
    public class GEDCOMHeaderSourceStructure : GEDCOMStructure
    {
        #region Constructors

        public GEDCOMHeaderSourceStructure() : base(new GEDCOMRecord(1, "", "", "SOUR", ""))
        {
        }

        public GEDCOMHeaderSourceStructure(string systemId) : base(new GEDCOMRecord(1, "", "", "SOUR", systemId))
        {
        }

        public GEDCOMHeaderSourceStructure(GEDCOMRecord record) : base(record)
        {
        }

        #endregion

        #region Protected Properties

        protected GEDCOMRecord CompanyRecord
        {
            get { return ChildRecords.GetLineByTag<GEDCOMRecord>(GEDCOMTag.CORP); }
        }

        protected GEDCOMRecord DataRecord
        {
            get { return ChildRecords.GetLineByTag<GEDCOMRecord>(GEDCOMTag.DATA); }
        }

        #endregion

        #region Public Properties

        public GEDCOMAddressStructure Address
        {
            get
            {
                GEDCOMAddressStructure address = null;
                if (CompanyRecord != null)
                {
                    address = CompanyRecord.ChildRecords.GetLineByTag<GEDCOMAddressStructure>(GEDCOMTag.ADDR);
                }
                return address;
            }
            set
            {
                if (CompanyRecord == null)
                {
                    //Add new Company Record
                    ChildRecords.Add(new GEDCOMRecord(Level + 1, "", "", "CORP", ""));
                }

                GEDCOMAddressStructure address = CompanyRecord.ChildRecords.GetLineByTag<GEDCOMAddressStructure>(GEDCOMTag.ADDR);

                if (address == null)
                {
                    //Add address structure
                    CompanyRecord.ChildRecords.Add(value);
                }
                else
                {
                    //Replace address structure
                    int index = CompanyRecord.ChildRecords.IndexOf(address);
                    CompanyRecord.ChildRecords[index] = value;
                }
            }
        }

        public string Company
        {
            get { return GetChildData(GEDCOMTag.CORP); }
            set { SetChildData(GEDCOMTag.CORP, value); }
        }

        public string ProductName
        {
            get { return GetChildData(GEDCOMTag.NAME); }
            set { SetChildData(GEDCOMTag.NAME, value); }
        }

        public string PublicationDate
        {
            get { return GetChildData(GEDCOMTag.DATA, GEDCOMTag.DATE); }
            set { SetChildData(GEDCOMTag.DATA, GEDCOMTag.DATE, value); }
        }

        public string SourceCopyright
        {
            get { return GetChildData(GEDCOMTag.DATA, GEDCOMTag.COPR); }
            set { SetChildData(GEDCOMTag.DATA, GEDCOMTag.COPR, value); }
        }

        public string SourceData
        {
            get { return GetChildData(GEDCOMTag.DATA); }
            set { SetChildData(GEDCOMTag.DATA, value); }
        }

        public string SystemId
        {
            get { return Data; }
            set { Data = value; }
        }

        public string Version
        {
            get { return GetChildData(GEDCOMTag.VERS); }
            set { SetChildData(GEDCOMTag.VERS, value); }
        }

        #endregion
    }
}