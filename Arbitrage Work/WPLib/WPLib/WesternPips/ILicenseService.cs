// Decompiled with JetBrains decompiler
// Type: WPLib.WesternPips.ILicenseService
// Assembly: WPLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A67F71FE-CC9D-4C7E-B402-72B871993086
// Assembly location: C:\Program Files (x86)\Westernpips\Westernpips Trade Monitor 3.7 Exclusive\WPLib.dll

using System.CodeDom.Compiler;
using System.ServiceModel;
using System.Threading.Tasks;

namespace WPLib.WesternPips
{
  [GeneratedCode("System.ServiceModel", "4.0.0.0")]
  [ServiceContract(ConfigurationName = "WesternPips.ILicenseService")]
  public interface ILicenseService
  {
    [OperationContract(Action = "http://tempuri.org/ILicenseService/checkUser", ReplyAction = "http://tempuri.org/ILicenseService/checkUserResponse")]
    int checkUser(Trader _traderData);

    [OperationContract(Action = "http://tempuri.org/ILicenseService/checkUser", ReplyAction = "http://tempuri.org/ILicenseService/checkUserResponse")]
    Task<int> checkUserAsync(Trader _traderData);

    [OperationContract(Action = "http://tempuri.org/ILicenseService/canUseFeed", ReplyAction = "http://tempuri.org/ILicenseService/canUseFeedResponse")]
    int canUseFeed(Trader _traderData);

    [OperationContract(Action = "http://tempuri.org/ILicenseService/canUseFeed", ReplyAction = "http://tempuri.org/ILicenseService/canUseFeedResponse")]
    Task<int> canUseFeedAsync(Trader _traderData);

    [OperationContract(Action = "http://tempuri.org/ILicenseService/sendRequest", ReplyAction = "http://tempuri.org/ILicenseService/sendRequestResponse")]
    int sendRequest(Trader _traderData);

    [OperationContract(Action = "http://tempuri.org/ILicenseService/sendRequest", ReplyAction = "http://tempuri.org/ILicenseService/sendRequestResponse")]
    Task<int> sendRequestAsync(Trader _traderData);

    [OperationContract(Action = "http://tempuri.org/ILicenseService/sendRequestSKU", ReplyAction = "http://tempuri.org/ILicenseService/sendRequestSKUResponse")]
    int sendRequestSKU(Trader _traderData, string SKU);

    [OperationContract(Action = "http://tempuri.org/ILicenseService/sendRequestSKU", ReplyAction = "http://tempuri.org/ILicenseService/sendRequestSKUResponse")]
    Task<int> sendRequestSKUAsync(Trader _traderData, string SKU);

    [OperationContract(Action = "http://tempuri.org/ILicenseService/getProviders", ReplyAction = "http://tempuri.org/ILicenseService/getProvidersResponse")]
    ProviderContract[] getProviders(Trader _traderData);

    [OperationContract(Action = "http://tempuri.org/ILicenseService/getProviders", ReplyAction = "http://tempuri.org/ILicenseService/getProvidersResponse")]
    Task<ProviderContract[]> getProvidersAsync(Trader _traderData);

    [OperationContract(Action = "http://tempuri.org/ILicenseService/getInstuments", ReplyAction = "http://tempuri.org/ILicenseService/getInstumentsResponse")]
    InstrumentsContract[] getInstuments(Trader _traderData);

    [OperationContract(Action = "http://tempuri.org/ILicenseService/getInstuments", ReplyAction = "http://tempuri.org/ILicenseService/getInstumentsResponse")]
    Task<InstrumentsContract[]> getInstumentsAsync(Trader _traderData);

    [OperationContract(Action = "http://tempuri.org/ILicenseService/setInstumentState", ReplyAction = "http://tempuri.org/ILicenseService/setInstumentStateResponse")]
    void setInstumentState(long _id, bool _state);

    [OperationContract(Action = "http://tempuri.org/ILicenseService/setInstumentState", ReplyAction = "http://tempuri.org/ILicenseService/setInstumentStateResponse")]
    Task setInstumentStateAsync(long _id, bool _state);

    [OperationContract(Action = "http://tempuri.org/ILicenseService/addInstrument", ReplyAction = "http://tempuri.org/ILicenseService/addInstrumentResponse")]
    int addInstrument(InstrumentsContract newInstrument);

    [OperationContract(Action = "http://tempuri.org/ILicenseService/addInstrument", ReplyAction = "http://tempuri.org/ILicenseService/addInstrumentResponse")]
    Task<int> addInstrumentAsync(InstrumentsContract newInstrument);

