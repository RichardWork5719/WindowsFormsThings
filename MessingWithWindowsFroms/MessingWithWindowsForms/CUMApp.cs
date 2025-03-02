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
    public partial class CUMApp : Form
    {
        //==========================================================================================================================================================
        //======================================================STARTUP SHIT========================================================================================
        //==========================================================================================================================================================

        private void SetWindowName(string name = "CUM")
        {
            this.Text = name;
            this.Name = name;
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

            this.WindowState = FormWindowState.Maximized;
        }

        private void FadeInWindowTick(System.Windows.Forms.Timer timer, float speed = 0.0025f)
        {
            if (Opacity >= 1)
            {
                timer.Stop();
                this.Show();

                //if (this.FormBorderStyle == FormBorderStyle.None)
                //{
                //    this.FormBorderStyle = FormBorderStyle.Sizable;
                //}
                //else
                //{
                //    this.FormBorderStyle = FormBorderStyle.None;
                //}

                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                Opacity += speed;
            }
            Console.WriteLine($"Window opacity: {Opacity}");
        }

        private void FadeIn(float speed = 0.0025f, bool startAtZero = true)
        {
            if(startAtZero)
            {
                Opacity = 0;
                this.Hide();
            }

            System.Windows.Forms.Timer fadeTimer = new System.Windows.Forms.Timer();
            fadeTimer.Interval = 1; // Adjust the interval for speed of fade
            fadeTimer.Tick += (s, e) => FadeInWindowTick(fadeTimer, speed);
            fadeTimer.Start();
        }

        private void RunLogoFadeIn()
        {
            FormLogoFadeInManager logoFadeInManager = new FormLogoFadeInManager();
            logoFadeInManager.ShowDialog();
        }

        private void WelcomeScreen()
        {
            ClearScreen();

            int padding = 20;

            /* CUM Name Text */
            {
                Label label = new Label();
                label.Text = "Crowbar Universal Media";
                label.Name = "MainText";

                label.ForeColor = Color.FromArgb(255, 255, 255);
                label.BackColor = Color.FromArgb(0, 0, 0, 0);

                label.Font = new Font("Arial", this.ClientSize.Height / 10, FontStyle.Bold);
                label.Size = new Size(this.ClientSize.Width - padding * 2, this.ClientSize.Height - padding * 2);
                label.Location = new Point(padding, padding);
                label.AutoSize = true;

                //var welcomeButton = this.Controls.Find("WelcomeTab", true)[0]; // just get the first button with the name "WelcomeTab"
                //label.Location = new Point(welcomeButton.Location.X, welcomeButton.Location.Y + (welcomeButton.Height * 3));

                this.Controls.Add(label);
            }

            /* CUM Credit Text */
            {
                Label creditText = new Label();
                creditText.Text = "by: sharptile";
                creditText.Name = "CreditText";

                creditText.ForeColor = Color.FromArgb(255, 255, 255);
                creditText.BackColor = Color.FromArgb(0, 0, 0, 0);

                creditText.Font = new Font("Arial", this.ClientSize.Height / 100, FontStyle.Bold);
                creditText.Size = new Size(this.ClientSize.Width - padding * 2, this.ClientSize.Height - padding * 2);

                var mainText = this.Controls.Find("MainText", true)[0]; // just get the first button with the name "WelomeText"
                creditText.Location = new Point(padding, mainText.Height + padding);
                creditText.AutoSize = true;

                this.Controls.Add(creditText);
            }

            /* CUM Credit Text */
            {
                Label extraText = new Label();
                extraText.Text = "Made for CUM, made by CUM, made of CUM, made from CUM, made to CUM, BORN TO CUM";
                extraText.Name = "ExtraText";

                extraText.ForeColor = Color.FromArgb(255, 255, 255);
                extraText.BackColor = Color.FromArgb(0, 0, 0, 0);

                extraText.Font = new Font("Arial", this.ClientSize.Height / 70, FontStyle.Bold);
                extraText.Size = new Size(this.ClientSize.Width - padding * 2, this.ClientSize.Height - padding * 2);

                var creditText = this.Controls.Find("CreditText", true)[0]; // just get the first button with the name "WelomeText"
                extraText.Location = new Point(padding, creditText.Location.Y + creditText.Height + padding);
                extraText.AutoSize = true;

                this.Controls.Add(extraText);
            }

            //var welcomeButton = this.Controls.Find("WelcomeTab", true)[0]; // just get the first button with the name "WelcomeTab"
            //welcomeButton.GotFocus -= (s, e) => WelcomeScreen();
        }

        private void ShowWelcomeScreenTick(System.Windows.Forms.Timer timer)
        {
            if (Opacity >= 1)
            {
                timer.Stop();
                WelcomeScreen();
            }
        }

        private void ShowWelcomeScreen(int speed = 1)
        {
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = speed; // Adjust the interval for speed of fade
            timer.Tick += (s, e) => ShowWelcomeScreenTick(timer);
            timer.Start();
        }

        private void InitielizeApp()
        {
            SetWindowName();

            SetBackgroundColor(0, 0, 0);

            SetFullScreen();

            FadeIn(startAtZero: true);

            //Application.OpenForms;

            Thread tr = new Thread(new ThreadStart(RunLogoFadeIn));
            tr.Start();

            ShowOptionButtons();

            ShowWelcomeScreen();
        }

        //==========================================================================================================================================================
        //======================================================MAIN SHIT===========================================================================================
        //==========================================================================================================================================================

        private void ClearScreen()
        {
            //this.Controls.Clear();
            foreach (Control control in this.Controls)
            {
                var allButtons = this.Controls.OfType<Button>();

                foreach (var button in allButtons)
                {
                    if (!button.Name.EndsWith("Tab"))
                    {
                        button.Enabled = false;
                        this.Controls.Remove(button);
                        button.Dispose();
                    }
                }

                var allLabels = this.Controls.OfType<Label>();

                foreach (var item in allLabels)
                {
                    item.Enabled = false;
                    this.Controls.Remove(item);
                    item.Dispose();
                }

                //var allThings = this.Controls.OfType<Object>();
                //
                //foreach (var item in allThings)
                //{
                //    item.Enabled = false;
                //    this.Controls.Remove(item);
                //    item.Dispose();
                //}
            }
        }
        
        /* Button Creator with some standards so i dont have to keep writing the same code every time */
        private Button CreateButton(string name, string text, Point location, Color textColor, Color backgroundColor, bool autosize = true) 
        {
            Button myButton0 = new Button();

            myButton0.Location = location;

            myButton0.Text = text;
            myButton0.Name = name;

            myButton0.AutoSize = autosize;

            myButton0.ForeColor = textColor; // text color
            myButton0.BackColor = backgroundColor; // background color

            this.Controls.Add(myButton0);

            myButton0.GotFocus += (s, e) =>
            {
                myButton0.ForeColor = Color.FromArgb(255 - textColor.R, 255 - textColor.G, 255 - textColor.B);
                myButton0.BackColor = Color.FromArgb(255 - backgroundColor.R, 255 - backgroundColor.G, 255 - backgroundColor.B);
            };
            myButton0.LostFocus += (s, e) =>
            {
                myButton0.ForeColor = textColor; // text color
                myButton0.BackColor = backgroundColor; // background color
            };

            return myButton0;
        }

        private void ShowOptionButtons()
        {
            /* Top Tab Window */
            string[] buttons = { "Welcome", "FileSender", "Aids", "Aids2", "Aids3" };
            Point point = new Point(0, 0);

            foreach (string buttonName in buttons)
            {
                Button myButton0 = CreateButton(buttonName + "Tab", buttonName, point, Color.FromArgb(0, 0, 0), Color.FromArgb(255, 255, 255));

                int tempPoint = point.X;
                point = new Point(tempPoint + myButton0.Width, 0);

                /* Setup the button Events */
                if (myButton0.Name == "WelcomeTab")
                {
                    myButton0.Click += (s, e) => WelcomeScreen();
                    //myButton0.GotFocus += (s, e) => WelcomeScreen();
                }
                else if (myButton0.Name == "FileSenderTab")
                {
                    myButton0.Click += (s, e) => SetupFileSender();
                }
            }
        }

        public CUMApp()
        {
            InitializeComponent();

            InitielizeApp();
        }

        //==========================================================================================================================================================
        //======================================================FILE SENDER SHIT====================================================================================
        //==========================================================================================================================================================

        /* Doing it from top to bottom now cuz i cant be fucked to keep doing gaps n shit */
        private void SetupFileSender()
        {
            ClearScreen();

            var welcomeButton = this.Controls.Find("WelcomeTab", true)[0]; // just get the first button with the name "Welcome"

            Button sendButton = CreateButton("FileSenderSendTabButton", "Send", new Point(welcomeButton.Location.X, welcomeButton.Location.Y + (welcomeButton.Height * 3)), Color.FromArgb(0, 0, 0), Color.FromArgb(255, 255, 255));
            Button receiveButton = CreateButton("FileSenderReceiveTabButton", "Receive", new Point(sendButton.Location.X + sendButton.Width, sendButton.Location.Y), Color.FromArgb(0, 0, 0), Color.FromArgb(255, 255, 255));

            sendButton.Click += (s, e) => StartFileSender();
            receiveButton.Click += (s, e) => StartFileReceiver();
        }

        private void StartFileSender()
        {
            Thread th = new Thread(() =>
            {
                FileSender fileSender = new FileSender();
                fileSender.ShowDialog();
            });
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }

        private void StartFileReceiver()
        {
            Thread th = new Thread(() =>
            {
                FileReceiver fileReceiver = new FileReceiver();
                fileReceiver.ShowDialog();
            });
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }
    }
}
