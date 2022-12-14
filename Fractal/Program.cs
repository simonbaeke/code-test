using Fractal;

string fileName = "../../../fractal - input.txt";
string[] lines = File.ReadAllLines(fileName);

Dictionary<string, string> regels = new Dictionary<string, string>();

foreach (string line in lines)
{
    regels.Add(line.Split("=>")[0].Trim(), line.Split("=>")[1].Trim());
}

Raster raster = new Raster(".#./..#/###");

IOmvormer omvormer = new Omvormer(regels);
//omvormer = new TestOmvormer();
Uitbreider uitbreider = new Uitbreider(omvormer);
raster.Draw();

for (int i = 0; i < 18; i++)
{
    raster = uitbreider.BreidtUit(raster);
    //raster.Draw();
    Console.WriteLine($"na {i + 1} iteraties: {raster.Count('#')}");
}