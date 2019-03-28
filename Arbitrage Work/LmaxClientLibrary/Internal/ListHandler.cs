// Decompiled with JetBrains decompiler
// Type: Com.Lmax.Api.Internal.ListHandler
// Assembly: LmaxClientLibrary, Version=1.8.2.0, Culture=neutral, PublicKeyToken=f45712b013135aca
// MVID: 5CF42455-8EBA-41F5-A8DC-3DC7279B187B
// Assembly location: C:\Users\range\Desktop\Arbitrage\new hope\ipcbus\LmaxClientLibrary.dll

using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Lmax.Api.Internal
{
  public class ListHandler : Handler
  {
    private readonly List<string> _contentList = new List<string>();
    private readonly StringBuilder _contentBuilder = new StringBuilder();

    public ListHandler(string tag)
      : base(tag)
    {
    }

    public override void Characters(string characterData, int start, int length)
    {
      this._contentList.Add(this._contentBuilder.Append(characterData, start, length).ToString());
      this._contentBuilder.Length = 0;
    }

    public List<TOutput> GetContentList<TOutput>(Converter<string, TOutput> converter)
    {
      return this._contentList.ConvertAll<TOutput>(converter);
    }

    public void Clear()
    {
      this._contentList.Clear();
    }
  }
}
