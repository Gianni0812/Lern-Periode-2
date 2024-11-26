using System;
using System.Collections.Generic;
using System.IO;

namespace Hangman
{
    class Programm
    {
        static string highscoreFilePath = "highscore.txt";
        static int highscore = 0;
        static string randomWord = "";
        static int trys = 0;
        static int points = 0;
        static string lastDifficulty = ""; 
        static List<string> userWords = new List<string>(); 

        static string[] Easywords = { "Hand", "Hund", "Auto", "Baum", "Haus", "Katze", "Wolf", "Apfel", "Banane", "Handy", "Fisch", "Mond", "Sonne", "Tisch", "Buch", "Zug", "Wald", "Ball", "Brot", "Maus" };
        static string[] MediumWords = { "Lampe", "Keller", "Fenster", "Koffer", "Kamera", "Wecker", "Brücke", "Gabel", "Zahnarzt", "Erdbeere", "Wand", "Lehrer", "Zugfahrt", "Freund", "Dorf", "Schule", "Löffel", "Wolke", "Uhrwerk", "Stadt" };
        static string[] Normalwords = { "Fenster", "Garten", "Schmetterling", "Geburtstag", "Frühstück", "Computer", "Abenteuer", "Bratpfanne", "Fahrrad", "Regenschirm", "Schlüssel", "Krawatte", "Wasserfall", "Flugzeug", "Schokolade", "Zeitung", "Teleskop", "Briefkasten", "Taschenlampe", "Fotografie" };
        static string[] AdvancedWords = { "Dachboden", "Wassermelone", "Telefonzelle", "Sandkasten", "Tiefgarage", "Dachschaden", "Hausmeister", "Buchhandlung", "Feuerwehr", "Geschirrspüler", "Kopfkissen", "Rettungswagen", "Waschmaschine", "Kabelsalat", "Verkehrsmittel", "Straßenlaterne", "Bilderrahmen", "Notausgang", "Gebäudereiniger", "Fensterbank" };
        static string[] Hardwords = { "Enzyklopädie", "Philosophie", "Astronautenanzug", "Thermometer", "Renaissance", "Transkription", "Psychologie", "Wirtschaftswissenschaften", "Katastrophenschutz", "Meteorologie", "Elektrizitätswerk", "Schmetterlingsflügel", "Parallelogramm", "Klimaanlage", "Elektromagnetismus", "Kaffeekanne", "Zentrifugalkraft", "Sauerstoffmaske", "Schwermetallvergiftung", "Gewissensbisse" };

