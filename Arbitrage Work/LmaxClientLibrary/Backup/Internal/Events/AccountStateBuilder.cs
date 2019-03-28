// Decompiled with JetBrains decompiler
// Type: Com.Lmax.Api.Internal.Events.AccountStateBuilder
// Assembly: LmaxClientLibrary, Version=1.8.2.0, Culture=neutral, PublicKeyToken=f45712b013135aca
// MVID: 5CF42455-8EBA-41F5-A8DC-3DC7279B187B
// Assembly location: C:\Users\range\Desktop\Arbitrage\new hope\ipcbus\LmaxClientLibrary.dll

using Com.Lmax.Api.Account;
using System;
using System.Collections.Generic;

namespace Com.Lmax.Api.Internal.Events
{
  internal class AccountStateBuilder
  {
    private long _accountId;
    private Decimal _balance;
    private Decimal _availableFunds;
    private Decimal _availableToWithdraw;
    private Decimal _unrealisedProfitAndLoss;
    private Decimal _margin;
    private Dictionary<string, Decimal> _wallets;

    public AccountStateBuilder AccountId(long accountId)
    {
      this._accountId = accountId;
      return this;
    }

    public AccountStateBuilder Balance(Decimal balance)
    {
      this._balance = balance;
      return this;
    }

    public AccountStateBuilder AvailableFunds(Decimal availableFunds)
    {
      this._availableFunds = availableFunds;
      return this;
    }

    public AccountStateBuilder AvailableToWithdraw(Decimal availableToWithdraw)
    {
      this._availableToWithdraw = availableToWithdraw;
      return this;
    }

    public AccountStateBuilder UnrealisedProfitAndLoss(Decimal unrealisedProfitAndLoss)
    {
      this._unrealisedProfitAndLoss = unrealisedProfitAndLoss;
      return this;
    }

    public AccountStateBuilder Margin(Decimal margin)
    {
      this._margin = margin;
      return this;
    }

    public AccountStateBuilder Wallets(Dictionary<string, Decimal> wallets)
    {
      this._wallets = wallets;
      return this;
    }

    public AccountStateEvent NewInstance()
    {
      return new AccountStateEvent(this._accountId, this._balance, this._availableFunds, this._availableToWithdraw, this._unrealisedProfitAndLoss, this._margin, this._wallets);
    }
  }
}
