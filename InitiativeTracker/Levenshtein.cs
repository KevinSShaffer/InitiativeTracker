using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
        k   i   t   t   e   n
    0	1	2	3	4	5	6
s	1	1	2	3	4	5	6
i	2	2	1	2	3	4	5
t	3	3	2	1	2	3	4
t	4	4	3	2	1	2	3
i	5	5	4	3	2	2	3
n	6	6	5	4	3	3	2
g	7	7	6	5	4	4	3

 */

namespace InitiativeTracker
{
    public static class Levenshtein
    {
        public static int Score(string s1, string s2)
        {
            if (s1 == s2)
                return 0;

            var array = InitializeArray(s1, s2);

            CalculateScores(array, s1, s2);

            return array[s1.Length, s2.Length];
        }

        public static void CalculateScores(int[,] array, string s1, string s2)
        {
            Func<int, int, int> substitutionCost = (x, y) => array[x - 1, y - 1] + (s1[x - 1] == s2[y - 1] ? -1 : 0);

            for (int x = 1; x < array.GetLength(0); x++)
                for (int y = 1; y < array.GetLength(1); y++)
                    array[x, y] = Math.Min(substitutionCost(x, y), Math.Min(array[x - 1, y], array[x, y - 1])) + 1;
        }

        public static int[,] InitializeArray(string s1, string s2)
        {
            var array = new int[s1.Length + 1, s2.Length + 1];

            for (int i = 0; i < array.GetLength(0); i++)
                array[i, 0] = i;

            for (int i = 0; i < array.GetLength(1); i++)
                array[0, i] = i;

            return array;
        }
    }
}
