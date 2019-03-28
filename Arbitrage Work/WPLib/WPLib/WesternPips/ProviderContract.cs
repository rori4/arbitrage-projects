// Decompiled with JetBrains decompiler
// Type: WPLib.WesternPips.ProviderContract
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
