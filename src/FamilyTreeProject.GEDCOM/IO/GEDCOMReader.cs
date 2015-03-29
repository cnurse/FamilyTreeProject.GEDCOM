//******************************************
//  Copyright (C) 2014-2015 Charles Nurse  *
//                                         *
//  Licensed under MIT License             *
//  (see included LICENSE)                 *
//                                         *
// *****************************************

using System;
using System.IO;

using FamilyTreeProject.GEDCOM.Common;
using FamilyTreeProject.GEDCOM.Records;
// ReSharper disable UseNullPropagation

namespace FamilyTreeProject.GEDCOM.IO
{
    /// <summary>
    ///   GEDCOMReader is a class that can read GEDCOM files
    /// </summary>
    /// <remarks>
    ///   This class always reads ahead to the next record (so it can determine whether the next 
    ///   record is a CONT or CONC record or a child record)
    ///   storing the next record in a buffer until it is needed.
    /// </remarks>
    public class GEDCOMReader : IDisposable
    {
        private bool _disposed;
        private TextReader _reader;
        private GEDCOMRecord _nextRecord;

        #region Constructors

        /// <summary>
        ///   This constructor creates a GEDCOMReader from a TextReader
        /// </summary>
        /// <param name = "reader">The TextReader to use</param>
        private GEDCOMReader(TextReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }
            _reader = reader;
            GetNextRecord();
        }

