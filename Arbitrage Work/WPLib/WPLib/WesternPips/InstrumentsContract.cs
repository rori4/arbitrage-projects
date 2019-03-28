// Decompiled with JetBrains decompiler
// Type: WPLib.WesternPips.InstrumentsContract
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
  [DataContract(Name = "InstrumentsContract", Namespace = "http://schemas.datacontract.org/2004/07/WesterpipesService")]
  [Serializable]
  public class InstrumentsContract : IExtensibleDataObject, INotifyPropertyChanged
  {
    [NonSerialized]
    private ExtensionDataObject extensionDataField;
    [OptionalField]
    private string DescriptionField;
    [OptionalField]
    private string DisplayIdField;
    [OptionalField]
    private bool EnabledField;
    [OptionalField]
    private long IdField;
    [OptionalField]
    private int InstrumentIdField;
    [OptionalField]
    private string Parametr1Field;
    [OptionalField]
    private string Parametr2Field;
    [OptionalField]
    private int ProviderIdField;
    [OptionalField]
    private int TraderIdField;

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
    public string Description
    {
      get
      {
        return this.DescriptionField;
      }
      set
      {
        if ((object) this.DescriptionField == (object) value)
          return;
        this.DescriptionField = value;
        this.RaisePropertyChanged(nameof (Description));
      }
    }

    [DataMember]
    public string DisplayId
    {
      get
      {
        return this.DisplayIdField;
      }
      set
      {
        if ((object) this.DisplayIdField == (object) value)
          return;
        this.DisplayIdField = value;
        this.RaisePropertyChanged(nameof (DisplayId));
      }
    }

    [DataMember]
    public bool Enabled
    {
      get
      {
        return this.EnabledField;
      }
      set
      {
        if (this.EnabledField.Equals(value))
          return;
        this.EnabledField = value;
        this.RaisePropertyChanged(nameof (Enabled));
      }
    }

    [DataMember]
    public long Id
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
    public int InstrumentId
    {
      get
      {
        return this.InstrumentIdField;
      }
      set
      {
        if (this.InstrumentIdField.Equals(value))
          return;
        this.InstrumentIdField = value;
        this.RaisePropertyChanged(nameof (InstrumentId));
      }
    }

    [DataMember]
    public string Parametr1
    {
      get
      {
        return this.Parametr1Field;
      }
      set
      {
        if ((object) this.Parametr1Field == (object) value)
          return;
        this.Parametr1Field = value;
        this.RaisePropertyChanged(nameof (Parametr1));
      }
    }

    [DataMember]
    public string Parametr2
    {
      get
      {
        return this.Parametr2Field;
      }
      set
      {
        if ((object) this.Parametr2Field == (object) value)
          return;
        this.Parametr2Field = value;
        this.RaisePropertyChanged(nameof (Parametr2));
      }
    }

    [DataMember]
    public int ProviderId
    {
      get
      {
        return this.ProviderIdField;
      }
      set
      {
        if (this.ProviderIdField.Equals(value))
          return;
        this.ProviderIdField = value;
        this.RaisePropertyChanged(nameof (ProviderId));
      }
    }

    [DataMember]
    public int TraderId
    {
      get
      {
        return this.TraderIdField;
      }
      set
      {
        if (this.TraderIdField.Equals(value))
          return;
        this.TraderIdField = value;
        this.RaisePropertyChanged(nameof (TraderId));
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
