// Decompiled with JetBrains decompiler
// Type: Com.Lmax.Api.Internal.Protocol.HeartbeatEventHandler
// Assembly: LmaxClientLibrary, Version=1.8.2.0, Culture=neutral, PublicKeyToken=f45712b013135aca
// MVID: 5CF42455-8EBA-41F5-A8DC-3DC7279B187B
// Assembly location: C:\Users\range\Desktop\Arbitrage\new hope\ipcbus\LmaxClientLibrary.dll

namespace Com.Lmax.Api.Internal.Protocol
{
  public class HeartbeatEventHandler : DefaultHandler
  {
    private const string RootNodeName = "heartbeat";
    private const string Token = "token";

    public event OnHeartbeatReceivedEvent HeartbeatReceived;

    public HeartbeatEventHandler()
      : base("heartbeat")
    {
      this.AddHandler("token");
    }

    public override void EndElement(string endElement)
    {
      if (this.HeartbeatReceived == null || !endElement.Equals("heartbeat"))
        return;
      this.HeartbeatReceived(this.GetStringValue("token"));
    }
  }
}
