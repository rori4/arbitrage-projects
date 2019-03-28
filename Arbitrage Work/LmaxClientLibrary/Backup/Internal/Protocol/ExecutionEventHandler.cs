// Decompiled with JetBrains decompiler
// Type: Com.Lmax.Api.Internal.Protocol.ExecutionEventHandler
// Assembly: LmaxClientLibrary, Version=1.8.2.0, Culture=neutral, PublicKeyToken=f45712b013135aca
// MVID: 5CF42455-8EBA-41F5-A8DC-3DC7279B187B
// Assembly location: C:\Users\range\Desktop\Arbitrage\new hope\ipcbus\LmaxClientLibrary.dll

using Com.Lmax.Api.Internal.Events;
using System;
using System.Collections.Generic;

namespace Com.Lmax.Api.Internal.Protocol
{
  public class ExecutionEventHandler : DefaultHandler
  {
    private ExecutionBuilder _executionBuilder = new ExecutionBuilder();
    private const string RootNodeName = "executions";
    private const string ExecutionNodeName = "execution";
    private const string ExecutionIdNodeName = "executionId";
    private const string PriceNodeName = "price";
    private const string QuantityNodeName = "quantity";
    private const string OrderCancelledNodeName = "orderCancelled";
    private readonly IList<ExecutionBuilder> _executionBuilders;

    public ExecutionEventHandler()
      : base("executions")
    {
      this.AddHandler("executionId");
      this.AddHandler("price");
      this.AddHandler("quantity");
      this._executionBuilders = (IList<ExecutionBuilder>) new List<ExecutionBuilder>();
    }

    public override void EndElement(string endElement)
    {
      if (!"execution".Equals(endElement) && !"orderCancelled".Equals(endElement))
        return;
      Decimal dec1;
      this.TryGetValue("quantity", out dec1);
      if ("execution".Equals(endElement))
      {
        Decimal dec2;
        this.TryGetValue("price", out dec2);
        this._executionBuilder.Price(dec2);
        this._executionBuilder.Quantity(dec1);
      }
      else if ("orderCancelled".Equals(endElement))
        this._executionBuilder.CancelledQuantity(dec1);
      long longValue;
      this.TryGetValue("executionId", out longValue);
      this._executionBuilder.ExecutionId(longValue);
      this._executionBuilders.Add(this._executionBuilder);
      this._executionBuilder = new ExecutionBuilder();
    }

    public IList<ExecutionBuilder> GetExecutionBuilders()
    {
      return this._executionBuilders;
    }

    public void Clear()
    {
      this._executionBuilders.Clear();
    }
  }
}
