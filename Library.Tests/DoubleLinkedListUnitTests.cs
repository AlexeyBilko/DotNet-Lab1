using AutoBogus;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections;
using System.Linq;

namespace Library.Tests
{
    public class DoubleLinkedListUnitTests
    {
        [Test]
        public void Add_AddValueToEmptyList_CountEquals1()
        {
            var list = new DoubleLinkedList<int>();
            var itemToAdd = AutoFaker.Generate<int>();

            list.Add(itemToAdd);
            list.Count.Should().Be(1);
        }

        [Test]
        public void Add_AddValueToEmptyList_HeadDataEqualsValue()
        {
            var list = new DoubleLinkedList<int>();
            var itemToAdd = AutoFaker.Generate<int>();

            list.Add(itemToAdd);
            list.Head.Data.Should().Be(itemToAdd);
        }

        [Test]
        public void Add_AddValueToEmptyList_TailDataEqualsValue()
        {
            var list = new DoubleLinkedList<int>();
            var itemToAdd = AutoFaker.Generate<int>();

            list.Add(itemToAdd);
            list.Tail.Data.Should().Be(itemToAdd);
        }

        [Test]
        public void Add_AddValueToEmptyList_IsEmptyEqualsFalse()
        {
            var list = new DoubleLinkedList<int>();
            var itemToAdd = AutoFaker.Generate<int>();

            list.Add(itemToAdd);
            list.IsEmpty().Should().BeFalse();
        }

        [Test]
        public void Add_AddValueToNotEmptyList_TailDataEqualsValue()
        {
            var list = GenerateNotEmptyList();

            var itemToAdd = AutoFaker.Generate<int>();

            list.Add(itemToAdd);
            list.Tail.Data.Should().Be(itemToAdd);
        }


        [Test]
        public void Add_AddValueToNotEmptyListOnIndex_ListContainsValue()
        {
            var list = GenerateNotEmptyList();

            var valueToAdd = AutoFaker.Generate<int>();

            list.Add(valueToAdd,2);
            list.Contains(valueToAdd).Should().BeTrue();
        }

        [Test]
        public void Add_AddValueToNotEmptyListOnIndexLowerThanZero_IndexOutOfRangeException()
        {
            var list = GenerateNotEmptyList();

            var valueToAdd = AutoFaker.Generate<int>();

            Action act = () => list.Add(valueToAdd, int.MinValue);
            act.Should().Throw<IndexOutOfRangeException>();
        }

        [Test]
        public void Add_AddValueToNotEmptyListOnIndexBiggerThanCountMinusOne_IndexOutOfRangeException()
        {
            var list = GenerateNotEmptyList();

            var valueToAdd = AutoFaker.Generate<int>();

            Action act = () => list.Add(valueToAdd, list.Count);
            act.Should().Throw<IndexOutOfRangeException>();
        }

        [Test]
        public void IsReadOnly_EmptyList_False()
        {
            var list = new DoubleLinkedList<int>();
            list.IsReadOnly.Should().BeFalse();
        }


        [Test]
        public void IsReadOnly_NotEmptyList_False()
        {
            var list = GenerateNotEmptyList();
            list.IsReadOnly.Should().BeFalse();
        }

        [Test]
        public void Contains_CheckOnExistingElement_True()
        {
            var list = new DoubleLinkedList<int>();
            var value = AutoFaker.Generate<int>();
            list.Add(value);
            list.Contains(value).Should().BeTrue();
        }

        [Test]
        public void Contains_CheckOnNotExistingElement_False()
        {
            var list = GenerateNotEmptyList();
            var value = AutoFaker.Generate<int>();
            while (list.Contains(value))
            {
                value = AutoFaker.Generate<int>();
            }
            list.Contains(value).Should().BeFalse();
        }

        [Test]
        public void Set_ValueOnIndex_DataOnThatIndexEqualsValue()
        {
            var list = GenerateNotEmptyList();

            var valueToSet = AutoFaker.Generate<int>();
            var index = 2;

            list.Set(valueToSet, index);
            list.Get(index).Should().Be(valueToSet);
        }

        [Test]
        public void Set_ValueOnIndexMoreThanCountMinusOne_IndexOutOfRangeException()
        {
            var list = GenerateNotEmptyList();

            var valueToSet = AutoFaker.Generate<int>();
            var index = list.Count;
            Action act = () => list.Set(valueToSet, index);
            act.Should().Throw<IndexOutOfRangeException>();
        }

