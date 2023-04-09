﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WECAnAPI
{
  class SplitNf
  {
    public static bool Handle(Get get,Set set,List<string> seged)
    {
      Dictionary<string,List<List<string>>> MaximumMatchingWordPtr = set.SimplifiedChinese == false ? get.MaximumMatchingWordTw : get.MaximumMatchingWordCn;


      bool haveHandle = false;
      for(int segedIndex = 0; segedIndex + 1 < seged.Count; segedIndex++)
      {// 是否拆量詞
        if(MaximumMatchingWordPtr.ContainsKey(seged[segedIndex + 1]) == false
            && (seged[segedIndex] == "那"
            || seged[segedIndex] == "這"
            || seged[segedIndex] == "某"
            || seged[segedIndex] == "有"
            || seged[segedIndex] == "每"
            || seged[segedIndex] == "那"
            || seged[segedIndex] == "这"
            || seged[segedIndex] == "某"
            || seged[segedIndex] == "有"
            || seged[segedIndex] == "每"
            || RuleBased.Neu.IsNeu(seged[segedIndex])
             ))
        {
          //雖然「年」可當Nf，但「年代」和「年度」次數較多，故不修正一致率上升較多
          if(seged[segedIndex + 1] == "年代" || seged[segedIndex + 1] == "年度")
          {
            continue;
          }

          List<string> posed = new List<string>();
          ToPoS.Handle(get,set,seged,posed,false);

          if(posed[segedIndex + 1] == "Nf")
          {
            continue;
          }

          string target = seged[segedIndex + 1];
          if(target.Length > 4 && get.PoSFreqAtWord(set,target.Substring(0,4),"Nf") > 0)
          {
            if (拆量詞(get, set, seged, posed, target, segedIndex, 4) == false) {
              continue;
            } else {
              haveHandle = true;
            }
          }
          else if(target.Length > 3 && get.PoSFreqAtWord(set,target.Substring(0,3),"Nf") > 0)
          {
            if (拆量詞(get, set, seged, posed, target, segedIndex, 3) == false) {
              continue;
            } else {
              haveHandle = true;
            }
          }
          else if(target.Length > 2 && get.PoSFreqAtWord(set,target.Substring(0,2),"Nf") > 0)
          {
            if (拆量詞(get, set, seged, posed, target, segedIndex, 2) == false) {
              continue;
            } else {
              haveHandle = true;
            }
          }
          else if(target.Length > 1 && get.PoSFreqAtWord(set,target.Substring(0,1),"Nf") > 0)
          {
            if (拆量詞(get, set, seged, posed, target, segedIndex, 1) == false) {
              continue;
            } else {
              haveHandle = true;
            }
          }
        }
      }
      return haveHandle;
    }
    public static bool 拆量詞 (Get get, Set set, List<string> seged, List<string> posed, string target, int segedIndex, int limit) {
      // seged, posed = 一(Neu)　份子(Na)　彈(hehe)
      // segedIndex = (數詞位置)
      // target = 份子彈
      // 可能量詞 = 份(量詞)
      // 可能名詞 = 子彈(後詞)
      if(
        get.PoSFreqAtWord(set,target.Substring(0,limit),"Nf") <= get.PoSFreqAtWord(set,target,posed[segedIndex + 1]) || 
        RuleBased.Nd.合併時間詞.Contains(target) 
        ) {
        return false;
      }
      string 可能量詞 = target.Substring(0, limit);
      string 可能名詞 = target.Substring(limit);
      var 單雙分開seged = new List<string>(seged);
      var 單雙分開posed = new List<string>(posed);
      bool 可往後合併 = (segedIndex + 2 < seged.Count && get.IsWord(set, 可能名詞 + seged[segedIndex + 2]));// 後詞是否可往後合併
      if (可往後合併) {
        可能名詞 += seged[segedIndex + 2];
        單雙分開seged.RemoveAt(segedIndex + 2);
        單雙分開posed.RemoveAt(segedIndex + 2);
      } else if ((get.IsWord(set, 可能名詞)) == false) {
        return false;// 量詞後詞不存在字典，也不是日期
      }
      單雙分開seged[segedIndex + 1] = 可能量詞;
      單雙分開posed[segedIndex + 1] = "Nf";
      單雙分開seged.Insert(segedIndex + 2, 可能名詞);
      單雙分開posed.Insert(segedIndex + 2, "Na");
      ToPoS.Handle(get, set, 單雙分開seged, 單雙分開posed, false);
      if (posed[segedIndex + 1] != "Nd") {
        int position = SegMaximumMatching.Relative_Position(seged, posed, segedIndex + 1);
        double mergeValue = 計算前後機率(get,set,seged,posed,segedIndex + 1,1,position);
        // double splitValue = 計算前後機率(get,set,單雙分開seged,單雙分開posed,segedIndex + 1,2,position);
        double splitValue = 計算前後機率(get,set,單雙分開seged,單雙分開posed,segedIndex + 1,單雙分開seged.Count - seged.Count + 1,position);
        if (
          double.IsInfinity(mergeValue) == false && 
          double.IsInfinity(splitValue) == false && 
          mergeValue > 0 && splitValue > 0 && 
          splitValue <= mergeValue
          ) {
          return false;
        }
        // position = SegMaximumMatching.Relative_Position(seged, posed, segedIndex);
        // mergeValue = 計算前後機率(get,set,seged,posed,segedIndex,1,position);
        // splitValue = 計算前後機率(get,set,單雙分開seged,單雙分開posed,segedIndex,2,position);
        // if(
        //   double.IsInfinity(mergeValue) == false && 
        //   double.IsInfinity(splitValue) == false && 
        //   mergeValue > 0 && splitValue > 0 && 
        //   splitValue <= mergeValue
        //   ) {
        //   return false;
        // }
      }
      if (可往後合併) {
        seged.RemoveAt(segedIndex + 2);
        posed.RemoveAt(segedIndex + 2);
      }
      seged[segedIndex + 1] = 可能量詞;
      // posed[segedIndex + 1] = "Nf";
      posed[segedIndex + 1] = 單雙分開posed[segedIndex + 1];
      seged.Insert(segedIndex + 2, 可能名詞);
      // posed.Insert(segedIndex + 2, "Na");
      posed.Insert(segedIndex + 2, 單雙分開posed[segedIndex + 2]);
      return true;
    }
    public static double 計算前後機率 (Get get,Set set,List<string> seged,List<string> posed,int startIndex,int wordCount,int position) {
      /*
      // wordCount = 1
           i
      一_  套好_ 的
           wi
      pa,  pi
           wb
           pb,  pb1

      // wordCount = 2
           i
      一_  套_  好_  的
           wi
      pa,  pi
                wb
                pb,  pb1
      */

      int shift = wordCount - 1;// +shift = 詞組最後一個
      // string pa = posed[startIndex - 1];// PoS at left
      string wi = seged[startIndex];// word at index
      string pi = posed[startIndex];// PoS at index
      string wb = seged[startIndex + shift];// word at shift
      string pb = posed[startIndex + shift];// PoS at shift
      // string pb1 = posed[startIndex + shift + 1];// PoS at shift + 1
      if(position == 0)
      {// 句中
        string pa = posed[startIndex - 1];
        string pb1 = posed[startIndex + shift + 1];

        double left = PoSTw.PairFreq(pa, pi) / Math.Pow(PoSTw.TotalFreq(pi), 2);
        double right = PoSTw.PairFreq(pb, pb1) / Math.Pow(PoSTw.TotalFreq(pb), 2);
        double here = get.PoSFreqAtWord(set, wi, pi) / get.PoSTotalFreqAtWord(set, wi);
        double there = get.PoSFreqAtWord(set, wb, pb) / get.PoSTotalFreqAtWord(set, wb);
        double score =
          (left + 0.0000000001) * 
          (right + 0.0000000001) * 
          Math.Sqrt(here + 0.0000000001) * 
          Math.Sqrt(there + 0.0000000001);
        
        return score;
      }
      else if(position == 1)
      {// 句首
        string pb1 = posed[startIndex + shift + 1];

        double left = PoSTw.BeginFreq(pi) / Math.Pow(PoSTw.TotalFreq(pi), 2);
        double right = PoSTw.PairFreq(pb, pb1) / Math.Pow(PoSTw.TotalFreq(pb), 2);
        double here = get.PoSFreqAtWord(set, wi, pi) / get.PoSTotalFreqAtWord(set, wi);
        double there = get.PoSFreqAtWord(set, wb, pb) / get.PoSTotalFreqAtWord(set, wb);
        double score =
          (left + 0.0000000001) * 
          (right + 0.0000000001) * 
          Math.Sqrt(here + 0.0000000001) * 
          Math.Sqrt(there + 0.0000000001);
        
        return score;
      }
      else if(position == 2)
      {// 句尾
        string pa = posed[startIndex - 1];
        
        double left = PoSTw.PairFreq(pa, pi) / Math.Pow(PoSTw.TotalFreq(pi), 2);
        double right = PoSTw.EndFreq(pb) / Math.Pow(PoSTw.TotalFreq(pb), 2);
        double here = get.PoSFreqAtWord(set, wi, pi) / get.PoSTotalFreqAtWord(set, wi);
        double there = get.PoSFreqAtWord(set, wb, pb) / get.PoSTotalFreqAtWord(set, wb);
        double score =
          (left + 0.0000000001) * 
          (right + 0.0000000001) * 
          Math.Sqrt(here + 0.0000000001) * 
          Math.Sqrt(there + 0.0000000001);
        
        return score;
      }
      else if(position == 11)
      {// 獨立詞
        double left = 1;
        double right = 1;
        double here = get.PoSFreqAtWord(set, wi, pi) / get.PoSTotalFreqAtWord(set, wi);
        double there = get.PoSFreqAtWord(set, wb, pb) / get.PoSTotalFreqAtWord(set, wb);
        double score =
          (left + 0.0000000001) * 
          (right + 0.0000000001) * 
          Math.Sqrt(here + 0.0000000001) * 
          Math.Sqrt(there + 0.0000000001);

        if(pi.First() == 'N')
        {// 名詞類分數加權
          score *= 100;
        }

        return score;
      }
      else
      {
        throw new NotSupportedException();
      }
    }
  }
}
