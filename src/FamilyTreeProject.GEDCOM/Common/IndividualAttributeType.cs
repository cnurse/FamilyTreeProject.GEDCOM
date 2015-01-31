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
    ///   An Enum representing the Event Types
    /// </summary>
    public enum IndividualAttributeType
    {
        Caste, //CAST
        Nationality, //NATI
        Occupation, //OCCU
        Residence, //RESI
        Attribute, //DSCR(meaning physical attribute, such as height) 
        Military,
        Education, //EDUC
        SSN, //SSN (meaning social security number) 
        NationalID, //IDNO
        Marriages, //NMR (information about and number of marriages) 
        Children, //NCHI (information about and number of children) 
        Property, //PROP
        Religion, //RELI
        Title, //TITL (meaning nobility title)
        Other,
        Unknown
    }
}