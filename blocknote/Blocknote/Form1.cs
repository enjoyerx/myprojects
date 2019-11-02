using Crypto;
using System;
using System.IO;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blocknote
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private TcpClient client;
        private NetworkStream ns;
        private byte[] sessionAESKey;
        private byte[] sessionAESIV;

        private byte[] masterAESKey;
        private byte[] masterAESIV;

        private Connection connection;
        private RSAKeyPair keyPair;

        private void Form1_Load(object sender, EventArgs e)
        {
            var masterKeyForm = new MasterKey();
            masterKeyForm.ShowDialog();
            GetMasterKey(masterKeyForm.Result);


            if (File.Exists("RSAKey.bin"))
            {
                var serialize = Encoding.UTF8.GetString(AES.Decrypt(File.ReadAllBytes("RSAKey.bin"), masterAESKey, masterAESIV));
                keyPair = new RSAKeyPair(
                    Serializer.DeserializeKey(serialize.Split('$')[0])
                );
                generateRSAButton.Enabled = false;
                getSessionKeyButton.Enabled = true;
            }
            else
            {
                generateRSAButton.Enabled = true;
                getSessionKeyButton.Enabled = false;
            }

            loginRejected.Visible = false;
            toggleLoginForm(false);

        }

        private void ReceiveResponseFromServerAsync()
        {
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    int messageType;
                    try
                    {
                        messageType = BitConverter.ToInt32(connection.Receive(4), 0);
                    }
                    catch (IOException e)
                    {
                        Invoke((MethodInvoker)delegate
                        {
                            toggleLoginForm(false);
                            blocknote.Visible = false;
                            getSessionKeyButton.Enabled = true;
                            serverResponses.Visible = true;
                            serverResponses.Text = "session has been expired";
                        });
                        
                        break;
                    }

                    var lenBytes = BitConverter.ToInt32(connection.Receive(4), 0);
                    Invoke((MethodInvoker)delegate
                    {
                        serverResponses.Visible = false;
                    });

                    byte[] msg = connection.Receive(lenBytes);

                    if (messageType == TCPConnection.ENCRYPTED_AES_WITH_RSA)
                    {
                        sessionAESKey = new byte[128];
                        Array.Copy(msg, 0, sessionAESKey, 0, 128);
                        sessionAESKey = keyPair.Decrypt(sessionAESKey);

                        sessionAESIV = new byte[128];
                        Array.Copy(msg, 128, sessionAESIV, 0, 128);
                        sessionAESIV = keyPair.Decrypt(sessionAESIV);

                        Invoke((MethodInvoker)delegate
                        {
                            getSessionKeyButton.Enabled = false;
                        });
                    }
                    else if (messageType == TCPConnection.LOGIN_APPROVED)
                    {
                        Invoke((MethodInvoker)delegate
                        {
                            toggleLoginForm(false);
                            loginRejected.Visible = false;
                            blocknote.Visible = true;
                        });
                    }
                    else if (messageType == TCPConnection.LOGIN_REJECTED)
                    {
                        Invoke((MethodInvoker)delegate
                        {
                            toggleLoginForm(true);
                            loginRejected.Visible = true;
                        });
                    }
                    else if (messageType == TCPConnection.TEXT)
                    {
                        var text = Encoding.UTF8.GetString(AES.Decrypt(msg, sessionAESKey, sessionAESIV));
                        Invoke((MethodInvoker)delegate
                        {
                            textBox1.Text = text;
                        });
                    }
                    else if (messageType == TCPConnection.FILE_DO_NOT_EXISTS)
                    {
                        Invoke((MethodInvoker)delegate
                        {
                            serverResponses.Visible = true;
                            serverResponses.Text = "file with such a filename does not exist";
                        });
                    }
                    else if (messageType == TCPConnection.FILE_SAVED)
                    {
                        Invoke((MethodInvoker)delegate
                        {
                            serverResponses.Visible = true;
                            serverResponses.Text = "file was saved";
                        });
                    }
                    else
                    {
                        break;
                    }
                }
            });
        }

        private void SendMessage()
        {
            string somethingToSay = "hello from client";
            byte[] buffer = Encoding.UTF8.GetBytes(somethingToSay);
            connection.Send(TCPConnection.PUBLIC_KEY, buffer);
        }

        private void getSessionKeyButton_Click(object sender, EventArgs e)
        {
            getSessionKeyButton.Enabled = false;

            client = new TcpClient();
            client.Connect("127.0.0.1", 9999);
            connection = new Connection(client, keyPair);

            connection.SendPublicKeyToServer();
            ReceiveResponseFromServerAsync();

            connection.Send(TCPConnection.GET_SESSION_KEY, null);
            getSessionKeyButton.Enabled = false;

            toggleLoginForm(true);
        }

        private void generateRSAButton_Click(object sender, EventArgs e)
        {
            getSessionKeyButton.Enabled = true;
            generateRSAButton.Enabled = false;

            keyPair = new RSAKeyPair();
            var serialized = Serializer.SerializeKey(keyPair.privateKey) + "$";
            var keyPairEncrypted = AES.Encrypt(Encoding.UTF8.GetBytes(serialized), masterAESKey, masterAESIV);
            File.WriteAllBytes("RSAKey.bin", keyPairEncrypted);
        }



        private void toggleLoginForm(bool toggle)
        {
            labelLogin.Visible = toggle;
            labelPassword.Visible = toggle;

            loginButton.Visible = toggle;

            textBoxLogin.Visible = toggle;
            textBoxPassword.Visible = toggle;
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            string login = textBoxLogin.Text;
            string password = textBoxPassword.Text;
            SendLogin(client, login, password);
        }

        private void SendLogin(TcpClient client, string login, string password)
        {
            login = textBoxLogin.Text;
            password = textBoxPassword.Text;
            byte[] loginEcnr = AES.Encrypt(Encoding.Default.GetBytes(login), sessionAESKey, sessionAESIV);
            byte[] passwordEcnr = AES.Encrypt(Encoding.Default.GetBytes(password), sessionAESKey, sessionAESIV);
            var loginLen = BitConverter.GetBytes(loginEcnr.Length);

            var msg = new byte[4 + loginEcnr.Length + passwordEcnr.Length];

            Array.Copy(loginLen, 0, msg, 0, 4);
            Array.Copy(loginEcnr, 0, msg, 4, loginEcnr.Length);
            Array.Copy(passwordEcnr, 0, msg, 4 + loginEcnr.Length, passwordEcnr.Length);

            connection.Send(TCPConnection.LOGIN, msg);
        }

        private void getTextButton_Click(object sender, EventArgs e)
        {
            var filename = filenameBox.Text;
            filename = "first.txt";
            var filenameArray = AES.Encrypt(Encoding.UTF8.GetBytes(filename), sessionAESKey, sessionAESIV);
            if (filename.Length != 0)
            {
                connection.Send(TCPConnection.FILENAME, filenameArray);
            }
        }

        private void GetMasterKey(string password)
        {
            byte[] salt;
            if (!File.Exists("salt.bin"))
            {
                new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
                File.WriteAllBytes("salt.bin", salt);
            }
            salt = File.ReadAllBytes("salt.bin");
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            masterAESKey = pbkdf2.GetBytes(32);
            masterAESIV = pbkdf2.GetBytes(16);
        }

        private void sendTextButton_Click(object sender, EventArgs e)
        {
            if (filenameBox.Text.Length != 0)
            {
                var text = AES.Encrypt(Encoding.Default.GetBytes(textBox1.Text), sessionAESKey, sessionAESIV);
                var filename = AES.Encrypt(Encoding.Default.GetBytes(filenameBox.Text), sessionAESKey, sessionAESIV);
                var filenameLength = BitConverter.GetBytes(filename.Length);

                var msg = new byte[4 + filename.Length + text.Length];
                Array.Copy(filenameLength, 0, msg, 0, 4);
                Array.Copy(filename, 0, msg, 4, filename.Length);
                Array.Copy(text, 0, msg, 4 + filename.Length, text.Length);

                connection.Send(TCPConnection.TEXT, msg);
            }

        }
    }

}
