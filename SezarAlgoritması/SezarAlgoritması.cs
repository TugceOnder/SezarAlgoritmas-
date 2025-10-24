using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SezarAlgoritması
{
    public class SezarAlgoritması
    {
        // Sezar şifreleme fonksiyonu
        static string Encrypt(string text, int shift)
        {
            char[] buffer = text.ToCharArray();

            for (int i = 0; i < buffer.Length; i++) //for döngüsü ile mesajın her karakterini sırayla alıyoruz.
            {
                char c = buffer[i];

                if (char.IsLetter(c)) // i. karakteri al ve c'ye koy
                {
                    char offset = char.IsUpper(c) ? 'A' : 'a'; //Büyük/küçük harf farkını hesaplıyoruz.
                    c = (char)(((c + shift - offset) % 26 + 26) % 26 + offset); // negatif kaydırma için
                    buffer[i] = c; //Şifrelenmiş harfi karakter dizisine geri koyuyoruz.
                }
            }

            return new string(buffer);
        }

        // Sezar şifre çözme fonksiyonu
        static string Decrypt(string text, int shift)
        {
            return Encrypt(text, -shift); // ters kaydırma
        }

        static void Main()
        {
            Console.Write("Şifrelenecek mesajı girin: ");
            string message = Console.ReadLine();

            Console.Write("Kaydırma miktarını girin: ");
            int shift = int.Parse(Console.ReadLine());

            string encrypted = Encrypt(message, shift);
            Console.WriteLine("Şifrelenmiş mesaj: " + encrypted);

            string decrypted = Decrypt(encrypted, shift);
            Console.WriteLine("Çözülmüş mesaj: " + decrypted);
        }
    }
    }
