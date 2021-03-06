#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using ESAPIX.Extensions;
using X = ESAPIX.Facade.XContext;

#endregion


namespace ESAPIX.Facade.API
{
    public class BrachyPlanSetup : PlanSetup
    {
        public BrachyPlanSetup()
        {
            _client = new ExpandoObject();
        }

        public BrachyPlanSetup(dynamic client)
        {
            _client = client;
        }

        public bool IsLive
        {
            get { return !DefaultHelper.IsDefault(_client) && !(_client is ExpandoObject); }
        }

        public string ApplicationSetupType
        {
            get
            {
                if (_client is ExpandoObject)
                    return (_client as ExpandoObject).HasProperty("ApplicationSetupType")
                        ? _client.ApplicationSetupType
                        : default(string);
                var local = this;
                return X.Instance.CurrentContext.GetValue<string>(sc => { return local._client.ApplicationSetupType; });
            }
            set
            {
                if (_client is ExpandoObject) _client.ApplicationSetupType = value;
            }
        }

        public IEnumerable<Catheter> Catheters
        {
            get
            {
                if (_client is ExpandoObject)
                {
                    if ((_client as ExpandoObject).HasProperty("Catheters"))
                        foreach (var item in _client.Catheters) yield return item;
                    else yield break;
                }
                else
                {
                    IEnumerator enumerator = null;
                    X.Instance.CurrentContext.Thread.Invoke(() =>
                    {
                        var asEnum = (IEnumerable) _client.Catheters;
                        enumerator = asEnum.GetEnumerator();
                    });
                    while (X.Instance.CurrentContext.GetValue(sc => enumerator.MoveNext()))
                    {
                        var facade = new Catheter();
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
            set
            {
                if (_client is ExpandoObject) _client.Catheters = value;
            }
        }

        public int? NumberOfPdrPulses
        {
            get
            {
                if (_client is ExpandoObject)
                    return (_client as ExpandoObject).HasProperty("NumberOfPdrPulses")
                        ? _client.NumberOfPdrPulses
                        : default(int?);
                var local = this;
                return X.Instance.CurrentContext.GetValue<int?>(sc => { return local._client.NumberOfPdrPulses; });
            }
            set
            {
                if (_client is ExpandoObject) _client.NumberOfPdrPulses = value;
            }
        }

        public double? PdrPulseInterval
        {
            get
            {
                if (_client is ExpandoObject)
                    return (_client as ExpandoObject).HasProperty("PdrPulseInterval")
                        ? _client.PdrPulseInterval
                        : default(double?);
                var local = this;
                return X.Instance.CurrentContext.GetValue<double?>(sc => { return local._client.PdrPulseInterval; });
            }
            set
            {
                if (_client is ExpandoObject) _client.PdrPulseInterval = value;
            }
        }

        public IEnumerable<SeedCollection> SeedCollections
        {
            get
            {
                if (_client is ExpandoObject)
                {
                    if ((_client as ExpandoObject).HasProperty("SeedCollections"))
                        foreach (var item in _client.SeedCollections) yield return item;
                    else yield break;
                }
                else
                {
                    IEnumerator enumerator = null;
                    X.Instance.CurrentContext.Thread.Invoke(() =>
                    {
                        var asEnum = (IEnumerable) _client.SeedCollections;
                        enumerator = asEnum.GetEnumerator();
                    });
                    while (X.Instance.CurrentContext.GetValue(sc => enumerator.MoveNext()))
                    {
                        var facade = new SeedCollection();
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
            set
            {
                if (_client is ExpandoObject) _client.SeedCollections = value;
            }
        }

        public IEnumerable<BrachySolidApplicator> SolidApplicators
        {
            get
            {
                if (_client is ExpandoObject)
                {
                    if ((_client as ExpandoObject).HasProperty("SolidApplicators"))
                        foreach (var item in _client.SolidApplicators) yield return item;
                    else yield break;
                }
                else
                {
                    IEnumerator enumerator = null;
                    X.Instance.CurrentContext.Thread.Invoke(() =>
                    {
                        var asEnum = (IEnumerable) _client.SolidApplicators;
                        enumerator = asEnum.GetEnumerator();
                    });
                    while (X.Instance.CurrentContext.GetValue(sc => enumerator.MoveNext()))
                    {
                        var facade = new BrachySolidApplicator();
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
            set
            {
                if (_client is ExpandoObject) _client.SolidApplicators = value;
            }
        }

        public DateTime? TreatmentDateTime
        {
            get
            {
                if (_client is ExpandoObject)
                    return (_client as ExpandoObject).HasProperty("TreatmentDateTime")
                        ? _client.TreatmentDateTime
                        : default(DateTime?);
                var local = this;
                return X.Instance.CurrentContext.GetValue<DateTime?>(sc => { return local._client.TreatmentDateTime; });
            }
            set
            {
                if (_client is ExpandoObject) _client.TreatmentDateTime = value;
            }
        }

        public string TreatmentTechnique
        {
            get
            {
                if (_client is ExpandoObject)
                    return (_client as ExpandoObject).HasProperty("TreatmentTechnique")
                        ? _client.TreatmentTechnique
                        : default(string);
                var local = this;
                return X.Instance.CurrentContext.GetValue<string>(sc => { return local._client.TreatmentTechnique; });
            }
            set
            {
                if (_client is ExpandoObject) _client.TreatmentTechnique = value;
            }
        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            var local = this;
            X.Instance.CurrentContext.Thread.Invoke(() => { local._client.WriteXml(writer); });
        }
    }
}