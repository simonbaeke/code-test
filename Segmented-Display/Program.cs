using Segmented_Display;
using System;
 
string fileName = "../../../segmented display - input.txt";
string[] lines = File.ReadAllLines(fileName);
Mapper mapper;
int score = 0;

foreach (string line in lines)
{
    string[] combinaties = line.Split('|')[0].Split(" ");
    string[] display = line.Split('|')[1].Trim().Split(" ");

    string gemappedDisplay = "";

    mapper = new Mapper(combinaties);

    foreach (string d in display)
    {
        gemappedDisplay += mapper.Map(d);
    }

    score += Int32.Parse(gemappedDisplay); ;

    gemappedDisplay = "";
}

Console.WriteLine($"score= {score}");
