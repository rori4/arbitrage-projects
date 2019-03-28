// Decompiled with JetBrains decompiler
// Type: Com.Lmax.Api.Order.OrderType
// Assembly: LmaxClientLibrary, Version=1.8.2.0, Culture=neutral, PublicKeyToken=f45712b013135aca
// MVID: 5CF42455-8EBA-41F5-A8DC-3DC7279B187B
// Assembly location: C:\Users\range\Desktop\Arbitrage\new hope\ipcbus\LmaxClientLibrary.dll

namespace Com.Lmax.Api.Order
{
  public enum OrderType
  {
    MARKET,
    LIMIT,
    STOP_ORDER,
    CLOSE_OUT_ORDER_POSITION,
    CLOSE_OUT_POSITION,
    STOP_LOSS_MARKET_ORDER,
    STOP_PROFIT_LIMIT_ORDER,
    SETTLEMENT_ORDER,
    OFF_ORDERBOOK,
    REVERSAL,
    UNKNOWN,
  }
}
