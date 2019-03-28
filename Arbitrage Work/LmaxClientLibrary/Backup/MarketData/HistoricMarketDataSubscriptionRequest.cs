// Decompiled with JetBrains decompiler
// Type: Com.Lmax.Api.MarketData.HistoricMarketDataSubscriptionRequest
// Assembly: LmaxClientLibrary, Version=1.8.2.0, Culture=neutral, PublicKeyToken=f45712b013135aca
// MVID: 5CF42455-8EBA-41F5-A8DC-3DC7279B187B
// Assembly location: C:\Users\range\Desktop\Arbitrage\new hope\ipcbus\LmaxClientLibrary.dll

using Com.Lmax.Api.Internal.Xml;

namespace Com.Lmax.Api.MarketData
{
  public class HistoricMarketDataSubscriptionRequest : ISubscriptionRequest, IRequest
  {
    public string Uri
    {
      get
      {
        return "/secure/subscribe";
      }
    }

    public void WriteTo(IStructuredWriter writer)
    {
      writer.StartElement("req").StartElement("body");
      writer.StartElement("subscription");
      writer.ValueOrEmpty("type", "historicMarketData");
      writer.EndElement("subscription").EndElement("body").EndElement("req");
    }
  }
}
