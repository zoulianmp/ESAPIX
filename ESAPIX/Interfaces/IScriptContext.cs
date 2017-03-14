﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMS.TPS.Common.Model.API;

namespace ESAPIX.Interfaces
{
    /// <summary>
    /// The common interface for use in standalone and plugin applications. It exposes properties that found in a the "ScriptContext" class
    /// </summary>
    public interface IScriptContext
    {

        User CurrentUser { get; }

        Course Course { get; }

        Image Image { get; }

        Patient Patient { get; }

        PlanSetup PlanSetup { get; }

        IEnumerable<PlanSetup> PlansInScope { get; }

        IEnumerable<PlanSum> PlanSumsInScope{ get; }

        StructureSet StructureSet { get; }

        /// <summary>
        /// The thread that can access VMS objects
        /// </summary>
        IVMSThread Thread { get; }

        /// <summary>
        /// Gets a value on the correct thread and returns it
        /// </summary>
        /// <typeparam name="T">the type of value to be returned</typeparam>
        /// <param name="toExecute">the function that assigns the value</param>
        /// <returns>the value requested</returns>
        T GetValue<T>(Func<IScriptContext, T> toExecute);

        /// <summary>
        /// Gets a value asynchronously on the correct thread and returns it
        /// </summary>
        /// <typeparam name="T">the type of value to be returned</typeparam>
        /// <param name="toExecute">the function that assigns the value</param>
        /// <returns>a task of the value requested</returns>
        Task<T> GetValueAsync<T>(Func<IScriptContext, T> toExecute);
    }
}
