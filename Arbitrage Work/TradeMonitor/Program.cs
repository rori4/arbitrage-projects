// Decompiled with JetBrains decompiler
// Type: TradeMonitor.Program
// Assembly: TradeMonitor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CEE2865B-9294-47DF-879B-0AFC01A708B6
// Assembly location: C:\Program Files (x86)\Westernpips\Westernpips Trade Monitor 3.7 Exclusive\TradeMonitor.exe

using System;
using System.Windows.Forms;

namespace TradeMonitor
{
  internal static class Program
  {
    [STAThread]
    private static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      int num = (int) new TM().ShowDialog();
      Login login = new Login();
      GOOD good = new GOOD();
      if (login.ShowDialog() == DialogResult.OK)
      {
        if (good.ShowDialog() != DialogResult.OK)
          return;
        Application.Run((Form) new MainWindow());
      }
//      else
//        Application.Exit();
    }
  }
}
