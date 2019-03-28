// Decompiled with JetBrains decompiler
// Type: DotfuscatorAttribute
// Assembly: lmaxdatafeed, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DFEBD8BE-A1D3-43D8-B547-6EC80FD649BE
// Assembly location: C:\Users\range\Desktop\Arbitrage\new hope\ipcbus\lmaxdatafeed.exe

using System;
using System.Runtime.InteropServices;

[AttributeUsage(AttributeTargets.Assembly)]
[ComVisible(false)]
public sealed class DotfuscatorAttribute : Attribute
{
  private string a;
  private int c;

  public string A
  {
    get
    {
      return this.a;
    }
  }

  public int C
  {
    get
    {
      return this.c;
    }
  }

  public DotfuscatorAttribute(string a, int c)
  {
    this.a = a;
    this.c = c;
  }
}
