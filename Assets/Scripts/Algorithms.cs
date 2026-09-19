using System;
using UnityEngine;

public static class Algorithms
{
    #region Bitonic

    public static void BitonicSort<T>(T[] array) where T : IComparable<T>
    {
        if (array == null || array.Length <= 1)
            return;

        if ((array.Length & (array.Length - 1)) != 0)
            throw new ArgumentException("The size of the array must be a power of 2.");

        Debug.Log("Original: ");

        foreach (T item in array)
            Debug.Log(item);

        BitonicSortRecursive(array, 0, array.Length, true);

        Debug.Log("Sorted: ");

        foreach (T item in array)
            Debug.Log(item);
    }

    private static void BitonicSortRecursive<T>(T[] array, int lowest, int count, bool isAscending)
        where T : IComparable<T>
    {
        if (count > 1)
        {
            int half = count / 2;

            BitonicSortRecursive(array, lowest, half, true); //sort first half in ascending order
            BitonicSortRecursive(array, lowest + half, half, false); //sort second half in descending order

            BitonicMerge(array, lowest, count, isAscending);
        }
    }

    private static void BitonicMerge<T>(T[] array, int lowest, int count, bool isAscending) where T : IComparable<T>
    {
        if (count > 1)
        {
            int half = count / 2;

            for (int i = lowest; i < lowest + half; i++)
            {
                CompareAndSwap(array, i, i + half, isAscending);
            }

            BitonicMerge(array, lowest, half, isAscending);
            BitonicMerge(array, lowest + half, half, isAscending);
        }
    }

    private static void CompareAndSwap<T>(T[] array, int i, int j, bool isAscending) where T : IComparable<T>
    {
        int comparison = array[i].CompareTo(array[j]);

        if ((isAscending && comparison > 0) || (!isAscending && comparison < 0))
        {
            // T temp = array[i];
            // array[i] = array[j];
            // array[j] = temp; 

            (array[i], array[j]) = (array[j], array[i]);
        }
    }

    #endregion Bitonic

    #region Selection

    public static void SelectionSort<T>(T[] array) where T : IComparable<T>
    {
        Debug.Log("Original: ");

        foreach (T item in array)
            Debug.Log(item);

        for (int i = 0; i < array.Length - 1; i++)
        {
            int smallestIdx = i;

            for (int j = i + 1; j < array.Length; j++)
            {
                if (array[j].CompareTo(array[smallestIdx]) < 0)
                {
                    smallestIdx = j;
                }
            }

            if (smallestIdx != i)
            {
                // T temp = array[i];
                // array[i] = array[smallestIdx];
                // array[smallestIdx] = temp;

                (array[i], array[smallestIdx]) = (array[smallestIdx], array[i]);
            }
        }

        Debug.Log("Sorted: ");

        foreach (T item in array)
            Debug.Log(item);
    }

    #endregion Selection

    #region Cocktail

    public static void CocktailShakerSort<T>(T[] array) where T : IComparable<T>
    {
        Debug.Log("Original: ");

        foreach (T item in array)
            Debug.Log(item);

        if (array.Length > 1)
        {
            bool swapped = true;
            int start = 0;
            int end = array.Length - 1;

            while (swapped)
            {
                swapped = false;

                for (int i = start; i < end; i++)
                {
                    if (array[i].CompareTo(array[i + 1]) > 0)
                    {
                        (array[i], array[i + 1]) = (array[i + 1], array[i]);
                        swapped = true;
                    }
                }

                if (!swapped)
                    break;

                swapped = false;

                end--;

                for (int i = end - 1; i >= start; i--)
                {
                    if (array[i].CompareTo(array[i + 1]) > 0)
                    {
                        (array[i], array[i + 1]) = (array[i + 1], array[i]);
                        swapped = true;
                    }
                }

                start++;
            }
        }

        Debug.Log("Sorted: ");

        foreach (T item in array)
            Debug.Log(item);
    }

    #endregion Cocktail

    #region Quick

    public static void QuickSort<T>(T[] array) where T : IComparable<T>
    {
        Debug.Log("Original: ");

        foreach (T item in array)
            Debug.Log(item);

        if (array.Length > 1)
        {
            PerformQuickSort(array, 0, array.Length - 1);
        }

        Debug.Log("Sorted: ");

        foreach (T item in array)
            Debug.Log(item);
    }

