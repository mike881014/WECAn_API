using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WECAnAPI
{
  public class Tree
  {
    string name;
    int count = 0;
    Tree father;
    Dictionary<string,Tree> child = new Dictionary<string,Tree>();

    public Tree() { throw new System.NotSupportedException(@"Tree()"); }
    public Tree(Tree father,string name)
    {
      this.father = father;
      this.name = name;
    }

    public void Add(string[] nodeList)
    {
      Add(nodeList,0,nodeList.Count() - 1);
    }
    public void Add(List<string> nodeList)
    {
      Add(nodeList,0,nodeList.Count() - 1);
    }
    public void Add(string[] nodeList,int startIndex)
    {
      Add(nodeList,startIndex,nodeList.Count() - 1);
    }
    public void Add(List<string> nodeList,int startIndex)
    {
      Add(nodeList,startIndex,nodeList.Count() - 1);
    }
    public void Add(string[] nodeList,int index,int endIndex)
    {
      this.count++;
      if(index <= endIndex)
      {
        if(child.ContainsKey(nodeList[index]) == false) { child.Add(nodeList[index],new Tree(this,nodeList[index])); }
        child[nodeList[index]].Add(nodeList,index + 1,endIndex);
      }
    }
    public void Add(List<string> nodeList,int index,int endIndex)
    {
      this.count++;
      if(index <= endIndex)
      {
        if(child.ContainsKey(nodeList[index]) == false) { child.Add(nodeList[index],new Tree(this,nodeList[index])); }
        child[nodeList[index]].Add(nodeList,index + 1,endIndex);
      }
    }
    public int GetCount(string[] nodeList)
    {
      return GetCount(nodeList,0,nodeList.Count() - 1);
    }
    public int GetCount(List<string> nodeList)
    {
      return GetCount(nodeList,0,nodeList.Count() - 1);
    }
    public int GetCount(string[] nodeList,int startIndex)
    {
      return GetCount(nodeList,startIndex,nodeList.Count() - 1);
    }
    public int GetCount(List<string> nodeList,int startIndex)
    {
      return GetCount(nodeList,startIndex,nodeList.Count() - 1);
    }
    public int GetCount(string[] nodeList,int index,int endIndex)
    {
      if(index > endIndex)
      {
        return GetCount(nodeList,index + 1,endIndex);
      }
      return this.count;
    }
    public int GetCount(List<string> nodeList,int index,int endIndex)
    {
      if(index > endIndex)
      {
        return GetCount(nodeList,index + 1,endIndex);
      }
      return this.count;
    }
  }
}
