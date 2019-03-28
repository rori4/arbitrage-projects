// Decompiled with JetBrains decompiler
// Type: MemoryDataWriter.Writer
// Assembly: WPLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A67F71FE-CC9D-4C7E-B402-72B871993086
// Assembly location: C:\Program Files (x86)\Westernpips\Westernpips Trade Monitor 3.7 Exclusive\WPLib.dll

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.MemoryMappedFiles;
using WPBase;

namespace MemoryDataWriter
{
  public class Writer : ITradeDataWriter
  {
    private Dictionary<string, MemoryMappedFile> RateFiles = new Dictionary<string, MemoryMappedFile>();

    public void close()
    {
      foreach (MemoryMappedFile memoryMappedFile in this.RateFiles.Values)
        memoryMappedFile?.Dispose();
    }

    public bool write(TradeInfo info)
    {
      bool flag = true;
      try
      {
        MemoryMappedFile rateFile;
        if (this.RateFiles.ContainsKey(info.Intrument))
        {
          rateFile = this.RateFiles[info.Intrument];
        }
        else
        {
          rateFile = MemoryMappedFile.CreateNew("Global\\" + info.Intrument, 16L);
          this.RateFiles[info.Intrument] = rateFile;
        }
        MemoryMappedViewAccessor viewAccessor = rateFile.CreateViewAccessor();
        viewAccessor.Write(0L, (double) info.Bid);
        viewAccessor.Write(8L, (double) info.Ask);
        viewAccessor.Dispose();
      }
      catch (Exception ex)
      {
        flag = false;
        this.writeException(ex);
      }
      return flag;
    }

    public void writeException(Exception e)
    {
      string source = "TradeMonitor";
      try
      {
        if (!EventLog.SourceExists(source))
          EventLog.CreateEventSource(source, "Application");
        EventLog.WriteEntry(source, e.Message);
      }
      catch
      {
        throw e;
      }
    }
  }
}
