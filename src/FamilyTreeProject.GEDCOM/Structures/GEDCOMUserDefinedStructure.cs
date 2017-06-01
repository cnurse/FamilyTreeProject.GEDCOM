using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FamilyTreeProject.GEDCOM.Records;

namespace FamilyTreeProject.GEDCOM.Structures
{
    public class GEDCOMUserDefinedStructure : GEDCOMRecord
    {
        public GEDCOMUserDefinedStructure() : base(new GEDCOMRecord())
        {            
        }

        public GEDCOMUserDefinedStructure(string tag) : base(new GEDCOMRecord(1, "", "", "_" + tag, ""))
        {
        }

        public GEDCOMUserDefinedStructure(string tag, int level) : base(new GEDCOMRecord(level, "", "", "_" + tag, ""))
        {
        }

        public GEDCOMUserDefinedStructure(string tag, int level, string data) : base(new GEDCOMRecord(level, "", "", "_" + tag, data))
        {
        }
    }
}
