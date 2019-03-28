// Decompiled with JetBrains decompiler
// Type: Com.Lmax.Api.Internal.DateTimeUtil
// Assembly: LmaxClientLibrary, Version=1.8.2.0, Culture=neutral, PublicKeyToken=f45712b013135aca
// MVID: 5CF42455-8EBA-41F5-A8DC-3DC7279B187B
// Assembly location: C:\Users\range\Desktop\Arbitrage\new hope\ipcbus\LmaxClientLibrary.dll

using System;

namespace Com.Lmax.Api.Internal
{
  public class DateTimeUtil
  {
    private static readonly DateTime UnixEpochStart = new DateTime(1970, 1, 1);

    public static long DateTimeToMillis(DateTime dateTime)
    {
      return (long) (dateTime - DateTimeUtil.UnixEpochStart).TotalMilliseconds;
    }
  }
}
