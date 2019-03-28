// Decompiled with JetBrains decompiler
// Type: WPLib.WesternPips.Trader
// Assembly: WPLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A67F71FE-CC9D-4C7E-B402-72B871993086
// Assembly location: C:\Program Files (x86)\Westernpips\Westernpips Trade Monitor 3.7 Exclusive\WPLib.dll

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace WPLib.WesternPips
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
