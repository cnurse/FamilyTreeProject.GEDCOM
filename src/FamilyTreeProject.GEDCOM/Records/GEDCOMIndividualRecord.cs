//******************************************
//  Copyright (C) 2014-2015 Charles Nurse  *
//                                         *
//  Licensed under MIT License             *
//  (see included LICENSE)                 *
//                                         *
// *****************************************

using System;
using System.Collections.Generic;
using FamilyTreeProject.Common;
using FamilyTreeProject.GEDCOM.Common;
using FamilyTreeProject.GEDCOM.Structures;
// ReSharper disable ConvertPropertyToExpressionBody

namespace FamilyTreeProject.GEDCOM.Records
{
    /// <summary>
    ///   The GEDCOMIndividual Class models a GEDCOM Individual Record.
    /// </summary>
    /// <remarks>
    ///   <h2>GEDCOM 5.5 Individual Record</h2>
    ///   n @XREF:INDI@ INDI                        {1:1} <i>see GEDCOMRecord</i><br />
    ///     +1 RESN <RESTRICTION_NOTICE>            {0:1} - RestrictionNotice<br />
    ///     +1 <<PERSONAL_NAME_STRUCTURE>>          {0:M} - Name<br />
    ///     +1 SEX <SEX_VALUE>                      {0:1} - Sex<br />
    ///     +1 <<INDIVIDUAL_EVENT_STRUCTURE>>       {0:M} - Facts<br />
    ///     +1 <<INDIVIDUAL_ATTRIBUTE_STRUCTURE>>   {0:M} - Attributes<br />
    ///     +1 <<LDS_INDIVIDUAL_ORDINANCE>>         {0:M} - not implemented<br />
    ///     +1 <<CHILD_TO_FAMILY_LINK>>             {0:M} - FamilyLinks<br />
    ///     +1 <<SPOUSE_TO_FAMILY_LINK>>            {0:M} - FamilyLinks<br />
    ///     +1 SUBM @<XREF:SUBM>@                   {0:M} - Submitters<br />
    ///     +1 <<ASSOCIATION_STRUCTURE>>            {0:M} - Associations<br />
    ///     +1 ALIA @<XREF:INDI>@                   {0:M} - Aliases<br />
    ///     +1 ANCI @<XREF:SUBM>@                   {0:M} - AncestorInterests<br />
    ///     +1 DESI @<XREF:SUBM>@                   {0:M} - DescendantInterests<br />
    ///     +1 <<SOURCE_CITATION>>                  {0:M} - <i>see GEDCOMBaseRecord - SourceCitations<br />
    ///     +1 <<MULTIMEDIA_LINK>>                  {0:M} - <i>see GEDCOMBaseRecord - Multimedia</i><br />
    ///     +1 <<NOTE_STRUCTURE>>                   {0:M} - <i>see GEDCOMBaseRecord - Notes</i><br />
    ///     +1 RFN <PERMANENT_RECORD_FILE_NUMBER>   {0:1} - <i>see GEDCOMBaseRecord - UserDefinedIDs</i><br />
    ///     +1 AFN <ANCESTRAL_FILE_NUMBER>          {0:1} - <i>see GEDCOMBaseRecord - UserDefinedIDs</i><br />
    ///     +1 REFN <USER_REFERENCE_NUMBER>         {0:M} - <i>see GEDCOMBaseRecord - UserDefinedIDs</i><br />
    ///       +2 TYPE <USER_REFERENCE_TYPE>         {0:1} - <br />
    ///     +1 RIN <AUTOMATED_RECORD_ID>            {0:1} - <i>see GEDCOMBaseRecord - AutomatedRecordID</i><br />
    ///     +1 <<CHANGE_DATE>>                      {0:1} - <i>see GEDCOMBaseRecord - ChangeDate</i><br />
    /// </remarks>
    public class GEDCOMIndividualRecord : GEDCOMBaseRecord
    {
        /// <summary>
        ///   Constructs a GEDCOMIndividualRecord
        /// </summary>
        public GEDCOMIndividualRecord(string id) : this(new GEDCOMRecord(0, "@I" + id + "@", "", "INDI", ""))
        {
        }

