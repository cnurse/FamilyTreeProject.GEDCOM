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
    ///  The GEDCOMFamilyLinkStructure class models the GEDCOM Child to Family Link Structure
    ///  and the Spouse to Family Link Structure
    ///</summary>
    ///<remarks>
    ///  <h2>GEDCOM 5.5 Link Structure</h2>
    ///  n FAMC @<XREF:FAM>@                       {1:1} - Family<br />
    ///    +1 PEDI <PEDIGREE_LINKAGE_TYPE>         {0:M} - Pedigree<br />
    ///    +1 <<NOTE_STRUCTURE>>                   {0:M} - <i>see GEDCOMStructure - Notes</i><br />
    ///		  
    ///  n FAMS @<XREF:FAM>@                       {1:1} - Family<br />
    ///    +1 <<NOTE_STRUCTURE>>                   {0:M} - <i>see GEDCOMStructure - Notes</i><br />
    ///</remarks>
    public class GEDCOMFamilyLinkStructure : GEDCOMStructure
    {
        #region Constructors

        /// <summary>
        ///   Constructs a GEDCOMFamilyLinkStructure
        /// </summary>        
        public GEDCOMFamilyLinkStructure()
        {
        }

        /// <summary>
        ///   Constructs a GEDCOMFamilyLinkStructure from a GEDCOMRecord
        /// </summary>
        /// <param name = "record">a GEDCOMRecord</param>
        public GEDCOMFamilyLinkStructure(GEDCOMRecord record) : base(record)
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets the Associated Family
        /// </summary>
        public string Family
        {
            get { return XRefId; }
        }

        /// <summary>
        ///   Gets the Typr of Link (Child or Spouse)
        /// </summary>
        public FamilyLinkType LinkType
        {
            get
            {
                if (Id == "FAMC")
                {
                    return FamilyLinkType.Child;
                }
                else
                {
                    return FamilyLinkType.Spouse;
                }
            }
        }

        /// <summary>
        ///   Gets the Pedigree ([ adopted | birth | foster | sealing ])
        /// </summary>
        public string Pedigree
        {
            get { return ChildRecords.GetRecordData(GEDCOMTag.PEDI); }
        }

        #endregion
    }
}