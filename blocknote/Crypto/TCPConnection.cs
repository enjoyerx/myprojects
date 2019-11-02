using System;
using System.Collections.Generic;
using System.Text;

namespace Crypto
{
    public static class TCPConnection
    {
        /*

 1 - sending public key RSA
 2 - sending file name
 3 - request for AES key generation
 4 - encrypted text with AES
 5 - encrypted AES with RSA
 6 - end connection
 7 - aes key is dying
 8 - login
     */

        public const int PUBLIC_KEY = 1;
        public const int FILE_NAME_HEADER = 2;
        public const int GET_SESSION_KEY = 3;
        public const int ENCRYPTED_AES_WITH_RSA = 5;
        public const int SESSION_KEY_EXPIRED = 7;
        public const int LOGIN = 8;
        public const int LOGIN_APPROVED = 9;
        public const int LOGIN_REJECTED = 10;
        public const int FILENAME = 11;
        public const int TEXT = 12;
        public const int FILE_DO_NOT_EXISTS = 13;
        public const int FILE_SAVED = 14;


    }
}
