// Decompiled with JetBrains decompiler
// Type: Com.Lmax.Api.Reject.InstructionRejectedEvent
// Assembly: LmaxClientLibrary, Version=1.8.2.0, Culture=neutral, PublicKeyToken=f45712b013135aca
// MVID: 5CF42455-8EBA-41F5-A8DC-3DC7279B187B
// Assembly location: C:\Users\range\Desktop\Arbitrage\new hope\ipcbus\LmaxClientLibrary.dll

using System;

namespace Com.Lmax.Api.Reject
{
  public class InstructionRejectedEvent : IEquatable<InstructionRejectedEvent>
  {
    private readonly string _instructionId;
    private readonly long _accountId;
    private readonly long _instrumentId;
    private readonly string _reason;

    public InstructionRejectedEvent(string instructionId, long accountId, long instrumentId, string reason)
    {
      this._instructionId = instructionId;
      this._reason = reason;
      this._instrumentId = instrumentId;
      this._accountId = accountId;
    }

    public string InstructionId
    {
      get
      {
        return this._instructionId;
      }
    }

    public long AccountId
    {
      get
      {
        return this._accountId;
      }
    }

    public long InstrumentId
    {
      get
      {
        return this._instrumentId;
      }
    }

    public string Reason
    {
      get
      {
        return this._reason;
      }
    }

    public override string ToString()
    {
      return string.Format("InstructionRejectedEvent{{InstructionId: {0}, AccountId: {1}, InstrumentId: {2}, Reason: {3}}}", (object) this._instructionId, (object) this._accountId, (object) this._instrumentId, (object) this._reason);
    }

    public bool Equals(InstructionRejectedEvent other)
    {
      if (object.ReferenceEquals((object) null, (object) other))
        return false;
      if (object.ReferenceEquals((object) this, (object) other))
        return true;
      return other._instructionId == this._instructionId && other._accountId == this._accountId && other._instrumentId == this._instrumentId && object.Equals((object) other._reason, (object) this._reason);
    }

    public override bool Equals(object obj)
    {
      if (object.ReferenceEquals((object) null, obj))
        return false;
      if (object.ReferenceEquals((object) this, obj))
        return true;
      if (obj.GetType() != typeof (InstructionRejectedEvent))
        return false;
      return this.Equals((InstructionRejectedEvent) obj);
    }

    public override int GetHashCode()
    {
      return ((this._instructionId.GetHashCode() * 397 ^ this._accountId.GetHashCode()) * 397 ^ this._instrumentId.GetHashCode()) * 397 ^ (this._reason != null ? this._reason.GetHashCode() : 0);
    }

    public static bool operator ==(InstructionRejectedEvent left, InstructionRejectedEvent right)
    {
      return object.Equals((object) left, (object) right);
    }

    public static bool operator !=(InstructionRejectedEvent left, InstructionRejectedEvent right)
    {
      return !object.Equals((object) left, (object) right);
    }
  }
}
