// Decompiled with JetBrains decompiler
// Type: Com.Lmax.Api.Account.AccountDetails
// Assembly: LmaxClientLibrary, Version=1.8.2.0, Culture=neutral, PublicKeyToken=f45712b013135aca
// MVID: 5CF42455-8EBA-41F5-A8DC-3DC7279B187B
// Assembly location: C:\Users\range\Desktop\Arbitrage\new hope\ipcbus\LmaxClientLibrary.dll

namespace Com.Lmax.Api.Account
{
  public sealed class AccountDetails
  {
    private readonly long _accountId;
    private readonly string _username;
    private readonly string _currency;
    private readonly string _legalEntity;
    private readonly string _displayLocale;
    private readonly bool _fundingAllowed;

    public AccountDetails(long accountId, string username, string currency, string legalEntity, string displayLocale, bool fundingAllowed)
    {
      this._accountId = accountId;
      this._username = username;
      this._currency = currency;
      this._legalEntity = legalEntity;
      this._displayLocale = displayLocale;
      this._fundingAllowed = fundingAllowed;
    }

    public long AccountId
    {
      get
      {
        return this._accountId;
      }
    }

    public string Username
    {
      get
      {
        return this._username;
      }
    }

    public string Currency
    {
      get
      {
        return this._currency;
      }
    }

    public string LegalEntity
    {
      get
      {
        return this._legalEntity;
      }
    }

    public string DisplayLocale
    {
      get
      {
        return this._displayLocale;
      }
    }

    public bool FundingAllowed
    {
      get
      {
        return this._fundingAllowed;
      }
    }

    public bool Equals(AccountDetails other)
    {
      if (object.ReferenceEquals((object) null, (object) other))
        return false;
      if (object.ReferenceEquals((object) this, (object) other))
        return true;
      return other._accountId == this._accountId && object.Equals((object) other._username, (object) this._username) && (object.Equals((object) other._currency, (object) this._currency) && object.Equals((object) other._legalEntity, (object) this._legalEntity)) && object.Equals((object) other._displayLocale, (object) this._displayLocale) && other._fundingAllowed.Equals(this._fundingAllowed);
    }

    public override bool Equals(object obj)
    {
      if (object.ReferenceEquals((object) null, obj))
        return false;
      if (object.ReferenceEquals((object) this, obj))
        return true;
      if (obj.GetType() != typeof (AccountDetails))
        return false;
      return this.Equals((AccountDetails) obj);
    }

    public override int GetHashCode()
    {
      return ((((this._accountId.GetHashCode() * 397 ^ (this._username != null ? this._username.GetHashCode() : 0)) * 397 ^ (this._currency != null ? this._currency.GetHashCode() : 0)) * 397 ^ (this._legalEntity != null ? this._legalEntity.GetHashCode() : 0)) * 397 ^ (this._displayLocale != null ? this._displayLocale.GetHashCode() : 0)) * 397 ^ this._fundingAllowed.GetHashCode();
    }
  }
}
