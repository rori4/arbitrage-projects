// Decompiled with JetBrains decompiler
// Type: WPBase.WPManager
// Assembly: WPLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A67F71FE-CC9D-4C7E-B402-72B871993086
// Assembly location: C:\Program Files (x86)\Westernpips\Westernpips Trade Monitor 3.7 Exclusive\WPLib.dll

using System;
using System.ComponentModel;
using System.Linq;
using System.Threading;

namespace WPBase
{
  public class WPManager
  {
    private IDataConnector dataConector;
    private ISecurityProvider securityProvider;
    private ITradeDataWriter writer;
    private SessionParameters session;
    private Thread controlThread;
    private Thread workThread;
    private bool connected;
    private AsyncOperation checkOp;

    private TimeSpan checkInterval { get; set; }

    public WPManager(IDataConnector _dataConector, ISecurityProvider _securityProvider, ITradeDataWriter _writer)
    {
      this.dataConector = _dataConector;
      this.securityProvider = _securityProvider;
      this.writer = _writer;
      this.dataConector.SecurityProvider = this.securityProvider;
      this.dataConector.DataWriter = this.writer;
      this.workThread = Thread.CurrentThread;
      this.checkOp = AsyncOperationManager.CreateOperation((object) null);
      this.checkInterval = TimeSpan.FromSeconds(10.0);
    }

    public void restart(object obj)
    {
    }

    public void initSession(SessionParameters _params)
    {
      this.session = _params;
    }

    public void registerinstruments()
    {
      if (this.securityProvider == null)
        throw new Exception("No security provider");
      if (this.dataConector == null)
        throw new Exception("No data conector");
      foreach (InstrumentInfo _newinstrument in this.securityProvider.getInsrumetns(this.session.user).Where<InstrumentInfo>((Func<InstrumentInfo, bool>) (i => i.Providerid.ToString() == this.session.connectionParameters[this.session.connectionParameters.Length - 1])))
        this.dataConector.addInstrument(_newinstrument);
    }

    public bool start()
    {
      if (this.dataConector == null)
        throw new Exception("No data conector");
      if (this.controlThread != null && this.controlThread.IsAlive)
        this.controlThread.Abort();
      this.connected = this.dataConector.start(this.session);
      return this.connected;
    }

    public void checkConnection()
    {
      while (this.connected)
      {
        Thread.Sleep(this.checkInterval);
        TimeSpan timeSpan = DateTime.UtcNow.Subtract(this.dataConector.lastUpdate());
        if (timeSpan.TotalSeconds > 30.0)
        {
          timeSpan = this.checkInterval;
          if (timeSpan.TotalMinutes < 30.0)
          {
            timeSpan = this.checkInterval;
            this.checkInterval = timeSpan.Add(TimeSpan.FromSeconds(30.0));
            break;
          }
          break;
        }
        this.checkInterval = TimeSpan.FromSeconds(10.0);
      }
      this.checkOp.Post(new SendOrPostCallback(this.restart), (object) EventArgs.Empty);
    }

    public bool stop()
    {
      this.connected = false;
      if (this.dataConector == null)
        throw new Exception("No data conector");
      int num = this.dataConector.stop() ? 1 : 0;
      this.writer.close();
      return num != 0;
    }
  }
}
