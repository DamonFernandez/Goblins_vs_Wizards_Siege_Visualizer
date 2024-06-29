namespace COIS2020./* FirstnameLastnameStudentnumber */.Assignment3;

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis; // For NotNull attributes


public sealed class Node<T>
{
    public T Item { get; set; }

    // "internal" = only things within `A3-DataStructures` have access (AKA can access within LinkedList, but not from
    // within Program.cs)
    public Node<T>? Next { get; internal set; }
    public Node<T>? Prev { get; internal set; }


    public Node(T item)
    {
        Item = item;
    }
}


public class LinkedList<T> : IEnumerable<T>
{
    public Node<T>? Head { get; protected set; }
    public Node<T>? Tail { get; protected set; }


    public LinkedList()
    {
        Head = null;
        Tail = null;
    }


    // IEnumerable is done for you:

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator(); // Call the <T> version

    public IEnumerator<T> GetEnumerator()
    {
        Node<T>? curr = Head;
        while (curr != null)
        {
            yield return curr.Item;
            curr = curr.Next;
        }
    }


    // This getter is done for you:

    /// <summary>
    /// Determines whether or not this list is empty or not.
    /// </summary>
    [MemberNotNullWhen(false, nameof(Head))] // (these "attributes" tell the `?` thingies that Head and Tail are not
    [MemberNotNullWhen(false, nameof(Tail))] // null whenever this getter returns `false`, which stops the `!` warnings)
    public bool IsEmpty
    {
        get
        {
            bool h = Head == null;
            bool t = Tail == null;
            if (h ^ t) // Can't hurt to do a sanity check while we're here.
                throw new Exception("Head and Tail should either both be null or both non-null.");
            return h;
        }
    }


    // --------------------------------------------------------------
    // Put your code down here:
    // --------------------------------------------------------------


}
