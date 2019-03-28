// Decompiled with JetBrains decompiler
// Type: Com.Lmax.Api.Internal.Xml.XmlStructuredWriter
// Assembly: LmaxClientLibrary, Version=1.8.2.0, Culture=neutral, PublicKeyToken=f45712b013135aca
// MVID: 5CF42455-8EBA-41F5-A8DC-3DC7279B187B
// Assembly location: C:\Users\range\Desktop\Arbitrage\new hope\ipcbus\LmaxClientLibrary.dll

using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace Com.Lmax.Api.Internal.Xml
{
  public class XmlStructuredWriter : IStructuredWriter
  {
    private readonly byte[] _defaultData = new byte[4096];
    private const int DefaultSize = 4096;
    private const string Left = "<";
    private const string Right = ">";
    private const string LeftClose = "</";
    private const string RightClose = "/>";
    private byte[] _currentData;
    private int _length;

    public XmlStructuredWriter()
    {
      this._currentData = this._defaultData;
    }

    public IStructuredWriter StartElement(string name)
    {
      this.WriteOpenTag(name);
      return (IStructuredWriter) this;
    }

    public IStructuredWriter ValueOrEmpty(string name, string value)
    {
      if (null != value)
        this.WriteTag(name, value);
      else
        this.WriteEmptyTag(name);
      return (IStructuredWriter) this;
    }

    public IStructuredWriter ValueOrNone(string name, string value)
    {
      if (value != null)
        this.WriteTag(name, value);
      return (IStructuredWriter) this;
    }

    public IStructuredWriter ValueUTF8(string name, string value)
    {
      if (null != value)
      {
        this.WriteOpenTag(name);
        this.WriteBytes(Encoding.UTF8.GetBytes(XmlStructuredWriter.Escape(value)));
        this.WriteCloseTag(name);
      }
      else
        this.WriteEmptyTag(name);
      return (IStructuredWriter) this;
    }

    public IStructuredWriter ValueOrEmpty(string name, long? value)
    {
      if (value.HasValue)
        this.WriteTag(name, Convert.ToString((object) value, (IFormatProvider) NumberFormatInfo.InvariantInfo));
      else
        this.WriteEmptyTag(name);
      return (IStructuredWriter) this;
    }

    public IStructuredWriter ValueOrNone(string name, long? value)
    {
      if (value.HasValue)
        this.WriteTag(name, Convert.ToString((object) value, (IFormatProvider) NumberFormatInfo.InvariantInfo));
      return (IStructuredWriter) this;
    }

    public IStructuredWriter ValueOrEmpty(string name, Decimal? value)
    {
      if (value.HasValue)
        this.WriteTag(name, Convert.ToString((object) value, (IFormatProvider) NumberFormatInfo.InvariantInfo));
      else
        this.WriteEmptyTag(name);
      return (IStructuredWriter) this;
    }

    public IStructuredWriter ValueOrNone(string name, Decimal? value)
    {
      if (value.HasValue)
        this.WriteTag(name, Convert.ToString((object) value, (IFormatProvider) NumberFormatInfo.InvariantInfo));
      return (IStructuredWriter) this;
    }

    public IStructuredWriter ValueOrEmpty(string name, bool value)
    {
      this.WriteTag(name, value ? "true" : "false");
      return (IStructuredWriter) this;
    }

    public IStructuredWriter WriteEmptyTag(string name)
    {
      this.WriteString("<");
      this.WriteString(name);
      this.WriteString("/>");
      return (IStructuredWriter) this;
    }

    public IStructuredWriter EndElement(string name)
    {
      this.WriteCloseTag(name);
      return (IStructuredWriter) this;
    }

    public void Reset()
    {
      this._length = 0;
      this._currentData = this._defaultData;
    }

    private void WriteString(string value)
    {
      int num = value.Length + this._length;
      while (num > this._currentData.Length)
        this.IncreaseBufferSize();
      for (int index = 0; index < value.Length; ++index)
        this._currentData[this._length++] = (byte) value[index];
    }

    private void WriteBytes(byte[] value)
    {
      int length = value.Length;
      while (length + this._length > this._currentData.Length)
        this.IncreaseBufferSize();
      Array.Copy((Array) value, 0, (Array) this._currentData, this._length, length);
      this._length += length;
    }

    private void IncreaseBufferSize()
    {
      byte[] currentData = this._currentData;
      this._currentData = new byte[currentData.Length << 1];
      Array.Copy((Array) currentData, (Array) this._currentData, this._length);
    }

    private void WriteTag(string name, string value)
    {
      this.WriteOpenTag(name);
      this.WriteString(value);
      this.WriteCloseTag(name);
    }

    private void WriteCloseTag(string name)
    {
      this.WriteString("</");
      this.WriteString(name);
      this.WriteString(">");
    }

    private void WriteOpenTag(string name)
    {
      this.WriteString("<");
      this.WriteString(name);
      this.WriteString(">");
    }

    private static string Escape(string value)
    {
      return value.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;").Replace("'", "&apos;");
    }

    public void WriteTo(Stream output)
    {
      output.Write(this._currentData, 0, this._length);
    }

    public new string ToString()
    {
      MemoryStream memoryStream = new MemoryStream();
      this.WriteTo((Stream) memoryStream);
      return Encoding.UTF8.GetString(memoryStream.GetBuffer(), 0, (int) memoryStream.Length);
    }
  }
}
