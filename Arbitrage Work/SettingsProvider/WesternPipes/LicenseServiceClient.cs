// Decompiled with JetBrains decompiler
// Type: SettingsProvider.WesternPipes.LicenseServiceClient
// Assembly: SettingsProvider, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 05C1E7FD-6DD9-4012-9105-50C0A7D91CF0
// Assembly location: F:\Arbitrage Cracks\TradeMonitor\lib\SettingsProvider.dll

using System.CodeDom.Compiler;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;

namespace SettingsProvider.WesternPipes
{
  [DebuggerStepThrough]
  [GeneratedCode("System.ServiceModel", "4.0.0.0")]
  public class LicenseServiceClient : ClientBase<ILicenseService>, ILicenseService
  {
    public LicenseServiceClient()
    {
    }

    public LicenseServiceClient(string endpointConfigurationName)
      : base(endpointConfigurationName)
    {
    }

    public LicenseServiceClient(string endpointConfigurationName, string remoteAddress)
      : base(endpointConfigurationName, remoteAddress)
    {
    }

    public LicenseServiceClient(string endpointConfigurationName, EndpointAddress remoteAddress)
      : base(endpointConfigurationName, remoteAddress)
    {
    }

    public LicenseServiceClient(Binding binding, EndpointAddress remoteAddress)
      : base(binding, remoteAddress)
    {
    }

    public int checkUser(Trader _traderData)
    {
      return this.Channel.checkUser(_traderData);
    }

    public Task<int> checkUserAsync(Trader _traderData)
    {
      return this.Channel.checkUserAsync(_traderData);
    }

    public int canUseFeed(Trader _traderData)
    {
      return this.Channel.canUseFeed(_traderData);
    }

    public Task<int> canUseFeedAsync(Trader _traderData)
    {
      return this.Channel.canUseFeedAsync(_traderData);
    }

    public int sendRequest(Trader _traderData)
    {
      return this.Channel.sendRequest(_traderData);
    }

    public Task<int> sendRequestAsync(Trader _traderData)
    {
      return this.Channel.sendRequestAsync(_traderData);
    }

    public ProviderContract[] getProviders(Trader _traderData)
    {
      return this.Channel.getProviders(_traderData);
    }

    public Task<ProviderContract[]> getProvidersAsync(Trader _traderData)
    {
      return this.Channel.getProvidersAsync(_traderData);
    }

    public InstrumentsContract[] getInstuments(Trader _traderData)
    {
      return this.Channel.getInstuments(_traderData);
    }

    public Task<InstrumentsContract[]> getInstumentsAsync(Trader _traderData)
    {
      return this.Channel.getInstumentsAsync(_traderData);
    }

    public void setInstumentState(long _id, bool _state)
    {
      this.Channel.setInstumentState(_id, _state);
    }

    public Task setInstumentStateAsync(long _id, bool _state)
    {
      return this.Channel.setInstumentStateAsync(_id, _state);
    }

    public int addInstrument(InstrumentsContract newInstrument)
    {
      return this.Channel.addInstrument(newInstrument);
    }

    public Task<int> addInstrumentAsync(InstrumentsContract newInstrument)
    {
      return this.Channel.addInstrumentAsync(newInstrument);
    }

    public string getLmaxFeedIP(Trader _traderData)
    {
      return this.Channel.getLmaxFeedIP(_traderData);
    }

    public Task<string> getLmaxFeedIPAsync(Trader _traderData)
    {
      return this.Channel.getLmaxFeedIPAsync(_traderData);
    }

    public string getRithmicFeedIP(Trader _traderData, bool usas)
    {
      return this.Channel.getRithmicFeedIP(_traderData, usas);
    }

    public Task<string> getRithmicFeedIPAsync(Trader _traderData, bool usas)
    {
      return this.Channel.getRithmicFeedIPAsync(_traderData, usas);
    }

    public string getLogServerAddr()
    {
      return this.Channel.getLogServerAddr();
    }

    public Task<string> getLogServerAddrAsync()
    {
      return this.Channel.getLogServerAddrAsync();
    }

    public byte[] getLmaxSettings(Trader _traderData)
    {
      return this.Channel.getLmaxSettings(_traderData);
    }

    public Task<byte[]> getLmaxSettingsAsync(Trader _traderData)
    {
      return this.Channel.getLmaxSettingsAsync(_traderData);
    }

    public byte[] getRithmicSettings(Trader _traderData)
    {
      return this.Channel.getRithmicSettings(_traderData);
    }

    public Task<byte[]> getRithmicSettingsAsync(Trader _traderData)
    {
      return this.Channel.getRithmicSettingsAsync(_traderData);
    }

    public void sendLogin(Trader _traderData)
    {
      this.Channel.sendLogin(_traderData);
    }

    public Task sendLoginAsync(Trader _traderData)
    {
      return this.Channel.sendLoginAsync(_traderData);
    }

    public int checkLicense(Trader _traderData, string SKU)
    {
      return this.Channel.checkLicense(_traderData, SKU);
    }

    public Task<int> checkLicenseAsync(Trader _traderData, string SKU)
    {
      return this.Channel.checkLicenseAsync(_traderData, SKU);
    }

    public InstrumentsContract[] getInstumentsSKU(Trader _traderData, string SKU, string ProviderName)
    {
      return this.Channel.getInstumentsSKU(_traderData, SKU, ProviderName);
    }

    public Task<InstrumentsContract[]> getInstumentsSKUAsync(Trader _traderData, string SKU, string ProviderName)
    {
      return this.Channel.getInstumentsSKUAsync(_traderData, SKU, ProviderName);
    }
  }
}
