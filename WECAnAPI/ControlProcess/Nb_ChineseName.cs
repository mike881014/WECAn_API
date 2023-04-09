using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WECAnAPI
{
  partial class Nb//partial為部分類別 將類別拆成幾個的部分
  {
    static Dictionary<string,string> namePrefix, nameSuffix;
    static Dictionary<string,int> ChinesePossibleNames;
    
    #region 
    static bool ChineseHandle(Get get,Set set,List<string> seged,List<string> posed)
    {
      bool handled = false;

      List<string> CollectName = new List<string>();

      for(int segedIndex = 0; segedIndex < seged.Count; segedIndex++)
      {
        bool mixedSegmentName = false;//詞組是否為斷在一起的名字(因斷在一起而不存在於百家姓)
        if(CouldBeAName(get,set,segedIndex,seged,posed,ref mixedSegmentName) == true && IsInNameCache(get,set,segedIndex,seged,posed,2,3) == false)
        {


          CollectName.Add(GetName(get,set,segedIndex,seged,posed,mixedSegmentName));//人名重組後的List

          if(CollectName.Last() == null)//?若只有姓
          {
            CollectName.Remove(null);
          }
          else
          {
            if(newName.combineSegs != 0 && CollectName.Last().Length > 1)
            {
              Position position = GetPosition(segedIndex,newName.seged,newName.posed);

              double mergeValue = GetProbability(get,set,newName.seged,newName.posed,segedIndex,1,position,true);

              double splitValue = GetProbability(get,set,seged,posed,segedIndex,newName.combineSegs,position,false);


              if(mergeValue >= splitValue || IsLowFreqWord(get,set,segedIndex,seged,posed) == true)
              {// 置換

                /*
                 * 原先「林志玲」如果「志玲」被偵測為人名，則「林志玲」不會被合成新的人名，為解決此問題而採用底下程式碼
                 * */
                if(newName.seged[segedIndex].Length == 2)
                {

                  if(get.nameCache.Contains(newName.seged[segedIndex]) == false) { get.nameCache.Add(newName.seged[segedIndex]); }



                  if(namePrefix.ContainsKey(newName.seged[segedIndex]) == false)
                  {
                    if(segedIndex > 0 && get.IsWord(set,newName.seged[segedIndex - 1].Last().ToString()) == true)
                    {

                      namePrefix.Add(newName.seged[segedIndex],newName.seged[segedIndex - 1].Last().ToString());
                      string name = newName.seged[segedIndex - 1].Last().ToString() + newName.seged[segedIndex];
                      if(ChinesePossibleNames.ContainsKey(name) == false) { ChinesePossibleNames.Add(name,1); } else { ChinesePossibleNames[name]++; }

                    }

                  }
                  else
                  {
                    if(segedIndex > 0 && get.IsWord(set,newName.seged[segedIndex - 1].Last().ToString()) == true)
                    {
                      if(ChinesePossibleNames.ContainsKey(newName.seged[segedIndex]) == false)
                      {
                        if(namePrefix[newName.seged[segedIndex]] != newName.seged[segedIndex - 1].Last().ToString())
                        {
                          ChinesePossibleNames.Remove(namePrefix[newName.seged[segedIndex]] + newName.seged[segedIndex]);
                          ChinesePossibleNames.Add(newName.seged[segedIndex],1);
                        }

                      }
                      else
                      { ChinesePossibleNames[newName.seged[segedIndex]]++; }
                    }

                  }

                  //後詞
                  if(nameSuffix.ContainsKey(newName.seged[segedIndex]) == false)
                  {
                    if(segedIndex + 1 < newName.seged.Count && get.IsWord(set,newName.seged[segedIndex + 1].First().ToString()) == true)
                    {
                      nameSuffix.Add(newName.seged[segedIndex],newName.seged[segedIndex + 1].First().ToString());
                      string name = newName.seged[segedIndex] + newName.seged[segedIndex + 1].First().ToString();
                      if(ChinesePossibleNames.ContainsKey(name) == false) { ChinesePossibleNames.Add(name,1); } else { ChinesePossibleNames[name]++; }

                    }

                  }
                  else
                  {
                    if(segedIndex + 1 < newName.seged.Count && get.IsWord(set,newName.seged[segedIndex + 1].First().ToString()) == true)
                    {
                      if(ChinesePossibleNames.ContainsKey(newName.seged[segedIndex]) == false)
                      {
                        if(nameSuffix[newName.seged[segedIndex]] != newName.seged[segedIndex + 1].First().ToString())
                        {
                          ChinesePossibleNames.Remove(newName.seged[segedIndex] + nameSuffix[newName.seged[segedIndex]]);
                          ChinesePossibleNames.Add(newName.seged[segedIndex],1);
                        }
                      }
                      else
                      { ChinesePossibleNames[newName.seged[segedIndex]]++; }

                    }
                  }

                }
                else
                {
                  if(get.nameCache.Contains(newName.seged[segedIndex]) == false) { get.nameCache.Add(newName.seged[segedIndex]); }
                }

                handled = true;
              }
            }
          }

        }
      }

      return handled;
    }
    #endregion
    
    #region 是否為名子
    /// <summary>
    /// 有沒有可能為中文人名
    /// </summary>
    /// <param name="WordIndex"></param>
    /// <param name="seged"></param>
    /// <param name="posed"></param>
    /// <param name="mixedSegmentName">是否為斷在一起的名字</param>
    /// <returns></returns>
    static bool CouldBeAName(Get get,Set set,int WordIndex,List<string> seged,List<string> posed,ref bool mixedSegmentName)
    {
      var Chinese百家姓Ptr = set.SimplifiedChinese == false ? get.Chinese百家姓Tw : get.Chinese百家姓Cn;
      var ChineseName_1Ptr = set.SimplifiedChinese == false ? get.ChineseName_1Tw : get.ChineseName_1Cn;
      var ChineseName_2Ptr = set.SimplifiedChinese == false ? get.ChineseName_2Tw : get.ChineseName_2Cn;
      if(Chinese百家姓Ptr.ContainsKey(seged[WordIndex]) == true)
      {
        mixedSegmentName = false;
        if(Chinese百家姓Ptr[seged[WordIndex]] <= 800) { return false; }//可讀性

        if (WordIndex - 1 >= 0)
        {
          if(WordIndex + 1 < seged.Count && seged[WordIndex - 1] == seged[WordIndex + 1]) { return false; }//陳陳元 return false
          if(posed[WordIndex - 1] == "Nes" && posed[WordIndex] == "Nf") { return false; }//帶指定詞，量詞 return false
          if(posed[WordIndex - 1] == "Neu") { return false; }//數字 return false
          if(seged[WordIndex - 1] == "之" && (seged[WordIndex] == "王" || seged[WordIndex] == "林")) { return false; }
          if(get.Co_occSinicaWordPairFre(seged[WordIndex - 1],seged[WordIndex]) > 50) { return false; }//為何兩字共同出現次數大於50就要return false 因為這邊主要判斷姓氏?
        }

        if(WordIndex + 1 < seged.Count)
        {
          if(posed[WordIndex] == "Nf" && posed[WordIndex + 1] == "Neu") { return false; }
          if(posed[WordIndex] == "D" && posed[WordIndex + 1] == "D") { return false; }//副詞 return false
          if(seged[WordIndex] == "曾" && seged[WordIndex + 1].Length > 1 && get.PoSTotalFreqAtWord(set,seged[WordIndex + 1]) > 50) { return false; }
          if(get.Co_occSinicaWordPairFre(seged[WordIndex],seged[WordIndex + 1]) > 5) { return false; }

        }
        return true;
      }
      else
      {//詞組不為百家姓

        if(seged[WordIndex].Length == 2)//兩字名字被斷為一組的
        {
          string name1 = seged[WordIndex].First().ToString();
          string name2 = seged[WordIndex].Last().ToString();
          if(IsLargerThan(Chinese百家姓Ptr,name1,800) && (IsLargerThan(ChineseName_1Ptr,name2,100) || IsLargerThan(ChineseName_2Ptr,name2,100))
            && (posed[WordIndex] == "Nb" || (posed[WordIndex] == "Na" && get.PoSTotalFreqAtWord(set,seged[WordIndex]) < 20) || (posed[WordIndex] == "Nc" && get.PoSTotalFreqAtWord(set,seged[WordIndex]) < 30)))
          {
            mixedSegmentName = true;
            return true;
          }
        }
        mixedSegmentName = false;
        return false;
      }

    }
    #endregion 

    /// <summary>
    /// 取得名字
    /// </summary>
    /// <param name="idx"></param>
    /// <param name="seged"></param>
    /// <param name="posed"></param>
    /// <param name="mixedSegmentName"></param>
    /// <returns></returns>
    static string GetName(Get get,Set set,int idx,List<string> seged,List<string> posed,bool mixedSegmentName)
    {
      int combineSegs = 0;//需組合多少字元
      string name = null;
      bool isTwoSegName = false;

      if(CN_TwoWord(get,set,idx,seged,posed) == true)
      {//二組斷詞組合的中文人名(不一定為兩字，可能為兩組，例如："藍天上"->"藍"+"天上")
        name = seged[idx] + seged[idx + 1];
        isTwoSegName = true;
        combineSegs = 2;
      }
      //Warning
      //!要確定seged在此期間不會被修改，否則會造成後面mixedSegmentName不準確
      if(mixedSegmentName == false && CN_ThreeWord(get,set,idx,seged,posed,isTwoSegName) == true)
      {//三字中文人名
        name = seged[idx] + seged[idx + 1] + seged[idx + 2];
        combineSegs = 3;
      }

      newName.Clear();
      if(name == null || FinalCheck(get,set,name,combineSegs,mixedSegmentName,idx,seged,posed) == false)
      {
        return null;
      }
      else
      {
        newName = GetNewNameSegment(idx,seged,posed,combineSegs,2,3);
        return name;
      }
    }
    /// <summary>
    /// 依條件判斷是否為兩字/組中文人名
    /// </summary>
    /// <param name="idx"></param>
    /// <param name="seged"></param>
    /// <param name="posed"></param>
    /// <returns></returns>
    static bool CN_TwoWord(Get get,Set set,int idx,List<string> seged,List<string> posed)
    {
      var ChineseName_1Ptr = set.SimplifiedChinese == false ? get.ChineseName_1Tw : get.ChineseName_1Cn;
      var ChineseName_2Ptr = set.SimplifiedChinese == false ? get.ChineseName_2Tw : get.ChineseName_2Cn;

      //出現百家姓,後有單字(依詞頻設定)無論詞性(因無從判定),極可能為名
      if(idx + 1 >= seged.Count) { return false; }
      string nextSeg = seged[idx + 1], nextPos = posed[idx + 1];
      if(Is_notword_integer(nextSeg,nextPos) == true) { return false; }

      if(idx + 1 < seged.Count)//判斷"二字組"(名可能被斷成"詞")的華人姓名合併
      {

        if(nextSeg.Length == 1)//判斷二字的華人姓名合併
        {
          if(get.PoSTotalFreqAtWord(set,nextSeg) <= 45)
          {
            if(IsLargerThan(ChineseName_1Ptr,nextSeg,100) == true) { return true; }
          }
          else if(seged[idx] == "盛" && get.PoSTotalFreqAtWord(set,nextSeg) >= 500)//"盛"補丁
          {
            return false;
          }
          else if(nextPos.IndexOf("N") == 0
                  && (IsLargerThan(ChineseName_1Ptr,nextSeg.First(),500) && IsLargerThan(ChineseName_2Ptr,nextSeg.Last(),500))
                  && IsSpecialPoSCase(get,set,idx + 1,seged,posed) == false)//百家姓後出現單一名詞
          { return true; }
        }
        else if(nextSeg.Length == 2)//判斷"二字組"的華人姓名合併,名為"詞"
        {
          //在二字人名中判斷三字人名，暫時拿掉

          if(get.PoSTotalFreqAtWord(set,nextSeg) <= 45
            && IsLargerThan(ChineseName_1Ptr,nextSeg.First(),100) == true && IsLargerThan(ChineseName_2Ptr,nextSeg.Last(),100))
          { return true; }

          if((nextPos == "Na" || nextPos == "Nb" || nextPos == "Nc")
            && IsLargerThan(ChineseName_1Ptr,nextSeg.First(),100) && IsLargerThan(ChineseName_2Ptr,nextSeg.Last(),100)
            && get.Co_occSinicaWordTotalFre(nextSeg) < 210 && get.PoSTotalFreqAtWord(set,nextSeg) < 100)
          { return true; }

        }
      }

      if(idx + 2 < seged.Count)//考慮前後詞性
      {
        if(nextSeg.Length == 1 && IsSpecialPoSCase(get,set,idx + 1,seged,posed) == false)
        {
          if(get.PoSTotalFreqAtWord(set,seged[idx + 2]) > 50 && posed[idx + 2].First() != 'N'
            && (IsLargerThan(ChineseName_1Ptr,nextSeg,500) && IsLargerThan(ChineseName_2Ptr,nextSeg,500)))
          { return true; }

        }
        else if(nextSeg.Length == 2)//判斷"二字組"的華人姓名合併,名為"詞"
        {
          //在二字人名中判斷三字人名，暫時拿掉

          if(posed[idx + 2].First() != 'N' && get.PoSTotalFreqAtWord(set,seged[idx + 2]) > 50 && IsSpecialPoSCase(get,set,idx + 1,seged,posed) == false
            && IsLargerThan(ChineseName_1Ptr,nextSeg.First(),100) && IsLargerThan(ChineseName_2Ptr,nextSeg.Last(),100)
            && get.Co_occSinicaWordTotalFre(nextSeg) < 210)
          { return true; }
        }

        if(posed[idx + 2] == "notword" && nextSeg.Length == 1 && IsSpecialPoSCase(get,set,idx + 1,seged,posed) == false
          && IsLargerThan(ChineseName_1Ptr,nextSeg.First(),500) && IsLargerThan(ChineseName_2Ptr,nextSeg.Last(),500))
        { return true; }
      }

      return false;
    }
    /// <summary>
    /// 依條件判斷是否為三字中文人名
    /// </summary>
    /// <param name="idx"></param>
    /// <param name="seged"></param>
    /// <param name="posed"></param>
    /// <param name="isTwoSegName"></param>
    /// <returns></returns>
    static bool CN_ThreeWord(Get get,Set set,int idx,List<string> seged,List<string> posed,bool isTwoSegName)
    {
      var Chinese百家姓Ptr = set.SimplifiedChinese == false ? get.Chinese百家姓Tw : get.Chinese百家姓Cn;
      var ChineseName_1Ptr = set.SimplifiedChinese == false ? get.ChineseName_1Tw : get.ChineseName_1Cn;
      var ChineseName_2Ptr = set.SimplifiedChinese == false ? get.ChineseName_2Tw : get.ChineseName_2Cn;

      if(idx + 2 >= seged.Count) { return false; }
      string nextSeg = seged[idx + 1], nextPos = posed[idx + 1];//後一組
      string nextSecondSeg = seged[idx + 2], nextSecondPos = posed[idx + 2];//後兩組中的第二組
      if(Is_notword_integer(nextSecondSeg,nextSecondPos) == true) { return false; }

      if(idx + 2 < seged.Count)//判斷三字的華人姓名合併
      {
        if(isTwoSegName == true && nextSeg.Length == 1 && nextSecondSeg.Length == 1)//若可得到前2字為可能的人名,後面再出現1個單字
        {
          if(get.PoSTotalFreqAtWord(set,nextSecondSeg) <= 45)
          { return true; }

          if(nextSecondPos.IndexOf("N") == 0
            && idx + 3 < seged.Count && get.PoSTotalFreqAtWord(set,seged[idx + 3]) > 50
            && get.PoSTotalFreqAtWord(set,nextSecondSeg) < 20000
            && IsSpecialPoSCase(get,set,idx + 2,seged,posed) == false)//二字名後出現單一名詞
          { return true; }

          if(IsLargerThan(ChineseName_2Ptr,nextSecondSeg,100)
            && get.PoSTotalFreqAtWord(set,nextSecondSeg) < 20000 && IsSpecialPoSCase(get,set,idx + 2,seged,posed) == false)
          { return true; }

        }
        if(nextSeg.Length == 1 && nextSecondSeg.Length == 1 && nextSecondSeg == "中"
          && (get.Co_occSinicaWordPairFre(nextSeg,nextSecondSeg) < 10)
          && IsLargerThan(ChineseName_1Ptr,nextSeg,100)
          && IsLargerThan(Chinese百家姓Ptr,seged[idx],14000)
          && Is_notword_integer(nextSeg,nextPos) == false)
        { return true; }

      }

      if(idx + 3 < seged.Count)
      {
        string nextThirdSeg = seged[idx + 3], nextThirdPos = posed[idx + 3];//後三組的第三組字詞與詞性
        if(isTwoSegName == true && nextSeg.Length == 1 && nextSecondSeg.Length == 1)
        {

          if(get.PoSTotalFreqAtWord(set,nextThirdSeg) > 50 && (nextThirdPos.First() != 'N' || nextThirdSeg.Length >= 2)
            && nextSecondPos.First() != 'D' && nextSecondPos != "VA" && nextSecondPos != "VB" && nextSecondPos != "VC"
            && IsLargerThan(ChineseName_2Ptr,nextSecondSeg,100)
            && get.PoSTotalFreqAtWord(set,nextSecondSeg) < 20000
            && IsSpecialPoSCase(get,set,idx + 2,seged,posed) == false)
          { return true; }
          else if(get.PoSTotalFreqAtWord(set,nextThirdSeg) < 50)
          {
            if(nextThirdSeg.Length >= 2 && get.PoSTotalFreqAtWord(set,nextSecondSeg) < 20000
              && nextSecondPos.First() != 'D' && nextSecondPos != "VA" && nextSecondPos != "VB" && nextSecondPos != "VC"
              && IsLargerThan(ChineseName_2Ptr,nextSecondSeg,100) && IsSpecialPoSCase(get,set,idx + 2,seged,posed) == false)
            { return true; }

            if(nextThirdSeg.Length == 1
              && nextSecondPos.First() != 'D' && nextSecondPos != "VA" && nextSecondPos != "VB" && nextSecondPos != "VC"
              && IsLargerThan(ChineseName_2Ptr,nextSecondSeg,100)
              && get.PoSTotalFreqAtWord(set,nextSecondSeg) < 20000
              && IsSpecialPoSCase(get,set,idx + 2,seged,posed) == false)
            { return true; }
          }

          if(nextThirdPos == "notword"
            && nextSecondPos != "VA" && nextSecondPos != "VB" && nextSecondPos != "VC"
            && IsLargerThan(ChineseName_2Ptr,nextSecondSeg,100))
          { return true; }
        }




        if(nextSeg.Length == 1 && nextSecondSeg.Length == 1 && Is_notword_integer(nextSeg,nextPos) == false)//二字無法為true情況
        {
          if(get.PoSTotalFreqAtWord(set,nextThirdSeg) > 50 && nextThirdPos.First() != 'N'
            && IsLargerThan(ChineseName_1Ptr,nextSeg,100) && IsLargerThan(ChineseName_2Ptr,nextSecondSeg,100)
            && get.PoSTotalFreqAtWord(set,nextSecondSeg) < 20000 && IsSpecialPoSCase(get,set,idx + 2,seged,posed) == false)
          { return true; }

          if(get.PoSTotalFreqAtWord(set,nextSecondSeg) <= 20
            && IsLargerThan(ChineseName_1Ptr,nextSeg,100) && IsLargerThan(ChineseName_2Ptr,nextSecondSeg,100))
          { return true; }
          if(nextThirdPos == "notword" && get.PoSTotalFreqAtWord(set,nextSecondSeg) < 20000
            && IsLargerThan(ChineseName_1Ptr,nextSeg,100) && IsLargerThan(ChineseName_2Ptr,nextSecondSeg,100))
          { return true; }
        }
      }


      return false;
    }
    /// <summary>
    /// 檢查詞性是否為特殊情況
    /// </summary>
    /// <param name="idx"></param>
    /// <param name="seged"></param>
    /// <param name="posed"></param>
    /// <returns></returns>
    static bool IsSpecialPoSCase(Get get,Set set,int idx,List<string> seged,List<string> posed)
    {
      //例如: 曾在
      if(idx + 1 < seged.Count)
      {
        if(posed[idx] == "D" && posed[idx + 1] == "P") { return true; }
        else if(posed[idx] == "D" && posed[idx + 1] == "D") { return true; }
        else if(posed[idx] == "Nf" && posed[idx + 1] == "Neu") { return true; }
        else if(get.Co_occSinicaWordPairFre(seged[idx],seged[idx + 1]) > 25) { return true; }
      }
      return false;
    }
    /// <summary>
    /// 最後進行條件式確認(補丁可以在這裡加入)
    /// </summary>
    /// <param name="name"></param>
    /// <param name="combineSegs"></param>
    /// <param name="mixedSegmentName"></param>
    /// <param name="idx"></param>
    /// <param name="seged"></param>
    /// <param name="posed"></param>
    /// <returns></returns>
    static bool FinalCheck(Get get,Set set,string name,int combineSegs,bool mixedSegmentName,int idx,List<string> seged,List<string> posed)
    {
      var Chinese百家姓Ptr = set.SimplifiedChinese == false ? get.Chinese百家姓Tw : get.Chinese百家姓Cn;

      if(name == null) { return false; }
      string firstWord = name.First().ToString(), lastWord = name.Last().ToString();

      if(firstWord == lastWord || Chinese百家姓Ptr.Keys.Contains(firstWord) == false) { return false; }

      if((firstWord == "季" || firstWord == "向") && get.PoSTotalFreqAtWord(set,name.Remove(0,1)) > 50) { return false; }

      if((idx + 2 < seged.Count && seged[idx + 1].Length == 1 && seged[idx + 2] != "中") && get.PoSTotalFreqAtWord(set,lastWord) > 20000)
      { return false; }

      if(name.Length == 2)
      {
        if(idx + 1 < seged.Count && posed[idx].First() == 'D' && (posed[idx + 1] == "VA" || posed[idx + 1] == "VB" || posed[idx + 1] == "VC"))
        { return false; }
        if(idx + 2 < seged.Count && posed[idx + 1].First() == 'D' && (posed[idx + 2] == "VA" || posed[idx + 2] == "VB" || posed[idx + 2] == "VC"))
        { return false; }
        if(get.Co_occSinicaWordTotalFre(name) < get.Co_occSinicaWordPairFre(firstWord,lastWord))
        { return false; }
      }

      if(idx + 1 < seged.Count && seged[idx + 1] == lastWord && posed[idx] == "Na" && seged[idx + 1].Length == 1 && (posed[idx + 1] == "Ng" || posed[idx + 1] == "Nd"))
      { return false; }


      if(mixedSegmentName == true)//兩字開頭+後詞為2字會出現問題→對應時出現PairFre(ABC,D)，而當初條件只有AB
      {
        string separate_LastName = name.Remove(name.Length - 1,1);
        string separate_FirstName = lastWord;

        if(name.Length > 3) { return false; }

        if(get.Co_occSinicaWordTotalFre(name) < get.Co_occSinicaWordPairFre(separate_LastName,separate_FirstName)) { return false; }
      }
      //補丁式修正
      //全國、全長、全縣等"全"開頭為兩字排除
      if(idx + 1 < seged.Count && name.Length == 2 && seged[idx] == "全") { return false; }

      //最後進行平衡語料單字詞篩選
      //為減少資料量，統計資料只含800次以上的姓，但如果修改門檻值800，需要重新統計資料

      if(name.Length == 2)
      {
        if(IsLargerThan(Chinese百家姓Ptr,name,800) == false && get.Co_occSinicaWordPairFre(name[0].ToString(),name[1].ToString()) >= 1) { return false; }
      }
      else if(name.Length == 3)
      {
        if(IsLargerThan(Chinese百家姓Ptr,name.Substring(0,2),800) == true)//為二字姓
        {
          if(get.Co_occSinicaWordPairFre(name.Substring(0,2),name.Substring(2,1)) >= 1) { return false; }
        }
        else
        {
          if(combineSegs == 2)
          {
            if(get.Co_occSinicaWordPairFre(name[0].ToString(),name.Substring(1,2)) >= 1) { return false; }
          }
          else if(combineSegs == 3)
          {
            if(get.Co_occSinicaWordPairFre(name[0].ToString(),name.Substring(1,1)) >= 1) { return false; }
          }
          else
          {
            throw new System.IO.InvalidDataException($"");
          }
        }
      }
      else if(name.Length == 4)
      {
        if(combineSegs == 2)
        {
          if(get.Co_occSinicaWordPairFre(name.Substring(0,2),name.Substring(2,2)) >= 1) { return false; }
        }
        else if(combineSegs == 3)
        {
          if(get.Co_occSinicaWordPairFre(name.Substring(0,2),name.Substring(2,1)) >= 1) { return false; }
        }
        else
        {
          throw new System.IO.InvalidDataException($"");
        }
      }
      else
      { throw new System.IO.InvalidDataException($""); }

      //常用字詞過濾



      if(name.Length == 3)
      {
        if(combineSegs == 2)
        {
          if(get.PoSTotalFreqAtWord(set,name.Substring(0,2)) > 25)
          { return false; }


          if(get.PoSTotalFreqAtWord(set,name.Substring(1,2)) > 25)
          { return false; }


        }

      }

      //連...都，連狗都會、連貓都會...等
      if(name.Length >= 1 && name[0] == '連')
      {
        if(idx + combineSegs < seged.Count && seged[idx + combineSegs] == "都")
        {
          return false;
        }
        else if(idx + combineSegs + 1 < seged.Count && seged[idx + combineSegs + 1] == "都")
        {
          return false;
        }
        else if(idx + combineSegs + 2 < seged.Count && seged[idx + combineSegs + 2] == "都")
        {
          return false;
        }
      }

      //??云加冒號
      if(name.Length >= 3 && idx + combineSegs < seged.Count && name.Last() == '云')
      {
        string word = seged[idx + combineSegs];
        if(word == "：" || word == ":" || word == "，" || word == "\"" || word == "," || word == "'" || word == "「" || word == "『" || word == "“" || word == "‘")
        {
          return false;
        }



      }

      return true;
    }
    /// <summary>
    /// 是否為極低詞頻
    /// </summary>
    /// <param name="idx"></param>
    /// <param name="seged"></param>
    /// <param name="posed"></param>
    /// <returns></returns>
    static bool IsLowFreqWord(Get get,Set set,int idx,List<string> seged,List<string> posed)
    => idx + 2 < seged.Count && (get.PoSTotalFreqAtWord(set,seged[idx + 1]) <= 1.0 && get.PoSTotalFreqAtWord(set,seged[idx + 2]) <= 1.0);

  }
}
