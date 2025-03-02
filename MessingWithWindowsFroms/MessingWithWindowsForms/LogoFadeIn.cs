using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MessingWithWindowsForms
{
    public partial class FormLogoFadeIn : Form
    {
        int waitTime = 1000;
        int waited = 0;

        private void SetWindowName(string name = "CUM")
        {
            this.Text = name;
        }

        private void SetBackgroundColor(byte r, byte g, byte b)
        {
            this.BackColor = Color.FromArgb(r, g, b);
        }

        private void SetFullScreen(bool properFullScreen = false)
        {
            if (properFullScreen)
            {
                this.TopMost = true;
                this.FormBorderStyle = FormBorderStyle.None;
            }

            this.WindowState = FormWindowState.Maximized;
        }

        private void ToggleProperFullScreen()
        {
            this.TopMost = !this.TopMost;

            if (this.FormBorderStyle == FormBorderStyle.None)
            {
                this.FormBorderStyle = FormBorderStyle.Sizable;
            }
            else
            {
                this.FormBorderStyle = FormBorderStyle.None;
            }
        }

        private void LostFocusToggleFS()
        {
            if (this.FormBorderStyle == FormBorderStyle.None)
            {
                ToggleProperFullScreen();
            }
        }

        private void UpdatePictureBoxSize(PictureBox pb, int padding)
        {
            pb.Size = new Size(this.ClientSize.Width - padding * 2, this.ClientSize.Height - padding * 2);
            pb.Location = new Point(padding, padding);
        }

        private void ShowLogoInCenterOfScreen(int padding = 25)
        {
            PictureBox pb1 = new PictureBox();
            pb1.ImageLocation = "images\\crowbar_gang_logo_white2.png";
            pb1.Image = Image.FromFile(pb1.ImageLocation); //Console.WriteLine(pb1.Image.Size);

            UpdatePictureBoxSize(pb1, padding); // Set the PictureBox size and location
            pb1.SizeMode = PictureBoxSizeMode.Zoom; // Maintain aspect ratio while fitting

            this.Controls.Add(pb1); // Add the PictureBox to the form's controls

            this.Resize += (s, e) => UpdatePictureBoxSize(pb1, padding); // Subscribe to the Resize event
        }

        /*
        private bool FadeToBackgroundColorTick(byte r, byte g, byte b, byte speed = 1)
        {
            byte currentR = this.BackColor.R;
            byte currentG = this.BackColor.G;
            byte currentB = this.BackColor.B;

            if (currentR > r)
            {
                try
                {
                    currentR -= (byte)speed;
                }
                catch
                {
                    currentR = 0;
                }
            }
            else if (currentR < r)
            {
                try
                {
                    currentR += (byte)speed;
                }
                catch
                {
                    currentR = byte.MaxValue;
                }
            }

            if (currentG > g)
            {
                try
                {
                    currentG -= (byte)speed;
                }
                catch
                {
                    currentG = 0;
                }
            }
            else if (currentG < g)
            {
                try
                {
                    currentG += (byte)speed;
                }
                catch
                {
                    currentG = byte.MaxValue;
                }
            }

            if (currentB > b)
            {
                try
                {
                    currentB -= (byte)speed;
                }
                catch
                {
                    currentB = 0;
                }
            }
            else if (currentB < b)
            {
                try
                {
                    currentB += (byte)speed;
                }
                catch
                {
                    currentB = byte.MaxValue;
                }
            }

            if (currentR == r && currentG == g && currentB == b)
            {
                return true;
            }

            SetBackgroundColor(currentR, currentG, currentB);
            return false;
        }

        private void FadeToBackgroundColor(byte r, byte g, byte b, byte speed = 1)
        {
            System.Windows.Forms.Timer fadeTimer = new System.Windows.Forms.Timer();
            fadeTimer.Interval = speed; // Adjust the interval for speed of fade
            fadeTimer.Tick += (s, e) => FadeToBackgroundColorTick(r, g, b);
            fadeTimer.Start();
        }

        private void UpdateFadeInPanelLocation(Panel overlayPanel)
        {
            overlayPanel.Size = new Size(this.ClientSize.Width, this.ClientSize.Height);
            overlayPanel.Location = new Point(0,0);
        }
        */

        private void CheckIfCanCloseTick(System.Windows.Forms.Timer fadeTimer)
        {
            var f1 = Form.ActiveForm;

            if (f1 != null)
            {
                if (f1.Text == "CUM")
                {
                    if (f1.Opacity >= 0.99f)
                    {
                        fadeTimer.Stop();
                        this.Close();
                    }
                }
            }
        }
        private void CheckIfCanClose()
        {
            System.Windows.Forms.Timer fadeTimer = new System.Windows.Forms.Timer();
            fadeTimer.Interval = 1; // Adjust the interval for speed of fade
            fadeTimer.Tick += (s, e) => CheckIfCanCloseTick(fadeTimer);
            fadeTimer.Start();
        }

        private void FadeInWindowTick(System.Windows.Forms.Timer timer, float speed = 0.0025f)
        {
            if (Opacity >= 1)
            {
                timer.Stop();
                FadeOutWindow();
                this.TopMost = !this.TopMost;
                CheckIfCanClose();
                //CUMApp f2 = new CUMApp(); //this is the change, code for redirect 
                //f2.ShowDialog();
                //Application.Run(new CUMApp()); // doesnt work
            }
            else
            {
                Opacity += speed;
            }
            Console.WriteLine($"Window opacity: {Opacity}");
        }

        private void FadeInWindow(float speed = 0.0025f, bool startFromZero = true)
        {
            if (startFromZero)
            {
                Opacity = 0;
            }
            System.Windows.Forms.Timer fadeTimer = new System.Windows.Forms.Timer();
            fadeTimer.Interval = 1; // Adjust the interval for speed of fade
            fadeTimer.Tick += (s, e) => FadeInWindowTick(fadeTimer, speed);
            fadeTimer.Start();
        }

        private void FadeOutWindowTick(System.Windows.Forms.Timer timer, float speed = 0.0025f)
        {
            if (Opacity <= 0)
            {
                timer.Stop();
                this.Hide();
                this.Close();
            }
            else
            {
                Opacity -= speed;
            }
            Console.WriteLine($"Window opacity: {Opacity}");
        }

        private void FadeOutWindow(float speed = 0.0025f, bool startFromOne = true)
        {
            if (startFromOne)
            {
                Opacity = 1;
            }
            System.Windows.Forms.Timer fadeTimer = new System.Windows.Forms.Timer();
            fadeTimer.Interval = 1; // Adjust the interval for speed of fade
            fadeTimer.Tick += (s, e) => FadeOutWindowTick(fadeTimer, speed);
            fadeTimer.Start();
        }

        public FormLogoFadeIn()
        {
            InitializeComponent();

            SetWindowName("CUM...");
            
            SetBackgroundColor(0, 0, 0);

            SetFullScreen();
            ToggleProperFullScreen();

            ShowLogoInCenterOfScreen();
            
            FadeInWindow();
            //FadeToBackgroundColor(0, 0, 0); // (255, 127, 0) <- very nicely saturated orange lol

            //this.LostFocus += (s, e) => LostFocusToggleFS();
        }
    }
}
