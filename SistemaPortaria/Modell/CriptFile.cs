using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;
using System.IO;
using System.Security.Cryptography;

namespace SistemaPortaria.Modell
{
    public class CriptFile
    {
        public void encrypt(string input, string output, string strHash)
        {
            FileStream inStream,outStream;
            CryptoStream CryStream;
            TripleDESCryptoServiceProvider TDC = new TripleDESCryptoServiceProvider();
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

            byte[] byteHash, byteTexto;

            inStream = new FileStream(input, FileMode.Open, FileAccess.Read);
            outStream = new FileStream(output, FileMode.OpenOrCreate, FileAccess.Write); 
            
            byteHash = md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(strHash));
            byteTexto = File.ReadAllBytes(input);

            md5.Clear();

            TDC.Key = byteHash;
            TDC.Mode = CipherMode.ECB;

            CryStream = new CryptoStream(outStream, TDC.CreateEncryptor(), CryptoStreamMode.Write);

            int bytesRead;
            long length, position = 0;
            length = inStream.Length;

            while (position < length)
            {
                bytesRead = inStream.Read(byteTexto, 0, byteTexto.Length);
                position += bytesRead;

                CryStream.Write(byteTexto, 0, bytesRead);
            }
            inStream.Close();
            outStream.Close();
        }

        public void decrypt(string input, string output, string strHash)
        {
            FileStream inStream, outStream;
            CryptoStream CryStream;
            TripleDESCryptoServiceProvider TDC = new TripleDESCryptoServiceProvider();
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

            byte[] byteHash, byteTexto;

            inStream = new FileStream(input, FileMode.Open, FileAccess.Read);
            outStream = new FileStream(output, FileMode.OpenOrCreate, FileAccess.Write);

            byteHash = md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(strHash));
            byteTexto = File.ReadAllBytes(input);

            md5.Clear();

            TDC.Key = byteHash;
            TDC.Mode = CipherMode.ECB;

            CryStream = new CryptoStream(outStream, TDC.CreateDecryptor(), CryptoStreamMode.Write);

            int bytesRead;
            long length, position = 0;
            length = inStream.Length;

            while (position < length)
            {
                bytesRead = inStream.Read(byteTexto, 0, byteTexto.Length);
                position += bytesRead;

                CryStream.Write(byteTexto, 0, bytesRead);
            }
            inStream.Close();
            outStream.Close();
        }

    }
}
