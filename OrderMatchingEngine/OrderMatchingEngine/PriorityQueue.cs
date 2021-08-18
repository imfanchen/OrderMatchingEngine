using System;
using System.Collections.Generic;

namespace AkunaHackerRank {

    /// <summary>
    /// This is my own implementation of PriorityQueue, because only .NET 6 contains build-in PriorityQueue data structure.
    /// By default, this is a min heap which smaller value has higher priority and will get dequeue first.
    /// To change the priority, create your own object of type <typeparamref name="T"/> and inherit from IComparable&lt;<typeparamref name="T"/>&gt;.
    /// Define the your custom priority comparer in the CompareTo(<typeparamref name="T"/> other) method.
    /// <para>Concept: <a href="https://en.wikipedia.org/wiki/Priority_queue"></a></para>
    /// <para>Discussion: <a href="https://github.com/dotnet/runtime/issues/14032"></a></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PriorityQueue<T> where T : IComparable<T> {
        private readonly List<T> data;
        private readonly bool maxHeap;
        private readonly bool minHeap;

        public PriorityQueue() {
            data = new List<T>();
            minHeap = true;
            maxHeap = false;
        }

        public PriorityQueue(bool isMaxHeap) {
            data = new List<T>();
            minHeap = !isMaxHeap;
            maxHeap = isMaxHeap;
        }

        public void Enqueue(T item) {
            data.Add(item);
            int childIndex = data.Count - 1; // start at the end
            while (childIndex > 0) {
                int parentIndex = (childIndex - 1) / 2;
                // if minheap child's priority value is greater than or equal to the parent's priority, then the method is done.
                if (minHeap && data[childIndex].CompareTo(data[parentIndex]) >= 0) break;
                // if maxheap child's priority value is less than or equal to the parent's priority, then the method is done.
                if (maxHeap && data[childIndex].CompareTo(data[parentIndex]) <= 0) break;
                T tmp = data[childIndex];
                data[childIndex] = data[parentIndex];
                data[parentIndex] = tmp;
                childIndex = parentIndex;
            }
        }

        public T Dequeue() {
            // Assumes priority queue isn't empty
            int lastIndex = data.Count - 1;
            T frontItem = data[0];
            data[0] = data[lastIndex];
            data.RemoveAt(lastIndex);

            lastIndex--;
            int parentIndex = 0; // start at the front
            while (true) {
                int childIndex = parentIndex * 2 + 1;
                if (childIndex > lastIndex) break; // no child
                int rightChildIndex = childIndex + 1;
                // if minheap and there is a right child and it is smaller than the left child, then use right child instead
                if (minHeap && rightChildIndex <= lastIndex && data[rightChildIndex].CompareTo(data[childIndex]) < 0) childIndex = rightChildIndex;
                if (minHeap && data[parentIndex].CompareTo(data[childIndex]) <= 0) break; // skip if parent
                                                                                          // if maxheap and there is a right child and it is larger than the left child, then use right child instead
                if (maxHeap && rightChildIndex <= lastIndex && data[rightChildIndex].CompareTo(data[childIndex]) > 0) childIndex = rightChildIndex;
                if (maxHeap && data[parentIndex].CompareTo(data[childIndex]) >= 0) break;
                T tmp = data[parentIndex];
                data[parentIndex] = data[childIndex];
                data[childIndex] = tmp;
                parentIndex = childIndex;
            }
            return frontItem;
        }

        public T Peek() {
            T frontItem = data[0];
            return frontItem;
        }

        public int Count() {
            return data.Count;
        }

        public bool Any() {
            return data.Count > 0;
        }

        public override string ToString() {
            string s = "";
            for (int i = 0; i < data.Count; ++i)
                s += data[i].ToString() + " ";
            s += "count = " + data.Count;
            return s;
        }
    }
}