        [Test]
        public void Set_ValueOnIndexLessThanZero_IndexOutOfRangeException()
        {
            var list = GenerateNotEmptyList();

            var valueToSet = AutoFaker.Generate<int>();
            var index = int.MinValue;
            Action act = () => list.Set(valueToSet, index);
            act.Should().Throw<IndexOutOfRangeException>();
        }

        [Test]
        public void Clear_ClearList_CountEqualsZero()
        {
            var list = GenerateNotEmptyList();

            list.Clear();
            list.Count.Should().Be(0);
        }

        [Test]
        public void Clear_ClearList_HeadEqualsNull()
        {
            var list = GenerateNotEmptyList();

            list.Clear();
            list.Head.Should().Be(null);
        }

        [Test]
        public void Clear_ClearList_TailEqualsNull()
        {
            var list = GenerateNotEmptyList();
            list.Clear();
            list.Tail.Should().Be(null);
        }

        [Test]
        public void Get_FromListWithIndexLowerThanZero_ReturnDefault()
        {
            var list = GenerateNotEmptyList();

            list.Get(int.MinValue).Should().Be(default(int));
        }

        [Test]
        public void Get_FromListWithIndexBiggerThanCountMinusOne_ReturnDefault()
        {
            var list = GenerateNotEmptyList();

            list.Get(list.Count).Should().Be(default(int));
        }


        [Test]
        public void Get_FromListWithExistingIndexMoreThanHalfCount_ReturnData()
        {
            var list = GenerateNotEmptyList();

            var valueToAdd = AutoFaker.Generate<int>();
            list.Add(valueToAdd);
            
            list.Get(list.Count - 1).Should().Be(valueToAdd);
        }

        [Test]
        public void Get_FromListWithExistingIndexLessThanHalfCount_ReturnData()
        {
            var list = GenerateNotEmptyList();

            var valueToAdd = AutoFaker.Generate<int>();
            list.Add(valueToAdd, 0);

            list.Get(0).Should().Be(valueToAdd);
        }

        [Test]
        public void RemoveAt_RemoveDataAtIndexFromList_True()
        {
            var list = GenerateNotEmptyList();

            list.RemoveAt(list.Count - 1).Should().BeTrue();
        }

        [Test]
        public void RemoveAt_RemoveDataFromListWithIndexLowerThanZero_False()
        {
            var list = GenerateNotEmptyList();

            list.RemoveAt(int.MinValue).Should().BeFalse();
        }


        [Test]
        public void RemoveAt_RemoveDataFromListWithBiggerThanCountMinusOne_False()
        {
            var list = GenerateNotEmptyList();

            list.RemoveAt(list.Count).Should().BeFalse();
        }

        [Test]
        public void RemoveAt_RemoveDataFromEmptyList_False()
        {
            var list = new DoubleLinkedList<int>();

            list.RemoveAt(0).Should().BeFalse();
        }

        [Test]
        public void RemoveAt_RemoveDataFromListContainingOneElement_TrueAndListEmpty()
        {
            var list = new DoubleLinkedList<int>();
            var itemToAdd = AutoFaker.Generate<int>();
            list.Add(itemToAdd);
            list.RemoveAt(0).Should().BeTrue();
            list.Should().BeEmpty();
        }

        [Test]
        public void RemoveAt_RemoveFirstFromListContainingTwoElements_True()
        {
            var list = new DoubleLinkedList<int>();
            var itemToAdd1 = AutoFaker.Generate<int>();
            var itemToAdd2 = AutoFaker.Generate<int>();
            list.Add(itemToAdd1);
            list.Add(itemToAdd2);
            list.RemoveAt(0).Should().BeTrue();
        }

        [Test]
        public void RemoveAt_RemoveSecondFromListContainingTwoElements_True()
        {
            var list = new DoubleLinkedList<int>();
            var itemToAdd1 = AutoFaker.Generate<int>();
            var itemToAdd2 = AutoFaker.Generate<int>();
            list.Add(itemToAdd1);
            list.Add(itemToAdd2);
            list.RemoveAt(0).Should().BeTrue();
        }

