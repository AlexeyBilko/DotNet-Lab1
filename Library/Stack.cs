using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
	public class Stack<T> : ICollection<T>, IEnumerable<T>, IReadOnlyCollection<T>
	{
		private DoubleLinkedList<T> list = null;

        public bool IsReadOnly => false;
        public int Count => list.Count;

		public delegate void ChangeHandler(Stack<T> sender);
		public event ChangeHandler OnChanged;
		public delegate void PushHandler(Stack<T> sender);
		public event PushHandler OnPushed;
		public delegate void PopHandler(Stack<T> sender);
		public event PopHandler OnPoped;
		public delegate void PeekHandler(Stack<T> sender);
		public event PeekHandler OnPeeked;

		public Stack()
		{
			this.list = new DoubleLinkedList<T>();
		}

		protected virtual void ChangedHandler()
		{
			OnChanged.Invoke(this);
		}

		protected virtual void PushedHandler()
		{
			OnPushed.Invoke(this);
			OnChanged.Invoke(this);
		}

		protected virtual void PopedHandler()
		{
			OnPoped.Invoke(this);
			OnChanged.Invoke(this);
		}

		protected virtual void PeekedHandler()
		{
			OnPeeked.Invoke(this);
			OnChanged.Invoke(this);
		}

		private void Push(T data)
		{
			list.Add(data);
			PushedHandler();
			Console.WriteLine(data.ToString() + " inserted into stack");
		}

		public void Pop()
		{
			if (IsEmpty())
			{
				Console.WriteLine("Stack is Empty");
				return;
			}
			T data = Peek();
			list.RemoveAt(Count - 1);
			PopedHandler();
			Console.WriteLine(data.ToString() + " removed from stack");
		}

		public T Peek()
		{
			PeekedHandler();
			return list.Get(Count - 1);
		}

		public bool IsEmpty()
		{
			return list.IsEmpty();
		}
		public override string ToString()
		{
			return list.ToString();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)list).GetEnumerator();
		}

		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return ((IEnumerable<T>)list).GetEnumerator();
		}

		public void Add(T item)
        {
			Push(item);
        }

        public void Clear()
        {
            list.Clear();
			ChangedHandler();
		}

        public bool Contains(T item)
        {
            return list.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
			if (array == null)
			{
				throw new NullReferenceException();
			}
			if (arrayIndex < 0 || arrayIndex > array.Length)
			{
				throw new IndexOutOfRangeException();
			}
			if (array.Length - arrayIndex < Count)
			{
				throw new ArgumentException();
			}
			for (int i = arrayIndex; i < list.Count; i++)
            {
				array.SetValue(list.Get(i), arrayIndex);
            }
        }

		public T[] ToArray()
        {
			if(list == null) return null;
			T[] objArray = new T[Count];
			int i = 0;
			while (i < Count)
			{
				objArray[i] = list.Get(Count - i - 1);
				i++;
			}
			return objArray;
		}

        public bool Remove(T item)
        {
			return list.Remove(item);
        }
	}
}
