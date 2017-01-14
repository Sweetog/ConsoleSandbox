using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Threading;
using ConsoleApplication1.DataStructures;

namespace ConsoleApplication1.Utility
{
    public static class UtilityCore
    {

        public static bool IsSparse(int n)
        {
            Console.WriteLine(Convert.ToString(n, 2));
            Console.WriteLine(Convert.ToString(n >> 1, 2));
            Console.WriteLine(Convert.ToString(n & (n >> 1)));
            return (n & (n >> 1)) == 0;
        }

        public static int BestPathGet(int[][] map)
        {
            //handle simpler base cases
            if (map == null)
            {
                return 0;
            }
            if (map.Length == 0 || map[0].Length == 0)
            {
                return 0;
            }

            if (map.Length == 1)
            {
                int sum = 0;
                for (int i = 0; i < map.Length; i++)
                {
                    sum += map[i][0];
                }
                return sum;
            }

            //now compute the value of each path through the map
            //each position is the best predecessor of the 3 possible choices
            //plus the current position in the map
            //ie: working[j] = Max(subtotal[j-1], subtotal[j], subtotal[j+1]) + map[i][j]

            //{ 3, 2, 5 }
            //{ 1, 7, 4 }
            //{ 8, 2, 1 }
            int[] working = new int[map.Length];
            int[] subtotal = new int[map.Length];
            int[] temp;

            //store the right first columns in subtotal
            for (int j = 0; j < subtotal.Length; j++)
            {
                subtotal[j] = map[j][0];
            }
            
            //store working totals for each column down
            for (int c = 1; c < map[0].Length; c++)
            {
                for (int r = 0; r < map.Length; r++)
                {
                    if (r == 0)
                    {
                        working[r] = Math.Max(subtotal[r], subtotal[r + 1]) + map[r][c];
                    }
                    else if (r == map[0].Length - 1)
                    {
                        working[r] = Math.Max(subtotal[r], subtotal[r - 1]) + map[r][c];
                    }
                    else
                    {
                        working[r] = Math.Max(subtotal[r - 1], Math.Max(subtotal[r], subtotal[r + 1])) + map[r][c];
                    }
                }
                temp = subtotal;
                subtotal = working;
                working = temp;
            }

            //now pick the best score out of the final results

            return subtotal.Max();
        }

        public static int UniquePaths(int m, int n)
        {
            var maxHeight = m + 2;
            var maxWidth = n + 2;

            var matrix = new int[maxHeight, maxWidth];

            for (var i = 0; i < maxHeight; i++)
            {
                for (var j = 0; j < maxWidth; j++)
                {
                    matrix[i, j] = -1;
                }
            }

            return backTrack(1, 1, m, n, matrix);
        }

        private static int backTrack(int r, int c, int m, int n, int[,] matrix)
        {
            if (r == m && c == n)
                return 1;
            if (r > m || c > n)
                return 0;

            if (matrix[r + 1, c] == -1)
                matrix[r + 1, c] = backTrack(r + 1, c, m, n, matrix);

            if (matrix[r, c + 1] == -1)
                matrix[r, c + 1] = backTrack(r, c + 1, m, n, matrix);

            //if (matrix[r + 1, c + 1] == -1)
            //    matrix[r + 1, c + 1] = backTrack(r + 1, c + 1, m, n, matrix);


            return backTrack(r + 1, c, m, n, matrix) + backTrack(r, c + 1, m, n, matrix);
        }

        public static bool IsOpenCloseStrValid(string input, HashTable<char> parensTable)
        {
            var charArray = input.ToCharArray();
            var tracker = new Stack<char>();

            foreach (var currentChar in charArray)
            {
                if (parensTable.ContainsKey(currentChar.ToString()))
                {
                    tracker.Push(currentChar);
                }
                else
                {
                    if (parensTable.ContainsValue(currentChar))
                    {
                        if (tracker.Count == 0)
                        {
                            return false;
                        }

                        var openingChar = tracker.Pop();
                        var closingValue = parensTable.GetValue(openingChar.ToString());

                        if (currentChar != closingValue)
                        {
                            return false;
                        }
                    }
                }
            }

            return (tracker.Count == 0);
        }

        private static bool IsOpeningOfRespectiveClosingCharacter(this char input, char current)
        {
            return ((input == '(' && current == ')'));
        }

        private static bool IsClosingCharacter(this char input)
        {
            return (input == ')');
        }

        private static bool IsOpeningCharacter(this char input)
        {
            return (input == '(');
        }

        public static int[] KWayMerge(int[][] lists)
        {
            var result = new List<int>();
            var indexes = new int[lists.Length];

            var minElement = int.MaxValue;
            var minIndex = -1;

            while (true)
            {
                for (int i = 0; i < lists.Length; i++)
                {
                    var lastIndex = indexes[i];

                    var currentArray = lists[i];

                    if (lastIndex < currentArray.Length)
                    {
                        if (currentArray[lastIndex] < minElement)
                        {
                            minElement = currentArray[lastIndex];
                            minIndex = i;
                        }
                    }
                }

                if (minIndex >= 0)
                {
                    indexes[minIndex]++;
                    result.Add(minElement);
                    minElement = int.MaxValue;
                    minIndex = -1;
                }
                else
                {
                    break;
                }
            }

            return result.ToArray();
        }

