using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeamUnicorn.CDAAU.Core;

namespace TeamUnicorn.CDAAU.DischargeSummary
{
    public enum ActStatusType
    {
        [Coded(Code = "active")]
        active,

        [Coded(Code = "completed")]
        completed,
    }
}
