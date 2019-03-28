// Decompiled with JetBrains decompiler
// Type: Com.Lmax.Api.Internal.IHttpInvoker
// Assembly: LmaxClientLibrary, Version=1.8.2.0, Culture=neutral, PublicKeyToken=f45712b013135aca
// MVID: 5CF42455-8EBA-41F5-A8DC-3DC7279B187B
// Assembly location: C:\Users\range\Desktop\Arbitrage\new hope\ipcbus\LmaxClientLibrary.dll

using System;

namespace Com.Lmax.Api.Internal
{
  public interface IHttpInvoker
  {
    Response Invoke(string baseUri, IRequest request, IXmlParser xmlParser, Handler handler, out string sessionId);

    Response PostInSession(string baseUri, IRequest request, IXmlParser xmlParser, Handler handler, string sessionId);

    Response GetInSession(string baseUri, IRequest request, IXmlParser xmlParser, Handler handler, string sessionId);

    IConnection Connect(string baseUri, IRequest request, string sessionId);

    IConnection Connect(Uri uri, string sessionId);
  }
}
