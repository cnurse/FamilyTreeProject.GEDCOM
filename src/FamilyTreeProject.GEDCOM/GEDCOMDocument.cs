//******************************************
//  Copyright (C) 2014-2015 Charles Nurse  *
//                                         *
//  Licensed under MIT License             *
//  (see included LICENSE)                 *
//                                         *
// *****************************************

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using FamilyTreeProject.GEDCOM.Common;
using FamilyTreeProject.GEDCOM.IO;
using FamilyTreeProject.GEDCOM.Records;
// ReSharper disable ConvertPropertyToExpressionBody

namespace FamilyTreeProject.GEDCOM
{
    /// <summary>
    ///   This Class provides utilities for working with GEDCOM 5.5 data
    /// </summary>
    public class GEDCOMDocument
    {
        #region Private Members

        private GEDCOMRecordList _familyRecords;
        private GEDCOMHeaderRecord _headerRecord;
        private GEDCOMRecordList _individualRecords;
        private GEDCOMRecordList _multimediaRecords;
        private GEDCOMRecordList _noteRecords;
        private GEDCOMRecordList _records = new GEDCOMRecordList();
        private GEDCOMRecordList _repositoryRecords;
        private GEDCOMRecordList _sourceRecords;
        private GEDCOMRecordList _submitterRecords;
        private GEDCOMRecord _trailerRecord;

        #endregion

        #region Public Properties

        public GEDCOMRecordList FamilyRecords
        {
            get { return _familyRecords ?? (_familyRecords = _records.GetLinesByTag(GEDCOMTag.FAM)); }
        }

        public GEDCOMRecordList IndividualRecords
        {
            get { return _individualRecords ?? (_individualRecords = _records.GetLinesByTag(GEDCOMTag.INDI)); }
        }

        public GEDCOMRecordList MultimediaRecords
        {
            get { return _multimediaRecords ?? (_multimediaRecords = _records.GetLinesByTag(GEDCOMTag.OBJE)); }
        }

        public GEDCOMRecordList NoteRecords
        {
            get { return _noteRecords ?? (_noteRecords = _records.GetLinesByTag(GEDCOMTag.NOTE)); }
        }

        public GEDCOMRecordList Records
        {
            get { return _records; }
        }

        public GEDCOMRecordList RepositoryRecords
        {
            get { return _repositoryRecords ?? (_repositoryRecords = _records.GetLinesByTag(GEDCOMTag.REPO)); }
        }

        public GEDCOMRecordList SourceRecords
        {
            get { return _sourceRecords ?? (_sourceRecords = _records.GetLinesByTag(GEDCOMTag.SOUR)); }
        }

        public GEDCOMSubmissionRecord Submission
        {
            get { return _records.GetLineByTag<GEDCOMSubmissionRecord>(GEDCOMTag.SUBN); }
        }

        public GEDCOMRecordList SubmitterRecords
        {
            get { return _submitterRecords ?? (_submitterRecords = _records.GetLinesByTag(GEDCOMTag.SUBM)); }
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///   Adds a record to the GEDCOM Document
        /// </summary>
        /// <param name = "record">The record to add</param>
        public void AddRecord(GEDCOMRecord record)
        {
            if (record == null)
            {
                throw new ArgumentNullException(typeof(GEDCOMRecord).Name);
            }

            _records.Add(record);

            //clear the assoicated RecordList so it is refreshed next time around
            ClearList(record.TagName);
        }

        /// <summary>
        ///   Adds a List of records to the GEDCOM Document
        /// </summary>
        /// <param name = "records">The list of records to add</param>
        public void AddRecords(GEDCOMRecordList records)
        {
            if (records == null)
            {
                throw new ArgumentNullException(typeof(GEDCOMRecordList).Name);
            }

            _records.AddRange(records);
        }

        /// <summary>
        ///   Loads the GEDCOM Document from a Stream
        /// </summary>
        /// <param name = "stream">The stream to load</param>
        public void Load(Stream stream)
        {
            using (var reader = GEDCOMReader.Create(stream))
            {
                _records = reader.Read();
            }
        }

        /// <summary>
        ///   Loads the GEDCOM Document from a TextReader
        /// </summary>
        /// <param name = "reader">The TextReader to load</param>
        public void Load(TextReader reader)
        {
            Load(GEDCOMReader.Create(reader));
        }

        /// <summary>
        ///   Loads the GEDCOM Document from a GEDCOMReader
        /// </summary>
        /// <param name = "reader">The GEDCOMReader to load.</param>
        public void Load(GEDCOMReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }

            //Read the GEDCOM file into a GEDCOMRecords Collection
            _records = reader.Read();
        }

