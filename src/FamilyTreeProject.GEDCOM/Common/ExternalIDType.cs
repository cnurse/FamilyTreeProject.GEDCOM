//******************************************
//  Copyright (C) 2011-2013 Charles Nurse  *
//                                         *
//  Licensed under MIT License             *
//  (see included License.txt file)        *
//                                         *
// *****************************************

namespace FamilyTreeProject.GEDCOM.Common
{
    /// <summary>
    ///   An Enum representing the Type of an External ID
    /// </summary>
    public enum ExternalIDType : byte
    {
        AncestralFileNo, //AFN
        AutomatedRecord, //RIN
        PermanentRecordFileNumber, //RFN
        UserDefined //REFN
    }
}