        static void Main(string[] args)
        {
            LoadHighscore();
            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.WriteLine("----------------------");
                Console.WriteLine("       Hangman       ");
                Console.WriteLine("----------------------");
                Console.WriteLine("    1. Schwierigkeit   ");
                Console.WriteLine("    2. Eigene Wörter   ");
                Console.WriteLine("    3. Starten         ");
                Console.WriteLine("    4. Beenden         ");
                Console.WriteLine("----------------------");
                Console.Write("Was wollen Sie ausführen? ");
                string inputMenu = Console.ReadLine();

                switch (inputMenu)
                {
                    case "1":
                        randomWord = Schwierigkeit();
                        break;

                    case "2":
                        ManageUserWords();
                        break;

                    case "3":
                        if (string.IsNullOrEmpty(randomWord))
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Sie haben noch kein Wort ausgewählt! Wählen Sie ein Wort mit Option 1 oder 2.");
                            Console.ResetColor();
                            Console.WriteLine("Drücken Sie eine beliebige Taste, um zurückzukehren");
                            Console.ReadKey();
                        }
                        else
                        {
                            PlayGame();
                        }
                        break;

                    case "4":
                        running = ConfirmExit();
                        break;

                    default:
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Ungültige Eingabe! Bitte versuchen Sie es erneut.");
                        Console.ResetColor();
                        Console.ReadKey();
                        break;
                }
            }
        }

        static string Schwierigkeit()
        {
            bool difficultySelected = false;
            string difficulty = "";
            string word = "";

            while (!difficultySelected)
            {
                Console.Clear();
                Console.Write("Bitte wählen Sie Ihren Schwierigkeitsgrad (einfach/mittel/normal/fortgeschritten/schwer): ");
                difficulty = Console.ReadLine()?.ToLower();
                lastDifficulty = difficulty; 

                switch (difficulty)
                {
                    case "einfach":
                        word = Easywords[new Random().Next(Easywords.Length)].ToUpper();
                        trys = 10;
                        difficultySelected = true;
                        break;

                    case "mittel":
                        word = MediumWords[new Random().Next(MediumWords.Length)].ToUpper();
                        trys = 10;
                        difficultySelected = true;
                        break;

                    case "normal":
                        word = Normalwords[new Random().Next(Normalwords.Length)].ToUpper();
                        trys = 10;
                        difficultySelected = true;
                        break;

                    case "fortgeschritten":
                        word = AdvancedWords[new Random().Next(AdvancedWords.Length)].ToUpper();
                        trys = 10;
                        difficultySelected = true;
                        break;

                    case "schwer":
                        word = Hardwords[new Random().Next(Hardwords.Length)].ToUpper();
                        trys = 10;
                        difficultySelected = true;
                        break;

                    case "user":
                        if (userWords.Count == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Ihre Wortliste ist leer! Fügen Sie zuerst Wörter hinzu.");
                            Console.ResetColor();
                            Console.WriteLine("Drücken Sie eine beliebige Taste, um zurückzukehren...");
                            Console.ReadKey();
                        }
                        else
                        {
                            word = userWords[new Random().Next(userWords.Count)].ToUpper();
                            trys = 10; 
                            difficultySelected = true;
                        }
                        break;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Ungültige Eingabe! Bitte versuchen Sie es erneut.");
                        Console.ResetColor();
                        Console.ReadKey();
                        break;
                }
            }
            return word;
        }

        static void ManageUserWords()
        {
            bool managing = true;

            while (managing)
            {
                Console.Clear();
                Console.WriteLine("Eigene Wörter verwalten:");
                Console.WriteLine("1. Wort hinzufügen");
                Console.WriteLine("2. Aktuelle Wörter anzeigen");
                Console.WriteLine("3. Als aktive Wortliste auswählen");
                Console.WriteLine("4. Zurück zum Hauptmenü");
                Console.Write("Wählen Sie eine Option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Geben Sie ein neues Wort ein: ");
                        string newWord = Console.ReadLine();
                        if (!string.IsNullOrEmpty(newWord))
                        {
                            userWords.Add(newWord.ToUpper());
                            Console.WriteLine($"Das Wort '{newWord}' wurde hinzugefügt.");
                        }
                        else
                        {
                            Console.WriteLine("Leeres Wort kann nicht hinzugefügt werden!");
                        }
                        Console.WriteLine("Drücken Sie eine beliebige Taste, um fortzufahren...");
                        Console.ReadKey();
                        break;

                    case "2":
                        Console.WriteLine("Aktuelle eigene Wörter:");
                        if (userWords.Count == 0)
                        {
                            Console.WriteLine("Keine Wörter verfügbar.");
                        }
                        else
                        {
                            foreach (string word in userWords)
                            {
                                Console.WriteLine(word);
                            }
                        }
                        Console.WriteLine("Drücken Sie eine beliebige Taste, um fortzufahren...");
                        Console.ReadKey();
                        break;

                    case "3":
                        if (userWords.Count == 0)
                        {
                            Console.WriteLine("Es gibt keine Wörter in Ihrer Liste. Bitte fügen Sie zuerst Wörter hinzu.");
                        }
                        else
                        {
                            lastDifficulty = "user"; 
                            randomWord = userWords[new Random().Next(userWords.Count)].ToUpper();
                            Console.WriteLine("Ihre eigene Wortliste wurde als aktive Liste ausgewählt.");
                        }
                        Console.WriteLine("Drücken Sie eine beliebige Taste, um fortzufahren...");
                        Console.ReadKey();
                        break;

                    case "4":
                        managing = false;
                        break;

                    default:
                        Console.WriteLine("Ungültige Eingabe. Bitte versuchen Sie es erneut.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void PlayGame()
        {
            bool playAgain = true;

            while (playAgain)
            {
                StartGame();

                Console.WriteLine("Möchten Sie noch einmal spielen? (ja/nein)");
                string input = Console.ReadLine()?.ToLower();

                if (input == "ja")
                {
                    randomWord = GenerateNewRandomWord(); 
                }
                else
                {
                    playAgain = false;
                }
            }
        }

        static string GenerateNewRandomWord()
        {
            if (lastDifficulty == "user" && userWords.Count > 0)
            {
                trys = 10;
                return userWords[new Random().Next(userWords.Count)].ToUpper();
            }

            return lastDifficulty switch
            {
                "einfach" => Easywords[new Random().Next(Easywords.Length)].ToUpper(),
                "mittel" => MediumWords[new Random().Next(MediumWords.Length)].ToUpper(),
                "normal" => Normalwords[new Random().Next(Normalwords.Length)].ToUpper(),
                "fortgeschritten" => AdvancedWords[new Random().Next(AdvancedWords.Length)].ToUpper(),
                "schwer" => Hardwords[new Random().Next(Hardwords.Length)].ToUpper(),
                _ => randomWord
            };
        }

        static void StartGame()
        {
            char[] guessedWord = new string('-', randomWord.Length).ToCharArray();
            List<char> incorrectGuesses = new List<char>();
            int remainingAttempts = trys;
            bool won = false;

            while (remainingAttempts > 0 && !won)
            {
                Console.Clear();
                Console.WriteLine($"Highscore: {highscore}");
                Console.WriteLine($"Punkte: {points}");
                Console.WriteLine($"Wort: {new string(guessedWord)}");
                Console.WriteLine($"Falsche Buchstaben: {string.Join(", ", incorrectGuesses)}");
                ZeichneHangman(remainingAttempts);

                Console.Write("Bitte geben Sie einen Buchstaben ein: ");
                char input;
                try
                {
                    input = char.ToUpper(Console.ReadLine()[0]);
                    if (!char.IsLetter(input))
                        throw new Exception();
                }
                catch
                {
                    Console.WriteLine("Ungültige Eingabe. Drücken Sie eine Taste, um es erneut zu versuchen...");
                    Console.ReadKey();
                    continue;
                }

                if (randomWord.Contains(input))
                {
                    for (int i = 0; i < randomWord.Length; i++)
                    {
                        if (randomWord[i] == input)
                        {
                            guessedWord[i] = input;
                        }
                    }
                }
                else if (!incorrectGuesses.Contains(input))
                {
                    incorrectGuesses.Add(input);
                    remainingAttempts--;
                }

                if (new string(guessedWord) == randomWord)
                {
                    won = true;
                    points++;
                    if (points > highscore)
                    {
                        highscore = points;
                        SaveHighscore();
                    }
                }
            }

            if (won)
            {
                Console.Clear();
                Console.WriteLine($"Herzlichen Glückwunsch! Das Wort war: {randomWord}");
                Console.WriteLine("Drücken Sie eine beliebige Taste, um fortzufahren...");
                Console.ReadKey();
            }
            else
            {
                Console.Clear();
                Console.WriteLine($"Sie haben verloren. Das richtige Wort war: {randomWord}");
                Console.WriteLine("Drücken Sie eine beliebige Taste, um fortzufahren...");
                Console.ReadKey();
            }
        }

        static bool ConfirmExit()
        {
            Console.Clear();
            Console.Write("Möchten Sie das Programm wirklich beenden? (ja/nein): ");
            string input = Console.ReadLine()?.ToLower();

            return input != "ja";
        }

        static void LoadHighscore()
        {
            if (File.Exists(highscoreFilePath))
            {
                string highscoreText = File.ReadAllText(highscoreFilePath);
                int.TryParse(highscoreText, out highscore);
            }
        }

        static void SaveHighscore()
        {
            File.WriteAllText(highscoreFilePath, highscore.ToString());
        }

        static void ZeichneHangman(int remainingAttempts)
        {

            switch (remainingAttempts)
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
