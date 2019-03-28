// Decompiled with JetBrains decompiler
// Type: c_class
// Assembly: lmaxdatafeed, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DFEBD8BE-A1D3-43D8-B547-6EC80FD649BE
// Assembly location: C:\Users\range\Desktop\Arbitrage\new hope\ipcbus\lmaxdatafeed.exe

using Com.Lmax.Api.OrderBook;
using System;
using System.Runtime.CompilerServices;

internal class c_class
{
  private long a;
  private long b;

  public c_class(Instrument A_0)
  {
    this.a = A_0.Id;
  }

  public long f()
  {
    return this.a;
  }

  public void e(long A_0)
  {
    this.a = A_0;
  }

  public long h()
  {
    return this.b;
  }

  public void f(long A_0)
  {
    this.b = A_0;
  }

  [CompilerGenerated]
  public Decimal g()
  {
    // ISSUE: reference to a compiler-generated field
    return this.c;
  }

  [CompilerGenerated]
  public void e(Decimal A_0)
  {
    // ISSUE: reference to a compiler-generated field
    this.c = A_0;
  }

  [CompilerGenerated]
  public Decimal i()
  {
    // ISSUE: reference to a compiler-generated field
    return this.d;
  }

  [CompilerGenerated]
  public void f(Decimal A_0)
  {
    // ISSUE: reference to a compiler-generated field
    this.d = A_0;
  }

  public void e(Decimal A_0, Decimal A_1)
  {
    this.e(A_0);
    this.f(A_1);
  }

  private static long e()
  {
    return (long) (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
  }
}
