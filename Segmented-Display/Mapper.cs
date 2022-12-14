using System;

namespace Segmented_Display
{
    public class Mapper
    {
        //array waarbij aan de hand van de index een signaalcombinatie aan een
        //cijfer kan gelink worden ==> index is cijfer, string is signaalcombinatie
        public string[] Mappings { get; private set; }

        //constructor met als parameter de te analyseren signaalcombinaties
        public Mapper(string[] combinaties)
        {
            //Achterhaal welke signaalcombinaties bij welk cijfer horen
            Mappings = this.CreateMapping(combinaties);
        }


        public int Map(string combinatie)
        {
            //Zoek een combinatie in de array, van mappings, de index is dan het corresponderende cijfer
            //zomaar vergelijken kan niet, want de signalen staan in een willekeurige volgorde. 
            //lengte moet gelijk zijn, en de actieve segmenten moeten gelijk zijn 
            return Array.FindIndex(Mappings, m => m != null && m.Length == combinatie.Length && calcDifferences(m, combinatie) == 0);
        }


        //stel de tabel op die de combinaties aan de cijfers linkt
        public string[] CreateMapping(string[] combinaties)
        {
            //actieve segmenten per cijfer
            //2 ==> 1
            //3 ==> 7 
            //4 ==> 4
            //5 ==> 2,3,5
            //6 ==> 6,9,0
            //7 ==> 8

            //array met de combinaties in volgorde gezet
            string[] mappings = new string[10];
            //array om te testen
            string[] testMappings = new string[4];

            //Om de cijfers 1,4,7 en 8 te vormen kan het verband gelegd worden aan de hand 
            //van het aantal actieve segmenten dus lengte van combinatie

            mappings[1] = (string)getCombinationByLength(2, combinaties);
            mappings[7] = (string)getCombinationByLength(3, combinaties);
            mappings[4] = (string)getCombinationByLength(4, combinaties);
            mappings[8] = (string)getCombinationByLength(7, combinaties);

            testMappings[0] = (string)getCombinationByLength(2, combinaties);
            testMappings[1] = (string)getCombinationByLength(3, combinaties);
            testMappings[2] = (string)getCombinationByLength(4, combinaties);
            testMappings[3] = (string)getCombinationByLength(7, combinaties);

            /*
             * Verschil van aantal actieve signalen van twee combinaties
             * bij cijfers met hetzelfde aantal actieve segmenten dus 
             * en een vast cijfer met een uniek aantal actieve segmenten
             * Het verschil in actieve segmenten tussen twee cijfers ligt vast
             * Dus als dit uniek is , is dat de enige mogelijk mapping
             
              verschillen tussen cdfbe en ab = 4  
              verschillen tussen gcdfa en ab = 4
              verschillen tussen fbcad en ab = 3 <==

              verschillen tussen cdfbe en dab = 3
              verschillen tussen gcdfa en dab = 3
              verschillen tussen fbcad en dab = 2

              verschillen tussen cdfbe en eafb = 2
              verschillen tussen gcdfa en eafb = 3 <==
              verschillen tussen fbcad en eafb = 2*/


            //list met de combinaties met lengte 5 ==> deze kunnen de cijfer 2,3 en 5 vormen
            List<string> length5 = getCombinationsByLength(5, combinaties);
            //list met de combinaties met lengte 6 ==> deze kunnen de cijfers 6,,9 en 0 vormen
            List<string> length6 = getCombinationsByLength(6, combinaties);

            //Verdere analyse door de actieve segementen te vergelijken met
            //eerder actieve segmenten van eerder gevonden cijfers.
            //Alle combinaties met lengte 5 worden vergeleken met de combinatie voor het cijfer 1
            //Uit eerdere analyse is gebleken dat het aantal verschillende segmenten tussen het cijfer
            //1 en het cijfer 3 uniek is 
            foreach (string s in length5)
            {

                if (calcDifferences(s, mappings[1]) == 3)
                {
                    mappings[3] = s;
                    //3 is gevonden en kan uit de list verwijderd worden
                    length5.Remove(s);
                    break;
                }
            }


            foreach (string s in length5)
            {
                if (calcDifferences(s, mappings[4]) == 3)
                {
                    //op een gelijke manier wordt aan de hand van het cijfer vier, het cijfer 2 gevonden
                    mappings[2] = s;
                    length5.Remove(s);
                    break;
                }
                ;
            }

            //voor het cijfer 5 bestaat geen uniek verschil; maar enige overblijvende combinatie met 
            //5 actieve segmenten 
            mappings[5] = length5[0];


            /*verschil tussen aantal actieve signalen van twee combinaties
            verschillen tussen cefabd en ab = 4
            verschillen tussen cdfgeb en ab = 5 <==
            verschillen tussen cagedb en ab = 4
            verschillen tussen cefabd en dab = 3
            verschillen tussen cdfgeb en dab = 4
            verschillen tussen cagedb en dab = 3
            verschillen tussen cefabd en eafb = 2 <==
            verschillen tussen cdfgeb en eafb = 3
            verschillen tussen cagedb en eafb = 3
            verschillen tussen cefabd en acedgfb = 1
            verschillen tussen cdfgeb en acedgfb = 1
            verschillen tussen cagedb en acedgfb = 1*/

            //find 6 ==> 
            foreach (string s in length6)
            {
                int differences = calcDifferences(s, mappings[1]);
                if (differences == 5)
                {
                    mappings[6] = s;
                    length6.Remove(s);
                    break;
                }
            }

            //find 9
            foreach (string s in length6)
            {
                if (calcDifferences(s, mappings[4]) == 2)
                {
                    mappings[9] = s;
                    length6.Remove(s);
                    break;
                }
                ;
            }

            //0 blijft over
            mappings[0] = length6[0];

            foreach (string mapping in testMappings)
            {
                foreach (string combinatie in combinaties)

                    if (combinatie.Length == 6)
                    {
                        //Console.WriteLine($"verschillen tussen {combinatie} en {mapping} = {calcDifferences(combinatie, mapping)}");
                    }
            }

            return mappings;
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

        //bepaal het aantal verschillende actieve segmenten tussen twee combinaties
        //Het aantal letters van de ene combinatie dat niet teruggevonden wordt in de andere combinatie 
        //wordt opgeteld
        //Indien combinaties met verschillende lengte, moet langste eerst komen
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

