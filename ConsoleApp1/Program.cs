
using System;
using System.Security.Cryptography;
using System.Text;
using static Program.Program;

namespace Program
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string encryptedPassword = "ua/+pvOiTphNXBknI33m1PaS8pGG/qCQRqCs0iKhoi4=";
            string decryptionKey = "tFZWl3oUx09Ed11MOA2tn24f9gaZG7he2F+679YYCDU=";

            string decryptedPassword = PasswordEncoder.DecodePassword(encryptedPassword, decryptionKey);

            Console.WriteLine("Contraseña descodificada: " + decryptedPassword);
        }

        public class PasswordEncoder
        {
            public static (string encryptedPassword, string decryptionKey) EncodePassword(string password)
            {
                // Generar una clave aleatoria para el desencriptado
                byte[] key = new byte[32];
                using (var rng = new RNGCryptoServiceProvider())
                {
                    rng.GetBytes(key);
                }
                string decryptionKey = Convert.ToBase64String(key);

                // Codificar la contraseña
                using (var aes = new AesCryptoServiceProvider())
                {
                    aes.GenerateIV();
                    aes.Key = key;
                    byte[] encryptedBytes;

                    using (var encryptor = aes.CreateEncryptor())
                    {
                        byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                        encryptedBytes = encryptor.TransformFinalBlock(passwordBytes, 0, passwordBytes.Length);
                    }

                    string encryptedPassword = Convert.ToBase64String(encryptedBytes);
                    return (encryptedPassword, decryptionKey);
                }
            }

            public static string DecodePassword(string encryptedPassword, string decryptionKey)
            {
                byte[] encryptedBytes = Convert.FromBase64String(encryptedPassword);
                byte[] key = Convert.FromBase64String(decryptionKey);

                using (var aes = new AesCryptoServiceProvider())
                {
                    aes.Key = key;
                    aes.IV = new byte[16]; // Inicializar el vector de inicialización en blanco ya que no se utilizó en la encriptación

                    using (var decryptor = aes.CreateDecryptor())
                    {
                        byte[] decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
                        string decryptedPassword = Encoding.UTF8.GetString(decryptedBytes);
                        return decryptedPassword;
                    }
                }
            }

        }
    }
}
