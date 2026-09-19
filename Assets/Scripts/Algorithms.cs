using System;
using UnityEngine;

public static class Algorithms
{
    #region O(n)
    
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
        
    #region Radix_MSD
    
        public static void RadixSort_MSD(int[] array)
        {
            Debug.Log("Original: ");
            foreach (int item in array)
                Debug.Log(item);
    
            if (array.Length <= 1)
                return;
    
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
    
            long exp = 1;
            while (max / exp >= 10)
                exp *= 10;
    
            MSD_Recursive(array, 0, array.Length - 1, (int)exp);
    
            if (min < 0)
            {
                for (int i = 0; i < array.Length; i++)
                    array[i] += min;
            }
    
            Debug.Log("Sorted: ");
            foreach (int item in array)
                Debug.Log(item);
        }
    
        private static void MSD_Recursive(int[] array, int low, int high, int exp)
        {
            if (low >= high || exp == 0)
                return;
    
            int[] bucketSizes = CountingSortMSD(array, low, high, exp);
    
            int[] bucketStarts = new int[10];
            bucketStarts[0] = low;
            for (int i = 1; i < 10; i++)
            {
                bucketStarts[i] = bucketStarts[i - 1] + bucketSizes[i - 1];
            }
    
            for (int i = 0; i < 10; i++)
            {
                if (bucketSizes[i] > 1)
                {
                    MSD_Recursive(array, bucketStarts[i], bucketStarts[i] + bucketSizes[i] - 1, exp / 10);
                }
            }
        }
    
        private static int[] CountingSortMSD(int[] array, int low, int high, int exp)
        {
            int[] output = new int[high - low + 1];
            int[] count = new int[10];
            int[] bucketSizes = new int[10];
    
            for (int i = low; i <= high; i++)
            {
                int digit = array[i] / exp % 10;
                count[digit]++;
                bucketSizes[digit]++;
            }
    
            for (int i = 1; i < 10; i++)
            {
                count[i] += count[i - 1];
            }
    
            for (int i = high; i >= low; i--)
            {
                int digit = array[i] / exp % 10;
                output[count[digit] - 1] = array[i];
                count[digit]--;
            }
    
            for (int i = 0; i < output.Length; i++)
            {
                array[low + i] = output[i];
            }
    
            return bucketSizes;
        }
    
        #endregion Radix_MSD
    
    #endregion O(n)
    
    #region O(n log n)
    
    #region Intro
    
        public static void IntroSort<T>(T[] array) where T : IComparable<T>
        {
            Debug.Log("Original: ");
    
            foreach (T item in array)
                Debug.Log(item);
    
            if (array.Length > 1)
            {
                int depthLimit = 2 * (int)Math.Floor(Math.Log(array.Length, 2));
    
                IntroSortRecursive(array, 0, array.Length - 1, depthLimit);
            }
    
            Debug.Log("Sorted: ");
    
            foreach (T item in array)
                Debug.Log(item);
        }
    
        private static void IntroSortRecursive<T>(T[] array, int low, int high, int depthLimit) where T : IComparable<T>
        {
            int size = high - low + 1;
            int sizeToInsertionSort = 16;
    
            if (size <= sizeToInsertionSort)
            {
                InsertionSortRange(array, low, high);
                return;
            }
    
            if (depthLimit == 0)
            {
                HeapSortRange(array, low, high);
                return;
            }
    
            int pivotIndex = Partition(array, low, high);
    
            IntroSortRecursive(array, low, pivotIndex - 1, depthLimit - 1);
            IntroSortRecursive(array, pivotIndex + 1, high, depthLimit - 1);
        }
    
        private static void InsertionSortRange<T>(T[] array, int low, int high) where T : IComparable<T>
        {
            for (int i = low + 1; i <= high; i++)
            {
                T temp = array[i];
                int j = i;
    
                while (j > low && array[j - 1].CompareTo(temp) > 0)
                {
                    array[j] = array[j - 1];
                    j--;
                }
    
                array[j] = temp;
            }
        }
    
