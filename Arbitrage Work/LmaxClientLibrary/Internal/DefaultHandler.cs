// Decompiled with JetBrains decompiler
// Type: Com.Lmax.Api.Internal.DefaultHandler
// Assembly: LmaxClientLibrary, Version=1.8.2.0, Culture=neutral, PublicKeyToken=f45712b013135aca
// MVID: 5CF42455-8EBA-41F5-A8DC-3DC7279B187B
// Assembly location: C:\Users\range\Desktop\Arbitrage\new hope\ipcbus\LmaxClientLibrary.dll

using System;
using System.Collections.Generic;
using System.Globalization;

namespace Com.Lmax.Api.Internal
{
  public class DefaultHandler : Handler
  {
    public static readonly NumberFormatInfo NumberFormat = CultureInfo.InvariantCulture.NumberFormat;
    private readonly IDictionary<string, Handler> _handlers = (IDictionary<string, Handler>) new Dictionary<string, Handler>();

    public DefaultHandler()
      : this("res")
    {
    }

    public DefaultHandler(string elementName)
      : base(elementName)
    {
      this.AddHandler("status");
      this.AddHandler("message");
    }

    public override bool IsOk
    {
      get
      {
        return "OK" == this.GetStringValue("status");
      }
    }

    public string Status
    {
      get
      {
        return this.GetStringValue("status");
      }
    }

    public override string Message
    {
      get
      {
        return this.GetStringValue("message");
      }
    }

    public void AddHandler(string tag)
    {
      this.AddHandler(new Handler(tag));
    }

    public void AddHandler(Handler handler)
    {
      this._handlers[handler.ElementName] = handler;
    }

    public override void Reset(string element)
    {
      Handler handler;
      if (!this._handlers.TryGetValue(element, out handler))
        return;
      handler.Reset(element);
    }

    public void ResetAll()
    {
      foreach (KeyValuePair<string, Handler> handler in (IEnumerable<KeyValuePair<string, Handler>>) this._handlers)
        handler.Value.Reset(handler.Key);
    }

    protected string GetStringValue(string tag)
    {
      Handler handler;
      if (this._handlers.TryGetValue(tag, out handler))
        return handler.Content;
      return (string) null;
    }

    public bool TryGetValue(string tag, out Decimal dec)
    {
      Handler handler;
      if (this._handlers.TryGetValue(tag, out handler))
      {
        string content = handler.Content;
        if (content.Length > 0)
        {
          dec = Convert.ToDecimal(content, (IFormatProvider) DefaultHandler.NumberFormat);
          return true;
        }
      }
      dec = new Decimal(0);
      return false;
    }

    public bool TryGetValue(string tag, out long longValue)
    {
      Handler handler;
      if (this._handlers.TryGetValue(tag, out handler))
      {
        string content = handler.Content;
        if (content.Length > 0)
        {
          longValue = Convert.ToInt64(content);
          return true;
        }
      }
      longValue = 0L;
      return false;
    }

    public bool TryGetValue(string tag, out bool boolValue)
    {
      Handler handler;
      if (this._handlers.TryGetValue(tag, out handler))
      {
        string content = handler.Content;
        if (content.Length > 0)
        {
          boolValue = Convert.ToBoolean(content);
          return true;
        }
      }
      boolValue = false;
      return false;
    }

    public bool TryGetValue(string tag, out string stringValue)
    {
      Handler handler;
      if (this._handlers.TryGetValue(tag, out handler))
      {
        string content = handler.Content;
        if (content.Length > 0)
        {
          stringValue = content;
          return true;
        }
      }
      stringValue = (string) null;
      return false;
    }

    public int GetIntValue(string tag, int defaultValue)
    {
      long longValue;
      return this.TryGetValue(tag, out longValue) ? (int) longValue : defaultValue;
    }

    public long GetLongValue(string tag, long defaultValue)
    {
      long longValue;
      return this.TryGetValue(tag, out longValue) ? longValue : defaultValue;
    }

    public Decimal GetDecimalValue(string tag, Decimal defaultValue)
    {
      Decimal dec;
      return this.TryGetValue(tag, out dec) ? dec : defaultValue;
    }

    public Decimal? GetDecimalValue(string tag)
    {
      Decimal dec;
      if (this.TryGetValue(tag, out dec))
        return new Decimal?(dec);
      return new Decimal?();
    }

    public override Handler GetHandler(string qName)
    {
      Handler handler;
      if (this._handlers.TryGetValue(qName, out handler))
        return handler;
      return (Handler) this;
    }
  }
}
