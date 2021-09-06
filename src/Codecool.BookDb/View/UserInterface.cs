using System;

namespace Codecool.BookDb.View
{
    public class UserInterface
    {
        public void PrintLn(Object obj)
        {
            Console.WriteLine(obj);
        }

        public void PrintTitle(string title)
        {
            Console.WriteLine("\n-- " + title + " --");
        }

        public void PrintOption(char option, string description)
        {
            Console.WriteLine("(" + option + ")" + " " + description);
        }

        public char Choice(string options)
        {
            // Given string options -> "abcd"
            // keep asking user for input until one of provided chars is provided
            char pressedKey;

            do
            {
                pressedKey = Console.ReadKey().KeyChar;

                if (options.Contains(pressedKey))
                {
                    break;
                }
                PrintLn("\nBad input, please retry!");
            } while (true);


            return pressedKey;
        }

        public string ReadString(string prompt, string defaultValue)
        {
            // Ask user for data. If no data was provided use default value.
            // User must be informed what the default value is.
            PrintLn(prompt);
            PrintLn($"Default value is : {defaultValue}");
            string inputString = Console.ReadLine();
            if (inputString == string.Empty)
            {
                return defaultValue;
            }
            return inputString;
        }

        public DateTime ReadDate(string prompt, DateTime defaultValue)
        {
            // Ask user for a date. If no data was provided use default value.
            // User must be informed what the default value is.
            // If provided date is in invalid format, ask user again.
            PrintLn(prompt);
            PrintLn($"Default value is : {defaultValue}");
            string inputString = Console.ReadLine();
            DateTime inputDate;
            if (!DateTime.TryParse(inputString, out inputDate))
            {
                return defaultValue;
            }
            return inputDate;
        }
        
        public int ReadInt(string prompt, int defaultValue)
        {
            // Ask user for a number. If no data was provided use default value.
            // User must be informed what the default value is.
            PrintLn(prompt);
            PrintLn($"Default value is : {defaultValue}");
            string inputString = Console.ReadLine();
            int inputInt;
            if (!int.TryParse(inputString, out inputInt))
            {
                return defaultValue;
            }
            return inputInt;
        }

        public void ClearScreen()
        {
            Console.Clear();
        }

        public void WaitForUser()
        {
            PrintLn("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
