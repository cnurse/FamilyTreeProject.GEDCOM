//******************************************
//  Copyright (C) 2014-2015 Charles Nurse  *
//                                         *
//  Licensed under MIT License             *
//  (see included LICENSE)                 *
//                                         *
// *****************************************

namespace FamilyTreeProject.GEDCOM.Records
{
    /// <summary>
    ///   The GEDCOMSubmissionRecord Class models a GEDCOM Submission Record.
    /// </summary>
    /// <remarks>
    ///   <h2>GEDCOM 5.5 Submission Record</h2>
    ///   n @XREF:SUBN@ SUBN                               {1:1} <br />
    ///     +1 SUBM @XREF:SUBM@                            {0:1} - not implemented<br />
    ///     +1 FAMF <NAME_OF_FAMILY_FILE>                  {0:1} - not implemented<br />
    ///     +1 TEMP <TEMPLE_CODE>                          {0:1} - not implemented<br />
    ///     +1 ANCE <GENERATIONS_OF_ANCESTORS>             {0:1} - not implemented<br />
    ///     +1 DESC <GENERATIONS_OF_DESCENDANTS>           {0:1} - not implemented<br />
    ///     +1 ORDI <ORDINANCE_PROCESS_FLAG>               {0:1} - not implemented<br />
    ///     +1 RIN <AUTOMATED_RECORD_ID>                   {0:1} - <i>see GEDCOMBaseRecord - AutomatedRecordID</i><br />
    /// </remarks>
    public class GEDCOMSubmissionRecord : GEDCOMBaseRecord
    {
        #region Constructors

        /// <summary>
        ///   Constructs a GEDCOMSubmissionRecord from a GEDCOMRecord
        /// </summary>
        /// <param name = "record">a GEDCOMRecord</param>
        public GEDCOMSubmissionRecord(GEDCOMRecord record) : base(record)
        {
        }

        #endregion
    }
}