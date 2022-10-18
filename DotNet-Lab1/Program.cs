using System;
using Library;

namespace DotNet_Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateStackWithEmptyConstructor_PushItems();
            CreateStackWithArrayConstructor_DisplayItems();
            PushDemonstration();
            PopDemonstration();
            PeekDemonstration();
            IsEmptyDemonstration();
            ForEachDemonstration();
            ClearDemonstration();
            ToArrayDemonstration();
            CopyToDemonstration();
            EventsDemonstration();
        }

        static void CreateStackWithEmptyConstructor_PushItems()
        {
            Console.WriteLine("CreateStackWithEmptyConstructor_PushItems");
            Library.Stack<int> stack = new Library.Stack<int>();
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            Console.WriteLine(stack.ToString());
        }


        static void CreateStackWithArrayConstructor_DisplayItems()
        {
            Console.WriteLine("CreateStackWithArrayConstructor_DisplayItems");
            int[] array = { 1, 2, 3 };
            Library.Stack<int> stack = new Library.Stack<int>(array);
            Console.WriteLine(stack.ToString());
        }

        static void PushDemonstration()
        {
            Console.WriteLine("PushDemonstration");
            Library.Stack<int> stack = new Library.Stack<int>();
            Console.WriteLine(stack.ToString());
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            Console.WriteLine(stack.ToString());
        }

        static void PopDemonstration()
        {
            Console.WriteLine("PopDemonstration");
            Library.Stack<int> stack = GenerateNotEmptyStack();
            Console.WriteLine(stack.ToString());
            Console.WriteLine(stack.Pop());
            Console.WriteLine(stack.ToString());
        }

        static void PeekDemonstration()
        {
            Console.WriteLine("PeekDemonstration");
            Library.Stack<int> stack = new Library.Stack<int>();
            stack.Push(1);
            Console.WriteLine(stack.Peek());
        }

        static void IsEmptyDemonstration()
        {
            Console.WriteLine("IsEmptyDemonstration");
            Library.Stack<int> stack = new Library.Stack<int>();
            Console.WriteLine(stack.IsEmpty());
            stack = GenerateNotEmptyStack();
            Console.WriteLine(stack.IsEmpty());
        }


        static void ForEachDemonstration()
        {
            Console.WriteLine("ForEachDemonstration");
            Library.Stack<int> stack = GenerateNotEmptyStack();
            foreach (var item in stack)
            {
                Console.WriteLine(item);
            }
        }

        static void ClearDemonstration()
        {
            Console.WriteLine("ClearDemonstration");
            Library.Stack<int> stack = GenerateNotEmptyStack();
            Console.WriteLine(stack.ToString());
            stack.Clear();
            Console.WriteLine(stack.ToString());
        }


        static void ToArrayDemonstration()
        {
            Console.WriteLine("ToArrayDemonstration");
            Library.Stack<int> stack = GenerateNotEmptyStack();
            int[] array = stack.ToArray();
            foreach (var item in array)
            {
                Console.WriteLine(item);
            }
        }

        static void CopyToDemonstration()
        {
            Console.WriteLine("CopyToDemonstration");
            Library.Stack<int> stack = GenerateNotEmptyStack();
            int[] array = new int[10];
            stack.CopyTo(array, 2);
            foreach (var item in array)
            {
                Console.WriteLine(item);
            }
        }

        static void EventsDemonstration()
        {
            Console.WriteLine("EventsDemonstration");
            Library.Stack<int> stack = new Library.Stack<int>();
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            stack.Peek();
            stack.Pop();
            stack.Clear();
            stack.Push(1);
            Console.WriteLine("OnChanged Count -> " + stack.OnChangedCount);
            Console.WriteLine("OnPushedCount Count -> " + stack.OnPushedCount);
            Console.WriteLine("OnPopedCount Count -> " + stack.OnPopedCount);
            Console.WriteLine("OnPeekedCount Count -> " + stack.OnPeekedCount);
            Console.WriteLine("OnClearedCount Count -> " + stack.OnClearedCount);
        }

        static Library.Stack<int> GenerateNotEmptyStack()
        {
            var stack = new Library.Stack<int>();
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            stack.Push(4);
            stack.Push(5);
            return stack;
        }
    }
}