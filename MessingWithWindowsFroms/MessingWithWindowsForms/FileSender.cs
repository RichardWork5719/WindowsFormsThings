using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace MessingWithWindowsForms
{
    public partial class FileSender : Form
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

        private void SetWindowSize(int x, int y)
        {
            this.Width = x;
            this.Height = y;
        }

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

        private void SetupUI()
        {
            int xPadding = 20;
            int yPadding = 20;
            int extraPad = 10;

            var defaultForeColor = Color.FromArgb(255, 255, 255);
            var defaultBackColor = Color.FromArgb(0, 0, 0);
            var defaultFont = new Font("Arial", Font.Size + 3, FontStyle.Bold);

            /* The "Selected file:" text */
            {
                Label selectFileText = new Label();
                selectFileText.Text = "Selected file:";
                selectFileText.Name = "SelectedFileText";

                selectFileText.ForeColor = defaultForeColor;
                selectFileText.BackColor = defaultBackColor;

                selectFileText.Font = defaultFont;
                selectFileText.Location = new Point(xPadding, yPadding);
                selectFileText.AutoSize = true;

                this.Controls.Add(selectFileText);

                yPadding += selectFileText.Height + extraPad;

                /* The input field for the file path */
                {
                    TextBox filePathInputField = new TextBox();
                    filePathInputField.Text = "Your/Mom/Gay.txt";
                    filePathInputField.Name = "SelectedFilePathInputField";

                    filePathInputField.Font = defaultFont;
                    filePathInputField.Location = new Point(xPadding, yPadding);
                    filePathInputField.Width = this.Width - (xPadding * 3); // the '3' is because the padding is already on one side and apparently you need the padding 2 more times on the other side for it to basically be nicely centered

                    this.Controls.Add(filePathInputField);

                    yPadding += filePathInputField.Height + extraPad;

                    /* The "Select file" button */
                    {
                        Button selectFileButton = CreateButton("SelectFileButton", "Select file", new Point(xPadding, yPadding), defaultForeColor, defaultBackColor);

                        selectFileButton.Font = defaultFont;

                        selectFileButton.Click += (s, e) => BrowseFiles();

                        yPadding += selectFileButton.Height + (extraPad * 3); // doing * 3 because the "Select file" section is finished

                        /* The "Target IP" text */
                        {
                            Label sendToIpText = new Label();
                            sendToIpText.Text = "Target IP:";
                            sendToIpText.Name = "SendToIpText";

                            sendToIpText.ForeColor = defaultForeColor;
                            sendToIpText.BackColor = defaultBackColor;

                            sendToIpText.Font = defaultFont;
                            sendToIpText.Location = new Point(xPadding, yPadding);
                            sendToIpText.AutoSize = true;

                            this.Controls.Add(sendToIpText);

                            yPadding += sendToIpText.Height + extraPad;

                            /* The input field for the target IP */
                            {
                                TextBox targetIpInputField = new TextBox();
                                targetIpInputField.Text = "127.0.0.1";
                                targetIpInputField.Name = "TargetIpInputField";

                                targetIpInputField.Font = defaultFont;
                                targetIpInputField.Location = new Point(xPadding, yPadding);
                                targetIpInputField.Width = this.Width / 8; // the 8 is to make is nice and small since the max IP size is not that big

                                this.Controls.Add(targetIpInputField);

                                yPadding += targetIpInputField.Height + (extraPad * 3);

                                /* The "Target port" text */
                                {
                                    Label sendToPortText = new Label();
                                    sendToPortText.Text = "Target port:";
                                    sendToPortText.Name = "SendToPortText";

                                    sendToPortText.ForeColor = defaultForeColor;
                                    sendToPortText.BackColor = defaultBackColor;

                                    sendToPortText.Font = defaultFont;
                                    sendToPortText.Location = new Point(xPadding, yPadding);
                                    sendToPortText.AutoSize = true;

                                    this.Controls.Add(sendToPortText);

                                    yPadding += sendToPortText.Height + extraPad;

                                    /* The input field for the target IP */
                                    {
                                        TextBox targetPortInputField = new TextBox();
                                        targetPortInputField.Text = "42069";
                                        targetPortInputField.Name = "TargetPortInputField";

                                        targetPortInputField.Font = defaultFont;
                                        targetPortInputField.Location = new Point(xPadding, yPadding);
                                        targetPortInputField.Width = this.Width / 8; // the 8 is to make is nice and small since the max IP size is not that big

                                        this.Controls.Add(targetPortInputField);

                                        yPadding += targetPortInputField.Height + (extraPad * 3);

                                        
                                        /* The "Send file" button */
                                        {
                                            Button sendFileButton = CreateButton("SendFileButton", "Send file", new Point(xPadding, yPadding), defaultForeColor, defaultBackColor);

                                            sendFileButton.Font = defaultFont;

                                            sendFileButton.Click += (s, e) => SendFile();

                                            yPadding += sendToPortText.Height + (extraPad * 2);

                                            /* The debug text */
                                            {
                                                Label sendFileDebug = new Label();
                                                sendFileDebug.Text = "";
                                                sendFileDebug.Name = "SendFileDebug";

                                                sendFileDebug.ForeColor = defaultForeColor;
                                                sendFileDebug.BackColor = defaultBackColor;

                                                sendFileDebug.Font = defaultFont;
                                                sendFileDebug.Location = new Point(xPadding, yPadding);
                                                sendFileDebug.AutoSize = true;

                                                this.Controls.Add(sendFileDebug);

                                                yPadding += sendFileDebug.Height + extraPad;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        //==========================================================================================================================================================
        //======================================================MAIN SHIT===========================================================================================
        //==========================================================================================================================================================

        public FileSender()
        {
            InitializeComponent();
        }

        private void FileSender_Load(object sender, EventArgs e)
        {
            SetWindowName("CUM File Sender");

            SetBackgroundColor(0, 0, 0);

            SetWindowSize(1920 / 2, 1080 / 2);

            SetupUI();
        }

        //==========================================================================================================================================================
        //======================================================BUTTON FUNTIONS SHIT================================================================================
        //==========================================================================================================================================================

        private void BrowseFiles()
        {
            var fd = new System.Windows.Forms.OpenFileDialog();

            if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string fileToOpen = fd.FileName;

                System.IO.FileInfo file = new System.IO.FileInfo(fd.FileName);

                var pathInputField = this.Controls.Find("SelectedFilePathInputField", true)[0];
                pathInputField.Text = file.FullName;
                Console.WriteLine(file.FullName);
                WriteDebugText($"File selected: {file.FullName}");
            }
        }

        private void SendFile()
        {
            try
            {
                var targetIP = this.Controls.Find("TargetIpInputField", true)[0].Text;
                var targetPort = int.Parse(this.Controls.Find("TargetPortInputField", true)[0].Text);
                var fileToSend = Path.Combine(this.Controls.Find("SelectedFilePathInputField", true)[0].Text);

                TcpClient client = new TcpClient(targetIP, targetPort); // Connect to the server
                WriteDebugText($"Connecting to: {targetIP}:{targetPort}");

                var bufferSize = client.ReceiveBufferSize;
                var buffer = new byte[bufferSize]; // default is 1024

                NetworkStream stream = client.GetStream();
                WriteDebugText($"Connected to: {targetIP}:{targetPort}");
                
                var fileSize = new System.IO.FileInfo(fileToSend).Length;
                WriteDebugText($"Sending data:");
                using (BinaryReader reader = new BinaryReader(new FileStream(fileToSend, FileMode.Open)))
                {
                    //reader.Read(buffer, buffer.Length, buffer.Length);
                    //stream.Write(buffer, 0, buffer.Length);
                    
                    for (int i = 0; i < fileSize; i++)
                    {
                        var byteRead = reader.ReadByte();
                        WriteDebugText($"s: {byteRead}"); //{BitConverter.ToString(buffer, 0, byteRead)}");
                        stream.WriteByte(byteRead);
                    }
                    reader.Close();
                }
                stream.Close();
                client.Close();

                //var data = File.ReadAllBytes(fileToSend);
                //foreach (var b in data)
                //{
                //    WriteDebugText($"{b.ToString()} ");
                //    //stream.WriteByte(b);
                //    //WriteDebugText($"{data.ToList<byte>().ToString()}");
                //}
                //stream.Write(data, 0, data.Length);
            }
            catch (Exception e)
            {
                WriteDebugText("Error..... " + e.StackTrace);
                WriteDebugText($"Full exception: {e}");
            }
        }

        //==========================================================================================================================================================
        //===========================================================DEBUG TEXT SHIT================================================================================
        //==========================================================================================================================================================

        private void WriteDebugText(string text)
        {
            int maxTextLength = 50;

            var debugText = this.Controls.Find("SendFileDebug", true).First();

            if (debugText.Text.Length > maxTextLength)
            {
                debugText.Text = text;
                Console.WriteLine(text);
            }
            else
            {
                debugText.Text += text;
                Console.WriteLine(text);
            }
        }
    }
}
