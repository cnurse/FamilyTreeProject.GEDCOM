using System;
using System.Globalization;
using System.Text.RegularExpressions;

using FamilyTreeProject.GEDCOM.Common;
using FamilyTreeProject.GEDCOM.Records;

namespace FamilyTreeProject.GEDCOM
{
    /// <summary>
    ///   This Class provides utilities for working with GEDCOM 5.5 data
    /// </summary>
    public class GEDCOMUtil
    {
        public const string FamilyEventTags = "ANUL, DIV, DIVF, ENGA, MARR, MARB, MARC, MARL, MARS, EVEN";
        public const string FamilyLinkTags = "FAMC, FAMS";
        public const string IndividualEventTags = "ADOP, BAPM, BARM, BASM, BIRT, BLES, BURI, CENS, CHR, CHRA, CONF, CREM, DEAT, EMIG, FCOM, GRAD, IMMI, NATU, ORDN, PROB, RETI, WILL, EVEN";
        public const string IndividualAttributeTags = "CAST, DSCR, EDUC, IDNO, NATI, NCHI, NMR, OCCU, PROP, RELI, RESI, SSN, TITL";
        public static readonly Regex GEDCOMRegex = new Regex(@"(?<level>\d+)\s+(?<tag>[\S]+)(\s+(?<data>.+))?");

        public static EventClass GetEventClass(string tag)
        {
            var eventClass = EventClass.Unknown;

            if (FamilyEventTags.Trim().Contains(tag))
            {
                eventClass = EventClass.Family;
            }
            else if (IndividualEventTags.Trim().Contains(tag))
            {
                eventClass = EventClass.Individual;
            }
            else if (IndividualAttributeTags.Trim().Contains(tag))
            {
                eventClass = EventClass.Attribute;
            }

            return eventClass;
        }

        public static string CleanId(string idString)
        {
            string cleanId = idString;
            if (cleanId.ToUpperInvariant() != "@SUBM@")
            {
                string prefix = idString.Substring(1, 1);
                string id = GetId(idString);

                cleanId = CreateId(prefix, id);
            }

            return cleanId;
        }

        public static string CreateId(string prefix, string id)
        {
            return String.Format("@{0}{1}@", prefix, id);
        }

        public static string GetId(string idString)
        {
            int id;
            if (String.IsNullOrEmpty(idString) || !Int32.TryParse(idString.Substring(2, idString.Length - 3), out id))
            {
                id = -1;
            }
            return id.ToString();
        }

        public static GEDCOMTag GetTag(string tag)
        {
            GEDCOMTag tagName;
            if (Enum.IsDefined(typeof(GEDCOMTag), tag))
            {
                tagName = (GEDCOMTag)Enum.Parse(typeof(GEDCOMTag), tag, true);
            }
            else
            {
                tagName = GEDCOMTag.UNKNOWN;
            }
            return tagName;
        }

        public static bool ParseGEDCOM(string text, GEDCOMRecord record)
        {
            try
            {
                // Init values.
                record.Clear();

                // Some GEDCOM files indent each record with whitespace, delete any
                // whitespace from the beginning and end of the record.
                text = text.Trim();

                // Return right away as nothing to parse.
                if (string.IsNullOrEmpty(text))
                {
                    return false;
                }

                // Get the parts of the record.
                Match match = GEDCOMRegex.Match(text);
                record.Level = Convert.ToInt32(match.Groups["level"].Value, CultureInfo.InvariantCulture);
                record.Tag = match.Groups["tag"].Value.Trim();
                record.Data = match.Groups["data"].Value.Trim();

                // For records the Id is specified in the tag, and the tag in the data
                if (record.Tag[0] == '@')
                {
                    record.Id = CleanId(record.Tag);
                    record.Tag = record.Data;
                    record.Data = String.Empty;

                    // Some GEDCOM files have additional info, 
                    // we only handle the tag info.
                    int pos = record.Tag.IndexOf(' ');
                    if (pos != -1)
                    {
                        record.Tag = record.Tag.Substring(0, pos);
                    }
                }

                // If there are cross-reference Pointers they are defined in the Data
                if (!String.IsNullOrEmpty(record.Data) && record.Data[0] == '@')
                {
                    record.XRefId = CleanId(record.Data);
                    record.Data = String.Empty;
                }

                return true;
            }
            catch
            {
                // This record is invalid, clear all values.
                record.Clear();
                return false;
            }
        }
    }
}