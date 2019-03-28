// Decompiled with JetBrains decompiler
// Type: WPBase.IDataConnector
// Assembly: WPLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A67F71FE-CC9D-4C7E-B402-72B871993086
// Assembly location: C:\Program Files (x86)\Westernpips\Westernpips Trade Monitor 3.7 Exclusive\WPLib.dll

using System;

namespace WPBase
{
  public interface IDataConnector
  {
    ISecurityProvider SecurityProvider { get; set; }

    bool start(SessionParameters _params);

    bool stop();

    ITradeDataWriter DataWriter { get; set; }

    bool addInstrument(InstrumentInfo _newinstrument);

    DateTime lastUpdate();
  }
}
