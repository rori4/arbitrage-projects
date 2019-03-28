// Decompiled with JetBrains decompiler
// Type: WPBase.ISecurityProvider
// Assembly: WPLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A67F71FE-CC9D-4C7E-B402-72B871993086
// Assembly location: C:\Program Files (x86)\Westernpips\Westernpips Trade Monitor 3.7 Exclusive\WPLib.dll

using System.Collections.Generic;

namespace WPBase
{
  public interface ISecurityProvider
  {
    bool Authorize(UserData _user);

    List<InstrumentInfo> getInsrumetns(UserData _user);

    bool canUseFeed(UserData _user);
  }
}
