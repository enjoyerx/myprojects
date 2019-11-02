using Crypto;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Blocknote
{
    public class Connection
    {
       
        public int RSAKeySize = 1024;
        private RSAKeyPair keys;
        private TcpClient client;
   
        public Connection(TcpClient client, RSAKeyPair keys)
        {
            this.keys = keys;
            this.client = client;
        }

        public void SendPublicKeyToServer() 
        {
            var serializedKey = Encoding.Default.GetBytes(keys.SerializePublicKey());
            Send(TCPConnection.PUBLIC_KEY, serializedKey);
        }

        public void SendTextName(string name)
        {
            var msg = Encoding.Default.GetBytes(name);
            Send(TCPConnection.FILE_NAME_HEADER, msg);
        }

        public byte[] Receive(int length)
        {
            Console.WriteLine("Read: " + length);
            var message = new byte[length];
            int read = 0;
            while (read < message.Length)
            {
                read += client.GetStream().Read(message, read, message.Length - read);
            }

            return message;
        }

        public void Send(int messageType, byte[] msg)
        {

            if (msg == null)
            {
                Console.WriteLine("Write: 4");
                Console.WriteLine("Write: 4");
                client.GetStream().Write(BitConverter.GetBytes(messageType), 0, 4);
                client.GetStream().Write(BitConverter.GetBytes(0), 0, 4);
            }

            if (messageType != TCPConnection.GET_SESSION_KEY)
            {
                client.GetStream().Write(BitConverter.GetBytes(messageType), 0, 4);
                Console.WriteLine("Write: 4");
                client.GetStream().Write(BitConverter.GetBytes(msg.Length), 0, 4);
                Console.WriteLine("Write: " + msg.Length);
                client.GetStream().Write(msg, 0, msg.Length);
            }

        }

    }
}
