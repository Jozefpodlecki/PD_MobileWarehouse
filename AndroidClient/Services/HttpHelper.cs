using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace Client.Services
{
    public class HttpHelper
    {
        private JsonTextWriter _jsonTextWriter;
        private JsonTextReader _jsonTextReader;
        private StreamWriter _streamWriter;
        private StreamReader _streamReader;
        private MemoryStream _memoryStream;
        private Encoding _encoding;
        private JsonSerializer _jsonSerializer;

        public HttpHelper()
        {
            _jsonSerializer = new JsonSerializer();
            _jsonSerializer.MissingMemberHandling = MissingMemberHandling.Ignore;
            _encoding = new UTF8Encoding();
        }

        public HttpContent CreateJsonContent(object content)
        {
            HttpContent httpContent = null;

            _memoryStream = new MemoryStream();
            _streamWriter = new StreamWriter(_memoryStream, _encoding, 1024, true);
            _jsonTextWriter = new JsonTextWriter(_streamWriter) { Formatting = Formatting.None };
            _jsonSerializer.Serialize(_jsonTextWriter, content);
            _jsonTextWriter.Flush();
            _memoryStream.Seek(0, SeekOrigin.Begin);
            httpContent = new StreamContent(_memoryStream);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return httpContent;
        }

        public T DeserializeJsonFromStream<T>(Stream stream)
        {
            _streamReader = new StreamReader(stream, _encoding, true, 1024);
            _jsonTextReader = new JsonTextReader(_streamReader);

            try
            {
                var obj = _jsonSerializer.Deserialize<T>(_jsonTextReader);
                return obj;
            }
            catch (Exception e)
            {
                return default(T);
            }
        }

        public void CleanUpWriters()
        {
            _jsonTextWriter.Close();
            _streamWriter.Close();
            _memoryStream.Close();
        }

        public void CleanUpReaders()
        {
            _streamReader.Close();
            _jsonTextReader.Close();
        }
    }
}