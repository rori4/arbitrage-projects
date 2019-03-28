// Decompiled with JetBrains decompiler
// Type: Com.Lmax.Api.LoginRequest
// Assembly: LmaxClientLibrary, Version=1.8.2.0, Culture=neutral, PublicKeyToken=f45712b013135aca
// MVID: 5CF42455-8EBA-41F5-A8DC-3DC7279B187B
// Assembly location: C:\Users\range\Desktop\Arbitrage\new hope\ipcbus\LmaxClientLibrary.dll

using Com.Lmax.Api.Internal.Xml;

namespace Com.Lmax.Api
{
  public class LoginRequest : IRequest
  {
    private const string ProtocolVersion = "1.8";
    private const string LoginUri = "/public/security/login";
    private readonly string _username;
    private readonly string _password;
    private readonly ProductType _productType;
    private readonly bool _checkProtocolVersion;

    public LoginRequest(string username, string password, ProductType productType, bool checkProtocolVersion)
    {
      this._username = username;
      this._password = password;
      this._productType = productType;
      this._checkProtocolVersion = checkProtocolVersion;
    }

    public LoginRequest(string username, string password, ProductType productType)
      : this(username, password, productType, true)
    {
    }

    public LoginRequest(string username, string password)
      : this(username, password, ProductType.CFD_LIVE, true)
    {
    }

    public string Uri
    {
      get
      {
        return "/public/security/login";
      }
    }

    public void WriteTo(IStructuredWriter writer)
    {
      writer.StartElement("req").StartElement("body").ValueUTF8("username", this._username).ValueUTF8("password", this._password);
      if (this._checkProtocolVersion)
        writer.ValueOrNone("protocolVersion", "1.8");
      writer.ValueOrNone("productType", this._productType.ToString()).EndElement("body").EndElement("req");
    }
  }
}
