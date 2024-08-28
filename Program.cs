//*****************************************************************************
//**                            Ski Game App                                 **
//** Classic Ascii Game written in C#                                        **
//*****************************************************************************

using System;
using System.Threading;

class SkiGame
{
    const int Width = 80;
    const int Height = 24;
    const int InitialX = 40;
    const int InitialY = 20;
    const int Speed = 100;  // Lower is faster
    const int TreeProbability = 10;  // Percentage chance of tree generation

    static char[,] screen = new char[Height, Width];
    static int x = InitialX, y = InitialY, score = 0;

    static void Main()
    {
        Console.SetWindowSize(Width, Height + 2); // Adjust to fit the game screen and score display
        Console.SetBufferSize(Width, Height + 2); // Ensure buffer size is sufficient

        Random rand = new Random();
        InitializeScreen();
        
        while (true)
        {
            score++;
            DrawScreen();
            HandleInput();

            if (CheckCollision())
            {
                GameOver();
                return;
            }

            ScrollScreen();
            GenerateNewRow(rand);
            Thread.Sleep(Speed);  // Control the game speed
        }
    }

    static void ClearScreen()
    {
        Console.Clear();  // Clears the console screen
    }

    static void InitializeScreen()
    {
        for (int i = 0; i < Height; i++)
        {
            for (int j = 0; j < Width; j++)
            {
                screen[i, j] = ' ';
            }
        }
    }

    static void DrawScreen()
    {
        ClearScreen();
        for (int i = 0; i < Height; i++)
        {
            for (int j = 0; j < Width; j++)
            {
                Console.Write(screen[i, j]);
            }
            Console.WriteLine();
        }
        // Ensure x and y are within the console window bounds
        x = Math.Max(0, Math.Min(x, Width - 1));
        y = Math.Max(0, Math.Min(y, Height - 1));
        Console.SetCursorPosition(x, y);
        Console.Write('*');  // Draw the skier
        Console.SetCursorPosition(0, Height + 1);
        Console.WriteLine($"Score: {score}");
    }

    static void ScrollScreen()
    {
        for (int i = Height - 1; i > 0; i--)
        {
            for (int j = 0; j < Width; j++)
            {
                screen[i, j] = screen[i - 1, j];
            }
        }
    }

    static void GenerateNewRow(Random rand)
    {
        for (int j = 0; j < Width; j++)
        {
            if (rand.Next(100) < TreeProbability)
            {
                screen[0, j] = 'T';
            }
            else
            {
                screen[0, j] = ' ';
            }
        }
    }

    static bool CheckCollision()
    {
        return screen[y, x] == 'T';
    }

    static void HandleInput()
    {
        if (Console.KeyAvailable)
        {
            ConsoleKeyInfo key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.LeftArrow && x > 0) x--;
            if (key.Key == ConsoleKey.RightArrow && x < Width - 1) x++;
        }
    }

    static void GameOver()
    {
        ClearScreen();
        Console.WriteLine($"GAME OVER! YOUR SCORE: {score}");
    }
}
