// Decompiled with JetBrains decompiler
// Type: Com.Lmax.Api.Internal.Protocol.LoginResponseHandler
// Assembly: LmaxClientLibrary, Version=1.8.2.0, Culture=neutral, PublicKeyToken=f45712b013135aca
// MVID: 5CF42455-8EBA-41F5-A8DC-3DC7279B187B
// Assembly location: C:\Users\range\Desktop\Arbitrage\new hope\ipcbus\LmaxClientLibrary.dll

using Com.Lmax.Api.Account;
using System;

namespace Com.Lmax.Api.Internal.Protocol
{
  public class LoginResponseHandler : DefaultHandler
  {
    private const string CURRENCY = "currency";
    private const string ACCOUNT_ID = "accountId";
    private const string USERNAME = "username";
    private const string REGISTRATION_LEGAL_ENTITY = "registrationLegalEntity";
    private const string FAILURE_TYPE = "failureType";
    private const string DISPLAY_LOCALE = "displayLocale";
    private const string FUNDING_DISALLOWED = "fundingDisallowed";
    private AccountDetails _accountDetails;

    public LoginResponseHandler()
    {
      this.AddHandler("accountId");
      this.AddHandler("username");
      this.AddHandler("registrationLegalEntity");
      this.AddHandler("failureType");
      this.AddHandler("currency");
      this.AddHandler("displayLocale");
      this.AddHandler("fundingDisallowed");
    }

    public override void EndElement(string local)
    {
      if (!("body" == local) || !this.IsOk)
        return;
      long longValue;
      this.TryGetValue("accountId", out longValue);
      string stringValue1 = this.GetStringValue("username");
      string stringValue2 = this.GetStringValue("registrationLegalEntity");
      bool fundingAllowed = false.ToString().Equals(this.GetStringValue("fundingDisallowed"), StringComparison.OrdinalIgnoreCase);
      string stringValue3 = this.GetStringValue("currency");
      string stringValue4 = this.GetStringValue("displayLocale");
      this._accountDetails = new AccountDetails(longValue, stringValue1, stringValue3, stringValue2, stringValue4, fundingAllowed);
    }

    public AccountDetails AccountDetails
    {
      get
      {
        return this._accountDetails;
      }
    }

    public string FailureType
    {
      get
      {
        return this.GetStringValue("failureType");
      }
    }
  }
}
