using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1.Utility
{
    public static class MergeSort
    {
        public static int ComparisonCount { get; set; }

        private static void Merge(int[] numbers, int left, int mid, int right)
        {
            var lengthLeft = mid - left + 1;
            var lengthRight = right - mid;
            var leftArray = new int[lengthLeft + 1];
            var rightArray = new int[lengthRight + 1];

            for (var i = 0; i < lengthLeft; i++)
            {
                leftArray[i] = numbers[left + i];
            }

            for (var j = 0; j < lengthRight; j++)
            {
                rightArray[j] = numbers[mid + j + 1];
            }

            leftArray[lengthLeft] = Int32.MaxValue;
            rightArray[lengthRight] = Int32.MaxValue;

            var ii = 0;
            var jj = 0;

            for (var k = left; k <= right; k++)
            {
                if (leftArray[ii] <= rightArray[jj])
                {
                    ComparisonCount++;
                    numbers[k] = leftArray[ii];
                    ii++;
                }
                else
                {
                    numbers[k] = rightArray[jj];
                    jj++;
                }
            }

        }

        private static void Sort(int[] numbers, int left, int right)
        {
            int mid;
            
            if (right > left)
            {
                mid = (right + left) / 2;
                Sort(numbers, left, mid);
                Sort(numbers, (mid + 1), right);

                Merge(numbers, left, mid, right);
            }

        }

        public static void Sort(int[] numbers)
        {
            Sort(numbers, 0, numbers.Length - 1);
        }
    }
}
