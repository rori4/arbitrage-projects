// Decompiled with JetBrains decompiler
// Type: Com.Lmax.Api.Internal.Protocol.AccountStateEventHandler
// Assembly: LmaxClientLibrary, Version=1.8.2.0, Culture=neutral, PublicKeyToken=f45712b013135aca
// MVID: 5CF42455-8EBA-41F5-A8DC-3DC7279B187B
// Assembly location: C:\Users\range\Desktop\Arbitrage\new hope\ipcbus\LmaxClientLibrary.dll

using Com.Lmax.Api.Internal.Events;
using System;

namespace Com.Lmax.Api.Internal.Protocol
{
  public class AccountStateEventHandler : DefaultHandler
  {
    private readonly WalletsHandler _walletsHandler = new WalletsHandler();
    private const string RootNodeName = "accountState";
    private const string AccountIdNodeName = "accountId";
    private const string BalanceNodeName = "balance";
    private const string AvailableFundsNodeName = "availableFunds";
    private const string AvailableToWithdrawNodeName = "availableToWithdraw";
    private const string UnrealisedProfitAndLossNodeName = "unrealisedProfitAndLoss";
    private const string MarginNodeName = "margin";
    private const string ActiveNodeName = "active";

    public event OnAccountStateEvent AccountStateUpdated;

    public AccountStateEventHandler()
      : base("accountState")
    {
      this.AddHandler("accountId");
      this.AddHandler("balance");
      this.AddHandler("availableFunds");
      this.AddHandler("availableToWithdraw");
      this.AddHandler("unrealisedProfitAndLoss");
      this.AddHandler("margin");
      this.AddHandler((Handler) this._walletsHandler);
      this.AddHandler("active");
    }

    public override void EndElement(string endElement)
    {
      if (this.AccountStateUpdated == null || !"accountState".Equals(endElement))
        return;
      long longValue;
      this.TryGetValue("accountId", out longValue);
      Decimal dec1;
      this.TryGetValue("balance", out dec1);
      Decimal dec2;
      this.TryGetValue("availableFunds", out dec2);
      Decimal dec3;
      this.TryGetValue("availableToWithdraw", out dec3);
      Decimal dec4;
      this.TryGetValue("unrealisedProfitAndLoss", out dec4);
      Decimal dec5;
      this.TryGetValue("margin", out dec5);
      AccountStateBuilder accountStateBuilder = new AccountStateBuilder();
      accountStateBuilder.AccountId(longValue).Balance(dec1).AvailableFunds(dec2).AvailableToWithdraw(dec3).UnrealisedProfitAndLoss(dec4).Margin(dec5).Wallets(this._walletsHandler.GetAndResetWallets());
      this.AccountStateUpdated(accountStateBuilder.NewInstance());
    }
  }
}
