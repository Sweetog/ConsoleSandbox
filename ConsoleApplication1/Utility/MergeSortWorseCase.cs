using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1.Utility
{
    public static class MergeSortWorseCase
    {
        
        private static void Merge(int[] numbers, int[] leftArray, int[] rightArray)
        {
            int i, j;
            for (i = 0; i < leftArray.Length; i++)
            {
                numbers[i] = leftArray[i];
            }

            for (j = 0; j < rightArray.Length; j++, i++)
            {
                numbers[i] = rightArray[j];
            }
              
        }

        public static void Sort(int[] numbers)
        {
            //base case
            if (numbers.Length == 1)
                return;
            
            //two elements case as an easy swap is all that is needed
            if (numbers.Length == 2)
            {
                var temp = numbers[0];
                numbers[0] = numbers[1];
                numbers[1] = temp;
                return;
            }

            int i, j;
            var mid = (numbers.Length + 1)/2;
            var leftArray = new int[mid];
            var rightArray = new int[numbers.Length - mid];

            //Storing alternate elements in left subarray
            for (i = 0, j = 0; i < numbers.Length; i = i + 2, j++)
            {
                leftArray[j] = numbers[i];
            }
               
            
            //Storing alternate elements in right subarray
            for (i = 1, j = 0; i < numbers.Length; i = i + 2, j++)
            {
                rightArray[j] = numbers[i];
            }
                

            Sort(leftArray);
            Sort(rightArray);

            Merge(numbers, leftArray, rightArray);
        }
    }
}
