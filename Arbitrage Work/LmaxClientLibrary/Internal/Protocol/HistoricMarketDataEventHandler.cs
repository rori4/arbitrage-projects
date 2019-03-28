// Decompiled with JetBrains decompiler
// Type: Com.Lmax.Api.Internal.Protocol.HistoricMarketDataEventHandler
// Assembly: LmaxClientLibrary, Version=1.8.2.0, Culture=neutral, PublicKeyToken=f45712b013135aca
// MVID: 5CF42455-8EBA-41F5-A8DC-3DC7279B187B
// Assembly location: C:\Users\range\Desktop\Arbitrage\new hope\ipcbus\LmaxClientLibrary.dll

using System;
using System.Collections.Generic;

namespace Com.Lmax.Api.Internal.Protocol
{
  public class HistoricMarketDataEventHandler : DefaultHandler
  {
    private const string RootNodeName = "historicMarketData";
    private const string InstructionIdNodeName = "instructionId";
    private readonly List<Uri> _urls;
    private readonly URLHandler _urlHandler;

    public event OnHistoricMarketDataEvent HistoricMarketDataReceived;

    public HistoricMarketDataEventHandler()
      : base("historicMarketData")
    {
      this._urls = new List<Uri>();
      this._urlHandler = new URLHandler(this._urls);
      this.AddHandler("instructionId");
      this.AddHandler((Handler) this._urlHandler);
    }

    public override void EndElement(string endElement)
    {
      if (this.HistoricMarketDataReceived == null || !"historicMarketData".Equals(endElement))
        return;
      string stringValue;
      this.TryGetValue("instructionId", out stringValue);
      this.HistoricMarketDataReceived(stringValue, this._urlHandler.GetFiles());
    }

    public override void Reset(string element)
    {
      base.Reset(element);
      if (!"historicMarketData".Equals(element))
        return;
      this._urls.Clear();
    }
  }
}
