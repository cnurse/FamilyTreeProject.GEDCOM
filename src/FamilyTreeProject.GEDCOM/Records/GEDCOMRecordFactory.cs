using System;

using FamilyTreeProject.GEDCOM.Common;
using FamilyTreeProject.GEDCOM.Structures;

namespace FamilyTreeProject.GEDCOM.Records
{
    internal class GEDCOMRecordFactory
    {
        internal static GEDCOMRecord Create(string text)
        {
            return Create(new GEDCOMRecord(text));
        }

        internal static GEDCOMRecord Create(GEDCOMRecord record)
        {
            switch (record.TagName)
            {
                case GEDCOMTag.ADDR:
                    return CreateAddressStructure(record);
                case GEDCOMTag.ASSO:
                    return CreateAssociationStructure(record);
                case GEDCOMTag.CHAN:
                    return CreateChangeDateStructure(record);
                case GEDCOMTag.CALN:
                    return CreateCallNumberStructure(record);
                case GEDCOMTag.AFN:
                case GEDCOMTag.RIN:
                case GEDCOMTag.RFN:
                case GEDCOMTag.REFN:
                    return CreateExternalIDStructure(record);
                case GEDCOMTag.FAM:
                    return CreateFamilyRecord(record);
                case GEDCOMTag.FAMC:
                case GEDCOMTag.FAMS:
                    return CreateLinkStructure(record);
                case GEDCOMTag.HEAD:
                    return CreateHeaderRecord(record);
                case GEDCOMTag.INDI:
                    return CreateIndividualRecord(record);
                case GEDCOMTag.OBJE:
                    if (record.Level == 0)
                    {
                        return CreateMultimediaRecord(record);
                    }

                    return CreateMultimediaStructure(record);
                case GEDCOMTag.NAME:
                    return CreateNameStructure(record);
                case GEDCOMTag.NOTE:
                    if (record.Level == 0)
                    {
                        return CreateNoteRecord(record);
                    }

                    return CreateNoteStructure(record);
                case GEDCOMTag.PLAC:
                    return CreatePlaceStructure(record);
                case GEDCOMTag.REPO:
                    if (record.Level == 0)
                    {
                        return CreateRepositoryRecord(record);
                    }

                    return CreateSourceRepositoryStructure(record);
                case GEDCOMTag.SOUR:
                    if (record.Level == 0)
                    {
                        return CreateSourceRecord(record);
                    }

                    if (String.IsNullOrEmpty(record.XRefId))
                    {
                        return CreateHeaderSourceStructure(record);
                    }

                    return CreateSourceStructure(record);
                case GEDCOMTag.SUBM:
                    return CreateSubmitterRecord(record);
                case GEDCOMTag.SUBN:
                    return CreateSubmissionRecord(record);
                default:
                    //Check if the tag is an Fact type tag
                    EventClass eventClass = GEDCOMUtil.GetEventClass(record.Tag);
                    if (eventClass != EventClass.Unknown)
                    {
                        return CreateEventStructure(record, eventClass);
                    }

                    return record;
            }
        }

        #region CreateRecord Methods

        internal static GEDCOMFamilyRecord CreateFamilyRecord(GEDCOMRecord record)
        {
            return new GEDCOMFamilyRecord(record);
        }

        internal static GEDCOMHeaderRecord CreateHeaderRecord(GEDCOMRecord record)
        {
            return new GEDCOMHeaderRecord(record);
        }

        internal static GEDCOMIndividualRecord CreateIndividualRecord(GEDCOMRecord record)
        {
            return new GEDCOMIndividualRecord(record);
        }

        internal static GEDCOMMultimediaRecord CreateMultimediaRecord(GEDCOMRecord record)
        {
            return new GEDCOMMultimediaRecord(record);
        }

        internal static GEDCOMNoteRecord CreateNoteRecord(GEDCOMRecord record)
        {
            return new GEDCOMNoteRecord(record);
        }

        internal static GEDCOMRepositoryRecord CreateRepositoryRecord(GEDCOMRecord record)
        {
            return new GEDCOMRepositoryRecord(record);
        }

        internal static GEDCOMSourceRecord CreateSourceRecord(GEDCOMRecord record)
        {
            return new GEDCOMSourceRecord(record);
        }

        internal static GEDCOMSubmitterRecord CreateSubmitterRecord(GEDCOMRecord record)
        {
            return new GEDCOMSubmitterRecord(record);
        }

        internal static GEDCOMSubmissionRecord CreateSubmissionRecord(GEDCOMRecord record)
        {
            return new GEDCOMSubmissionRecord(record);
        }

        #endregion

        #region CreateStructure Methods

        internal static GEDCOMAddressStructure CreateAddressStructure(GEDCOMRecord record)
        {
            return new GEDCOMAddressStructure(record);
        }

        internal static GEDCOMAssociationStructure CreateAssociationStructure(GEDCOMRecord record)
        {
            return new GEDCOMAssociationStructure(record);
        }

        internal static GEDCOMCallNumberStructure CreateCallNumberStructure(GEDCOMRecord record)
        {
            return new GEDCOMCallNumberStructure(record);
        }

        internal static GEDCOMChangeDateStructure CreateChangeDateStructure(GEDCOMRecord record)
        {
            return new GEDCOMChangeDateStructure(record);
        }

        internal static GEDCOMExternalIDStructure CreateExternalIDStructure(GEDCOMRecord record)
        {
            return new GEDCOMExternalIDStructure(record);
        }

        internal static GEDCOMEventStructure CreateEventStructure(GEDCOMRecord record, EventClass eventClass)
        {
            return new GEDCOMEventStructure(record, eventClass);
        }

        internal static GEDCOMFamilyLinkStructure CreateLinkStructure(GEDCOMRecord record)
        {
            return new GEDCOMFamilyLinkStructure(record);
        }

        internal static GEDCOMHeaderSourceStructure CreateHeaderSourceStructure(GEDCOMRecord record)
        {
            return new GEDCOMHeaderSourceStructure(record);
        }

        internal static GEDCOMMultimediaStructure CreateMultimediaStructure(GEDCOMRecord record)
        {
            return new GEDCOMMultimediaStructure(record);
        }

        internal static GEDCOMNameStructure CreateNameStructure(GEDCOMRecord record)
        {
            return new GEDCOMNameStructure(record);
        }

        internal static GEDCOMNoteStructure CreateNoteStructure(GEDCOMRecord record)
        {
            return new GEDCOMNoteStructure(record);
        }

        internal static GEDCOMPlaceStructure CreatePlaceStructure(GEDCOMRecord record)
        {
            return new GEDCOMPlaceStructure(record);
        }

        internal static GEDCOMSourceCitationStructure CreateSourceStructure(GEDCOMRecord record)
        {
            return new GEDCOMSourceCitationStructure(record);
        }

        internal static GEDCOMSourceRepositoryCitationStructure CreateSourceRepositoryStructure(GEDCOMRecord record)
        {
            return new GEDCOMSourceRepositoryCitationStructure(record);
        }

        #endregion
    }
}