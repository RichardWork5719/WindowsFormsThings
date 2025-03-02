using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MessingWithWindowsForms
{
    public partial class FormLogoFadeInManager : Form
    {
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
                //this.TopMost = true;
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

        private void CreateActualAppWindow()
        {
            CUMApp f2 = new CUMApp();
            f2.ShowDialog();
        }

        private void StartLogoFadeIn()
        {
            FormLogoFadeIn f1 = new FormLogoFadeIn();
            f1.ShowDialog();
        }

        private void CheckIfCanCloseTick(System.Windows.Forms.Timer fadeTimer)
        {
            var f1 = Form.ActiveForm;
            
            if(f1 != null)
            {
                if (f1.Text == "CUM...")
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

        public FormLogoFadeInManager()
        {
            InitializeComponent();

            SetWindowName("CUM!!!");

            SetBackgroundColor(0, 0, 0);

            SetFullScreen(true);

            //Thread tr = new Thread(new ThreadStart(CreateActualAppWindow));
            //tr.Start();
            //
            //Thread tr1 = new Thread(new ThreadStart(StartLogoFadeIn));
            //tr1.Start();

            Opacity =  1;

            CheckIfCanClose();

            //Thread tr = new Thread(new ThreadStart(StartLogoFadeIn));
            //tr.Start();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            SetWindowName("CUM!!!");

            SetBackgroundColor(0, 0, 0);

            SetFullScreen(true);

            //Thread tr = new Thread(new ThreadStart(CreateActualAppWindow));
            //tr.Start();
            //
            //Thread tr1 = new Thread(new ThreadStart(StartLogoFadeIn));
            //tr1.Start();

            Opacity = 1;

            CheckIfCanClose();

            this.TopMost = true;

            Thread tr = new Thread(new ThreadStart(StartLogoFadeIn));
            tr.Start();

            this.TopMost = false;
        }
    }
}
