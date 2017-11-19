using System;
using System.Text.RegularExpressions;

using FamilyTreeProject.GEDCOM.Common;
using FamilyTreeProject.GEDCOM.Records;
#pragma warning disable 1570

namespace FamilyTreeProject.GEDCOM.Structures
{
    ///<summary>
    ///  The GEDCOMNameStructure class models the GEDCOM Personal Name Structure
    ///</summary>
    ///<remarks>
    ///  <h2>GEDCOM 5.5 Personal Name Structure</h2>
    ///  n  NAME <NAME_PERSONAL>						{1:1} - FullName<br />
    ///    +1 NPFX <NAME_PIECE_PREFIX>				    {0:1} - Prefix<br />
    ///    +1 GIVN <NAME_PIECE_GIVEN>				    {0:1} - GivenName<br />
    ///    +1 NICK <NAME_PIECE_NICKNAME>		        {0:1} - NickName<br />
    ///    +1 SPFX <NAME_PIECE_SURNAME_PREFIX>          {0:1} - LastNamePrefix<br />
    ///    +1 SURN <NAME_PIECE_SURNAME>			        {0:1} - LastName<br />
    ///    +1 NSFX <NAME_PIECE_SUFFIX>				    {0:1} - Suffix<br />
    ///    +1 <<SOURCE_CITATION>>					    {0:M} - <i>see GEDCOMStructure - SourceCitations</i><br />
    ///    +1 <<NOTE_STRUCTURE>>					    {0:M} - <i>see GEDCOMStructure - Notes</i><br />
    ///</remarks>
    public class GEDCOMNameStructure : GEDCOMStructure
    {
        // Expression pattern used to parse the Name record.
        private readonly Regex nameReg = new Regex(@"(?<first>[\w\s]*)/(?<last>[\S]*)/");

        #region Constructors

        /// <summary>
        ///   Constructs a GEDCOMNameStructure
        /// </summary>
        /// <param name = "name">the name</param>
        /// <param name = "level">the level</param>
        public GEDCOMNameStructure(string name, int level) : base(new GEDCOMRecord(level, "", "", "NAME", name))
        {
        }

        /// <summary>
        ///   Constructs a GEDCOMNameStructure from a GEDCOMRecord
        /// </summary>
        /// <param name = "record">a GEDCOMRecord</param>
        public GEDCOMNameStructure(GEDCOMRecord record) : base(record)
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets and sets the FullName
        /// </summary>
        public string FullName
        {
            get { return Data; }
            set { Data = value; }
        }

        /// <summary>
        ///   Gets and sets the GivenName
        /// </summary>
        public string GivenName
        {
            get
            {
                string givenName = ChildRecords.GetRecordData(GEDCOMTag.GIVN);
                if (String.IsNullOrEmpty(givenName) && !String.IsNullOrEmpty(FullName))
                {
                    Match match = nameReg.Match(FullName);
                    givenName = match.Groups["first"].Value.Trim();
                }
                return givenName;
            }
            set { SetChildData(GEDCOMTag.GIVN, value); }
        }

        /// <summary>
        ///   Gets and sets the Last Name
        /// </summary>
        public string LastName
        {
            get
            {
                string lastName = ChildRecords.GetRecordData(GEDCOMTag.SURN);
                if (String.IsNullOrEmpty(lastName) && !String.IsNullOrEmpty(FullName))
                {
                    Match match = nameReg.Match(FullName);
                    lastName = match.Groups["last"].Value.Trim();
                }
                return lastName;
            }
            set { SetChildData(GEDCOMTag.SURN, value); }
        }

        /// <summary>
        ///   Gets and sets the NickName
        /// </summary>
        public string NickName
        {
            get { return GetChildData(GEDCOMTag.NICK); }
            set { SetChildData(GEDCOMTag.NICK, value); }
        }

        /// <summary>
        ///   Gets and sets the Prefix
        /// </summary>
        public string Prefix
        {
            get { return GetChildData(GEDCOMTag.NPFX); }
            set { SetChildData(GEDCOMTag.NPFX, value); }
        }

        /// <summary>
        ///   Gets and sets the Suffix
        /// </summary>
        public string Suffix
        {
            get { return GetChildData(GEDCOMTag.NSFX); }
            set { SetChildData(GEDCOMTag.NSFX, value); }
        }

        /// <summary>
        ///   Gets and sets the LastNamePrefix
        /// </summary>
        public string LastNamePrefix
        {
            get { return GetChildData(GEDCOMTag.SPFX); }
            set { SetChildData(GEDCOMTag.SPFX, value); }
        }

        #endregion
    }
}