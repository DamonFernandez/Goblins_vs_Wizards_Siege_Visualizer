namespace COIS2020./* FirstnameLastnameStudentnumber */.Assignment3;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Queue<T>
{
    public const int DefaultCapacity = 8;

    private T?[] buffer;
    private int start;
    private int end;


    public bool IsEmpty
    {
        get
        {

        }
    }

    public int Count
    {
        get
        {


        }
    }

    public int Capacity
    {
        get
        {


        }

    }




    public Queue() : this(DefaultCapacity)
    { }

    public Queue(int capacity)
    {
        buffer = new T[capacity];
        start = 0;
        end = 0;
    }


    private void Grow()
    {
        Array.Resize(ref buffer, Capacity * 2);
    }

    public void Enqueue(T item)
    {

        if ()


            // Shift everything up by 1 index to make room for new element 
            for (int i = Count; i > start; --i)
            {

            }


        buffer[end] = item;


    }

    public T Dequeue()
    {

    }

    public T Peek()
    {

    }


    // Implement the Inumerable interface, and actually ankify it this time!
    Enumerable
}
