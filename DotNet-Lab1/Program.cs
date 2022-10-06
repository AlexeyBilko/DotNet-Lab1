Library.Stack<int> stack = new Library.Stack<int>();

int pushedCount = 0;
int popedCount = 0;
int changedCount = 0;
int peekedCount = 0;
stack.OnChanged += () => { changedCount++; System.Console.WriteLine("Changed"); };
stack.OnPushed += () =>{ pushedCount++; System.Console.WriteLine("Pushed"); };
stack.OnPoped += () => { popedCount++; System.Console.WriteLine("Poped"); };
stack.OnPeeked += () => { peekedCount++; System.Console.WriteLine("Peeked"); };


stack.Add(1);
stack.Add(2);
stack.Add(3);

int[] array = stack.ToArray();
foreach (var item in array)
{
    Console.WriteLine(item);
}

Console.WriteLine($"\nStack State: {stack}\n");

stack.Pop();

Console.WriteLine($"\nStack State: {stack}\n");
Console.WriteLine("Top Element is " + stack.Peek());
Console.WriteLine($"\nStack State: {stack}\n");
Console.WriteLine("Size of the Stack is " + stack.Count);
Console.WriteLine("Stack Empty : " + stack.IsEmpty());

stack.Pop();
stack.Pop();

Console.WriteLine($"\nStack State: {stack}\n");
Console.WriteLine("Size of the Stack is " + stack.Count);

stack.Pop();

Console.WriteLine("Stack Empty : " + stack.IsEmpty());

Console.WriteLine($"Changed Count: {changedCount}");
Console.WriteLine($"Pushed Count: {pushedCount}");
Console.WriteLine($"Poped Count: {popedCount}");
Console.WriteLine($"Peeked Count: {peekedCount}");