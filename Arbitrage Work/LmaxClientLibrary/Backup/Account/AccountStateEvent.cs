// Decompiled with JetBrains decompiler
// Type: Com.Lmax.Api.Account.AccountStateEvent
// Assembly: LmaxClientLibrary, Version=1.8.2.0, Culture=neutral, PublicKeyToken=f45712b013135aca
// MVID: 5CF42455-8EBA-41F5-A8DC-3DC7279B187B
// Assembly location: C:\Users\range\Desktop\Arbitrage\new hope\ipcbus\LmaxClientLibrary.dll

using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Lmax.Api.Account
{
  public sealed class AccountStateEvent : IEquatable<AccountStateEvent>
  {
    private readonly long _accountId;
    private readonly Decimal _balance;
    private readonly Decimal _availableFunds;
    private readonly Decimal _availableToWithdraw;
    private readonly Decimal _unrealisedProfitAndLoss;
    private readonly Decimal _margin;
    private readonly Dictionary<string, Decimal> _walletByCurrency;

    public AccountStateEvent(long accountId, Decimal balance, Decimal availableFunds, Decimal availableToWithdraw, Decimal unrealisedProfitAndLoss, Decimal margin, Dictionary<string, Decimal> walletByCurrency)
    {
      this._accountId = accountId;
      this._balance = balance;
      this._availableFunds = availableFunds;
      this._availableToWithdraw = availableToWithdraw;
      this._unrealisedProfitAndLoss = unrealisedProfitAndLoss;
      this._margin = margin;
      this._walletByCurrency = walletByCurrency;
    }

    public long AccountId
    {
      get
      {
        return this._accountId;
      }
    }

    public Decimal Balance
    {
      get
      {
        return this._balance;
      }
    }

    public Decimal AvailableFunds
    {
      get
      {
        return this._availableFunds;
      }
    }

    public Decimal AvailableToWithdraw
    {
      get
      {
        return this._availableToWithdraw;
      }
    }

    public Decimal UnrealisedProfitAndLoss
    {
      get
      {
        return this._unrealisedProfitAndLoss;
      }
    }

    public Decimal Margin
    {
      get
      {
        return this._margin;
      }
    }

    public Dictionary<string, Decimal> Wallets
    {
      get
      {
        return this._walletByCurrency;
      }
    }

    public bool Equals(AccountStateEvent other)
    {
      if (object.ReferenceEquals((object) null, (object) other))
        return false;
      if (object.ReferenceEquals((object) this, (object) other))
        return true;
      return other._accountId == this._accountId && other._balance == this._balance && (other._availableFunds == this._availableFunds && other._availableToWithdraw == this._availableToWithdraw) && (other._unrealisedProfitAndLoss == this._unrealisedProfitAndLoss && other._margin == this._margin) && this.walletsEquals(other._walletByCurrency);
    }

    public override bool Equals(object obj)
    {
      if (object.ReferenceEquals((object) null, obj))
        return false;
      if (object.ReferenceEquals((object) this, obj))
        return true;
      if (obj.GetType() != typeof (AccountStateEvent))
        return false;
      return this.Equals((AccountStateEvent) obj);
    }

    public override int GetHashCode()
    {
      return (((((this._accountId.GetHashCode() * 397 ^ this._balance.GetHashCode()) * 397 ^ this._availableFunds.GetHashCode()) * 397 ^ this._availableToWithdraw.GetHashCode()) * 397 ^ this._unrealisedProfitAndLoss.GetHashCode()) * 397 ^ this._margin.GetHashCode()) * 397 ^ (this._walletByCurrency != null ? this._walletByCurrency.GetHashCode() : 0);
    }

    public static bool operator ==(AccountStateEvent left, AccountStateEvent right)
    {
      return object.Equals((object) left, (object) right);
    }

    public static bool operator !=(AccountStateEvent left, AccountStateEvent right)
    {
      return !object.Equals((object) left, (object) right);
    }

    private bool walletsEquals(Dictionary<string, Decimal> otherWallets)
    {
      bool flag = true;
      if (otherWallets.Count != this._walletByCurrency.Count)
        return false;
      foreach (KeyValuePair<string, Decimal> otherWallet in otherWallets)
      {
        Decimal num = new Decimal(0);
        flag &= this._walletByCurrency.TryGetValue(otherWallet.Key, out num);
        flag = ((flag ? 1 : 0) & (!this._walletByCurrency.ContainsKey(otherWallet.Key) ? 0 : (num == otherWallet.Value ? 1 : 0))) != 0;
      }
      return flag;
    }

    public override string ToString()
    {
      return string.Format("AccountId: {0}, Balance: {1}, AvailableFunds: {2}, AvailableToWithdraw: {3}, UnrealisedProfitAndLoss: {4}, Margin: {5}, WalletByCurrency: {6}", (object) this._accountId, (object) this._balance, (object) this._availableFunds, (object) this._availableToWithdraw, (object) this._unrealisedProfitAndLoss, (object) this._margin, (object) AccountStateEvent.DictionaryToString<string, Decimal>((IEnumerable<KeyValuePair<string, Decimal>>) this._walletByCurrency, (string) null));
    }

    private static string DictionaryToString<T, V>(IEnumerable<KeyValuePair<T, V>> items, string format)
    {
      format = string.IsNullOrEmpty(format) ? "{0}='{1}' " : format;
      StringBuilder stringBuilder = new StringBuilder();
      foreach (KeyValuePair<T, V> keyValuePair in items)
        stringBuilder.AppendFormat(format, (object) keyValuePair.Key, (object) keyValuePair.Value);
      return stringBuilder.ToString();
    }
  }
}
