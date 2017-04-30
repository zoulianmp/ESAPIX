using System.Dynamic;
using System.Xml;
using System.Xml.Schema;
using X = ESAPIX.Facade.XContext;

namespace ESAPIX.Facade.Types
{
    public class MetersetValue
    {
        internal dynamic _client;

        public MetersetValue()
        {
            _client = new ExpandoObject();
        }

        public MetersetValue(dynamic client)
        {
            _client = client;
        }

        public MetersetValue(double value, DosimeterUnit unit)
        {
            if (X.Instance.CurrentContext != null)
            {
                X.Instance.CurrentContext.Thread.Invoke(() =>
                {
                    _client = VMSConstructor.ConstructMetersetValue(value, unit);
                });
            }
            else
            {
                _client = new ExpandoObject();
                _client.Value = value;
                _client.Unit = unit;
            }
        }

        public bool IsLive => !DefaultHelper.IsDefault(_client);

        public double Value
        {
            get
            {
                if (_client is ExpandoObject) return _client.Value;
                var local = this;
                return X.Instance.CurrentContext.GetValue<double>(sc => { return local._client.Value; });
            }
            set
            {
                if (_client is ExpandoObject) _client.Value = value;
            }
        }

        public DosimeterUnit Unit
        {
            get
            {
                if (_client is ExpandoObject) return _client.Unit;
                var local = this;
                return X.Instance.CurrentContext.GetValue(sc => { return (DosimeterUnit) local._client.Unit; });
            }
            set
            {
                if (_client is ExpandoObject) _client.Unit = value;
            }
        }

        public XmlSchema GetSchema()
        {
            var local = this;
            var retVal = X.Instance.CurrentContext.GetValue(sc => { return local._client.GetSchema(); });
            return retVal;
        }

        public void ReadXml(XmlReader reader)
        {
            var local = this;
            X.Instance.CurrentContext.Thread.Invoke(() => { local._client.ReadXml(reader); });
        }

        public void WriteXml(XmlWriter writer)
        {
            var local = this;
            X.Instance.CurrentContext.Thread.Invoke(() => { local._client.WriteXml(writer); });
        }
    }
}