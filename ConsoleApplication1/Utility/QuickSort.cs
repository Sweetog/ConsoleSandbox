using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ConsoleApplication1.Utility
{
    public static class QuickSort
    {
        public static void Sort(int[] list)
        {
            Sort(list, 0, list.Length - 1);
        }

        //{3, 4, 1}
        private static void Sort(int[] list, int low, int high)
        {
            if (low < high)
            {
                var p = Partition(list, low, high);

                Sort(list, low, p - 1);
                Sort(list, p+1, high);

            }

        }

        private static int Partition(int[] list, int low, int high)
        {
            var pivot = list[high];
            var i = low;

            for (var j = low; j < high; j++)
            {
                if (list[j] <= pivot)
                {
                    var temp = list[i];
                    list[i] = list[j];
                    list[j] = temp;
                    i++;
                }
            }

            list[high] = list[i];
            list[i] = pivot;

            return i;
        }
    }
}
