//******************************************
//  Copyright (C) 2014-2015 Charles Nurse  *
//                                         *
//  Licensed under MIT License             *
//  (see included LICENSE)                 *
//                                         *
// *****************************************

using System.Collections.Generic;

namespace FamilyTreeProject.GEDCOM.Records
{
    internal class EqualityComparerGEDCOMRecord : EqualityComparer<GEDCOMRecord>
    {
        public override bool Equals(GEDCOMRecord x, GEDCOMRecord y)
        {
            if (x.Id == y.Id && x.Level == y.Level && x.Data == y.Data && x.Tag == y.Tag && x.XRefId == y.XRefId)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode(GEDCOMRecord obj)
        {
            return base.GetHashCode();
        }
    }
}