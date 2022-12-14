using System;
namespace Fractal
{
    public class Omvormer : IOmvormer
    {
        public Dictionary<string,string> UitBreidingsRegels {get; set;}

		public Omvormer(Dictionary<string,string> uitbreidingsRegels)
		{
            UitBreidingsRegels = uitbreidingsRegels;
		}

        //zoek het resulterende patroon in de uitbreidingsregels
        public string VormOm(string deelRaster)
        {
            string resultaat = null; 

            Raster m = new Raster(deelRaster);

            int teller = 0;

            //check of patroon in tabel staat, zoniet roteer en spiegel
            while(!UitBreidingsRegels.ContainsKey(m.ToString()) && teller < 9)
            {
                m.Roteer();

                if (teller == 4)
                {
                    m.Spiegel();
                }

                teller++;
            }

            //vervang patroon door uitbreiding
            UitBreidingsRegels.TryGetValue(m.ToString(), out resultaat);

            return resultaat;
        }
    }
}

