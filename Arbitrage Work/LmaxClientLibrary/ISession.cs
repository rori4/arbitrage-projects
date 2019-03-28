// Decompiled with JetBrains decompiler
// Type: Com.Lmax.Api.ISession
// Assembly: LmaxClientLibrary, Version=1.8.2.0, Culture=neutral, PublicKeyToken=f45712b013135aca
// MVID: 5CF42455-8EBA-41F5-A8DC-3DC7279B187B
// Assembly location: C:\Users\range\Desktop\Arbitrage\new hope\ipcbus\LmaxClientLibrary.dll

using Com.Lmax.Api.Account;
using Com.Lmax.Api.MarketData;
using Com.Lmax.Api.Order;
using Com.Lmax.Api.OrderBook;
using System;

namespace Com.Lmax.Api
{
  public interface ISession
  {
    AccountDetails AccountDetails { get; }

    string Id { get; }

    event OnOrderBookEvent MarketDataChanged;

    event OnOrderBookStatusEvent OrderBookStatusChanged;

    event OnException EventStreamFailed;

    event OnSessionDisconnected EventStreamSessionDisconnected;

    event OnPositionEvent PositionChanged;

    event OnExecutionEvent OrderExecuted;

    event OnOrderEvent OrderChanged;

    event OnRejectionEvent InstructionRejected;

    event OnAccountStateEvent AccountStateUpdated;

    event OnHistoricMarketDataEvent HistoricMarketDataReceived;

    event OnHeartbeatReceivedEvent HeartbeatReceived;

    void Subscribe(ISubscriptionRequest subscriptionRequest, OnSuccess successCallback, OnFailure failureCallback);

    void PlaceMarketOrder(MarketOrderSpecification marketOrderSpecification, OnInstructionResponse instructionCallback, OnFailure failureCallback);

    void PlaceLimitOrder(LimitOrderSpecification limitOrderSpecification, OnInstructionResponse instructionCallback, OnFailure failureCallback);

    void PlaceStopOrder(StopOrderSpecification stopOrderSpecification, OnInstructionResponse instructionCallback, OnFailure failureCallback);

    void CancelOrder(CancelOrderRequest cancelOrderRequest, OnInstructionResponse instructionCallback, OnFailure failureCallback);

    void AmendStops(AmendStopLossProfitRequest amendStopLossProfitRequest, OnInstructionResponse instructionCallback, OnFailure failureCallback);

    void RequestAccountState(AccountStateRequest accountStateRequest, OnSuccess successCallback, OnFailure failureCallback);

    void RequestHistoricMarketData(IHistoricMarketDataRequest historicMarketDataRequest, OnSuccess successCallback, OnFailure failureCallback);

    void RequestHeartbeat(HeartbeatRequest heartbeatRequest, OnSuccess successCallback, OnFailure failureCallback);

    void SearchInstruments(SearchRequest searchRequest, OnSearchResponse searchCallback, OnFailure onFailure);

    void OpenUri(Uri uri, OnUriResponse uriCallback, OnFailure onFailure);

    void Start();

    void Stop();

    void Logout(OnSuccess successCallback, OnFailure onFailure);
  }
}
