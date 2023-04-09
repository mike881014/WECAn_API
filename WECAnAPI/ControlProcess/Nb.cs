using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WECAnAPI
{
  partial class Nb
  {



    #region 更新名稱Clear
    struct _NewName
    {
      public List<string> seged;
      public List<string> posed;
      public int combineSegs;
      public void Clear()
      {
        seged = null;
        posed = null;
        combineSegs = 0;
      }
    }
    static _NewName newName;
    #endregion
    
    #region (列舉)詞的狀態判斷
    /// <summary>
    /// 取得詞的位置
    /// </summary>
    enum Position
    {
      /// <summary>
      /// 開頭詞
      /// </summary>
      Begin,
      /// <summary>
      /// 結尾詞
      /// </summary>
      End,
      /// <summary>
      /// 中間詞
      /// </summary>
      Middle,
      /// <summary>
      /// 獨立詞
      /// </summary>
      Single,
      /// <summary>
      /// 未設定
      /// </summary>
      NotInitialize
    };
    #endregion

    #region Nb起始介面
    public static void HandlePaper(Get get,Set set,List<List<string>> segedSet,List<List<string>> posedSet,List<string> sentenceSet,List<List<List<string>>> sourcesSet)
    {


      if(set.SimplifiedChinese == false)
      {
        HandleTw(get,set,segedSet,posedSet,sentenceSet,sourcesSet);
      }
      else
      {
        HandleCn(get,set,segedSet,posedSet,sentenceSet,sourcesSet);
      }
    }
    #endregion


    #region HandleTw
    static void HandleTw(Get get,Set set,List<List<string>> segedSet,List<List<string>> posedSet,List<string> sentenceSet,List<List<List<string>>> sourcesSet)
    {
      //pass1 收集所有可能的人名
      if(set.ChineseName == true)
      {
        namePrefix = new Dictionary<string,string>();
        nameSuffix = new Dictionary<string,string>();
        ChinesePossibleNames = new Dictionary<string,int>();

        for(int lineIndex = 0; lineIndex < segedSet.Count(); lineIndex++)
        {
          if(get.nameCache.Count > 1000) { break; }//防止人名爆掉
          ChineseHandle(get,set,segedSet[lineIndex],posedSet[lineIndex]);
        }
        //把潛在人名加入姓名快取
        foreach(var name in ChinesePossibleNames)//潛在人名門檻直設置為1
        {
          if(get.nameCache.Count > 1000) { break; }
          if((name.Key.Length == 2 || name.Value > 1) && get.nameCache.Contains(name.Key) == false)
          {
            get.nameCache.Add(name.Key);
          }
        }
      }
      #endregion

      #region 日文
      //if(set.JapaneseName == true)
      //{
      //  for(int lineIndex = 0; lineIndex < segedSet.Count(); lineIndex++)
      //  {
      //    if(get.nameCache.Count > 1000) { break; }//防止人名爆掉

      //    JapaneseHandle(get,set,segedSet[lineIndex],posedSet[lineIndex]);

      //  }
      //}
      #endregion

      #region 外國

      //if(set.ForeignName == true)
      //{
      //  for(int lineIndex = 0; lineIndex < segedSet.Count(); lineIndex++)
      //  {
      //    if(get.nameCache.Count > 1000) { break; }//防止人名爆掉

      //    ForeignHandle(get,set,segedSet[lineIndex],posedSet[lineIndex]);

      //  }
      //}
      #endregion
    }


    static void HandleCn(Get get,Set set,List<List<string>> segedSet,List<List<string>> posedSet,List<string> sentenceSet,List<List<List<string>>> sourcesSet)
    {
      //簡體中文由繁體直接轉換而來
      #region 中文
      //pass1 收集所有可能的人名
      if(set.ChineseName == true)
      {
        namePrefix = new Dictionary<string,string>();
        nameSuffix = new Dictionary<string,string>();
        ChinesePossibleNames = new Dictionary<string,int>();

        for(int lineIndex = 0; lineIndex < segedSet.Count(); lineIndex++)
        {
          if(get.nameCache.Count > 1000) { break; }//防止人名爆掉
          ChineseHandle(get,set,segedSet[lineIndex],posedSet[lineIndex]);
        }
        //把潛在人名加入姓名快取
        foreach(var name in ChinesePossibleNames)//潛在人名門檻直設置為1
        {
          if(get.nameCache.Count > 1000) { break; }
          if((name.Key.Length == 2 || name.Value > 1) && get.nameCache.Contains(name.Key) == false)
          {
            get.nameCache.Add(name.Key);
          }
        }
      }
      #endregion
    }

    #region 其他程式碼
    /// <summary>
    /// 檢查是否已有該姓名存在記憶體
    /// </summary>
    /// <param name="startIndex"></param>
    /// <param name="seged"></param>
    /// <param name="posed"></param>
    /// <param name="lowerBound">姓名組數下界</param>
    /// <param name="upperBound">姓名組數上屆</param>
    /// <returns></returns>
    static bool IsInNameCache(Get get,Set set,int startIndex,List<string> seged,List<string> posed,int lowerBound,int upperBound)
    {
      if(lowerBound > 1 && upperBound >= lowerBound)
      {
        for(int idx = upperBound - 1; idx >= lowerBound - 1 && (startIndex + idx) < seged.Count; idx--)
        {
          string tmpName = null;
          for(int shiftIdx = 0; shiftIdx < idx; shiftIdx++)
          {
            tmpName += seged[startIndex + shiftIdx];
          }
          if(get.nameCache.Contains(tmpName) == true) { return true; }
        }
      }


      return false;


    }



    /// <summary>
    /// 取得新人名組合
    /// </summary>
    /// <param name="beginIdx"></param>
    /// <param name="seged"></param>
    /// <param name="posed"></param>
    /// <param name="combineSegs">人名的原句斷詞詞組數</param>
    /// <param name="lowerBound">combineSegs下界</param>
    /// <param name="upperBound">combineSegs上屆</param>
    /// <returns></returns>
    static _NewName GetNewNameSegment(int beginIdx,List<string> seged,List<string> posed,int combineSegs,int lowerBound,int upperBound)
    {
      if(combineSegs > 1 && lowerBound > 1 && upperBound >= lowerBound && combineSegs >= lowerBound && combineSegs <= upperBound)
      {
        List<string> NewNameSeged = new List<string>(seged);
        List<string> NewNamePosed = new List<string>(posed);

        string tmpName = null;
        if((beginIdx + combineSegs - 1) < seged.Count)
        {
          for(int shiftIdx = 0; shiftIdx < combineSegs; shiftIdx++)
          {
            tmpName += NewNameSeged[beginIdx + shiftIdx];
          }
        }


        NewNameSeged[beginIdx] = tmpName;
        NewNameSeged.RemoveRange(beginIdx + 1,combineSegs - 1);
        NewNamePosed.RemoveRange(beginIdx,combineSegs);
        NewNamePosed.Insert(beginIdx,"Nb");//給予Nb的詞性

        newName.seged = NewNameSeged;
        newName.posed = NewNamePosed;
        newName.combineSegs = combineSegs;
      }
      else
      {
        newName.Clear();
      }
      return newName;
    }
    /// <summary>
    /// 取得詞組在句子中的位置
    /// </summary>
    /// <param name="idx"></param>
    /// <param name="seged"></param>
    /// <param name="posed"></param>
    /// <returns></returns>
    static Position GetPosition(int idx,List<string> seged,List<string> posed)
    {
      Position position = Position.Middle;
      if(idx == 0)//句首
      {
        if(idx + 1 < seged.Count && posed[idx + 1] == "notword" || idx + 1 == seged.Count)
          position = Position.Single;//是開頭後面非中文或結尾，獨立詞
        else
          position = Position.Begin; //其他情況為開頭詞
      }
      if(idx > 0)//句中
      {
        if(posed[idx - 1] == "notword")//前詞不為中文
        {
          if(idx + 1 < seged.Count && posed[idx + 1] == "notword" || idx + 1 == seged.Count)
            position = Position.Single;//前後為非中文，後面為非中文或結尾，獨立詞
          else
            position = Position.Begin;//其他情況為開頭詞
        }
        else if(posed[idx - 1] != "notword")//前詞為中文
        {
          if(idx + 1 < seged.Count && posed[idx + 1] == "notword" || idx + 1 == seged.Count)
            position = Position.End;//前面為中文，後面非中文或本詞為結尾，結尾詞
          else
            position = Position.Middle;//其他情況為中間詞
        }
      }
      return position;
    }


    /// <summary>
    /// 取得機率分數
    /// </summary>
    /// <param name="seged"></param>
    /// <param name="posed"></param>
    /// <param name="startIndex"></param>
    /// <param name="wordCount"></param>
    /// <param name="position"></param>
    /// <param name="isNb"></param>
    /// <returns></returns>
    static double GetProbability(Get get,Set set,List<string> seged,List<string> posed,int startIndex,int wordCount,Position position,bool isNb)
    {
      int shift = wordCount - 1;// 加上shift，索引至姓名組合中的結尾。 
      //例如: TTTXXXTTT，X為名字，startIndex為第一個X，最後一個x為startIndex+shift

      if(position == Position.Middle)
      {// 句中
        double forwardConditionalProbability = (PoSTw.PairFreq(posed[startIndex - 1],posed[startIndex]) + 0.0001) / PoSTw.TotalFreq(posed[startIndex]) / PoSTw.TotalFreq(posed[startIndex]);
        double backwardConditionalProbability = (PoSTw.PairFreq(posed[startIndex + shift],posed[startIndex + shift + 1]) + 0.0001) / PoSTw.TotalFreq(posed[startIndex + shift]) / PoSTw.TotalFreq(posed[startIndex + shift]);
        double forwardMarginalProbability = (isNb) ? (1) : ((get.PoSFreqAtWord(set,seged[startIndex],posed[startIndex]) + 0.0001) / get.PoSTotalFreqAtWord(set,seged[startIndex]));
        double backwardMarginalProbability = (get.PoSFreqAtWord(set,seged[startIndex + shift + 1],posed[startIndex + shift + 1]) + 0.0001);// / get.PoSTotalFreqAtWord(seged[lastNameIndex + Shift + 1]);

        double score =
            forwardConditionalProbability
            * backwardConditionalProbability
            * Math.Sqrt(forwardMarginalProbability)
            * Math.Sqrt(backwardMarginalProbability);

        return score;
      }
      else if(position == Position.Begin)
      {// 句首
        double forwardConditionalProbability = (PoSTw.BeginFreq(posed[startIndex]) + 0.0001) / PoSTw.TotalFreq(posed[startIndex]) / PoSTw.TotalFreq(posed[startIndex]);
        double backwardConditionalProbability = (PoSTw.PairFreq(posed[startIndex + shift],posed[startIndex + shift + 1]) + 0.0001) / PoSTw.TotalFreq(posed[startIndex + shift]) / PoSTw.TotalFreq(posed[startIndex + shift]);
        double forwardMarginalProbability = (isNb) ? (1) : ((get.PoSFreqAtWord(set,seged[startIndex],posed[startIndex]) + 0.0001) / get.PoSTotalFreqAtWord(set,seged[startIndex]));
        double backwardMarginalProbability = (get.PoSFreqAtWord(set,seged[startIndex + shift + 1],posed[startIndex + shift + 1]) + 0.0001);// / get.PoSTotalFreqAtWord(seged[lastNameIndex + Shift + 1]);

        double score =
            forwardConditionalProbability
            * backwardConditionalProbability
            * Math.Sqrt(forwardMarginalProbability)
            * Math.Sqrt(backwardMarginalProbability);

        return score;
      }
      else if(position == Position.End)
      {// 句尾
        double forwardConditionalProbability = (PoSTw.PairFreq(posed[startIndex - 1],posed[startIndex]) + 0.0001) / PoSTw.TotalFreq(posed[startIndex]) / PoSTw.TotalFreq(posed[startIndex]);
        double backwardConditionalProbability = (PoSTw.EndFreq(posed[startIndex + shift]) + 0.0001) / PoSTw.TotalFreq(posed[startIndex + shift]) / PoSTw.TotalFreq(posed[startIndex + shift]);
        double forwardMarginalProbability = (isNb) ? (1) : (Math.Sqrt(
            (get.PoSFreqAtWord(set,seged[startIndex],posed[startIndex]) + 0.0001) / get.PoSTotalFreqAtWord(set,seged[startIndex])
          * (get.PoSFreqAtWord(set,seged[startIndex + shift],posed[startIndex + shift]) + 0.0001) / get.PoSTotalFreqAtWord(set,seged[startIndex + shift])));// 林嘉藤為了也考慮後面的名，一次看兩項用幾何平均數耦合
        double backwardMarginalProbability = 1;

        double score =
            forwardConditionalProbability
            * backwardConditionalProbability
            * Math.Sqrt(forwardMarginalProbability)
            * Math.Sqrt(backwardMarginalProbability);

        return score;
      }
      else if(position == Position.Single)
      {// 獨立詞
        double forwardConditionalProbability = 1;
        double backwardConditionalProbability = 1;
        double forwardMarginalProbability = (isNb) ? (1) : ((get.PoSFreqAtWord(set,seged[startIndex],posed[startIndex]) + 0.0001) / get.PoSTotalFreqAtWord(set,seged[startIndex]));
        double backwardMarginalProbability = 1;

        double score =
            forwardConditionalProbability
            * backwardConditionalProbability
            * Math.Sqrt(forwardMarginalProbability)
            * Math.Sqrt(backwardMarginalProbability);

        if(posed[startIndex].First() == 'N')
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


    /// <summary>
    /// 是否大於(不等於)門檻值
    /// </summary>
    /// <param name="source"></param>
    /// <param name="word"></param>
    /// <param name="threshold"></param>
    /// <returns></returns>
    static bool IsLargerThan(Dictionary<string,int> source,string word,int threshold) => source.ContainsKey(word) && source[word] > threshold;
    /// <summary>
    /// 是否大於(不等於)門檻值
    /// </summary>
    /// <param name="source"></param>
    /// <param name="ch"></param>
    /// <param name="threshold"></param>
    /// <returns></returns>
    static bool IsLargerThan(Dictionary<string,int> source,char ch,int threshold) => source.ContainsKey(ch.ToString()) && source[ch.ToString()] > threshold;

    /// <summary>
    /// 是否為notword或整數
    /// </summary>
    /// <param name="word"></param>
    /// <param name="pos"></param>
    /// <returns></returns>
    static bool Is_notword_integer(string word,string pos) => int.TryParse(word,out int tmp) || pos == "notword";





    #endregion

  }
}

