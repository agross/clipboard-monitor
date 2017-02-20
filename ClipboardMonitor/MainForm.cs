using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ClipboardMonitor
{
  public partial class MainForm : Form
  {
    /// <summary>
    ///  A description of the regular expression:
    ///
    ///  href="
    ///      href="
    ///  [1]: A numbered capture group. [.*?]
    ///      Any character, any number of repetitions, as few as possible
    ///  "
    /// </summary>
    static readonly Regex Hrefs = new Regex("href=\"(.*?)\"",
                                            RegexOptions.CultureInvariant | RegexOptions.Compiled);

    string _lastText;

    public MainForm()
    {
      InitializeComponent();
    }

    protected override void WndProc(ref Message m)
    {
      base.WndProc(ref m);

      switch (m.Msg)
      {
        case NativeConstants.WM_CLIPBOARDUPDATE:
          var thisText = GetLinksOrText();
          if (!string.IsNullOrWhiteSpace(thisText))
          {
            if (_lastText != thisText)
            {
              txtList.AppendText(thisText + Environment.NewLine);
            }

            _lastText = thisText;
          }
          m.Result = IntPtr.Zero;
          break;
      }
    }

    static string GetLinksOrText()
    {
      if (Clipboard.ContainsText(TextDataFormat.Html))
      {
        var html = Clipboard.GetText(TextDataFormat.Html);
        var hrefs = Hrefs
          .Matches(html)
          .Cast<Match>()
          .Select(x => x.Groups[1].Value);

        return string.Join(Environment.NewLine, hrefs);
      }

      return Clipboard.GetText();
    }

    void btnClear_Click(object sender, EventArgs e)
    {
      if (MessageBox.Show("Do you really want to clear?",
                          "Are you sure?",
                          MessageBoxButtons.YesNo,
                          MessageBoxIcon.Question,
                          MessageBoxDefaultButton.Button2) == DialogResult.Yes)
      {
        txtList.Text = "";
      }
    }

    void btnCopy_Click(object sender, EventArgs e)
    {
      if (!string.IsNullOrEmpty(txtList.Text))
      {
        Clipboard.SetText(txtList.Text);
      }
    }

    void MainForm_FormClosed(object sender, FormClosedEventArgs e)
    {
      NativeMethods.RemoveClipboardFormatListener(Handle);
    }

    void MainForm_Shown(object sender, EventArgs e)
    {
      while (!NativeMethods.AddClipboardFormatListener(Handle))
      {
        if (MessageBox.Show("Could not set up clipboard monitor",
                            "Error",
                            MessageBoxButtons.RetryCancel,
                            MessageBoxIcon.Error,
                            MessageBoxDefaultButton.Button1) == DialogResult.Cancel)
        {
          return;
        }
      }
    }
  }
}
