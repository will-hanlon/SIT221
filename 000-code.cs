using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.VisualBasic;

namespace Vector
{
    public class Vector<T>
    {
        // This constant determines the default number of elements in a newly created vector.
        // It is also used to extended the capacity of the existing vector
        private const int DEFAULT_CAPACITY = 1;

        // This array represents the internal data structure wrapped by the vector class.
        // In fact, all the elements are to be stored in this private  array. 
        // You will just write extra functionality (methods) to make the work with the array more convenient for the user.
        private T[] data;

        private Comparer<T> defComp = Comparer<T>.Default;

        // This property represents the number of elements in the vector
        public int Count { get; private set; } = 0;

        // This property represents the maximum number of elements (capacity) in the vector
        public int Capacity
        {
            get { return data.Length; }
        }

        // This is an overloaded constructor
        public Vector(int capacity)
        {
            data = new T[capacity];
        }

        // This is the implementation of the default constructor
        public Vector() : this(DEFAULT_CAPACITY) { }

        // An Indexer is a special type of property that allows a class or structure to be accessed the same way as array for its internal collection. 
        // For example, introducing the following indexer you may address an element of the vector as vector[i] or vector[0] or ...
        public T this[int index]
        {
            get
            {
                if (index >= Count || index < 0) throw new IndexOutOfRangeException();
                return data[index];
            }
            set
            {
                if (index >= Count || index < 0) throw new IndexOutOfRangeException();
                data[index] = value;
            }
        }

        // This private method allows extension of the existing capacity of the vector by another 'extraCapacity' elements.
        // The new capacity is equal to the existing one plus 'extraCapacity'.
        // It copies the elements of 'data' (the existing array) to 'newData' (the new array), and then makes data pointing to 'newData'.
        private void ExtendData(int extraCapacity)
        {
            T[] newData = new T[Capacity + extraCapacity];
            for (int i = 0; i < Count; i++) newData[i] = data[i];
            data = newData;
        }

        // This method adds a new element to the existing array.
        // If the internal array is out of capacity, its capacity is first extended to fit the new element.
        public void Add(T element)
        {
            if (Count == Capacity) ExtendData(DEFAULT_CAPACITY);
            data[Count] = element;
            Count++;
            if (Count < Capacity)
            {
                T[] newData = new T[Count];
                for (int i = 0; i < Count; i ++)
                {
                    newData[i] = data[i];
                }
                data = newData;
            }
        }

        // This method searches for the specified object and returns the zero‐based index of the first occurrence within the entire data structure.
        // This method performs a linear search; therefore, this method is an O(n) runtime complexity operation.
        // If occurrence is not found, then the method returns –1.
        // Note that Equals is the proper method to compare two objects for equality, you must not use operator '=' for this purpose.
        public int IndexOf(T element)
        {
            for (var i = 0; i < Count; i++)
            {
                if (data[i].Equals(element)) return i;
            }
            return -1;
        }

        // TODO: Your task is to implement all the remaining methods.
        // Read the instruction carefully, study the code examples from above as they should help you to write the rest of the code.
        public void Insert(int index, T element)
        {
            if (index <= Count)
            {
                if (Count == Capacity) ExtendData(1);
                Count = Count + 1;
                T[] newData = new T[Capacity];
                for (var i = 0; i < Count; i++)
                {
                    newData[i] = data[i];
                }

                for (var i = Count - 1;i > index;i--)
                {
                    newData[i] = newData[i-1];
                }
                newData[index] = element;
                data = newData;
            }
            else throw new IndexOutOfRangeException();
        }

        public void Clear()
        {
            T[] newData = new T[0];
            Count = 0;
            data = newData;
        }

        public bool Contains(T element)
        {
            for (var i = 0; i < Count; i++)
            {
                if (EqualityComparer<T>.Default.Equals(data[i], element))
                {
                    return true;
                }
            }
            return false;
        }

        public bool Remove(T element)
        {
            if (Contains(element))
            {
                for (var i = 0; i < Count; i++)
                {
                    if (EqualityComparer<T>.Default.Equals(data[i], element))
                    {
                        RemoveAt(i);
                        return true;

                    }
                }
            }
            return false;
        }
        public void RemoveAt(int index)
        {
            if (index >= 0 && index < Count)
            {
                for(var i = index; i < Count - 1; i++)
                {
                    data[i] = data[i+1];
                }
                Count = Count -1;
                T[] newData = new T[Capacity - 1];
                for (var j = 0; j < Count; j++)
                    {
                        newData[j] = data[j];
                    }
                data = newData;

            }
            else throw new IndexOutOfRangeException();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            for (var i = 0; i < Count; i ++)
            {
               sb.Append(data[i]);

               if (i < Count - 1 )
                {
                    sb.Append(",");
                }
            }
            sb.Append("]");
            return sb.ToString();
        }

        public void Sort()
        {
            // Compare this overload with the assignment wording.
            // Ask yourself whether sorting the whole backing array is always correct,
            // or whether the task only wants the first `Count` logical elements sorted.
            Array.Sort(data, defComp);
        }

        public void Sort(IComparer<T> comparer)
        {
            // Same idea here: the vector may have capacity beyond its element count.
            // Tests usually care about the active data, not unused slots in `data`.
            Array.Sort(data, comparer);
        }

        void Sort(ISorter algorithm, IComparer<T> comparer)
        {
            // This method is the handoff point between Vector<T> and your sort classes.
            // The assignment hint is:
            // - if `algorithm` is null, use the built-in array sort as the default path
            // - otherwise, delegate to `algorithm.Sort(...)`
            //
            // Before you write that delegation, decide exactly which array segment
            // represents the vector's real contents. That same segment definition
            // should be used by every sorting path.
            if (algorithm == null)
            {
                Array.Sort(data, comparer);
            }

            // Once the null case works, the missing branch should feel symmetric:
            // same slice, same comparer, different sorting engine.
        }

    }
}
