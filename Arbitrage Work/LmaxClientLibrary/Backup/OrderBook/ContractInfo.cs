// Decompiled with JetBrains decompiler
// Type: Com.Lmax.Api.OrderBook.ContractInfo
// Assembly: LmaxClientLibrary, Version=1.8.2.0, Culture=neutral, PublicKeyToken=f45712b013135aca
// MVID: 5CF42455-8EBA-41F5-A8DC-3DC7279B187B
// Assembly location: C:\Users\range\Desktop\Arbitrage\new hope\ipcbus\LmaxClientLibrary.dll

using System;

namespace Com.Lmax.Api.OrderBook
{
  public class ContractInfo
  {
    private readonly string _currency;
    private readonly Decimal _unitPrice;
    private readonly string _unitOfMeasure;
    private readonly Decimal _contractSize;

    public ContractInfo(string currency, Decimal unitPrice, string unitOfMeasure, Decimal contractSize)
    {
      this._currency = currency;
      this._unitPrice = unitPrice;
      this._unitOfMeasure = unitOfMeasure;
      this._contractSize = contractSize;
    }

    public string Currency
    {
      get
      {
        return this._currency;
      }
    }

    public Decimal UnitPrice
    {
      get
      {
        return this._unitPrice;
      }
    }

    public string UnitOfMeasure
    {
      get
      {
        return this._unitOfMeasure;
      }
    }

    public Decimal ContractSize
    {
      get
      {
        return this._contractSize;
      }
    }

    public bool Equals(ContractInfo other)
    {
      if (object.ReferenceEquals((object) null, (object) other))
        return false;
      if (object.ReferenceEquals((object) this, (object) other))
        return true;
      return object.Equals((object) other._currency, (object) this._currency) && other._unitPrice == this._unitPrice && object.Equals((object) other._unitOfMeasure, (object) this._unitOfMeasure) && other._contractSize == this._contractSize;
    }

    public override bool Equals(object obj)
    {
      if (object.ReferenceEquals((object) null, obj))
        return false;
      if (object.ReferenceEquals((object) this, obj))
        return true;
      if (obj.GetType() != typeof (ContractInfo))
        return false;
      return this.Equals((ContractInfo) obj);
    }

    public override int GetHashCode()
    {
      return (((this._currency != null ? this._currency.GetHashCode() : 0) * 397 ^ this._unitPrice.GetHashCode()) * 397 ^ (this._unitOfMeasure != null ? this._unitOfMeasure.GetHashCode() : 0)) * 397 ^ this._contractSize.GetHashCode();
    }

    public override string ToString()
    {
      return string.Format("Currency: {0}, UnitPrice: {1}, UnitOfMeasure: {2}, ContractSize: {3}", (object) this._currency, (object) this._unitPrice, (object) this._unitOfMeasure, (object) this._contractSize);
    }
  }
}
