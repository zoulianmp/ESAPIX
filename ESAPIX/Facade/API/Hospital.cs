using System;
using System.Dynamic;
using System.Xml;
using X = ESAPIX.Facade.XContext;

namespace ESAPIX.Facade.API
{
    public class Hospital : ApiDataObject
    {
        public Hospital()
        {
            _client = new ExpandoObject();
        }

        public Hospital(dynamic client)
        {
            _client = client;
        }

        public bool IsLive => !DefaultHelper.IsDefault(_client);

        public DateTime? CreationDateTime
        {
            get
            {
                if (_client is ExpandoObject) return _client.CreationDateTime;
                var local = this;
                return X.Instance.CurrentContext.GetValue<DateTime?>(sc => { return local._client.CreationDateTime; });
            }
            set
            {
                if (_client is ExpandoObject) _client.CreationDateTime = value;
            }
        }

        public string Location
        {
            get
            {
                if (_client is ExpandoObject) return _client.Location;
                var local = this;
                return X.Instance.CurrentContext.GetValue<string>(sc => { return local._client.Location; });
            }
            set
            {
                if (_client is ExpandoObject) _client.Location = value;
            }
        }

        public void WriteXml(XmlWriter writer)
        {
            var local = this;
            X.Instance.CurrentContext.Thread.Invoke(() => { local._client.WriteXml(writer); });
        }
    }
}