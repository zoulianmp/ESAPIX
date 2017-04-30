using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Xml;
using ESAPIX.Facade.Types;
using X = ESAPIX.Facade.XContext;

namespace ESAPIX.Facade.API
{
    public class PlanSum : PlanningItem
    {
        public PlanSum()
        {
            _client = new ExpandoObject();
        }

        public PlanSum(dynamic client)
        {
            _client = client;
        }

        public bool IsLive => !DefaultHelper.IsDefault(_client);

        public Course Course
        {
            get
            {
                if (_client is ExpandoObject) return _client.Course;
                var local = this;
                return X.Instance.CurrentContext.GetValue(sc =>
                {
                    if (DefaultHelper.IsDefault(local._client.Course)) return default(Course);
                    return new Course(local._client.Course);
                });
            }
            set
            {
                if (_client is ExpandoObject) _client.Course = value;
            }
        }

        public IEnumerable<PlanSumComponent> PlanSumComponents
        {
            get
            {
                IEnumerator enumerator = null;
                X.Instance.CurrentContext.Thread.Invoke(() =>
                {
                    var asEnum = (IEnumerable) _client.PlanSumComponents;
                    enumerator = asEnum.GetEnumerator();
                });
                while (X.Instance.CurrentContext.GetValue(sc => enumerator.MoveNext()))
                {
                    var facade = new PlanSumComponent();
                    X.Instance.CurrentContext.Thread.Invoke(() =>
                    {
                        var vms = enumerator.Current;
                        if (vms != null)
                            facade._client = vms;
                    });
                    if (facade._client != null)
                        yield return facade;
                }
            }
        }

        public StructureSet StructureSet
        {
            get
            {
                if (_client is ExpandoObject) return _client.StructureSet;
                var local = this;
                return X.Instance.CurrentContext.GetValue(sc =>
                {
                    if (DefaultHelper.IsDefault(local._client.StructureSet)) return default(StructureSet);
                    return new StructureSet(local._client.StructureSet);
                });
            }
            set
            {
                if (_client is ExpandoObject) _client.StructureSet = value;
            }
        }

        public IEnumerable<PlanSetup> PlanSetups
        {
            get
            {
                IEnumerator enumerator = null;
                X.Instance.CurrentContext.Thread.Invoke(() =>
                {
                    var asEnum = (IEnumerable) _client.PlanSetups;
                    enumerator = asEnum.GetEnumerator();
                });
                while (X.Instance.CurrentContext.GetValue(sc => enumerator.MoveNext()))
                {
                    var facade = new PlanSetup();
                    X.Instance.CurrentContext.Thread.Invoke(() =>
                    {
                        var vms = enumerator.Current;
                        if (vms != null)
                            facade._client = vms;
                    });
                    if (facade._client != null)
                        yield return facade;
                }
            }
        }

        public void WriteXml(XmlWriter writer)
        {
            var local = this;
            X.Instance.CurrentContext.Thread.Invoke(() => { local._client.WriteXml(writer); });
        }

        public PlanSumOperation GetPlanSumOperation(PlanSetup planSetupInPlanSum)
        {
            var local = this;
            var retVal = X.Instance.CurrentContext.GetValue(sc =>
            {
                return (PlanSumOperation) local._client.GetPlanSumOperation(planSetupInPlanSum._client);
            });
            return retVal;
        }

        public double GetPlanWeight(PlanSetup planSetupInPlanSum)
        {
            var local = this;
            var retVal = X.Instance.CurrentContext.GetValue(sc =>
            {
                return local._client.GetPlanWeight(planSetupInPlanSum._client);
            });
            return retVal;
        }
    }
}