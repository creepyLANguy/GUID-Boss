using System;
using System.Text;
using System.Windows.Forms;

namespace GUIDBoss
{
  public partial class Form1 : Form
  {
    private const bool shouldCopyToClipBoard = true;
    private const bool shouldHighlightText = false;

    private const int opacity_max = 100;

    private int opacity_min = 30;

    private const int balloonTime = 1000;

    private static string guid;

    public Form1()
    {
      InitializeComponent();
    }

    private void Form1_Load(object sender, EventArgs e)
    {
      textBox1.TextAlign = HorizontalAlignment.Center;
      PerformGUIDGenerationProcedure();
    }

    private void button1_Click(object sender, EventArgs e)
    {
      PerformGUIDGenerationProcedure();
    }

    private void PerformGUIDGenerationProcedure()
    {
      guid = Guid.NewGuid().ToString();

      textBox1.Text = guid;

      if (shouldHighlightText == true)
      {
        textBox1.SelectAll();
        textBox1.Focus();
      }
      else
      {
        this.Focus();
      }

      if (shouldCopyToClipBoard == true)
      {
        Clipboard.SetText(guid);

        notifyIcon1.BalloonTipTitle = "Copied To Clipboard"; 
        notifyIcon1.BalloonTipText = guid;
        notifyIcon1.ShowBalloonTip(balloonTime);
      }
    }

    private void Form1_Activated(object sender, EventArgs e)
    {
      this.Opacity = (float)(opacity_max) / 100;
    }

    private void Form1_Deactivate(object sender, EventArgs e)
    {
      if (textBox1.Text != guid)
      {
        int newOpacity = opacity_min;
        if (int.TryParse(textBox1.Text, out newOpacity))
        {
          opacity_min = newOpacity;
        }
      }

      this.Opacity = (float)(opacity_min) / 100;
    }

    private void generateGUIDToolStripMenuItem_Click(object sender, EventArgs e)
    {
      PerformGUIDGenerationProcedure();
    }

    private void exitToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
    {
      MouseEventArgs me = (MouseEventArgs) e;

      if (me.Button == MouseButtons.Left)
      {
        if (this.Visible == false)
        {
          this.Show();
          this.Activate();
        }
        else
        {
          this.Hide();
          notifyIcon1.BalloonTipTitle = "";
          notifyIcon1.BalloonTipText = "GUID Boss is hidden";
          notifyIcon1.ShowBalloonTip(balloonTime);
        }
      }
    }

  }
}
