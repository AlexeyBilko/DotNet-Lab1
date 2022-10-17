using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
	public class Stack<T> : ICollection, IEnumerable<T>, IEnumerable
	{
		private DoubleLinkedList<T> list = null;
        public bool IsReadOnly => false;
        public int Count => list.Count;
        public bool IsSynchronized { get; }
        public object SyncRoot { get; }

		public event Action OnChanged;
		public event Action OnPushed;
		public event Action OnPoped;
		public event Action OnPeeked;
		public event Action OnClear;

		public Stack()
		{
			this.list = new DoubleLinkedList<T>();
		}

		protected virtual void ChangedHandler()
		{
			Action Changed = Volatile.Read(ref OnChanged);
			if (Changed != null) 
				Changed();
		}

		protected virtual void PushedHandler()
		{
			Action Pushed = Volatile.Read(ref OnPushed);
			if (Pushed != null)
				Pushed();

			ChangedHandler();
		}

		protected virtual void PopedHandler()
		{
			Action Poped = Volatile.Read(ref OnPoped);
			if (Poped != null)
				Poped();

			ChangedHandler();
		}

		protected virtual void ClearedHandler()
		{
			Action Cleared = Volatile.Read(ref OnClear);
			if (Cleared != null)
				Cleared();

			ChangedHandler();
		}

		protected virtual void PeekedHandler()
		{
			Action Peeked = Volatile.Read(ref OnPeeked);
			if (Peeked != null)
				Peeked();
		}

		public void Push(T data)
		{
            PushedHandler();
			if (data != null)
				list.Add(data);
			else throw new NullReferenceException();
		}

		public T Pop()
		{
			PopedHandler();
			if (IsEmpty())
			{
				throw new InvalidOperationException();
			}
			T data = Peek();
			list.RemoveAt(Count - 1);
            return data;
        }

		public T Peek()
		{
			PeekedHandler();
            if (IsEmpty())
            {
                throw new InvalidOperationException();
            }
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

        public void Clear()
		{
			ClearedHandler();
			list.Clear();
		}

        public static IEnumerable TestIEnumerable(IEnumerable source)
        {
            foreach (object o in source)
            {
                yield return o;
            }
        }
		
		public T[] ToArray()
        {
			if(list == null) throw new NullReferenceException();
			T[] objArray = new T[Count];
			int i = 0;
			while (i < Count)
			{
				objArray[i] = list.Get(Count - i - 1);
				i++;
			}
			return objArray;
		}
        public void CopyTo(Array array, int arrayIndex)
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
			if (IsEmpty()) return;
			list.CopyTo(array as T[],arrayIndex);
        }
    }
}
