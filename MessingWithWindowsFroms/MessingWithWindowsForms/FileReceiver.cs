using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.IO.Pipes;

namespace MessingWithWindowsForms
{
    public partial class FileReceiver : Form
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

            /* The "Selected folder:" text */
            {
                Label selectFolderText = new Label();
                selectFolderText.Text = "Selected folder:";
                selectFolderText.Name = "SelectedFolderText";

                selectFolderText.ForeColor = defaultForeColor;
                selectFolderText.BackColor = defaultBackColor;

                selectFolderText.Font = defaultFont;
                selectFolderText.Location = new Point(xPadding, yPadding);
                selectFolderText.AutoSize = true;

                this.Controls.Add(selectFolderText);

                yPadding += selectFolderText.Height + extraPad;

                /* The input field for the file path */
                {
                    TextBox filePathInputField = new TextBox();
                    filePathInputField.Text = "Your/Mom/Gay";
                    filePathInputField.Name = "SelectedFolderPathInputField";

                    filePathInputField.Font = defaultFont;
                    filePathInputField.Location = new Point(xPadding, yPadding);
                    filePathInputField.Width = this.Width - (xPadding * 3); // the '3' is because the padding is already on one side and apparently you need the padding 2 more times on the other side for it to basically be nicely centered

                    this.Controls.Add(filePathInputField);

                    yPadding += filePathInputField.Height + extraPad;

                    /* The "Select folder" button */
                    {
                        Button selectFolderButton = CreateButton("SelectFolderButton", "Select folder", new Point(xPadding, yPadding), defaultForeColor, defaultBackColor);

                        selectFolderButton.Font = defaultFont;

                        selectFolderButton.Click += (s, e) => BrowseFolders();

                        yPadding += selectFolderButton.Height + (extraPad * 3);

                        /* The "Port number:" text */
                        {
                            Label portNumberText = new Label();
                            portNumberText.Text = "Port number:";
                            portNumberText.Name = "PortNumberTextText";

                            portNumberText.ForeColor = defaultForeColor;
                            portNumberText.BackColor = defaultBackColor;

                            portNumberText.Font = defaultFont;
                            portNumberText.Location = new Point(xPadding, yPadding);
                            portNumberText.AutoSize = true;

                            this.Controls.Add(portNumberText);

                            yPadding += portNumberText.Height + extraPad;

                            /* The port number input field */
                            {
                                TextBox portNumberInputField = new TextBox();
                                portNumberInputField.Text = "42069";
                                portNumberInputField.Name = "PortNumberInputField";

                                portNumberInputField.Font = defaultFont;
                                portNumberInputField.Location = new Point(xPadding, yPadding);
                                portNumberInputField.AutoSize = true;

                                this.Controls.Add(portNumberInputField);

                                yPadding += portNumberInputField.Height + (extraPad * 3);

                                /* The "Received file name" text */
                                {
                                    Label fileNameText = new Label();
                                    fileNameText.Text = "Received file name:";
                                    fileNameText.Name = "ReceivedFileNameTextText";

                                    fileNameText.ForeColor = defaultForeColor;
                                    fileNameText.BackColor = defaultBackColor;

                                    fileNameText.Font = defaultFont;
                                    fileNameText.Location = new Point(xPadding, yPadding);
                                    fileNameText.AutoSize = true;

                                    this.Controls.Add(fileNameText);

                                    yPadding += fileNameText.Height + extraPad;

                                    /* The name of the received file */
                                    {
                                        TextBox fileName = new TextBox();
                                        fileName.Text = "receivedFile.txt";
                                        fileName.Name = "ReceivedFileNameInputField";

                                        fileName.Font = defaultFont;
                                        fileName.Location = new Point(xPadding, yPadding);
                                        fileName.Width = filePathInputField.Width;

                                        this.Controls.Add(fileName);

                                        yPadding += fileName.Height + (extraPad * 3);

                                        /* The "Start receiving" button */
                                        {
                                            Button startReceivingButton = CreateButton("StartReceivingButton", "Start receiving", new Point(xPadding, yPadding), defaultForeColor, defaultBackColor);

                                            startReceivingButton.Font = defaultFont;

                                            startReceivingButton.Click += (s, e) => ReceiveFile();

                                            yPadding += startReceivingButton.Height + (extraPad * 2);

                                            /* The debug text */
                                            {
                                                Label receiveFileDebug = new Label();
                                                receiveFileDebug.Text = "";
                                                receiveFileDebug.Name = "ReceiveFileDebug";

                                                receiveFileDebug.ForeColor = defaultForeColor;
                                                receiveFileDebug.BackColor = defaultBackColor;

                                                receiveFileDebug.Font = defaultFont;
                                                receiveFileDebug.Location = new Point(xPadding, yPadding);
                                                receiveFileDebug.AutoSize = true;
                                                receiveFileDebug.Width = this.Width;
                                                receiveFileDebug.Height = this.Height;

                                                this.Controls.Add(receiveFileDebug);

                                                yPadding += receiveFileDebug.Height + extraPad;
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

        public FileReceiver()
        {
            InitializeComponent();
        }

        private void FileReceiver_Load(object sender, EventArgs e)
        {
            SetWindowName("CUM File Receiver");

            SetBackgroundColor(0, 0, 0);

            SetWindowSize(1920 / 2, 1080 / 2);

            SetupUI();
        }

        //==========================================================================================================================================================
        //======================================================BUTTON FUNTIONS SHIT================================================================================
        //==========================================================================================================================================================

        private void BrowseFolders()
        {
            var fd = new System.Windows.Forms.FolderBrowserDialog();

            if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var folderPath = fd.SelectedPath;

                var pathInputField = this.Controls.Find("SelectedFolderPathInputField", true)[0];
                pathInputField.Text = folderPath;
                Console.WriteLine(folderPath);
                WriteDebugText($"Folder selected: {folderPath}");
            }
        }

        private void ReceiveFile()
        {
            var timerStart = DateTime.Now;

            try
            {
                /*
                IPAddress ipAd = IPAddress.Parse("127.0.0.1");

                // Initializes the Listener 
                TcpListener myList = new TcpListener(ipAd, 1234);

                // Start Listeneting at the specified port 
                myList.Start();

                Console.WriteLine("The server is running at port 8001...");
                Console.WriteLine("The local End point is  :" + myList.LocalEndpoint);
                Console.WriteLine("Waiting for a connection.....");

                Socket s = myList.AcceptSocket();
                Console.WriteLine("Connection accepted from " + s.RemoteEndPoint);

                byte[] b = new byte[100];
                int k = s.Receive(b);
                Console.WriteLine("Recieved...");
                for (int i = 0; i < k; i++)
                    Console.Write(Convert.ToChar(b[i]));

                ASCIIEncoding asen = new ASCIIEncoding();
                s.Send(asen.GetBytes("The string was recieved by the server."));
                Console.WriteLine("\nSent Acknowledgement");

                s.Close();
                myList.Stop();
                */
                
                var portNum = Convert.ToInt32(this.Controls.Find("PortNumberInputField", true)[0].Text.Trim());
                var pathInputField = Path.Combine($"{this.Controls.Find("SelectedFolderPathInputField", true)[0].Text}");

                TcpListener server = new TcpListener(IPAddress.Loopback, portNum); // i hope i dont have to set the IP too IPAddress.Parse("127.0.0.1")
                server.Start();
                WriteDebugText($"Server started as: {server.Server.LocalEndPoint}");

                bool running = true;
                try
                {
                    while (running)
                    {
                        var client = server.AcceptTcpClient();
                        WriteDebugText($"Client connected: {client.Client.RemoteEndPoint}");
                        HandleClient(client, pathInputField);
                        //Thread th = new Thread(() =>
                        //{
                        //    HandleClient(client);
                        //});
                        //th.Start();
                        if (!client.Connected)
                        {
                            running = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    running = false;
                    server.Stop();
                }
            }
            catch(OperationAbortedException e)
            {
                WriteDebugText($"Full exception: \n{e}");
            }
            catch (Exception e)
            {
                //WriteDebugText("Error..... " + e.StackTrace);
                WriteDebugText($"Full exception: \n{e}");
            }

            var timerStop = DateTime.Now;
            var timerDifference = timerStop - timerStart;
            WriteDebugText($"\nTime taken: {timerDifference}");
        }

        //==========================================================================================================================================================
        //======================================================INNER BUTTON FUNCTION SHIT==========================================================================
        //==========================================================================================================================================================

        private void HandleClient(TcpClient client, string filePath)
        {
            using (client)
            {
                string fileName = this.Controls.Find("ReceivedFileNameInputField", true).First().Text;
                string defaultFileName = Path.Combine(filePath, fileName);

                var buffer = new byte[1024]; // default is 1024
                client.SendBufferSize = buffer.Length;

                var stream = client.GetStream();
                int bytesRead = 0;

                WriteDebugText($"Receiving data: "); //var file = File.WriteAllBytes(defaultFileName, );
                using (var fileStream = new FileStream(defaultFileName, FileMode.Create, FileAccess.Write)) // i THINK here is where it shits itself
                {
                    int totalBytesRead = 0;
                    while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0 && client.Connected)
                    {
                        //bytesRead = stream.Read(buffer, 0, buffer.Length); //ReadByte(); //Read(buffer, 0, buffer.Length);
                        fileStream.Write(buffer, 0, bytesRead); //fileStream.WriteByte((byte)bytesRead); //fileStream.Write(buffer, 0, bytesRead);
                        totalBytesRead += bytesRead;
                        WriteDebugText($"r: {BitConverter.ToString(buffer, 0, bytesRead)}"); // bytesRead}"); //
                    }
                    WriteDebugText($"Total received: {totalBytesRead}");
                }
            }

            WriteDebugText($"\nDone");
            client.Close();
        }

        //==========================================================================================================================================================
        //===========================================================DEBUG TEXT SHIT================================================================================
        //==========================================================================================================================================================

        private void WriteDebugText(string text)
        {
            int maxTextLength = 50;

            var debugText = this.Controls.Find("ReceiveFileDebug", true).First();

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
