namespace COIS2020.DamonFernandez0813575.Assignment3;

using System;
using System.Collections;
using System.Collections.Generic;

public class Queue<T> : IEnumerable<T>
{
    public const int DefaultCapacity = 8;

    private T[] buffer;
    private int start;
    private int end;

    public bool IsEmpty => start == end;

    public int Count
    {
        get
        {
            if (end >= start)
            {
                return end - start;
            }
            else
            {
                return end + (buffer.Length - start);
            }
        }
    }

    public int Capacity => buffer.Length;

    public Queue() : this(DefaultCapacity) { }

    public Queue(int capacity)
    {
        buffer = new T[capacity];
        start = 0;
        end = 0;
    }

    private void Grow()
    {
        T[] newArray = new T[Capacity * 2];
        int newCount = Count;
        for (int i = 0; i < newCount; i++)
        {
            newArray[i] = buffer[(start + i) % Capacity];
        }

        buffer = newArray;
        start = 0;
        end = newCount;
    }

    public void Enqueue(T item)
    {
        if (Count == Capacity - 1)
        {
            Grow();
        }

        buffer[end] = item;
        end = (end + 1) % Capacity;
    }

    public T Dequeue()
    {
        if (IsEmpty)
        {
            throw new InvalidOperationException("The buffer is empty");
        }

        T poppedOffHead = buffer[start];
        buffer[start] = default;
        start = (start + 1) % Capacity;

        return poppedOffHead;
    }

    public T Peek()
    {
        if (IsEmpty)
        {
            throw new InvalidOperationException("The buffer is empty");
        }
        return buffer[start];
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (int i = 0; i < Count; i++)
        {
            yield return buffer[(start + i) % Capacity];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
