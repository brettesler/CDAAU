using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeamUnicorn.CDAAU.Core;

namespace TeamUnicorn.CDAAU.DischargeSummary
{
    public enum GlobalStatementType
    {
        [Coded(Code = "01", CodeSystem = "1.2.36.1.2001.1001.101.104.16299", CodeSystemName = "Global Statement Values", DisplayName = "None known")]
        NoneKnown,

        [Coded(Code = "02", CodeSystem = "1.2.36.1.2001.1001.101.104.16299", CodeSystemName = "Global Statement Values", DisplayName = "Not asked")]
        NotAsked,

        [Coded(Code = "03", CodeSystem = "1.2.36.1.2001.1001.101.104.16299", CodeSystemName = "Global Statement Values", DisplayName = "None supplied")]
        NoneSupplied,

    }
}
