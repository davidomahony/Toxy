// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Yarp.ReverseProxy.Forwarder;
using Yarp.ReverseProxy.Transforms;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpForwarder();

var app = builder.Build();

// Configure our own HttpMessageInvoker for outbound calls for proxy operations
var httpClient = new HttpMessageInvoker(new SocketsHttpHandler()
{
    UseProxy = false,
    AllowAutoRedirect = false,
    AutomaticDecompression = DecompressionMethods.None,
    UseCookies = false,
    ActivityHeadersPropagator = new ReverseProxyPropagator(DistributedContextPropagator.Current),
    ConnectTimeout = TimeSpan.FromSeconds(15),
});

// Setup our own request transform class
var requestOptions = new ForwarderRequestConfig { ActivityTimeout = TimeSpan.FromSeconds(100) };

app.UseRouting();

// When using IHttpForwarder for direct forwarding you are responsible for routing, destination discovery, load balancing, affinity, etc..
// For an alternate example that includes those features see BasicYarpSample.
app.Map("/{**catch-all}", async (HttpContext httpContext, IHttpForwarder forwarder) =>
{
    var convertTokens = await forwarder.SendAsync(httpContext, "https://localhost:7160/Tokenization", httpClient, requestOptions,
        static (context, proxyRequest) =>
        {
            // Customize the query string:
            var queryContext = new QueryTransformContext(context.Request);
            queryContext.Collection.Remove("param1");
            queryContext.Collection["area"] = "xx2";

            // Assign the custom uri. Be careful about extra slashes when concatenating here. RequestUtilities.MakeDestinationAddress is a safe default.
            proxyRequest.RequestUri = RequestUtilities.MakeDestinationAddress(
                "https://localhost:7160/Tokenization", 
                PathString.Empty,
                QueryString.Empty);

            // Suppress the original request header, use the one from the destination Uri.
            proxyRequest.Headers.Host = null;

            return default;
        });
});

app.Run();
