﻿// Decompiled with JetBrains decompiler
// Type: Com.Lmax.Api.Account.AccountStateRequest
// Assembly: LmaxClientLibrary, Version=1.8.2.0, Culture=neutral, PublicKeyToken=f45712b013135aca
// MVID: 5CF42455-8EBA-41F5-A8DC-3DC7279B187B
// Assembly location: C:\Users\range\Desktop\Arbitrage\new hope\ipcbus\LmaxClientLibrary.dll

using Com.Lmax.Api.Internal.Xml;

namespace Com.Lmax.Api.Account
{
  public class AccountStateRequest : IRequest
  {
    public string Uri
    {
      get
      {
        return "/secure/account/requestAccountState";
      }
    }

    public void WriteTo(IStructuredWriter writer)
    {
      writer.StartElement("req").WriteEmptyTag("body").EndElement("req");
    }
  }
}
