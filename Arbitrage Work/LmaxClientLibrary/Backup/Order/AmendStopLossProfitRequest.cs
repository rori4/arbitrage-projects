// Decompiled with JetBrains decompiler
// Type: Com.Lmax.Api.Order.AmendStopLossProfitRequest
// Assembly: LmaxClientLibrary, Version=1.8.2.0, Culture=neutral, PublicKeyToken=f45712b013135aca
// MVID: 5CF42455-8EBA-41F5-A8DC-3DC7279B187B
// Assembly location: C:\Users\range\Desktop\Arbitrage\new hope\ipcbus\LmaxClientLibrary.dll

using Com.Lmax.Api.Internal.Xml;
using System;

namespace Com.Lmax.Api.Order
{
  public sealed class AmendStopLossProfitRequest : IRequest
  {
    private readonly long _instrumentId;
    private readonly string _instructionId;
    private readonly string _originalInstructionId;
    private readonly Decimal? _stopLossOffset;
    private readonly Decimal? _stopProfitOffset;

    public AmendStopLossProfitRequest(long instrumentId, string instructionId, string originalInstructionId, Decimal? stopLossOffset, Decimal? stopProfitOffset)
    {
      this._instrumentId = instrumentId;
      this._instructionId = instructionId;
      this._originalInstructionId = originalInstructionId;
      this._stopLossOffset = stopLossOffset;
      this._stopProfitOffset = stopProfitOffset;
    }

    public string Uri
    {
      get
      {
        return "/secure/trade/amendOrder";
      }
    }

    public void WriteTo(IStructuredWriter writer)
    {
      writer.StartElement("req").StartElement("body").ValueOrEmpty("instrumentId", new long?(this._instrumentId)).ValueOrEmpty("originalInstructionId", this._originalInstructionId).ValueOrEmpty("instructionId", this._instructionId).ValueOrEmpty("stopLossOffset", this._stopLossOffset).ValueOrEmpty("stopProfitOffset", this._stopProfitOffset).EndElement("body").EndElement("req");
    }
  }
}
