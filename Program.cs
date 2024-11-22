using System;
using System.Collections.Generic;
using System.IO;

namespace Hangman
{
    class Program
    {

    
        static string highscoreFilePath = "highscore.txt";
        static int highscore = 0;
        static void Main(string[] args)
        {    

        
            Console.WriteLine("Willkommen zu Hangman");
            LoadHighscore();

            string[] Easywords = { "Hand", "Hund", "Auto", "Baum", "Haus", "Katze", "Wolf", "Apfel", "Banane", "Handy", "Fisch", "Mond", "Sonne", "Tisch", "Buch", "Zug", "Wald", "Ball", "Brot", "Maus" };
            string[] MediumWords = { "Lampe", "Keller", "Fenster", "Koffer", "Kamera", "Wecker", "Brücke", "Gabel", "Zahnarzt", "Erdbeere", "Wand", "Lehrer", "Zugfahrt", "Freund", "Dorf", "Schule", "Löffel", "Wolke", "Uhrwerk", "Stadt" };
            string[] Normalwords = { "Fenster", "Garten", "Schmetterling", "Geburtstag", "Frühstück", "Computer", "Abenteuer", "Bratpfanne", "Fahrrad", "Regenschirm", "Schlüssel", "Krawatte", "Wasserfall", "Flugzeug", "Schokolade", "Zeitung", "Teleskop", "Briefkasten", "Taschenlampe", "Fotografie" };
            string[] AdvancedWords = { "Dachboden", "Wassermelone", "Telefonzelle", "Sandkasten", "Tiefgarage", "Dachschaden", "Hausmeister", "Buchhandlung", "Feuerwehr", "Geschirrspüler", "Kopfkissen", "Rettungswagen", "Waschmaschine", "Kabelsalat", "Verkehrsmittel", "Straßenlaterne", "Bilderrahmen", "Notausgang", "Gebäudereiniger", "Fensterbank" };
            string[] Hardwords = { "Enzyklopädie", "Philosophie", "Astronautenanzug", "Thermometer", "Renaissance", "Transkription", "Psychologie", "Wirtschaftswissenschaften", "Katastrophenschutz", "Meteorologie", "Elektrizitätswerk", "Schmetterlingsflügel", "Parallelogramm", "Klimaanlage", "Elektromagnetismus", "Kaffeekanne", "Zentrifugalkraft", "Sauerstoffmaske", "Schwermetallvergiftung", "Gewissensbisse" };


            bool returnGame = true;
            bool difficultyChange = true;
            int trys = 0;
            int points = 0;
            string randomWord = "";
            string difficulty = "";

            while (returnGame)
            {
                if (difficultyChange)
                {
                    Console.Write("Bitte wählen Sie Ihren Schwierigkeitsgrad (");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("einfach");
                    Console.ResetColor();
                    Console.Write("/");
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write("mittel");
                    Console.ResetColor();
                    Console.Write("/");
                    Console.ForegroundColor= ConsoleColor.DarkYellow;
                    Console.Write("normal");
                    Console.ResetColor();
                    Console.Write("/");
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write("fortgeschritten");
                    Console.ResetColor();
                    Console.Write("/");
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write("schwer");
                    Console.ResetColor();
                    Console.Write("):");
                    difficulty = Console.ReadLine();

                    switch (difficulty.ToLower())
                    {
                        case "einfach":
                            randomWord = Easywords[new Random().Next(Easywords.Length)].ToUpper();
                            trys = 10;
                            break;
                        case "normal":
                            randomWord = Normalwords[new Random().Next(Normalwords.Length)].ToUpper();
                            trys = 10;
                            break;
                        case "schwer":
                            randomWord = Hardwords[new Random().Next(Hardwords.Length)].ToUpper();
                            trys = 10;
                            break;
                        case "mittel":
                            randomWord = MediumWords[new Random().Next(MediumWords.Length)].ToUpper();
                            trys = 10;
                            break;
                        case "fortgeschritten":
                            randomWord = AdvancedWords[new Random().Next(AdvancedWords.Length)].ToUpper();
                            trys = 10;
                            break;
                        default:
                            Console.ForegroundColor=ConsoleColor.DarkRed; 
                            Console.WriteLine("Ungültige Eingabe. Bitte versuchen Sie es erneut.");
                            Console.ResetColor();
                            continue;
                    }
                    difficultyChange = false;
                }

                Console.Clear();
                bool game = true;

                while (game)
                {
                    char[] striche = new string('-', randomWord.Length).ToCharArray();
                    List<char> falscheBuchstaben = new List<char>();
                    int verbleibendeVersuche = trys;
                    bool gewonnen = false;

                    while (verbleibendeVersuche > 0 && !gewonnen)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Highscore: {highscore}");
                        Console.WriteLine($"Punkte: {points}");
                        Console.ResetColor();
                        Console.WriteLine("\n" + new string(striche));
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Falsche Buchstaben: " + string.Join(", ", falscheBuchstaben));
                        Console.ResetColor();
                        ZeichneHangman(verbleibendeVersuche);

                        Console.Write("Bitte geben Sie einen Buchstaben ein: ");
                        char buchstabe;
                        try
                        {
                            buchstabe = char.ToUpper(Console.ReadLine()[0]);
                            if (!Char.IsLetter(buchstabe))
                            {
                                continue;
                            }
                        }
                        catch
                        {
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
                                
                            }
                        }

                        if (new string(striche) == randomWord)
                        {
                            gewonnen = true;
                            points += 1;
                        }

                        if (highscore < points)
                        {
                            highscore = points;
                        }
                    }

                    if (gewonnen)
                    {
                        Console.Clear();
                        Console.Write($"Herzlichen Glückwunsch! Sie haben das Wort ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(randomWord );
                        Console.ResetColor();   
                        Console.WriteLine(" richtig erraten.");    
                        Console.WriteLine("Wenn Sie bereit sind, drücken Sie Enter, um ein neues Wort zu erraten.");
                        Console.ReadKey();

                        
                        randomWord = difficulty.ToLower() switch
                        {
                            "einfach" => Easywords[new Random().Next(Easywords.Length)].ToUpper(),
                            "mittel" => Normalwords[new Random().Next(Normalwords.Length)].ToUpper(),
                            "schwer" => Hardwords[new Random().Next(Hardwords.Length)].ToUpper(),
                            _ => randomWord
                        };

                        game = true; 
                    }
                    else if (verbleibendeVersuche == 0)
                    {
                        Console.Clear();
                        Console.Write($"Leider haben Sie verloren. Das richtige Wort war: ");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(randomWord);
                        Console.ResetColor();
                        Console.WriteLine("Möchten Sie erneut spielen? (Ja/Nein): ");
                        string playAgain = Console.ReadLine().Trim().ToLower();

                        if (playAgain == "ja")
                        {
                            points = 0;
                            difficultyChange = true;
                            game = false;
                        }
                        else
                        {
                            Console.WriteLine("Vielen Dank, dass Sie Hangman gespielt haben!");
                            returnGame = false;
                            game = false;
                        }
                    }
                }
            }
            SaveHighscore();
        }
        static void LoadHighscore()
        {
            if (File.Exists(highscoreFilePath))
            {
                try
                {
                    string highscoreText = File.ReadAllText(highscoreFilePath);
                    highscore = int.Parse(highscoreText);
                }
                catch
                {
                    Console.WriteLine("Fehler beim Laden des Highscores. Der Highscore wird auf 0 gesetzt.");
                    highscore = 0;
                }
            }
            else
            {
                highscore = 0; 
            }
        }

       
        static void SaveHighscore()
        {
            try
            {
                File.WriteAllText(highscoreFilePath, highscore.ToString());
            }
            catch
            {
                Console.WriteLine("Fehler beim Speichern des Highscores.");
            }
        }

