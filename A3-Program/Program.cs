
global using static Program; // Makes static fields on Program available globally (for `RNG`).

using COIS2020.DamonFernandez0813575.Assignment3;
using COIS2020.StarterCode.Assignment3;

static class Program
{





    /// <summary>
    /// The random number generator used for all RNG in the program.
    /// </summary>
    /// 
    public static Random RNG = new(/* Seed here */);


    private static void Main()
    {

        TestBasicEnqueueDequeue();
        TestEnqueueBeyondCapacity();
        TestCircularBehavior();
        TestMultipleGrows();
        TestDequeueEmptyQueue();
        TestEnqueueDequeueCircularWrap();

        // The wizard/goblin ToString methods use emojis. You need this line if you want to Console.WriteLine them.
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        var renderer = new CastleGameRenderer()
        {
            CaptureConsoleOutput = true,    // Makes your `Console.WriteLine` calls appear in the game window
            FrameDelayMS = 100,             // Controls how fast the animation plays
        };

        renderer.Run(new CastleDefender(), startPaused: false);
    }


    static void TestBasicEnqueueDequeue()
    {
        Console.WriteLine("TestBasicEnqueueDequeue: Enqueuing and dequeuing elements");
        Queue<int> queue = new Queue<int>(3);

        queue.Enqueue(1);
        queue.Enqueue(2);
        queue.Enqueue(3);

        Console.Write("Expected: 1 2 3 | Actual: ");
        while (!queue.IsEmpty)
        {
            Console.Write(queue.Dequeue() + " ");
        }
        Console.WriteLine();
    }

    static void TestEnqueueBeyondCapacity()
    {
        Console.WriteLine("TestEnqueueBeyondCapacity: Enqueuing beyond initial capacity");
        Queue<int> queue = new Queue<int>(3);

        queue.Enqueue(1);
        queue.Enqueue(2);
        queue.Enqueue(3);
        queue.Enqueue(4); // Should trigger a grow

        Console.Write("Expected: 1 2 3 4 | Actual: ");
        while (!queue.IsEmpty)
        {
            Console.Write(queue.Dequeue() + " ");
        }
        Console.WriteLine();
    }

    static void TestCircularBehavior()
    {
        Console.WriteLine("TestCircularBehavior: Ensuring circular behavior");
        Queue<int> queue = new Queue<int>(3);

        queue.Enqueue(1);
        queue.Enqueue(2);
        queue.Dequeue();
        queue.Enqueue(3);
        queue.Enqueue(4);

        Console.Write("Expected: 2 3 4 | Actual: ");
        while (!queue.IsEmpty)
        {
            Console.Write(queue.Dequeue() + " ");
        }
        Console.WriteLine();
    }

    static void TestMultipleGrows()
    {
        Console.WriteLine("TestMultipleGrows: Enqueuing enough elements to trigger multiple growths");
        Queue<int> queue = new Queue<int>(2);

        for (int i = 1; i <= 10; i++)
        {
            queue.Enqueue(i);
        }

        Console.Write("Expected: 1 2 3 4 5 6 7 8 9 10 | Actual: ");
        while (!queue.IsEmpty)
        {
            Console.Write(queue.Dequeue() + " ");
        }
        Console.WriteLine();
    }

    static void TestDequeueEmptyQueue()
    {
        Console.WriteLine("TestDequeueEmptyQueue: Attempting to dequeue from an empty queue");
        Queue<int> queue = new Queue<int>(3);

        try
        {
            queue.Dequeue();
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine(ex.Message == "The buffer is empty"); // Expected: True
        }
    }

    static void TestEnqueueDequeueCircularWrap()
    {
        Console.WriteLine("TestEnqueueDequeueCircularWrap: Ensuring proper functionality with circular wrap-around");
        Queue<int> queue = new Queue<int>(3);

        queue.Enqueue(1);
        queue.Enqueue(2);
        queue.Enqueue(3);
        queue.Dequeue();
        queue.Enqueue(4);
        queue.Dequeue();
        queue.Enqueue(5);

        Console.Write("Expected: 3 4 5 | Actual: ");
        while (!queue.IsEmpty)
        {
            Console.Write(queue.Dequeue() + " ");
        }
        Console.WriteLine();
    }
}
