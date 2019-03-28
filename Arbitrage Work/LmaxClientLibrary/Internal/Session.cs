// Decompiled with JetBrains decompiler
// Type: Com.Lmax.Api.Internal.Session
// Assembly: LmaxClientLibrary, Version=1.8.2.0, Culture=neutral, PublicKeyToken=f45712b013135aca
// MVID: 5CF42455-8EBA-41F5-A8DC-3DC7279B187B
// Assembly location: C:\Users\range\Desktop\Arbitrage\new hope\ipcbus\LmaxClientLibrary.dll

using Com.Lmax.Api.Account;
using Com.Lmax.Api.Internal.Protocol;
using Com.Lmax.Api.Internal.Xml;
using Com.Lmax.Api.MarketData;
using Com.Lmax.Api.Order;
using Com.Lmax.Api.OrderBook;
using System;
using System.Net;
using System.Threading;

namespace Com.Lmax.Api.Internal
{
  public class Session : ISession
  {
    private readonly OrderBookEventHandler _orderBookEventHandler = new OrderBookEventHandler();
    private readonly OrderBookStatusEventHandler _orderBookStatusEventHandler = new OrderBookStatusEventHandler();
    private readonly OrderStateEventHandler _orderStateEventHandler = new OrderStateEventHandler();
    private readonly InstructionRejectedEventHandler _instructionRejectedEventHandler = new InstructionRejectedEventHandler();
    private readonly PositionEventHandler _positionEventHandler = new PositionEventHandler();
    private readonly AccountStateEventHandler _accountStateEventHandler = new AccountStateEventHandler();
    private readonly HeartbeatEventHandler _heartbeatEventHandler = new HeartbeatEventHandler();
    private readonly HistoricMarketDataEventHandler _historicMarketDataEventHandler = new HistoricMarketDataEventHandler();
    private int _volatileRunFlag = 0;
    private const int Stopped = 0;
    private const int Running = 1;
    private readonly AccountDetails _accountDetails;
    private readonly IHttpInvoker _httpInvoker;
    private readonly string _baseUri;
    private readonly IXmlParser _xmlParser;
    private readonly DefaultHandler _eventHandler;
    private readonly string _sessionId;
    private readonly bool _restartStreamOnFailure;
    private readonly EventStreamHandler _eventStreamHandler;
    private volatile IConnection _streamConnection;

    public event OnOrderBookEvent MarketDataChanged
    {
      add
      {
        this._orderBookEventHandler.MarketDataChanged += value;
      }
      remove
      {
        this._orderBookEventHandler.MarketDataChanged -= value;
      }
    }

    public event OnOrderBookStatusEvent OrderBookStatusChanged
    {
      add
      {
        this._orderBookStatusEventHandler.OrderBookStatusChanged += value;
      }
      remove
      {
        this._orderBookStatusEventHandler.OrderBookStatusChanged -= value;
      }
    }

    public event OnPositionEvent PositionChanged
    {
      add
      {
        this._positionEventHandler.PositionEventListener += value;
      }
      remove
      {
        this._positionEventHandler.PositionEventListener -= value;
      }
    }

    public event OnException EventStreamFailed;

    public event OnSessionDisconnected EventStreamSessionDisconnected;

    public event OnExecutionEvent OrderExecuted
    {
      add
      {
        this._orderStateEventHandler.ExecutionEvent += value;
      }
      remove
      {
        this._orderStateEventHandler.ExecutionEvent -= value;
      }
    }

    public event OnOrderEvent OrderChanged
    {
      add
      {
        this._orderStateEventHandler.OrderEvent += value;
      }
      remove
      {
        this._orderStateEventHandler.OrderEvent -= value;
      }
    }

    public event OnRejectionEvent InstructionRejected
    {
      add
      {
        this._instructionRejectedEventHandler.RejectionEventListener += value;
      }
      remove
      {
        this._instructionRejectedEventHandler.RejectionEventListener -= value;
      }
    }

    public event OnAccountStateEvent AccountStateUpdated
    {
      add
      {
        this._accountStateEventHandler.AccountStateUpdated += value;
      }
      remove
      {
        this._accountStateEventHandler.AccountStateUpdated -= value;
      }
    }

    public event OnHistoricMarketDataEvent HistoricMarketDataReceived
    {
      add
      {
        this._historicMarketDataEventHandler.HistoricMarketDataReceived += value;
      }
      remove
      {
        this._historicMarketDataEventHandler.HistoricMarketDataReceived -= value;
      }
    }

    public event OnHeartbeatReceivedEvent HeartbeatReceived
    {
      add
      {
        this._heartbeatEventHandler.HeartbeatReceived += value;
      }
      remove
      {
        this._heartbeatEventHandler.HeartbeatReceived -= value;
      }
    }