        private static void HeapSortRange<T>(T[] array, int low, int high) where T : IComparable<T>
        {
            int rangeSize = high - low + 1;
    
            for (int i = rangeSize / 2 - 1; i >= 0; i--)
            {
                Heapify(array, rangeSize, i, low);
            }
    
            for (int i = rangeSize - 1; i > 0; i--)
            {
                (array[low], array[low + i]) = (array[low + i], array[low]);
                Heapify(array, i, 0, low);
            }
        }
    
        private static void Heapify<T>(T[] array, int rangeSize, int i, int offset) where T : IComparable<T>
        {
            int largest = i;
            int left = 2 * i + 1;
            int right = 2 * i + 2;
    
            if (left < rangeSize && array[offset + left].CompareTo(array[offset + largest]) > 0)
                largest = left;
    
            if (right < rangeSize && array[offset + right].CompareTo(array[offset + largest]) > 0)
                largest = right;
    
            if (largest != i)
            {
                (array[offset + i], array[offset + largest]) = (array[offset + largest], array[offset + i]);
                Heapify(array, rangeSize, largest, offset);
            }
        }
    
        #endregion Intro
        
    #region Adaptative
    
        public static void AdaptiveMergeSort<T>(T[] array) where T : IComparable<T>
        {
            Debug.Log("Original: ");
    
            foreach (T item in array)
                Debug.Log(item);
    
            if (array.Length > 1)
            {
                T[] temp = new T[array.Length];
    
                AdaptiveMergeSortRecursive(array, temp, 0, array.Length - 1);
            }
    
            Debug.Log("Sorted: ");
    
            foreach (T item in array)
                Debug.Log(item);
        }
    
        private static void AdaptiveMergeSortRecursive<T>(T[] array, T[] temp, int low, int high) where T : IComparable<T>
        {
            int size = high - low + 1;
            int sizeToInsertionSort = 16;
    
            if (size <= sizeToInsertionSort)
            {
                InsertionSortRange(array, low, high);
                return;
            }
    
            int mid = low + (high - low) / 2;
    
            AdaptiveMergeSortRecursive(array, temp, low, mid);
            AdaptiveMergeSortRecursive(array, temp, mid + 1, high);
    
            if (array[mid].CompareTo(array[mid + 1]) <= 0)
            {
                return;
            }
    
            Merge(array, temp, low, mid, high);
        }
    
        private static void Merge<T>(T[] array, T[] temp, int low, int mid, int high) where T : IComparable<T>
        {
            for (int k = low; k <= high; k++)
            {
                temp[k] = array[k];
            }
    
            int i = low;
            int j = mid + 1;
    
            for (int k = low; k <= high; k++)
            {
                if (i > mid)
                {
                    array[k] = temp[j++];
                }
                else if (j > high)
                {
                    array[k] = temp[i++];
                }
                else if (temp[j].CompareTo(temp[i]) < 0)
                {
                    array[k] = temp[j++];
                }
                else
                {
                    array[k] = temp[i++];
                }
            }
        }
    
        #endregion Adaptative
    
    #region Merge
        
            public static void MergeSort<T>(T[] array) where T : IComparable<T>
            {
                Debug.Log("Original: ");
        
                foreach (T item in array)
                    Debug.Log(item);
        
                if (array.Length > 1)
                {
                    T[] temp = new T[array.Length];
        
                    MergeSortRecursive(array, temp, 0, array.Length - 1);
                }
        
                Debug.Log("Sorted: ");
        
                foreach (T item in array)
                    Debug.Log(item);
            }
        
            private static void MergeSortRecursive<T>(T[] array, T[] temp, int low, int high) where T : IComparable<T>
            {
                if (low < high)
                {
                    int mid = low + (high - low) / 2;
        
                    MergeSortRecursive(array, temp, low, mid);
                    MergeSortRecursive(array, temp, mid + 1, high);
        
                    Merge(array, temp, low, mid, high);
                }
            }
        
            #endregion Merge
        
    #region Heap
            
