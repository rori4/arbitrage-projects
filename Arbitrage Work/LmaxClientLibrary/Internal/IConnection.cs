// Decompiled with JetBrains decompiler
// Type: Com.Lmax.Api.Internal.IConnection
// Assembly: LmaxClientLibrary, Version=1.8.2.0, Culture=neutral, PublicKeyToken=f45712b013135aca
// MVID: 5CF42455-8EBA-41F5-A8DC-3DC7279B187B
// Assembly location: C:\Users\range\Desktop\Arbitrage\new hope\ipcbus\LmaxClientLibrary.dll

using System.IO;

namespace Com.Lmax.Api.Internal
{
  public interface IConnection
  {
    TextReader GetTextReader();

    BinaryReader GetBinaryReader();

    void Abort();

    void Close();
  }
}
