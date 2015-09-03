//******************************************
//  Copyright (C) 2014-2015 Charles Nurse  *
//                                         *
//  Licensed under MIT License             *
//  (see included LICENSE)                 *
//                                         *
// *****************************************

using FamilyTreeProject.GEDCOM.Common;
using FamilyTreeProject.GEDCOM.Structures;

namespace FamilyTreeProject.GEDCOM.Records
{
    /// <summary>
    ///   The GEDCOMHeaderRecord Class models a GEDCOM Header Record.
    /// </summary>
    /// <remarks>
    ///   <h2>GEDCOM 5.5 Header Record</h2>
    ///   n HEAD                                          {1:1} <br />
    ///     +1 SOUR <APPROVED_SYSTEM_ID>                  {1:1} - see GEDCOMHeaderSourceStructure<br />
    ///       +2 VERS <VERSION_NUMBER>                    {0:1} - see GEDCOMHeaderSourceStructure<br />
    ///       +2 NAME <NAME_OF_PRODUCT>                   {0:1} - see GEDCOMHeaderSourceStructure<br />
    ///       +2 CORP <NAME_OF_BUSINESS>                  {0:1} - see GEDCOMHeaderSourceStructure<br />
    ///         +3 <<ADDRESS_STRUCTURE>>                  {0:1} - see GEDCOMHeaderSourceStructure<br />
    ///       +2 DATA <NAME_OF_SOURCE_DATA>               {0:1} - see GEDCOMHeaderSourceStructure<br />
    ///         +3 DATE <PUBLICATION_DATE>                {0:1} - see GEDCOMHeaderSourceStructure<br />
    ///         +3 COPR <COPYRIGHT_SOURCE_DATA>           {0:1} - see GEDCOMHeaderSourceStructure<br />
    ///     +1 DEST <RECEIVING_SYSTEM_NAME>               {0:1*} - Destination<br />
    ///     +1 DATE <TRANSMISSION_DATE>                   {0:1} - TransmissionDate<br />
    ///       +2 TIME <TIME_VALUE>                        {0:1} - TransmissionTime<br />
    ///     +1 SUBM @XREF:SUBM@                           {1:1} - Submitter<br />
    ///     +1 SUBN @XREF:SUBN@                           {0:1} - SubmissionRecord<br />
    ///     +1 FILE <FILE_NAME>                           {0:1} - FileName<br />
    ///     +1 COPR <COPYRIGHT_GEDCOM_FILE>               {0:1} - Copyright<br />
    ///     +1 GEDC                                       {1:1} - <br />
    ///       +2 VERS <VERSION_NUMBER>                    {1:1} - GEDCOMVersion<br />
    ///       +2 FORM <GEDCOM_FORM>                       {1:1} - GEDCOMForm<br />
    ///     +1 CHAR <CHARACTER_SET>                       {1:1} - CharacterSet<br />
    ///       +2 VERS <VERSION_NUMBER>                    {0:1} - CharacterSetVersion<br />
    ///     +1 LANG <LANGUAGE_OF_TEXT>                    {0:1} - Language<br />
    ///     +1 PLAC                                       {0:1} - <br />
    ///       +2 FORM <PLACE_HIERARCHY>                   {1:1} - PlaceHeirarchy<br />
    ///     +1 NOTE <GEDCOM_CONTENT_DESCRIPTION>          {0:1} - <i>see GEDCOMBaseRecord - Notes</i><br />
    ///       +2 [CONT|CONC] <GEDCOM_CONTENT_DESCRIPTION> {0:M} - <br />
    /// </remarks>
    public class GEDCOMHeaderRecord : GEDCOMRecord
    {
        #region Constructors

        /// <summary>
        ///   Constructs a GEDCOMHeaderRecord
        /// </summary>
        public GEDCOMHeaderRecord() : base(0, "", "", "HEAD", "")
        {
        }

        /// <summary>
        ///   Constructs a GEDCOMHeaderRecord from a GEDCOMRecord
        /// </summary>
        /// <param name = "record">a GEDCOMRecord</param>
        public GEDCOMHeaderRecord(GEDCOMRecord record) : base(record)
        {
        }

        #endregion

        #region Public Properties

        public string CharacterSet
        {
            get { return GetChildData(GEDCOMTag.CHAR); }
            set { SetChildData(GEDCOMTag.CHAR, value); }
        }

        public string CharacterSetVersion
        {
            get { return GetChildData(GEDCOMTag.CHAR, GEDCOMTag.VERS); }
            set { SetChildData(GEDCOMTag.CHAR, GEDCOMTag.VERS, value); }
        }

        public string Copyright
        {
            get { return GetChildData(GEDCOMTag.COPR); }
            set { SetChildData(GEDCOMTag.COPR, value); }
        }

        public string Destination
        {
            get { return GetChildData(GEDCOMTag.DEST); }
            set { SetChildData(GEDCOMTag.DEST, value); }
        }

        public string FileName
        {
            get { return GetChildData(GEDCOMTag.FILE); }
            set { SetChildData(GEDCOMTag.FILE, value); }
        }

        public string GEDCOMForm
        {
            get { return GetChildData(GEDCOMTag.GEDC, GEDCOMTag.FORM); }
            set { SetChildData(GEDCOMTag.GEDC, GEDCOMTag.FORM, value); }
        }

        public string GEDCOMVersion
        {
            get { return GetChildData(GEDCOMTag.GEDC, GEDCOMTag.VERS); }
            set { SetChildData(GEDCOMTag.GEDC, GEDCOMTag.VERS, value); }
        }

        public string Language
        {
            get { return GetChildData(GEDCOMTag.LANG); }
            set { SetChildData(GEDCOMTag.LANG, value); }
        }

        public string PlaceHeirarchy
        {
            get { return GetChildData(GEDCOMTag.PLAC, GEDCOMTag.FORM); }
            set { SetChildData(GEDCOMTag.PLAC, GEDCOMTag.FORM, value); }
        }

        public GEDCOMHeaderSourceStructure Source
        {
            get { return ChildRecords.GetLineByTag<GEDCOMHeaderSourceStructure>(GEDCOMTag.SOUR); }
            set
            {
                GEDCOMHeaderSourceStructure source = ChildRecords.GetLineByTag<GEDCOMHeaderSourceStructure>(GEDCOMTag.SOUR);
                if (source == null)
                {
                    ChildRecords.Add(value);
                }
                else
                {
                    //Replace address structure
                    int index = ChildRecords.IndexOf(source);
                    ChildRecords[index] = value;
                }
            }
        }

        /// <summary>
        ///   Gets the Id of the Submission Record
        /// </summary>
        public string SubmissionRecord
        {
            get { return GetChildXRefId(GEDCOMTag.SUBN); }
            set { SetChildXRefId(GEDCOMTag.SUBN, value); }
        }

        /// <summary>
        ///   Gets the Id of the Submitter
        /// </summary>
        public string Submitter
        {
            get { return GetChildXRefId(GEDCOMTag.SUBM); }
            set { SetChildXRefId(GEDCOMTag.SUBM, value); }
        }

        public string TransmissionDate
        {
            get { return GetChildData(GEDCOMTag.DATE); }
            set { SetChildData(GEDCOMTag.DATE, value); }
        }

        public string TransmissionTime
        {
            get { return GetChildData(GEDCOMTag.DATE, GEDCOMTag.TIME); }
            set { SetChildData(GEDCOMTag.DATE, GEDCOMTag.TIME, value); }
        }

        #endregion
    }
}