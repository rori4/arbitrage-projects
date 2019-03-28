// Decompiled with JetBrains decompiler
// Type: Com.Lmax.Api.Internal.Protocol.InstructionRejectedEventHandler
// Assembly: LmaxClientLibrary, Version=1.8.2.0, Culture=neutral, PublicKeyToken=f45712b013135aca
// MVID: 5CF42455-8EBA-41F5-A8DC-3DC7279B187B
// Assembly location: C:\Users\range\Desktop\Arbitrage\new hope\ipcbus\LmaxClientLibrary.dll

using Com.Lmax.Api.Reject;

namespace Com.Lmax.Api.Internal.Protocol
{
  public class InstructionRejectedEventHandler : DefaultHandler
  {
    private const string RootNodeName = "instructionRejected";
    private const string ReasonNodeName = "reason";
    private const string InstrumentIdNodeName = "instrumentId";
    private const string AccountIdNodeName = "accountId";
    private const string InstructionIdNodeName = "instructionId";

    public event OnRejectionEvent RejectionEventListener;

    public InstructionRejectedEventHandler()
      : base("instructionRejected")
    {
      this.AddHandler("reason");
      this.AddHandler("instrumentId");
      this.AddHandler("accountId");
      this.AddHandler("instructionId");
    }

    public override void EndElement(string endElement)
    {
      if (!"instructionRejected".Equals(endElement) || this.RejectionEventListener == null)
        return;
      string stringValue;
      this.TryGetValue("instructionId", out stringValue);
      long longValue1;
      this.TryGetValue("instrumentId", out longValue1);
      long longValue2;
      this.TryGetValue("accountId", out longValue2);
      this.RejectionEventListener(new InstructionRejectedEvent(stringValue, longValue2, longValue1, this.GetStringValue("reason")));
    }
  }
}
