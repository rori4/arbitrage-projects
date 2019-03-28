// Decompiled with JetBrains decompiler
// Type: Com.Lmax.Api.Internal.EventStreamHandler
// Assembly: LmaxClientLibrary, Version=1.8.2.0, Culture=neutral, PublicKeyToken=f45712b013135aca
// MVID: 5CF42455-8EBA-41F5-A8DC-3DC7279B187B
// Assembly location: C:\Users\range\Desktop\Arbitrage\new hope\ipcbus\LmaxClientLibrary.dll

using System.IO;

namespace Com.Lmax.Api.Internal
{
  public class EventStreamHandler
  {
    private readonly ISaxContentHandler _saxContentHandler;

    public EventStreamHandler()
      : this((ISaxContentHandler) null)
    {
    }

    public EventStreamHandler(ISaxContentHandler saxContentHandler)
    {
      this._saxContentHandler = saxContentHandler;
    }

    public virtual void ParseEventStream(TextReader reader)
    {
      new SaxParser().Parse(reader, this._saxContentHandler);
    }
  }
}
