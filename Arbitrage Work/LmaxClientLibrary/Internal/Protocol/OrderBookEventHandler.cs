// Decompiled with JetBrains decompiler
// Type: Com.Lmax.Api.Internal.Protocol.OrderBookEventHandler
// Assembly: LmaxClientLibrary, Version=1.8.2.0, Culture=neutral, PublicKeyToken=f45712b013135aca
// MVID: 5CF42455-8EBA-41F5-A8DC-3DC7279B187B
// Assembly location: C:\Users\range\Desktop\Arbitrage\new hope\ipcbus\LmaxClientLibrary.dll

using Com.Lmax.Api.OrderBook;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Com.Lmax.Api.Internal.Protocol
{
  public class OrderBookEventHandler : Handler
  {
    private static readonly char[] PipeDelimiter = new char[1]
    {
      '|'
    };
    private static readonly char[] SemicolonDelimiter = new char[1]
    {
      ';'
    };
    private static readonly char[] AtDelimiter = new char[1]
    {
      '@'
    };
    private const int InstrumentId = 0;
    private const int Timestamp = 1;
    private const int Bids = 2;
    private const int Asks = 3;
    private const int MarketClose = 4;
    private const int DailyHigh = 5;
    private const int DailyLow = 6;
    private const int ValuationBid = 7;
    private const int ValuationAsk = 8;
    private const int LastTraded = 9;

    public OrderBookEventHandler()
      : base("ob2")
    {
    }

    public override void EndElement(string endElement)
    {
      if (!(this.ElementName == endElement))
        return;
      string[] strArray = this.Content.Split(OrderBookEventHandler.PipeDelimiter);
      long instrumentId = OrderBookEventHandler.ParseLong(strArray[0]);
      long timestap = OrderBookEventHandler.ParseTimestap(strArray[1]);
      Decimal output1;
      bool hasValuationBidPrice = OrderBookEventHandler.TryParseDecimal(strArray[7], out output1);
      Decimal output2;
      bool hasValuationAskPrice = OrderBookEventHandler.TryParseDecimal(strArray[8], out output2);
      Decimal output3;
      bool hasLastTradedPrice = OrderBookEventHandler.TryParseDecimal(strArray[9], out output3);
      Decimal output4;
      bool hasDailyHighestTradedPrice = OrderBookEventHandler.TryParseDecimal(strArray[5], out output4);
      Decimal output5;
      bool hasDailyLowestTradedPrice = OrderBookEventHandler.TryParseDecimal(strArray[6], out output5);
      List<PricePoint> prices1 = OrderBookEventHandler.ParsePrices(strArray[2]);
      List<PricePoint> prices2 = OrderBookEventHandler.ParsePrices(strArray[3]);
      Decimal marketClosePrice;
      long marketClosePriceTimestamp;
      bool marketClose = OrderBookEventHandler.TryParseMarketClose(strArray[4], out marketClosePrice, out marketClosePriceTimestamp);
      this.MarketDataChanged(new OrderBookEvent(instrumentId, hasValuationBidPrice, hasValuationAskPrice, output1, output2, prices1, prices2, marketClose, marketClosePrice, marketClosePriceTimestamp, hasLastTradedPrice, output3, hasDailyHighestTradedPrice, output4, hasDailyLowestTradedPrice, output5, timestap));
    }

    public event OnOrderBookEvent MarketDataChanged;

    private static bool TryParseMarketClose(string payload, out Decimal marketClosePrice, out long marketClosePriceTimestamp)
    {
      if (payload.Length == 0)
      {
        marketClosePrice = new Decimal(0);
        marketClosePriceTimestamp = 0L;
        return false;
      }
      string[] strArray = payload.Split(OrderBookEventHandler.SemicolonDelimiter);
      if (strArray[0].Length == 0)
      {
        marketClosePrice = new Decimal(0);
        marketClosePriceTimestamp = 0L;
        return false;
      }
      marketClosePrice = Decimal.Parse(strArray[0], (IFormatProvider) DefaultHandler.NumberFormat);
      marketClosePriceTimestamp = OrderBookEventHandler.ParseTimestap(strArray[1]);
      return true;
    }

    private static List<PricePoint> ParsePrices(string payload)
    {
      if (payload.Length == 0)
        return new List<PricePoint>(0);
      string[] strArray1 = payload.Split(OrderBookEventHandler.SemicolonDelimiter);
      List<PricePoint> pricePointList = new List<PricePoint>(strArray1.Length);
      foreach (string str in strArray1)
      {
        string[] strArray2 = str.Split(OrderBookEventHandler.AtDelimiter, 2);
        pricePointList.Add(new PricePoint(Decimal.Parse(strArray2[0], (IFormatProvider) DefaultHandler.NumberFormat), Decimal.Parse(strArray2[1], (IFormatProvider) DefaultHandler.NumberFormat)));
      }
      return pricePointList;
    }

    private static bool TryParseDecimal(string value, out Decimal output)
    {
      if (value.Length == 0)
      {
        output = new Decimal(0);
        return false;
      }
      output = Decimal.Parse(value, (IFormatProvider) DefaultHandler.NumberFormat);
      return true;
    }

    private static long ParseTimestap(string value)
    {
      return long.Parse(value, NumberStyles.HexNumber);
    }

    private static long ParseLong(string value)
    {
      return long.Parse(value, (IFormatProvider) DefaultHandler.NumberFormat);
    }
  }
}
