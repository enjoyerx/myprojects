using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Blocknote
{
    public class RSAKeyPair
    {
        public RSAParameters publicKey;
        public RSAParameters privateKey;
        private RSACryptoServiceProvider cryptoServiceProvider;

        public RSAKeyPair()
        {
            cryptoServiceProvider = new RSACryptoServiceProvider();
            privateKey = cryptoServiceProvider.ExportParameters(true);
            publicKey = cryptoServiceProvider.ExportParameters(false);
        }

        public RSAKeyPair(RSAParameters rsaParams)
        {
            cryptoServiceProvider = new RSACryptoServiceProvider();
            cryptoServiceProvider.ImportParameters(rsaParams);
            this.privateKey = rsaParams;
            this.publicKey = cryptoServiceProvider.ExportParameters(false);
        }

        public byte[] Decrypt(byte[] message)
        {
            return cryptoServiceProvider.Decrypt(message, false);
        }

        public string SerializePublicKey()
        {
            return Serializer.SerializeKey(publicKey);
        }

    }
}
