// Decompiled with JetBrains decompiler
// Type: Com.Lmax.Api.OrderBook.CommercialInfo
// Assembly: LmaxClientLibrary, Version=1.8.2.0, Culture=neutral, PublicKeyToken=f45712b013135aca
// MVID: 5CF42455-8EBA-41F5-A8DC-3DC7279B187B
// Assembly location: C:\Users\range\Desktop\Arbitrage\new hope\ipcbus\LmaxClientLibrary.dll

using System;

namespace Com.Lmax.Api.OrderBook
{
  public class CommercialInfo
  {
    private readonly Decimal _minimumCommission;
    private readonly Decimal? _aggressiveCommissionRate;
    private readonly Decimal? _passiveCommissionRate;
    private readonly Decimal? _aggressiveCommissionPerContract;
    private readonly Decimal? _passiveCommissionPerContract;
    private readonly string _fundingBaseRate;
    private readonly int _dailyInterestRateBasis;
    private readonly Decimal? _fundingPremiumPercentage;
    private readonly Decimal? _fundingReductionPercentage;
    private readonly Decimal? _longSwapPoints;
    private readonly Decimal? _shortSwapPoints;

    public CommercialInfo(Decimal minimumCommission, Decimal? aggressiveCommissionRate, Decimal? passiveCommissionRate, Decimal? aggressiveCommissionPerContract, Decimal? passiveCommissionPerContract, string fundingBaseRate, int dailyInterestRateBasis, Decimal? fundingPremiumPercentage, Decimal? fundingReductionPercentage, Decimal? longSwapPoints, Decimal? shortSwapPoints)
    {
      this._minimumCommission = minimumCommission;
      this._aggressiveCommissionRate = aggressiveCommissionRate;
      this._passiveCommissionRate = passiveCommissionRate;
      this._aggressiveCommissionPerContract = aggressiveCommissionPerContract;
      this._passiveCommissionPerContract = passiveCommissionPerContract;
      this._fundingBaseRate = fundingBaseRate;
      this._dailyInterestRateBasis = dailyInterestRateBasis;
      this._fundingPremiumPercentage = fundingPremiumPercentage;
      this._fundingReductionPercentage = fundingReductionPercentage;
      this._longSwapPoints = longSwapPoints;
      this._shortSwapPoints = shortSwapPoints;
    }

    public Decimal MinimumCommission
    {
      get
      {
        return this._minimumCommission;
      }
    }

    public Decimal? AggressiveCommissionRate
    {
      get
      {
        return this._aggressiveCommissionRate;
      }
    }

    public Decimal? PassiveCommissionRate
    {
      get
      {
        return this._passiveCommissionRate;
      }
    }

    public Decimal? AggressiveCommissionPerContract
    {
      get
      {
        return this._aggressiveCommissionPerContract;
      }
    }

    public Decimal? PassiveCommissionPerContract
    {
      get
      {
        return this._passiveCommissionPerContract;
      }
    }

    public string FundingBaseRate
    {
      get
      {
        return this._fundingBaseRate;
      }
    }

    public int DailyInterestRateBasis
    {
      get
      {
        return this._dailyInterestRateBasis;
      }
    }

    [Obsolete("Use FundingPremiumPercentage, FundingReductionPercentage, LongSwapPoints and ShortSwapPoints instead")]
    public Decimal FundingRate
    {
      get
      {
        return new Decimal(0);
      }
    }

    public Decimal? FundingPremiumPercentage
    {
      get
      {
        return this._fundingPremiumPercentage;
      }
    }

    public Decimal? FundingReductionPercentage
    {
      get
      {
        return this._fundingReductionPercentage;
      }
    }

    public Decimal? LongSwapPoints
    {
      get
      {
        return this._longSwapPoints;
      }
    }

    public Decimal? ShortSwapPoints
    {
      get
      {
        return this._shortSwapPoints;
      }
    }

