//******************************************
//  Copyright (C) 2014-2015 Charles Nurse  *
//                                         *
//  Licensed under MIT License             *
//  (see included LICENSE)                 *
//                                         *
// *****************************************

using System;

using FamilyTreeProject.GEDCOM.Common;
using FamilyTreeProject.GEDCOM.Records;

namespace FamilyTreeProject.GEDCOM.Structures
{
    ///<summary>
    ///  The GEDCOMChangeDateStructure class models the GEDCOM Change Date Structure
    ///</summary>
    ///<remarks>
    ///  <h2>GEDCOM 5.5 Change Date</h2>
    ///  n  CHAN                        {1:1} - <br />
    ///    +1 DATE <CHANGE_DATE>        {1:1} - ChangeDate<br />
    ///      +2 TIME <TIME_VALUE>       {0:1} - <br />
    ///    +1 <<NOTE_STRUCTURE>>        {0:M} - <i>see GEDCOMStructure - Notes</i><br />
    ///</remarks>
    public class GEDCOMChangeDateStructure : GEDCOMStructure
    {
        #region Constructors

        /// <summary>
        /// Constructs a GEDCOMChangeDataStructure
        /// </summary>
        public GEDCOMChangeDateStructure() : this(new GEDCOMRecord(1, string.Empty, string.Empty, "CHAN", ""))
        {            
        }

        /// <summary>
        /// Constructs a GEDCOMChangeDataStructure with level and dateTime
        /// </summary>
        /// <param name="level"></param>
        /// <param name="dateTime"></param>
        public GEDCOMChangeDateStructure(int level, DateTime dateTime) : this(new GEDCOMRecord(level, string.Empty, string.Empty, "CHAN", ""))
        {
            // May not need this
            Level = level;

            AddChildRecord(string.Empty, string.Empty, "DATE", dateTime.ToString("dd MMM yyyy").ToUpper());
            AddChildRecord(string.Empty, string.Empty, "TIME", dateTime.ToString("HH:mm:ss"), Level + 2);            
        }

        /// <summary>
        ///   Constructs a GEDCOMChangeDateStructure from a GEDCOMRecord
        /// </summary>
        /// <param name = "record">a GEDCOMRecord</param>
        public GEDCOMChangeDateStructure(GEDCOMRecord record) : base(record)
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets the Chnage Date
        /// </summary>
        public DateTime ChangeDate
        {
            get
            {
                //Get the Date Record
                GEDCOMRecord dateRecord = ChildRecords.GetLineByTag(GEDCOMTag.DATE);
                DateTime changeDate = DateTime.MinValue;

                //If dateRecord is not null
                if (dateRecord != null)
                {
                    string dateString = dateRecord.Data;
                    string timeString = dateRecord.ChildRecords.GetRecordData(GEDCOMTag.TIME);

                    if (!String.IsNullOrEmpty(timeString))
                    {
                        dateString += " " + timeString;
                    }

                    //Parse the string into a DateTime value
                    DateTime.TryParse(dateString, out changeDate);
                }

                //Return the date
                return changeDate;
            }
        }

        #endregion
    }
}