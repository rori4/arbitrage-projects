// Decompiled with JetBrains decompiler
// Type: Com.Lmax.Api.Internal.Protocol.OrderResponseHandler
// Assembly: LmaxClientLibrary, Version=1.8.2.0, Culture=neutral, PublicKeyToken=f45712b013135aca
// MVID: 5CF42455-8EBA-41F5-A8DC-3DC7279B187B
// Assembly location: C:\Users\range\Desktop\Arbitrage\new hope\ipcbus\LmaxClientLibrary.dll

namespace Com.Lmax.Api.Internal.Protocol
{
  public class OrderResponseHandler : DefaultHandler
  {
    private const string InstructionIdElementName = "instructionId";
    private string _instructionId;

    public OrderResponseHandler()
      : base("body")
    {
      this.AddHandler("instructionId");
    }

    public string InstructionId
    {
      get
      {
        return this._instructionId;
      }
    }

    public override void EndElement(string endElement)
    {
      this.TryGetValue("instructionId", out this._instructionId);
    }
  }
}