        /// <summary>
        ///   Loads the GEDCOM Document from a String
        /// </summary>
        /// <param name = "text">The String to load</param>
        public void LoadGEDCOM(string text)
        {
            //Load(GEDCOMReader.Create(text));
            using (var reader = GEDCOMReader.Create(text))
            {
                _records = reader.Read();
            }
        }

        /// <summary>
        ///   Removes a single record from the Document
        /// </summary>
        /// <param name = "record"></param>
        public void RemoveRecord(GEDCOMRecord record)
        {
            if (record == null)
            {
                throw new ArgumentNullException(typeof(GEDCOMRecord).Name);
            }

            if (_records.Remove(record))
            {
                //clear the assoicated RecordList so it is refreshed next time around
                ClearList(record.TagName);
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        ///   Save the GEDCOM Document to a Stream.
        /// </summary>
        /// <param name = "stream">The streanm to save to.</param>
        public void Save(Stream stream)
        {
            using (var writer = GEDCOMWriter.Create(stream))
            {
                Save(writer);
            }
        }

        /// <summary>
        ///   Save the GEDCOM Document to a TextWriter
        /// </summary>
        /// <param name = "writer">The TextWriter to save to</param>
        public void Save(TextWriter writer)
        {
            Save(GEDCOMWriter.Create(writer));
        }

        /// <summary>
        ///   Save the GEDCOM Document to a GEDCOMWriter
        /// </summary>
        /// <param name = "writer">The GEDCOMWriter to save to.</param>
        public void Save(GEDCOMWriter writer)
        {
            if (writer == null)
            {
                throw new ArgumentNullException(typeof(GEDCOMWriter).Name);
            }

            writer.NewLine = "\n";

            if (SelectTrailer() == null)
            {
                AddRecord(new GEDCOMRecord(0, "", "", "TRLR", ""));
            }

            //Write Header
            writer.WriteRecord(SelectHeader());

            //Write Submitters
            writer.WriteRecords(SubmitterRecords, true);

            //Write individuals
            writer.WriteRecords(IndividualRecords, true);

            //Write families
            writer.WriteRecords(FamilyRecords, true);

            //Write sources
            writer.WriteRecords(SourceRecords, true);

            //Write repos
            writer.WriteRecords(RepositoryRecords, true);

            //Write notes
            writer.WriteRecords(NoteRecords, true);

            //Write Trailer
            writer.WriteRecord(SelectTrailer());

            writer.Flush();
        }

        /// <summary>
        ///   Save the GEDCOM Document to a String
        /// </summary>
        /// <returns>The String representation of the document</returns>
        public string SaveGEDCOM()
        {
            var sb = new StringBuilder();

            using (var writer = GEDCOMWriter.Create(sb))
            {
                Save(writer);
            }

            return sb.ToString();
        }

        public GEDCOMFamilyRecord SelectChildsFamilyRecord(string childId)
        {
            if (childId == null)
            {
                throw new ArgumentNullException(typeof(string).Name);
            }

            return (from GEDCOMFamilyRecord familyRecord in FamilyRecords
                    where familyRecord.Children.Contains(childId)
                    select familyRecord)
                .SingleOrDefault();
        }

        public GEDCOMFamilyRecord SelectFamilyRecord(string id)
        {
            GEDCOMFamilyRecord family;
            try
            {
                family = FamilyRecords[id] as GEDCOMFamilyRecord;
            }
            catch (ArgumentOutOfRangeException)
            {
                family = null;
            }
            return family;
        }

        public GEDCOMFamilyRecord SelectFamilyRecord(string husbandId, string wifeId)
        {
            if (husbandId == null)
            {
                throw new ArgumentNullException(typeof(string).Name);
            }
            if (wifeId == null)
            {
                throw new ArgumentNullException(typeof(string).Name);
            }

            return (from GEDCOMFamilyRecord familyRecord in FamilyRecords
                    where familyRecord.Husband == husbandId && familyRecord.Wife == wifeId
                    select familyRecord)
                .SingleOrDefault();
        }

        public IEnumerable<GEDCOMFamilyRecord> SelectFamilyRecords(string individualId)
        {
            if (individualId == null)
            {
                throw new ArgumentNullException(typeof(string).Name);
            }

            return from GEDCOMFamilyRecord familyRecord in FamilyRecords
                   where (familyRecord.Husband == individualId) || (familyRecord.Wife == individualId)
                   select familyRecord;
        }

        public GEDCOMHeaderRecord SelectHeader()
        {
            return _headerRecord ?? (_headerRecord = _records.GetLineByTag<GEDCOMHeaderRecord>(GEDCOMTag.HEAD));
        }

        public IEnumerable<GEDCOMFamilyRecord> SelectHusbandsFamilyRecords(string husbandId)
        {
            if (husbandId == null)
            {
                throw new ArgumentNullException(typeof(string).Name);
            }

            return from GEDCOMFamilyRecord familyRecord in FamilyRecords
                   where familyRecord.Husband == husbandId
                   select familyRecord;
        }

        public GEDCOMIndividualRecord SelectIndividualRecord(string id)
        {
            GEDCOMIndividualRecord individual;
            try
            {
                individual = IndividualRecords[id] as GEDCOMIndividualRecord;
            }
            catch (ArgumentOutOfRangeException)
            {
                individual = null;
            }
            return individual;
        }

        public GEDCOMMultimediaRecord SelectMultimediaRecord(string id)
        {
            return MultimediaRecords[id] as GEDCOMMultimediaRecord;
        }

        public GEDCOMNoteRecord SelectNoteRecord(string id)
        {
            return NoteRecords[id] as GEDCOMNoteRecord;
        }

        public GEDCOMRecord SelectRecord(string id)
        {
            GEDCOMRecord record;
            try
            {
                record = _records[id];
            }
            catch (ArgumentOutOfRangeException)
            {
                record = null;
            }
            return record;
        }

        public GEDCOMRepositoryRecord SelectRepositoryRecord(string id)
        {
            return RepositoryRecords[id] as GEDCOMRepositoryRecord;
        }

        public GEDCOMSourceRecord SelectSourceRecord(string id)
        {
            return SourceRecords[id] as GEDCOMSourceRecord;
        }

        public GEDCOMSubmitterRecord SelectSubmitterRecord(string id)
        {
            return SubmitterRecords[id] as GEDCOMSubmitterRecord;
        }

        public GEDCOMRecord SelectTrailer()
        {
            return _trailerRecord ?? (_trailerRecord = _records.GetLineByTag<GEDCOMRecord>(GEDCOMTag.TRLR));
        }

        public IEnumerable<GEDCOMFamilyRecord> SelectWifesFamilyRecords(string wifeId)
        {
            if (wifeId == null)
            {
                throw new ArgumentNullException(typeof(string).Name);
            }

            return from GEDCOMFamilyRecord familyRecord in FamilyRecords
                   where familyRecord.Wife == wifeId
                   select familyRecord;
        }

        /// <summary>
        ///   ToString creates a string represenattion of the GEDCOMDocument
        /// </summary>
        /// <returns>a String</returns>
        public override string ToString()
        {
            return _records.ToString();
        }

        #endregion

        private void ClearList(GEDCOMTag tag)
        {
            switch (tag)
            {
                case GEDCOMTag.INDI:
                    _individualRecords = null;
                    break;
                case GEDCOMTag.FAM:
                    _familyRecords = null;
                    break;
            }
        }
    }
}