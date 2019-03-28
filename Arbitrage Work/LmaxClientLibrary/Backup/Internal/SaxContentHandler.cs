// Decompiled with JetBrains decompiler
// Type: Com.Lmax.Api.Internal.SaxContentHandler
// Assembly: LmaxClientLibrary, Version=1.8.2.0, Culture=neutral, PublicKeyToken=f45712b013135aca
// MVID: 5CF42455-8EBA-41F5-A8DC-3DC7279B187B
// Assembly location: C:\Users\range\Desktop\Arbitrage\new hope\ipcbus\LmaxClientLibrary.dll

using System.Collections.Generic;

namespace Com.Lmax.Api.Internal
{
  public class SaxContentHandler : ISaxContentHandler
  {
    private readonly Stack<Handler> _handlers = new Stack<Handler>();

    public SaxContentHandler(Handler rootHandler)
    {
      this._handlers.Push(rootHandler);
    }

    public void StartElement(string tagName)
    {
      Handler handler = this._handlers.Peek().GetHandler(tagName);
      handler.Reset(tagName);
      this._handlers.Push(handler);
    }

    public void Content(string value)
    {
      this._handlers.Peek().Characters(value, 0, value.Length);
    }

    public void EndElement(string tagName)
    {
      this._handlers.Pop().EndElement(tagName);
    }
  }
}
