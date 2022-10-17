using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class DoubleLinkedList<T> : ICollection<T>, IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable
	{
        public Node<T> Head { get; private set; }
        public Node<T> Tail { get; private set; }

        public int Count { get; private set; } = 0;
        public bool IsReadOnly => false;

        public void Add(T data)
		{
			Node<T> newNode = new Node<T>(data);
			if (Head == null)
			{
				Head = Tail = newNode;
			}
			else
			{
				Tail.Next = newNode;
				newNode.Prev = Tail;
				Tail = newNode;
			}
			Count++;
		}

		public void Add(T data, int index)
		{
			if (index < 0 || index > (Count - 1))
			{
				throw new IndexOutOfRangeException();
			}
			Node<T> newNode = new Node<T>(data);
			Node<T> curr = FindNode(index);

			Node<T> prevNode = curr.Prev;
			if (curr == Head)
			{
				newNode.Next = Head;
				Head.Prev = newNode;
				Head = newNode;
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
				throw new IndexOutOfRangeException();
			}
			Node<T> curr = FindNode(index);
			curr.Data = data;
		}

		public bool RemoveAt(int index)
		{
			if (index < 0 || index > (Count - 1))
			{
				return false;
			}
			Node<T> curr = FindNode(index);

			Node<T> prevNode = curr.Prev;
			Node<T> nextNode = curr.Next;

			if (curr == Head)
			{
				Head = Head.Next;
				if (Head != null)
				{
					Head.Prev = null;
				}
				else
				{
					Head = Tail = null;
				}
			}
			else if (curr == Tail)
			{
				prevNode.Next = null;
				Tail = prevNode;
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

		public bool Remove(T data)
		{
			Node<T> curr = Head;
			while (curr != null && !curr.Data.Equals(data))
			{
				curr = curr.Next;
			}
			if (curr == null)
			{
				return false;
			}

			Node<T> prevNode = curr.Prev;
			Node<T> nextNode = curr.Next;


			if (curr == Head)
			{
                if (Head.Next == null)
                {
                    Head = Tail = null;
                }
                else
                {
                    Head = Head.Next;
                    Head.Prev = null;
                }
            }
			else if (curr == Tail)
			{
				prevNode.Next = null;
				Tail = prevNode;
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
			Node<T> curr = Head;
			while (curr != null && !curr.Data.Equals(data))
			{
				curr = curr.Next;
				i++;
			}
			if (curr == null)
			{
				i = -1;
			}
			return i;
		}

		public int LastIndexOf(T data)
		{
			int i = Count - 1;
			Node<T> curr = Tail;
			while (curr != null && !curr.Data.Equals(data))
			{
				curr = curr.Prev;
				i--;
			}
			if (curr == null)
			{
				i = -1;
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
			Head = Tail = null;
			Count = 0;
		}

		public T? Get(int index)
		{
			if (index < 0 || index > (Count - 1))
			{
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
            Node<T> curr = Head;
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
                Head = prev.Prev;
            }
        }

		private Node<T> FindForward(int index)
		{
			int i = 0;
			Node<T> curr = Head;
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
			Node<T> curr = Tail;
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
			Node<T> curr = Head;
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
			else if (buf.Length == 0) buf = "Empty - Nothing To Display";
			return buf;
		}

        public void CopyTo(T[] array, int arrayIndex)
        {
			if(arrayIndex >= Count)
            {
				throw new IndexOutOfRangeException();
            }
			if(Count > array.Length - arrayIndex)
			{
				throw new ArgumentException();
			}
			Node<T> current = Head;
			for (int i = arrayIndex; i < Count; i++)
			{
				array.SetValue(current.Data, i);
				current = current.Next;
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			Node<T> current = Head;
			while (current != null)
			{
				yield return current.Data;
				current = current.Next;
			}
		}

		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			Node<T> current = Head;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
		}

        public static IEnumerable TestIEnumerable(IEnumerable source)
        {
            foreach (object o in source)
            {
                yield return o;
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
