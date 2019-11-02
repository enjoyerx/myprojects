using Blocknote;
using Crypto;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        private static TcpListener server;
        private static NetworkStream ns;
        private static TcpClient client;

        private static RSACryptoServiceProvider GetPublicKeyFromClient(TcpClient client, int len)
        {
            var msg = Receive(client, len);
            RSAParameters publicKey = Serializer.DeserializeKey(Encoding.Default.GetString(msg));
            var rsa = new RSACryptoServiceProvider();
            rsa.ImportParameters(publicKey);

            return rsa;
        }

        public static byte[] Receive(TcpClient client, int length)
        {
            Console.Write("Read: " + length);
            var message = new byte[length];
            int read = 0;
            while (read < message.Length)
            {
                read += client.GetStream().Read(message, read, message.Length - read);
            }

            return message;
        }

        public static void Send(TcpClient client, int messageType, byte[] msg)
        {
            if (msg == null)
            {
                client.GetStream().Write(BitConverter.GetBytes(messageType), 0, 4);
                client.GetStream().Write(BitConverter.GetBytes(0), 0, 4);
            }
            else
            {
                client.GetStream().Write(BitConverter.GetBytes(messageType), 0, 4);
                client.GetStream().Write(BitConverter.GetBytes(msg.Length), 0, 4);
                client.GetStream().Write(msg, 0, msg.Length);
            }
        }


        public static AES GenerateSessionKey()
        {
            AES aes = new AES();
            aes.GenerateSessionKey();
            return aes;
        }

        public static void SendEcryptedSessionKey(AES aes, RSACryptoServiceProvider rsa)
        {
            var encr = rsa.Encrypt(aes.rijndaelManaged.Key, false);
            var encrIV = rsa.Encrypt(aes.rijndaelManaged.IV, false);

            var msg = new byte[encr.Length + encrIV.Length];
            Array.Copy(encr, 0, msg, 0, encr.Length);
            Array.Copy(encrIV, 0, msg, encr.Length, encrIV.Length);

            Send(client, TCPConnection.ENCRYPTED_AES_WITH_RSA, msg);
        }

        public static string getHash(string password)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            return Convert.ToBase64String(hashBytes);

        }

        public static bool checkUser(string login, string password)
        {
            var json = System.IO.File.ReadAllText(@"users.json");
            var storage = JsonConvert.DeserializeObject<Storage>(json);

            if (storage.Users.ContainsKey(login) && storage.Users[login] != null)
            {
                /* Fetch the stored value */
                var savedHash = storage.Users[login];
                /* Extract the bytes */
                byte[] hashBytes = Convert.FromBase64String(savedHash);
                /* Get the salt */
                byte[] salt = new byte[16];
                Array.Copy(hashBytes, 0, salt, 0, 16);
                /* Compute the hash on the password the user entered */
                var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
                byte[] hash = pbkdf2.GetBytes(20);
                /* Compare the results */
                for (int i = 0; i < 20; i++)
                    if (hashBytes[i + 16] != hash[i])
                        return false;
            }
            else
            {
                return false;
            }
            return true;
        }

        static void Main(string[] args)
        {
            server = new TcpListener(IPAddress.Any, 9999);
            server.Start();
            while (true)
            {
                client = server.AcceptTcpClient();
                object my_lock = new object();

                ns = client.GetStream();

                Task.Factory.StartNew(() =>
                {
                    Thread.Sleep(10000);
                    Console.Write(DateTime.Now);

                    lock (my_lock)
                    {
                        Console.Write(DateTime.Now);

                        client.Client.Close();
                    }
                });

                Task.Factory.StartNew(() =>
                {
                    AES aes = null;
                    RSACryptoServiceProvider rsa = null;
                    while (true)
                    {
                        int messageType;
                        try
                        {
                            messageType = BitConverter.ToInt32(Receive(client, 4), 0);
                        }
                        catch (IOException e)
                        {
                            break;
                        }

                        lock (my_lock)
                        {
                            if (messageType == TCPConnection.GET_SESSION_KEY)
                            {
                                var lenBytes = BitConverter.ToInt32(Receive(client, 4), 0);
                                aes = GenerateSessionKey();
                                SendEcryptedSessionKey(aes, rsa);
                            }

                            if (messageType == TCPConnection.PUBLIC_KEY)
                            {
                                var lenBytes = BitConverter.ToInt32(Receive(client, 4), 0);
                                rsa = GetPublicKeyFromClient(client, lenBytes);
                            }

                            if (messageType == TCPConnection.LOGIN)
                            {
                                var lenBytes = BitConverter.ToInt32(Receive(client, 4), 0);
                                var msg = Receive(client, lenBytes);

                                byte[] loginLenArray = new byte[4];
                                Array.Copy(msg, 0, loginLenArray, 0, 4);

                                var loginLen = BitConverter.ToInt32(loginLenArray, 0);

                                byte[] loginArray = new byte[loginLen];
                                Array.Copy(msg, 4, loginArray, 0, loginLen);
                                loginArray = AES.Decrypt(loginArray, aes.rijndaelManaged.Key, aes.rijndaelManaged.IV);
                                var login = Encoding.Default.GetString(loginArray);

                                var passwordLen = msg.Length - 4 - loginLen;
                                byte[] passwordArray = new byte[passwordLen];
                                Array.Copy(msg, 4 + loginLen, passwordArray, 0, passwordLen);
                                passwordArray = AES.Decrypt(passwordArray, aes.rijndaelManaged.Key, aes.rijndaelManaged.IV);
                                var password = Encoding.Default.GetString(passwordArray);

                                if (checkUser(login, password))
                                {
                                    Send(client, TCPConnection.LOGIN_APPROVED, null);
                                }
                                else
                                {
                                    Send(client, TCPConnection.LOGIN_REJECTED, null);
                                }
                            }
                            else if (messageType == TCPConnection.FILENAME)
                            {
                                var lenBytes = BitConverter.ToInt32(Receive(client, 4), 0);
                                var msg = Receive(client, lenBytes);
                                var filename = Encoding.UTF8.GetString(AES.Decrypt(msg, aes.rijndaelManaged.Key, aes.rijndaelManaged.IV));
                                if (File.Exists(filename))
                                {
                                    var text = File.ReadAllBytes(filename);
                                    text = AES.Encrypt(text, aes.rijndaelManaged.Key, aes.rijndaelManaged.IV);
                                    Send(client, TCPConnection.TEXT, text);
                                }
                                else
                                {
                                    Send(client, TCPConnection.FILE_DO_NOT_EXISTS, null);
                                }
                            }
                            else if (messageType == TCPConnection.TEXT)
                            {
                                var lenBytes = BitConverter.ToInt32(Receive(client, 4), 0);
                                var msg = Receive(client, lenBytes);

                                byte[] filenameLenArray = new byte[4];
                                Array.Copy(msg, 0, filenameLenArray, 0, 4);

                                var filenameLen = BitConverter.ToInt32(filenameLenArray, 0);

                                byte[] filenameArray = new byte[filenameLen];
                                Array.Copy(msg, 4, filenameArray, 0, filenameLen);
                                filenameArray = AES.Decrypt(filenameArray, aes.rijndaelManaged.Key, aes.rijndaelManaged.IV);
                                var filename = Encoding.Default.GetString(filenameArray);

                                var textLen = msg.Length - 4 - filenameLen;
                                byte[] textArray = new byte[textLen];
                                Array.Copy(msg, 4 + filenameLen, textArray, 0, textLen);

                                textArray = AES.Decrypt(textArray, aes.rijndaelManaged.Key, aes.rijndaelManaged.IV);

                                File.WriteAllBytes(filename, textArray);
                                Send(client, TCPConnection.FILE_SAVED, null);
                            }
                        }

                    }
                });
            }
        }
    }
}