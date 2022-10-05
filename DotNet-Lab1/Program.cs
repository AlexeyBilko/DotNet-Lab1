Library.Stack<int> stack = new Library.Stack<int>();

int pushedCount = 0;
int popedCount = 0;
int changedCount = 0;
int peekedCount = 0;
stack.OnChanged += stack => { changedCount++; };
stack.OnPushed += stack =>{ pushedCount++; };
stack.OnPoped += stack => { popedCount++; };
stack.OnPeeked += stack => { peekedCount++; };

stack.Add(1);
stack.Add(2);
stack.Add(3);

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
Console.WriteLine("Is Stack EMpty : " + stack.IsEmpty());

Console.WriteLine($"Changed Count: {changedCount}");
Console.WriteLine($"Pushed Count: {pushedCount}");
Console.WriteLine($"Changed Count: {popedCount}");
Console.WriteLine($"Peeked Count: {peekedCount}");