    public Session(AccountDetails accountDetails, string baseUri, IHttpInvoker httpInvoker, IXmlParser xmlParser, string sessionId, bool restartStreamOnFailure)
    {
      this._accountDetails = accountDetails;
      this._baseUri = baseUri;
      this._httpInvoker = httpInvoker;
      this._xmlParser = xmlParser;
      this._eventHandler = new DefaultHandler();
      this._eventStreamHandler = new EventStreamHandler((ISaxContentHandler) new SaxContentHandler((Handler) this._eventHandler));
      this._sessionId = sessionId;
      this._restartStreamOnFailure = restartStreamOnFailure;
      this._eventHandler.AddHandler((Handler) this._orderBookEventHandler);
      this._eventHandler.AddHandler((Handler) this._orderBookStatusEventHandler);
      this._eventHandler.AddHandler((Handler) this._orderStateEventHandler);
      this._eventHandler.AddHandler((Handler) this._instructionRejectedEventHandler);
      this._eventHandler.AddHandler((Handler) this._positionEventHandler);
      this._eventHandler.AddHandler((Handler) this._accountStateEventHandler);
      this._eventHandler.AddHandler((Handler) this._historicMarketDataEventHandler);
      this._eventHandler.AddHandler((Handler) this._heartbeatEventHandler);
    }

    public Session(AccountDetails accountDetails, string baseUri, IHttpInvoker httpInvoker, IXmlParser xmlParser, EventStreamHandler eventStreamHandler, DefaultHandler eventHandler, string sessionId, bool restartStreamOnFailure)
    {
      this._accountDetails = accountDetails;
      this._baseUri = baseUri;
      this._httpInvoker = httpInvoker;
      this._xmlParser = xmlParser;
      this._eventHandler = eventHandler;
      this._eventStreamHandler = eventStreamHandler;
      this._sessionId = sessionId;
      this._restartStreamOnFailure = restartStreamOnFailure;
    }

    public string Id
    {
      get
      {
        return this._sessionId;
      }
    }

    public AccountDetails AccountDetails
    {
      get
      {
        return this._accountDetails;
      }
    }

    public bool IsRunning
    {
      get
      {
        return 1 == Thread.VolatileRead(ref this._volatileRunFlag);
      }
    }

    public void Subscribe(ISubscriptionRequest subscriptionRequest, OnSuccess successCallback, OnFailure failureCallback)
    {
      Handler handler = (Handler) new DefaultHandler();
      this.SendRequest((IRequest) subscriptionRequest, handler, (Session.OnSucessfulRequest) (() => successCallback()), failureCallback);
    }

    public void PlaceMarketOrder(MarketOrderSpecification marketOrderSpecification, OnInstructionResponse instructionCallback, OnFailure failureCallback)
    {
      OrderResponseHandler handler = new OrderResponseHandler();
      this.SendRequest((IRequest) marketOrderSpecification, (Handler) handler, (Session.OnSucessfulRequest) (() => instructionCallback(handler.InstructionId)), failureCallback);
    }

    public void PlaceLimitOrder(LimitOrderSpecification limitOrderSpecification, OnInstructionResponse instructionCallback, OnFailure failureCallback)
    {
      OrderResponseHandler handler = new OrderResponseHandler();
      this.SendRequest((IRequest) limitOrderSpecification, (Handler) handler, (Session.OnSucessfulRequest) (() => instructionCallback(handler.InstructionId)), failureCallback);
    }

    public void PlaceStopOrder(StopOrderSpecification stopOrderSpecification, OnInstructionResponse instructionCallback, OnFailure failureCallback)
    {
      OrderResponseHandler handler = new OrderResponseHandler();
      this.SendRequest((IRequest) stopOrderSpecification, (Handler) handler, (Session.OnSucessfulRequest) (() => instructionCallback(handler.InstructionId)), failureCallback);
    }

    public void CancelOrder(CancelOrderRequest cancelOrderRequest, OnInstructionResponse instructionCallback, OnFailure failureCallback)
    {
      OrderResponseHandler handler = new OrderResponseHandler();
      this.SendRequest((IRequest) cancelOrderRequest, (Handler) handler, (Session.OnSucessfulRequest) (() => instructionCallback(handler.InstructionId)), failureCallback);
    }

    public void AmendStops(AmendStopLossProfitRequest amendStopLossProfitRequest, OnInstructionResponse instructionCallback, OnFailure failureCallback)
    {
      OrderResponseHandler handler = new OrderResponseHandler();
      this.SendRequest((IRequest) amendStopLossProfitRequest, (Handler) handler, (Session.OnSucessfulRequest) (() => instructionCallback(handler.InstructionId)), failureCallback);
    }

    public void RequestAccountState(AccountStateRequest accountStateRequest, OnSuccess successCallback, OnFailure failureCallback)
    {
      Handler handler = (Handler) new DefaultHandler();
      this.SendRequest((IRequest) accountStateRequest, handler, (Session.OnSucessfulRequest) (() => successCallback()), failureCallback);
    }

    public void RequestHistoricMarketData(IHistoricMarketDataRequest historicMarketDataRequest, OnSuccess successCallback, OnFailure failureCallback)
    {
      Handler handler = (Handler) new DefaultHandler();
      this.SendRequest((IRequest) historicMarketDataRequest, handler, (Session.OnSucessfulRequest) (() => successCallback()), failureCallback);
    }

