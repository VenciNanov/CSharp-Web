﻿using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using SIS.HTTP.Cookies;
using SIS.HTTP.Enums;
namespace SIS.WebServer
{
    using HTTP.Common;
    using HTTP.Exceptions;
    using HTTP.Requests;
    using HTTP.Responses;
    using HTTP.Sessions;
    using Results;
    using Routing;
    using System.IO;

    public class ConnectionHandler
    {
        private const string RootDirectoryRelativePath = "../../..";

        private const string RESOURCES_FOLDER_PATH = "../../../Resources";

        private readonly Socket client;

        private readonly ServerRoutingTable serverRoutingTable;

        public ConnectionHandler(
            Socket client,
            ServerRoutingTable serverRoutingTable)
        {
            CoreValidator.ThrowIfNull(client, nameof(client));
            CoreValidator.ThrowIfNull(serverRoutingTable, nameof(serverRoutingTable));

            this.client = client;
            this.serverRoutingTable = serverRoutingTable;
        }

        private async Task<IHttpRequest> ReadRequest()
        {
            var result = new StringBuilder();
            var data = new ArraySegment<byte>(new byte[1024]);

            while (true)
            {
                int numberOfBytesRead = await this.client.ReceiveAsync(data.Array, SocketFlags.None);

                if (numberOfBytesRead == 0)
                {
                    break;
                }

                var bytesAsString = Encoding.UTF8.GetString(data.Array, 0, numberOfBytesRead);
                result.Append(bytesAsString);

                if (numberOfBytesRead < 1023)
                {
                    break;
                }
            }

            if (result.Length == 0)
            {
                return null;
            }

            return new HttpRequest(result.ToString());
        }

        private IHttpResponse HandleRequest(IHttpRequest httpRequest)
        {
            if (!this.serverRoutingTable.Routes.ContainsKey(httpRequest.RequestMethod)
                || !this.serverRoutingTable.Routes[httpRequest.RequestMethod].ContainsKey(httpRequest.Path))
            {
                return this.ReturnIfResource(httpRequest.Path);
            }

            return this.serverRoutingTable.Routes[httpRequest.RequestMethod][httpRequest.Path].Invoke(httpRequest);
        }

        //private IHttpResponse ReturnIfResource(string path)
        //{
        //    var indexOfStartOfExtension = path.LastIndexOf('.');
        //    var indexOfStartOfNameOfResource = path.LastIndexOf('/');

        //    var requestPathExtension = path.Substring(indexOfStartOfExtension);

        //    var resourceName = path
        //        .Substring(
        //            indexOfStartOfNameOfResource);


        //    var resourcePath = RootDirectoryRelativePath
        //        + ""
        //                        + "/Resources"
        //                        + $"/{requestPathExtension.Substring(1)}"
        //                        + resourceName;

        //    if (!File.Exists(resourcePath))
        //    {
        //        return new HttpResponse(HttpResponseStatusCode.NotFound);
        //    }
        //    var content = File.ReadAllBytes(resourcePath);

        //    return new InlineResourceResult(content, HttpResponseStatusCode.Ok);
        //}

        private IHttpResponse ReturnIfResource(string path)
        {
            int indexOfLastDot = path.LastIndexOf('.');
            int indexOflastSlash = path.LastIndexOf('/');

            string fileFolder = path.Substring(indexOfLastDot + 1);
            string fileFullName = path.Substring(indexOflastSlash + 1);

            string resourceFullPath = $"{RESOURCES_FOLDER_PATH}/{fileFolder}/{fileFullName}";

            if (File.Exists(resourceFullPath))
            {
                byte[] resource = File.ReadAllBytes(resourceFullPath);

                return new InlineResourceResult(resource, HttpResponseStatusCode.Ok);
            }

            return new HttpResponse(HttpResponseStatusCode.NotFound);
        }


        private async Task PrepareResponse(IHttpResponse httpResponse)
        {
            byte[] byteSegments = httpResponse.GetBytes();

            await this.client.SendAsync(byteSegments, SocketFlags.None);
        }

        private string SetRequestSession(IHttpRequest httpRequest)
        {
            string sessionId = null;

            if (httpRequest.Cookies.ContainsCookie(HttpSessionStorage.SessionCookieKey))
            {
                var cookie = httpRequest.Cookies.GetCookie(HttpSessionStorage.SessionCookieKey);
                sessionId = cookie.Value;
                httpRequest.Session = HttpSessionStorage.GetSession(sessionId);
            }
            else
            {
                sessionId = Guid.NewGuid().ToString();
                httpRequest.Session = HttpSessionStorage.GetSession(sessionId);
            }

            return sessionId;
        }

        private void SetResponseSession(IHttpResponse httpResponse, string sessionId)
        {
            if (sessionId != null)
            {
                httpResponse
                    .AddCookie(new HttpCookie(HttpSessionStorage.SessionCookieKey
                        , $"{sessionId}; HttpOnly"));
            }
        }

        public async Task ProcessRequestAsync()
        {
            try
            {
                var httpRequest = await this.ReadRequest();

                if (httpRequest != null)
                {
                    string sessionId = this.SetRequestSession(httpRequest);

                    var httpResponse = this.HandleRequest(httpRequest);

                    this.SetResponseSession(httpResponse, sessionId);

                    await this.PrepareResponse(httpResponse);
                }
            }
            catch (BadRequestException e)
            {
                await this.PrepareResponse(new TextResult(e.Message, HttpResponseStatusCode.BadRequest));
            }
            catch (Exception e)
            {
                await this.PrepareResponse(new TextResult(e.Message, HttpResponseStatusCode.InternalServerError));
            }

            this.client.Shutdown(SocketShutdown.Both);
        }
    }
}