using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            Console.SetWindowSize(50, 50);
            Console.SetBufferSize(50, 50);
            Console.WriteLine("Enter name:");
            Console.CursorTop = topPosition;

            var sb = new StringBuilder();

            while (true)
            {
                var keyInfo = Console.ReadKey();

                if (char.IsLetterOrDigit(keyInfo.KeyChar))
                {
                    sb.Append(keyInfo.KeyChar);
                }
                else if (keyInfo.Key == ConsoleKey.Backspace)
                {
                    Console.Write('\0');
                    Console.CursorLeft = Console.CursorLeft - 1;

                    if (sb.Length > 0)
                        sb.Remove(sb.Length - 1, 1);
                }                

                string bestMatch = BestMatch(names, sb.ToString());

                if (keyInfo.Key == ConsoleKey.Enter && names.Contains(bestMatch))
                {
                    name = bestMatch;
                    break;
                }
                else
                {
                    CompleteWord(sb.ToString(), bestMatch);
                }
            }

            Console.Clear();
            Console.WriteLine("Quantity:");
            Console.CursorTop = topPosition;

            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int result))
                {
                    quantity = result;
                    break;
                }
            }

            Console.Clear();
            Console.WriteLine($"Rolled 15 for quantity {quantity} of {name}");

            Console.ReadKey();
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