                public static void HeapSort<T>(T[] array) where T : IComparable<T>
                {
                    Debug.Log("Original: ");
            
                    foreach (T item in array)
                        Debug.Log(item);
            
                    for (int i = array.Length / 2 - 1; i >= 0; i--)
                    {
                        Heapify(array, array.Length, i, 0);
                    }
            
                    for (int i = array.Length - 1; i > 0; i--)
                    {
                        (array[0], array[i]) = (array[i], array[0]);
            
                        Heapify(array, i, 0, 0);
                    }
            
                    Debug.Log("Sorted: ");
            
                    foreach (T item in array)
                        Debug.Log(item);
                }
            
                #endregion Heap
            
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
                
    #endregion O(n log n)
    
    #region O(n log^2 n)
    
    #region Bitonic
    
        public static void BitonicSort<T>(T[] array) where T : IComparable<T>
        {
            if (array.Length <= 1)
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
    
    #endregion O(n log^2 n)
    
    #region O(n^2)
    
    #region Insertion
    
        public static void InsertionSort<T>(T[] array) where T : IComparable<T>
        {
            Debug.Log("Original: ");
    
            foreach (T item in array)
                Debug.Log(item);
    
            if (array.Length > 1)
            {
                InsertionSortRange(array, 0, array.Length - 1);
            }
    
            Debug.Log("Sorted: ");
    
            foreach (T item in array)
                Debug.Log(item);
        }
    
        #endregion Insertion
    
    #region Bubble
        
            public static void BubbleSort<T>(T[] array) where T : IComparable<T>
            {
                Debug.Log("Original: ");
        
                foreach (T item in array)
                    Debug.Log(item);
        
                for (int i = 0; i < array.Length - 1; i++)
                {
                    bool swapped = false;
        
                    for (int j = 0; j < array.Length - 1 - i; j++)
                    {
                        if (array[j].CompareTo(array[j + 1]) > 0)
                        {
                            // T temp = array[j];
                            // array[j] = array[j + 1];
                            // array[j + 1] = temp;
        
                            (array[j], array[j + 1]) = (array[j + 1], array[j]);
                            swapped = true;
                        }
                    }
        
                    if (!swapped)
                    {
                        break;
                    }
                }
        
                Debug.Log("Sorted: ");
        
                foreach (T item in array)
                    Debug.Log(item);
            }
        
            #endregion Bubble
        
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
    
    #region Gnome
        
            public static void GnomeSort<T>(T[] array) where T : IComparable<T>
            {
                Debug.Log("Original: ");
        
                foreach (T item in array)
                    Debug.Log(item);
        
                if (array.Length > 1)
                {
                    int index = 1;
        
                    while (index < array.Length)
                    {
                        if (index == 0 || array[index].CompareTo(array[index - 1]) >= 0)
                        {
                            index++;
                        }
                        else
                        {
                            (array[index], array[index - 1]) = (array[index - 1], array[index]);
        
                            index--;
                        }
                    }
                }
        
                Debug.Log("Sorted: ");
        
                foreach (T item in array)
                    Debug.Log(item);
            }
        
            #endregion Gnome
        
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
                
    #endregion O(n^2)
    
    #region O(n!)
    
    #region Bogo
    
        public static void BogoSort<T>(T[] array) where T : IComparable<T>
        {
            Debug.Log("Original: ");
    
            foreach (T item in array)
                Debug.Log(item);
    
            System.Random random = new System.Random();
    
            while (!IsSorted(array))
            {
                for (int i = 0; i < array.Length; i++)
                {
                    int randomIndex = random.Next(i, array.Length);
    
                    if (i != randomIndex)
                    {
                        (array[i], array[randomIndex]) = (array[randomIndex], array[i]);
                    }
                }
            }
    
            Debug.Log("Sorted: ");
    
            foreach (T item in array)
                Debug.Log(item);
        }
    
        private static bool IsSorted<T>(T[] array) where T : IComparable<T>
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                if (array[i].CompareTo(array[i + 1]) > 0)
                {
                    return false;
                }
            }
    
            return true;
        }
    
        #endregion Bogo
    
    #endregion O(n!)
}