        /// <summary>
        ///   This constructor creates a GEDCOMReader that reads from a Stream
        /// </summary>
        /// <param name = "stream">The Stream to use</param>
        private GEDCOMReader(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }
            _reader = new StreamReader(stream);
            GetNextRecord();
        }

        /// <summary>
        ///   This constructor creates a GEDCOMReader that reads from a String
        /// </summary>
        /// <param name = "text">The String to use</param>
        private GEDCOMReader(String text)
        {
            if (text == null)
            {
                throw new ArgumentNullException("text");
            }
            _reader = new StringReader(text);
            GetNextRecord();
        }

        #endregion

        #region Private Methods

        /// <summary>
        ///   GetNextRecord loads the next record of text into the buffer
        /// </summary>
        private void GetNextRecord()
        {
            String text = _reader.ReadLine();

            _nextRecord = text != null ? new GEDCOMRecord(text) : null;
        }

        /// <summary>
        ///   Moves to the next Record of  a specified Tag type
        /// </summary>
        /// <param name = "tag">The type of tag to move to.</param>
        /// <param name="level"></param>
        /// <returns>true if the reader is positioned on a record, false if the reader is not positioned on a record</returns>
        private bool MoveToRecord(GEDCOMTag tag, int level)
        {
            while (_nextRecord != null && _nextRecord.Level >= level && _nextRecord.TagName != tag)
            {
                GetNextRecord();
            }

            //As the nextRecord field always containes the next GEDCOMRecord, return whether this is null.
            return (_nextRecord != null && _nextRecord.TagName == tag && _nextRecord.Level == level);
        }

        /// <summary>
        ///   ReadRecord - reads the next GEDCOM Record of a specified Tag type
        /// </summary>
        /// <param name = "tag">The type of tag to read</param>
        /// <returns>A GEDCOM Record or null</returns>
        private GEDCOMRecord ReadRecord(GEDCOMTag tag)
        {
            GEDCOMRecord record;

            while ((record = ReadRecord()) != null)
            {
                if (record.TagName == tag)
                {
                    break;
                }
            }

            return record;
        }

        /// <summary>
        ///   ReadRecords reads to the end of the source and return all the records
        ///   of a sepcified Tag type
        /// </summary>
        /// <returns>A List of GEDCOMRecords.</returns>
        private GEDCOMRecordList ReadRecords(GEDCOMTag tag)
        {
            GEDCOMRecord record;
            var lines = new GEDCOMRecordList();

            while ((record = ReadRecord()) != null)
            {
                if (record.TagName == tag || tag == GEDCOMTag.ANY)
                {
                    lines.Add(record);
                }
            }

            return lines;
        }

        #endregion

        #region Public Static Methods

        /// <summary>
        ///   Creates a GEDCOMReader from a TextReader
        /// </summary>
        /// <param name = "reader">The TextReader to use</param>
        public static GEDCOMReader Create(TextReader reader)
        {
            return new GEDCOMReader(reader);
        }

        /// <summary>
        ///   Creates a GEDCOMReader that reads from a Stream
        /// </summary>
        /// <param name = "stream">The Stream to use</param>
        public static GEDCOMReader Create(Stream stream)
        {
            return new GEDCOMReader(stream);
        }

        /// <summary>
        ///   Creates a GEDCOMReader that reads from a String
        /// </summary>
        /// <param name = "text">The String to use</param>
        public static GEDCOMReader Create(String text)
        {
            return new GEDCOMReader(text);
        }

        #endregion

        #region Public Methods

        #region MoveTo Methods

        /// <summary>
        ///   Moves to the next Record
        /// </summary>
        /// <returns>true if the reader is positioned on a record, false if the reader is not positioned on a record</returns>
        public bool MoveToRecord()
        {
            //As the nextRecord field always containes the next GEDCOMRecord, return whether this is null.
            return (_nextRecord != null);
        }

        /// <summary>
        ///   Moves to the next Family Record (Tag = FAM)
        /// </summary>
        /// <returns>true if the reader is positioned on a Family record, false if the reader is not positioned on a Family record</returns>
        public bool MoveToFamily()
        {
            return MoveToRecord(GEDCOMTag.FAM, 0);
        }

        /// <summary>
        ///   Moves to the Header Record (Tag = HEAD)
        /// </summary>
        /// <returns>true if the reader is positioned on a Header record, false if the reader is not positioned on a Header record</returns>
        public bool MoveToHeader()
        {
            return MoveToRecord(GEDCOMTag.HEAD, 0);
        }

        /// <summary>
        ///   Moves to the next Individual Record (Tag = INDI)
        /// </summary>
        /// <returns>true if the reader is positioned on a Individual record, false if the reader is not positioned on a Individual record</returns>
        public bool MoveToIndividual()
        {
            return MoveToRecord(GEDCOMTag.INDI, 0);
        }

        /// <summary>
        ///   Moves to the next Multimedia Object Record (Tag = OBJE)
        /// </summary>
        /// <returns>true if the reader is positioned on a Multimedia Object record, false if the reader is not positioned on a Multimedia Object record</returns>
        public bool MoveToMultimedia()
        {
            return MoveToRecord(GEDCOMTag.OBJE, 0);
        }

        /// <summary>
        ///   Moves to the next Note Record (Tag = NOTE)
        /// </summary>
        /// <returns>true if the reader is positioned on a Note record, false if the reader is not positioned on a Note record</returns>
        public bool MoveToNote()
        {
            return MoveToRecord(GEDCOMTag.NOTE, 0);
        }

        /// <summary>
        ///   Moves to the next Repository Record (Tag = REPO)
        /// </summary>
        /// <returns>true if the reader is positioned on a Repository record, false if the reader is not positioned on a Repository record</returns>
        public bool MoveToRepository()
        {
            return MoveToRecord(GEDCOMTag.REPO, 0);
        }

        /// <summary>
        ///   Moves to the next Source Record (Tag = SOUR)
        /// </summary>
        /// <returns>true if the reader is positioned on a Source record, false if the reader is not positioned on a Source record</returns>
        public bool MoveToSource()
        {
            return MoveToRecord(GEDCOMTag.SOUR, 0);
        }

        /// <summary>
        ///   Moves to the next Submitter Record (Tag = SUBM)
        /// </summary>
        /// <returns>true if the reader is positioned on a Submitter record, false if the reader is not positioned on a Submitter record</returns>
        public bool MoveToSubmitter()
        {
            return MoveToRecord(GEDCOMTag.SUBM, 0);
        }

        /// <summary>
        ///   Moves to the Submission Record (Tag = SUBN)
        /// </summary>
        /// <returns>true if the reader is positioned on a Submission record, false if the reader is not positioned on a Submission record</returns>
        public bool MoveToSubmission()
        {
            return MoveToRecord(GEDCOMTag.SUBN, 0);
        }

        #endregion

        #region Read Method

        public GEDCOMRecordList Read()
        {
            return ReadRecords(GEDCOMTag.ANY);
        }

        #endregion

        #region ReadLine Methods

        /// <summary>
        ///   ReadFamily - reads the next GEDCOM Family (FAM)
        /// </summary>
        /// <returns>A GEDCOM Family</returns>
        public GEDCOMFamilyRecord ReadFamily()
        {
            return (GEDCOMFamilyRecord) ReadRecord(GEDCOMTag.FAM);
        }

        /// <summary>
        ///   ReadHeader - reads the next GEDCOM Header (HEAD)
        /// </summary>
        /// <returns>A GEDCOM Family</returns>
        public GEDCOMHeaderRecord ReadHeader()
        {
            return (GEDCOMHeaderRecord) ReadRecord(GEDCOMTag.HEAD);
        }

        /// <summary>
        ///   ReadIndividual - reads the next GEDCOM Individual (INDI)
        /// </summary>
        /// <returns>A GEDCOM Individual</returns>
        public GEDCOMIndividualRecord ReadIndividual()
        {
            return (GEDCOMIndividualRecord) ReadRecord(GEDCOMTag.INDI);
        }

        /// <summary>
        ///   ReadMultimediaObject - reads the next GEDCOM Multimedia Object (OBJE)
        /// </summary>
        /// <returns>A GEDCOM MultimediaObject</returns>
        public GEDCOMMultimediaRecord ReadMultimediaObject()
        {
            return GEDCOMRecordFactory.CreateMultimediaRecord(ReadRecord(GEDCOMTag.OBJE));
        }

        /// <summary>
        ///   ReadNote - reads the next GEDCOM Note (NOTE)
        /// </summary>
        /// <returns>A GEDCOM Note</returns>
        public GEDCOMNoteRecord ReadNote()
        {
            return GEDCOMRecordFactory.CreateNoteRecord(ReadRecord(GEDCOMTag.NOTE));
        }

        /// <summary>
        ///   ReadLine - reads the next GEDCOM record from the buffer
        /// </summary>
        /// <returns>A GEDCOM Record</returns>
        public GEDCOMRecord ReadRecord()
        {
            //Declare Variable
            GEDCOMRecord record = null;

            if (_nextRecord != null)
            {
                record = _nextRecord;

                //Load the next record into the buffer
                GetNextRecord();

                while (_nextRecord != null)
                {
                    if (_nextRecord.Level == record.Level + 1)
                    {
                        switch (_nextRecord.TagName)
                        {
                                // Concatenate.
                            case GEDCOMTag.CONC:
                                record.AppendData(_nextRecord.Data);
                                GetNextRecord();
                                break;

                                // Continue, add record return and then the text.
                            case GEDCOMTag.CONT:
                                record.AppendData("\n" + _nextRecord.Data);
                                GetNextRecord();
                                break;

                                //Add child lines
                            default:
                                GEDCOMRecord childLine = ReadRecord();
                                if (childLine != null)
                                {
                                    record.ChildRecords.Add(childLine);
                                }
                                break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }

            if (record == null)
            {
                return null;
            }

            //Pass to RecordFactory to convert the record into the relevant subclass
            return GEDCOMRecordFactory.Create(record);
        }

        /// <summary>
        ///   ReadRepositoy - reads the next GEDCOM Repositoy (REPO)
        /// </summary>
        /// <returns>A GEDCOM Repository</returns>
        public GEDCOMRepositoryRecord ReadRepository()
        {
            return GEDCOMRecordFactory.CreateRepositoryRecord(ReadRecord(GEDCOMTag.REPO));
        }

        /// <summary>
        ///   ReadSource - reads the next GEDCOM Source (SOUR)
        /// </summary>
        /// <returns>A GEDCOM Source</returns>
        public GEDCOMSourceRecord ReadSource()
        {
            return GEDCOMRecordFactory.CreateSourceRecord(ReadRecord(GEDCOMTag.SOUR));
        }

        /// <summary>
        ///   ReadSubmission - reads the next GEDCOM Submission (SUBN)
        /// </summary>
        /// <returns>A GEDCOM Submitter</returns>
        public GEDCOMSubmissionRecord ReadSubmission()
        {
            return GEDCOMRecordFactory.CreateSubmissionRecord(ReadRecord(GEDCOMTag.SUBN));
        }

        /// <summary>
        ///   ReadSubmitter - reads the next GEDCOM Submitter (SUBM)
        /// </summary>
        /// <returns>A GEDCOM Submitter</returns>
        public GEDCOMSubmitterRecord ReadSubmitter()
        {
            return GEDCOMRecordFactory.CreateSubmitterRecord(ReadRecord(GEDCOMTag.SUBM));
        }

        #endregion

        #region ReadLines Methods

        /// <summary>
        ///   ReadFamilies reads to the end of the source and return all the Family Lines (FAM)
        /// </summary>
        /// <returns>A RecordCollection.</returns>
        public GEDCOMRecordList ReadFamilies()
        {
            return ReadRecords(GEDCOMTag.FAM);
        }

        /// <summary>
        ///   ReadIndividuals reads to the end of the source and return all the Individual Lines (INDI)
        /// </summary>
        /// <returns>A RecordCollection.</returns>
        public GEDCOMRecordList ReadIndividuals()
        {
            return ReadRecords(GEDCOMTag.INDI);
        }

        /// <summary>
        ///   ReadRecords reads to the end of the source and return all the lines
        /// </summary>
        /// <returns>A RecordCollection.</returns>
        public GEDCOMRecordList ReadRecords()
        {
            return ReadRecords(GEDCOMTag.ANY);
        }

        /// <summary>
        ///   ReadMultimediaObjects reads to the end of the source and return all the Multimedia Object Lines (OBJE)
        /// </summary>
        /// <returns>A RecordCollection.</returns>
        public GEDCOMRecordList ReadMultimediaObjects()
        {
            return ReadRecords(GEDCOMTag.OBJE);
        }

        /// <summary>
        ///   ReadNotes reads to the end of the source and return all the Note Lines (NOTE)
        /// </summary>
        /// <returns>A RecordCollection.</returns>
        public GEDCOMRecordList ReadNotes()
        {
            return ReadRecords(GEDCOMTag.NOTE);
        }

        /// <summary>
        ///   ReadRepositories reads to the end of the source and return all the Repository Lines (REPO)
        /// </summary>
        /// <returns>A RecordCollection.</returns>
        public GEDCOMRecordList ReadRepositories()
        {
            return ReadRecords(GEDCOMTag.REPO);
        }

        /// <summary>
        ///   ReadSources reads to the end of the source and return all the Source Lines (SOUR)
        /// </summary>
        /// <returns>A RecordCollection.</returns>
        public GEDCOMRecordList ReadSources()
        {
            return ReadRecords(GEDCOMTag.SOUR);
        }

        /// <summary>
        ///   ReadSubmitters reads to the end of the source and return all the Submitter Lines (SUBM)
        /// </summary>
        /// <returns>A RecordCollection.</returns>
        public GEDCOMRecordList ReadSubmitters()
        {
            return ReadRecords(GEDCOMTag.SUBM);
        }

        #endregion

        #endregion

        #region IDisposable Implementation

        public void Dispose()
        {
            Dispose(true);

            // Use SupressFinalize in case a subclass 
            // of this type implements a finalizer.
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            // If you need thread safety, use a lock around these  
            // operations, as well as in your methods that use the resource. 
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_reader != null)
                    {
                        _reader.Dispose();
                    }
                }

                // Indicate that the instance has been disposed.
                _reader = null;
                _disposed = true;
            }
        }

        #endregion

    }
}