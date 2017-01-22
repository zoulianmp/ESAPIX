﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESAPIX.Interfaces;
using ESAPIX.Extensions;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;

namespace ESAPIX.DVH.Constraints
{
    public class MaxVolAtDoseConstraint : VolumeAtDoseConstraint
    {
        public MaxVolAtDoseConstraint()
        {
            PassingFunc = new Func<double, bool>((vol => { return vol <= Volume; }));
        }

        public override string ToString()
        {
            //Mayo format
            var vol = Volume.ToString("N1");
            var volUnit = VolumeType == VolumePresentation.AbsoluteCm3 ? "cc" : "%";
            var doseUnit = ConstraintDose.UnitAsString;
            var dose = ConstraintDose.ValueAsString;
            return $"V{dose}{doseUnit}[{volUnit}]<={vol}";
        }
    }
}