        [Test]
        public void RemoveAt_RemoveDataFromListContainingOneElement_HeadAndTailNulls()
        {
            var list = new DoubleLinkedList<int>();
            var itemToAdd = AutoFaker.Generate<int>();
            list.Add(itemToAdd);
            list.RemoveAt(0).Should().BeTrue();
            list.Head.Should().BeNull();
            list.Tail.Should().BeNull();
        }

        [Test]
        public void RemoveAt_RemoveMiddleElement_True()
        {
            var list = new DoubleLinkedList<int>();
            var itemToAdd1 = AutoFaker.Generate<int>();
            var itemToAdd2 = AutoFaker.Generate<int>();
            var itemToAdd3 = AutoFaker.Generate<int>();
            list.Add(itemToAdd1);
            list.Add(itemToAdd2);
            list.Add(itemToAdd3);
            list.RemoveAt(1).Should().BeTrue();
        }

        [Test]
        public void Remove_RemoveExistingDataFromList_True()
        {
            var list = GenerateNotEmptyList();
            
            var valueToRemove = list.Get(2);

            list.Remove(valueToRemove).Should().BeTrue();
        }

        [Test]
        public void Remove_RemoveExistingDataFromListWithOneElement_TrueAndListEmpty()
        {
            var list = new DoubleLinkedList<int>();
            var itemToAdd = AutoFaker.Generate<int>();
            list.Add(itemToAdd);
            list.Remove(itemToAdd).Should().BeTrue();
            list.Should().BeEmpty();
        }

        [Test]
        public void Remove_RemoveFirstFromListContainingTwoElements_True()
        {
            var list = new DoubleLinkedList<int>();
            var itemToAdd1 = AutoFaker.Generate<int>();
            var itemToAdd2 = AutoFaker.Generate<int>();
            list.Add(itemToAdd1);
            list.Add(itemToAdd2);
            list.Remove(itemToAdd1).Should().BeTrue();
        }

        [Test]
        public void Remove_RemoveSecondFromListContainingTwoElements_True()
        {
            var list = new DoubleLinkedList<int>();
            var itemToAdd1 = AutoFaker.Generate<int>();
            var itemToAdd2 = AutoFaker.Generate<int>();
            list.Add(itemToAdd1);
            list.Add(itemToAdd2);
            list.Remove(itemToAdd2).Should().BeTrue();
        }

        [Test]
        public void Remove_RemoveDataFromEmptyList_False()
        {
            var list = new DoubleLinkedList<int>();

            list.Remove(AutoFaker.Generate<int>()).Should().BeFalse();
        }

        [Test]
        public void Remove_RemoveDataFromListThatContainsOneElement_TrueAndEmptyList()
        {
            var list = new DoubleLinkedList<int>();

            var itemToAdd = AutoFaker.Generate<int>();
            list.Add(itemToAdd);
            list.Remove(itemToAdd).Should().BeTrue();
            list.IsEmpty().Should().BeTrue();
            list.Tail.Should().BeNull();
        }

        [Test]
        public void Remove_RemoveDataFromListThatContainsOneElement_TrueHeadAndTailNulls()
        {
            var list = new DoubleLinkedList<int>();

            var itemToAdd = AutoFaker.Generate<int>();
            list.Add(itemToAdd);
            list.Remove(itemToAdd).Should().BeTrue();
            list.Tail.Should().BeNull();
            list.Head.Should().BeNull();
        }

        [Test]
        public void Remove_RemoveFromListNotExistingData_False()
        {
            var list = GenerateNotEmptyList();
            var value = AutoFaker.Generate<int>();
            while (list.Contains(value))
            {
                value = AutoFaker.Generate<int>();
            }
            list.Remove(value).Should().BeFalse();
        }

        [Test]
        public void IndexOf_DataThatDoesNotExist_Minus1()
        {
            var list = GenerateNotEmptyList();
            var value = AutoFaker.Generate<int>();
            while (list.Contains(value))
            {
                value = AutoFaker.Generate<int>();
            }
            list.IndexOf(value).Should().Be(-1);
        }

        [Test]
        public void IndexOf_FirstElement_ZeroIndex()
        {
            var list = new DoubleLinkedList<int>();
            var value = AutoFaker.Generate<int>();
            list.Add(value);
            list.IndexOf(value).Should().Be(0);
        }

