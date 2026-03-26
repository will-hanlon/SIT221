using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vector
{
    public interface ISorter
    {
        // Treat this as the contract for every sorting algorithm you add.
        // `index` is the starting position of the slice to sort.
        // `num` is how many elements from that starting position belong to the slice.
        // The comparer decides the ordering; your algorithm should not hard-code
        // ascending or descending rules.
        void Sort<K>(K[] array, int index, int num, IComparer<K> comparer) where K : IComparable<K>;
    }

    public class BubbleSort : ISorter
    {
        public BubbleSort()
        {
            
        }

        public void Sort<K>(K[] array, int index, int num, IComparer<K> comparer) where K : IComparable<K>
        {
            if (array == null)
            {
                throw new ArgumentNullException();
            }

            if (index < 0 || num < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            // There is still one range-related exception case missing here.
            // Hint: think about values that are individually non-negative but,
            // when combined, describe a slice that does not fit inside `array`.

            // Before the loops, decide what the final valid position of the slice is.
            // Most off-by-one bugs in this task come from treating `num` as if it were
            // an ending index rather than a count of elements.

            // Another key choice: if `comparer` is null, replace it with
            // Comparer<K>.Default once, then use that single comparison path everywhere.
            // That keeps BubbleSort, SelectionSort, and InsertionSort consistent.

            for (var i = index; i < num - 1; i++)
            {
                for (var j = i; j < num; j++)
                {
                    int compare = comparer.Compare(array[j], array[j + 1]);

                    if (compare < 0)
                    {
                        var temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                    }
                }
            }
        }

    }
}
