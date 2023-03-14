using System;

namespace Segmented_Display
{
    public class Mapper
    {
        private Dictionary<String, int> Mappings { get;  set; }

        public Mapper(string[] combinaties)
        {
            Mappings = this.MapSignalsToDigits(combinaties);
        }

        public int Map(string combinatie)
        {
           return Mappings[String.Concat(combinatie.OrderBy(c => c))];

            //return Array.FindIndex(Mappings, m => m != null && m.Length == combinatie.Length && calcDifferences(m, combinatie) == 0);
        }

        private Dictionary<string, int> MapSignalsToDigits(string[] combinaties)
        {
            Dictionary<string, int> mappings = new Dictionary<string, int>();
             
            mappings.Add((string)getCombinationByLength(2, combinaties),1);
            mappings.Add((string)getCombinationByLength(3, combinaties),7);
            mappings.Add( (string)getCombinationByLength(4, combinaties),4);
            mappings.Add((string)getCombinationByLength(7, combinaties),8);

            //list met de combinaties met lengte 5 ==> deze kunnen de cijfer 2,3 en 5 vormen
            List<string> length5 = getCombinationsByLength(5, combinaties);
            //list met de combinaties met lengte 6 ==> deze kunnen de cijfers 6,,9 en 0 vormen
            List<string> length6 = getCombinationsByLength(6, combinaties);
            string length2 = getCombinationByLength(2, combinaties);
            string length4 = getCombinationByLength(4, combinaties);
            foreach (string s in length5)
            {
                if (calcDifferences(s, length2) == 3)
                {
                    //mappings[3] = s;
                    mappings.Add(s, 3);
                    //3 is gevonden en kan uit de list verwijderd worden
                    length5.Remove(s);
                    break;
                }
            }

            foreach (string s in length5)
            {
                if (calcDifferences(s, length4) == 3)
                {
                    //op een gelijke manier wordt aan de hand van het cijfer vier, het cijfer 2 gevonden
                    //mappings[2] = s;
                    mappings.Add(s,2);
                    length5.Remove(s);
                    break;
                };
            }

            //voor het cijfer 5 bestaat geen uniek verschil; maar enige overblijvende combinatie met 
            //5 actieve segmenten
            mappings.Add(length5[0], 5);

            foreach (string s in length6)
            {
                int differences = calcDifferences(s, length2);
                if (differences == 5)
                {
                    mappings.Add(s, 6);
                    length6.Remove(s);
                    break;
                }
            }

            //find 9
            foreach (string s in length6)
            {
                if (calcDifferences(s, length4) == 2)
                {
                    mappings.Add(s, 9);
                    length6.Remove(s);
                    break;
                }
                ;
            }

            //0 blijft over
            mappings.Add(length6[0],0);

            //sorteer combinaties
            Dictionary<string, int> mappingsSortedCombinations = new Dictionary<string, int>();

            foreach (string key in mappings.Keys)
            {
                mappingsSortedCombinations.Add(String.Concat(key.OrderBy(c => c)), mappings[key]);
            }

            return mappingsSortedCombinations;
        }

        //Zoek meerdere combinaties op het aantal actieve segmenten
        private List<string> getCombinationsByLength(int length, string[] combinaties)
        {
            return Array.FindAll(combinaties, c => c.Length == length).ToList<string>();
        }

        //Zoek een combinatie op het aantal actieve segmenten
        private string getCombinationByLength(int length, string[] combinaties)
        {
            return Array.Find(combinaties, c => c.Length == length);
        }

        private int calcDifferences(string one, string two)
        {
            string longest = one.Length > two.Length ? one : two;
            string shortest = one.Length <= two.Length ? one : two;

            int differences = 0;

            foreach (char c in longest.ToCharArray())
            {
                differences = shortest.IndexOf(c) < 0 ? differences + 1 : differences;
            }

            return differences;
        }

         
    }
}

