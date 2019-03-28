// Decompiled with JetBrains decompiler
// Type: Com.Lmax.Api.LmaxApi
// Assembly: LmaxClientLibrary, Version=1.8.2.0, Culture=neutral, PublicKeyToken=f45712b013135aca
// MVID: 5CF42455-8EBA-41F5-A8DC-3DC7279B187B
// Assembly location: C:\Users\range\Desktop\Arbitrage\new hope\ipcbus\LmaxClientLibrary.dll

using Com.Lmax.Api.Internal;
using Com.Lmax.Api.Internal.Protocol;
using System;

namespace Com.Lmax.Api
{
  public class LmaxApi
  {
    private readonly IHttpInvoker _httpInvoker;
    private readonly IXmlParser _xmlParser;
    private readonly string _baseUri;

    public LmaxApi(string baseUri, IHttpInvoker httpInvoker, IXmlParser xmlParser)
    {
      this._baseUri = baseUri;
      this._httpInvoker = httpInvoker;
      this._xmlParser = xmlParser;
    }

    public LmaxApi(string urlBase)
      : this(urlBase, (IHttpInvoker) new HttpInvoker(), (IXmlParser) new SaxParser())
    {
    }

    public LmaxApi(string urlBase, string clientIdentifier)
      : this(urlBase, (IHttpInvoker) new HttpInvoker(LmaxApi.TruncateClientId(clientIdentifier)), (IXmlParser) new SaxParser())
    {
    }

    public void Login(LoginRequest loginRequest, OnLogin loginCallback, OnFailure failureCallback)
    {
      LoginResponseHandler loginResponseHandler = new LoginResponseHandler();
      try
      {
        string sessionId;
        Response response = this._httpInvoker.Invoke(this._baseUri, (IRequest) loginRequest, this._xmlParser, (Handler) loginResponseHandler, out sessionId);
        if (response.IsOk)
        {
          if (loginResponseHandler.IsOk)
            loginCallback((ISession) new Session(loginResponseHandler.AccountDetails, this._baseUri, this._httpInvoker, this._xmlParser, sessionId, true));
          else
            failureCallback(new FailureResponse(false, loginResponseHandler.FailureType, loginResponseHandler.Message, (Exception) null));
        }
        else
          failureCallback(new FailureResponse(true, "HttpStatus: " + (object) response.Status + ", for: " + this._baseUri + loginRequest.Uri));
      }
      catch (Exception ex)
      {
        failureCallback(new FailureResponse(ex, "URI: " + this._baseUri + loginRequest.Uri));
      }
    }

    private static string TruncateClientId(string clientIdentifier)
    {
      if (clientIdentifier == null)
        return "";
      if (clientIdentifier.Length < 25)
        return clientIdentifier;
      return clientIdentifier.Substring(0, 25);
    }
  }
}
