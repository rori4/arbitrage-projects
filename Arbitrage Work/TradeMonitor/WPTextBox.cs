// Decompiled with JetBrains decompiler
// Type: TradeMonitor.WPTextBox
// Assembly: TradeMonitor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CEE2865B-9294-47DF-879B-0AFC01A708B6
// Assembly location: C:\Program Files (x86)\Westernpips\Westernpips Trade Monitor 3.7 Exclusive\TradeMonitor.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace TradeMonitor
{
  internal class WPTextBox : Control
  {
    private int selectionStart = -1;
    private Keys input = Keys.F24;
    private Point mousePos = Point.Empty;
    private List<int> lines = new List<int>();
    private ContextMenuStrip contextMenuStrip1;
    private IContainer components;
    private ToolStripMenuItem toolStripMenuItemCut;
    private ToolStripMenuItem toolStripMenuItemCopy;
    private ToolStripMenuItem toolStripMenuItemPaste;
    private ToolStripMenuItem toolStripMenuItemDel;
    private int startY;
    private int lineHeight;
    private int maxHeight;
    private int selectionLength;
    private string selectText;
    private int charIndex;
    private bool selecting;
    private int lineIndex;
    private bool DialogChar;
    private bool Modified;
    private const int maxLength = 32;

    public override Font Font
    {
      get
      {
        return base.Font;
      }
      set
      {
        base.Font = value;
        this.Modified = true;
        this.lineHeight = this.Font.Height;
      }
    }

    public int SelectionStart
    {
      get
      {
        return this.selectionStart;
      }
      set
      {
        this.selectionStart = value;
      }
    }

    public int SelectionLength
    {
      get
      {
        return this.selectionLength;
      }
      set
      {
        this.selectionLength = value;
      }
    }

    public string SelectText
    {
      get
      {
        this.selectText = "";
        if (this.SelectionLength > 0)
          this.selectText = this.Text.Substring(this.SelectionStart, this.SelectionLength);
        return this.selectText;
      }
      set
      {
        this.selectText = value;
      }
    }

    [DllImport("user32.dll")]
    private static extern bool CreateCaret(IntPtr hWnd, IntPtr hBitmap, int nWidth, int nHeight);

    [DllImport("user32.dll")]
    public static extern bool SetCaretPos(int x, int y);

    [DllImport("user32.dll")]
    public static extern bool GetCaretPos(ref Point lpPoint);

    [DllImport("user32.dll")]
    private static extern bool ShowCaret(IntPtr hWnd);

    [DllImport("user32.dll")]
    private static extern bool HideCaret(IntPtr hWnd);

    [DllImport("user32.dll")]
    private static extern bool DestroyCaret();

    public int CharIndex
    {
      get
      {
        return this.charIndex;
      }
      set
      {
        this.charIndex = value;
        if (this.Selecting)
        {
          if (this.SelectionStart == -1)
            this.SelectionStart = this.charIndex;
          this.SelectionLength = value - this.SelectionStart;
        }
        else
        {
          this.SelectionStart = -1;
          this.SelectionLength = 0;
        }
      }
    }

    public bool Selecting
    {
      get
      {
        return this.selecting;
      }
      set
      {
        if (!this.selecting & value)
        {
          this.SelectionStart = -1;
          this.SelectionLength = 0;
        }
        if (!value)
          this.NormalizeStartEnd();
        this.selecting = value;
      }
    }

    public WPTextBox()
    {
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint, true);
      this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
      this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
      this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
      this.Cursor = Cursors.IBeam;
      this.ImeMode = ImeMode.On;
    }

    protected override void OnGotFocus(EventArgs e)
    {
      this.lineHeight = this.Font.Height;
      WPTextBox.CreateCaret(this.Handle, new IntPtr(0), 1, this.lineHeight);
      WPTextBox.ShowCaret(this.Handle);
    }

    protected override void OnLostFocus(EventArgs e)
    {
      if (this.input == Keys.Left || this.input == Keys.Right || (this.input == Keys.Up || this.input == Keys.Down) || this.Selecting)
        this.Focus();
      else
        WPTextBox.DestroyCaret();
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      this.Focus();
      base.OnMouseDown(e);
      if (this.Text.Length == 0)
        return;
      if (e.Button == MouseButtons.Left)
      {
        this.mousePos = e.Location;
        this.Selecting = true;
        this.SelectionLength = 0;
      }
      this.Invalidate();
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Left)
      {
        this.mousePos = e.Location;
        this.Invalidate();
      }
      base.OnMouseMove(e);
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Left)
        this.Selecting = false;
      base.OnMouseUp(e);
    }

    protected override void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
    {
      this.input = e.KeyCode;
      this.Selecting = e.Shift;
      base.OnPreviewKeyDown(e);
      this.Invalidate();
    }

    private void SetCaretAtPosX(Graphics g, int posX)
    {
      if (this.lineIndex == 1 && (this.Text[0] == '\r' || posX < 3))
      {
        this.CharIndex = 0;
        WPTextBox.SetCaretPos(1, 0);
      }
      else if (this.lineIndex != 1 && this.lines[this.lineIndex - 1] + 1 < this.Text.Length && this.Text[this.lines[this.lineIndex - 1] + 1] == '\r')
      {
        this.CharIndex = this.lines[this.lineIndex - 1] + 1;
        WPTextBox.SetCaretPos(1, (this.lineIndex - 1) * this.lineHeight + this.startY);
      }
      else
      {
        int First;
        for (First = this.lines[this.lineIndex - 1] + 1; First <= this.lines[this.lineIndex]; ++First)
        {
          if (this.Text[First] == '\r')
          {
            this.CharIndex = First;
            this.SetCaretAtNormalChar(g, false);
            break;
          }
          CharacterRange[] ranges = new CharacterRange[1]
          {
            new CharacterRange(First, 1)
          };
          StringFormat stringFormat = new StringFormat();
          stringFormat.SetMeasurableCharacterRanges(ranges);
          stringFormat.FormatFlags = StringFormatFlags.MeasureTrailingSpaces;
          RectangleF bounds = g.MeasureCharacterRanges(this.Text, this.Font, new RectangleF(0.0f, (float) this.startY, (float) this.Width, (float) this.maxHeight), stringFormat)[0].GetBounds(g);
          if ((double) posX <= (double) bounds.X + (double) bounds.Width / 2.0)
          {
            this.CharIndex = First;
            WPTextBox.SetCaretPos((int) bounds.Left, (int) bounds.Y);
            break;
          }
        }
        if (First <= this.lines[this.lineIndex])
          return;
        this.CharIndex = First;
        this.SetCaretAtNormalChar(g, false);
      }
    }

    private void SetCaretPosAtMouse(Graphics g)
    {
      if (this.Text.Length < 3 || this.mousePos == Point.Empty)
        return;
      this.CalculateLines(g);
      this.lineIndex = (this.mousePos.Y - this.startY) / this.lineHeight + 1;
      if (this.lineIndex < 1)
        this.lineIndex = 1;
      if (this.lineIndex > this.lines.Count - 1)
        this.SetCaretAtEnd(g);
      else
        this.SetCaretAtPosX(g, this.mousePos.X);
      this.mousePos = Point.Empty;
    }

    private void NormalizeStartEnd()
    {
      if (this.SelectionLength >= 0)
        return;
      this.selectionStart += this.SelectionLength;
      this.SelectionLength = -this.SelectionLength;
    }

    private void DrawSelection(Graphics g)
    {
      if (this.SelectionStart == -1 || this.SelectionLength == 0)
        return;
      int num1 = this.SelectionLength;
      int First = this.selectionStart;
      if (this.SelectionLength < 0)
      {
        First = this.selectionStart + this.SelectionLength;
        num1 = -this.SelectionLength;
      }
      int num2 = num1 / 32;
      CharacterRange[] ranges = new CharacterRange[num2 + 1];
      ranges[0] = new CharacterRange(First, num1 - num2 * 32);
      for (int index = 1; index < num2 + 1; ++index)
        ranges[index] = new CharacterRange(ranges[index - 1].First + ranges[index - 1].Length, 32);
      StringFormat stringFormat = new StringFormat();
      stringFormat.SetMeasurableCharacterRanges(ranges);
      stringFormat.FormatFlags = StringFormatFlags.MeasureTrailingSpaces;
      foreach (Region measureCharacterRange in g.MeasureCharacterRanges(this.Text, this.Font, new RectangleF(0.0f, (float) this.startY, (float) this.Width, (float) this.maxHeight), stringFormat))
        g.FillRegion(Brushes.LightBlue, measureCharacterRange);
    }

    private void CalculateCaretPos(Graphics g, Keys input)
    {
      if (input == Keys.F24)
        return;
      if (this.Text.Length == 0)
      {
        WPTextBox.SetCaretPos(1, 1);
      }
      else
      {
        bool flag = false;
        bool afterReturn = false;
        Point empty1 = Point.Empty;
        switch (input)
        {
          case Keys.Back:
            this.Modified = true;
            this.AutoScrollDown();
            if (this.SelectionLength > 0)
            {
              this.Text = this.Text.Remove(this.SelectionStart, this.SelectionLength);
              this.CharIndex = this.SelectionStart;
              flag = false;
              this.Selecting = false;
              break;
            }
            this.Selecting = false;
            --this.CharIndex;
            if (this.CharIndex > 0)
            {
              if (this.Text[this.CharIndex] == '\n')
              {
                this.Text = this.Text.Remove(--this.CharIndex, 2);
                if (this.CharIndex <= 0)
                {
                  WPTextBox.SetCaretPos(1, 0);
                  this.CharIndex = 0;
                  flag = true;
                  break;
                }
                if (this.Text[this.CharIndex - 1] == '\n')
                {
                  WPTextBox.GetCaretPos(ref empty1);
                  WPTextBox.SetCaretPos(1, empty1.Y - this.lineHeight);
                  flag = true;
                  break;
                }
                flag = false;
                break;
              }
              this.Text = this.Text.Remove(this.CharIndex, 1);
              if (this.Text[this.CharIndex - 1] == '\n')
              {
                WPTextBox.GetCaretPos(ref empty1);
                WPTextBox.SetCaretPos(1, empty1.Y);
                flag = true;
                break;
              }
              flag = false;
              break;
            }
            if (this.CharIndex == 0)
            {
              this.Text = this.Text.Remove(this.CharIndex, 1);
              WPTextBox.SetCaretPos(1, 0);
              flag = true;
              break;
            }
            this.CharIndex = 0;
            this.lineIndex = 1;
            flag = true;
            break;
          case Keys.Return:
            this.Modified = true;
            this.AutoScrollUp();
            WPTextBox.GetCaretPos(ref empty1);
            WPTextBox.SetCaretPos(1, empty1.Y + this.lineHeight);
            flag = true;
            break;
          case Keys.End:
            this.Modified = true;
            this.CalculateLines(g);
            this.SetCaretAtEnd(g);
            flag = true;
            break;
          case Keys.Home:
            this.CharIndex = 0;
            this.startY = 0;
            this.lineIndex = 1;
            WPTextBox.SetCaretPos(1, 1);
            flag = true;
            break;
          case Keys.Left:
            this.Modified = true;
            this.AutoScrollDown();
            if (--this.CharIndex <= 0)
            {
              this.CharIndex = 0;
              this.lineIndex = 1;
              WPTextBox.SetCaretPos(1, 0);
              flag = true;
              break;
            }
            if (this.Text[this.CharIndex - 1] != '\n' && this.Text[this.CharIndex - 1] != '\r')
            {
              flag = false;
              break;
            }
            if (this.Text[this.CharIndex - 1] == '\n')
            {
              afterReturn = true;
              flag = false;
              break;
            }
            if (this.Text[this.CharIndex] == '\n')
            {
              --this.CharIndex;
              if (this.CharIndex <= 0)
              {
                WPTextBox.SetCaretPos(1, 0);
                this.CharIndex = 0;
                flag = true;
                break;
              }
              if (this.Text[this.CharIndex - 1] == '\n')
              {
                WPTextBox.GetCaretPos(ref empty1);
                WPTextBox.SetCaretPos(1, empty1.Y - this.lineHeight);
                flag = true;
                break;
              }
              flag = false;
              break;
            }
            break;
          case Keys.Up:
            this.CalculateLines(g);
            this.AutoScrollDown();
            if (this.lineIndex > 1)
            {
              --this.lineIndex;
              WPTextBox.GetCaretPos(ref empty1);
              this.SetCaretAtPosX(g, empty1.X);
            }
            flag = true;
            break;
          case Keys.Right:
            this.Modified = true;
            if (++this.CharIndex > this.Text.Length)
            {
              this.CharIndex = this.Text.Length;
              flag = true;
              break;
            }
            this.AutoScrollUp();
            if (this.CharIndex < this.Text.Length && (this.Text[this.CharIndex - 1] == '\r' || this.Text[this.CharIndex - 1] == '\n'))
            {
              if (this.Text[this.CharIndex - 1] == '\r')
                ++this.CharIndex;
              WPTextBox.GetCaretPos(ref empty1);
              WPTextBox.SetCaretPos(1, empty1.Y + this.lineHeight);
              flag = true;
              break;
            }
            flag = false;
            break;
          case Keys.Down:
            this.CalculateLines(g);
            this.AutoScrollUp();
            if (this.lineIndex < this.lines.Count)
            {
              ++this.lineIndex;
              if (this.lineIndex == this.lines.Count)
              {
                this.lineIndex = this.lines.Count - 1;
                this.SetCaretAtEnd(g);
              }
              else
              {
                Point empty2 = Point.Empty;
                WPTextBox.GetCaretPos(ref empty2);
                this.SetCaretAtPosX(g, empty2.X);
              }
            }
            flag = true;
            break;
          case Keys.Delete:
            this.Modified = true;
            if (this.SelectionLength > 0)
            {
              this.Text = this.Text.Remove(this.SelectionStart, this.SelectionLength);
              this.CharIndex = this.SelectionStart;
              flag = false;
              this.Selecting = false;
              break;
            }
            if (this.CharIndex == this.Text.Length)
            {
              flag = true;
              break;
            }
            if (this.Text[this.CharIndex] == '\r')
              this.Text = this.Text.Remove(this.CharIndex, 2);
            else
              this.Text = this.Text.Remove(this.CharIndex, 1);
            flag = true;
            break;
          default:
            if (this.DialogChar)
            {
              this.DialogChar = false;
              flag = false;
              break;
            }
            break;
        }
        if (!flag)
          this.SetCaretAtNormalChar(g, afterReturn);
        input = Keys.F24;
      }
    }

    private void SetCaretAtEnd(Graphics g)
    {
      this.CharIndex = this.Text.Length;
      this.CalculateLines(g);
      if (this.Text[this.Text.Length - 1] == '\n')
      {
        this.lineIndex = this.lines.Count;
        WPTextBox.SetCaretPos(1, (this.lineIndex - 1) * this.lineHeight);
      }
      else
      {
        this.lineIndex = this.lines.Count - 1;
        this.SetCaretAtNormalChar(g, false);
      }
    }

    private void SetCaretAtNormalChar(Graphics g, bool afterReturn)
    {
      if (this.CharIndex == 0)
      {
        WPTextBox.SetCaretPos(1, 0);
      }
      else
      {
        if (this.maxHeight == 0)
          this.maxHeight = this.Height;
        if (afterReturn)
        {
          CharacterRange[] ranges = new CharacterRange[1]
          {
            new CharacterRange(this.CharIndex, 1)
          };
          StringFormat stringFormat = new StringFormat();
          stringFormat.SetMeasurableCharacterRanges(ranges);
          stringFormat.FormatFlags = StringFormatFlags.MeasureTrailingSpaces;
          RectangleF bounds = g.MeasureCharacterRanges(this.Text, this.Font, new RectangleF(0.0f, (float) this.startY, (float) this.Width, (float) this.maxHeight), stringFormat)[0].GetBounds(g);
          WPTextBox.SetCaretPos((int) bounds.Left, (int) bounds.Top);
        }
        else
        {
          int num = (int) this.Text[this.CharIndex - 1];
          CharacterRange[] ranges = new CharacterRange[1]
          {
            new CharacterRange(this.CharIndex - 1, 1)
          };
          StringFormat stringFormat = new StringFormat();
          stringFormat.SetMeasurableCharacterRanges(ranges);
          stringFormat.FormatFlags = StringFormatFlags.MeasureTrailingSpaces;
          RectangleF bounds = g.MeasureCharacterRanges(this.Text, this.Font, new RectangleF(0.0f, (float) this.startY, (float) this.Width, (float) this.maxHeight), stringFormat)[0].GetBounds(g);
          WPTextBox.SetCaretPos((int) bounds.Right, (int) bounds.Top);
        }
      }
    }

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
      if (keyData == Keys.Delete)
        return true;
      return base.ProcessCmdKey(ref msg, keyData);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      if (this.Focused)
      {
        this.CalculateCaretPos(e.Graphics, this.input);
        this.input = Keys.F24;
        this.SetCaretPosAtMouse(e.Graphics);
      }
      else
      {
        WPTextBox.HideCaret(this.Handle);
        this.input = Keys.F24;
      }
      e.Graphics.DrawString(this.Text, this.Font, (Brush) new SolidBrush(this.ForeColor), new RectangleF(0.0f, (float) this.startY, (float) this.Width, (float) (this.Height - this.startY)), StringFormat.GenericDefault);
    }

    protected override bool ProcessDialogChar(char charCode)
    {
      if (charCode == '\b' || charCode == '\x001A' || charCode == '\x0019')
        return false;
      this.Modified = true;
      if (this.SelectionLength > 0)
      {
        this.Text = this.Text.Remove(this.SelectionStart, this.SelectionLength);
        this.CharIndex = this.SelectionStart;
      }
      this.Selecting = false;
      if (charCode == '\r')
      {
        this.Text = this.Text.Insert(this.CharIndex, Environment.NewLine);
        this.CharIndex += Environment.NewLine.Length;
        return true;
      }
      if (this.CharIndex == this.Text.Length)
      {
        this.Text += charCode.ToString();
        ++this.CharIndex;
      }
      else
        this.Text = this.Text.Insert(this.CharIndex++, charCode.ToString());
      this.DialogChar = true;
      this.Invalidate();
      return true;
    }

    protected override void OnSizeChanged(EventArgs e)
    {
      this.Modified = true;
      base.OnSizeChanged(e);
    }

    private void CalculateLines(Graphics g)
    {
      if (!this.Modified)
        return;
      this.lines.Clear();
      this.lines.Add(0);
      StringFormat stringFormat = new StringFormat();
      stringFormat.FormatFlags = StringFormatFlags.MeasureTrailingSpaces;
      RectangleF layoutRect = new RectangleF(0.0f, (float) this.startY, (float) this.Width, (float) (this.Height - this.startY + this.lineHeight));
      int First;
      for (First = 0; First < this.Text.Length; ++First)
      {
        if (this.Text[First] == '\r')
        {
          this.lines.Add(++First);
        }
        else
        {
          CharacterRange[] ranges = new CharacterRange[1]
          {
            new CharacterRange(First, 1)
          };
          stringFormat.SetMeasurableCharacterRanges(ranges);
          RectangleF bounds = g.MeasureCharacterRanges(this.Text, this.Font, layoutRect, stringFormat)[0].GetBounds(g);
          if (this.input == Keys.End && (double) bounds.Height == 0.0 && this.Text[First] != ' ')
          {
            layoutRect.Height += (float) this.lineHeight;
            --First;
          }
          else if ((double) bounds.Y > (double) (this.lineHeight * this.lines.Count + this.startY) - (double) bounds.Height / 2.0)
            this.lines.Add(First - 1);
        }
      }
      if ((double) layoutRect.Height > (double) this.maxHeight)
        this.maxHeight = (int) layoutRect.Height;
      if (First == this.Text.Length && this.lines[this.lines.Count - 1] != this.Text.Length - 1)
        this.lines.Add(this.Text.Length - 1);
      Point empty = Point.Empty;
      WPTextBox.GetCaretPos(ref empty);
      this.lineIndex = (int) ((double) (empty.Y - this.startY) / (double) this.lineHeight + 1.5);
      if (this.lineIndex < 1)
      {
        this.lineIndex = 1;
        this.startY = 0;
      }
      if (this.CharIndex == this.Text.Length || this.lineIndex >= this.lines.Count)
        this.lineIndex = this.Text[this.Text.Length - 1] != '\n' ? this.lines.Count - 1 : this.lines.Count;
      if (this.input == Keys.End)
      {
        this.startY = this.Height - this.lineHeight * this.lines.Count;
        if (this.startY > 0)
          this.startY = 0;
      }
      this.Modified = false;
    }

    private void AutoScrollUp()
    {
      if (Point.Empty.Y + this.lineHeight <= this.Height || this.maxHeight + this.startY <= 0)
        return;
      this.Modified = true;
      this.startY -= this.lineHeight;
    }

    private void AutoScrollDown()
    {
      if (Point.Empty.Y < this.lineHeight && this.CharIndex > 0)
        this.startY += this.lineHeight;
      if (this.lineIndex > 2)
        return;
      this.startY = 0;
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      this.contextMenuStrip1 = new ContextMenuStrip(this.components);
      this.toolStripMenuItemCut = new ToolStripMenuItem();
      this.toolStripMenuItemCopy = new ToolStripMenuItem();
      this.toolStripMenuItemPaste = new ToolStripMenuItem();
      this.toolStripMenuItemDel = new ToolStripMenuItem();
      this.contextMenuStrip1.SuspendLayout();
      this.SuspendLayout();
      this.contextMenuStrip1.Items.AddRange(new ToolStripItem[4]
      {
        (ToolStripItem) this.toolStripMenuItemCut,
        (ToolStripItem) this.toolStripMenuItemCopy,
        (ToolStripItem) this.toolStripMenuItemPaste,
        (ToolStripItem) this.toolStripMenuItemDel
      });
      this.contextMenuStrip1.Name = "contextMenuStrip1";
      this.contextMenuStrip1.Size = new Size(148, 92);
      this.contextMenuStrip1.Opening += new CancelEventHandler(this.contextMenuStrip1_Opening);
      this.toolStripMenuItemCut.Name = "toolStripMenuItemCut";
      this.toolStripMenuItemCut.ShortcutKeys = Keys.X | Keys.Control;
      this.toolStripMenuItemCut.Size = new Size(147, 22);
      this.toolStripMenuItemCut.Text = "Cu&t";
      this.toolStripMenuItemCut.Click += new EventHandler(this.toolStripMenuItemCut_Click);
      this.toolStripMenuItemCopy.Name = "toolStripMenuItemCopy";
      this.toolStripMenuItemCopy.ShortcutKeys = Keys.C | Keys.Control;
      this.toolStripMenuItemCopy.Size = new Size(147, 22);
      this.toolStripMenuItemCopy.Text = "&Copy";
      this.toolStripMenuItemCopy.Click += new EventHandler(this.toolStripMenuItemCopy_Click);
      this.toolStripMenuItemPaste.Name = "toolStripMenuItemPaste";
      this.toolStripMenuItemPaste.ShortcutKeys = Keys.V | Keys.Control;
      this.toolStripMenuItemPaste.Size = new Size(147, 22);
      this.toolStripMenuItemPaste.Text = "&Paste";
      this.toolStripMenuItemPaste.Click += new EventHandler(this.toolStripMenuItemPaste_Click);
      this.toolStripMenuItemDel.Name = "toolStripMenuItemDel";
      this.toolStripMenuItemDel.ShortcutKeys = Keys.Delete;
      this.toolStripMenuItemDel.Size = new Size(147, 22);
      this.toolStripMenuItemDel.Text = "Delete";
      this.toolStripMenuItemDel.Click += new EventHandler(this.toolStripMenuItemDel_Click);
      this.ContextMenuStrip = this.contextMenuStrip1;
      this.contextMenuStrip1.ResumeLayout(false);
      this.ResumeLayout(false);
    }

    protected override void Dispose(bool disposing)
    {
      this.components.Dispose();
      base.Dispose(disposing);
    }

    private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
    {
      if (this.SelectionLength == 0)
      {
        this.toolStripMenuItemCut.Enabled = false;
        this.toolStripMenuItemCopy.Enabled = false;
        this.toolStripMenuItemDel.Enabled = false;
      }
      else
      {
        this.toolStripMenuItemCut.Enabled = true;
        this.toolStripMenuItemCopy.Enabled = true;
        this.toolStripMenuItemDel.Enabled = true;
      }
      if (Clipboard.ContainsText())
        this.toolStripMenuItemPaste.Enabled = true;
      else
        this.toolStripMenuItemPaste.Enabled = false;
    }

    private void toolStripMenuItemDel_Click(object sender, EventArgs e)
    {
      this.input = Keys.Delete;
      this.Invalidate();
    }

    private void toolStripMenuItemCut_Click(object sender, EventArgs e)
    {
      if (this.SelectionLength <= 0)
        return;
      Clipboard.SetText(this.SelectText);
      this.input = Keys.Delete;
      this.Invalidate();
    }

    private void toolStripMenuItemCopy_Click(object sender, EventArgs e)
    {
      if (this.SelectionLength <= 0)
        return;
      Clipboard.SetText(this.SelectText);
    }

    private void toolStripMenuItemPaste_Click(object sender, EventArgs e)
    {
      if (!Clipboard.ContainsText())
        return;
      if (this.SelectionLength > 0)
      {
        this.Text = this.Text.Remove(this.SelectionStart, this.SelectionLength);
        this.CharIndex = this.SelectionStart;
      }
      this.Text = this.Text.Insert(this.CharIndex, Clipboard.GetText());
      this.CharIndex += Clipboard.GetText().Length;
      this.Modified = true;
      this.Invalidate();
    }
  }
}
