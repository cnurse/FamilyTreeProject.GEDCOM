using System.Collections.Generic;
// ReSharper disable UnusedMember.Global

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

            return false;
        }

        public override int GetHashCode(GEDCOMRecord obj)
        {
            return base.GetHashCode();
        }
    }
}