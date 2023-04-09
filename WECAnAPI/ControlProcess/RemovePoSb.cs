using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WECAnAPI
{
  class RemovePoSb
  {
    /// <summary>
    /// 去除 <paramref name="posed"/> 當中詞類為"b"的情況
    /// </summary>
    /// <param name="get"></param>
    /// <param name="set"></param>
    /// <param name="seged"></param>
    /// <param name="posed"></param>
    public static void Handle(Get get,Set set,List<string> seged,List<string> posed)//將詞性為b的詞揀選出來，並依照該詞被判b的方式採用取代或是刪除b再次標記，替代詞性13個為師大日龢提供，詞頻是採用本身詞性出現的總頻率建置
    {
      for(int segedIndex = 0; segedIndex < seged.Count; ++segedIndex)
      {
        if(posed[segedIndex] == "b")
        {
          if(get.IsWord(set,seged[segedIndex]) == false)
          {//如果字典檔不存在這詞
            List<string> traSeged = new List<string>();
            traSeged.Add((segedIndex - 1 >= 0) ? (seged[segedIndex - 1]) : ("●"));
            traSeged.Add("去除詞類b專用特殊詞");
            traSeged.Add((segedIndex + 1 < seged.Count) ? (seged[segedIndex + 1]) : ("●"));
            List<string> traPos = new List<string>();
            ToPoS.Handle(get,set,traSeged,traPos,true);
            posed[segedIndex] = traPos[1];
          }
          else
          {
            List<PoS.WordPoS> wordPoS = get.WordPoSList(set,seged[segedIndex]);

            if(wordPoS.Count() == 1)
            {//如果這個詞只有唯一一種詞性b
              List<string> traSeged = new List<string>();
              traSeged.Add((segedIndex - 1 >= 0) ? (seged[segedIndex - 1]) : ("●"));
              traSeged.Add("去除詞類b專用特殊詞");
              traSeged.Add((segedIndex + 1 < seged.Count) ? (seged[segedIndex + 1]) : ("●"));
              List<string> traPos = new List<string>();
              ToPoS.Handle(get,set,traSeged,traPos,true);
              posed[segedIndex] = traPos[1];
            }
            else if(wordPoS.Count() == 2)
            {//如果這個詞除了b只有另一種可能詞性
              List<string> tem = new List<string>();
              foreach(PoS.WordPoS item in wordPoS)
              {
                if(item.PoS != "b")
                {
                  tem.Add(item.PoS);
                }
              }
              if(tem.Count() == 1)
              {
                posed[segedIndex] = tem[0];
              }
              else
              {
                List<string> traSeged = new List<string>();
                traSeged.Add((segedIndex - 1 >= 0) ? (seged[segedIndex - 1]) : ("●"));
                traSeged.Add("去除詞類b專用特殊詞");
                traSeged.Add((segedIndex + 1 < seged.Count) ? (seged[segedIndex + 1]) : ("●"));
                List<string> traPos = new List<string>();
                ToPoS.Handle(get,set,traSeged,traPos,true);
                posed[segedIndex] = traPos[1];
              }
            }
            else
            {//除了詞性b還有許多種其他詞性可能
              List<PoS.WordPoS> tem = new List<PoS.WordPoS>();
              for(int posIndex = wordPoS.Count() - 1; posIndex > -1; posIndex--)
              {
                if(wordPoS[posIndex].PoS == "b")
                {
                  tem.Add(wordPoS[posIndex]);
                  wordPoS.RemoveAt(posIndex);
                }
              }
              if(wordPoS.Count == 0)
              {
                List<string> traSeged = new List<string>();
                traSeged.Add((segedIndex - 1 >= 0) ? (seged[segedIndex - 1]) : ("●"));
                traSeged.Add("去除詞類b專用特殊詞");
                traSeged.Add((segedIndex + 1 < seged.Count) ? (seged[segedIndex + 1]) : ("●"));
                List<string> traPos = new List<string>();
                ToPoS.Handle(get,set,traSeged,traPos,true);
                posed[segedIndex] = traPos[1];
              }
              else if(wordPoS.Count == 1)
              {// 剩一個非b
                posed[segedIndex] = wordPoS[0].PoS;
              }
              else
              {// 非b兩個以上
                List<string> traSeged = new List<string>();
                traSeged.Add((segedIndex - 1 >= 0) ? (seged[segedIndex - 1]) : ("●"));
                traSeged.Add(seged[segedIndex]);
                traSeged.Add((segedIndex + 1 < seged.Count) ? (seged[segedIndex + 1]) : ("●"));
                List<string> traPos = new List<string>();
                ToPoS.Handle(get,set,traSeged,traPos,true);
                posed[segedIndex] = traPos[1];
              }
              wordPoS.AddRange(tem);
            }
          }
        }
      }
    }
  }
}
