using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using ConsoleApplication1.DataStructures;
using ConsoleApplication1.DataStructures.IntervalTreeLib;
using ConsoleApplication1.Utility;
using System.Numerics;
using System.Text;

namespace ConsoleApplication1
{
    class Program
    {
        static bool T<T>(T t)
        {
            return typeof(T) != t.GetType();
        }



        static void Main(string[] args)
        {
            var sw = new Stopwatch();
            sw.Start();

            try
            {
                MainHashTable();
            }
            catch (Exception ex)
            {
                
            }

            sw.Stop();
            Console.WriteLine(string.Empty);
            Console.WriteLine(string.Empty);
            Console.WriteLine("Time Elapsed: {0}", sw.Elapsed);
        }

        static void insertionSort(int[] ar)
        {
            for (var i = ar.Length - 2; i >= 0; i--)
            {
                var j = i;

                while (j >= 0 && ar[j + 1] < ar[j])
                {
                    var temp = ar[j + 1];
                    ar[j + 1] = ar[j];

                    //print
                    for (var k = 0; k < ar.Length; k++)
                    {
                        Console.Write("{0} ", ar[k]);
                    }
                    Console.WriteLine();

                    ar[j] = temp;
                    j--;
                }
            }

            for (var k = 0; k < ar.Length; k++)
            {
                Console.Write("{0} ", ar[k]);
            }
            Console.WriteLine();

        }

        static BigInteger Factorial(BigInteger f, int next)
        {
            if (next == 0)
            {
                return f;
            }

            return Factorial(f * next, next - 1);
        }

        //static void BSTVisitorForPrint(int nodeVal)
        //{
        //    Console.WriteLine(nodeVal);

        //}

        //   protected static Dictionary<int, string> timeLabels = new Dictionary<int, string>
        //{
        //    {1, "one"}, {2, "two"}, {3, "three"}, {4, "four"}, {5, "five"},  {6, "six"}, {7, "seven"}, {8, "eight"}, {9, "nine"}, 
        //    {10, "ten"}, {11,"eleven"}, {12,"twelve"}, {13,"thirteen"}, {14,"fourteen"}, {15,"quarter"}, 
        //    {16,"sixteen"},   {17,"seventeen"}, {18,"eighteen"}, {19,"nineteen"}, {20,"twenty"},{30,"half"}
        //};


        //static Dictionary<int, string> CreateTimeWordsDict()
        //{
        //    var timeWords = new Dictionary<int, string>();
        //    timeWords.Add(0, "{0} o' clock");


        //    for (var i = 1; i < 60; i++)
        //    {
        //        var t = "past";
        //        var m = "minutes";
        //        var s = string.Empty;

        //        var rem = i;

        //        if (i > 30)
        //        {
        //            t = "to";
        //            rem = 60 - rem;
        //        }

        //        if (i % 15 == 0)
        //        {
        //            s = string.Format("{0} {1}", timeLabels[rem], t);
        //            s += " {0}";
        //            timeWords.Add(i, s);
        //            continue;
        //        }

        //        var minLabel = string.Empty;

        //        if (rem > 20 && rem < 30)
        //        {
        //            rem = rem - 20;
        //            minLabel = timeLabels[20] + " " + timeLabels[rem];
        //        }
        //        else
        //        {
        //            minLabel = timeLabels[rem];
        //        }

        //        if (rem % 10 == 1)
        //        {
        //            m = "minute";
        //        }


        //        s = string.Format("{0} {1} {2}", minLabel, m, t);
        //        s += " {0}";
        //        timeWords.Add(i, s);

        //    }

        //    return timeWords;
        //}