    [OperationContract(Action = "http://tempuri.org/ILicenseService/getLmaxFeedIP", ReplyAction = "http://tempuri.org/ILicenseService/getLmaxFeedIPResponse")]
    string getLmaxFeedIP(Trader _traderData);

    [OperationContract(Action = "http://tempuri.org/ILicenseService/getLmaxFeedIP", ReplyAction = "http://tempuri.org/ILicenseService/getLmaxFeedIPResponse")]
    Task<string> getLmaxFeedIPAsync(Trader _traderData);

    [OperationContract(Action = "http://tempuri.org/ILicenseService/getRithmicFeedIP", ReplyAction = "http://tempuri.org/ILicenseService/getRithmicFeedIPResponse")]
    string getRithmicFeedIP(Trader _traderData, bool usas);

    [OperationContract(Action = "http://tempuri.org/ILicenseService/getRithmicFeedIP", ReplyAction = "http://tempuri.org/ILicenseService/getRithmicFeedIPResponse")]
    Task<string> getRithmicFeedIPAsync(Trader _traderData, bool usas);

    [OperationContract(Action = "http://tempuri.org/ILicenseService/getLogServerAddr", ReplyAction = "http://tempuri.org/ILicenseService/getLogServerAddrResponse")]
    string getLogServerAddr();

    [OperationContract(Action = "http://tempuri.org/ILicenseService/getLogServerAddr", ReplyAction = "http://tempuri.org/ILicenseService/getLogServerAddrResponse")]
    Task<string> getLogServerAddrAsync();

    [OperationContract(Action = "http://tempuri.org/ILicenseService/getLmaxSettings", ReplyAction = "http://tempuri.org/ILicenseService/getLmaxSettingsResponse")]
    byte[] getLmaxSettings(Trader _traderData);

    [OperationContract(Action = "http://tempuri.org/ILicenseService/getLmaxSettings", ReplyAction = "http://tempuri.org/ILicenseService/getLmaxSettingsResponse")]
    Task<byte[]> getLmaxSettingsAsync(Trader _traderData);

    [OperationContract(Action = "http://tempuri.org/ILicenseService/getRithmicSettings", ReplyAction = "http://tempuri.org/ILicenseService/getRithmicSettingsResponse")]
    byte[] getRithmicSettings(Trader _traderData);

    [OperationContract(Action = "http://tempuri.org/ILicenseService/getRithmicSettings", ReplyAction = "http://tempuri.org/ILicenseService/getRithmicSettingsResponse")]
    Task<byte[]> getRithmicSettingsAsync(Trader _traderData);

    [OperationContract(Action = "http://tempuri.org/ILicenseService/sendLogin", ReplyAction = "http://tempuri.org/ILicenseService/sendLoginResponse")]
    void sendLogin(Trader _traderData);

    [OperationContract(Action = "http://tempuri.org/ILicenseService/sendLogin", ReplyAction = "http://tempuri.org/ILicenseService/sendLoginResponse")]
    Task sendLoginAsync(Trader _traderData);

    [OperationContract(Action = "http://tempuri.org/ILicenseService/checkLicense", ReplyAction = "http://tempuri.org/ILicenseService/checkLicenseResponse")]
    int checkLicense(Trader _traderData, string SKU);

    [OperationContract(Action = "http://tempuri.org/ILicenseService/checkLicense", ReplyAction = "http://tempuri.org/ILicenseService/checkLicenseResponse")]
    Task<int> checkLicenseAsync(Trader _traderData, string SKU);

    [OperationContract(Action = "http://tempuri.org/ILicenseService/getInstumentsSKU", ReplyAction = "http://tempuri.org/ILicenseService/getInstumentsSKUResponse")]
    InstrumentsContract[] getInstumentsSKU(Trader _traderData, string SKU, string ProviderName);

    [OperationContract(Action = "http://tempuri.org/ILicenseService/getInstumentsSKU", ReplyAction = "http://tempuri.org/ILicenseService/getInstumentsSKUResponse")]
    Task<InstrumentsContract[]> getInstumentsSKUAsync(Trader _traderData, string SKU, string ProviderName);

    [OperationContract(Action = "http://tempuri.org/ILicenseService/canUseAnalysis", ReplyAction = "http://tempuri.org/ILicenseService/canUseAnalysisResponse")]
    bool canUseAnalysis(byte[] _secret);

    [OperationContract(Action = "http://tempuri.org/ILicenseService/canUseAnalysis", ReplyAction = "http://tempuri.org/ILicenseService/canUseAnalysisResponse")]
    Task<bool> canUseAnalysisAsync(byte[] _secret);
  }
}
