// Decompiled with JetBrains decompiler
// Type: Com.Lmax.Api.Internal.Protocol.URLHandler
// Assembly: LmaxClientLibrary, Version=1.8.2.0, Culture=neutral, PublicKeyToken=f45712b013135aca
// MVID: 5CF42455-8EBA-41F5-A8DC-3DC7279B187B
// Assembly location: C:\Users\range\Desktop\Arbitrage\new hope\ipcbus\LmaxClientLibrary.dll

using System;
using System.Collections.Generic;

namespace Com.Lmax.Api.Internal.Protocol
{
  internal class URLHandler : Handler
  {
    private const string RootNodeName = "url";
    private readonly List<Uri> _urls;

    public URLHandler(List<Uri> urls)
      : base("url")
    {
      this._urls = urls;
    }

    public override void EndElement(string endElement)
    {
      if (!"url".Equals(endElement))
        return;
      this._urls.Add(new Uri(this.Content));
    }

    public List<Uri> GetFiles()
    {
      return this._urls;
    }
  }
}