        static void ZeichneHangman(int verbleibendeVersuche)
        {
            switch (verbleibendeVersuche)
            {
                case 10:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("              ");
                    Console.WriteLine("              ");
                    Console.WriteLine("              ");
                    Console.WriteLine("              ");
                    Console.WriteLine("              ");
                    Console.WriteLine("              ");
                    Console.WriteLine("==============");
                    Console.ResetColor();   
                    break;
                case 9:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("              ");
                    Console.WriteLine("¦             ");
                    Console.WriteLine("¦             ");
                    Console.WriteLine("¦             ");
                    Console.WriteLine("¦             ");
                    Console.WriteLine("¦             ");
                    Console.WriteLine("¦             ");
                    Console.WriteLine("==============");
                    Console.ResetColor();
                    break;
                case 8:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("+-------+     ");
                    Console.WriteLine("¦             ");
                    Console.WriteLine("¦             ");
                    Console.WriteLine("¦             ");
                    Console.WriteLine("¦             ");
                    Console.WriteLine("¦             ");
                    Console.WriteLine("¦             ");
                    Console.WriteLine("==============");
                    Console.ResetColor();
                    break;
                case 7:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("+-------+     ");
                    Console.WriteLine("¦       ¦     ");
                    Console.WriteLine("¦             ");
                    Console.WriteLine("¦             ");
                    Console.WriteLine("¦             ");
                    Console.WriteLine("¦             ");
                    Console.WriteLine("¦             ");
                    Console.WriteLine("==============");
                    Console.ResetColor();
                    break;
                case 6:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("+-------+     ");
                    Console.WriteLine("¦       ¦     ");
                    Console.WriteLine("¦       o     ");
                    Console.WriteLine("¦             ");
                    Console.WriteLine("¦             ");
                    Console.WriteLine("¦             ");
                    Console.WriteLine("¦             ");
                    Console.WriteLine("==============");
                    Console.ResetColor();
                    break;

                case 5:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("+-------+     ");
                    Console.WriteLine("¦       ¦     ");
                    Console.WriteLine("¦       o     ");
                    Console.WriteLine("¦       ¦     ");
                    Console.WriteLine("¦       ¦     ");
                    Console.WriteLine("¦             ");
                    Console.WriteLine("¦             ");
                    Console.WriteLine("==============");
                    Console.ResetColor();
                    break;
                case 4:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("+-------+     ");
                    Console.WriteLine("¦       ¦     ");
                    Console.WriteLine("¦       o     ");
                    Console.WriteLine("¦      /¦     ");
                    Console.WriteLine("¦       ¦     ");
                    Console.WriteLine("¦             ");
                    Console.WriteLine("¦             ");
                    Console.WriteLine("==============");
                    Console.ResetColor();
                    break;
                case 3:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("+-------+     ");
                    Console.WriteLine("¦       ¦     ");
                    Console.WriteLine("¦       o     ");
                    Console.WriteLine("¦      /¦\\   ");
                    Console.WriteLine("¦       ¦     ");
                    Console.WriteLine("¦             ");
                    Console.WriteLine("¦             ");
                    Console.WriteLine("==============");
                    Console.ResetColor();
                    break;
                case 2:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("+-------+     ");
                    Console.WriteLine("¦       ¦     ");
                    Console.WriteLine("¦       o     ");
                    Console.WriteLine("¦      /¦\\   ");
                    Console.WriteLine("¦       ¦     ");
                    Console.WriteLine("¦      /      ");
                    Console.WriteLine("¦             ");
                    Console.WriteLine("==============");
                    Console.ResetColor();
                    break;
                case 1:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("+-------+     ");
                    Console.WriteLine("¦       ¦     ");
                    Console.WriteLine("¦       o     ");
                    Console.WriteLine("¦      /¦\\   ");
                    Console.WriteLine("¦       ¦     ");
                    Console.WriteLine("¦      / \\   ");
                    Console.WriteLine("¦             ");
                    Console.WriteLine("==============");
                    Console.WriteLine("LETZTER VERSUCH!");
                    Console.ResetColor();
                    break;
                case 0:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("+-------+     ");
                    Console.WriteLine("¦       ¦     ");
                    Console.WriteLine("¦       o     ");
                    Console.WriteLine("¦      /¦\\   ");
                    Console.WriteLine("¦       ¦     ");
                    Console.WriteLine("¦      / \\   ");
                    Console.WriteLine("¦             ");
                    Console.WriteLine("==============");
                    Console.ResetColor();
                    break;
            }
        }
    }
}
