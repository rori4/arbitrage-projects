﻿// Decompiled with JetBrains decompiler
// Type: Com.Lmax.Api.Internal.Response
// Assembly: LmaxClientLibrary, Version=1.8.2.0, Culture=neutral, PublicKeyToken=f45712b013135aca
// MVID: 5CF42455-8EBA-41F5-A8DC-3DC7279B187B
// Assembly location: C:\Users\range\Desktop\Arbitrage\new hope\ipcbus\LmaxClientLibrary.dll

using System.Net;

namespace Com.Lmax.Api.Internal
{
  public class Response
  {
    private readonly HttpStatusCode _statusCode;

    public Response(HttpStatusCode statusCode)
    {
      this._statusCode = statusCode;
    }

    public bool IsOk
    {
      get
      {
        return this._statusCode == HttpStatusCode.OK;
      }
    }

    public int Status
    {
      get
      {
        return (int) this._statusCode;
      }
    }
  }
}
