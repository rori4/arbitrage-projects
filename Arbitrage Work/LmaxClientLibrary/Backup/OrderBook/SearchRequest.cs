// Decompiled with JetBrains decompiler
// Type: Com.Lmax.Api.OrderBook.SearchRequest
// Assembly: LmaxClientLibrary, Version=1.8.2.0, Culture=neutral, PublicKeyToken=f45712b013135aca
// MVID: 5CF42455-8EBA-41F5-A8DC-3DC7279B187B
// Assembly location: C:\Users\range\Desktop\Arbitrage\new hope\ipcbus\LmaxClientLibrary.dll

using Com.Lmax.Api.Internal.Xml;
using System;

namespace Com.Lmax.Api.OrderBook
{
  public class SearchRequest : IRequest
  {
    private readonly string _queryString;
    private readonly long _offsetInstrumentId;

    public SearchRequest(string queryString)
      : this(queryString, 0L)
    {
    }

    public SearchRequest(string queryString, long offsetInstrumentId)
    {
      this._queryString = queryString;
      this._offsetInstrumentId = offsetInstrumentId;
    }

    public string Uri
    {
      get
      {
        return "/secure/instrument/searchCurrentInstruments?q=" + System.Uri.EscapeDataString(this._queryString) + "&offset=" + (object) this._offsetInstrumentId;
      }
    }

    public void WriteTo(IStructuredWriter writer)
    {
      throw new InvalidOperationException("This is a GET request and it does not generate a body.");
    }

    public static string All
    {
      get
      {
        return "";
      }
    }
  }
}
