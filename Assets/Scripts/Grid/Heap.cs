using System.Collections.Generic;
using UnityEngine;

public class Heap
{
    public int Count => count;
    public bool IsEmpty => count == 0;

    PathNode[] items;
    HashSet<PathNode> lookup;
    int count;
    
    public Heap(int maxSize)
    {
        items = new PathNode[maxSize];
        lookup = new HashSet<PathNode>();
        count = 0;
    }
    
    public void Clear()
    {
        count = 0;
    }

    public bool Contains(PathNode node)
    {
        return lookup.Contains(node);
    }
    
    public void Add(PathNode node)
    {
        items[count] = node;
        lookup.Add(node);
        SortUp(count);
        count++;
    }
    
    public PathNode Remove()
    {
        PathNode min = items[0];
        lookup.Remove(min);
        count--;
        items[0] = items[count];
        SortDown(0);
        
        return min;
    }
    
    void SortUp(int index)
    {
        while(index > 0)
        {
            int parent = (index - 1) / 2;
            if(items[index].F >= items[parent].F)
            {
                break;
            }
            
            Swap(index, parent);
            index = parent;
        }
    }
    
    void SortDown(int index)
    {
        while(true)
        {
            int left = 2 * index + 1;
            int right = 2 * index + 2;
            int smallest = index;
            
            if(left < count && items[left].F < items[smallest].F)
            {
                smallest = left;
            }
            if(right < count && items[right].F < items[smallest].F)
            {
                smallest = right;
            }
                
            if(smallest == index) 
            {
                break;
            }
            
            Swap(index, smallest);
            index = smallest;
        }
    }
    
    void Swap(int i, int j)
    {
        (items[i], items[j]) = (items[j], items[i]);
    }
}
