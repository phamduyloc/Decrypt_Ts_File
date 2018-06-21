using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Decrypt_Ts_File
{
    class Program
    {
        static void Main(string[] args)
        {
            string folder = @"D:\f1nd0u7\Decrypt Video hocmai.vn\Video Dowloaded";
            byte[] encryptionKey = File.ReadAllBytes(@"D:\f1nd0u7\Decrypt Video hocmai.vn\hocmai.key");

            string outputFile = @"D:\f1nd0u7\Decrypt Video hocmai.vn\Video Decrypted\test.ts";
            using (FileStream outputFileStream = new FileStream(outputFile, FileMode.Create))
            {
                var files = Directory.GetFiles(folder, "*.ts");
                for (int i = 0; i < files.Length; i++)
                {
                    byte[] encryptionIV = new byte[16];
                    using (FileStream inputFileStream = new FileStream(files[i], FileMode.Open))
                    {
                        using (var aes = new AesManaged { Key = encryptionKey, IV = encryptionIV, Mode = CipherMode.CBC })
                        using (var encryptor = aes.CreateDecryptor())
                        using (var cryptoStream = new CryptoStream(inputFileStream, encryptor, CryptoStreamMode.Read))
                        {
                            cryptoStream.CopyTo(outputFileStream);
                        }
                    }
                }
            }
        }
    }
}
