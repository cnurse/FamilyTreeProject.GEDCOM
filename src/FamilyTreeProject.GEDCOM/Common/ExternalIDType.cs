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