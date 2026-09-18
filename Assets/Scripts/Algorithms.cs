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
    public static void CocktailShakerSort<T>(T[] array) where T : IComparable<T>
    {
        throw new System.NotImplementedException();
    }

    public static void QuickSort<T>(T[] array) where T : IComparable<T>
    {
        throw new System.NotImplementedException();
    }

    public static void RadixSort_LSD<T>(T[] array) where T : IComparable<T>
    {
        throw new System.NotImplementedException();
    }

    public static void ShellSort<T>(T[] array) where T : IComparable<T>
    {
        throw new System.NotImplementedException();
    }

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