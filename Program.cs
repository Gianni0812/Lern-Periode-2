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
        static int jokers = 3;

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
                Console.WriteLine("       Hangman        ");
                Console.WriteLine("----------------------");
                Console.WriteLine("    1. Schwierigkeit  ");
                Console.WriteLine("    2. Eigene Wörter  ");
                Console.WriteLine("    3. Starten        ");
                Console.WriteLine("    4. Info           ");
                Console.WriteLine("    5. Beenden        ");
                Console.WriteLine("----------------------");
                Console.Write("Was wollen Sie ausführen: ");
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
                        Info();
                        break;

                    case "5":
                        running = ConfirmExit();
                        break;

                    default:
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Ungültige Eingabe! Bitte versuchen Sie es erneut oder geben Sie die Zahl ein");
                        Console.ResetColor();
                        Console.ReadKey();
                        break;
                }
            }
        }
        static void Info()
        {
            bool bedienung = true;
            int currentPage = 0;
            int totalPages = 5;  

            do
            {
                Console.Clear();
                ShowPage(currentPage); 

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                switch (keyInfo.Key)
                {
                    case ConsoleKey.RightArrow:
                        if (currentPage < totalPages - 1) 
                        {
                            currentPage++;
                        }
                        break;

                    case ConsoleKey.LeftArrow:
                        if (currentPage > 0) 
                        {
                            currentPage--;
                        }
                        break;

                    case ConsoleKey.Escape:
                        bedienung = false; 
                        Console.WriteLine("Programm beendet.");
                        break;

                    default:
                        break; 
                }

            } while (bedienung);
        }

        
        static void ShowPage(int pageNumber)
        {
            switch (pageNumber)
            {
                case 0:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("");
                    Console.WriteLine("Erstellt von Gianluca Caruso");
                    Console.WriteLine("Hangmann Spiel");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nAlgemeine Bedienung:");
                    Console.WriteLine("\n 1. Wenn Sie gefragt werden was Sie ausführen wollen,\n schreiben Sie immer die Zahl!");
                    Console.WriteLine("\n 2. Wenn angezeigt wird, dass sie eine Ungültige Eingabe haben,\n Lesen Sie an was es gelegen hat");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\nSeite 1 ");
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("Sie können die Seiten mit den Pfeiltasten ändern.\nMit esc kommen sie zürück zum Hauptmenu");
                    break;

                case 1:                    
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nSpiel;");
                    Console.WriteLine("\n 1. Sie müssen immer Zuerst den Schwierigkeits grad aus wählen. (Mehr dazu s.3");
                    Console.WriteLine("\n 2. Wenn Sie noch 1 Versuch haben, dann kommt unter dem Hangmann eine Warnung.");
                    Console.WriteLine("\n 3. Das Spiel wird beendet, wenn die Zeit abgelaufen\noder wenn Sie alle Versuche aufgebraucht haben.");
                    Console.WriteLine("\n 4. Sie haben 3 Jokers die ein zufälligen Buchstaben aufdeckt,\ndie jedoch nicht zurückgesetzt werden.\nbenutzen Sie die Joker Weise!");
                    Console.WriteLine("\n 5. Sie sehen oben Links Ihren Highscore. Dieser wird gespeichert,\nso dass Sie sich in jedem Spiel verbessern können.");

                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\nSeite 2 ");
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("Sie können die Seiten mit den Pfeiltasten ändern.\nMit esc kommen sie zürück zum Hauptmenu");

                    break;

                case 2:
                    Console.WriteLine("Seite 3: Schwierigkeit wählen");
                    Console.WriteLine("Im Schwierigkeitsmodus können Sie zwischen fünf Stufen wählen.");
                    Console.WriteLine("Jede Stufe hat ein eigenes Zeitlimit.");
                    break;

                case 3:
                    Console.WriteLine("Seite 4: Joker verwenden");
                    Console.WriteLine("Verwenden Sie Joker, um schwierige Wörter leichter zu lösen.");
                    Console.WriteLine("Sie haben nur begrenzt Joker, also setzen Sie sie weise ein.");
                    break;

                case 4:
                    Console.WriteLine("Seite 5: Highscores speichern");
                    Console.WriteLine("Das Spiel speichert Ihren Highscore automatisch.");
                    Console.WriteLine("Versuchen Sie, Ihre eigene Bestleistung zu schlagen!");
                    Console.WriteLine("\nDrücken Sie ESC, um das Info-Büchlein zu verlassen.");
                    break;

                default:
                    Console.WriteLine("Ungültige Seite.");
                    break;
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
            int maxAttempts = trys; 
            bool won = false;
            bool timeUp = false; 

            
            int timeLimit = GetTimeLimitForDifficulty(); 
            DateTime startTime = DateTime.Now;
            DateTime endTime = startTime.AddMinutes(timeLimit);

            
            Task.Run(() =>
            {
                while (DateTime.Now < endTime && !timeUp && remainingAttempts > 0 && !won)
                {
                    Task.Delay(100).Wait();
                }
                if (DateTime.Now >= endTime)
                {
                    timeUp = true; 
                }
            });

            while (remainingAttempts > 0 && !won)
            {
                if (timeUp) 
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Die Zeit ist abgelaufen!");
                    Console.WriteLine($"Das richtige Wort war: {randomWord}");
                    Console.ResetColor();
                    Console.WriteLine("Drücken Sie eine beliebige Taste, um fortzufahren...");
                    Console.ReadKey();
                    return; 
                }

                Console.Clear();
                Console.WriteLine($"Highscore: {highscore}");
                Console.WriteLine($"Punkte: {points}");
                Console.WriteLine($"Wort: {new string(guessedWord)}");
                Console.WriteLine($"Falsche Buchstaben: {string.Join(", ", incorrectGuesses)}");
                Console.WriteLine($"Verbleibende Joker: {jokers}");
                Console.WriteLine($"Verbleibende Zeit: {Math.Max(0, (int)(endTime - DateTime.Now).TotalSeconds)} Sekunden"); 
                ZeichneHangman(remainingAttempts);
                Console.WriteLine("Drücken Sie [2] für Joker");

                Console.Write("Bitte geben Sie Ihre Wahl ein (Buchstabe oder 2 für Joker): ");
                if (Console.KeyAvailable) 
                {
                    string choice = Console.ReadLine()?.Trim();

                    if (choice == "2")
                    {
                        if (jokers > 0)
                        {
                            UseJoker(guessedWord, randomWord);
                        }
                        else
                        {
                            Console.WriteLine("Keine Joker mehr verfügbar!");
                            Console.WriteLine("Drücken Sie eine beliebige Taste, um fortzufahren...");
                            Console.ReadKey();
                        }
                    }
                    else if (choice.Length == 1 && char.IsLetter(choice[0]))
                    {
                        char input = char.ToUpper(choice[0]);

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
                            int basePoints = GetBasePointsForDifficulty();
                            points += CalculatePoints(remainingAttempts, maxAttempts, startTime, DateTime.Now, basePoints);

                            if (points > highscore)
                            {
                                highscore = points;
                                SaveHighscore();
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Ungültige Eingabe. Drücken Sie eine Taste, um es erneut zu versuchen...");
                        Console.ReadKey();
                    }
                }
                else
                {
                    Task.Delay(200).Wait(); 
                }
            }

            if (won)
            {
                Console.Clear();
                Console.WriteLine($"Herzlichen Glückwunsch! Das Wort war: {randomWord}");
                Console.WriteLine($"Sie haben {points} Punkte in dieser Runde erhalten.");
                Console.WriteLine("Drücken Sie eine beliebige Taste, um fortzufahren...");
                Console.ReadKey();
            }
            else if (!timeUp) 
            {
                Console.Clear();
                Console.WriteLine($"Sie haben verloren. Das richtige Wort war: {randomWord}");
                Console.WriteLine("Drücken Sie eine beliebige Taste, um fortzufahren...");
                Console.ReadKey();
            }
        }


        static int GetTimeLimitForDifficulty()
        {
            return lastDifficulty switch
            {
                "einfach" => 1,
                "mittel" => 1,
                "normal" => 2,
                "fortgeschritten" => 3,
                "schwer" => 3,
                _ => 1
            };
        }

        static int GetBasePointsForDifficulty()
        {
            return lastDifficulty switch
            {
                "einfach" => 1,
                "mittel" => 1,
                "normal" => 2,
                "fortgeschritten" => 3,
                "schwer" => 4,
                _ => 1
            };
        }

        static int CalculatePoints(int remainingAttempts, int maxAttempts, DateTime startTime, DateTime endTime, int basePoints)
        {
            int pointsEarned = basePoints;

            
            double totalTime = (endTime - startTime).TotalSeconds;
            double maxTime = GetTimeLimitForDifficulty() * 60; 
            if (totalTime <= maxTime / 2)
            {
                pointsEarned++; 
            }

            
            if (remainingAttempts >= maxAttempts / 2)
            {
                pointsEarned++; 
            }

            return pointsEarned;
        }

        static void UseJoker(char[] guessedWord, string randomWord)
        {
            List<int> notGuessedIndexes = new List<int>();

            for (int i = 0; i < randomWord.Length; i++)
            {
                if (guessedWord[i] == '-')
                {
                    notGuessedIndexes.Add(i);
                }
            }

            if (notGuessedIndexes.Count > 0)
            {
                Random random = new Random();
                int randomIndex = notGuessedIndexes[random.Next(notGuessedIndexes.Count)];
                guessedWord[randomIndex] = randomWord[randomIndex];               
                jokers--;
            }
            else
            {
                Console.WriteLine("Es gibt keine Buchstaben mehr, die aufgedeckt werden können.");
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
