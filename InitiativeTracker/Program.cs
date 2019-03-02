using System;
using System.Collections.Generic;
using System.Linq;

namespace InitiativeTracker
{
    class Program
    {
        static string[] names = new string[]
        {
            "LaTierra",
            "Kevin",
            "Josh",
            "Humphrey",
            "Aaron",
            "Nick",
            "Nicholas",
            "Tai",
            "John",
            "Keith",
            "Mike",
            "Colten",
            "Luke"
        };
        static int topPosition = 1;

        static void Main(string[] args)
        {
            string name;
            int quantity;

            var canvas = new Canvas(new Console(), 50, 50);

            // USE MVC pattern

            canvas.Header = "Enter name:";

            while (true)
            {
                var keyInfo = System.Console.ReadKey();

                if (char.IsLetterOrDigit(keyInfo.KeyChar))
                {
                    canvas.CurrentLine.Append(keyInfo.KeyChar);
                }
                else if (keyInfo.Key == ConsoleKey.Backspace)
                {
                    canvas.CurrentLine.Remove(canvas.CurrentLine.Length - 1, 1);
                }                

                string bestMatch = BestMatch(names, canvas.CurrentLine.ToString());

                if (keyInfo.Key == ConsoleKey.Enter && names.Contains(bestMatch))
                {
                    name = bestMatch;
                    break;
                }
                else
                {
                    CompleteWord(canvas.CurrentLine.ToString(), bestMatch);
                }
            }

            canvas.Header = "Quantity:";

            //while (true)
            //{
            //    if (int.TryParse(Console.ReadLine(), out int result))
            //    {
            //        quantity = result;
            //        break;
            //    }
            //}

            //Console.Clear();
            //Console.WriteLine($"Rolled 15 for quantity {quantity} of {name}");

            //Console.ReadKey();
        }

        static void CompleteWord(string currentLine, string bestMatch)
        {
            int pos = currentLine.Length;

            Console.CursorVisible = false;
            Console.Clear();
            Console.WriteLine("Enter name:");
            Console.CursorTop = topPosition;

            if (bestMatch.Length >= pos && pos > 0)
            {
                string rest = bestMatch.Substring(pos, bestMatch.Length - pos);

                for (int i = 0; i < bestMatch.Length; i++)
                {
                    if (i < currentLine.Length)
                    {
                        if (currentLine.ToLower()[i] != bestMatch.ToLower()[i])
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.Write(bestMatch[i]);
                        }
                        else
                        {
                            Console.ResetColor();
                            Console.Write(currentLine[i]);
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write(bestMatch[i]);
                    }
                }

                Console.SetCursorPosition(pos, topPosition);
                Console.ResetColor();
            }
            else
                Console.Write(currentLine);

            Console.CursorVisible = true;
        }

        static IEnumerable<string> StartsWithFirstThreeCharacters(IEnumerable<string> names, string input)
        {
            return names.Where(name => name.ToLower().StartsWith(input.Substring(0, Math.Min(input.Length, 3)).ToLower()));
        }

        static string ClosestMatch(IEnumerable<string> names, string input)
        {
            return names.ToDictionary(name => name, name => Levenshtein.Score(input.ToLower(), name.ToLower()))
                .OrderBy(kvp => kvp.Value)
                .FirstOrDefault().Key;
        }

        static string BestMatch(IEnumerable<string> names, string input)
        {
            var startsWith = StartsWithFirstThreeCharacters(names, input);

            if (startsWith.Count() > 0)
                return ClosestMatch(startsWith, input) ?? "";
            else
                return ClosestMatch(names, input);
        }
    }
}
