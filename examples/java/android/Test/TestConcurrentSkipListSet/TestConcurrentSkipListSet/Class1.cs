using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConcurrentSkipListSet
{
    // https://docs.oracle.com/javase/7/docs/api/java/util/concurrent/ConcurrentSkipListSet.html#headSet(E,%20boolean)

    public class ConcurrentSkipListSet<E>
        : SortedSet<E>, NavigableSet<E>
    {
        public NavigableSet<E> headSet(E toElement, bool inclusive) { return null; }
        public NavigableSet<E> headSet(E toElement) { return null; }

        SortedSet<E> SortedSet<E>.headSet(E toElement)
        {
            throw new NotImplementedException();
        }

        SortedSet<E> NavigableSet<E>.headSet(E toElement)
        {
            throw new NotImplementedException();
        }

        NavigableSet<E> NavigableSet<E>.headSet(E toElement, bool inclusive)
        {
            throw new NotImplementedException();
        }
    }

    // https://docs.oracle.com/javase/7/docs/api/java/util/SortedSet.html
    public interface SortedSet<E>
    {
        // this method looks distinct?
        SortedSet<E> headSet(E toElement);
    }

    // https://docs.oracle.com/javase/7/docs/api/java/util/NavigableSet.html#headSet(E,%20boolean)
    public interface NavigableSet<E>
    {
        SortedSet<E> headSet(E toElement);
        NavigableSet<E> headSet(E toElement, bool inclusive);

    }
}
