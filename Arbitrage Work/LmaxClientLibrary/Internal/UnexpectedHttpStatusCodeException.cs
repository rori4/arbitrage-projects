// Decompiled with JetBrains decompiler
// Type: Com.Lmax.Api.Internal.UnexpectedHttpStatusCodeException
// Assembly: LmaxClientLibrary, Version=1.8.2.0, Culture=neutral, PublicKeyToken=f45712b013135aca
// MVID: 5CF42455-8EBA-41F5-A8DC-3DC7279B187B
// Assembly location: C:\Users\range\Desktop\Arbitrage\new hope\ipcbus\LmaxClientLibrary.dll

using System;
using System.Net;

namespace Com.Lmax.Api.Internal
{
  public class UnexpectedHttpStatusCodeException : Exception
  {
    private readonly HttpStatusCode _statusCode;

    public UnexpectedHttpStatusCodeException(HttpStatusCode statusCode)
    {
      this._statusCode = statusCode;
    }

    public HttpStatusCode StatusCode
    {
      get
      {
        return this._statusCode;
      }
    }
  }
}
