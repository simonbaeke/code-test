using System;
namespace Fractal
{
	public class OmvormRegelFactory
	{
		public OmvormRegelFactory()
		{

		}

		public static Dictionary<string,string> Parse(string[] lines)
		{
            Dictionary<string, string> regels = new Dictionary<string, string>();

            foreach (string line in lines)
            {
				string regel = line.Split("=>")[0].Trim();
				string uitbreiding = line.Split("=>")[1].Trim();

                regels.Add(line.Split("=>")[0].Trim(), line.Split("=>")[1].Trim());

				//TODO spiegelen en draaien
            }

            return regels;
		}

		private string Roteer(string regel)
		{


			return regel;
		}

		private string Spiegel(string regel)
		{
			return regel;
		}


	}
}

