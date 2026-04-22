using System;
using System.Diagnostics;

namespace SearchingSortingDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Searching and Sorting Algorithms ===");

            // Step 2: Take input from user for array and element to be searched
            Console.Write("Enter the size of the array: ");
            if (!int.TryParse(Console.ReadLine(), out int size) || size <= 0)
            {
                Console.WriteLine("Invalid size.");
                return;
            }

            int[] arr = new int[size];
            Console.WriteLine($"Enter {size} elements of the array:");
            for (int i = 0; i < size; i++)
            {
                arr[i] = int.Parse(Console.ReadLine());
            }

            Console.Write("Enter the element to be searched: ");
            int element = int.Parse(Console.ReadLine());

            Console.WriteLine("\n--- Linear Search ---");
            Stopwatch sw = Stopwatch.StartNew();
            int linearSearchResult = LinearSearch(arr, element);
            sw.Stop();
            
            if (linearSearchResult != -1)
                Console.WriteLine($"Element found at index: {linearSearchResult}");
            else
                Console.WriteLine("Element not found in the array.");
            Console.WriteLine($"Time taken (Linear Search): {sw.Elapsed.TotalMilliseconds} ms");

            // Binary Search requires sorted array
            Console.WriteLine("\n--- Sorting Algorithms ---");
            Console.WriteLine("We will sort the array using Bubble Sort to prepare for Binary Search.");
            int[] sortedArr = (int[])arr.Clone();
            
            sw.Restart();
            BubbleSort(sortedArr);
            sw.Stop();
            Console.WriteLine("Array sorted using Bubble Sort.");
            Console.WriteLine($"Time taken (Bubble Sort): {sw.Elapsed.TotalMilliseconds} ms");

            Console.WriteLine("\n--- Binary Search ---");
            sw.Restart();
            int binarySearchResult = BinarySearch(sortedArr, element);
            sw.Stop();

            if (binarySearchResult != -1)
                Console.WriteLine($"Element found at index: {binarySearchResult} (in sorted array)");
            else
                Console.WriteLine("Element not found in the array.");
            Console.WriteLine($"Time taken (Binary Search): {sw.Elapsed.TotalMilliseconds} ms");

            // Demonstrate other sorting algorithms on fresh copies of original array
            DemonstrateSorting("Insertion Sort", InsertionSort, arr);
            DemonstrateSorting("Selection Sort", SelectionSort, arr);
            DemonstrateSorting("Merge Sort", MergeSortDemo, arr);
            DemonstrateSorting("Heap Sort", HeapSort, arr);
            
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

        static void DemonstrateSorting(string sortName, Action<int[]> sortMethod, int[] originalArray)
        {
            int[] copy = (int[])originalArray.Clone();
            Stopwatch sw = Stopwatch.StartNew();
            sortMethod(copy);
            sw.Stop();
            Console.WriteLine($"\n--- {sortName} ---");
            Console.WriteLine($"Time taken ({sortName}): {sw.Elapsed.TotalMilliseconds} ms");
            // Console.WriteLine("Sorted Array: " + string.Join(", ", copy));
        }

        // --- Searching Algorithms ---

        // Linear Search Method
        public static int LinearSearch(int[] arr, int element)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == element)
                {
                    return i; // Return the index of the found element
                }
            }
            return -1; // Return -1 if the element is not found
        }

        // Binary Search Method (Requires sorted array)
        public static int BinarySearch(int[] arr, int element)
        {
            int left = 0;
            int right = arr.Length - 1;

            while (left <= right)
            {
                int mid = left + (right - left) / 2;

                if (arr[mid] == element)
                    return mid;
                
                if (arr[mid] < element)
                    left = mid + 1;
                else
                    right = mid - 1;
            }
            return -1;
        }

        // --- Sorting Algorithms ---

        public static void BubbleSort(int[] arr)
        {
            int n = arr.Length;
            for (int i = 0; i < n - 1; i++)
            {
                bool swapped = false;
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (arr[j] > arr[j + 1])
                    {
                        // Swap
                        int temp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = temp;
                        swapped = true;
                    }
                }
                if (!swapped)
                    break;
            }
        }

        public static void InsertionSort(int[] arr)
        {
            int n = arr.Length;
            for (int i = 1; i < n; i++)
            {
                int key = arr[i];
                int j = i - 1;

                while (j >= 0 && arr[j] > key)
                {
                    arr[j + 1] = arr[j];
                    j = j - 1;
                }
                arr[j + 1] = key;
            }
        }

        public static void SelectionSort(int[] arr)
        {
            int n = arr.Length;
            for (int i = 0; i < n - 1; i++)
            {
                int minIndex = i;
                for (int j = i + 1; j < n; j++)
                {
                    if (arr[j] < arr[minIndex])
                    {
                        minIndex = j;
                    }
                }
                
                if (minIndex != i)
                {
                    int temp = arr[minIndex];
                    arr[minIndex] = arr[i];
                    arr[i] = temp;
                }
            }
        }

        public static void MergeSortDemo(int[] arr)
        {
            MergeSort(arr, 0, arr.Length - 1);
        }

        private static void MergeSort(int[] arr, int left, int right)
        {
            if (left < right)
            {
                int mid = left + (right - left) / 2;

                MergeSort(arr, left, mid);
                MergeSort(arr, mid + 1, right);

                Merge(arr, left, mid, right);
            }
        }

        private static void Merge(int[] arr, int left, int mid, int right)
        {
            int n1 = mid - left + 1;
            int n2 = right - mid;

            int[] leftArr = new int[n1];
            int[] rightArr = new int[n2];

            for (int i = 0; i < n1; ++i)
                leftArr[i] = arr[left + i];
            for (int j = 0; j < n2; ++j)
                rightArr[j] = arr[mid + 1 + j];

            int iMerge = 0, jMerge = 0;
            int k = left;
            while (iMerge < n1 && jMerge < n2)
            {
                if (leftArr[iMerge] <= rightArr[jMerge])
                {
                    arr[k] = leftArr[iMerge];
                    iMerge++;
                }
                else
                {
                    arr[k] = rightArr[jMerge];
                    jMerge++;
                }
                k++;
            }

            while (iMerge < n1)
            {
                arr[k] = leftArr[iMerge];
                iMerge++;
                k++;
            }

            while (jMerge < n2)
            {
                arr[k] = rightArr[jMerge];
                jMerge++;
                k++;
            }
        }

        public static void HeapSort(int[] arr)
        {
            int n = arr.Length;

            for (int i = n / 2 - 1; i >= 0; i--)
                Heapify(arr, n, i);

            for (int i = n - 1; i > 0; i--)
            {
                int temp = arr[0];
                arr[0] = arr[i];
                arr[i] = temp;

                Heapify(arr, i, 0);
            }
        }

        private static void Heapify(int[] arr, int n, int i)
        {
            int largest = i;
            int left = 2 * i + 1;
            int right = 2 * i + 2;

            if (left < n && arr[left] > arr[largest])
                largest = left;

            if (right < n && arr[right] > arr[largest])
                largest = right;

            if (largest != i)
            {
                int swap = arr[i];
                arr[i] = arr[largest];
                arr[largest] = swap;

                Heapify(arr, n, largest);
            }
        }
    }
}