    private static void PerformQuickSort<T>(T[] array, int low, int high) where T : IComparable<T>
    {
        if (low < high)
        {
            int pivotIndex = Partition(array, low, high);

            PerformQuickSort(array, low, pivotIndex - 1);
            PerformQuickSort(array, pivotIndex + 1, high);
        }
    }

    private static int Partition<T>(T[] array, int low, int high) where T : IComparable<T>
    {
        T pivot = array[high];

        int i = low - 1;

        for (int j = low; j < high; j++)
        {
            if (array[j].CompareTo(pivot) <= 0)
            {
                i++;

                if (i != j)
                {
                    (array[i], array[j]) = (array[j], array[i]);
                }
            }
        }

        i++;
        if (i != high)
        {
            (array[i], array[high]) = (array[high], array[i]);
        }

        return i;
    }

    #endregion Quick

    #region Radix_LSD

    public static void RadixSort_LSD(int[] array)
    {
        Debug.Log("Original: ");

        foreach (int item in array)
            Debug.Log(item);

        int min = array[0];
        int max = array[0];

        for (int i = 1; i < array.Length; i++)
        {
            if (array[i].CompareTo(min) < 0)
                min = array[i];

            if (array[i].CompareTo(max) > 0)
                max = array[i];
        }

        if (min < 0)
        {
            for (int i = 0; i < array.Length; i++)
                array[i] -= min;

            max -= min;
        }

        for (long exp = 1; max / exp > 0; exp *= 10)
        {
            CountingSort(array, (int)exp);
        }

        if (min < 0)
        {
            for (int i = 0; i < array.Length; i++)
                array[i] += min;
        }

        Debug.Log("Sorted: ");

        foreach (int item in array)
            Debug.Log(item);
    }

    private static void CountingSort(int[] array, int exp)
    {
        int[] output = new int[array.Length];
        int[] count = new int[10];

        for (int i = 0; i < array.Length; i++)
        {
            int digit = array[i] / exp % 10;
            count[digit]++;
        }

        for (int i = 1; i < 10; i++)
        {
            count[i] += count[i - 1];
        }

        for (int i = array.Length - 1; i >= 0; i--)
        {
            int digit = array[i] / exp % 10;
            output[count[digit] - 1] = array[i];
            count[digit]--;
        }

        for (int i = 0; i < array.Length; i++)
        {
            array[i] = output[i];
        }
    }

    #endregion Radix_LSD

    #region Shell

    public static void ShellSort<T>(T[] array) where T : IComparable<T>
    {
        Debug.Log("Original: ");

        foreach (T item in array)
            Debug.Log(item);

        for (int gap = array.Length / 2; gap > 0; gap /= 2)
        {
            for (int i = gap; i < array.Length; i++)
            {
                T temp = array[i];
                int j = i;

                while (j >= gap && array[j - gap].CompareTo(temp) > 0)
                {
                    array[j] = array[j - gap];
                    j -= gap;
                }

                array[j] = temp;
            }
        }

        Debug.Log("Sorted: ");

        foreach (T item in array)
            Debug.Log(item);
    }

    #endregion Shell

    public static void BogoSort<T>(T[] array) where T : IComparable<T>
    {
        throw new System.NotImplementedException();
    }

    public static void RadixSort_MSD<T>(T[] array) where T : IComparable<T>
    {
        throw new System.NotImplementedException();
    }

    public static void IntroSort<T>(T[] array) where T : IComparable<T>
    {
        throw new System.NotImplementedException();
    }

    public static void AdaptiveMergeSort<T>(T[] array) where T : IComparable<T>
    {
        throw new System.NotImplementedException();
    }

    public static void BubbleSort<T>(T[] array) where T : IComparable<T>
    {
        throw new System.NotImplementedException();
    }

    public static void GnomeSort<T>(T[] array) where T : IComparable<T>
    {
        throw new System.NotImplementedException();
    }

    public static void MergeSort<T>(T[] array) where T : IComparable<T>
    {
        throw new System.NotImplementedException();
    }

    public static void HeapSort<T>(T[] array) where T : IComparable<T>
    {
        throw new System.NotImplementedException();
    }

    public static void InsertionSort<T>(T[] array) where T : IComparable<T>
    {
        throw new System.NotImplementedException();
    }
}