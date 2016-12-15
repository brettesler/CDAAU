﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oridashi.CDAAU.Core;
using Nehta.HL7.CDA;

namespace Oridashi.CDAAU.EventSummary
{
    [RIMAct(ElementType=typeof(POCD_MT000040EncompassingEncounter), ClassCode="ENC", MoodCode="EVN")]
    public class Encounter
    {
        public Encounter() { }

        [RIMAttribute(Name = RIMAttributeType.EffectiveTime, EffectiveTimeUse = EffectiveTimeUseType.DateTimeZone)]
        public time_period EncounterPeriod { get; set; }

    }

}
