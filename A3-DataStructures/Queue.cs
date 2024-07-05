namespace COIS2020.DamonFernandez0813575.Assignment3;

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;


public class Queue<T> : IEnumerable<T>
{
    public const int DefaultCapacity = 8;

    private T?[] buffer;
    private int start;
    private int end;


    public bool IsEmpty
    {
        get
        {
            return start == end;
        }
    }

    public int Count
    {
        get
        {
            if (IsEmpty)
            {
                return 0;
            }
            else
            {
                return end - start + 1;
            }
            // Add 1 since Count is talking about elements, not indices 
            // like end and start, so we should count from 1
        }
    }

    public int Capacity
    {
        get
        {
            return buffer.Length - start;
        }

    }

    public Queue() : this(DefaultCapacity)
    {
        buffer = new T[DefaultCapacity];
        start = 0;
        end = 0;
    }

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

        // checking if buffer is full
        if (Capacity == Count)
        {
            Grow();
        }

        buffer[end] = item;
        end++;
    }

    public T Dequeue()
    {
        if (IsEmpty)
        {
            throw new InvalidOperationException("The buffer is empty");
        }

        T poppedOffHead = buffer[start]!;
        buffer[start] = default;
        start++;

        return poppedOffHead;
    }

    public T Peek()
    {
        if (IsEmpty)
        {
            throw new InvalidOperationException("The buffer is empty");
        }
        return buffer[start]!;
    }


    public IEnumerator<T> GetEnumerator()
    {
        for (int i = start; i < end; i++)
            yield return buffer[i]!;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
