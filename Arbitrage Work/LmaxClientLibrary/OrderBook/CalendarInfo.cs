// Decompiled with JetBrains decompiler
// Type: Com.Lmax.Api.OrderBook.CalendarInfo
// Assembly: LmaxClientLibrary, Version=1.8.2.0, Culture=neutral, PublicKeyToken=f45712b013135aca
// MVID: 5CF42455-8EBA-41F5-A8DC-3DC7279B187B
// Assembly location: C:\Users\range\Desktop\Arbitrage\new hope\ipcbus\LmaxClientLibrary.dll

using System;
using System.Collections.Generic;

namespace Com.Lmax.Api.OrderBook
{
  public class CalendarInfo
  {
    private readonly DateTime _startTime;
    private readonly DateTime? _expiryTime;
    private readonly TimeSpan _open;
    private readonly TimeSpan _close;
    private readonly string _timeZone;
    private readonly List<DayOfWeek> _tradingDays;

    public CalendarInfo(DateTime startTime, DateTime? expiryTime, TimeSpan open, TimeSpan close, string timeZone, List<DayOfWeek> tradingDays)
    {
      this._startTime = startTime;
      this._expiryTime = expiryTime;
      this._open = open;
      this._close = close;
      this._timeZone = timeZone;
      this._tradingDays = tradingDays;
    }

    public DateTime StartTime
    {
      get
      {
        return this._startTime;
      }
    }

    public DateTime? ExpiryTime
    {
      get
      {
        return this._expiryTime;
      }
    }

    public TimeSpan Open
    {
      get
      {
        return this._open;
      }
    }

    public TimeSpan Close
    {
      get
      {
        return this._close;
      }
    }

    public string TimeZone
    {
      get
      {
        return this._timeZone;
      }
    }

    public List<DayOfWeek> TradingDays
    {
      get
      {
        return this._tradingDays;
      }
    }

    public bool Equals(CalendarInfo other)
    {
      if (object.ReferenceEquals((object) null, (object) other))
        return false;
      if (object.ReferenceEquals((object) this, (object) other))
        return true;
      return other._startTime.Equals(this._startTime) && other._expiryTime.Equals((object) this._expiryTime) && (other._open.Equals(this._open) && (other._close.Equals(this._close) && object.Equals((object) other._timeZone, (object) this._timeZone))) && CalendarInfo.CompareDaysOfWeek(other._tradingDays, this._tradingDays);
    }

    private static bool CompareDaysOfWeek(List<DayOfWeek> thisObj, List<DayOfWeek> other)
    {
      if (object.ReferenceEquals((object) null, (object) other))
        return false;
      if (object.ReferenceEquals((object) thisObj, (object) other))
        return true;
      int num1 = 0;
      int num2 = 0;
      foreach (DayOfWeek dayOfWeek in thisObj)
        num1 |= 1 << (int) (dayOfWeek & (DayOfWeek) 31);
      foreach (DayOfWeek dayOfWeek in other)
        num2 |= 1 << (int) (dayOfWeek & (DayOfWeek) 31);
      return num1 == num2;
    }

    public override bool Equals(object obj)
    {
      if (object.ReferenceEquals((object) null, obj))
        return false;
      if (object.ReferenceEquals((object) this, obj))
        return true;
      if (obj.GetType() != typeof (CalendarInfo))
        return false;
      return this.Equals((CalendarInfo) obj);
    }

    public override int GetHashCode()
    {
      int num1 = (this._startTime.GetHashCode() * 397 ^ (this._expiryTime.HasValue ? this._expiryTime.Value.GetHashCode() : 0)) * 397;
      TimeSpan timeSpan = this._open;
      int hashCode1 = timeSpan.GetHashCode();
      int num2 = (num1 ^ hashCode1) * 397;
      timeSpan = this._close;
      int hashCode2 = timeSpan.GetHashCode();
      return ((num2 ^ hashCode2) * 397 ^ (this._timeZone != null ? this._timeZone.GetHashCode() : 0)) * 397 ^ (this._tradingDays != null ? this._tradingDays.GetHashCode() : 0);
    }

    public override string ToString()
    {
      return string.Format("StartTime: {0}, ExpiryTime: {1}, Open: {2}, Close: {3}, TimeZone: {4}, TradingDays: [{5}]", (object) this._startTime, (object) this._expiryTime, (object) this._open, (object) this._close, (object) this._timeZone, (object) string.Join(",", this._tradingDays.ConvertAll<string>(new Converter<DayOfWeek, string>(CalendarInfo.DayToWeekToString)).ToArray()));
    }

    private static string DayToWeekToString(DayOfWeek dayOfWeek)
    {
      return dayOfWeek.ToString();
    }
  }
}
