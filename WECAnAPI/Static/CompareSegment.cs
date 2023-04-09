using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WECAnAPI
{
  public static class CompareSegment//順斷逆斷判斷
  {
    public struct IndexGroup
    {
      public List<int> GroupA, GroupB;
      public IndexGroup(List<int> _GroupA,List<int> _GroupB)
      {
        GroupA = _GroupA;
        GroupB = _GroupB;
      }
    }

    public static void CompareSegmentHandle(List<string> segmentA,List<string> segmentB,List<IndexGroup> result)
    {//比對SMM逆斷正斷分出來是否相同，判別LIST中之字數
      int indexA = -1;
      int indexB = -1;
      int charCountA = 0;
      int charCountB = 0;
      //SUBA，B負責記錄index的變化，包刮當不同長時需對齊，之index也會記錄
      List<int> subA = new List<int>();
      List<int> subB = new List<int>();
      while(indexA < segmentA.Count() - 1 || indexB < segmentB.Count() - 1)
      {
        if(charCountA == charCountB)
        {
          indexA++;
          charCountA += segmentA[indexA].Length;
          subA.Add(indexA);

          indexB++;
          charCountB += segmentB[indexB].Length;
          subB.Add(indexB);
        }
        else if(charCountA > charCountB)
        {
          indexB++;
          charCountB += segmentB[indexB].Length;
          subB.Add(indexB);
        }
        else
        {
          indexA++;
          charCountA += segmentA[indexA].Length;
          subA.Add(indexA);
        }

        if(charCountA == charCountB)
        {
          result.Add(new IndexGroup(new List<int>(subA),new List<int>(subB)));
          subA.Clear();
          subB.Clear();
        }
      }
    }

    public static double GetSegmentLengthVar(List<string> segment)
    {
      return GetSegmentLengthVar(segment,0,segment.Count());
    }
    public static double GetSegmentLengthVar(List<string> segment,List<int> indexGroup)
    {
      return GetSegmentLengthVar(segment,indexGroup[0],indexGroup.Count());
    }
    public static double GetSegmentLengthVar(List<string> segment,int startIndex,int count)
    {
      double averageLength = 0.0;
      double var = 0.0;
      for(int forCount = startIndex; forCount <= startIndex + count - 1; ++forCount)
      {
        averageLength += (double)segment[forCount].Length / (double)count;
      }
      for(int forCount = startIndex; forCount <= startIndex + count - 1; ++forCount)
      {
        var += Math.Pow(((double)segment[forCount].Length - averageLength),2);
      }
      return var;
    }

    //public static List<List<int>> OrderedGrouping(int number, int group, int lessNumber = 0)
    //{
    //    List<List<int>> result = new List<List<int>>();
    //    orderedGroupingRecursive(number, group, result, new List<int>());
    //    for (int forCount = result.Count() - 1; forCount >= 0; forCount--)
    //    {
    //        bool deleteFlag = false;
    //        foreach (int item in result[forCount])
    //        {
    //            if (item < lessNumber)
    //            {
    //                deleteFlag = true;
    //                break;
    //            }
    //        }
    //        if (deleteFlag == true)
    //        {
    //            result.RemoveAt(forCount);
    //        }
    //    }
    //    return result;
    //}
    //static void orderedGroupingRecursive(int number, int group, List<List<int>> result, List<int> log)
    //{
    //    if (group == 1)
    //    {
    //        log.Add(number);
    //        result.Add(new List<int>(log));
    //        return;
    //    }
    //    else
    //    {
    //        for (int subInt = 0; subInt <= number; subInt++)
    //        {
    //            List<int> subLog = new List<int>(log);
    //            subLog.Add(subInt);
    //            orderedGroupingRecursive(number - subInt, group - 1, result, subLog);
    //        }
    //    }
    //}
  }
}