using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Extension method: adds functionality to a class that already exists
//1. It can't inherit from anything
//2. Static
public static class ListSortExtensions
{
    //Static, method
    //In the case of list we give it T
    //The parameter is key: this, the class we're extending. 
    //This is not actually a parameter, it is saying we are passing this object.
    //Only list is extended by this function
    public static void BubbleSort <T> (this List<T> list) where T:IComparable //Ensures the type has a comparator
    {
        T temp;

        for (int j = 0; j < list.Count - 1; j++)
        {
            for (int i = 0; i < list.Count - 1; i++)
            {
                IComparable first = list[i];
                IComparable second = list[i + 1];

                int comparison = first.CompareTo(second);
                if (comparison > 0)
                {
                    temp = list[i + 1];
                    list[i + 1] = list[i];
                    list[i] = temp;
                }
            }
        }
        
        

       
    }
}