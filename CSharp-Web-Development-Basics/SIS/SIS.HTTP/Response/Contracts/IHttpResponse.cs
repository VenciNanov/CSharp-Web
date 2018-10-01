using SIS.HTTP.Headers;
using SIS.HTTP.Headers.Contracts;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace SIS.HTTP.Response.Contracts
{
    public interface IHttpResponse
    {
        HttpStatusCode StatusCode { get; set; }
        IHttpHeadersCollection Headers { get; }

        byte[] Content { get; set; }

        void AddHeader(HttpHeader header);
        byte[] GetBytes();
    }
}
