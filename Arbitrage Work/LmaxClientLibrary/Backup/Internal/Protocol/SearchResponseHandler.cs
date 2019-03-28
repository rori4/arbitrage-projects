// Decompiled with JetBrains decompiler
// Type: Com.Lmax.Api.Internal.Protocol.SearchResponseHandler
// Assembly: LmaxClientLibrary, Version=1.8.2.0, Culture=neutral, PublicKeyToken=f45712b013135aca
// MVID: 5CF42455-8EBA-41F5-A8DC-3DC7279B187B
// Assembly location: C:\Users\range\Desktop\Arbitrage\new hope\ipcbus\LmaxClientLibrary.dll

using Com.Lmax.Api.OrderBook;
using System;
using System.Collections.Generic;

namespace Com.Lmax.Api.Internal.Protocol
{
  public class SearchResponseHandler : DefaultHandler
  {
    private readonly List<Instrument> _instruments = new List<Instrument>();
    private readonly ListHandler _tradingDaysHandler = new ListHandler("tradingDay");
    private const string Id = "id";
    private const string Name = "name";
    private const string StartTime = "startTime";
    private const string EndTime = "endTime";
    private const string OpeningOffset = "openingOffset";
    private const string ClosingOffset = "closingOffset";
    private const string TradingDay = "tradingDay";
    private const string Timezone = "timezone";
    private const string Margin = "margin";
    private const string Currency = "currency";
    private const string UnitPrice = "unitPrice";
    private const string MinimumOrderQuantity = "minimumOrderQuantity";
    private const string OrderQuantityIncrement = "orderQuantityIncrement";
    private const string PriceIncrement = "priceIncrement";
    private const string AssetClass = "assetClass";
    private const string UnderlyingIsin = "underlyingIsin";
    private const string Symbol = "symbol";
    private const string MaximumPositionThreshold = "maximumPositionThreshold";
    private const string AggressiveCommisionRate = "aggressiveCommissionRate";
    private const string PassiveCommissionRate = "passiveCommissionRate";
    private const string MinimumCommission = "minimumCommission";
    private const string AggressiveCommissionPerContract = "aggressiveCommissionPerContract";
    private const string PassiveCommissionPerContract = "passiveCommissionPerContract";
    private const string FundingPremiumPercentage = "fundingPremiumPercentage";
    private const string FundingReductionPercentage = "fundingReductionPercentage";
    private const string LongSwapPoints = "longSwapPoints";
    private const string ShortSwapPoints = "shortSwapPoints";
    private const string FundingBaseRate = "fundingBaseRate";
    private const string DailyInteresetRateBasis = "dailyInterestRateBasis";
    private const string ContractUnitMeasure = "contractUnitOfMeasure";
    private const string ContractSize = "contractSize";
    private const string RetailVolatilityBandPercentage = "retailVolatilityBandPercentage";
    private const string HasMoreResultsTag = "hasMoreResults";

    public SearchResponseHandler()
    {
      this.AddHandler("id");
      this.AddHandler("name");
      this.AddHandler("startTime");
      this.AddHandler("endTime");
      this.AddHandler("openingOffset");
      this.AddHandler("closingOffset");
      this.AddHandler("timezone");
      this.AddHandler("margin");
      this.AddHandler("currency");
      this.AddHandler("unitPrice");
      this.AddHandler("minimumOrderQuantity");
      this.AddHandler("orderQuantityIncrement");
      this.AddHandler("priceIncrement");
      this.AddHandler("assetClass");
      this.AddHandler("underlyingIsin");
      this.AddHandler("symbol");
      this.AddHandler("maximumPositionThreshold");
      this.AddHandler("aggressiveCommissionRate");
      this.AddHandler("passiveCommissionRate");
      this.AddHandler("minimumCommission");
      this.AddHandler("aggressiveCommissionPerContract");
      this.AddHandler("passiveCommissionPerContract");
      this.AddHandler("fundingPremiumPercentage");
      this.AddHandler("fundingReductionPercentage");
      this.AddHandler("longSwapPoints");
      this.AddHandler("shortSwapPoints");
      this.AddHandler("fundingBaseRate");
      this.AddHandler("dailyInterestRateBasis");
      this.AddHandler("contractUnitOfMeasure");
      this.AddHandler("contractSize");
      this.AddHandler("retailVolatilityBandPercentage");
      this.AddHandler((Handler) this._tradingDaysHandler);
      this.AddHandler("hasMoreResults");
    }

