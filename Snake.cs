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
        int level = 1;
        int gameSpeed = 150;

        List<int> teljePositie = new List<int>();
        teljePositie.Add(hoofd.xPos);
        teljePositie.Add(hoofd.yPos);

        string obstacle = "*";
        int obstacleXpos = randomnummer.Next(1, screenwidth - 1);
        int obstacleYpos = randomnummer.Next(1, screenheight - 1);

        while (true)
        {
            Console.Clear();

            // Rysowanie przeszkody (jedzenie)
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(obstacleXpos, obstacleYpos);
            Console.Write(obstacle);

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

            // Wyswietlanie wyniku
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(1, 0);
            Console.Write(" Score: " + score + " | Level: " + level + " ");

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

            // Zjedzenie przeszkody
            if (hoofd.xPos == obstacleXpos && hoofd.yPos == obstacleYpos)
            {
                score++;
                obstacleXpos = randomnummer.Next(2, screenwidth - 2);
                obstacleYpos = randomnummer.Next(2, screenheight - 2);

                // Przyspieszenie co 3 punkty
                if (score % 3 == 0 && gameSpeed > 50)
                {
                    gameSpeed -= 15;
                    level++;
                }
            }
            else
            {
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
                Console.WriteLine("Poziom: " + level);
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
                    Console.WriteLine("Poziom: " + level);
                    Console.ReadKey();
                    Environment.Exit(0);
                }
            }

            Thread.Sleep(gameSpeed);
        }
    }
}