        /// <summary>
        ///   Constructs a GEDCOMIndividualRecord from a GEDCOMRecord
        /// </summary>
        /// <param name = "record">a GEDCOMRecord</param>
        public GEDCOMIndividualRecord(GEDCOMRecord record) : base(record)
        {
        }

        /// <summary>
        ///   Gets the Alias XRefIds for this Individual
        /// </summary>
        public List<String> Aliases
        {
            get { return ChildRecords.GetXRefIDs(GEDCOMTag.ALIA); }
        }

        /// <summary>
        ///   Gets the XRefIds of Ancestors of Interest for this Individual
        /// </summary>
        public List<String> AncestorInterests
        {
            get { return ChildRecords.GetXRefIDs(GEDCOMTag.ANCI); }
        }

        /// <summary>
        ///   Gets the Associations for this Individual
        /// </summary>
        public List<GEDCOMAssociationStructure> Associations
        {
            get { return ChildRecords.GetLinesByTag<GEDCOMAssociationStructure>(GEDCOMTag.ASSO); }
        }

        /// <summary>
        ///   Gets the Attributes for this Individual
        /// </summary>
        public List<GEDCOMEventStructure> Attributes
        {
            get { return ChildRecords.GetLinesByTags<GEDCOMEventStructure>(GEDCOMUtil.IndividualAttributeTags); }
        }

        /// <summary>
        ///   Gets the XRefIds of Descendants of Interest for this Individual
        /// </summary>
        public List<String> DescendantInterests
        {
            get { return ChildRecords.GetXRefIDs(GEDCOMTag.DESI); }
        }

        /// <summary>
        ///   Gets the Facts for this Individual
        /// </summary>
        public List<GEDCOMEventStructure> Events
        {
            get { return ChildRecords.GetLinesByTags<GEDCOMEventStructure>(GEDCOMUtil.IndividualEventTags); }
        }

        /// <summary>
        ///   Gets the Family Links for this Individual
        /// </summary>
        public List<GEDCOMFamilyLinkStructure> FamilyLinks
        {
            get { return ChildRecords.GetLinesByTags<GEDCOMFamilyLinkStructure>(GEDCOMUtil.FamilyLinkTags); }
        }

        /// <summary>
        ///   Gets and sets the Name
        /// </summary>
        public GEDCOMNameStructure Name
        {
            get { return ChildRecords.GetLineByTag<GEDCOMNameStructure>(GEDCOMTag.NAME); }
            set
            {
                var source = ChildRecords.GetLineByTag<GEDCOMNameStructure>(GEDCOMTag.NAME);
                if (source == null)
                {
                    ChildRecords.Add(value);
                }
                else
                {
                    //Replace name structure
                    int i = ChildRecords.IndexOf(source);
                    ChildRecords[i] = value;
                }
            }
        }

        /// <summary>
        ///   Gets the RestrictionNotice
        /// </summary>
        public string RestrictionNotice
        {
            get { return ChildRecords.GetRecordData(GEDCOMTag.RESN); }
        }

        /// <summary>
        ///   Gets and sets the Sex
        /// </summary>
        public Sex Sex
        {
            get
            {
                Sex sex = Sex.Unknown;
                GEDCOMRecord sexRecord = ChildRecords.GetLineByTag(GEDCOMTag.SEX);
                if (sexRecord != null)
                {
                    if (sexRecord.Data.StartsWith("M"))
                    {
                        sex = Sex.Male;
                    }
                    else if (sexRecord.Data.StartsWith("F"))
                    {
                        sex = Sex.Female;
                    }
                    else
                    {
                        sex = Sex.Unknown;
                    }
                }

                return sex;
            }
            set
            {
                switch (value)
                {
                    case Sex.Male:
                        SetChildData(GEDCOMTag.SEX, "M");
                        break;
                    case Sex.Female:
                        SetChildData(GEDCOMTag.SEX, "F");
                        break;
                    default:
                        SetChildData(GEDCOMTag.SEX, "");
                        break;
                }
            }
        }

        /// <summary>
        ///   Gets the Submitter XRefIds for this Individual
        /// </summary>
        public List<String> Submitters
        {
            get { return ChildRecords.GetXRefIDs(GEDCOMTag.SUBM); }
        }
    }
}