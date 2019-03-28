// Decompiled with JetBrains decompiler
// Type: b_сlass
// Assembly: lmaxdatafeed, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DFEBD8BE-A1D3-43D8-B547-6EC80FD649BE
// Assembly location: C:\Users\range\Desktop\Arbitrage\new hope\ipcbus\lmaxdatafeed.exe

using Com.Lmax.Api;
using Com.Lmax.Api.Account;
using Com.Lmax.Api.OrderBook;
using lmaxdatafeed;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

internal class b_сlass
{
  public static byte d = 0;
  public static bool f = false;
  public static int g = 0;
  private static bool j = false;
  private int bp = 5;
  private Dictionary<long, c_class> a_c;
  private ISession _session;
  public static string e;
  private long h;
  public Form1 i;
  public Thread k;
  private RegistryKey readKey;
  private RegistryKey saveKey;

  public void fm()
  {
    ProductType productType = !this.i.type.Contains("CFD_DEMO") ? ProductType.CFD_LIVE : ProductType.CFD_DEMO;
    while (true)
    {
      LmaxApi lmaxApi = new LmaxApi(this.i.url);
      this.i.WriteLog("Attempting to login to: " + this.i.url + " as " + this.i.login);
      lmaxApi.Login(new LoginRequest(this.i.login, this.i.password, productType), new OnLogin(this.a1), b_сlass.a1("Failed to log in"));
      this.i.WriteLog("Logged out, pausing for 10s before retrying...");
      Thread.Sleep(10000);
    }
  }

  public bool a1(string A_0, string A_1, string A_2)
  {
    this.k = new Thread(new ThreadStart(this.fm));
    this.k.IsBackground = true;
    this.k.Start();
    return b_сlass.j;
  }

  private void b1(string A_0)
  {
    this.i.WriteLog("Received heartbeat: " + A_0);
  }

  private void e1()
  {
    try
    {
      while (Thread.CurrentThread.IsAlive)
      {
        Thread.Sleep(300000);
        this.d1();
      }
    }
    catch (ThreadInterruptedException ex)
    {
      this.i.WriteLog(ex.StackTrace);
    }
  }

  private void d1()
  {
    long h;
    this.h = (h = this.h) + 1L;
    this._session.RequestHeartbeat(new HeartbeatRequest("token-" + (object) h), (OnSuccess) (() => {}), (OnFailure) (A_0 => {}));
  }

  public b_сlass(Form1 A_0)
  {
    this.i = A_0;
  }

  private void c1()
  {
    this.i.WriteLog("Session Disconnected");
    this.i.SetStatus(false);
  }

  private void a1(Exception A_0)
  {
    this.i.WriteLog("Error occured on the stream " + A_0.Message);
    this.i.WriteLog(A_0.StackTrace);
    if (A_0 is FileNotFoundException)
      this._session.Stop();
    if (--this.bp != -1)
      return;
    this._session.Stop();
  }

  private void a1(OrderBookEvent A_0)
  {
    long instrumentId = A_0.InstrumentId;
    string s1 = this.openKeyInst(instrumentId, "bid");
    string s2 = this.openKeyInst(instrumentId, "ask");
    int num = this.i.InstrumentIDs.IndexOf(instrumentId.ToString());
    if (num < 0)
      return;
    this.i.SendDataToBus(num, double.Parse(s2), double.Parse(s1));
    if (this.i.ShowQuotes)
    {
      this.i.SetDataGridValue(num, 2, s2.ToString());
      this.i.SetDataGridValue(num, 3, s1.ToString());
    }
  }

  private string openKeyInst(long instrumentId, string name)
  {
    if (name == "bid" && instrumentId == 4001L)
      return this.openKeyF("EURUSDbid").ToString();
    if (name == "ask" && instrumentId == 4001L)
      return this.openKeyF("EURUSDask").ToString();
    if (name == "bid" && instrumentId == 4004L)
      return this.openKeyF("USDJPYbid").ToString();
    if (name == "ask" && instrumentId == 4004L)
      return this.openKeyF("USDJPYask").ToString();
    if (name == "bid" && instrumentId == 4007L)
      return this.openKeyF("AUDUSDbid").ToString();
    if (name == "ask" && instrumentId == 4007L)
      return this.openKeyF("AUDUSDask").ToString();
    if (name == "bid" && instrumentId == 4002L)
      return this.openKeyF("GBPUSDbid").ToString();
    if (name == "ask" && instrumentId == 4002L)
      return this.openKeyF("GBPUSDask").ToString();
    if (name == "bid" && instrumentId == 4010L)
      return this.openKeyF("USDCHFbid").ToString();
    if (name == "ask" && instrumentId == 4010L)
      return this.openKeyF("USDCHFask").ToString();
    if (name == "bid" && instrumentId == 100613L)
      return this.openKeyF("NZDUSDbid").ToString();
    if (name == "ask" && instrumentId == 100613L)
      return this.openKeyF("NZDUSDask").ToString();
    if (name == "bid" && instrumentId == 4006L)
      return this.openKeyF("EURJPYbid").ToString();
    if (name == "ask" && instrumentId == 4006L)
      return this.openKeyF("EURJPYask").ToString();
    return "0";
  }

  private string openKeyF(string name)
  {
    this.readKey = Registry.LocalMachine.OpenSubKey("HARDWARE");
    string str = (string) this.readKey.GetValue(name);
    this.readKey.Close();
    return str;
  }

  private static Decimal a1(List<PricePoint> A_0)
  {
    if (A_0.Count == 0)
      return new Decimal(0);
    return A_0[0].Price;
  }

  private void a1(ISession A_0)
  {
    this.i.WriteLog("My accountId is: " + (object) A_0.AccountDetails.AccountId);
    this.i.SetStatus(true);
    this._session = A_0;
    this._session.MarketDataChanged += new OnOrderBookEvent(this.a1);
    this._session.EventStreamFailed += new OnException(this.a1);
    this._session.EventStreamSessionDisconnected += new OnSessionDisconnected(this.c1);
    this._session.HeartbeatReceived += new OnHeartbeatReceivedEvent(this.b1);
    A_0.Subscribe((ISubscriptionRequest) new HeartbeatSubscriptionRequest(), (OnSuccess) (() => this.i.WriteLog("Subscribed to heartbeat")), b_сlass.a1("subscribe to heartbeats"));
    new Thread(new ThreadStart(this.e1))
    {
      IsBackground = true
    }.Start();
    for (int index = 0; index < this.i.InstrumentIDs.Count<string>(); ++index)
      this.a1(Convert.ToInt64(Convert.ToDecimal(this.i.InstrumentIDs[index])));
    A_0.Start();
  }

  private void a1(long A_0)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    b_сlass.a a = new b_сlass.a();
    // ISSUE: reference to a compiler-generated field
    a.b = A_0;
    // ISSUE: reference to a compiler-generated field
    a.a2 = this;
    // ISSUE: reference to a compiler-generated field
    this.i.WriteLog("Subscribing to: " + (object) a.b);
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated method
    // ISSUE: reference to a compiler-generated method
    this._session.Subscribe((ISubscriptionRequest) new OrderBookSubscriptionRequest(a.b), new OnSuccess(a.c), new OnFailure(a.c));
  }

  private static OnFailure a1(string A_0)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: reference to a compiler-generated method
    return new OnFailure(new b_сlass.b()
    {
      a = A_0
    }.b2);
  }
}
