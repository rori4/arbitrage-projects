// Decompiled with JetBrains decompiler
// Type: WPBase.DataConnectorBase
// Assembly: WPLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A67F71FE-CC9D-4C7E-B402-72B871993086
// Assembly location: C:\Program Files (x86)\Westernpips\Westernpips Trade Monitor 3.7 Exclusive\WPLib.dll

using System;
using System.Collections.Concurrent;

namespace WPBase
{
  public class DataConnectorBase : IDataConnector, IDisposable
  {
    protected TradeInfo currentTradeInfo = new TradeInfo();
    protected SessionParameters sessionParemeters;
    public static int instanceNum;
    public static int connectionNum;

    public ISecurityProvider SecurityProvider { get; set; }

    public ITradeDataWriter DataWriter { get; set; }

    protected ConcurrentBag<InstrumentInfo> Instruments { get; set; }

    public DateTime lastUpdate()
    {
      DateTime dateTime = DateTime.UtcNow;
      if (this.currentTradeInfo != null && this.currentTradeInfo.TickTimeUtc != DateTime.MinValue)
        dateTime = this.currentTradeInfo.TickTimeUtc;
      return dateTime;
    }

    public bool addInstrument(InstrumentInfo _newinstrument)
    {
      bool flag = true;
      try
      {
        this.Instruments.Add(_newinstrument);
      }
      catch
      {
        flag = false;
      }
      return flag;
    }

    protected DataConnectorBase()
    {
      this.Instruments = new ConcurrentBag<InstrumentInfo>();
      ++DataConnectorBase.instanceNum;
    }

    public void Dispose()
    {
      --DataConnectorBase.instanceNum;
      this.SecurityProvider = (ISecurityProvider) null;
      this.DataWriter = (ITradeDataWriter) null;
    }

    public virtual bool start(SessionParameters _params)
    {
      this.sessionParemeters = _params;
      if (this.SecurityProvider == null || this.DataWriter == null)
        return false;
      int num = this.SecurityProvider.Authorize(this.sessionParemeters.user) ? 1 : 0;
      ++DataConnectorBase.connectionNum;
      if (DataConnectorBase.connectionNum <= 1)
        return num != 0;
      throw new Exception("Too many connections");
    }

    public virtual bool stop()
    {
      --DataConnectorBase.connectionNum;
      GC.Collect();
      return true;
    }
  }
}
