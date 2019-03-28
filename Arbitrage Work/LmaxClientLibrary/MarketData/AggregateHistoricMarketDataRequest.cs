// Decompiled with JetBrains decompiler
// Type: Com.Lmax.Api.MarketData.AggregateHistoricMarketDataRequest
// Assembly: LmaxClientLibrary, Version=1.8.2.0, Culture=neutral, PublicKeyToken=f45712b013135aca
// MVID: 5CF42455-8EBA-41F5-A8DC-3DC7279B187B
// Assembly location: C:\Users\range\Desktop\Arbitrage\new hope\ipcbus\LmaxClientLibrary.dll

using Com.Lmax.Api.Internal;
using Com.Lmax.Api.Internal.Xml;
using System;

namespace Com.Lmax.Api.MarketData
{
  public class AggregateHistoricMarketDataRequest : IHistoricMarketDataRequest, IRequest
  {
    private readonly long _instructionId;
    private readonly long _instrumentId;
    private readonly DateTime _from;
    private readonly DateTime _to;
    private readonly Option[] _options;
    private readonly Resolution _resolution;
    private readonly Format _format;
    private readonly int _depth;

    public AggregateHistoricMarketDataRequest(long instructionId, long instrumentId, DateTime from, DateTime to, Resolution resolution, Format format, params Option[] options)
    {
      this._instructionId = instructionId;
      this._instrumentId = instrumentId;
      this._from = from;
      this._to = to;
      this._options = options;
      this._resolution = resolution;
      this._format = format;
      this._depth = 1;
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
      writer.StartElement("req").StartElement("body").ValueOrNone("instructionId", new long?(this._instructionId)).ValueOrNone("orderBookId", new long?(this._instrumentId)).ValueOrNone("from", Convert.ToString(DateTimeUtil.DateTimeToMillis(this._from))).ValueOrNone("to", Convert.ToString(DateTimeUtil.DateTimeToMillis(this._to))).StartElement("aggregate");
      this.WriteOptions(writer).ValueOrNone("resolution", Convert.ToString((object) this._resolution).ToUpper()).ValueOrNone("depth", new long?((long) this._depth)).ValueOrNone("format", Convert.ToString((object) this._format).ToUpper()).EndElement("aggregate").EndElement("body").EndElement("req");
    }

    private IStructuredWriter WriteOptions(IStructuredWriter writer)
    {
      if (this._options.Length <= 0)
        return writer;
      writer.StartElement("options");
      foreach (Option option in this._options)
        writer.ValueOrNone("option", Convert.ToString((object) option).ToUpper());
      writer.EndElement("options");
      return writer;
    }
  }
}
