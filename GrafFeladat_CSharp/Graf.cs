using System;
using System.Collections.Generic;

namespace GrafFeladat_CSharp
{
    /// <summary>
    /// Irányítatlan, egyszeres gráf.
    /// </summary>
    class Graf
    {
        int csucsokSzama;
        /// <summary>
        /// A gráf élei.
        /// Ha a lista tartalmaz egy(A, B) élt, akkor tartalmaznia kell
        /// a(B, A) vissza irányú élt is.
        /// </summary>
        readonly List<El> elek = new List<El>();
        /// <summary>
        /// A gráf csúcsai.
        /// A gráf létrehozása után új csúcsot nem lehet felvenni.
        /// </summary>
        readonly List<Csucs> csucsok = new List<Csucs>();

        /// <summary>
        /// Létehoz egy úgy, N pontú gráfot, élek nélkül.
        /// </summary>
        /// <param name="csucsok">A gráf csúcsainak száma</param>
        public Graf(int csucsok)
        {
            this.csucsokSzama = csucsok;

            // Minden csúcsnak hozzunk létre egy új objektumot
            for (int i = 0; i < csucsok; i++)
            {
                this.csucsok.Add(new Csucs(i));
            }
        }

        /// <summary>
        /// Hozzáad egy új élt a gráfhoz.
        /// Mindkét csúcsnak érvényesnek kell lennie:
        /// 0 &lt;= cs &lt; csúcsok száma.
        /// </summary>
        /// <param name="cs1">Az él egyik pontja</param>
        /// <param name="cs2">Az él másik pontja</param>
        public void Hozzaad(int cs1, int cs2)
        {
            if (cs1 < 0 || cs1 >= csucsokSzama ||
                cs2 < 0 || cs2 >= csucsokSzama)
            {
                throw new ArgumentOutOfRangeException("Hibas csucs index");
            }

            // Ha már szerepel az él, akkor nem kell felvenni
            foreach (var el in elek)
            {
                if (el.Csucs1 == cs1 && el.Csucs2 == cs2)
                {
                    return;
                }
            }

            elek.Add(new El(cs1, cs2));
            elek.Add(new El(cs2, cs1));
        }

        /// <summary>
        /// Szélességi bejárás
        /// </summary>
        /// <param name="kezdopont"></param>
        public void SzelessegiBejar(int kezdopont)
        {
            // Kezdetben egy pontot sem jártunk be
            List<int> bejart = new List<int>();

            // A következőnek vizsgált elem a kezdőpont
            List<int> kovetkezok = new List<int>();
            kovetkezok.Add(kezdopont);
            bejart.Add(kezdopont);

            // Amíg van következő, addig megyünk. Ciklus amíg következők nem üres:
            while (kovetkezok.Count != 0)
            {
                // A sor elejéről vesszük ki
                int k = kovetkezok[0];
                kovetkezok.RemoveAt(0);

                // Elvégezzük a bejárási műveletet, pl. a konzolra kiírást:
                Console.WriteLine(this.csucsok[k]);

                foreach (var el in this.elek)
                {
                    // Megkeressük azokat az éleket, amelyek k-ból indulnak
                    // Ha az él másik felét még nem vizsgáltuk, akkor megvizsgáljuk
                    if (el.Csucs1 == k && !bejart.Contains(el.Csucs2))
                    {
                        // A sor végére és a bejártak közé szúrjuk be
                        kovetkezok.Add(el.Csucs2);
                        bejart.Add(el.Csucs1);
                    }
                }
                // Jöhet a sor szerinti következő elem
            }
        }


        // A fő függvény, amivel a rekurziót elindítjuk
        /// <summary>
        /// Mélységi bejárás 2. változat
        /// </summary>
        /// <param name="kezdopont"></param>
        public void MelysegiBejar(int kezdopont)
        {
            List<int> bejart = new List<int>();
            bejart.Add(kezdopont);
            this.MelysegiBejarRekurziv(kezdopont, bejart);
        }

        // Segédfüggvény, amely magát a rekurziót végzi
        /// <summary>
        /// 
        /// </summary>
        /// <param name="kezdopont"></param>
        public void MelysegiBejarRekurziv(int k, List<int> bejart)
        {
            Console.WriteLine(this.csucsok[k]);
            foreach (var el in this.elek)
            {
                if (el.Csucs1 == k && !bejart.Contains(el.Csucs2))
                {
                    bejart.Add(el.Csucs2);
                    this.MelysegiBejarRekurziv(el.Csucs2, bejart);
                }
            }
        }

        public bool Osszefuggo()
        {
            List<int> bejart = new List<int>();

            List<int> kovetkezok = new List<int>();
            kovetkezok.Add(0); //Tetszőleges, mondjuk 0 kezdőpont
            bejart.Add(0);

            while (kovetkezok.Count != 0)
            {
                int k = kovetkezok[0];
                kovetkezok.RemoveAt(0);

                // Bejárás közben nem kell semmit csinálni

                foreach (var el in this.elek)
                {
                    if (el.Csucs1 == k && !bejart.Contains(el.Csucs2))
                    {
                        kovetkezok.Add(el.Csucs2);
                        bejart.Add(el.Csucs2);
                    }
                }
            }

            // A végén megvizsgáljuk, hogy minden pontot bejártunk-e
            if (bejart.Count == this.csucsokSzama)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Graf Feszitofa()
        {
            Graf fa = new GrafFeladat_CSharp.Graf(this.csucsokSzama);

            return fa;
        }

        public override string ToString()
        {
            string str = "Csucsok:\n";
            foreach (var cs in csucsok)
            {
                str += cs + "\n";
            }
            str += "Elek:\n";
            foreach (var el in elek)
            {
                str += el + "\n";
            }
            return str;
        }
    }
}