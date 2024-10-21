using System;
using System.Collections.Generic;

namespace SortingApp
{
    public interface ISortingStrategy
    {
        void Sort<T>(List<T> list) where T : IComparable;
    }

    public class BubbleSort : ISortingStrategy
    {
        public void Sort<T>(List<T> list) where T : IComparable
        {
            for (int i = 0; i < list.Count - 1; i++)
            {
                for (int j = 0; j < list.Count - i - 1; j++)
                {
                    if (list[j].CompareTo(list[j + 1]) > 0)
                    {
                        var temp = list[j];
                        list[j] = list[j + 1];
                        list[j + 1] = temp;
                    }
                }
            }
        }
    }

    public class QuickSort : ISortingStrategy
    {
        public void Sort<T>(List<T> list) where T : IComparable
        {
            QuickSortHelper(list, 0, list.Count - 1);
        }

        private void QuickSortHelper<T>(List<T> list, int low, int high) where T : IComparable
        {
            if (low < high)
            {
                int pi = Partition(list, low, high);
                QuickSortHelper(list, low, pi - 1);
                QuickSortHelper(list, pi + 1, high);
            }
        }

        private int Partition<T>(List<T> list, int low, int high) where T : IComparable
        {
            T pivot = list[high];
            int i = (low - 1);
            for (int j = low; j < high; j++)
            {
                if (list[j].CompareTo(pivot) < 0)
                {
                    i++;
                    var temp = list[i];
                    list[i] = list[j];
                    list[j] = temp;
                }
            }
            var temp1 = list[i + 1];
            list[i + 1] = list[high];
            list[high] = temp1;
            return i + 1;
        }
    }

    public class MergeSort : ISortingStrategy
    {
        public void Sort<T>(List<T> list) where T : IComparable
        {
            MergeSortHelper(list, 0, list.Count - 1);
        }

        private void MergeSortHelper<T>(List<T> list, int left, int right) where T : IComparable
        {
            if (left < right)
            {
                int middle = (left + right) / 2;
                MergeSortHelper(list, left, middle);
                MergeSortHelper(list, middle + 1, right);
                Merge(list, left, middle, right);
            }
        }

        private void Merge<T>(List<T> list, int left, int middle, int right) where T : IComparable
        {
            int leftSize = middle - left + 1;
            int rightSize = right - middle;
            var leftArray = new List<T>(leftSize);
            var rightArray = new List<T>(rightSize);

            for (int i = 0; i < leftSize; i++)
                leftArray.Add(list[left + i]);
            for (int j = 0; j < rightSize; j++)
                rightArray.Add(list[middle + 1 + j]);

            int leftIndex = 0, rightIndex = 0, mergedIndex = left;
            while (leftIndex < leftSize && rightIndex < rightSize)
            {
                if (leftArray[leftIndex].CompareTo(rightArray[rightIndex]) <= 0)
                {
                    list[mergedIndex] = leftArray[leftIndex];
                    leftIndex++;
                }
                else
                {
                    list[mergedIndex] = rightArray[rightIndex];
                    rightIndex++;
                }
                mergedIndex++;
            }

            while (leftIndex < leftSize)
            {
                list[mergedIndex] = leftArray[leftIndex];
                leftIndex++;
                mergedIndex++;
            }

            while (rightIndex < rightSize)
            {
                list[mergedIndex] = rightArray[rightIndex];
                rightIndex++;
                mergedIndex++;
            }
        }
    }
}
