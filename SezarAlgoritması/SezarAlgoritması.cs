using System;
using System.Linq;

namespace SezarAlgoritması
{
    class Program
    {
        // Türk alfabesi (29 harf)
        static readonly string TurkishUpper = "ABCÇDEFGĞHIİJKLMNOÖPRSŞTUÜVYZ";
        static readonly string TurkishLower = "abcçdefgğhıijklmnoöprsştuüvyz";

        // Şifreleme fonksiyonu
        static string Encrypt(string text, int shift)
        {
            char[] buffer = text.ToCharArray();
            int alphabetLength = TurkishUpper.Length; // 29

            for (int i = 0; i < buffer.Length; i++)
            {
                char c = buffer[i];

                if (TurkishUpper.Contains(c))
                {
                    int index = TurkishUpper.IndexOf(c);
                    buffer[i] = TurkishUpper[(index + shift + alphabetLength) % alphabetLength];
                }
                else if (TurkishLower.Contains(c))
                {
                    int index = TurkishLower.IndexOf(c);
                    buffer[i] = TurkishLower[(index + shift + alphabetLength) % alphabetLength];
                }
                // Harf değilse (boşluk, sayı, noktalama) aynen bırakılır
            }

            return new string(buffer);
        }

        // Şifre çözme
        static string Decrypt(string text, int shift)
        {
            return Encrypt(text, -shift);
        }

        // Brute-force (29 olasılık)
        static void BruteForce(string encryptedMessage)
        {
            Console.WriteLine("\nTüm olası çözümler:");
            for (int shift = 0; shift < TurkishUpper.Length; shift++)
            {
                Console.WriteLine($"Shift {shift}: {Decrypt(encryptedMessage, shift)}");
            }
        }

        static void Main()
        {
            Console.WriteLine("=== TÜRKÇE DESTEKLİ SEZAR ALGORİTMASI ===");
            Console.WriteLine("Şifreleme (1) | Çözme / Brute Force (0) | Çıkış (q)\n");

            while (true)
            {
                Console.Write("Seçiminiz: ");
                string choice = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(choice))
                    continue;

                if (choice.Equals("q", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Programdan çıkılıyor...");
                    break;
                }

                if (choice == "1")
                {
                    Console.Write("Şifrelenecek mesaj: ");
                    string message = Console.ReadLine() ?? "";

                    Console.Write("Kaydırma miktarı (0-28): ");
                    if (!int.TryParse(Console.ReadLine(), out int shift))
                    {
                        Console.WriteLine("Geçersiz sayı!\n");
                        continue;
                    }

                    shift = ((shift % 29) + 29) % 29;

                    string encrypted = Encrypt(message, shift);
                    string decrypted = Decrypt(encrypted, shift);

                    Console.WriteLine("\n--- SONUÇ ---");
                    Console.WriteLine("Şifreli Metin : " + encrypted);
                    Console.WriteLine("Çözülmüş Metin: " + decrypted);
                    Console.WriteLine("--------------\n");
                }
                else if (choice == "0")
                {
                    Console.Write("Şifreli mesajı girin: ");
                    string encryptedMessage = Console.ReadLine() ?? "";

                    if (string.IsNullOrWhiteSpace(encryptedMessage))
                    {
                        Console.WriteLine("Boş mesaj girildi.\n");
                        continue;
                    }

                    BruteForce(encryptedMessage);
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("Geçersiz seçim!\n");
                }
            }
        }
    }
}
