namespace ClipboardMonitor
{
  partial class MainForm
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.Windows.Forms.Button btnCopy;
      System.Windows.Forms.Button btnClear;
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
      this.txtList = new System.Windows.Forms.TextBox();
      this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
      btnCopy = new System.Windows.Forms.Button();
      btnClear = new System.Windows.Forms.Button();
      this.SuspendLayout();
      //
      // btnCopy
      //
      btnCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      btnCopy.Location = new System.Drawing.Point(564, 377);
      btnCopy.Name = "btnCopy";
      btnCopy.Size = new System.Drawing.Size(75, 23);
      btnCopy.TabIndex = 1;
      btnCopy.Text = "Copy";
      btnCopy.UseVisualStyleBackColor = true;
      btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
      //
      // btnClear
      //
      btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      btnClear.Location = new System.Drawing.Point(483, 377);
      btnClear.Name = "btnClear";
      btnClear.Size = new System.Drawing.Size(75, 23);
      btnClear.TabIndex = 2;
      btnClear.Text = "Clear";
      btnClear.UseVisualStyleBackColor = true;
      btnClear.Click += new System.EventHandler(this.btnClear_Click);
      //
      // txtList
      //
      this.txtList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtList.Location = new System.Drawing.Point(12, 12);
      this.txtList.Multiline = true;
      this.txtList.Name = "txtList";
      this.txtList.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txtList.Size = new System.Drawing.Size(627, 359);
      this.txtList.TabIndex = 0;
      //
      // notifyIcon
      //
      this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
      this.notifyIcon.Visible = true;
      //
      // MainForm
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(651, 409);
      this.Controls.Add(btnClear);
      this.Controls.Add(btnCopy);
      this.Controls.Add(this.txtList);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "MainForm";
      this.Text = "Clipboard Monitor";
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
      this.Shown += new System.EventHandler(this.MainForm_Shown);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox txtList;
    private System.Windows.Forms.NotifyIcon notifyIcon;
  }
}