        [Test]
        public void LastIndexOf_DataThatDoesNotExist_Minus1()
        {
            var list = GenerateNotEmptyList();
            var value = AutoFaker.Generate<int>();
            while (list.Contains(value))
            {
                value = AutoFaker.Generate<int>();
            }
            list.LastIndexOf(value).Should().Be(-1);
        }

        [Test]
        public void LastIndexOf_FirstElement_Zero()
        {
            var list = new DoubleLinkedList<int>();
            var value = AutoFaker.Generate<int>();
            list.Add(value);
            list.LastIndexOf(value).Should().Be(0);
        }

        [Test]
        public void LastIndexOf_ListContainsTwoRepeatableElements_One()
        {
            var list = new DoubleLinkedList<int>();
            var value = AutoFaker.Generate<int>();
            list.Add(value);
            list.Add(value);
            list.LastIndexOf(value).Should().Be(1);
        }

        [Test]
        public void ToString_NotEmptyList_ListElementsInString()
        {
            var list = new DoubleLinkedList<int>();
            var value1 = AutoFaker.Generate<int>();
            var value2 = AutoFaker.Generate<int>();
            list.Add(value1);
            list.Add(value2);
            string expectedResult = $"{value1},{value2}";
            list.ToString().Should().Be(expectedResult);
        }

        [Test]
        public void ToString_EmptyList_StringWithMessageAboutEmpty()
        {
            var list = new DoubleLinkedList<int>();
            string expectedResult = "Empty - Nothing To Display";
            list.ToString().Should().Be(expectedResult);
        }

        [Test]
        public void CopyTo_NotEmptyListToArray_FirstArrayElementEqualsListHead()
        {
            var list = GenerateNotEmptyList();
            int[] array = new int[10];
            list.CopyTo(array, 1);
            array[1].Should().Be(list.Head.Data);
        }

        [Test]
        public void CopyTo_ArrayIndexMoreThanListCount_IndexOutOfRangeException()
        {
            var list = GenerateNotEmptyList();
            int[] array = new int[10];
            Action act = () => list.CopyTo(array, list.Count + 1);
            act.Should().Throw<IndexOutOfRangeException>();
        }

        [Test]
        public void CopyTo_ArrayLengthMinusArrayIndexLowerThanCount_ArgumentException()
        {
            var list = GenerateNotEmptyList();
            int[] array = new int[6];
            Action act = () => list.CopyTo(array, 4);
            act.Should().Throw<ArgumentException>();
        }

        [Test]
        public void GetEnumerator_NotEmptyList_ForEachWorks()
        {
            var list = GenerateNotEmptyList();
            var array = new int[list.Count];
            list.CopyTo(array, 0);
            int i = 0;
            foreach (var item in list)
            {
                item.Should().Be(array[i++]);
            }
        }

        [Test]
        public void GetEnumerator_NotGeneric_IsCorrect()
        {
            var list = GenerateNotEmptyList();
            IEnumerable weak = DoubleLinkedList<int>.TestIEnumerable(list);
            var sequence = weak.Cast<int>().Take(4).ToArray();
            CollectionAssert.AreEqual(sequence,
                new[] { list.Get(0), list.Get(1), list.Get(2), list.Get(3), });
        }


        [Test]
        public void Reverse_NotEmptyList_CorrectReversedList()
        {
            var list = new DoubleLinkedList<int>();
            var value1 = AutoFaker.Generate<int>();
            var value2 = AutoFaker.Generate<int>();
            list.Add(value1);
            list.Add(value2);
            string expectedResult = $"{value1},{value2}";
            list.ToString().Should().Be(expectedResult);
            list.Reverse();
            expectedResult = $"{value2},{value1}";
            list.ToString().Should().Be(expectedResult);

        }

        [Test]
        public void Reverse_EmptyList_Null()
        {
            var list = new DoubleLinkedList<int>();
            list.Reverse();
            list.Should().BeEmpty();
        }

        private DoubleLinkedList<int> GenerateNotEmptyList()
        {
            var list = new DoubleLinkedList<int>();
            list.Add(AutoFaker.Generate<int>());
            list.Add(AutoFaker.Generate<int>());
            list.Add(AutoFaker.Generate<int>());
            list.Add(AutoFaker.Generate<int>());
            list.Add(AutoFaker.Generate<int>());
            return list;
        }
    }
}