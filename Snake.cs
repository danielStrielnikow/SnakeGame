using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

class Program
{
    static void Main()
    {
        Console.WindowHeight = 16;
        Console.WindowWidth = 32;
        int screenwidth = Console.WindowWidth;
        int screenheight = Console.WindowHeight;
        Random randomnummer = new Random();

        Pixel hoofd = new Pixel();
        hoofd.xPos = screenwidth / 2;
        hoofd.yPos = screenheight / 2;
        hoofd.schermKleur = ConsoleColor.Red;

        string movement = "RIGHT";
        int score = 0;
        int bestScore = 0;

        List<int> teljePositie = new List<int>();
        teljePositie.Add(hoofd.xPos);
        teljePositie.Add(hoofd.yPos);

        // Jedzenie - pozycja i symbol
        string foodSymbol = "@";
        int foodXpos = randomnummer.Next(2, screenwidth - 2);
        int foodYpos = randomnummer.Next(2, screenheight - 2);

        while (true)
        {
            Console.Clear();

            // Rysowanie jedzenia
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.SetCursorPosition(foodXpos, foodYpos);
            Console.Write(foodSymbol);

            // Rysowanie scian
            Console.ForegroundColor = ConsoleColor.White;
            for (int i = 0; i < screenwidth; i++)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write("■");
            }
            for (int i = 0; i < screenwidth; i++)
            {
                Console.SetCursorPosition(i, screenheight - 1);
                Console.Write("■");
            }
            for (int i = 0; i < screenheight; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("■");
            }
            for (int i = 0; i < screenheight; i++)
            {
                Console.SetCursorPosition(screenwidth - 1, i);
                Console.Write("■");
            }

            // Rysowanie ciala weza
            Console.ForegroundColor = ConsoleColor.Green;
            for (int i = 0; i < teljePositie.Count; i += 2)
            {
                Console.SetCursorPosition(teljePositie[i], teljePositie[i + 1]);
                Console.Write("■");
            }

            // Rysowanie glowy weza
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(hoofd.xPos, hoofd.yPos);
            Console.Write("■");

            // Wyswietlanie punktacji
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(1, 0);
            Console.Write(" Score: " + score + " | Best: " + bestScore + " ");

            // Odczyt klawisza
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo info = Console.ReadKey(true);

                // Blokada zawracania w przeciwnym kierunku
                switch (info.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (movement != "DOWN")
                            movement = "UP";
                        break;
                    case ConsoleKey.DownArrow:
                        if (movement != "UP")
                            movement = "DOWN";
                        break;
                    case ConsoleKey.LeftArrow:
                        if (movement != "RIGHT")
                            movement = "LEFT";
                        break;
                    case ConsoleKey.RightArrow:
                        if (movement != "LEFT")
                            movement = "RIGHT";
                        break;
                }
            }

            // Ruch weza
            if (movement == "UP")
                hoofd.yPos--;
            if (movement == "DOWN")
                hoofd.yPos++;
            if (movement == "LEFT")
                hoofd.xPos--;
            if (movement == "RIGHT")
                hoofd.xPos++;

            // Zjedzenie jedzenia
            if (hoofd.xPos == foodXpos && hoofd.yPos == foodYpos)
            {
                score++;
                if (score > bestScore)
                    bestScore = score;

                // Nowe jedzenie w losowym miejscu (nie na scianach)
                foodXpos = randomnummer.Next(2, screenwidth - 2);
                foodYpos = randomnummer.Next(2, screenheight - 2);

                // Zmiana symbolu jedzenia co 5 punktow
                if (score % 5 == 0)
                    foodSymbol = (foodSymbol == "@") ? "#" : "@";
            }
            else
            {
                // Usuwanie ogona tylko gdy waz nie zjadl jedzenia
                if (teljePositie.Count > 0)
                {
                    teljePositie.RemoveAt(teljePositie.Count - 1);
                    teljePositie.RemoveAt(teljePositie.Count - 1);
                }
            }

            teljePositie.Insert(0, hoofd.xPos);
            teljePositie.Insert(1, hoofd.yPos);

            // Kolizja ze scianami
            if (hoofd.xPos == 0 || hoofd.xPos == screenwidth - 1 || hoofd.yPos == 0 || hoofd.yPos == screenheight - 1)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(screenwidth / 5, screenheight / 2);
                Console.WriteLine("Game Over!");
                Console.SetCursorPosition(screenwidth / 5, screenheight / 2 + 1);
                Console.WriteLine("Twoj wynik: " + score);
                Console.SetCursorPosition(screenwidth / 5, screenheight / 2 + 2);
                Console.WriteLine("Najlepszy wynik: " + bestScore);
                Console.ReadKey();
                Environment.Exit(0);
            }

            // Kolizja z samym soba
            for (int i = 2; i < teljePositie.Count; i += 2)
            {
                if (hoofd.xPos == teljePositie[i] && hoofd.yPos == teljePositie[i + 1])
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(screenwidth / 5, screenheight / 2);
                    Console.WriteLine("Game Over!");
                    Console.SetCursorPosition(screenwidth / 5, screenheight / 2 + 1);
                    Console.WriteLine("Twoj wynik: " + score);
                    Console.SetCursorPosition(screenwidth / 5, screenheight / 2 + 2);
                    Console.WriteLine("Najlepszy wynik: " + bestScore);
                    Console.ReadKey();
                    Environment.Exit(0);
                }
            }

            Thread.Sleep(100);
        }
    }
}
