// Decompiled with JetBrains decompiler
// Type: WPLib.WesternPips.ILicenseServiceChannel
// Assembly: WPLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A67F71FE-CC9D-4C7E-B402-72B871993086
// Assembly location: C:\Program Files (x86)\Westernpips\Westernpips Trade Monitor 3.7 Exclusive\WPLib.dll

using System;
using System.CodeDom.Compiler;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace WPLib.WesternPips
{
  [GeneratedCode("System.ServiceModel", "4.0.0.0")]
  public interface ILicenseServiceChannel : ILicenseService, IClientChannel, IContextChannel, IChannel, ICommunicationObject, IExtensibleObject<IContextChannel>, IDisposable
  {
  }
}
