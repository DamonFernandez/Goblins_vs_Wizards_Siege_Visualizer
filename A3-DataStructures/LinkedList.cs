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
    private readonly EqualityComparer<T> comparer = EqualityComparer<T>.Default;
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




    public void AddFront(T item)
    {
        Node<T> newNode = new Node<T>(item);
        AddFront(newNode);
    }
    public void AddFront(Node<T> node)
    {
        if (IsEmpty)
        {
            Head = node;
            Tail = node;
        }
        else
        {
            Node<T> poppedOffHead = Head;
            Head = node;
            Head.Next = poppedOffHead;
            poppedOffHead.Prev = Head;
        }

    }

    public void AddBack(T item)
    {
        Node<T> newNode = new Node<T>(item);
        AddBack(newNode);
    }
    public void AddBack(Node<T> node)
    {

        if (IsEmpty)
        {
            Head = node;
            Tail = node;
        }
        else
        {
            Node<T> poppedOffTail = Tail;
            Tail = node;
            Tail.Prev = poppedOffTail;
            poppedOffTail.Next = Tail;
        }
    }

    public Node<T>? Find(T item)
    {
        if (IsEmpty)
        {
            return null;
        }

        else
        {
            Node<T> nextNode = Head;
            while (nextNode != null)
            {
                if (comparer.Equals(item, nextNode.Item))
                {
                    return nextNode;
                }
                else
                {
                    nextNode = nextNode.Next!;
                }

            }

            return null;
        }
    }
    public void InsertAfter(Node<T> node, T item)
    {
        Node<T> newNode = new Node<T>(item);
        InsertAfter(node, newNode);
    }
    public void InsertAfter(Node<T> node, Node<T> newNode)
    {


        if (!IsEmpty)
        {
            // node.next being null means that this node 
            // we are inserting after is the tail
            if (node.Next == null)
            {
                newNode.Prev = Tail;
                Tail.Next = newNode;
                Tail = newNode;
            }
            else
            {
                newNode.Prev = node;
                newNode.Next = node.Next;
                node.Next.Prev = newNode;
                node.Next = newNode;

            }

        }
    }


    public void InsertBefore(Node<T> node, T item)
    {
        Node<T> newNode = new Node<T>(item);
        InsertBefore(node, newNode);
    }
    public void InsertBefore(Node<T> node, Node<T> newNode)
    {

        if (!IsEmpty)
        {


            // if node.Prev == null is true, this means that this node is the head 
            if (node.Prev == null)
            {
                Head.Prev = newNode;
                newNode.Next = Head;
                Head = newNode;
            }
            else
            {
                newNode.Next = node;
                newNode.Prev = node.Prev;
                node.Prev.Next = newNode;
                node.Prev = newNode;
            }
        }
    }

    public void Remove(Node<T> node)
    {
        if (!IsEmpty)
        {
            // If node is head 
            if (node.Prev == null)
            {
                if (node.Next != null)
                {
                    Head = node.Next;
                    Head.Prev = null;

                }
                else
                {
                    Head = null;
                    Tail = null; // since the other if statement being false
                                 // means that there is only 1 element in the list
                }
            }

            // If node is tail
            else if (node.Next == null)
            {
                if (node.Prev != null)
                {
                    Tail = node.Prev;
                    Tail.Next = null;
                }
                else
                {
                    Tail = null;
                    Head = null; // since the other if statement being false
                                 // means that there is only 1 element in the list
                }
            }
            else
            {
                node.Prev.Next = node.Next;
                node.Next.Prev = node.Prev;
            }



        }


    }
    public void Remove(T item)
    {

        Node<T> nodeToRemove = Find(item);
        if (nodeToRemove != null)
        {
            Remove(nodeToRemove);
        }
    }

    public LinkedList<T> SplitAfter(Node<T> node)
    {


        // Node is tail
        if (node.Next == null)
        {
            return new LinkedList<T>();
        }

        else
        {
            Tail = node;

            LinkedList<T> newLinkedList = new LinkedList<T>();
            newLinkedList.AddFront(node.Next);
            node.Next.Prev = null;
            node.Next = null;
            return newLinkedList;
        }

    }

    public void AppendAll(LinkedList<T> otherList)
    {
        if (otherList.Head != null)
        {
            AddBack(otherList.Head);
            Tail = otherList.Tail;
        }

        // Removes all nodes from otherList
        otherList.Head = null;
        otherList.Tail = null;

    }

}




