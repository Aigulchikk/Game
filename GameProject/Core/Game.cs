using System;

namespace GameProject.Core
{
    public class Game
    {
        private bool isRunning = true;

        public void Run()
        {
            Console.WriteLine("Игра запущена! Нажмите ESC для выхода.");

            while (isRunning)
            {
                HandleInput();
                Update();
                Render();

                System.Threading.Thread.Sleep(100); 
            }

            Console.WriteLine("Игра завершена. Спасибо за игру!");
        }

        private void HandleInput()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                
                if (keyInfo.Key == ConsoleKey.Escape)
                {
                    isRunning = false;
                }
            }
        }

        private void Update()
        {

        }

        private void Render()
        {

        }
    }
}