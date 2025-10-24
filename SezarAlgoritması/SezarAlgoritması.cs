using System;

namespace SezarAlgoritması
{
    public class SezarAlgoritması
    {
        // Sezar şifreleme fonksiyonu
        static string Encrypt(string text, int shift)
        {
            char[] buffer = text.ToCharArray();

            for (int i = 0; i < buffer.Length; i++) // for döngüsü ile mesajın her karakterini sırayla alıyoruz.
            {
                char c = buffer[i];

                if (char.IsLetter(c)) // i. karakteri al ve c'ye koy
                {
                    char offset = char.IsUpper(c) ? 'A' : 'a'; // Büyük/küçük harf farkını hesaplıyoruz.
                    c = (char)(((c + shift - offset) % 26 + 26) % 26 + offset); // negatif kaydırma için
                    buffer[i] = c; // Şifrelenmiş harfi karakter dizisine geri koyuyoruz.
                }
            }

            return new string(buffer);
        }

        // Sezar şifre çözme fonksiyonu (Encrypt'in tersini çağırır)
        static string Decrypt(string text, int shift)
        {
            return Encrypt(text, -shift); // ters kaydırma
        }

        // Brute-force ile tüm kaydırmaları deneyip listeler
        static void BruteForce(string encryptedMessage)
        {
            Console.WriteLine("\nTüm olası çözümler:");
            for (int shift = 0; shift < 26; shift++)
            {
                string attempt = Decrypt(encryptedMessage, shift);
                Console.WriteLine($"Shift {shift}: {attempt}");
            }
        }

        static void Main()
        {
            Console.WriteLine("Sezar Algoritması - Oluşturma(1) / Çözme(0)");
            Console.WriteLine("Çıkmak için 'q' girin.\n");

            while (true)
            {
                Console.Write("Seçiminiz (1 = oluşturma, 0 = çözme, q = çıkış): ");
                string choice = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(choice))
                {
                    Console.WriteLine("Lütfen geçerli bir seçim yapın.");
                    continue;
                }

                if (choice.Equals("q", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Programdan çıkılıyor...");
                    break;
                }

                if (choice == "1") // Oluşturma (şifreleme)
                {
                    Console.Write("Şifrelenecek mesajı girin: ");
                    string message = Console.ReadLine() ?? "";

                    Console.Write("Kaydırma miktarını girin (0-25): ");
                    string shiftInput = Console.ReadLine();
                    if (!int.TryParse(shiftInput, out int shift))
                    {
                        Console.WriteLine("Geçersiz kaydırma değeri. Lütfen bir sayı girin.\n");
                        continue;
                    }

                    // Normalize shift (negatif veya büyük sayılar için)
                    shift = ((shift % 26) + 26) % 26;

                    string encrypted = Encrypt(message, shift);
                    string decrypted = Decrypt(encrypted, shift);

                    Console.WriteLine("\n--- Sonuç ---");
                    Console.WriteLine("Şifrelenmiş mesaj: " + encrypted);
                    Console.WriteLine("Tekrar çözülmüş mesaj (kontrol): " + decrypted);
                    Console.WriteLine("--------------\n");
                }
                else if (choice == "0") // Çözme (brute-force)
                {
                    Console.Write("Çözülecek şifreli mesajı girin: ");
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
                    Console.WriteLine("Geçersiz seçim! Lütfen 1, 0 veya q girin.\n");
                }
            }
        }
    }
}
