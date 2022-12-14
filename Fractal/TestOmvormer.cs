using System;
namespace Fractal
{
	public class TestOmvormer : IOmvormer
	{
		public TestOmvormer()
		{
		}

        public string VormOm(string deelRaster)
        {
            string[] raster = deelRaster.Split('/');
            string[] nieuwRaster = new string[raster.Length + 1];

            Random rnd = new Random();
            char randomChar = (char)rnd.Next('a', 'z');

            for (int i = 0; i < raster.Length; i++)
            {
                nieuwRaster[i] = raster[i] + (char)rnd.Next('a', 'z');
                nieuwRaster[nieuwRaster.Length - 1] += (char)rnd.Next('a', 'z');
            }

            nieuwRaster[nieuwRaster.Length - 1] += (char)rnd.Next('a', 'z');

            return String.Join('/', nieuwRaster);
        }
    }
}

