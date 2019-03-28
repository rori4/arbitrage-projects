// Decompiled with JetBrains decompiler
// Type: Com.Lmax.Api.Internal.Connection
// Assembly: LmaxClientLibrary, Version=1.8.2.0, Culture=neutral, PublicKeyToken=f45712b013135aca
// MVID: 5CF42455-8EBA-41F5-A8DC-3DC7279B187B
// Assembly location: C:\Users\range\Desktop\Arbitrage\new hope\ipcbus\LmaxClientLibrary.dll

using System.IO;
using System.Net;
using System.Text;

namespace Com.Lmax.Api.Internal
{
  public class Connection : IConnection
  {
    private readonly WebRequest _webRequest;
    private readonly WebResponse _webResponse;

    public Connection(WebRequest webRequest, WebResponse webResponse)
    {
      this._webRequest = webRequest;
      this._webResponse = webResponse;
    }

    public TextReader GetTextReader()
    {
      return (TextReader) new StreamReader(this._webResponse.GetResponseStream(), (Encoding) new UTF8Encoding());
    }

    public BinaryReader GetBinaryReader()
    {
      return new BinaryReader(this._webResponse.GetResponseStream());
    }

    public void Abort()
    {
      this._webRequest.Abort();
    }

    public void Close()
    {
      this._webResponse.Close();
    }
  }
}