        static char Rotate(char c, int k)
        {

            const int A = (int)'A' - 1; //A = 65
            const int a = (int)'a' - 1; //a = 97
            const int Z = (int)'Z'; //Z = 90
            const int z = (int)'z'; //z = 122

            if (!Char.IsLetter(c)) return c;

            var cInt = (int)c;
            var zComparer = Z;
            var aVal = A;
            var rem = k % 26;

            if (rem != 0)
                k = rem;

            if (Char.IsLower(c))
            {
                zComparer = z;
                aVal = a;
            }



            if (cInt + k <= zComparer)
            {
                return Convert.ToChar(cInt + k);
            }

            var addToA = ((cInt + k) - zComparer) * rem;

            return Convert.ToChar(aVal + addToA);
        }

        static void Encryptor()
        {
            var input = string.Empty;
            do
            {
                input = Console.ReadLine();
                var len = input.Length;
                var sq = Math.Sqrt(len);
                var floor = (int)sq;
                var ceiling = (int)Math.Ceiling(sq);

                if (ceiling < sq)
                {
                    ceiling = ceiling + 1;
                }


                //high
                //hg
                //ih

                for (var i = 0; i < ceiling; i++)
                {
                    for (var j = i; j < len;)
                    {
                        Console.Write(input[j]);
                        j += (int)Math.Sqrt(len);

                        if (floor != ceiling)
                            j++;
                    }

                    Console.Write(" ");
                }

                Console.WriteLine();

            } while (input != "x");
        }

        static int BitsOnCount(BitArray bitArray)
        {
            var count = 0;
            foreach (var bit in bitArray)
            {
                if ((bool)bit)
                    count++;
            }

            return count;
        }

        public static string BinaryString(int x)
        {
            char[] bits = new char[32];
            int i = 0;
            while (x != 0)
            {
                bits[i++] = (x & 1) == 1 ? '1' : '0';
                x >>= 1;
            }

            Array.Reverse(bits, 0, i);
            return new string(bits);
        }

        static void BuyHighSellLow()
        {
            var data = new int[] { 10, 5, 3, 5, 1, 20 };


            var low = data[0];
            var maxDiff = int.MinValue;
            var diff = 0;
            var high = int.MinValue;

            for (var i = 1; i < data.Length; i++)
            {
                diff += data[i] - data[i - 1];

                if (diff > maxDiff)
                {
                    maxDiff = diff;
                    high = data[i];
                }

                if (data[i] < low)
                {
                    diff = 0;
                    low = data[i];
                }
            }


            Console.WriteLine("Buy AMZN when price is: {0}", high - maxDiff);
            Console.WriteLine("Sell AMZN when price is: {0}", high);
        }

        static void MainIntersectingTime()
        {
            var ZERO = new DateTime(2015, 01, 01, 00, 00, 00);

            IntervalTree<int, DateTime> tree = new IntervalTree<int, DateTime>();
            tree.AddInterval(ZERO.AddDays(1), ZERO.AddDays(5), 10);
            tree.AddInterval(ZERO.AddDays(2), ZERO.AddDays(8), 5);
            tree.AddInterval(ZERO.AddDays(4), ZERO.AddDays(6), 20);
            tree.AddInterval(ZERO.AddHours(4), ZERO.AddHours(5), 0);

            var results = tree.GetIntersections().ToList();

            var sum = results.SelectMany(result => result).Sum(num => num.Data);

            Console.WriteLine("Max Discount: {0}", sum);
        }

        static void MainBestPath()
        {
            var goldPlate4x4 = new int[4][];

            goldPlate4x4[0] = new int[] { 3, 2, 9, 4 };
            goldPlate4x4[1] = new int[] { 1, 7, 5, 3 };
            goldPlate4x4[2] = new int[] { 2, 4, 6, 9 };
            goldPlate4x4[3] = new int[] { 1, 6, 2, 5 };

            var goldPlate3x3 = new int[3][];

            //{ 3, 2, 5 }
            //{ 1, 7, 4 }
            //{ 8, 2, 1 }
            goldPlate3x3[0] = new int[] { 3, 2, 5 };
            goldPlate3x3[1] = new int[] { 1, 7, 4 };
            goldPlate3x3[2] = new int[] { 8, 2, 1 };

            Console.WriteLine("How much Gold: {0}", UtilityCore.BestPathGet(goldPlate3x3));
        }

