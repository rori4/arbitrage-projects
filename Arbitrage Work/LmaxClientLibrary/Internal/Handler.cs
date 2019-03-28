// Decompiled with JetBrains decompiler
// Type: Com.Lmax.Api.Internal.Handler
// Assembly: LmaxClientLibrary, Version=1.8.2.0, Culture=neutral, PublicKeyToken=f45712b013135aca
// MVID: 5CF42455-8EBA-41F5-A8DC-3DC7279B187B
// Assembly location: C:\Users\range\Desktop\Arbitrage\new hope\ipcbus\LmaxClientLibrary.dll

using System.Text;

namespace Com.Lmax.Api.Internal
{
  public class Handler
  {
    private StringBuilder _contentBuilder = new StringBuilder();
    protected const string OK = "OK";
    protected const string BODY = "body";
    protected const string MESSAGE = "message";
    protected const string STATUS = "status";
    private readonly string _elementName;

    public Handler()
      : this((string) null)
    {
    }

    public Handler(string elementName)
    {
      this._elementName = elementName;
    }

    public virtual bool IsOk
    {
      get
      {
        return false;
      }
    }

    public virtual string Message
    {
      get
      {
        return "";
      }
    }

    public virtual string ElementName
    {
      get
      {
        return this._elementName;
      }
    }

    public virtual string Content
    {
      get
      {
        return this._contentBuilder.ToString();
      }
    }

    public virtual Handler GetHandler(string qName)
    {
      return this;
    }

    public virtual void Characters(string characterData, int start, int length)
    {
      this._contentBuilder.Append(characterData, start, length);
    }

    public virtual void Reset(string element)
    {
      this._contentBuilder = new StringBuilder();
    }

    public virtual void EndElement(string endElement)
    {
    }
  }
}
