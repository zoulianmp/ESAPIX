﻿using ESAPIX.Helpers;
using ESAPIX.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using VMS.TPS.Common.Model.API;

namespace ESAPIX.AppKit
{
    /// <summary>
    /// A simple wrapper around the script context to make it compliant with a common interface IScriptContext
    /// </summary>
    public class PluginContext : IScriptContext
    {
        private ScriptContext _ctx;

        public PluginContext(ScriptContext ctx, Window w) { _ctx = ctx; Thread = new ScriptComThread(w.Dispatcher); }

        public Course Course { get { return _ctx?.Course;  } }

        public User CurrentUser { get { return _ctx?.CurrentUser; } }

        public Image Image { get { return _ctx?.Image; } }

        public Patient Patient { get { return _ctx?.Patient; } }

        public PlanSetup PlanSetup { get { return _ctx?.PlanSetup; } }

        public IEnumerable<PlanSetup> PlansInScope { get { return _ctx?.PlansInScope; } }

        public IEnumerable<PlanSum> PlanSumsInScope { get { return _ctx?.PlanSumsInScope; } }

        public StructureSet StructureSet { get { return _ctx?.StructureSet; } }

        public IVMSThread Thread { get; private set; }

        public async Task<T> GetValueAsync<T>(Func<IScriptContext, T> toExecute)
        {
            T result = default(T);
            await Thread.InvokeAsync(() =>
            {
                result = toExecute(this);
            });
            return result;
        }

        public T GetValue<T>(Func<IScriptContext, T> toExecute)
        {
            T result = default(T);
            Thread.Invoke(() =>
            {
                result = toExecute(this);
            });
            return result;
        }
    }
}
