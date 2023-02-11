using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StooqWebsite.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace StooqWebsite.Tests;

[TestClass]
public class StooqWebsiteClientTests
{
    const string fileContent =
            @"Data,Otwarcie,Najwyzszy,Najnizszy,Zamkniecie
2014-10-27,3.3186,3.333,3.3115,3.324
2014-10-28,3.3241,3.3306,3.3062,3.3064";

    [TestMethod]
    public async Task Should_FormatDataRequestCorrectly()
    {
        string symbol = "IGLN";

        Func<HttpRequestMessage, bool> requestPredicate = request =>
        {
            return request.Method == HttpMethod.Get && request.RequestUri.AbsoluteUri == @"https://stooq.pl/q/d/l/?s=IGLN&i=d";
        };

        var httpHandler = A.Fake<HttpMessageHandler>();
        var client = new StooqWebsiteClient(new HttpClient(httpHandler));

        A.CallTo(httpHandler).Where(x => x.Method.Name == "SendAsync")
            .WithReturnType<Task<HttpResponseMessage>>().Returns(new HttpResponseMessage(HttpStatusCode.BadRequest));
        A.CallTo(httpHandler).Where(x => x.Method.Name == "SendAsync" && requestPredicate(x.GetArgument<HttpRequestMessage>(0)))
            .WithReturnType<Task<HttpResponseMessage>>().Returns(new HttpResponseMessage(HttpStatusCode.OK));

        var result = await client.GetData(symbol);
        Assert.IsNotNull(result);
    }

    [TestMethod]
    public async Task Should_FormatDataRequestForDatesCorrectly()
    {
        string symbol = "IGLN";

        Func<HttpRequestMessage, bool> requestPredicate = request =>
        {
            return request.Method == HttpMethod.Get && request.RequestUri.AbsoluteUri == @"https://stooq.pl/q/d/l/?s=IGLN&d1=20000101&d2=20000131&i=d";
        };

        var httpHandler = A.Fake<HttpMessageHandler>();
        var client = new StooqWebsiteClient(new HttpClient(httpHandler));

        A.CallTo(httpHandler).Where(x => x.Method.Name == "SendAsync")
            .WithReturnType<Task<HttpResponseMessage>>().Returns(new HttpResponseMessage(HttpStatusCode.BadRequest));
        A.CallTo(httpHandler).Where(x => x.Method.Name == "SendAsync" && requestPredicate(x.GetArgument<HttpRequestMessage>(0)))
            .WithReturnType<Task<HttpResponseMessage>>().Returns(new HttpResponseMessage(HttpStatusCode.OK));

        var result = await client.GetData(symbol, new DateTime(2000, 1, 1), new DateTime(2000, 1, 31));
        Assert.IsNotNull(result);
    }

    [TestMethod]
    [ExpectedException(typeof(StooqWebsiteException))]
    public async Task Should_ThrowStooqWebsiteException_When_ResponseStatusCodeIsNot200()
    {
        string symbol = "IGLN";

        var httpHandler = A.Fake<HttpMessageHandler>();
        var client = new StooqWebsiteClient(new HttpClient(httpHandler));

        A.CallTo(httpHandler).Where(x => x.Method.Name == "SendAsync")
            .WithReturnType<Task<HttpResponseMessage>>().Returns(new HttpResponseMessage(HttpStatusCode.BadRequest));

        await client.GetData(symbol);
    }

    [TestMethod]
    [ExpectedException(typeof(StooqWebsiteException))]
    public async Task Should_ThrowStooqWebsiteException_When_HttpClientThrowsAnyException()
    {
        string symbol = "IGLN";

        var httpHandler = A.Fake<HttpMessageHandler>();
        var client = new StooqWebsiteClient(new HttpClient(httpHandler));

        A.CallTo(httpHandler).Where(x => x.Method.Name == "SendAsync")
            .WithReturnType<Task<HttpResponseMessage>>()
            .Throws(new Exception());

        await client.GetData(symbol);
    }
}
