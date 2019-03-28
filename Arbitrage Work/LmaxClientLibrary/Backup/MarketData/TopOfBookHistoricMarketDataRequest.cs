// Decompiled with JetBrains decompiler
// Type: Com.Lmax.Api.MarketData.TopOfBookHistoricMarketDataRequest
// Assembly: LmaxClientLibrary, Version=1.8.2.0, Culture=neutral, PublicKeyToken=f45712b013135aca
// MVID: 5CF42455-8EBA-41F5-A8DC-3DC7279B187B
// Assembly location: C:\Users\range\Desktop\Arbitrage\new hope\ipcbus\LmaxClientLibrary.dll

using Com.Lmax.Api.Internal;
using Com.Lmax.Api.Internal.Xml;
using System;

namespace Com.Lmax.Api.MarketData
{
  public class TopOfBookHistoricMarketDataRequest : IHistoricMarketDataRequest, IRequest
  {
    private const int Depth = 1;
    private readonly long _instructionId;
    private readonly long _instrumentId;
    private readonly DateTime _from;
    private readonly DateTime _to;
    private readonly Format _format;

    public TopOfBookHistoricMarketDataRequest(long instructionId, long instrumentId, DateTime from, DateTime to, Format format)
    {
      this._instructionId = instructionId;
      this._instrumentId = instrumentId;
      this._from = from;
      this._to = to;
      this._format = format;
    }

    public string Uri
    {
      get
      {
        return "/secure/read/marketData/requestHistoricMarketData";
      }
    }

    public void WriteTo(IStructuredWriter writer)
    {
      writer.StartElement("req").StartElement("body").ValueOrNone("instructionId", new long?(this._instructionId)).ValueOrNone("orderBookId", new long?(this._instrumentId)).ValueOrNone("from", Convert.ToString(DateTimeUtil.DateTimeToMillis(this._from))).ValueOrNone("to", Convert.ToString(DateTimeUtil.DateTimeToMillis(this._to))).StartElement("orderBook").StartElement("options").ValueOrNone("option", "BID").ValueOrNone("option", "ASK").EndElement("options").ValueOrNone("depth", new long?(1L)).ValueOrNone("format", Convert.ToString((object) this._format).ToUpper()).EndElement("orderBook").EndElement("body").EndElement("req");
    }
  }
}
