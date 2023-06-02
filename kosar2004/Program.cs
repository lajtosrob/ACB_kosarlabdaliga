using System.Security.Cryptography.X509Certificates;
using System.Linq.Expressions;
using kosar2004.DataSource;
using System.IO;
using System.Runtime.CompilerServices;

namespace kosar2004
{
    internal class Program
    {
        static List<Eredmenyek> eredmenyeks = new List<Eredmenyek>();
        static string[] eredmenyekLista;
        static void Main(string[] args)
        {

            //2. Adatbeolvasás

            /* string[] sorok = File.ReadAllLines("DataSource\\eredmenyek.csv");


            for (int i = 1; i < sorok.Length; i++)
            {
                string[] adatok = sorok[i].Split(';');

                string hazai = adatok[0];
                string idegen = adatok[1];
                int hazaiPont = int.Parse(adatok[2]);
                int idegenPont = int.Parse((adatok[3]));
                string helyszin = adatok[4];
                DateTime idopont = DateTime.Parse(adatok[5]);

                Eredmenyek eredmenyekDatas = new(hazai, idegen, hazaiPont, idegenPont, helyszin, idopont);

                eredmenyeks.Add(eredmenyekDatas);
            } */

            StreamReader sr = new StreamReader("DataSource\\eredmenyek.csv");

            string headerLine = sr.ReadLine();

            while (!sr.EndOfStream) {
                string[] line = sr.ReadLine().Split(";");

                Eredmenyek eredmenyekDatas = new Eredmenyek(
                    line[0],
                    line[1],
                    int.Parse(line[2]),
                    int.Parse(line[3]),
                    line[4],
                    DateTime.Parse(line[5])
                    );
                eredmenyeks.Add( eredmenyekDatas );
            }

            /*
            string[] sorok = File.ReadAllLines("DataSource\\eredmenyek.csv");

            eredmenyeks = sorok
                .Skip(1)
                .Select(x => {

                string[] adatok = x.Split(';');

                    return new Eredmenyek
                    {
                        Hazai = adatok[0],
                        Idegen = adatok[1],
                        HazaiPont = int.Parse(adatok[2]),
                        IdegenPont = int.Parse(adatok[3]),
                        Helyszin = adatok[4],
                        Idopont = DateTime.Parse(adatok[5])
                    };
                })
                .ToList(); */




            Console.WriteLine("A beolvasás készen van.");

            //.3

            int realHazai = 0;
            int realIdegen = 0;

            foreach(var item in eredmenyeks)
            {
                if (item.Hazai == "Real Madrid")
                {
                    realHazai++;
                }
                if (item.Idegen == "Real Madrid")
                {
                    realIdegen++;
                }
            }

            Console.WriteLine($"3. feladat: Real Madrid: Hazai: {realHazai}, Idegen: {realIdegen}");

            //4. 

            bool voltDontetlen = false;
            foreach (var eredmeny in eredmenyeks)
            {
                if (eredmeny.HazaiPont == eredmeny.IdegenPont)
                {
                    voltDontetlen = true;
                }
            }
            Console.Write("4. feladat: Volt döntetlen: ");
            Console.WriteLine(voltDontetlen ? "Igen" : "Nem");

            //.5

            Console.WriteLine("5. feladat:");
            foreach (var eredmeny in eredmenyeks)
            {
                if(eredmeny.Idopont == DateTime.Parse("2004, 11, 21"))
                {
                    Console.WriteLine($"\t{eredmeny.Hazai}-{eredmeny.Idegen} ({eredmeny.HazaiPont}:{eredmeny.IdegenPont})");
                }
            }

            //6. 

            Console.WriteLine("7. feladat: ");
            var stadionok = eredmenyeks
                .GroupBy(x => x.Helyszin)
                .Where(g => g.Count() > 20)
                .Select(g => new { Stadion = g.Key, MerkozesekSzama = g.Count() })
                .OrderBy(z => z.Stadion);

            foreach (var stadion in stadionok)
            {
                Console.WriteLine($"\t{stadion.Stadion}: {stadion.MerkozesekSzama}");
            }
        }
    }
}
