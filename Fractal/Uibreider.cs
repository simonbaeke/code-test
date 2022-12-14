using System;

namespace Fractal
{
    public class Uitbreider
    {
        IOmvormer Omvormer;

        public Uitbreider(IOmvormer omvormer)
        {
            Omvormer = omvormer;
        }

        //verwerkt het raster
        public Raster BreidtUit(Raster raster)
        {
            string[] deelRasters = raster.Splits();
            string[] resultaat = new string[deelRasters.Length];

            //zet alle deelpatronen om volgens de regels
            for (int i = 0; i < deelRasters.Length; i++)
            {
                resultaat[i] = Omvormer.VormOm(deelRasters[i]).Replace("/","");
            }

            Raster nieuwRaster = new Raster(resultaat);

            return new Raster(resultaat);
        }
    }
}

