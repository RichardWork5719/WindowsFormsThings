using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MessingWithWindowsForms
{
    class LogoFadeInMk1
    {
        /*
        private void SetWindowName(string name = "CUM")
        {
            this.Text = name;
        }

        private void SetBackgroundColor(byte r, byte g, byte b)
        {
            this.BackColor = Color.FromArgb(r, g, b);
        }

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

        public Form1()
        {
            InitializeComponent();

            SetWindowName();

            SetBackgroundColor(255, 255, 255);
            FadeToBackgroundColor(0, 0, 0); // (255, 127, 0) <- very nicely saturated orange lol

            SetFullScreen();
            ToggleProperFullScreen();

            ShowLogoInCenterOfScreen();

            this.LostFocus += (s, e) => LostFocusToggleFS();
        }
        */
    }
}
