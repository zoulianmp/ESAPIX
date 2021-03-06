#region

using System.Dynamic;
using X = ESAPIX.Facade.XContext;

#endregion


namespace ESAPIX.Facade.API
{
    public class SerializableObject
    {
        internal dynamic _client;

        public SerializableObject()
        {
            _client = new ExpandoObject();
        }

        public SerializableObject(dynamic client)
        {
            _client = client;
        }

        public bool IsLive
        {
            get { return !DefaultHelper.IsDefault(_client) && !(_client is ExpandoObject); }
        }

        public System.Xml.Schema.XmlSchema GetSchema()
        {
            var local = this;
            var retVal = X.Instance.CurrentContext.GetValue(sc => { return local._client.GetSchema(); });
            return retVal;
        }

        public void ReadXml(System.Xml.XmlReader reader)
        {
            var local = this;
            X.Instance.CurrentContext.Thread.Invoke(() => { local._client.ReadXml(reader); });
        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            var local = this;
            X.Instance.CurrentContext.Thread.Invoke(() => { local._client.WriteXml(writer); });
        }

        public static void ClearSerializationHistory()
        {
            StaticHelper.SerializableObject_ClearSerializationHistory();
        }
    }
}