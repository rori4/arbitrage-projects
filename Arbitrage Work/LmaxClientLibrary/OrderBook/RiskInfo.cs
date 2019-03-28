// Decompiled with JetBrains decompiler
// Type: Com.Lmax.Api.OrderBook.RiskInfo
// Assembly: LmaxClientLibrary, Version=1.8.2.0, Culture=neutral, PublicKeyToken=f45712b013135aca
// MVID: 5CF42455-8EBA-41F5-A8DC-3DC7279B187B
// Assembly location: C:\Users\range\Desktop\Arbitrage\new hope\ipcbus\LmaxClientLibrary.dll

using System;

namespace Com.Lmax.Api.OrderBook
{
  public class RiskInfo
  {
    private readonly Decimal _marginRate;
    private readonly Decimal _maximumPosition;

    public RiskInfo(Decimal marginRate, Decimal maximumPosition)
    {
      this._marginRate = marginRate;
      this._maximumPosition = maximumPosition;
    }

    public Decimal MarginRate
    {
      get
      {
        return this._marginRate;
      }
    }

    public Decimal MaximumPosition
    {
      get
      {
        return this._maximumPosition;
      }
    }

    public bool Equals(RiskInfo other)
    {
      if (object.ReferenceEquals((object) null, (object) other))
        return false;
      if (object.ReferenceEquals((object) this, (object) other))
        return true;
      return other._marginRate == this._marginRate && other._maximumPosition == this._maximumPosition;
    }

    public override bool Equals(object obj)
    {
      if (object.ReferenceEquals((object) null, obj))
        return false;
      if (object.ReferenceEquals((object) this, obj))
        return true;
      if (obj.GetType() != typeof (RiskInfo))
        return false;
      return this.Equals((RiskInfo) obj);
    }

    public override int GetHashCode()
    {
      Decimal num1 = this._marginRate;
      int num2 = num1.GetHashCode() * 397;
      num1 = this._maximumPosition;
      int hashCode = num1.GetHashCode();
      return num2 ^ hashCode;
    }

    public override string ToString()
    {
      return string.Format("MarginRate: {0}, MaximumPosition: {1}", (object) this._marginRate, (object) this._maximumPosition);
    }
  }
}
