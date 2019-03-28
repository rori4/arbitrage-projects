// Decompiled with JetBrains decompiler
// Type: Com.Lmax.Api.Account.Account
// Assembly: LmaxClientLibrary, Version=1.8.2.0, Culture=neutral, PublicKeyToken=f45712b013135aca
// MVID: 5CF42455-8EBA-41F5-A8DC-3DC7279B187B
// Assembly location: C:\Users\range\Desktop\Arbitrage\new hope\ipcbus\LmaxClientLibrary.dll

namespace Com.Lmax.Api.Account
{
  public class Account
  {
    private readonly long accountId;
    private readonly string accountName;

    public Account(long accountId, string accountName)
    {
      this.accountId = accountId;
      this.accountName = accountName;
    }

    public bool Equals(Com.Lmax.Api.Account.Account other)
    {
      if (object.ReferenceEquals((object) null, (object) other))
        return false;
      if (object.ReferenceEquals((object) this, (object) other))
        return true;
      return other.accountId == this.accountId && object.Equals((object) other.accountName, (object) this.accountName);
    }

    public override bool Equals(object obj)
    {
      if (object.ReferenceEquals((object) null, obj))
        return false;
      if (object.ReferenceEquals((object) this, obj))
        return true;
      if (obj.GetType() != typeof (Com.Lmax.Api.Account.Account))
        return false;
      return this.Equals((Com.Lmax.Api.Account.Account) obj);
    }

    public override int GetHashCode()
    {
      return this.accountId.GetHashCode() * 397 ^ (this.accountName != null ? this.accountName.GetHashCode() : 0);
    }
  }
}
