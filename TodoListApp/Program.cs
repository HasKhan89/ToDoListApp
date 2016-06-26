using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace TodoListApp
{
    class Program
    {
        

        static string[] kayitlar = new string[0];
        static string dosyaYolu = Application.StartupPath + "\\teldefdata.txt";

        static void Main(string[] args)
        {
            // TodoList uygulaması.
            // Görev ekleyebilmeliyim..
            // Görevlerim listelenmeli..
            // Görev düzenleyebilmeliyim..(Metnini değiştirme, tamamlandı/yapılacak şeklinde biçimi değiştirme)
            // Görev silebilmeliyim..

            // Listeleme yaparken önce yapılacaklar sonra tamamlananlar listelensin.
            // Program çıkışında dosyaya verileri kaydetme
            // Program girişinde dosyadan verileri okuma

            VerileriOku();

            string secim;

            do
            {
                secim = Menu();

                switch (secim)
                {
                    case "1":
                        YeniGorevEkle();    // CTRL + .     // F12
                        break;

                    case "2":
                        Listeleme();
                        break;

                    case "3":
                        Duzenleme();
                        break;

                    case "4":
                        Sil();
                        break;

                    case "5":
                        KaydetveCik();
                        break;

                    default:
                        break;
                }

            } while (secim != "5"); // Çıkış yapılmadıkça dön..



            Console.ReadKey();


        }

        private static void VerileriOku()
        {
            if(File.Exists(dosyaYolu) == true)
            {
                kayitlar = File.ReadAllLines(dosyaYolu);
            }
        }

        private static void KaydetveCik()
        {
            File.WriteAllLines(dosyaYolu, kayitlar);
        }

        private static void Sil()
        {
            Console.Clear();
            Console.WriteLine("===== KAYIT SİL =====");
            Console.WriteLine();

            Console.Write("Kayıt ID : ");
            int index = int.Parse(Console.ReadLine());

            Console.WriteLine();
            Console.WriteLine(" KAYIT DETAY => ");

            // Görev detayları parçalanır ve ekrana yazılır.
            string[] gorev = kayitlar[index].Split(';');
            Console.WriteLine(" Görev : {0}\t{1}", gorev[0], gorev[1]);

            Console.WriteLine();
            Console.Write("Silmek istediğinize emin misiniz?(E/H)");

            string silSecim = Console.ReadLine();

            if (silSecim == "E")
            {
                string[] kayitlar2 = new string[kayitlar.Length - 1];
                int sayac = 0;

                for (int i = 0; i < kayitlar.Length; i++)
                {
                    if (i != index)
                    {
                        // Silinecek kayıt haric diğerleri yeni diziye eklenir.
                        kayitlar2[sayac] = kayitlar[i];
                        sayac++;
                    }
                }

                // Silinmiş olarak oluşturulan yeni dizi(kayitlar2) eski diziye(kayitlar) aktarılır.
                kayitlar = kayitlar2;
            }
        }

        private static void Duzenleme()
        {
            // Bir kayıt ID si alarak kayıt detayları gösterilir.
            // 1-Tamamlandı , 2-Yapılacak olarak seçenekler yazılır.
            // Seçim yapması beklenir.

            // markete gidilecek;yapılacak
            // split
            // metin alınır. seçime göre tamalandı ya da yapılacak ifadesi tekrar yazılır.
            // diziye kayıt geri yazılır.

            Console.Clear();
            Console.WriteLine("===== KAYIT DÜZENLEME =====");
            Console.WriteLine();

            Console.Write("Kayıt ID : ");
            int index = int.Parse(Console.ReadLine());

            Console.WriteLine();
            Console.WriteLine(" KAYIT DETAY => ");

            // Görev detayları parçalanır ve ekrana yazılır.
            string[] gorev = kayitlar[index].Split(';');
            Console.WriteLine(" Görev : {0}\t{1}", gorev[0], gorev[1]);

            Console.WriteLine();
            Console.WriteLine(" [1] => Tamamlandı");
            Console.WriteLine(" [2] => Yapılacak");
            Console.WriteLine();

            Console.Write("Seçiminizi belirtiniz : ");
            string duzenleSecim = Console.ReadLine();

            switch (duzenleSecim)
            {
                case "1":
                    gorev[1] = "Tamamlandı";
                    break;

                case "2":
                    gorev[1] = "Yapılacak";
                    break;

                default:
                    break;
            }

            kayitlar[index] = string.Join(";", gorev);
        }

        private static void Listeleme()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.BackgroundColor = ConsoleColor.White;

            Console.WriteLine("===== LİSTELEME =====");
            Console.WriteLine();

            int sayac = 0;

            while (kayitlar.Length > sayac)
            {
                string[] gorev = kayitlar[sayac].Split(';');
                Console.WriteLine(" {0} => {1}\t{2}", sayac, gorev[0], gorev[1]);

                sayac++;
            }

            Console.WriteLine();
            Console.WriteLine("Menü'ye gitmek için bir tuşa basınız.");
            Console.ReadKey();

            Console.ResetColor();
        }

        private static void YeniGorevEkle()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine("===== YENİ GÖREV EKLEME =====");
            Console.WriteLine();

            Console.Write("Lütfen  görevi belirtiniz : ");
            string gorev = Console.ReadLine();

            Array.Resize(ref kayitlar, kayitlar.Length + 1);
            kayitlar[kayitlar.Length - 1] = gorev + ";yapılacak";

            Console.WriteLine("Görev eklenmiştir.");
            Console.Write("Devam etmek için bir tuşa basınız..");

            Console.ReadKey();

            Console.ResetColor();
        }

        private static string Menu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine("===== MENÜ =====");
            Console.WriteLine();
            // CTRL + K,C
            //Console.WriteLine(" 1 => Yeni Görev Ekleme");
            //Console.WriteLine(" 2 => Listeleme");
            //Console.WriteLine(" 3 => Düzenleme");
            //Console.WriteLine(" 4 => Silme");
            //Console.WriteLine(" 5 => Çıkış");

            string secenekler = "Yeni Görev Ekleme,Listeleme,Düzenleme,Silme,Çıkış";
            string[] seceneklerDizi = secenekler.Split(',');
            string[] secenekIndexleri = new string[seceneklerDizi.Length];

            for (int i = 0; i < seceneklerDizi.Length; i++)
            {
                Console.WriteLine(" {0} => {1}", (i + 1), seceneklerDizi[i]);
                secenekIndexleri[i] = (i + 1).ToString();
            }

            Console.WriteLine();    // Bir satır boşluk verme..

            string secim;

            do
            {
                Console.Write("Lütfen işleminizi belirtiniz : ");
                secim = Console.ReadLine();
            } while (secenekIndexleri.Contains(secim) == false);

            Console.ResetColor();   // Ekran yazı rengini eski hale çevirir.

            return secim;
        }
    }
}