    public void RequestHeartbeat(HeartbeatRequest heartbeatRequest, OnSuccess successCallback, OnFailure failureCallback)
    {
      Handler handler = (Handler) new DefaultHandler();
      this.SendRequest((IRequest) heartbeatRequest, handler, (Session.OnSucessfulRequest) (() => successCallback()), failureCallback);
    }

    public void SearchInstruments(SearchRequest searchRequest, OnSearchResponse searchCallback, OnFailure onFailure)
    {
      try
      {
        SearchResponseHandler searchResponseHandler = new SearchResponseHandler();
        Response inSession = this._httpInvoker.GetInSession(this._baseUri, (IRequest) searchRequest, this._xmlParser, (Handler) searchResponseHandler, this._sessionId);
        if (inSession.IsOk)
        {
          if (searchResponseHandler.IsOk)
            searchCallback(searchResponseHandler.Instruments, searchResponseHandler.HasMoreResults);
          else
            onFailure(new FailureResponse(false, searchResponseHandler.Message, "", (Exception) null));
        }
        else
          onFailure(new FailureResponse(true, "HttpStatus: " + (object) inSession.Status + ", for: " + this._baseUri + searchRequest.Uri));
      }
      catch (Exception ex)
      {
        onFailure(new FailureResponse(ex, "URI: " + this._baseUri + searchRequest.Uri));
      }
    }

    public void OpenUri(Uri uri, OnUriResponse uriCallback, OnFailure onFailure)
    {
      try
      {
        IConnection connection = this._httpInvoker.Connect(uri, this._sessionId);
        try
        {
          uriCallback(uri, connection.GetBinaryReader());
        }
        finally
        {
          connection.Close();
        }
      }
      catch (UnexpectedHttpStatusCodeException ex)
      {
        onFailure(new FailureResponse(true, "HttpStatus: " + (object) ex.StatusCode + ", for: " + uri.AbsoluteUri));
      }
    }

    public void Start()
    {
      if (1 == Interlocked.CompareExchange(ref this._volatileRunFlag, 1, 0))
        throw new InvalidOperationException("Can not call start twice concurrently on the same session");
      do
      {
        try
        {
          this._streamConnection = this._httpInvoker.Connect(this._baseUri, (IRequest) new Session.StreamRequest(), this._sessionId);
          this._eventStreamHandler.ParseEventStream(this._streamConnection.GetTextReader());
        }
        catch (UnexpectedHttpStatusCodeException ex)
        {
          if (1 == Thread.VolatileRead(ref this._volatileRunFlag) && HttpStatusCode.Forbidden == ex.StatusCode)
          {
            if (null != this.EventStreamSessionDisconnected)
              this.EventStreamSessionDisconnected();
            Interlocked.CompareExchange(ref this._volatileRunFlag, 0, 1);
          }
          else if (1 == Thread.VolatileRead(ref this._volatileRunFlag) && null != this.EventStreamFailed)
            this.EventStreamFailed((Exception) ex);
        }
        catch (Exception ex)
        {
          if (1 == Thread.VolatileRead(ref this._volatileRunFlag) && null != this.EventStreamFailed)
            this.EventStreamFailed(ex);
        }
        finally
        {
          if (null != this._streamConnection)
          {
            this._streamConnection.Abort();
            this._streamConnection = (IConnection) null;
          }
        }
      }
      while (this._restartStreamOnFailure && 1 == Thread.VolatileRead(ref this._volatileRunFlag));
    }

    public void Stop()
    {
      Interlocked.CompareExchange(ref this._volatileRunFlag, 0, 1);
      if (null == this._streamConnection)
        return;
      this._streamConnection.Abort();
      this._streamConnection = (IConnection) null;
    }

    public void Logout(OnSuccess successCallback, OnFailure onFailure)
    {
      this.SendRequest((IRequest) new LogoutRequest(), (Handler) new DefaultHandler(), (Session.OnSucessfulRequest) (() => successCallback()), onFailure);
    }

    private void SendRequest(IRequest request, Handler handler, Session.OnSucessfulRequest onSucessfulRequest, OnFailure failureCallback)
    {
      try
      {
        Response response = this._httpInvoker.PostInSession(this._baseUri, request, this._xmlParser, handler, this._sessionId);
        if (response.IsOk)
        {
          if (handler.IsOk)
            onSucessfulRequest();
          else
            failureCallback(new FailureResponse(false, handler.Message, handler.Content, (Exception) null));
        }
        else
          failureCallback(new FailureResponse(true, "HttpStatus: " + (object) response.Status + ", for: " + this._baseUri + request.Uri));
      }
      catch (Exception ex)
      {
        failureCallback(new FailureResponse(ex, "URI: " + this._baseUri + request.Uri));
      }
    }

    private delegate void OnSucessfulRequest();

    private class StreamRequest : IRequest
    {
      public string Uri
      {
        get
        {
          return "/push/stream";
        }
      }

      public void WriteTo(IStructuredWriter writer)
      {
      }
    }
  }
}
