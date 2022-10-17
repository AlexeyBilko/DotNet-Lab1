using AutoBogus;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Library.Tests
{
    public class StackUnitTests
    {
        [Test]
        public void Push_AddValueToEmptyStack_CountEquals1()
        {
            var stack = new Stack<int>();
            var itemToAdd = AutoFaker.Generate<int>();

            stack.Push(itemToAdd);
            stack.Count.Should().Be(1);
        }


        [Test]
        public void Push_AddNullValueToEmptyList_NullReferenceException()
        {
            var stack = new Stack<string>();
            string tmp = null;

            Action act = () => stack.Push(tmp);
            act.Should().Throw<NullReferenceException>();
        }

        [Test]
        public void Push_AddValueToEmptyStack_IsEmptyEqualsFalse()
        {
            var stack = new Stack<int>();
            var itemToAdd = AutoFaker.Generate<int>();

            stack.Push(itemToAdd);
            stack.IsEmpty().Should().BeFalse();
        }

        [Test]
        public void Push_AddValueToNotEmptyListOnIndex_StackContainsValue()
        {
            var stack = GenerateNotEmptyStack();
            var valueToAdd = AutoFaker.Generate<int>();

            stack.Push(valueToAdd);
            stack.Contains(valueToAdd).Should().BeTrue();
        }

        [Test]
        public void Contains_CheckOnExistingElement_True()
        {
            var stack = new Stack<int>();
            var value = AutoFaker.Generate<int>();

            stack.Push(value);
            stack.Contains(value).Should().BeTrue();
        }

        [Test]
        public void Contains_CheckOnNotExistingElement_False()
        {
            var list = GenerateNotEmptyStack();
            var value = AutoFaker.Generate<int>();

            while (list.Contains(value))
            {
                value = AutoFaker.Generate<int>();
            }
            list.Contains(value).Should().BeFalse();
        }

        [Test]
        public void ChangedHandler_PushItem()
        {
            var stack = new Stack<int>();
            var monitoredSubject = stack.Monitor();

            var value = AutoFaker.Generate<int>();
            stack.Push(value);

            monitoredSubject.Should().Raise("OnChanged");
        }

        [Test]
        public void PushedHandler_PushItem()
        {
            var stack = new Stack<int>();
            var monitoredSubject = stack.Monitor();

            var value = AutoFaker.Generate<int>();
            stack.Push(value);
            monitoredSubject.Should().Raise("OnPushed");
        }

        [Test]
        public void PopedHandler_PushItem()
        {
            var stack = GenerateNotEmptyStack();
            var monitoredSubject = stack.Monitor();
            
            stack.Pop();
            monitoredSubject.Should().Raise("OnPoped");
        }

        [Test]
        public void ClearedHandler_PushItem()
        {
            var stack = GenerateNotEmptyStack();
            var monitoredSubject = stack.Monitor();

            stack.Clear();
            monitoredSubject.Should().Raise("OnClear");
        }

        [Test]
        public void PeekedHandler_PushItem()
        {
            var stack = GenerateNotEmptyStack();
            var monitoredSubject = stack.Monitor();

            stack.Peek();
            monitoredSubject.Should().Raise("OnPeeked");
        }

        [Test]
        public void IsReadOnly_EmptyList_False()
        {
            var stack = new Stack<int>();
            stack.IsReadOnly.Should().BeFalse();
        }


        [Test]
        public void IsReadOnly_NotEmptyList_False()
        {
            var stack = GenerateNotEmptyStack();
            stack.IsReadOnly.Should().BeFalse();
        }

        [Test]
        public void Peek_NotEmptyList_ReturnsTopElement()
        {
            var stack = GenerateNotEmptyStack();

            var value = AutoFaker.Generate<int>();
            stack.Push(value);

            var topElement = stack.Peek();

            topElement.Should().Be(value);
        }

        [Test]
        public void Peek_EmptyList_InvalidOperationException()
        {
            var stack = new Stack<int>();

            Action act = () => stack.Peek();
            act.Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void Pop_NotEmptyList_StackDoNotContainsTopElement()
        {
            var stack = GenerateNotEmptyStack();
            var topElement = stack.Peek();

            stack.Pop();
            stack.Contains(topElement).Should().BeFalse();
        }

        [Test]
        public void Pop_NotEmptyList_PoppedElementRetuns()
        {
            var stack = GenerateNotEmptyStack();
            var topElement = stack.Peek();

            var returnedItem = stack.Pop();
            returnedItem.Should().Be(topElement);
        }

        [Test]
        public void Pop_EmptyList_StackDonotContainsTopElement()
        {
            var stack = new Stack<int>();

            Action act = () => stack.Pop();
            act.Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void Clear_Count_EqualsZero()
        {
            var list = GenerateNotEmptyStack();

            list.Clear();
            list.Count.Should().Be(0);
        }

        [Test]
        public void Clear_IsEmpty_Truw()
        {
            var list = GenerateNotEmptyStack();

            list.Clear();
            list.IsEmpty().Should().BeTrue();
        }

        [Test]
        public void ToString_NotEmptyStack_ListElementsInString()
        {
            var stack = new Stack<int>();
            var value1 = AutoFaker.Generate<int>();
            var value2 = AutoFaker.Generate<int>();
            stack.Push(value1);
            stack.Push(value2);
            string expectedResult = $"{value1},{value2}";
            stack.ToString().Should().Be(expectedResult);
        }

        [Test]
        public void ToString_EmptyStack_StringWithMessageAboutEmpty()
        {
            var stack = new Stack<int>();
            string expectedResult = "Empty - Nothing To Display";
            stack.ToString().Should().Be(expectedResult);
        }

        [Test]
        public void GetEnumerator_NotEmptyStack_ForEachWorks()
        {
            var stack = GenerateNotEmptyStack();
            var array = new int[stack.Count];
            stack.CopyTo(array, 0);
            int i = 0;
            foreach (var item in stack)
            {
                item.Should().Be(array[i++]);
            }
        }

        [Test]
        public void IsSynchronized_NotEmptyStack_ReturnFalse()
        {
            var stack = GenerateNotEmptyStack();
            stack.IsSynchronized.Should().BeFalse();
        }

        [Test]
        public void IsSynchronized_EmptyStack_ReturnFalse()
        {
            var stack = new Stack<int>();
            stack.IsSynchronized.Should().BeFalse();
        }

        [Test]
        public void SyncRoot_NotEmptyStack_Null()
        {
            var stack = GenerateNotEmptyStack();
            stack.SyncRoot.Should().BeNull();
        }

        [Test]
        public void SyncRoot_EmptyStack_Null()
        {
            var stack = new Stack<int>();
            stack.SyncRoot.Should().BeNull();
        }

        [Test]
        public void CopyTo_CopyAllStackToArray_EveryElementMatches()
        {
            var stack = GenerateNotEmptyStack();
            var tmp = stack.ToArray();
            int[] array = new int[stack.Count];
            stack.CopyTo(array, 0);
            for (int i = 0; i < array.Length; i++)
            {
                array[i].Should().Be(tmp[tmp.Length - 1 - i]);
            }
        }

        [Test]
        public void CopyTo_EmptyList_ArrayFirstElementStaysTheSame()
        {
            var stack = new Stack<int>();

            int[] array = new int[5];
            int firstElemet = array[0];
            stack.CopyTo(array, 0);
            int newFirstElemnt = array[0];
            firstElemet.Should().Be(newFirstElemnt);
        }

        [Test]
        public void CopyTo_ArrayTooSmall_ArgumentException()
        {
            var stack = GenerateNotEmptyStack();

            int[] array = new int[2];

            Action act = () => stack.CopyTo(array, 0);
            act.Should().Throw<ArgumentException>();
        }

        [Test]
        public void CopyTo_NegativeIndex_IndexOutOfRangeException()
        {
            var stack = GenerateNotEmptyStack();

            int[] array = new int[5];
            Action act = () => stack.CopyTo(array, -2);
            act.Should().Throw<IndexOutOfRangeException>();
        }

        [Test]
        public void CopyTo_NullArray_NullReferenceException()
        {
            var stack = new Stack<int>();
            int[] array = null;

            Action act = () => stack.CopyTo(array, 0);
            act.Should().Throw<NullReferenceException>();
        }

        [Test]
        public void ToArray_NullStack_NullReferenceException()
        {
            Stack<int> stack = null;
            

            Action act = () => stack.ToArray();
            act.Should().Throw<NullReferenceException>();
        }

        [Test]
        public void ToArray_NotEmptyStack_EachElementMatches()
        {
            var stack = new Stack<int>();
            int value1 = AutoFaker.Generate<int>();
            int value2 = AutoFaker.Generate<int>();
            int value3 = AutoFaker.Generate<int>();
            var tmpArray = new int[] { value1, value2, value3 };
            stack.Push(value1);
            stack.Push(value2);
            stack.Push(value3);

            var array = stack.ToArray();
            for (int i = 0; i < array.Length; i++)
            {
                array[i].Should().Be(tmpArray[tmpArray.Length - 1 - i]);
            }
        }

        [Test]
        public void ToArray_EmptyStack_EmptyArray()
        {
            var stack = new Stack<int>();
            var array = stack.ToArray();
            array.Should().BeEmpty();
        }

        [Test]
        public void GetEnumerator_NotGeneric_IsCorrect()
        {
            var stack = GenerateNotEmptyStack();
            int[] tmpArray = new int[stack.Count];
            stack.CopyTo(tmpArray, 0);
            IEnumerable weak = Stack<int>.TestIEnumerable(stack);
            var sequence = weak.Cast<int>().Take(3).ToArray();
            CollectionAssert.AreEqual(sequence,
                new[] { tmpArray[^5], tmpArray[^4], tmpArray[^3] });
        }

        private Stack<int> GenerateNotEmptyStack()
        {
            var stack = new Stack<int>();
            stack.Push(AutoFaker.Generate<int>());
            stack.Push(AutoFaker.Generate<int>());
            stack.Push(AutoFaker.Generate<int>());
            stack.Push(AutoFaker.Generate<int>());
            stack.Push(AutoFaker.Generate<int>());
            return stack;
        }
    }
}