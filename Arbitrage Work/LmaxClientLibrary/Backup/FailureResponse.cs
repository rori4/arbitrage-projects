// Decompiled with JetBrains decompiler
// Type: Com.Lmax.Api.FailureResponse
// Assembly: LmaxClientLibrary, Version=1.8.2.0, Culture=neutral, PublicKeyToken=f45712b013135aca
// MVID: 5CF42455-8EBA-41F5-A8DC-3DC7279B187B
// Assembly location: C:\Users\range\Desktop\Arbitrage\new hope\ipcbus\LmaxClientLibrary.dll

using System;

namespace Com.Lmax.Api
{
  public class FailureResponse
  {
    private readonly bool _isSystemFailure;
    private readonly string _message;
    private readonly string _description;
    private readonly Exception _exception;

    public FailureResponse(bool isSystemFailure, string message, string description, Exception exception)
    {
      this._isSystemFailure = isSystemFailure;
      this._message = message;
      this._description = description;
      this._exception = exception;
    }

    public FailureResponse(bool isSystemFailure, string message)
      : this(isSystemFailure, message, "", (Exception) null)
    {
    }

    public FailureResponse(Exception exception, string description)
      : this(true, exception.Message, description, exception)
    {
    }

    public bool IsSystemFailure
    {
      get
      {
        return this._isSystemFailure;
      }
    }

    public string Message
    {
      get
      {
        return this._message;
      }
    }

    public string Description
    {
      get
      {
        return this._description;
      }
    }

    public Exception Exception
    {
      get
      {
        return this._exception;
      }
    }

    public override string ToString()
    {
      return string.Format("IsSystemFailure: {0}, Message: {1}, Description: {2}, Exception: {3}", (object) this._isSystemFailure, (object) this._message, (object) this._description, (object) this._exception);
    }
  }
}
