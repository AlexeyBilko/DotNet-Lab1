using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
	public class Stack<T> : ICollection<T>, IEnumerable<T>, ICollection
	{
		private DoubleLinkedList<T> list = null;
        public bool IsReadOnly => false;
        public int Count => list.Count;

        public bool IsSynchronized => throw new NotImplementedException();

        public object SyncRoot => throw new NotImplementedException();

		public event Action OnChanged;
		public event Action OnPushed;
		public event Action OnPoped;
		public event Action OnPeeked;

		public Stack()
		{
			this.list = new DoubleLinkedList<T>();
		}

		protected virtual void ChangedHandler()
		{
			Action tempAction = Volatile.Read(ref OnChanged);
			if (tempAction != null) 
				tempAction();
		}

		protected virtual void PushedHandler()
		{
			Action tempAction = Volatile.Read(ref OnPushed);
			if (tempAction != null)
				tempAction();

			Action tempAction2 = Volatile.Read(ref OnChanged);
			if (tempAction2 != null)
				tempAction2();
		}

		protected virtual void PopedHandler()
		{
			Action tempAction = Volatile.Read(ref OnPoped);
			if (tempAction != null)
				tempAction();


			Action tempAction2 = Volatile.Read(ref OnChanged);
			if (tempAction2 != null)
				tempAction2();
		}

		protected virtual void PeekedHandler()
		{
			Action tempAction = Volatile.Read(ref OnPeeked);
			if (tempAction != null)
				tempAction();
		}

		private void Push(T data)
		{
			if (data != null)
				list.Add(data);
			else throw new NullReferenceException();
			PushedHandler();
		}

		public void Pop()
		{
			PopedHandler();
			if (IsEmpty())
			{
				return;
			}
			T data = Peek();
			list.RemoveAt(Count - 1);
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

        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }
    }
}
