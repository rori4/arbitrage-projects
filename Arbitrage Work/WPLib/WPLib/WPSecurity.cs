// Decompiled with JetBrains decompiler
// Type: WPLib.WPSecurity
// Assembly: WPLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A67F71FE-CC9D-4C7E-B402-72B871993086
// Assembly location: C:\Program Files (x86)\Westernpips\Westernpips Trade Monitor 3.7 Exclusive\WPLib.dll

using System;
using System.Collections.Generic;
using System.Linq;
using WPBase;
using WPLib.WesternPips;

namespace WPLib
{
  public class WPSecurity : ISecurityProvider
  {
    public bool Authorize(UserData _user)
    {
      LicenseServiceClient licenseServiceClient = new LicenseServiceClient();
      bool flag;
      try
      {
        flag = licenseServiceClient.checkUser(new Trader()
        {
          Account = _user.User,
          Signature = _user.Signature
        }) == 0;
        licenseServiceClient.Close();
      }
      catch
      {
        flag = false;
      }
      return true;
    }

    public bool canUseFeed(UserData _user)
    {
      LicenseServiceClient licenseServiceClient = new LicenseServiceClient();
      bool flag;
      try
      {
        flag = (uint) licenseServiceClient.canUseFeed(new Trader()
        {
          Account = _user.User,
          Signature = _user.Signature
        }) > 0U;
        licenseServiceClient.Close();
      }
      catch
      {
        flag = false;
      }
      return true;
    }

    public List<InstrumentInfo> getInsrumetns(UserData _user)
    {
      List<InstrumentInfo> instrumentInfoList = new List<InstrumentInfo>();
      LicenseServiceClient licenseServiceClient = new LicenseServiceClient();
      foreach (InstrumentsContract instrumentsContract in ((IEnumerable<InstrumentsContract>) licenseServiceClient.getInstuments(new Trader()
      {
        Account = _user.User,
        Signature = _user.Signature
      })).Where<InstrumentsContract>((Func<InstrumentsContract, bool>) (inst => inst.Enabled)))
      {
        InstrumentInfo instrumentInfo = new InstrumentInfo()
        {
          ID = instrumentsContract.DisplayId,
          Name = instrumentsContract.Description,
          Parameters = new string[2]
          {
            instrumentsContract.Parametr1,
            instrumentsContract.Parametr2
          },
          Providerid = instrumentsContract.ProviderId
        };
        instrumentInfoList.Add(instrumentInfo);
      }
      return instrumentInfoList;
    }
  }
}
