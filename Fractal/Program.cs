using Fractal;

string fileName = "../../../fractal - input.txt";

Dictionary<string, string> regels = OmvormRegelFactory.Parse(File.ReadAllLines(fileName));

Raster raster = new Raster(".#./..#/###");

IOmvormer omvormer = new Omvormer(regels);
//omvormer = new TestOmvormer();
Uitbreider uitbreider = new Uitbreider(omvormer);

raster.Draw();

raster = uitbreider.BreidtUit(raster, 18);

Console.WriteLine($"na 18  iteraties: {raster.Count('#')}");