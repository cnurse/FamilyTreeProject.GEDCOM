namespace FamilyTreeProject.GEDCOM.Common
{
    /// <summary>
    ///   An Enum representing the Fact Types
    /// </summary>
    public enum FactType
    {
        //Individual Facts
        Adoption = 0, // ADOP
        Baptism = 1, // BAPM
        BarMitzvah = 2, // BARM
        BasMitzvah = 3, // BASM
        Birth = 4, // BIRT
        Blessing = 5, // BLES
        Burial = 6, // BURI
        Census = 7, // CENS
        Christening = 8, // CHR
        AdultChristening = 9, // CHRA 
        Confirmation = 10, // CONF
        Cremation = 11, // CREM
        Death = 12, // DEAT
        Emigration = 13, // EMIG
        FirstCommunion = 14, // FCOM
        Graduation = 15, // GRAD
        Immigration = 16, // IMMI
        Naturalisation = 17, // NATU
        Ordination = 18, // ORDN
        Probate = 19, // PROB
        Retirement = 20, // RETI
        Will = 21, // WILL

        //Individual Attributes
        Caste = 100, // CAST
        Description = 101, //DESC
        Education = 102, //EDUC
        IdNumber = 103, //IDNO
        NationalOrTribalOrigin = 104, //NATI
        NoOfChildren = 105, //NCHI
        NoOfMarriages = 106, //NMR
        Occupation = 107, //OCCU
        Property = 108, //PROP
        Religion = 109, //RELI
        Residence = 110, //RESI
        SocialSecurityNumber = 111, //SSN
        Title = 112, //TITL

        //Family Facts
        Annulment = 200, // ANUL
        Divorce = 201, // DIV
        DivorceFiled = 202, // DIVF
        Engagement = 203, // ENGA
        Marriage = 204, // MARR
        MarriageBann = 205, // MARB
        MarriageContract = 206, // MARC
        MarriageLicense = 207, // MARL
        MarriageSettlement = 208, // MARS

        Other = 998, // EVEN

        Unknown = 999
    }
}