        static void MainStringOpenClose()
        {
            var str1 = "<ad675+-fkmfd>";
            var str2 = "<[((kskfhdbh7)";
            var str3 = "[<<((shfs8))>>]";
            var str4 = "(sasas<sd[qw(1)qw]sd{12}>)";

            var parensHashTable = new HashTable<char>();

            parensHashTable.Add("(", ')');


            Console.WriteLine("{0} IsValid: {1}", str1, UtilityCore.IsOpenCloseStrValid(str1, parensHashTable));
            Console.WriteLine("{0} IsValid {1}", str2, UtilityCore.IsOpenCloseStrValid(str2, parensHashTable));
            Console.WriteLine("{0} IsValid {1}", str3, UtilityCore.IsOpenCloseStrValid(str3, parensHashTable));
            Console.WriteLine("{0} IsValid {1}", str4, UtilityCore.IsOpenCloseStrValid(str4, parensHashTable));
        }

        static void BinaryTreeSameIntegers()
        {

            var bt1 = new BinaryTree<int>();
            var bt2 = new BinaryTree<int>();
            var list1 = new[] { 1, 3, 5, 7, 9, 3 };
            var list2 = new[] { 1, 3, 5, 7, 9, 1, 9, 9 };

            foreach (var number in list1)
            {
                bt1.Add(number);
            }

            foreach (var number in list2)
            {
                bt2.Add(number);
            }

            bt1.Traverse(TreeVisitorForPrint);
            Console.WriteLine("----------------------------");
            bt2.Traverse(TreeVisitorForPrint);

            var sameInts = bt1.ContainsSameT(bt2);

            Console.WriteLine((sameInts) ? "Same!" : "Not Same!");
        }

        static void TreeVisitorForPrint(int nodeVal)
        {
            Console.WriteLine(nodeVal);

        }

        static int MinIntCompare(int i1, int i2)
        {
            if (i1 < i2)
                return 1;
            else if (i1 > i2)
                return -1;
            return 0;
        }


        static void MainKWayMerge()
        {

            var list1 = new[] { 1, 3, 5, 7, 9 };
            var list2 = new[] { 2, 6, 8, 10, 84 };
            var list3 = new[] { 33, 34, 35, 36, 40 };
            var list4 = new[] { 18, 22, 23, 24, 25 };
            var list5 = new[] { 10, 13, 16, 19, 20 };

            var result = UtilityCore.KWayMerge(new[] { list1, list2, list3, list4, list5 });

            foreach (int number in result)
            {
                Console.Write("{0},", number);
            }

        }

        static void MainBinarySearch()
        {
            var numbers = UtilityCore.GetUniqueRandoms(1000, 10000).ToArray();
            MergeSort.Sort(numbers);

            var index = UtilityCore.BinarySearch(numbers, numbers[22]);

            Console.WriteLine("Index is: {0}", index);
        }