        public static int BinarySearch(int[] sortedArr, int target)
        {
            return BinarySearch(sortedArr, target, 0, sortedArr.Length - 1);
        }

        private static int BinarySearch(int[] sortedArr, int target, int min, int Max)
        {
            //base case
            if (Max < min)
                return -1;

            var guess = (Max + min) / 2;

            if (sortedArr[guess] == target)
                return guess;

            if (sortedArr[guess] < target)
            {
                min = guess + 1;
            }
            else
            {
                Max = guess - 1;
            }

            return BinarySearch(sortedArr, target, min, Max);
        }

        public static int[] GetUniqueRandoms(int count, int Max)
        {
            var random = new Random();
            var result = new List<int>(count);
            var set = new HashSet<int>();
            for (var i = 0; i < count; i++)
            {
                int num;

                do
                {
                    num = random.Next(1, Max);
                } while (!set.Add(num));

                result.Add(num);
            }
            return result.ToArray();
        }

        public static void SelectionSort(int[] numbers)
        {
            for (var i = 0; i < numbers.Length - 1; i++)
            {
                var pos_min = i;
                var element = numbers[i];
                for (var j = i + 1; j < numbers.Length; j++)
                {
                    if (numbers[j] < element)
                    {
                        element = numbers[j];
                        pos_min = j;
                    }
                }

                if (pos_min == i) continue;

                var temp = numbers[i];
                numbers[i] = numbers[pos_min];
                numbers[pos_min] = temp;
            }
        }

        public static int LcmOfRange()
        {
            const int min = 2;
            const int Max = 20;
            var accum = min;

            for (var i = min; i <= Max; i++)
            {
                //LCM(x0,x1,x2) = LCM(x0,LCM(x1,x2))
                accum = lcm(accum, i);
            }

            return accum;
        }

        private static int gcd(int a, int b)
        {
            return b == 0 ? a : gcd(b, a % b);
        }

        private static int lcm(int a, int b)
        {
            return a / gcd(a, b) * b;
        }

        public static Int64 SumFibonacciEven()
        {
            const long n = 4000000;
            Int64 term1 = 1;//previous term 1
            Int64 term2 = 2;//previous term 2
            Int64 sum = 0;

            while (term2 < n)
            {
                if (term2 % 2 == 0)//if second term is even sum it
                    sum += term2;

                Int64 temp = term1; //store first term in sequence
                term1 = term2; //set first term for next loop iteration to current second term
                term2 = temp + term2; //add first and second term for current iteration creating second term for next iteration

            }

            return sum;
        }

        public static void PythagoreanTriplet()
        {
            int sum = 1000;
            int a;

            for (a = 1; a <= 1000 / 3; a++)
            {
                int b;
                for (b = a + 1; b <= 1000 / 2; b++)
                {
                    int c = sum - (a + b);

                    if (a * a + b * b == c * c)
                    {
                        Console.WriteLine("a={0},b={1},c={2}", a, b, c);
                        Console.WriteLine(a * b * c);
                    }
                }
            }
        }

        public static Int64 FindLargestAdjacentProductBigNumber()
        {
            string number =
"7316717653133062491922511967442657474235534919493496983520312774506326239578318016984801869478851843858615607891129494954595017379583319528532088055111254069874715852386305071569329096329522744304355766896648950445244523161731856403098711121722383113622298934233803081353362766142828064444866452387493035890729629049156044077239071381051585930796086670172427121883998797908792274921901699720888093776657273330010533678812202354218097512545405947522435258490771167055601360483958644670632441572215539753697817977846174064955149290862569321978468622482839722413756570560574902614079729686524145351004748216637048440319989000889524345065854122758866688116427171479924442928230863465674813919123162824586178664583591245665294765456828489128831426076900422421902267105562632111110937054421750694165896040807198403850962455444362981230987879927244284909188845801561660979191338754992005240636899125607176060588611646710940507754100225698315520005593572972571636269561882670428252483600823257530420752963450";

            var chars = number.ToCharArray();
            var products = new List<Int64>();

            for (var i = 0; i < chars.Length - 12; i++) //last 13 digits do not matter, they contain a zero
            {
                var text = new String(chars, i, 13);
                if (text.Contains("0"))
                    continue;

                Int64 temp = 1;
                for (var j = i; j < i + 13; j++)
                {
                    temp *= (Int64)Char.GetNumericValue(chars[j]);
                }

                products.Add(temp);

            }

            return products.Max();
        }
        public static int Largest3DigitPalindrome()
        {
            var palinDromes = new List<int>();

            for (var i = 999; i > 100; i--)
            {
                for (var j = 999; j > 100; j--)
                {
                    var product = i * j;
                    if (IsPalindrome(product))
                        palinDromes.Add(product);
                }
            }

            return palinDromes.Max();
        }

        private static bool IsPalindrome(int number)
        {
            return number.ToString() == new string(number.ToString().ToCharArray().Reverse().ToArray());

        }

        public static Int64 LargestPrimeNumber()
        {
            Int64 number, factor;
            number = 600851475143;
            for (factor = 2; number > 1; factor++)
            {
                if (number % factor != 0) continue;

                int x = 0;
                while (number % factor == 0)
                {
                    number /= factor;
                    x++;
                }
                Console.WriteLine("{0} is a prime factor {1} times!", factor, x);
            }

            return factor;
        }
    }
}
