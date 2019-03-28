// Decompiled with JetBrains decompiler
// Type: Com.Lmax.Api.Order.CancelOrderRequest
// Assembly: LmaxClientLibrary, Version=1.8.2.0, Culture=neutral, PublicKeyToken=f45712b013135aca
// MVID: 5CF42455-8EBA-41F5-A8DC-3DC7279B187B
// Assembly location: C:\Users\range\Desktop\Arbitrage\new hope\ipcbus\LmaxClientLibrary.dll

using Com.Lmax.Api.Internal.Xml;

namespace Com.Lmax.Api.Order
{
  public class CancelOrderRequest : IRequest
  {
    private readonly string _instructionId;
    private readonly long _instrumentId;
    private readonly string _originalInstructionId;

    public CancelOrderRequest(string instructionId, long instrumentId, string originalInstructionId)
    {
      this._instructionId = instructionId;
      this._instrumentId = instrumentId;
      this._originalInstructionId = originalInstructionId;
    }

    public string Uri
    {
      get
      {
        return "/secure/trade/cancel";
      }
    }

    public void WriteTo(IStructuredWriter writer)
    {
      writer.StartElement("req").StartElement("body").ValueOrEmpty("instructionId", this._instructionId).ValueOrEmpty("instrumentId", new long?(this._instrumentId)).ValueOrEmpty("originalInstructionId", this._originalInstructionId).EndElement("body").EndElement("req");
    }
  }
}