    public bool Equals(CommercialInfo other)
    {
      if (object.ReferenceEquals((object) null, (object) other))
        return false;
      if (object.ReferenceEquals((object) this, (object) other))
        return true;
      int num;
      if (other._minimumCommission == this._minimumCommission && other._aggressiveCommissionRate.Equals((object) this._aggressiveCommissionRate) && (other._passiveCommissionRate.Equals((object) this._passiveCommissionRate) && other._aggressiveCommissionPerContract.Equals((object) this._aggressiveCommissionPerContract)) && (other._passiveCommissionPerContract.Equals((object) this._passiveCommissionPerContract) && object.Equals((object) other._fundingBaseRate, (object) this._fundingBaseRate) && other._dailyInterestRateBasis == this._dailyInterestRateBasis))
      {
        Decimal? nullable1 = other._fundingPremiumPercentage;
        Decimal valueOrDefault1 = nullable1.GetValueOrDefault();
        Decimal? nullable2 = this._fundingPremiumPercentage;
        Decimal valueOrDefault2 = nullable2.GetValueOrDefault();
        if ((!(valueOrDefault1 == valueOrDefault2) ? 0 : (nullable1.HasValue == nullable2.HasValue ? 1 : 0)) != 0)
        {
          nullable1 = other._fundingReductionPercentage;
          Decimal valueOrDefault3 = nullable1.GetValueOrDefault();
          nullable2 = this._fundingReductionPercentage;
          Decimal valueOrDefault4 = nullable2.GetValueOrDefault();
          if ((!(valueOrDefault3 == valueOrDefault4) ? 0 : (nullable1.HasValue == nullable2.HasValue ? 1 : 0)) != 0)
          {
            nullable1 = other._longSwapPoints;
            Decimal valueOrDefault5 = nullable1.GetValueOrDefault();
            nullable2 = this._longSwapPoints;
            Decimal valueOrDefault6 = nullable2.GetValueOrDefault();
            if ((!(valueOrDefault5 == valueOrDefault6) ? 0 : (nullable1.HasValue == nullable2.HasValue ? 1 : 0)) != 0)
            {
              nullable1 = other._shortSwapPoints;
              Decimal valueOrDefault7 = nullable1.GetValueOrDefault();
              nullable2 = this._shortSwapPoints;
              Decimal valueOrDefault8 = nullable2.GetValueOrDefault();
              num = !(valueOrDefault7 == valueOrDefault8) ? 0 : (nullable1.HasValue == nullable2.HasValue ? 1 : 0);
              goto label_10;
            }
          }
        }
      }
      num = 0;
label_10:
      return num != 0;
    }

    public override bool Equals(object obj)
    {
      if (object.ReferenceEquals((object) null, obj))
        return false;
      if (object.ReferenceEquals((object) this, obj))
        return true;
      if (obj.GetType() != typeof (CommercialInfo))
        return false;
      return this.Equals((CommercialInfo) obj);
    }

    public override int GetHashCode()
    {
      int num1 = (this._minimumCommission.GetHashCode() * 397 ^ (this._aggressiveCommissionRate.HasValue ? this._aggressiveCommissionRate.Value.GetHashCode() : 0)) * 397;
      Decimal? nullable = this._passiveCommissionRate;
      int num2;
      if (!nullable.HasValue)
      {
        num2 = 0;
      }
      else
      {
        nullable = this._passiveCommissionRate;
        num2 = nullable.Value.GetHashCode();
      }
      int num3 = (num1 ^ num2) * 397;
      nullable = this._aggressiveCommissionPerContract;
      int num4;
      if (!nullable.HasValue)
      {
        num4 = 0;
      }
      else
      {
        nullable = this._aggressiveCommissionPerContract;
        num4 = nullable.Value.GetHashCode();
      }
      int num5 = (num3 ^ num4) * 397;
      nullable = this._passiveCommissionPerContract;
      int num6;
      if (!nullable.HasValue)
      {
        num6 = 0;
      }
      else
      {
        nullable = this._passiveCommissionPerContract;
        num6 = nullable.Value.GetHashCode();
      }
      int num7 = (((num5 ^ num6) * 397 ^ (this._fundingBaseRate != null ? this._fundingBaseRate.GetHashCode() : 0)) * 397 ^ this._dailyInterestRateBasis) * 397;
      nullable = this._fundingPremiumPercentage;
      int hashCode1 = nullable.GetHashCode();
      int num8 = (num7 ^ hashCode1) * 397;
      nullable = this._fundingReductionPercentage;
      int hashCode2 = nullable.GetHashCode();
      int num9 = (num8 ^ hashCode2) * 397;
      nullable = this._longSwapPoints;
      int hashCode3 = nullable.GetHashCode();
      int num10 = (num9 ^ hashCode3) * 397;
      nullable = this._shortSwapPoints;
      int hashCode4 = nullable.GetHashCode();
      return num10 ^ hashCode4;
    }

    public override string ToString()
    {
      return string.Format("MinimumCommission: {0}, AggressiveCommissionRate: {1}, PassiveCommissionRate: {2}, AggressiveCommissionPerContract: {3}, PassiveCommissionPerContract: {4}, FundingBaseRate: {5}, DailyInterestRateBasis: {6}, FundingPremiumPercentage: {7}, FundingReductionPercentage: {8}, LongSwapPoints: {9}, ShortSwapPoints{10}", (object) this._minimumCommission, (object) this._aggressiveCommissionRate, (object) this._passiveCommissionRate, (object) this._aggressiveCommissionPerContract, (object) this._passiveCommissionPerContract, (object) this._fundingBaseRate, (object) this._dailyInterestRateBasis, (object) this._fundingPremiumPercentage, (object) this._fundingReductionPercentage, (object) this._longSwapPoints, (object) this._shortSwapPoints);
    }
  }
}
