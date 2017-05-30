//******************************************
//  Copyright (C) 2014-2015 Charles Nurse  *
//                                         *
//  Licensed under MIT License             *
//  (see included LICENSE)                 *
//                                         *
// *****************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FamilyTreeProject.GEDCOM.Common;
using FamilyTreeProject.GEDCOM.Extensions;

namespace FamilyTreeProject.GEDCOM.Records
{
    public class GEDCOMRecord : IEquatable<GEDCOMRecord>
    {
        #region Constants

        private const int MAX_STRING_LENGTH = 255;
        private const int SPACE_LENGTH = 1;

        #endregion

        #region Fields

        private readonly GEDCOMRecordList _childRecords;
        private string _data;
        private string _xRefId;        

        #endregion

        #region Constructors

        /// <summary>
        ///   Constructs a GEDCOMRecord object
        /// </summary>
        internal GEDCOMRecord()
        {
        }

        /// <summary>
        ///   Constructs a GEDCOMRecord object
        /// </summary>
        /// <param name = "record">The record of text that represents a GEDCOMRecord</param>
        internal GEDCOMRecord(string record)
        {
            Parse(record);
            _childRecords = new GEDCOMRecordList();
        }

        /// <summary>
        ///   Constructs a GEDCOMRecord object
        /// </summary>
        /// <param name = "level">The level (or depth) of the GEDCOM Record</param>
        /// <param name = "id">the id of the record</param>
        /// <param name = "xRefId">An optional XrefId reference</param>
        /// <param name = "tag">The tag name of the GEDCOM Record</param>
        /// <param name = "data">The data part of the GEDCOM Record</param>
        public GEDCOMRecord(int level, string id, string xRefId, string tag, string data)
        {
            Level = level;
            Id = id;
            _xRefId = xRefId;
            Tag = tag;
            _data = data;
            _childRecords = new GEDCOMRecordList();
        }

        /// <summary>
        ///   Constructs a GEDCOMRecord object
        /// </summary>
        /// <remarks>
        ///   This constructor is primarily to allow sublasses of GEDCOMRecord
        ///   to be constructed from the base class
        /// </remarks>
        /// <param name = "record">A GEDCOM Record</param>
        internal GEDCOMRecord(GEDCOMRecord record)
        {
            Level = record.Level;
            Id = record.Id;
            _xRefId = record.XRefId;
            Tag = record.Tag;
            _data = record.Data;
            _childRecords = record.ChildRecords;
        }

        #endregion

        #region Properties

        /// <summary>
        ///   Gets the Child GEDCOM Records for this GEDCOM Record
        /// </summary>
        public GEDCOMRecordList ChildRecords
        {
            get { return _childRecords; }
        }

        /// <summary>
        ///   Gets or sets the Data part of the GEDCOMRecord
        /// </summary>
        public string Data
        {
            get { return _data; }
            set { _data = value; }
        }

        /// <summary>
        ///   Gets whether the Record has any Child Records
        /// </summary>
        public bool HasChildren
        {
            get { return (ChildRecords.Count > 0); }
        }

        /// <summary>
        ///   Gets or sets the Id of the GEDCOMRecord
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        ///   Gets or sets the Level (Depth) of the GEDCOMRecord
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        ///   Gets or sets the Tag of the GEDCOMRecord
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        ///   The TagName (INDI, FAM etc)
        /// </summary>
        public GEDCOMTag TagName
        {
            get { return GEDCOMUtil.GetTag(Tag); }
        }

        /// <summary>
        ///   Gets or sets the XRefId of the GEDCOMRecord
        /// </summary>
        public string XRefId
        {
            get { return _xRefId; }
            set { _xRefId = value; }
        }

        #endregion

        #region Private Methods

        /// <summary>
        ///   Parse parses the text and creates a GEDCOMRecord object
        /// </summary>
        /// <param name = "text">The text to parse</param>
        /// <returns>A flag that indicates whther the record was parsed</returns>
        private bool Parse(string text)
        {
            return GEDCOMUtil.ParseGEDCOM(text, this);
        }

        #endregion

        #region Protected Methods

        protected void AddChildRecord(string childId, string childXRefId, string childTag, string childData)
        {
            AddChildRecord(childId, childXRefId, childTag, childData, null);
        }

        protected void AddChildRecord(string childId, string childXRefId, string childTag, string childData, int? level)
        {
            level = level ?? Level + 1;

            ChildRecords.Add(new GEDCOMRecord((int)level, childId, childXRefId, childTag, childData));
        }

        protected string GetChildData(GEDCOMTag childTag)
        {
            return ChildRecords.GetRecordData(childTag);
        }

