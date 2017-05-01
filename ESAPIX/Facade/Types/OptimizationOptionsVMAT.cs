#region

using System;
using System.Dynamic;
using ESAPIX.Extensions;
using X = ESAPIX.Facade.XContext;

#endregion

namespace ESAPIX.Facade.Types
{
    public class OptimizationOptionsVMAT : OptimizationOptionsBase
    {
        public OptimizationOptionsVMAT()
        {
            _client = new ExpandoObject();
        }

        public OptimizationOptionsVMAT(dynamic client)
        {
            _client = client;
        }

        public OptimizationOptionsVMAT(OptimizationOption startOption, string mlcId)
        {
            if (X.Instance.CurrentContext != null)
                X.Instance.CurrentContext.Thread.Invoke(() =>
                {
                    _client = VMSConstructor.ConstructOptimizationOptionsVMAT(startOption, mlcId);
                });
            else throw new Exception("There is no VMS Context to create the class");
        }

        public OptimizationOptionsVMAT(OptimizationIntermediateDoseOption intermediateDoseOption, string mlcId)
        {
            if (X.Instance.CurrentContext != null)
                X.Instance.CurrentContext.Thread.Invoke(() =>
                {
                    _client = VMSConstructor.ConstructOptimizationOptionsVMAT(intermediateDoseOption, mlcId);
                });
            else throw new Exception("There is no VMS Context to create the class");
        }

        public OptimizationOptionsVMAT(int numberOfCycles, string mlcId)
        {
            if (X.Instance.CurrentContext != null)
                X.Instance.CurrentContext.Thread.Invoke(() =>
                {
                    _client = VMSConstructor.ConstructOptimizationOptionsVMAT(numberOfCycles, mlcId);
                });
            else throw new Exception("There is no VMS Context to create the class");
        }

        public OptimizationOptionsVMAT(OptimizationOption startOption,
            OptimizationIntermediateDoseOption intermediateDoseOption, int numberOfCycles, string mlcId)
        {
            if (X.Instance.CurrentContext != null)
                X.Instance.CurrentContext.Thread.Invoke(() =>
                {
                    _client = VMSConstructor.ConstructOptimizationOptionsVMAT(startOption, intermediateDoseOption,
                        numberOfCycles, mlcId);
                });
            else throw new Exception("There is no VMS Context to create the class");
        }

        public OptimizationOptionsVMAT(OptimizationOptionsVMAT options)
        {
            if (X.Instance.CurrentContext != null)
                X.Instance.CurrentContext.Thread.Invoke(() =>
                {
                    _client = VMSConstructor.ConstructOptimizationOptionsVMAT(options);
                });
            else throw new Exception("There is no VMS Context to create the class");
        }

        public bool IsLive
        {
            get { return !DefaultHelper.IsDefault(_client) && !(_client is ExpandoObject); }
        }

        public int NumberOfOptimizationCycles
        {
            get
            {
                if (_client is ExpandoObject)
                    return (_client as ExpandoObject).HasProperty("NumberOfOptimizationCycles")
                        ? _client.NumberOfOptimizationCycles
                        : default(int);
                var local = this;
                return X.Instance.CurrentContext.GetValue<int>(sc =>
                {
                    return local._client.NumberOfOptimizationCycles;
                });
            }
            set
            {
                if (_client is ExpandoObject) _client.NumberOfOptimizationCycles = value;
            }
        }
    }
}