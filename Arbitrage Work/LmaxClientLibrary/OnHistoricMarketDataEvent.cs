﻿// Decompiled with JetBrains decompiler
// Type: Com.Lmax.Api.OnHistoricMarketDataEvent
// Assembly: LmaxClientLibrary, Version=1.8.2.0, Culture=neutral, PublicKeyToken=f45712b013135aca
// MVID: 5CF42455-8EBA-41F5-A8DC-3DC7279B187B
// Assembly location: C:\Users\range\Desktop\Arbitrage\new hope\ipcbus\LmaxClientLibrary.dll

using System;
using System.Collections.Generic;

namespace Com.Lmax.Api
{
  public delegate void OnHistoricMarketDataEvent(string instructionId, List<Uri> uris);
}