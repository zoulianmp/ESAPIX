using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Xml;
using X = ESAPIX.Facade.XContext;

namespace ESAPIX.Facade.API
{
    public class StructureSet : ApiDataObject
    {
        public StructureSet()
        {
            _client = new ExpandoObject();
        }

        public StructureSet(dynamic client)
        {
            _client = client;
        }

        public bool IsLive => !DefaultHelper.IsDefault(_client);

        public IEnumerable<Structure> Structures
        {
            get
            {
                IEnumerator enumerator = null;
                X.Instance.CurrentContext.Thread.Invoke(() =>
                {
                    var asEnum = (IEnumerable) _client.Structures;
                    enumerator = asEnum.GetEnumerator();
                });
                while (X.Instance.CurrentContext.GetValue(sc => enumerator.MoveNext()))
                {
                    var facade = new Structure();
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

        public Image Image
        {
            get
            {
                if (_client is ExpandoObject) return _client.Image;
                var local = this;
                return X.Instance.CurrentContext.GetValue(sc =>
                {
                    if (DefaultHelper.IsDefault(local._client.Image)) return default(Image);
                    return new Image(local._client.Image);
                });
            }
            set
            {
                if (_client is ExpandoObject) _client.Image = value;
            }
        }

        public string UID
        {
            get
            {
                if (_client is ExpandoObject) return _client.UID;
                var local = this;
                return X.Instance.CurrentContext.GetValue<string>(sc => { return local._client.UID; });
            }
            set
            {
                if (_client is ExpandoObject) _client.UID = value;
            }
        }

        public void WriteXml(XmlWriter writer)
        {
            var local = this;
            X.Instance.CurrentContext.Thread.Invoke(() => { local._client.WriteXml(writer); });
        }

        public Structure AddStructure(string dicomType, string id)
        {
            var local = this;
            var retVal = X.Instance.CurrentContext.GetValue(sc =>
            {
                return new Structure(local._client.AddStructure(dicomType, id));
            });
            return retVal;
        }

        public bool CanAddStructure(string dicomType, string id)
        {
            var local = this;
            var retVal = X.Instance.CurrentContext.GetValue(sc =>
            {
                return local._client.CanAddStructure(dicomType, id);
            });
            return retVal;
        }

        public bool CanRemoveStructure(Structure structure)
        {
            var local = this;
            var retVal = X.Instance.CurrentContext.GetValue(sc =>
            {
                return local._client.CanRemoveStructure(structure._client);
            });
            return retVal;
        }

        public void RemoveStructure(Structure structure)
        {
            var local = this;
            X.Instance.CurrentContext.Thread.Invoke(() => { local._client.RemoveStructure(structure._client); });
        }
    }
}