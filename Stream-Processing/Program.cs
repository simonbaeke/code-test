using System.IO;

string fileName = "../../../stream processing - input.txt";
string text = File.ReadAllText(fileName);

int score = Processor.Process(Processor.VerwijderOnzin(text));

Console.WriteLine($"Score groepen: {score}");

public static class Processor
{
    public static int Process(string text, int index = 0, int nesting = 0, int score = 0)
    {
        //einde van de tekst
        if (index == text.Length)
        {
            return score;
        }

        //einde van een groep
        if (text[index] == '}')
        {
            return Process(text, index + 1, nesting - 1, score + nesting);
        }

        //begin van een groep
        if (text[index] == '{')
        {
            return Process(text, index + 1, nesting + 1, score);
        }

        return Process(text, index + 1, nesting, score);
    }


    public static string VerwijderOnzin(string text, bool isOnzin = false, int index = 0, int score = 0)
    {
        if (index == text.Length)
        {
            //resultaat
            Console.WriteLine($"score onzin: {score}");
            return text;
        }

        switch (text[index])
        {
            case '!':
                //TODO: zal fout geven indien laatste karakter in string een ! is
                //verwijder ! en het escaped karakter
                return VerwijderOnzin(text.Remove(index, 2), isOnzin, index, score);
            case '<':
                //verwijder karakter, start onzin
                //indien onzin reeds gestart: score + 1;
                return VerwijderOnzin(text.Remove(index, 1), true, index, isOnzin ? score + 1 : score);
            case '>':
                //verwijder karakter, einde van de onzin
                return VerwijderOnzin(text.Remove(index, 1), false, index, score);
            default:
                if (isOnzin)
                {
                    //onzin: verwijder karakter, score
                    return VerwijderOnzin(text.Remove(index, 1), true, index, score + 1);
                }

                return VerwijderOnzin(text, false, index + 1, score);
        }
    }

}











