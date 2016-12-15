using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oridashi.CDAAU.Core;

namespace Oridashi.CDAAU.DischargeSummary
{
    public enum ActStatusType
    {
        [Coded(Code = "active")]
        active,

        [Coded(Code = "completed")]
        completed,
    }
}
