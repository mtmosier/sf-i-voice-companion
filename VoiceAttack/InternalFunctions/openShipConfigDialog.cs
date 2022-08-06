using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



public class VAInline
{
	public void main() {
        Form frm = new WindowsFormsApp1.Form1();
        frm.ShowDialog();
    }



/*

    /// <summary>
    /// This is the calling convention of the above method.
    /// Use this to show a custom dialog in your source code.
    /// </summary>
    private void ExampleCall()
    {
        DialogResult result = BetterDialog.ShowDialog("Reset Journal Settings",
            "Settings will be erased permanently",
            "This will not affect any of the text content in the database.",
            "Reset", "Cancel", null);

        if (result == DialogResult.OK)
        {
            Console.WriteLine("User accepted the dialog");
        }
    }
*/
}


namespace WindowsFormsApp1
{


    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Application.OpenForms["Form1"].FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Application.OpenForms["Form1"].MinimumSize = new System.Drawing.Size(635, 283);
            Application.OpenForms["Form1"].Location = new System.Drawing.Point(996, 1114);
            textBox1.SelectionStart = 0;
        }


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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            //
            // textBox1
            //
            // this.textBox1.BackColor = System.Drawing.SystemColors.MenuText;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Font = new System.Drawing.Font("Euro Caps", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // this.textBox1.ForeColor = System.Drawing.Color.Lime;
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(627, 282);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = "Welcome, Commander Brigetiol!\r\n\r\nSystems initializing...";
            //
            // Form1
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            // this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(627, 248);
            this.Controls.Add(this.textBox1);
            this.Font = new System.Drawing.Font("Euro Caps", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Location = new System.Drawing.Point(966, 1114);
            this.MinimumSize = new System.Drawing.Size(635, 283);
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
    }
}


/*
public class BetterDialog : Form
{
    /// <summary>
    /// This method is part of the dialog.
    /// </summary>
    static public DialogResult ShowDialog(string title,
        string largeHeading,
        string smallExplanation,
        string leftButton,
        string rightButton,
        Image iconSet)
    {
        // Call the private constructor so the users only need to call this
        // function, which is similar to MessageBox.Show.
        // Returns a standard DialogResult.
        using (BetterDialog dialog = new BetterDialog(title, largeHeading,
            smallExplanation, leftButton, rightButton, iconSet))
        {
            DialogResult result = dialog.ShowDialog();
            return result;
        }
    }


    /// <summary>
    /// Use this with the above static method.
    /// </summary>
    private BetterDialog(string title,
        string largeHeading,
        string smallExplanation,
        string leftButton,
        string rightButton,
        Image iconSet)
    {
        // Set up some properties.
        this.Font = SystemFonts.MessageBoxFont;
        this.ForeColor = SystemColors.WindowText;
        InitializeComponent();
        this.Width = 350;
        this.Height = 150;

        // Do some measurements with Graphics.
        using (Graphics graphics = this.CreateGraphics())
        {
            SizeF smallSize;
            SizeF bigSize;

            if (string.IsNullOrEmpty(smallExplanation) == false)
            {
                // Note: at this point, we could detect that the OS is Vista
                // and do some customizations. That logic is in the download.
                // The code here does some measurements.
                label1.Font = new Font(SystemFonts.MessageBoxFont.FontFamily.Name, 8.0f,
                    FontStyle.Bold, GraphicsUnit.Point);
                smallSize = graphics.MeasureString(smallExplanation, this.Font,
                    this.label2.Width);
                bigSize = graphics.MeasureString(largeHeading, label1.Font,
                    this.label1.Width);
                this.Height = (int)smallSize.Height + 166;
                double bigger = (smallSize.Width > bigSize.Width) ?
                    smallSize.Width : bigSize.Width;
                this.Width = (int)bigger + 100;
            }
            else
            {
                // We have a null "smallExplanation", so we have a single message
                // dialog. Do some different changes. Code omitted for brevity (you
                // can find the logic in the download if you want to see it).
            }
        }

        // Establish a minimum width.
        if (this.Width < 260)
        {
            this.Width = 260;
        }

        // Set the title, and some Text properties.
        this.Text = title;
        label1.Text = largeHeading;
        label2.Text = string.IsNullOrEmpty(smallExplanation) ?
            string.Empty : smallExplanation;

        // Set the left button, which is optional.
        if (string.IsNullOrEmpty(leftButton) == false)
        {
            this.buttonLeft.Text = leftButton;
        }
        else
        {
            this.AcceptButton = buttonRight;
            this.buttonLeft.Visible = false;
        }
        this.buttonRight.Text = rightButton;

        // Set the PictureBox and the icon.
        pictureBox1.Image = iconSet;
    }


}
*/
