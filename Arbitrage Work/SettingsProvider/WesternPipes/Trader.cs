// Decompiled with JetBrains decompiler
// Type: SettingsProvider.WesternPipes.Trader
// Assembly: SettingsProvider, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 05C1E7FD-6DD9-4012-9105-50C0A7D91CF0
// Assembly location: F:\Arbitrage Cracks\TradeMonitor\lib\SettingsProvider.dll

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace SettingsProvider.WesternPipes
{
  [DebuggerStepThrough]
  [GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
  [DataContract(Name = "Trader", Namespace = "http://schemas.datacontract.org/2004/07/TraderData")]
  [Serializable]
  public class Trader : IExtensibleDataObject, INotifyPropertyChanged
  {
    [NonSerialized]
    private ExtensionDataObject extensionDataField;
    [OptionalField]
    private string AccountField;
    [OptionalField]
    private bool ServerFeedField;
    [OptionalField]
    private byte[] SignatureField;

    [Browsable(false)]
    public ExtensionDataObject ExtensionData
    {
      get
      {
        return this.extensionDataField;
      }
      set
      {
        this.extensionDataField = value;
      }
    }

    [DataMember]
    public string Account
    {
      get
      {
        return this.AccountField;
      }
      set
      {
        if ((object) this.AccountField == (object) value)
          return;
        this.AccountField = value;
        this.RaisePropertyChanged(nameof (Account));
      }
    }

    [DataMember]
    public bool ServerFeed
    {
      get
      {
        return this.ServerFeedField;
      }
      set
      {
        if (this.ServerFeedField.Equals(value))
          return;
        this.ServerFeedField = value;
        this.RaisePropertyChanged(nameof (ServerFeed));
      }
    }

    [DataMember]
    public byte[] Signature
    {
      get
      {
        return this.SignatureField;
      }
      set
      {
        if (this.SignatureField == value)
          return;
        this.SignatureField = value;
        this.RaisePropertyChanged(nameof (Signature));
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void RaisePropertyChanged(string propertyName)
    {
      // ISSUE: reference to a compiler-generated field
      PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
      if (propertyChanged == null)
        return;
      propertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
