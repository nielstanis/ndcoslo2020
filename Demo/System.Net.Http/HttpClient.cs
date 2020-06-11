using System;

namespace System.Net.Http
{
    public class HttpClient
    {
        public System.Threading.Tasks.Task<System.IO.Stream> GetStreamAsync (Uri requestUri)
        {
            throw new NotSupportedException("You're not allowed to use this!");
        }
    }
}
