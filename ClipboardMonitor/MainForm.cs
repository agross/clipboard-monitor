using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
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
    Subject<int> _timeouts;
    Color _defaultBackground;
    IDisposable _disp;

    public MainForm()
    {
      InitializeComponent();

      _timeouts = new Subject<int>();
    }

    protected override void OnClosed(EventArgs e)
    {
      _disp.Dispose();
      base.OnClosed(e);
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
              _timeouts.OnNext(42);
              txtList.AppendText(thisText + Environment.NewLine);
              notifyIcon.ShowBalloonTip(500,
                                        "Link received",
                                        thisText,
                                        ToolTipIcon.Info);
            }

            _lastText = thisText;
          }
          m.Result = IntPtr.Zero;
          break;
      }
    }

    static string GetLinksOrText()
    {
      try
      {
        if (!Clipboard.ContainsText(TextDataFormat.Html))
        {
          return Clipboard.GetText();
        }

        var html = Clipboard.GetText(TextDataFormat.Html);
        var hrefs = Hrefs
          .Matches(html)
          .Cast<Match>()
          .Select(x => x.Groups[1].Value);

        return string.Join(Environment.NewLine, hrefs);
      }
      catch (System.Runtime.InteropServices.ExternalException)
      {
        return null;
      }
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

      _defaultBackground = BackColor;

      _disp = new CompositeDisposable(
                              _timeouts
                                .ObserveOn(this)
                                .Do(_ => BackColor = Color.LightGreen)
                                .Delay(TimeSpan.FromMilliseconds(500))
                                .ObserveOn(this)
                                .Do(_ => BackColor = _defaultBackground)
                                .Subscribe())
        ;

    }
  }
}
