// Decompiled with JetBrains decompiler
// Type: Com.Lmax.Api.Internal.HttpInvoker
// Assembly: LmaxClientLibrary, Version=1.8.2.0, Culture=neutral, PublicKeyToken=f45712b013135aca
// MVID: 5CF42455-8EBA-41F5-A8DC-3DC7279B187B
// Assembly location: C:\Users\range\Desktop\Arbitrage\new hope\ipcbus\LmaxClientLibrary.dll

using Com.Lmax.Api.Internal.Xml;
using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;

namespace Com.Lmax.Api.Internal
{
  public class HttpInvoker : IHttpInvoker
  {
    [ThreadStatic]
    private static XmlStructuredWriter _writer;
    private readonly string _userAgent;

    static HttpInvoker()
    {
      ServicePointManager.Expect100Continue = false;
      ServicePointManager.UseNagleAlgorithm = false;
      ServicePointManager.DefaultConnectionLimit = 5;
    }

    public HttpInvoker()
      : this("")
    {
    }

    public HttpInvoker(string clientIdentifier)
    {
      this._userAgent = "LMAX .Net API, version: " + (object) Assembly.GetExecutingAssembly().GetName().Version + ", id: " + clientIdentifier;
    }

    public virtual Response Invoke(string baseUri, IRequest request, IXmlParser xmlParser, Handler handler, out string sessionId)
    {
      HttpWebRequest webRequest = this.CreateWebRequest(baseUri, request);
      this.setUserAgent(webRequest);
      HttpInvoker.WriteRequest(webRequest, request);
      HttpWebResponse httpWebResponse = this.ReadResponse(webRequest, xmlParser, handler);
      try
      {
        string[] values = httpWebResponse.Headers.GetValues("Set-Cookie");
        if (null != values)
        {
          StringBuilder stringBuilder = new StringBuilder();
          foreach (string cookie in values)
          {
            stringBuilder.Append(this.ExtractCookiePair(cookie));
            stringBuilder.Append("; ");
          }
          stringBuilder.Remove(stringBuilder.Length - "; ".Length, "; ".Length);
          sessionId = stringBuilder.ToString();
        }
        else
          sessionId = (string) null;
      }
      finally
      {
        httpWebResponse.Close();
      }
      return new Response(httpWebResponse.StatusCode);
    }

    public virtual Response PostInSession(string baseUri, IRequest request, IXmlParser xmlParser, Handler handler, string sessionId)
    {
      HttpWebResponse httpWebResponse = this.ReadResponse(this.SendRequest(baseUri, request, sessionId), xmlParser, handler);
      HttpStatusCode statusCode = httpWebResponse.StatusCode;
      httpWebResponse.Close();
      return new Response(statusCode);
    }

    public Response GetInSession(string baseUri, IRequest request, IXmlParser xmlParser, Handler handler, string sessionId)
    {
      if (null == sessionId)
        throw new ArgumentException("'sessionId' must not be null");
      HttpWebRequest webRequest = (HttpWebRequest) WebRequest.Create(baseUri + request.Uri);
      this.setUserAgent(webRequest);
      webRequest.Method = "GET";
      webRequest.Accept = "text/xml";
      webRequest.Headers.Add("Cookie", sessionId);
      HttpWebResponse httpWebResponse = this.ReadResponse(webRequest, xmlParser, handler);
      HttpStatusCode statusCode = httpWebResponse.StatusCode;
      httpWebResponse.Close();
      return new Response(statusCode);
    }

    public IConnection Connect(Uri uri, string sessionId)
    {
      HttpWebRequest binaryGetRequest = this.CreateBinaryGetRequest(uri);
      binaryGetRequest.Headers.Add("Cookie", sessionId);
      try
      {
        HttpWebResponse response = (HttpWebResponse) binaryGetRequest.GetResponse();
        return (IConnection) new Connection((WebRequest) binaryGetRequest, (WebResponse) response);
      }
      catch (WebException ex)
      {
        throw new UnexpectedHttpStatusCodeException(((HttpWebResponse) ex.Response).StatusCode);
      }
    }

    public IConnection Connect(string baseUri, IRequest request, string sessionId)
    {
      HttpWebRequest httpWebRequest = this.SendRequest(baseUri, request, sessionId);
      try
      {
        HttpWebResponse response = (HttpWebResponse) httpWebRequest.GetResponse();
        return (IConnection) new Connection((WebRequest) httpWebRequest, (WebResponse) response);
      }
      catch (WebException ex)
      {
        throw new UnexpectedHttpStatusCodeException(((HttpWebResponse) ex.Response).StatusCode);
      }
    }

    private static XmlStructuredWriter Writer
    {
      get
      {
        return HttpInvoker._writer ?? (HttpInvoker._writer = new XmlStructuredWriter());
      }
    }

    private HttpWebRequest SendRequest(string baseUri, IRequest request, string sessionId)
    {
      if (null == sessionId)
        throw new ArgumentException("'sessionId' must not be null");
      HttpWebRequest webRequest = this.CreateWebRequest(baseUri, request);
      webRequest.Headers.Add("Cookie", sessionId);
      this.setUserAgent(webRequest);
      HttpInvoker.WriteRequest(webRequest, request);
      return webRequest;
    }

    private HttpWebResponse ReadResponse(HttpWebRequest webRequest, IXmlParser xmlParser, Handler handler)
    {
      HttpWebResponse response = (HttpWebResponse) webRequest.GetResponse();
      using (StreamReader streamReader = new StreamReader(response.GetResponseStream(), (Encoding) new UTF8Encoding()))
        xmlParser.Parse((TextReader) streamReader, (ISaxContentHandler) new SaxContentHandler(handler));
      return response;
    }

    private static void WriteRequest(HttpWebRequest webRequest, IRequest request)
    {
      try
      {
        request.WriteTo((IStructuredWriter) HttpInvoker.Writer);
        using (Stream requestStream = webRequest.GetRequestStream())
          HttpInvoker.Writer.WriteTo(requestStream);
      }
      finally
      {
        HttpInvoker.Writer.Reset();
      }
    }

    private HttpWebRequest CreateBinaryGetRequest(Uri uri)
    {
      HttpWebRequest webRequest = (HttpWebRequest) WebRequest.Create(uri);
      webRequest.Method = "GET";
      webRequest.Accept = "*/*";
      this.setUserAgent(webRequest);
      return webRequest;
    }

    private HttpWebRequest CreateWebRequest(string baseUri, IRequest request)
    {
      HttpWebRequest httpWebRequest = (HttpWebRequest) WebRequest.Create(baseUri + request.Uri);
      httpWebRequest.Method = "POST";
      httpWebRequest.ContentType = "text/xml";
      httpWebRequest.Accept = "text/xml";
      return httpWebRequest;
    }

    private void setUserAgent(HttpWebRequest webRequest)
    {
      webRequest.UserAgent = this._userAgent;
    }

    private string ExtractCookiePair(string cookie)
    {
      int length = cookie.IndexOf(';');
      return length != -1 ? cookie.Substring(0, length) : cookie;
    }
  }
}
