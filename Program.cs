namespace Hangman
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Willkommen zu Hangman");

            string[] Easywords = new string[] { "Hand", "Hund", "Auto", "Baum", "Haus", "Katze", "Wolf", "Apfel", "Banane", "Handy", "Fisch", "Mond", "Sonne", "Tisch", "Buch", "Zug", "Wald", "Ball", "Brot", "Maus", "Affe" };
            string[] Normalwords = new string[] { "Fenster", "Garten", "Schmetterling", "Geburtstag", "Frühstück", "Computer", "Abenteuer", "Bratpfanne", "Fahrrad", "Regenschirm", "Schlüssel", "Krawatte", "Wasserfall", "Flugzeug", "Schokolade", "Zeitung", "Teleskop", "Briefkasten", "Taschenlampe", "Fotografie" };
            string[] Hardwords = new string[] { "Enzyklopädie", "Philosophie", "Astronautenanzug", "Thermometer", "Renaissance", "Transkription", "Psychologie", "Wirtschaftswissenschaften", "Katastrophenschutz", "Meteorologie", "Elektrizitätswerk", "Schmetterlingsflügel", "Parallelogramm", "Klimaanlage", "Elektromagnetismus", "Kaffeekanne", "Zentrifugalkraft", "Sauerstoffmaske", "Schwermetallvergiftung", "Gewissensbisse" };

            bool returnGame = true;
            bool difficultyChange = true;
            int versuche = 0;
            int punkte = 0;
            int highscore = 0;
            string randomWord = "";

            while (returnGame)
            {
                if (difficultyChange)
                {
                    Console.Write("Bitte wählen Sie Ihren Schwierigkeitsgrad (Einfach / Mittel / Schwer): ");
                    string schwierigkeit = Console.ReadLine();

                    switch (schwierigkeit.ToLower())
                    {
                        case "einfach":
                            randomWord = Easywords[new Random().Next(Easywords.Length)].ToUpper();
                            versuche = 10;
                            break;
                        case "mittel":
                            randomWord = Normalwords[new Random().Next(Normalwords.Length)].ToUpper();
                            versuche = 10;
                            break;
                        case "schwer":
                            randomWord = Hardwords[new Random().Next(Hardwords.Length)].ToUpper();
                            versuche = 10;
                            break;
                        default:
                            Console.WriteLine("Ungültige Eingabe. Bitte versuchen Sie es erneut.");
                            continue;
                    }
                }

                Console.Clear();
                bool schleife = true;

                while (schleife)
                {
                    char[] striche = new string('-', randomWord.Length).ToCharArray();
                    List<char> falscheBuchstaben = new List<char>();
                    int verbleibendeVersuche = versuche;
                    bool gewonnen = false;

                    while (verbleibendeVersuche > 0 && !gewonnen)
                    {
                        Console.Clear();
                        Console.WriteLine($"Highscore: {highscore}");
                        Console.WriteLine($"Punkte: {punkte}");
                        Console.WriteLine("\n" + new string(striche));
                        Console.WriteLine("Falsche Buchstaben: " + string.Join(", ", falscheBuchstaben));
                        ZeichneHangman(verbleibendeVersuche);

                        Console.Write("Bitte geben Sie einen Buchstaben ein: ");
                        char buchstabe;
                        try
                        {
                            buchstabe = char.ToUpper(Console.ReadLine()[0]);
                            if (!Char.IsLetter(buchstabe))
                            {
                                Console.WriteLine("Bitte geben Sie nur einen Buchstaben ein.");
                                continue;
                            }
                        }
                        catch
                        {
                            Console.WriteLine("Ungültige Eingabe. Bitte geben Sie einen Buchstaben ein!");
                            continue;
                        }

                        if (randomWord.Contains(buchstabe))
                        {
                            for (int i = 0; i < randomWord.Length; i++)
                            {
                                if (randomWord[i] == buchstabe)
                                {
                                    striche[i] = buchstabe;
                                }
                            }
                        }
                        else
                        {
                            if (!falscheBuchstaben.Contains(buchstabe))
                            {
                                falscheBuchstaben.Add(buchstabe);
                                verbleibendeVersuche--;
                            }
                            else
                            {
                                Console.WriteLine("Diesen Buchstaben haben Sie bereits falsch geraten.");
                            }
                        }

                        if (new string(striche) == randomWord)
                        {
                            gewonnen = true;
                            punkte += 1;
                        }

                        if (highscore < punkte)
                        {
                            highscore = punkte;
                        }
                    }

                    if (gewonnen)
                    {
                        Console.Clear();
                        Console.WriteLine($"Herzlichen Glückwunsch! Sie haben das Wort '{randomWord}' erraten.");
                        Console.Write("Möchten Sie eine andere Schwierigkeit wählen? (Ja/Nein): ");
                        string antwort = Console.ReadLine().Trim().ToLower();

                        if (antwort == "ja")
                        {
                            difficultyChange = true;
                            schleife = false;
                        }
                        else
                        {
                            difficultyChange = false;
                            schleife = false;
                        }
                    }
                    else if (verbleibendeVersuche == 0)
                    {
                        Console.WriteLine($"Leider haben Sie verloren. Das richtige Wort war '{randomWord}'.");
                        Console.Write("Möchten Sie erneut spielen? (Ja/Nein): ");
                        string erneutSpielen = Console.ReadLine().Trim().ToLower();

                        if (erneutSpielen == "ja")
                        {
                            punkte = 0;
                            difficultyChange = true;
                            schleife = false;
                        }
                        else
                        {
                            Console.WriteLine("Vielen Dank, dass Sie Hangman gespielt haben!");
                            returnGame = false;
                            schleife = false;
                        }
                    }
                }
            }
        }
        static void ZeichneHangman(int verbleibendeVersuche)
        {
            switch (verbleibendeVersuche)
            {
                case 10:
                    Console.WriteLine("              ");
                    Console.WriteLine("              ");
                    Console.WriteLine("              ");
                    Console.WriteLine("              ");
                    Console.WriteLine("              ");
                    Console.WriteLine("              ");
                    Console.WriteLine("==============");

                    break;
                case 9:
                    Console.WriteLine("              ");
                    Console.WriteLine("¦             ");
                    Console.WriteLine("¦             ");
                    Console.WriteLine("¦             ");
                    Console.WriteLine("¦             ");
                    Console.WriteLine("¦             ");
                    Console.WriteLine("¦             ");
                    Console.WriteLine("==============");
                    break;
                case 8:
                    Console.WriteLine("+-------+     ");
                    Console.WriteLine("¦             ");
                    Console.WriteLine("¦             ");
                    Console.WriteLine("¦             ");
                    Console.WriteLine("¦             ");
                    Console.WriteLine("¦             ");
                    Console.WriteLine("¦             ");
                    Console.WriteLine("==============");

                    break;
                case 7:
                    Console.WriteLine("+-------+     ");
                    Console.WriteLine("¦       ¦     ");
                    Console.WriteLine("¦             ");
                    Console.WriteLine("¦             ");
                    Console.WriteLine("¦             ");
                    Console.WriteLine("¦             ");
                    Console.WriteLine("¦             ");
                    Console.WriteLine("==============");
                    break;
                case 6:
                    Console.WriteLine("+-------+     ");
                    Console.WriteLine("¦       ¦     ");
                    Console.WriteLine("¦       o     ");
                    Console.WriteLine("¦             ");
                    Console.WriteLine("¦             ");
                    Console.WriteLine("¦             ");
                    Console.WriteLine("¦             ");
                    Console.WriteLine("==============");
                    break;
                case 5:
                    Console.WriteLine("+-------+     ");
                    Console.WriteLine("¦       ¦     ");
                    Console.WriteLine("¦       o     ");
                    Console.WriteLine("¦       ¦     ");
                    Console.WriteLine("¦       ¦     ");
                    Console.WriteLine("¦             ");
                    Console.WriteLine("¦             ");
                    Console.WriteLine("==============");
                    break;
                case 4:
                    Console.WriteLine("+-------+     ");
                    Console.WriteLine("¦       ¦     ");
                    Console.WriteLine("¦       o     ");
                    Console.WriteLine("¦      /¦     ");
                    Console.WriteLine("¦       ¦     ");
                    Console.WriteLine("¦             ");
                    Console.WriteLine("¦             ");
                    Console.WriteLine("==============");
                    break;
                case 3:
                    Console.WriteLine("+-------+     ");
                    Console.WriteLine("¦       ¦     ");
                    Console.WriteLine("¦       o     ");
                    Console.WriteLine("¦      /¦\\   ");
                    Console.WriteLine("¦       ¦     ");
                    Console.WriteLine("¦             ");
                    Console.WriteLine("¦             ");
                    Console.WriteLine("==============");
                    break;
                case 2:
                    Console.WriteLine("+-------+     ");
                    Console.WriteLine("¦       ¦     ");
                    Console.WriteLine("¦       o     ");
                    Console.WriteLine("¦      /¦\\   ");
                    Console.WriteLine("¦       ¦     ");
                    Console.WriteLine("¦      /      ");
                    Console.WriteLine("¦             ");
                    Console.WriteLine("==============");
                    break;
                case 1:
                    Console.WriteLine("+-------+     ");
                    Console.WriteLine("¦       ¦     ");
                    Console.WriteLine("¦       o     ");
                    Console.WriteLine("¦      /¦\\   ");
                    Console.WriteLine("¦       ¦     ");
                    Console.WriteLine("¦      / \\   ");
                    Console.WriteLine("¦             ");
                    Console.WriteLine("==============");
                    break;
                case 0:
                    Console.WriteLine("+-------+     ");
                    Console.WriteLine("¦       ¦     ");
                    Console.WriteLine("¦       o     ");
                    Console.WriteLine("¦      /¦\\   ");
                    Console.WriteLine("¦       ¦     ");
                    Console.WriteLine("¦      / \\   ");
                    Console.WriteLine("¦             ");
                    Console.WriteLine("==============");
                    break;
            }
        }
    }
}