    public override void EndElement(string endElement)
    {
      if (!("instrument" == endElement))
        return;
      this._instruments.Add(new Instrument(this.GetLongValue("id", 0L), this.GetStringValue("name"), new UnderlyingInfo(this.GetStringValue("symbol"), this.GetStringValue("underlyingIsin"), this.GetStringValue("assetClass")), new CalendarInfo(this.GetDateTime("startTime", DateTime.MinValue), this.GetDateTime("endTime"), this.GetTimeSpan("openingOffset", TimeSpan.MinValue), this.GetTimeSpan("closingOffset", TimeSpan.MinValue), this.GetStringValue("timezone"), this.GetDaysOfWeek()), new RiskInfo(this.GetDecimalValue("margin", new Decimal(0)), this.GetDecimalValue("maximumPositionThreshold", new Decimal(0))), new OrderBookInfo(this.GetDecimalValue("priceIncrement", new Decimal(0)), this.GetDecimalValue("orderQuantityIncrement", new Decimal(0)), this.GetDecimalValue("retailVolatilityBandPercentage", new Decimal(0))), new ContractInfo(this.GetStringValue("currency"), this.GetDecimalValue("unitPrice", new Decimal(0)), this.GetStringValue("contractUnitOfMeasure"), this.GetDecimalValue("contractSize", new Decimal(0))), new CommercialInfo(this.GetDecimalValue("minimumCommission", new Decimal(0)), this.GetDecimalValue("aggressiveCommissionRate"), this.GetDecimalValue("passiveCommissionRate"), this.GetDecimalValue("aggressiveCommissionPerContract"), this.GetDecimalValue("passiveCommissionPerContract"), this.GetStringValue("fundingBaseRate"), this.GetIntValue("dailyInterestRateBasis", 0), this.GetDecimalValue("fundingPremiumPercentage"), this.GetDecimalValue("fundingReductionPercentage"), this.GetDecimalValue("longSwapPoints"), this.GetDecimalValue("shortSwapPoints"))));
    }

    private List<DayOfWeek> GetDaysOfWeek()
    {
      List<DayOfWeek> contentList = this._tradingDaysHandler.GetContentList<DayOfWeek>(new Converter<string, DayOfWeek>(SearchResponseHandler.ConvertToDayOfWeek));
      this._tradingDaysHandler.Clear();
      return contentList;
    }

    private TimeSpan GetTimeSpan(string tag, TimeSpan defaultValue)
    {
      long longValue;
      if (!this.TryGetValue(tag, out longValue))
        return defaultValue;
      if (longValue < 0L)
        longValue = 1440L + longValue;
      return TimeSpan.FromMinutes((double) longValue);
    }

    private DateTime GetDateTime(string tag, DateTime defaultValue)
    {
      string stringValue = this.GetStringValue(tag);
      DateTime result;
      if (stringValue != null && DateTime.TryParse(stringValue, out result))
        return result;
      return defaultValue;
    }

    private DateTime? GetDateTime(string tag)
    {
      string stringValue = this.GetStringValue(tag);
      DateTime result;
      if (stringValue != null && DateTime.TryParse(stringValue, out result))
        return new DateTime?(result);
      return new DateTime?();
    }

    public List<Instrument> Instruments
    {
      get
      {
        return this._instruments;
      }
    }

    public bool HasMoreResults
    {
      get
      {
        return "true".Equals(this.GetStringValue("hasMoreResults"), StringComparison.OrdinalIgnoreCase);
      }
    }

    private static DayOfWeek ConvertToDayOfWeek(string dayOfWeekAsString)
    {
      return (DayOfWeek) Enum.Parse(typeof (DayOfWeek), dayOfWeekAsString, true);
    }
  }
}
