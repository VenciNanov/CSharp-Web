﻿using SIS.HTTP.Common;
using SIS.HTTP.Extensions;
using SIS.HTTP.Headers;
using SIS.HTTP.Headers.Contracts;
using SIS.HTTP.Response.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace SIS.HTTP.Response
{
    public class HttpResponse : IHttpResponse
    {
        public HttpResponse()
        {

        }
        public HttpResponse(HttpStatusCode statusCode)
        {
            this.Headers = new HttpHeaderCollection();
            this.Content = new byte[0];
            this.StatusCode = statusCode;
        }
        public HttpStatusCode StatusCode { get; set; }

        public IHttpHeadersCollection Headers { get; private set; }

        public byte[] Content { get; set; }

        public void AddHeader(HttpHeader header)
        {
            this.Headers.Add(header);
        }

        public byte[] GetBytes()
        {
            return Encoding.UTF8
                .GetBytes(this.ToString())
                .Concat(this.Content)
                .ToArray();
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            result
                .AppendLine($"{GlobalConstants.HttpOneProtocolFragment} {this.StatusCode.GetResponseLine()}")
                .AppendLine($"{this.Headers}")
                .AppendLine();

            return result.ToString();
        }
    }
}