        protected string GetChildData(GEDCOMTag childTag, GEDCOMTag grandChildTag)
        {
            string data = "";
            var child = ChildRecords.GetLineByTag<GEDCOMRecord>(childTag);

            if (child != null)
            {
                data = child._childRecords.GetRecordData(grandChildTag);
            }
            return data;
        }

        protected string GetChildXRefId(GEDCOMTag childTag)
        {
            return ChildRecords.GetXRefID(childTag);
        }

        protected void SetChildData(GEDCOMTag childTag, string data)
        {
            GEDCOMRecord child = ChildRecords.GetLineByTag<GEDCOMRecord>(childTag);

            if (child == null)
            {
                ChildRecords.Add(new GEDCOMRecord(Level + 1, "", "", childTag.ToString(), data));
            }
            else
            {
                child.Data = data;
            }
        }

        protected void SetChildData(GEDCOMTag childTag, GEDCOMTag grandChildTag, string data)
        {
            GEDCOMRecord child = ChildRecords.GetLineByTag<GEDCOMRecord>(childTag);

            if (child == null)
            {
                child = new GEDCOMRecord(Level + 1, "", "", childTag.ToString(), "");
                ChildRecords.Add(child);
            }

            GEDCOMRecord grandChild = child.ChildRecords.GetLineByTag<GEDCOMRecord>(grandChildTag);

            if (grandChild == null)
            {
                child.ChildRecords.Add(new GEDCOMRecord(Level + 2, "", "", grandChildTag.ToString(), data));
            }
            else
            {
                grandChild.Data = data;
            }
        }

        protected void SetChildXRefId(GEDCOMTag childTag, string xRefId)
        {
            GEDCOMRecord child = ChildRecords.GetLineByTag<GEDCOMRecord>(childTag);

            if (child == null)
            {
                ChildRecords.Add(new GEDCOMRecord(Level + 1, "", xRefId, childTag.ToString(), ""));
            }
            else
            {
                child._xRefId = xRefId;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///   Append a string to the lineValue field
        /// </summary>
        /// <param name = "data">The string to append</param>
        public void AppendData(String data)
        {
            this._data += data;
        }

        /// <summary>
        ///   Reset all values.
        /// </summary>
        public void Clear()
        {
            Level = 0;
            Tag = "";
            Data = "";
            XRefId = "";
        }

        public int GetId()
        {
            return GEDCOMUtil.GetId(Id);
        }

        /// <summary>
        ///   Splits the Data field into Child CONT records
        /// </summary>
        public void SplitDataWithNewline()
        {
            if (string.IsNullOrEmpty(Data)) { return; }

            string[] data = Data.Split(new[] {'\n'});

            if (data.Length > 1)
            {
                //The original Data field holds the first part
                Data = data[0];

                //Add CONT records for eacdh other part
                for (int i = 1; i < data.Length; i++)
                {
                    ChildRecords.Insert(i - 1, new GEDCOMRecord(Level + 1, "", "", "CONT", data[i]));
                }
            }
        }

        /// <summary>
        ///   Splits the Data field into Child CONT records
        /// </summary>
        public void SplitLongNoteData()
        {            
            // 255 - Level(1) - Space(1) - Tag - Space(1)            
            int lineLength = MAX_STRING_LENGTH - Level.ToString().Length - SPACE_LENGTH - Tag.Length - SPACE_LENGTH;
            List<string> data = Data.Split(lineLength).ToList();

            if (data.Count() > 1)
            {
                //The original Data field holds the first part
                Data = data[0];

                //Add CONT records for eacdh other part
                for (int i = 1; i < data.Count; i++)
                {
                    ChildRecords.Insert(i - 1, new GEDCOMRecord(Level + 1, "", "", "CONC", data[i]));
                }
            }
        }

        /// <summary>
        ///   ToString creates a string represenattion of the GEDCOMRecord
        /// </summary>
        /// <returns>a String</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("Level:");
            sb.Append(Level);
            sb.Append(" Id:");
            sb.Append(Id);
            sb.Append(" Tag:");
            sb.Append(Tag);
            sb.Append(" XRefId:");
            sb.Append(XRefId);
            sb.Append(" Data:");
            sb.Append(Data);

            sb.Append(ChildRecords);

            return sb.ToString();
        }

        #endregion

        #region IEquatable<GEDCOMRecord> Members

        public bool Equals(GEDCOMRecord other)
        {
            if (Id == other.Id && Level == other.Level && Data == other.Data && Tag == other.Tag && XRefId == other.XRefId)
            {
                return true;
            }
            return false;
        }

        #endregion
    }
}