using System;
using System.Drawing;
using System.Reflection;

namespace Fractal
{
    public class Raster
    {
        public int Dim { get; private set; }

        //Elke string stelt een rij voor, de 
        public string[] Vierkant { get; private set; }

        //maak raster aan de hand van patroon
        public Raster(string regel)
        {
            Vierkant = regel.Split('/');
            Dim = Vierkant.Length;
        }

        //maak raster met random nummers: 
        public Raster(int dim)
        {
            Dim = dim;
            Vierkant = new string[Dim];
            int count = 0;

            for (int i = 0; i < Dim; i++)
            {
                for (int j = 0; j < Dim; j++)
                {
                    Vierkant[i] += count.ToString();
                    count++;
                }
            }
        }

        //Maak nieuw raster aan de hand van patronen
        public Raster(string[] deelPatronen)
        { 
            int rijIndexDeelPatroon;
            int rijIndexVierkant = 0;
            int dimDeelPatronen = (int) Math.Sqrt(deelPatronen.Length);
            int dimDeelpatroon = (int)Math.Sqrt(deelPatronen[0].Length);  //2 of 3
            Dim = dimDeelPatronen * dimDeelpatroon; 

            Vierkant = new string[Dim];

            //Zet de deelpatronen om in in nieuw vierkant
            //Teller die 
            for (int x = 0; x < dimDeelPatronen; x++)//gaat de rijen af gevormd door de deelpatronen
            {
                for (int j = 0; j < dimDeelpatroon; j++)//vult rijen binnen een deelpatroon
                {
                    for (int i = 0; i < dimDeelPatronen; i++)//vormt kolommen gevormd door de deelpatroon

                        //herhalen tot sqrt regels.length
                    {
                        rijIndexDeelPatroon = i + dimDeelPatronen * x;

                        Vierkant[rijIndexVierkant] += deelPatronen[rijIndexDeelPatroon][(j * dimDeelpatroon).. (j * dimDeelpatroon + dimDeelpatroon)];
                    }

                    //vul volgende rij van het vierkant
                    rijIndexVierkant++; 
                }
            }
        }

        //Splits het vierkant in kleinere vierkanten met dimensie 2 of 3
        public string[] Splits()
        {
            //bepaal de dimensie van de deelpatronen waarin het raster gesplitst wordt.
            int splitDim = Dim % 2 == 0 ? 2 : 3;

            //Array met deelrasters, lengte is het totaal aantal deelrasters
            string[] deelRasters = new string[(Dim / splitDim) * (Dim / splitDim)];

            string geselecteerdePixels = "";
            int indexDeelRaster = 0;
            int rijRasterVanDeelRasters = 0;

            //Ga alle rijen in het raster af 
            for (int rij = 0; rij < Dim; rij++)
            {
                //Ga kolommen af, stap per grootte van een deelraster
                for (int kolom = 0; kolom < Dim; kolom += splitDim)
                {
                    //Bereken index van het deelraster in de array van deelrasters
                    indexDeelRaster = rijRasterVanDeelRasters + kolom / splitDim;
                    //
                    geselecteerdePixels = Vierkant[rij][kolom..(kolom + splitDim)];
                    //
                    deelRasters[indexDeelRaster] += geselecteerdePixels + "/";

                    //Console.WriteLine($"index: {indexDeelpatroon}, kolom: {kolom}, rij; {rij}");
                }

                //volgende rij in het gesplitste raster ==> wanneer er twee of drie rijen zijn gedaan in het originele raster
                if ((rij + 1) % splitDim == 0)
                {
                    rijRasterVanDeelRasters += Dim / splitDim;
                }
            }

            //verwijder laatste teken van elk deeelraster: '/' te veel
            for (int i = 0; i < deelRasters.Length; i++)
            {
                deelRasters[i] = deelRasters[i].Substring(0, deelRasters[i].Length - 1);
            }

            return deelRasters;
        }

        //Teken het vierkant in de console
        public void Draw()
        {
            string lijn = "";

            for (int rij = 0; rij < Dim; rij++)
            {
                for (int kolom = 0; kolom < Dim; kolom++)
                {
                    Console.Write(this.Vierkant[rij][kolom] + " ");
                }

                Console.WriteLine("");
                lijn += "__";
            }

            Console.WriteLine(lijn);
            Console.WriteLine("");
        }

        //Roteer het vierkant 90 graden
        public void Roteer()
        {
            string[] geroteerd = new string[Dim];

            for (int rij = 0; rij < Dim; rij++)
            {
                for (int kolom = 0; kolom < Dim; kolom++)
                {
                    geroteerd[kolom] += Vierkant[Dim - rij - 1][kolom];
                }
            }

            this.Vierkant = geroteerd;
        }

        public void Spiegel()
        {
            string[] gespiegeld = new string[Dim];

            for (int rij = 0; rij < Dim; rij++)
            {
                for (int kolom = 0; kolom < Dim; kolom++)
                {
                    gespiegeld[rij] += Vierkant[rij][Dim - kolom - 1] ;
                }
            }

            this.Vierkant = gespiegeld;
        }

        public override string ToString()
        {
            return String.Join('/', Vierkant);
        }

        public int Count(char c)
        {
            int count = 0;

            foreach (string s in Vierkant)
            {
                count += s.Where(x => (x == c)).Count();
            }

            return count;
        }
    }
}

