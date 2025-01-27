﻿using System;
using System.Collections.Generic;

public class Program
{
	public static void Main(String[] args)
	{
		var d = new Dictionary<string, string>();
		Console.WriteLine(d.Insert("1", "1"));
		Console.WriteLine(d.Insert("dsa", "dsa"));
		Console.WriteLine(d.Insert("Nico", "Nico"));
		Console.WriteLine(d.Insert("Nata", "Nata"));
		Console.WriteLine(d.Insert("Cata", "Cata"));
		Console.WriteLine(d.Insert("Mama", "Mama"));
		Console.WriteLine(d.Insert("Papa", "Papa"));
		Console.WriteLine(d.Insert("Nico1", "Nico1"));
		Console.WriteLine(d.Insert("Nata1", "Nata1"));
		Console.WriteLine(d.Insert("Cata1", "Cata1"));
		Console.WriteLine(d.Insert("Mama1", "Mama1"));
		Console.WriteLine(d.Insert("Papa1", "Papa1"));
		Console.WriteLine(d.Insert("Nico2", "Nico2"));
		Console.WriteLine(d.Insert("Nata2", "Nata2"));
		Console.WriteLine(d.Insert("Cata2", "Cata2"));
		Console.WriteLine(d.Insert("Mama2", "Mama2"));
		Console.WriteLine(d.Insert("Papa2", "Papa2"));
		Console.WriteLine(d.Insert("Nico3", "Nico3"));
		Console.WriteLine(d.Insert("Nata3", "Nata3"));
		Console.WriteLine(d.Insert("Cata3", "Cata3"));
		Console.WriteLine(d.Insert("Mama3", "Mama3"));
		Console.WriteLine(d.Insert("Papa3", "Papa3"));
		Console.WriteLine(d.Insert("Nico3", "Nico3"));
		Console.WriteLine(d.Insert("Nata3", "Nata3"));
		Console.WriteLine(d.Insert("Cata3", "Cata3"));
		Console.WriteLine(d.Insert("Mama3", "Mama3"));
		Console.WriteLine(d.Insert("Papa3", "Papa3"));

		string mama3;
		Console.WriteLine(d.GetByKey("Mama3", out mama3));
		Console.WriteLine(mama3);
		string dx;
		Console.WriteLine(d.GetByKey("dx", out dx));
		Console.WriteLine(dx);

		Console.WriteLine(d.Remove("Nico"));
		Console.WriteLine(d.Remove("Nico"));
		Console.WriteLine(d.Remove("Nico7"));
	}
}


public class Dictionary<TKey, TValue> where TKey : IEquatable<TKey>
{
	private Node[] LinkedLists { get; set; }
	private int Count;

	public Dictionary(int initialCapacity = 10)
	{
		LinkedLists = new Node[initialCapacity];
		Count = 0;
	}

	public bool Insert(TKey key, TValue value)
	{
		RehashIfNecessary();
		if (Insert(LinkedLists, key, value))
		{
			Count++;
			return true;
		}
		return false;
	}

	private bool Insert(Node[] linkedLists, TKey key, TValue value)
	{
		var index = GetIndex(linkedLists.Length, key);
		var newNode = new Node(key, value);
		if (linkedLists[index] == null)
		{
			linkedLists[index] = newNode;
		}
		else
		{
			// The list is not empty, iterate through to make sure the key does not already exist
			Node pointer = linkedLists[index];
			while (pointer != null)
			{
				if (pointer.Key.Equals(newNode.Key))
				{
					return false;
				}
				else
				{
					if (pointer.Next == null) {
						pointer.Next = newNode;
						pointer = null;
					}
					else
					{
						pointer = pointer.Next;
					}
				}
			}
		}
		return true;
	}

	public bool GetByKey(TKey key, out TValue value)
	{
		var index = GetIndex(LinkedLists.Length, key);
		Node pointer = LinkedLists[index];
		while (pointer != null) {
			if (pointer.Key.Equals(key)) {
				value = pointer.Value;
				return true;
			} else {
				pointer = pointer.Next;
			}
		}
		value = default(TValue);
		return false;
	}

	public bool Remove(TKey key)
	{
		var index = GetIndex(LinkedLists.Length, key);
		if (LinkedLists[index] == null) {
			return false;
		} else if (LinkedLists[index].Key.Equals(key)) {
			LinkedLists[index] = LinkedLists[index].Next;
			return true;
		} else {
			// The list is not empty and key is not at the head
			Node pointer = LinkedLists[index];
			while (pointer.Next != null)
			{
				if (pointer.Next.Key.Equals(key))
				{
					pointer.Next = pointer.Next.Next;
					return true;
				} else
				{
					pointer = pointer.Next;
				}
			}
			return false;
		}
	}

	private int GetIndex(int numberOfPositions, TKey key)
	{
		var hash = key.GetHashCode();
		var positive = hash & 0x7FFFFFFF;
		return positive % numberOfPositions;
	}

	private void RehashIfNecessary()
	{
		if (LinkedLists.Length * 2 < Count)
		{
			var newLinkedLists = new Node[LinkedLists.Length * 4];
			var enumerable = GetAsEnumerable();
			foreach(var node in enumerable)
			{
				Insert(newLinkedLists, node.Key, node.Value);
			}
			LinkedLists = newLinkedLists;
		}
	}

	private IEnumerable<Node> GetAsEnumerable()
	{
		foreach (var linkedList in LinkedLists)
		{
			Node pointer = linkedList;
			while (pointer != null)
			{
				yield return pointer;
				pointer = pointer.Next;
			}
		}
	}

	private class Node
	{
		public TKey Key { get; set; }
		public TValue Value { get; set; }
		public Node Next { get; set; }

		public Node(TKey key, TValue value)
		{
			Key = key;
			Value = value;
		}
	}
}
