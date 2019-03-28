// Decompiled with JetBrains decompiler
// Type: Com.Lmax.Api.Internal.Xml.IStructuredWriter
// Assembly: LmaxClientLibrary, Version=1.8.2.0, Culture=neutral, PublicKeyToken=f45712b013135aca
// MVID: 5CF42455-8EBA-41F5-A8DC-3DC7279B187B
// Assembly location: C:\Users\range\Desktop\Arbitrage\new hope\ipcbus\LmaxClientLibrary.dll

using System;

namespace Com.Lmax.Api.Internal.Xml
{
  public interface IStructuredWriter
  {
    IStructuredWriter StartElement(string name);

    IStructuredWriter EndElement(string name);

    IStructuredWriter WriteEmptyTag(string name);

    IStructuredWriter ValueUTF8(string name, string value);

    IStructuredWriter ValueOrEmpty(string name, string value);

    IStructuredWriter ValueOrNone(string name, string value);

    IStructuredWriter ValueOrEmpty(string name, long? value);

    IStructuredWriter ValueOrNone(string name, long? value);

    IStructuredWriter ValueOrEmpty(string name, Decimal? value);

    IStructuredWriter ValueOrNone(string name, Decimal? value);

    IStructuredWriter ValueOrEmpty(string name, bool value);
  }
}
