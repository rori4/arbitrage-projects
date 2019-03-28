// Decompiled with JetBrains decompiler
// Type: Com.Lmax.Api.OrderBook.UnderlyingInfo
// Assembly: LmaxClientLibrary, Version=1.8.2.0, Culture=neutral, PublicKeyToken=f45712b013135aca
// MVID: 5CF42455-8EBA-41F5-A8DC-3DC7279B187B
// Assembly location: C:\Users\range\Desktop\Arbitrage\new hope\ipcbus\LmaxClientLibrary.dll

namespace Com.Lmax.Api.OrderBook
{
  public class UnderlyingInfo
  {
    private readonly string _symbol;
    private readonly string _isin;
    private readonly string _assetClass;

    public UnderlyingInfo(string symbol, string isin, string assetClass)
    {
      this._symbol = symbol;
      this._isin = isin;
      this._assetClass = assetClass;
    }

    public string Symbol
    {
      get
      {
        return this._symbol;
      }
    }

    public string Isin
    {
      get
      {
        return this._isin;
      }
    }

    public string AssetClass
    {
      get
      {
        return this._assetClass;
      }
    }

    public bool Equals(UnderlyingInfo other)
    {
      if (object.ReferenceEquals((object) null, (object) other))
        return false;
      if (object.ReferenceEquals((object) this, (object) other))
        return true;
      return object.Equals((object) other._symbol, (object) this._symbol) && object.Equals((object) other._isin, (object) this._isin) && object.Equals((object) other._assetClass, (object) this._assetClass);
    }

    public override bool Equals(object obj)
    {
      if (object.ReferenceEquals((object) null, obj))
        return false;
      if (object.ReferenceEquals((object) this, obj))
        return true;
      if (obj.GetType() != typeof (UnderlyingInfo))
        return false;
      return this.Equals((UnderlyingInfo) obj);
    }

    public override int GetHashCode()
    {
      return ((this._symbol != null ? this._symbol.GetHashCode() : 0) * 397 ^ (this._isin != null ? this._isin.GetHashCode() : 0)) * 397 ^ (this._assetClass != null ? this._assetClass.GetHashCode() : 0);
    }

    public override string ToString()
    {
      return string.Format("Symbol: {0}, Isin: {1}, AssetClass: {2}", (object) this._symbol, (object) this._isin, (object) this._assetClass);
    }
  }
}
