// Decompiled with JetBrains decompiler
// Type: Com.Lmax.Api.Internal.Protocol.WalletsHandler
// Assembly: LmaxClientLibrary, Version=1.8.2.0, Culture=neutral, PublicKeyToken=f45712b013135aca
// MVID: 5CF42455-8EBA-41F5-A8DC-3DC7279B187B
// Assembly location: C:\Users\range\Desktop\Arbitrage\new hope\ipcbus\LmaxClientLibrary.dll

using System;
using System.Collections.Generic;

namespace Com.Lmax.Api.Internal.Protocol
{
  internal class WalletsHandler : DefaultHandler
  {
    private Dictionary<string, Decimal> _wallets = new Dictionary<string, Decimal>();
    private const string RootNodeName = "wallet";
    private const string CurrencyNodeName = "currency";
    private const string BalanceNodeName = "balance";

    public WalletsHandler()
      : base("wallet")
    {
      this.AddHandler("currency");
      this.AddHandler("balance");
    }

    public override void EndElement(string endElement)
    {
      if (!"wallet".Equals(endElement))
        return;
      Decimal dec;
      this.TryGetValue("balance", out dec);
      this._wallets[this.GetStringValue("currency")] = dec;
    }

    public Dictionary<string, Decimal> GetAndResetWallets()
    {
      Dictionary<string, Decimal> dictionary = new Dictionary<string, Decimal>((IDictionary<string, Decimal>) this._wallets);
      this._wallets.Clear();
      return dictionary;
    }
  }
}
