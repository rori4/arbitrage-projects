// Decompiled with JetBrains decompiler
// Type: Com.Lmax.Api.Internal.Protocol.OrderStateEventHandler
// Assembly: LmaxClientLibrary, Version=1.8.2.0, Culture=neutral, PublicKeyToken=f45712b013135aca
// MVID: 5CF42455-8EBA-41F5-A8DC-3DC7279B187B
// Assembly location: C:\Users\range\Desktop\Arbitrage\new hope\ipcbus\LmaxClientLibrary.dll

using Com.Lmax.Api.Internal.Events;
using Com.Lmax.Api.Order;
using System;
using System.Collections.Generic;

namespace Com.Lmax.Api.Internal.Protocol
{
  public class OrderStateEventHandler : DefaultHandler
  {
    private const string InstructionIdNodeName = "instructionId";
    private const string OrderIdNodeName = "orderId";
    private const string InstrumentIdNodeName = "instrumentId";
    private const string AccountIdNodeName = "accountId";
    private const string QuantityNodeName = "quantity";
    private const string MatchedQuantityNodeName = "matchedQuantity";
    private const string CancelledQuantityNodeName = "cancelledQuantity";
    private const string OrderTypeNodeName = "orderType";
    private const string StopProfitOffsetNodeName = "stopProfitOffset";
    private const string StopLossOffsetNodeName = "stopLossOffset";
    private const string StopReferencePriceNodeName = "stopReferencePrice";
    private const string PriceNodeName = "price";
    private const string RootNode = "order";
    private const string CommissionNodeName = "commission";
    private const string TimeInForceNodeName = "timeInForce";
    private readonly ExecutionEventHandler _executionEventHandler;

    public OrderStateEventHandler()
      : base("order")
    {
      this._executionEventHandler = new ExecutionEventHandler();
      this.AddHandler("instructionId");
      this.AddHandler("orderId");
      this.AddHandler("instrumentId");
      this.AddHandler("accountId");
      this.AddHandler("quantity");
      this.AddHandler("matchedQuantity");
      this.AddHandler("cancelledQuantity");
      this.AddHandler("price");
      this.AddHandler("orderType");
      this.AddHandler("stopProfitOffset");
      this.AddHandler("stopLossOffset");
      this.AddHandler("stopReferencePrice");
      this.AddHandler("commission");
      this.AddHandler((Handler) this._executionEventHandler);
      this.AddHandler("timeInForce");
    }

    public override void EndElement(string endElement)
    {
      if (!"order".Equals(endElement))
        return;
      OrderBuilder orderBuilder = new OrderBuilder();
      string stringValue;
      this.TryGetValue("instructionId", out stringValue);
      long longValue1;
      this.TryGetValue("instrumentId", out longValue1);
      long longValue2;
      this.TryGetValue("accountId", out longValue2);
      Decimal dec1;
      this.TryGetValue("quantity", out dec1);
      Decimal dec2;
      this.TryGetValue("matchedQuantity", out dec2);
      Decimal dec3;
      this.TryGetValue("cancelledQuantity", out dec3);
      Decimal dec4;
      this.TryGetValue("commission", out dec4);
      orderBuilder.InstructionId(stringValue).OrderId(this.GetStringValue("orderId")).InstrumentId(longValue1).AccountId(longValue2).Quantity(dec1).FilledQuantity(dec2).CancelledQuantity(dec3).OrderType(this.GetStringValue("orderType")).Commission(dec4).TimeInForce(this.GetStringValue("timeInForce"));
      Decimal dec5;
      if (this.TryGetValue("price", out dec5))
        orderBuilder.Price(dec5);
      Decimal dec6;
      if (this.TryGetValue("stopReferencePrice", out dec6))
        orderBuilder.StopReferencePrice(dec6);
      Decimal dec7;
      if (this.TryGetValue("stopProfitOffset", out dec7))
        orderBuilder.StopProfitOffset(dec7);
      Decimal dec8;
      if (this.TryGetValue("stopLossOffset", out dec8))
        orderBuilder.StopLossOffset(dec8);
      Com.Lmax.Api.Order.Order order = orderBuilder.NewInstance();
      if (this.ShouldEmitOrder(order) && this.OrderEvent != null)
        this.OrderEvent(order);
      this.NotifyExecutions(order);
      this.ResetAll();
    }

    private void NotifyExecutions(Com.Lmax.Api.Order.Order order)
    {
      foreach (ExecutionBuilder executionBuilder in (IEnumerable<ExecutionBuilder>) this._executionEventHandler.GetExecutionBuilders())
      {
        executionBuilder.Order(order);
        Execution execution = executionBuilder.NewInstance();
        if (OrderStateEventHandler.IsExecutionForOrder(order, execution) && this.ExecutionEvent != null)
          this.ExecutionEvent(execution);
      }
      this._executionEventHandler.Clear();
    }

    private bool ShouldEmitOrder(Com.Lmax.Api.Order.Order order)
    {
      return this._executionEventHandler.GetExecutionBuilders().Count == 0 || OrderStateEventHandler.IsExecutionForOrder(order, this._executionEventHandler.GetExecutionBuilders()[0].NewInstance());
    }

    private static bool IsExecutionForOrder(Com.Lmax.Api.Order.Order order, Execution execution)
    {
      Decimal quantity = order.Quantity;
      int num1 = quantity.CompareTo(new Decimal(0));
      quantity = execution.Quantity;
      int num2 = quantity.CompareTo(new Decimal(0));
      int num3;
      if (num1 != num2)
      {
        Decimal num4 = order.Quantity;
        int num5 = num4.CompareTo(new Decimal(0));
        num4 = execution.CancelledQuantity;
        int num6 = num4.CompareTo(new Decimal(0));
        num3 = num5 == num6 ? 1 : 0;
      }
      else
        num3 = 1;
      return num3 != 0;
    }

    public event OnExecutionEvent ExecutionEvent;

    public event OnOrderEvent OrderEvent;
  }
}
