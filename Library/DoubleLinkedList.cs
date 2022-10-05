using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class DoubleLinkedList<T> : ICollection<T>, IReadOnlyCollection<T>
	{
        private Node<T> head;
        private Node<T> tail;

        public int Count { get; private set; } = 0;
        public bool IsReadOnly => false;

        public void Add(T data)
		{
			Node<T> newNode = new Node<T>(data);
			if (head == null)
			{
				head = tail = newNode;
			}
			else
			{
				tail.Next = newNode;
				newNode.Prev = tail;
				tail = newNode;
			}
			Count++;
		}

		public void Add(T data, int index)
		{
			if (index < 0 || index > (Count - 1))
			{
                Console.WriteLine("Index out of Bound");
				return;
			}
			Node<T> newNode = new Node<T>(data);
			Node<T> curr = FindNode(index);

			Node<T> prevNode = curr.Prev;
			if (curr == head)
			{
				newNode.Next = head;
				head.Prev = newNode;
				head = newNode;
			}
			else
			{
				prevNode.Next = newNode;
				newNode.Prev = prevNode;
				newNode.Next = curr;
				curr.Prev = newNode;
			}
			Count++;

		}

		public void Set(T data, int index)
		{
			if (index < 0 || index > (Count - 1))
			{
                Console.WriteLine("Index out of Bound");
				return;
			}
			Node<T> curr = FindNode(index);
			curr.Data = data;
		}

		public void RemoveAt(int index)
		{
			if (index < 0 || index > (Count - 1))
			{
                Console.WriteLine("Index out of Bound");
				return;
			}
			Node<T> curr = FindNode(index);

			Node<T> prevNode = curr.Prev;
			Node<T> nextNode = curr.Next;

			if (curr == head)
			{
				head = head.Next;
				if (head != null)
				{
					head.Prev = null;
				}
				else
				{
					head = tail = null;
				}
			}
			else if (curr == tail)
			{
				prevNode.Next = null;
				tail = prevNode;
			}
			else
			{
				prevNode.Next = nextNode;
				nextNode.Prev = prevNode;
			}
			curr.Next = curr.Prev = null;

			Count--;
		}

		public bool Remove(T data)
		{
			Node<T> curr = head;
			while (curr != null && !curr.Data.Equals(data))
			{
				curr = curr.Next;
			}
			if (curr == null)
			{
                Console.WriteLine("Target Data Not found");
				return false;
			}

			Node<T> prevNode = curr.Prev;
			Node<T> nextNode = curr.Next;

			if (curr == head)
			{
				head = head.Next;
				head.Prev = null;
			}
			else if (curr == tail)
			{
				prevNode.Next = null;
				tail = prevNode;
			}
			else
			{
				prevNode.Next = nextNode;
				nextNode.Prev = prevNode;
			}
			curr.Next = curr.Prev = null;

			Count--;
			return true;
		}


		public int IndexOf(T data)
		{
			int i = 0;
			Node<T> curr = head;
			while (curr != null && !curr.Data.Equals(data))
			{
				curr = curr.Next;
				i++;
			}
			if (curr == null)
			{
				i = -1;// data not found
			}
			return i;
		}

		public int LastIndexOf(T data)
		{
			int i = Count - 1;
			Node<T> curr = tail;
			while (curr != null && !curr.Data.Equals(data))
			{
				curr = curr.Prev;
				i--;
			}
			if (curr == null)
			{
				i = -1;// data not found
			}
			return i;
		}

		public bool Contains(T data)
		{
			return IndexOf(data) != -1;
		}

		public bool IsEmpty()
		{
			return Count == 0;
		}

		public void Clear()
		{
			head = tail = null;
			Count = 0;
		}

		public T? Get(int index)
		{
			if (index < 0 || index > (Count - 1))
			{
                Console.WriteLine("Index out of Bound");
				return default;
			}
			Node<T> target = FindNode(index);
			return target.Data;
		}

		private Node<T> FindNode(int index)
		{
			int mid = (Count - 1) / 2;
			if (mid > index)
			{
				return FindForward(index);
			}
			return FindReverse(index);
		}

		public void Reverse()
		{
			Node<T> prev = null;
			Node<T> curr = head;
			Node<T> next = null;
			while (curr != null)
			{
				prev = curr.Prev;
				next = curr.Next;
				curr.Prev = next;
				curr.Next = prev;
				curr = next;
			}
			if (prev != null)
			{
				head = prev.Prev;
			}
		}

		private Node<T> FindForward(int index)
		{
			int i = 0;
			Node<T> curr = head;
			while (curr != null && i != index)
			{
				curr = curr.Next;
				i++;
			}
			return curr;
		}

		private Node<T> FindReverse(int index)
		{
			int i = Count - 1;
			Node<T> curr = tail;
			while (curr != null && i != index)
			{
				curr = curr.Prev;
				i--;
			}
			return curr;
		}

		public override string ToString()
		{
			string buf = "";
			Node<T> curr = head;
			while (curr != null)
			{
				buf += curr.Data.ToString();
				buf += ",";
				curr = curr.Next;
			}
			if (buf.Length > 1)
			{
				buf.Remove(buf.Length - 1, 1);
			}
			if (buf.Length > 0) buf = buf.Substring(0, buf.Length - 1);
			else if (buf.Length == 0) buf = "Stack is Empty - Nothing To Display";
			return buf;
		}

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

		public Enumerator GetEnumerator()
		{
			return new Enumerator(this);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return GetEnumerator();
		}

		public class Enumerator : IEnumerator<T>
		{
			private readonly DoubleLinkedList<T> list;
			private Node<T>? currentNode;
			private bool hasEnumerationStarted = false;

			public T Current => currentNode!.Data;
			object? IEnumerator.Current => Current;


			public Enumerator(DoubleLinkedList<T> list)
			{
				this.list = list;
				currentNode = list.head;
			}

			public void Reset()
			{
				hasEnumerationStarted = false;
				currentNode = list.head;
			}

			public bool MoveNext()
			{
				if (!hasEnumerationStarted)
				{
					currentNode = list.head;
					hasEnumerationStarted = true;
					return currentNode != null;
				}

				currentNode = currentNode!.Next;

				return currentNode != list.head;
			}

            public void Dispose()
            {
            }
        }
	}

    public class Node<T>
	{
        public T Data;
        public Node<T> Next;
        public Node<T> Prev;

        public Node(T data) => Data = data;
	}
}