        static void MainMergeSort()
        {
            var sw = new Stopwatch();

            //====================================================================
            //Worst Case for 8 elements
            //====================================================================
            var worstCase = new int[] { 0, 1, 2, 3, 4, 5, 6, 7 };
            MergeSortWorseCase.Sort(worstCase);


            sw.Start();
            MergeSort.Sort(worstCase);
            sw.Stop();

            Console.WriteLine("MergeSort Worst, Count: {0}, Time Elapsed: {1}", MergeSort.ComparisonCount, sw.ElapsedTicks);
            MergeSort.ComparisonCount = 0;
            //====================================================================

            Console.WriteLine(string.Empty);
            Console.WriteLine(string.Empty);
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine(string.Empty);
            Console.WriteLine(string.Empty);

            //====================================================================
            //Another Case
            //====================================================================
            var anotherCase = new int[] { 2, 0, 6, 4, 1, 3, 5, 7 };

            sw.Start();
            MergeSort.Sort(anotherCase);
            sw.Stop();

            Console.WriteLine("MergeSort Another, Count: {0}, Time Elapsed: {1}", MergeSort.ComparisonCount, sw.ElapsedTicks);
            MergeSort.ComparisonCount = 0;
            //====================================================================


            Console.WriteLine(string.Empty);
            Console.WriteLine(string.Empty);
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine(string.Empty);
            Console.WriteLine(string.Empty);


            //====================================================================
            //Sorted Case
            //====================================================================
            var sortedCase = new int[] { 0, 1, 2, 3, 4, 5, 6, 7 };

            sw.Start();
            MergeSort.Sort(sortedCase);
            sw.Stop();

            Console.WriteLine("MergeSort Already Sorted, Count: {0}, Time Elapsed: {1}", MergeSort.ComparisonCount, sw.ElapsedTicks);
            MergeSort.ComparisonCount = 0;
            //====================================================================

            Console.WriteLine(string.Empty);
            Console.WriteLine(string.Empty);
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine(string.Empty);
            Console.WriteLine(string.Empty);


            //====================================================================
            //1000 Unique Numbers
            //====================================================================
            var numbers = UtilityCore.GetUniqueRandoms(1000, 10000).ToArray();

            sw.Start();
            MergeSort.Sort(numbers);
            sw.Stop();

            Console.WriteLine("MergeSort 1000 Unique Numbers, Count: {0}, Time Elapsed: {1}",
                MergeSort.ComparisonCount, sw.ElapsedTicks);
            MergeSort.ComparisonCount = 0;
            //====================================================================

            Console.WriteLine(string.Empty);
            Console.WriteLine(string.Empty);
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine(string.Empty);
            Console.WriteLine(string.Empty);
        }

        static void MainTree()
        {

            var tree1 = new BinarySearchTree<int>();
            var tree2 = new BinarySearchTree<int>();
            var numbers = UtilityCore.GetUniqueRandoms(1000, 10000);

            foreach (var num in numbers)
                tree1.Add(num);

            foreach (var num in numbers)
                tree2.Add(num);

            Console.WriteLine("Tree.Size = {0}", tree1.Size);
            Console.WriteLine(string.Empty);


            Console.WriteLine("Tree.Max = {0}", tree1.Max());
            Console.WriteLine("Tree Min = {0}", tree1.Min());

            Console.WriteLine(string.Empty);
            Console.WriteLine(string.Empty);

            var nodeExists = tree1.Exists(numbers[44]);

            Console.WriteLine(nodeExists ? "Node FOUND" : "Node not found!!");


            Console.WriteLine(string.Empty);
            Console.WriteLine(string.Empty);

            Console.WriteLine(tree1.TreeEquals(tree2) ? "Tree1 equals Tree2" : "Trees are not equal!");
            Console.WriteLine(string.Empty);
            Console.WriteLine("Adding new value to Tree2");
            Console.WriteLine(string.Empty);

            tree2.Add(23232);

            Console.WriteLine(tree1.TreeEquals(tree2) ? "Tree1 equals Tree2" : "Trees are not equal!");

            //tree.Traverse(MyVisitor);
        }

        static void MainHashTable()
        {
            var hashTable = new HashTable<string>();

            hashTable.Add("Brian", "Porsche");
            hashTable.Add("Rachael", "Sentra");
            hashTable.Add("Lauren", "Mazada");
            hashTable.Add("Ortopan", "Purple Neon");
            hashTable.Add("Mom", "Generic SUV");

            hashTable.Add("Ortopan", "Mini Van");

            hashTable.Remove("Brian");

            hashTable.Print();

            Console.WriteLine(string.Empty);
            Console.WriteLine("hashTable.Count: {0}", hashTable.Count);
        }
    }

}
