// Decompiled with JetBrains decompiler
// Type: SettingsProvider.WesternPipes.ProviderContract
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
  [DataContract(Name = "ProviderContract", Namespace = "http://schemas.datacontract.org/2004/07/WesterpipesService")]
  [Serializable]
  public class ProviderContract : IExtensibleDataObject, INotifyPropertyChanged
  {
    [NonSerialized]
    private ExtensionDataObject extensionDataField;
    [OptionalField]
    private int IdField;
    [OptionalField]
    private string NameField;

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
    public int Id
    {
      get
      {
        return this.IdField;
      }
      set
      {
        if (this.IdField.Equals(value))
          return;
        this.IdField = value;
        this.RaisePropertyChanged(nameof (Id));
      }
    }

    [DataMember]
    public string Name
    {
      get
      {
        return this.NameField;
      }
      set
      {
        if ((object) this.NameField == (object) value)
          return;
        this.NameField = value;
        this.RaisePropertyChanged(nameof (Name));
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
