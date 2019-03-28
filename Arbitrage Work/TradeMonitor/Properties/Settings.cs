// Decompiled with JetBrains decompiler
// Type: TradeMonitor.Properties.Settings
// Assembly: TradeMonitor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CEE2865B-9294-47DF-879B-0AFC01A708B6
// Assembly location: C:\Program Files (x86)\Westernpips\Westernpips Trade Monitor 3.7 Exclusive\TradeMonitor.exe

using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace TradeMonitor.Properties
{
  [CompilerGenerated]
  [GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "12.0.0.0")]
  internal sealed class Settings : ApplicationSettingsBase
  {
    private static Settings defaultInstance = (Settings) SettingsBase.Synchronized((SettingsBase) new Settings());

    public static Settings Default
    {
      get
      {
        return Settings.defaultInstance;
      }
    }

    [ApplicationScopedSetting]
    [DebuggerNonUserCode]
    [SpecialSetting(SpecialSetting.ConnectionString)]
    [DefaultSettingValue("Data Source=|DataDirectory|\\TradeMonitor.sdf")]
    public string TradeMonitorConnectionString
    {
      get
      {
        return (string) this[nameof (TradeMonitorConnectionString)];
      }
    }
  }
}
