// Decompiled with JetBrains decompiler
// Type: Com.Lmax.Api.OrderBook.OrderBookEvent
// Assembly: LmaxClientLibrary, Version=1.8.2.0, Culture=neutral, PublicKeyToken=f45712b013135aca
// MVID: 5CF42455-8EBA-41F5-A8DC-3DC7279B187B
// Assembly location: C:\Users\range\Desktop\Arbitrage\new hope\ipcbus\LmaxClientLibrary.dll

using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Lmax.Api.OrderBook
{
  public class OrderBookEvent
  {
    private readonly long _instrumentId;
    private readonly bool _hasValuationBidPrice;
    private readonly bool _hasValuationAskPrice;
    private readonly Decimal _valuationBidPrice;
    private readonly Decimal _valuationAskPrice;
    private readonly List<PricePoint> _bidPrices;
    private readonly List<PricePoint> _askPrices;
    private readonly bool _hasMarketClosePrice;
    private readonly Decimal _mktClosePrice;
    private readonly Decimal _lastTradedPrice;
    private readonly bool _hasDailyHighestTradedPrice;
    private readonly Decimal _dailyHighestTradedPrice;
    private readonly bool _hasDailyLowestTradedPrice;
    private readonly Decimal _dailyLowestTradedPrice;
    private readonly long _mktClosePriceTimestamp;
    private readonly bool _hasLastTradedPrice;
    private readonly long _timestamp;

    public OrderBookEvent(long instrumentId, bool hasValuationBidPrice, bool hasValuationAskPrice, Decimal valuationBidPrice, Decimal valuationAskPrice, List<PricePoint> bidPrices, List<PricePoint> askPrices, bool hasMarketClosePrice, Decimal mktClosePrice, long mktClosePriceTimestamp, bool hasLastTradedPrice, Decimal lastTradedPrice, bool hasDailyHighestTradedPrice, Decimal dailyHighestTradedPrice, bool hasDailyLowestTradedPrice, Decimal dailyLowestTradedPrice, long timestamp)
    {
      this._instrumentId = instrumentId;
      this._hasValuationBidPrice = hasValuationBidPrice;
      this._hasValuationAskPrice = hasValuationAskPrice;
      this._valuationBidPrice = valuationBidPrice;
      this._valuationAskPrice = valuationAskPrice;
      this._bidPrices = bidPrices;
      this._askPrices = askPrices;
      this._hasMarketClosePrice = hasMarketClosePrice;
      this._mktClosePrice = mktClosePrice;
      this._lastTradedPrice = lastTradedPrice;
      this._hasDailyHighestTradedPrice = hasDailyHighestTradedPrice;
      this._dailyHighestTradedPrice = dailyHighestTradedPrice;
      this._hasDailyLowestTradedPrice = hasDailyLowestTradedPrice;
      this._dailyLowestTradedPrice = dailyLowestTradedPrice;
      this._mktClosePriceTimestamp = mktClosePriceTimestamp;
      this._hasLastTradedPrice = hasLastTradedPrice;
      this._timestamp = timestamp;
    }

    public bool HasValuationBidPrice
    {
      get
      {
        return this._hasValuationBidPrice;
      }
    }

    public bool HasValuationAskPrice
    {
      get
      {
        return this._hasValuationAskPrice;
      }
    }

    public Decimal ValuationBidPrice
    {
      get
      {
        return this._valuationBidPrice;
      }
    }

    public Decimal ValuationAskPrice
    {
      get
      {
        return this._valuationAskPrice;
      }
    }

    public long Timestamp
    {
      get
      {
        return this._timestamp;
      }
    }

    public List<PricePoint> BidPrices
    {
      get
      {
        return this._bidPrices;
      }
    }

    public List<PricePoint> AskPrices
    {
      get
      {
        return this._askPrices;
      }
    }

    public long InstrumentId
    {
      get
      {
        return this._instrumentId;
      }
    }

    public bool HasMarketClosePrice
    {
      get
      {
        return this._hasMarketClosePrice;
      }
    }

    public Decimal MktClosePrice
    {
      get
      {
        return this._mktClosePrice;
      }
    }

    public long MktClosePriceTimestamp
    {
      get
      {
        return this._mktClosePriceTimestamp;
      }
    }

    public bool HasLastTradedPrice
    {
      get
      {
        return this._hasLastTradedPrice;
      }
    }

    public Decimal LastTradedPrice
    {
      get
      {
        return this._lastTradedPrice;
      }
    }

    public bool HasDailyHighestTradedPrice
    {
      get
      {
        return this._hasDailyHighestTradedPrice;
      }
    }

    public Decimal DailyHighestTradedPrice
    {
      get
      {
        return this._dailyHighestTradedPrice;
      }
    }

    public bool HasDailyLowestTradedPrice
    {
      get
      {
        return this._hasDailyLowestTradedPrice;
      }
    }

    public Decimal DailyLowestTradedPrice
    {
      get
      {
        return this._dailyLowestTradedPrice;
      }
    }

    public bool Equals(OrderBookEvent other)
    {
      if (object.ReferenceEquals((object) null, (object) other))
        return false;
      if (object.ReferenceEquals((object) this, (object) other))
        return true;
      return other._instrumentId == this._instrumentId && other._hasValuationBidPrice.Equals(this._hasValuationBidPrice) && (other._hasValuationAskPrice.Equals(this._hasValuationAskPrice) && other._valuationBidPrice == this._valuationBidPrice && (other._valuationAskPrice == this._valuationAskPrice && other._mktClosePrice == this._mktClosePrice) && (other._lastTradedPrice == this._lastTradedPrice && other._dailyHighestTradedPrice == this._dailyHighestTradedPrice && (other._dailyLowestTradedPrice == this._dailyLowestTradedPrice && object.Equals((object) other._mktClosePriceTimestamp, (object) this._mktClosePriceTimestamp))) && (other._timestamp == this._timestamp && OrderBookEvent.EqualPrices(other._bidPrices, this._bidPrices))) && OrderBookEvent.EqualPrices(other._askPrices, this._askPrices);
    }

    private static bool EqualPrices(List<PricePoint> other, List<PricePoint> me)
    {
      if (other.Count != me.Count)
        return false;
      int index = 0;
      foreach (PricePoint pricePoint in me)
      {
        if (!pricePoint.Equals(other[index]))
          return false;
        ++index;
      }
      return true;
    }

    public override bool Equals(object obj)
    {
      if (object.ReferenceEquals((object) null, obj))
        return false;
      if (object.ReferenceEquals((object) this, obj))
        return true;
      if (obj.GetType() != typeof (OrderBookEvent))
        return false;
      return this.Equals((OrderBookEvent) obj);
    }

    public override int GetHashCode()
    {
      int num1 = ((((((this._instrumentId.GetHashCode() * 397 ^ this._hasValuationBidPrice.GetHashCode()) * 397 ^ this._hasValuationAskPrice.GetHashCode()) * 397 ^ this._valuationBidPrice.GetHashCode()) * 397 ^ this._valuationAskPrice.GetHashCode()) * 397 ^ (this._bidPrices != null ? this._bidPrices.GetHashCode() : 0)) * 397 ^ (this._askPrices != null ? this._askPrices.GetHashCode() : 0)) * 397;
      Decimal num2 = this._mktClosePrice;
      int hashCode1 = num2.GetHashCode();
      int num3 = (num1 ^ hashCode1) * 397;
      num2 = this._lastTradedPrice;
      int hashCode2 = num2.GetHashCode();
      int num4 = (num3 ^ hashCode2) * 397;
      num2 = this._dailyHighestTradedPrice;
      int hashCode3 = num2.GetHashCode();
      int num5 = (num4 ^ hashCode3) * 397;
      num2 = this._dailyLowestTradedPrice;
      int hashCode4 = num2.GetHashCode();
      int num6 = (num5 ^ hashCode4) * 397;
      long num7 = this._mktClosePriceTimestamp;
      int hashCode5 = num7.GetHashCode();
      int num8 = (num6 ^ hashCode5) * 397;
      num7 = this._timestamp;
      int hashCode6 = num7.GetHashCode();
      return num8 ^ hashCode6;
    }

    public override string ToString()
    {
      return string.Format("OrderBookEvent{{InstrumentId: {0}, ValuationBidPrice: {1}, ValuationAskPrice: {2}, BidPrices: {3}, AskPrices: {4}, MarketClosePrice: {5}, MarketClosePriceTimestamp: {6}, LastTradedPrice: {7}, DailyHighestTradedPrice: {8}, DailyLowestTradedPrice: {9}, Timestamp: {10}}}", (object) this._instrumentId, (object) this._valuationBidPrice, (object) this._valuationAskPrice, (object) OrderBookEvent.FormatPricePoints(this._bidPrices), (object) OrderBookEvent.FormatPricePoints(this._askPrices), (object) this._mktClosePrice, (object) this._mktClosePriceTimestamp, (object) this._lastTradedPrice, (object) this._dailyHighestTradedPrice, (object) this._dailyLowestTradedPrice, (object) this._timestamp);
    }

    private static string FormatPricePoints(List<PricePoint> pricePoints)
    {
      StringBuilder stringBuilder = new StringBuilder();
      foreach (PricePoint pricePoint in pricePoints)
        stringBuilder.Append(pricePoint.Quantity).Append("@").Append(pricePoint.Price).Append(", ");
      return stringBuilder.ToString();
    }

    private static class MillisToTicksConverter
    {
      private const long MillisToTicksOffset = 621355968000000000;

      public static long MillisToTicks(long millis)
      {
        return millis * 10000L + 621355968000000000L;
      }

      public static DateTime MillisToDateTime(long millis)
      {
        return new DateTime(OrderBookEvent.MillisToTicksConverter.MillisToTicks(millis));
      }
    }
  }
}
