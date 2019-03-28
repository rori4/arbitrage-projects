// Decompiled with JetBrains decompiler
// Type: TradeMonitor.NewInstrumentRequest
// Assembly: TradeMonitor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CEE2865B-9294-47DF-879B-0AFC01A708B6
// Assembly location: C:\Program Files (x86)\Westernpips\Westernpips Trade Monitor 3.7 Exclusive\TradeMonitor.exe

using SettingsProvider;
using SettingsProvider.WesternPipes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TradeMonitor
{
  public class NewInstrumentRequest : Form
  {
    private Dictionary<string, int> providerDict;
    private IContainer components;
    private Label lProvider;
    private ComboBox cbProvider;
    private Label lInstumentId;
    private TextBox tbInstrumentId;
    private Label lName;
    private TextBox tbName;
    private TextBox tbExchange;
    private Label Exchange;
    private Button bSubmit;

    public NewInstrumentRequest()
    {
      this.InitializeComponent();
      this.providerDict = new Dictionary<string, int>();
      LicenseServiceClient licenseServiceClient = new LicenseServiceClient();
      ProviderContract[] providers = licenseServiceClient.getProviders(new Trader()
      {
        Account = Settings.LocalSetting.User,
        Signature = Settings.LocalSetting.Config
      });
      if (providers != null)
      {
        foreach (ProviderContract providerContract in providers)
        {
          this.providerDict.Add(providerContract.Name, providerContract.Id);
          this.cbProvider.Items.Add((object) providerContract.Name);
        }
      }
      licenseServiceClient.Close();
    }

    private void bSubmit_Click(object sender, EventArgs e)
    {
      bool flag = true;
      string text = "";
      if (this.tbInstrumentId.Text == "")
      {
        text += "Instrument id must be defined\n";
        flag = false;
      }
      if (this.tbExchange.Text == "" && this.cbProvider.Text == "Rithmic")
      {
        text += "Exchange must be defined";
        flag = false;
      }
      if (this.tbName.Text == "")
      {
        text += "Name  must be defined";
        flag = false;
      }
      if (this.cbProvider.Text == "")
      {
        text += "Provider must be defined";
        flag = false;
      }
      if (!flag)
      {
        Error2.showError(text);
      }
      else
      {
        LicenseServiceClient licenseServiceClient = new LicenseServiceClient();
        int num = licenseServiceClient.addInstrument(new InstrumentsContract()
        {
          DisplayId = this.tbInstrumentId.Text,
          Description = this.tbName.Text,
          Parametr1 = this.tbExchange.Text,
          ProviderId = this.providerDict[this.cbProvider.Text]
        });
        licenseServiceClient.Close();
        if (num == 0)
        {
          Error2.showError("Instument already under revision");
        }
        else
        {
          if (num != 1)
            return;
          Error2.showError("Your request has been added");
        }
      }
    }

    private void cbProvider_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.cbProvider.Text == "Rithmic")
        this.tbExchange.Enabled = true;
      else
        this.tbExchange.Enabled = false;
    }

    private void tbInstrumentId_TextChanged(object sender, EventArgs e)
    {
    }

    private void lInstumentId_Click(object sender, EventArgs e)
    {
    }

    private void tbName_TextChanged(object sender, EventArgs e)
    {
    }

    private void lName_Click(object sender, EventArgs e)
    {
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.lProvider = new Label();
      this.cbProvider = new ComboBox();
      this.lInstumentId = new Label();
      this.tbInstrumentId = new TextBox();
      this.lName = new Label();
      this.tbName = new TextBox();
      this.tbExchange = new TextBox();
      this.Exchange = new Label();
      this.bSubmit = new Button();
      this.SuspendLayout();
      this.lProvider.AutoSize = true;
      this.lProvider.Location = new Point(12, 27);
      this.lProvider.Name = "lProvider";
      this.lProvider.Size = new Size(46, 13);
      this.lProvider.TabIndex = 0;
      this.lProvider.Text = "Provider";
      this.cbProvider.FormattingEnabled = true;
      this.cbProvider.Location = new Point(151, 24);
      this.cbProvider.Name = "cbProvider";
      this.cbProvider.Size = new Size(121, 21);
      this.cbProvider.TabIndex = 1;
      this.cbProvider.SelectedIndexChanged += new EventHandler(this.cbProvider_SelectedIndexChanged);
      this.lInstumentId.AutoSize = true;
      this.lInstumentId.Location = new Point(12, 64);
      this.lInstumentId.Name = "lInstumentId";
      this.lInstumentId.Size = new Size(62, 13);
      this.lInstumentId.TabIndex = 2;
      this.lInstumentId.Text = "InstumentId";
      this.lInstumentId.Click += new EventHandler(this.lInstumentId_Click);
      this.tbInstrumentId.Location = new Point(151, 61);
      this.tbInstrumentId.Name = "tbInstrumentId";
      this.tbInstrumentId.Size = new Size(121, 20);
      this.tbInstrumentId.TabIndex = 3;
      this.tbInstrumentId.TextChanged += new EventHandler(this.tbInstrumentId_TextChanged);
      this.lName.AutoSize = true;
      this.lName.Location = new Point(12, 99);
      this.lName.Name = "lName";
      this.lName.Size = new Size(35, 13);
      this.lName.TabIndex = 4;
      this.lName.Text = "Name";
      this.lName.Click += new EventHandler(this.lName_Click);
      this.tbName.Location = new Point(151, 96);
      this.tbName.Name = "tbName";
      this.tbName.Size = new Size(121, 20);
      this.tbName.TabIndex = 5;
      this.tbName.TextChanged += new EventHandler(this.tbName_TextChanged);
      this.tbExchange.Location = new Point(151, 133);
      this.tbExchange.Name = "tbExchange";
      this.tbExchange.Size = new Size(121, 20);
      this.tbExchange.TabIndex = 6;
      this.Exchange.AutoSize = true;
      this.Exchange.Location = new Point(12, 136);
      this.Exchange.Name = "Exchange";
      this.Exchange.Size = new Size(55, 13);
      this.Exchange.TabIndex = 7;
      this.Exchange.Text = "Exchange";
      this.bSubmit.Location = new Point(151, 180);
      this.bSubmit.Name = "bSubmit";
      this.bSubmit.Size = new Size(121, 23);
      this.bSubmit.TabIndex = 8;
      this.bSubmit.Text = "Send request";
      this.bSubmit.UseVisualStyleBackColor = true;
      this.bSubmit.Click += new EventHandler(this.bSubmit_Click);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(284, 225);
      this.Controls.Add((Control) this.bSubmit);
      this.Controls.Add((Control) this.Exchange);
      this.Controls.Add((Control) this.tbExchange);
      this.Controls.Add((Control) this.tbName);
      this.Controls.Add((Control) this.lName);
      this.Controls.Add((Control) this.tbInstrumentId);
      this.Controls.Add((Control) this.lInstumentId);
      this.Controls.Add((Control) this.cbProvider);
      this.Controls.Add((Control) this.lProvider);
      this.Name = nameof (NewInstrumentRequest);
      this.Text = nameof (NewInstrumentRequest);
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
