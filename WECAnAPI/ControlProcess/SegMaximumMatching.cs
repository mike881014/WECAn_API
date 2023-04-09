using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WECAnAPI
{
  class SegMaximumMatching
  {
    #region Handle處理介面Method
    public static void Handle(Get get,Set set,string sentence,List<string> seged)
    {
      //繁簡混和 強制轉繁簡
      if (set.MixTwCn == true) {
        if (set.SimplifiedChinese == true) {
          sentence = SimplifiedChinese.ToSimplifiedChinese(sentence);
        } else {
          sentence = SimplifiedChinese.ToTraditionalChinese(sentence);
        }
      }

      //最大長度優先斷詞法
      maximumMatching(get,set,sentence,seged);
      maximumMatchingSplit(get,set,seged);
    }
    #endregion

    #region 個+人，才+能
    static void maximumMatchingSplit(Get get,Set set,List<string> seged)
    {
      Dictionary<string,List<List<string>>> MaximumMatchingWordPtr = set.SimplifiedChinese == false ? get.MaximumMatchingWordTw : get.MaximumMatchingWordCn;

      for(int segedIndex = 0; segedIndex < seged.Count; segedIndex++)
      {
        if(MaximumMatchingWordPtr.ContainsKey(seged[segedIndex]) == true)
        {
          foreach(List<string> maximumMatchingError in MaximumMatchingWordPtr[seged[segedIndex]])
          {
            List<string> posed = new List<string>();
            ToPoS.Handle(get,set,seged,posed,false);
            List<string> regulateSeged = new List<string>(seged);
            List<string> regulatePosed = new List<string>();
            regulateSeged.RemoveAt(segedIndex);
            regulateSeged.InsertRange(segedIndex,maximumMatchingError);
            ToPoS.Handle(get,set,regulateSeged,regulatePosed,false);

            if((regulateSeged[segedIndex] == "個" || regulateSeged[segedIndex] == "个") && regulatePosed[segedIndex] == "Di" && regulateSeged[segedIndex + 1] == "人" && regulatePosed[segedIndex + 1] == "Na")
            {
              regulatePosed[segedIndex] = "Nf";
            }

            int position = Relative_Position(seged,posed,segedIndex);
            double mergeValue = 詞性分母平方O_字詞分母平方X(get,set,seged,posed,segedIndex,1,position);
            double splitValue = 詞性分母平方O_字詞分母平方X(get,set,regulateSeged,regulatePosed,segedIndex,maximumMatchingError.Count(),position);

            //在有正確數值下才得以進行比較(在統計資料沒有對應數值會不正確)，否則以總次數檢查。
            if(double.IsInfinity(mergeValue) == false && double.IsInfinity(splitValue) == false && mergeValue > 0 && splitValue > 0)
            {
              if(splitValue > mergeValue)
              {
                seged.RemoveAt(segedIndex);
                seged.InsertRange(segedIndex,maximumMatchingError);
                break;
              }
            }
            else
            {
              double splitProb = get.Co_occ單雙模糊PairFre(set,seged[segedIndex].First().ToString(),seged[segedIndex].Last().ToString());
              double mergeProb = get.Co_occ單雙模糊TotalFre(set,seged[segedIndex]);
              if(splitProb > 0 && mergeProb > 0 && splitProb > mergeProb)
              {
                seged.RemoveAt(segedIndex);
                seged.InsertRange(segedIndex,maximumMatchingError);
                break;
              }
            }
          }
        }
      }
    }
    #endregion

    #region 
    public static double 詞性分母平方O_字詞分母平方X(Get get,Set set,List<string> seged,List<string> posed,int startIndex,int wordCount,int position)
    {// 組合前分散字

     
      int shift = wordCount - 1;// +shift = 詞組最後一個

      if(position == 0)
      {// 句中
        double forwardConditionalProbability = PoSTw.PairFreq(posed[startIndex - 1],posed[startIndex])
            / PoSTw.TotalFreq(posed[startIndex]) / PoSTw.TotalFreq(posed[startIndex]);

        double backwardConditionalProbability = PoSTw.PairFreq(posed[startIndex + shift],posed[startIndex + shift + 1])
            / PoSTw.TotalFreq(posed[startIndex + shift]) / PoSTw.TotalFreq(posed[startIndex + shift]);

        double forwardMarginalProbability = get.PoSFreqAtWord(set,seged[startIndex],posed[startIndex])
            / get.PoSTotalFreqAtWord(set,seged[startIndex]);

        double backwardMarginalProbability = (get.PoSFreqAtWord(set,seged[startIndex + shift],posed[startIndex + shift]))
            / get.PoSTotalFreqAtWord(set,seged[startIndex + shift]);

        double forwardCo_occurrenceProbability = (get.Co_occ單雙模糊PairFre(set,seged[startIndex - 1],seged[startIndex]))
             / get.Co_occ單雙模糊TotalFre(set,seged[startIndex]);

        double backwardCo_occurrenceProbability = (get.Co_occ單雙模糊PairFre(set,seged[startIndex + shift],seged[startIndex + shift + 1]))
            / get.Co_occ單雙模糊TotalFre(set,seged[startIndex + shift]);

        double score =
            (forwardConditionalProbability + 0.0000000001)
            * (backwardConditionalProbability + 0.0000000001)
            * Math.Sqrt(forwardMarginalProbability + 0.0000000001)
            * Math.Sqrt(backwardMarginalProbability + 0.0000000001)
            * (forwardCo_occurrenceProbability + 0.0000000001)
            * (backwardCo_occurrenceProbability + 0.0000000001);
        
        return score;
      }
      else if(position == 1)
      {// 句首
        double forwardConditionalProbability = (PoSTw.BeginFreq(posed[startIndex]))
            / PoSTw.TotalFreq(posed[startIndex]) / PoSTw.TotalFreq(posed[startIndex]);

        double backwardConditionalProbability = (PoSTw.PairFreq(posed[startIndex + shift],posed[startIndex + shift + 1]))
            / PoSTw.TotalFreq(posed[startIndex + shift]) / PoSTw.TotalFreq(posed[startIndex + shift]);

        double forwardMarginalProbability = (get.PoSFreqAtWord(set,seged[startIndex],posed[startIndex]))
            / get.PoSTotalFreqAtWord(set,seged[startIndex]);

        double backwardMarginalProbability = (get.PoSFreqAtWord(set,seged[startIndex + shift],posed[startIndex + shift]))
            / get.PoSTotalFreqAtWord(set,seged[startIndex + shift]);

        double forwardCo_occurrenceProbability = (get.Co_occ單雙模糊BeginFre(set,seged[startIndex]))
             / get.Co_occ單雙模糊TotalFre(set,seged[startIndex]);

        double backwardCo_occurrenceProbability = (get.Co_occ單雙模糊PairFre(set,seged[startIndex + shift],seged[startIndex + shift + 1]))
             / get.Co_occ單雙模糊TotalFre(set,seged[startIndex + shift]);

        double score =
            (forwardConditionalProbability + 0.0000000001)
            * (backwardConditionalProbability + 0.0000000001)
            * Math.Sqrt(forwardMarginalProbability + 0.0000000001)
            * Math.Sqrt(backwardMarginalProbability + 0.0000000001)
            * (forwardCo_occurrenceProbability + 0.0000000001)
            * (backwardCo_occurrenceProbability + 0.0000000001);
        
        return score;
      }
      else if(position == 2)
      {// 句尾
        double forwardConditionalProbability = (PoSTw.PairFreq(posed[startIndex - 1],posed[startIndex]))
            / PoSTw.TotalFreq(posed[startIndex]) / PoSTw.TotalFreq(posed[startIndex]);

        double backwardConditionalProbability = (PoSTw.EndFreq(posed[startIndex + shift]))
            / PoSTw.TotalFreq(posed[startIndex + shift]) / PoSTw.TotalFreq(posed[startIndex + shift]);

        double forwardMarginalProbability = (get.PoSFreqAtWord(set,seged[startIndex],posed[startIndex]))
            / get.PoSTotalFreqAtWord(set,seged[startIndex]);

        double backwardMarginalProbability = (get.PoSFreqAtWord(set,seged[startIndex + shift],posed[startIndex + shift]))
            / get.PoSTotalFreqAtWord(set,seged[startIndex + shift]);

        double forwardCo_occurrenceProbability = (get.Co_occ單雙模糊PairFre(set,seged[startIndex - 1],seged[startIndex]))
           / get.Co_occ單雙模糊TotalFre(set,seged[startIndex]);

        double backwardCo_occurrenceProbability = (get.Co_occ單雙模糊EndFre(set,seged[startIndex + shift]))
            / get.Co_occ單雙模糊TotalFre(set,seged[startIndex + shift]);

        double score =
            (forwardConditionalProbability + 0.0000000001)
            * (backwardConditionalProbability + 0.0000000001)
            * Math.Sqrt(forwardMarginalProbability + 0.0000000001)
            * Math.Sqrt(backwardMarginalProbability + 0.0000000001)
            * (forwardCo_occurrenceProbability + 0.0000000001)
            * (backwardCo_occurrenceProbability + 0.0000000001);
        
        return score;
      }
      else if(position == 11)
      {// 獨立詞
        double forwardConditionalProbability = 1;

        double backwardConditionalProbability = 1;

        double forwardMarginalProbability = (get.PoSFreqAtWord(set,seged[startIndex],posed[startIndex]))
            / get.PoSTotalFreqAtWord(set,seged[startIndex]);

        double backwardMarginalProbability = (get.PoSFreqAtWord(set,seged[startIndex + shift],posed[startIndex + shift]))
            / get.PoSTotalFreqAtWord(set,seged[startIndex + shift]);

        double forwardCo_occurrenceProbability = 1;

        double backwardCo_occurrenceProbability = 1;

        double score =
            (forwardConditionalProbability + 0.0000000001)
            * (backwardConditionalProbability + 0.0000000001)
            * Math.Sqrt(forwardMarginalProbability + 0.0000000001)
            * Math.Sqrt(backwardMarginalProbability + 0.0000000001)
            * (forwardCo_occurrenceProbability + 0.0000000001)
            * (backwardCo_occurrenceProbability + 0.0000000001);

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
    #endregion

    #region 姓名分析
    public static int Relative_Position(List<string> seged,List<string> posed,int startIndex)
    {
      int i = startIndex;//百家姓起始值
      int position = 0;//0表示中間，1表示前面，2表示後面
                       //int FrontFlag = 0, MiddleFlag = 0, RearFlag = 0;/*指位旗標指向這個詞與前一個詞、後一個詞*/


      if(i == 0)//若"姓"出現在句首
      {
        if(i + 1 < seged.Count && posed[i + 1] == "notword") //下一個字為標點符號,或在句尾 => 獨立詞
          position = 11;
        else if(i + 1 == seged.Count)
          position = 11;
        else
          position = 1; // => 句首
      }
      if(i > 0)//"姓"在句中
      {
        if(posed[i - 1] == "notword")//若"姓"出現在表點符號之後 => 算句首 ,或在最後出現的結尾詞
        {
          if(i + 1 < seged.Count && posed[i + 1] == "notword")//下一個字為標點符號,或在句尾 => 獨立詞
            position = 11;
          else if(i + 1 == seged.Count)
            position = 11;
          else
            position = 1;// => 句首
        }
        else if(posed[i - 1] != "notword")//若"姓"前一個詞性不為標點符號,就只有可能是句中或是句尾
        {
          if(i + 1 < seged.Count && posed[i + 1] == "notword")
            position = 2;
          else if(i + 1 == seged.Count)
            position = 2;
          else
            position = 0;
        }
      }
      return position;
    }
    #endregion

    #region 合併英數，先檢查英文是否為半形，若為全型，則向前置放
    private static void 合併英數 (Set set, List<string> seged)
    {
      int start = -1, nowWord;
      for (nowWord = 0; nowWord < seged.Count; nowWord++) {
        bool allSemi = true; // 這個詞是否全由半形組成
        foreach (char c in seged[nowWord]) {
          if (c < 0x20 || c > 0x7e) { // 若在ASCII 32~126範圍外則視為全形
            allSemi = false;
            break;
          }
          if (set.SeperateInSpace == true && c == ' ') {
            allSemi = false;
            break;
          }
        }

        if (allSemi) {
          if (start == -1) {
            start = nowWord;
          }
        } else {
          if (start != -1) {
            int len = nowWord - start;
            seged.Insert(start, String.Join("", seged.GetRange(start, len)));
            seged.RemoveRange(start + 1, len);
            nowWord = nowWord - len + 1;
            start = -1;
          }
        }
      }
      
      if (start != -1) {
        int len = nowWord - start;
        seged.Insert(start, String.Join("", seged.GetRange(start, len)));
        seged.RemoveRange(start + 1, len);
        nowWord = nowWord - len + 1;
        start = -1;
      }
    }
    #endregion

    #region 強制合併，語料檔案，預設關
    private static void 強制合併 (Get get, Set set, string sentence, List<string> seged)
    {
      foreach (var userWord in get.ForceMergeWord) {
        int next = 0;
        while (true) {
          int start = sentence.IndexOf(userWord, next);
          int end = start + userWord.Length - 1;
          next = start + userWord.Length;
          if (start == -1) {
            break;
          }

          int readLen = 0, startID = 0, endID = 0, leftLen, rightLen;
          while (true) {
            readLen += seged[startID].Length;
            if (readLen >= start + 1) {
              break;
            }
            startID++;
          }

          if (readLen > end) {
            continue;
          }

          rightLen = readLen - start;
          leftLen = seged[startID].Length - rightLen;
          if (leftLen > 0) {
            seged.Insert(startID, seged[startID].Substring(0, leftLen));
            startID++;
            seged[startID] = seged[startID].Substring(leftLen);
          }

          endID = startID + 1;
          while (true) {
            readLen += seged[endID].Length;
            if (readLen >= end + 1) {
              break;
            }
            endID++;
          }

          rightLen = readLen - end - 1;
          leftLen = seged[endID].Length - rightLen;
          if (rightLen > 0) {
            seged.Insert(endID, seged[endID].Substring(0, leftLen));
            seged[endID + 1] = seged[endID + 1].Substring(leftLen);
          }

          seged[startID] = String.Join("", seged.GetRange(startID, endID - startID + 1));
          seged.RemoveRange(startID + 1, endID - startID);
        }
      }
    }
    #endregion

    #region 強制合併，語料檔案，預設關
    private static void 強制拆開 (Get get, Set set, string sentence, List<string> seged)
    {
      Dictionary<string, List<string>> wordList = get.ForceSplitWord;
      for (int i = 0; i < seged.Count; i++) {
        if (wordList.ContainsKey(seged[i])) {
          var list = wordList[seged[i]];
          seged.RemoveRange(i, 1);
          seged.InsertRange(i, list);
        }
      }
    }
        #endregion

    #region maximumMatching最大優先長詞斷詞法，由Handle直接呼叫，分成順斷與逆斷，順斷後先強制拆開(method)、強制合併(method)、合併英數(method)，逆斷相同，最後送入ToPoS.cs
    private static void maximumMatching(Get get,Set set,string sentence,List<string> seged)
    {
      seged.Clear();
      List<string> 順seged = new List<string>();
      List<string> 順posed = new List<string>();
      順斷(get,set,sentence,順seged);
      if (set.ForceSplitWord == true) {
        強制拆開(get, set, sentence, 順seged);
      }
      if (set.ForceMergeWord == true) {
        強制合併(get, set, sentence, 順seged);
      }
      if (set.ForceMergeNeuAndFW == true) {//為何需要先合併英數，而不放到後面再處理
        合併英數(set, 順seged);
      }
      ToPoS.Handle(get,set,順seged,順posed,false);
      List<string> 逆seged = new List<string>();
      List<string> 逆posed = new List<string>();
      逆斷(get,set,sentence,逆seged);
      if (set.ForceSplitWord == true) {
        強制拆開(get, set, sentence, 逆seged);
      }
      if (set.ForceMergeWord == true) {
        強制合併(get, set, sentence, 逆seged);
      }
      if (set.ForceMergeNeuAndFW == true) {
        合併英數(set, 逆seged);
      }
      ToPoS.Handle(get,set,逆seged,逆posed,false);

      List<CompareSegment.IndexGroup> segSubIndex = new List<CompareSegment.IndexGroup>();
      CompareSegment.CompareSegmentHandle(順seged,逆seged,segSubIndex);
      
      //上述CompareSegmentHandle，所存之值為順斷與逆斷之index(CompareSegment.IndexGroup)
      foreach(CompareSegment.IndexGroup item in segSubIndex)
      {//GroupAB中的值為目前這個字的INDEX紀錄，類似帳本功能，如有1筆以上，代表對齊過，代表正不等於順
        if(item.GroupA.Count() == 1 && item.GroupB.Count() == 1)//若一樣則隨便挑一個 這邊選順
        {
          foreach(int _index in item.GroupA)
          {
            seged.Add(順seged[_index]);
          }
        }
        else if(item.GroupA.Count() < item.GroupB.Count())//GroupB比較大代表對齊過，原字較小，選順
                {
          foreach(int _index in item.GroupA)
          {
            seged.Add(順seged[_index]);
          }
        }
        else if(item.GroupA.Count() > item.GroupB.Count())//GroupA比較大代表對齊過，原字較小，選逆
                {
          foreach(int _index in item.GroupB)
          {
            seged.Add(逆seged[_index]);
          }
        }
        else//餵IOSegment
        {
          double ratioA = correctionVarValue(CompareSegment.GetSegmentLengthVar(順seged,item.GroupA));
          double ratioB = correctionVarValue(CompareSegment.GetSegmentLengthVar(逆seged,item.GroupB));

          if(mathModel(get,set,item.GroupB.Count(),順seged,順posed,item.GroupA[0],ratioA,逆seged,逆posed,item.GroupB[0],ratioB) == true)
          {
            foreach(int _index in item.GroupB)
            {
              seged.Add(逆seged[_index]);
            }
          }
          else
          {
            foreach(int _index in item.GroupA)
            {
              seged.Add(順seged[_index]);
            }
          }
        }
      }
    }
    #endregion

    #region 順向最大長詞優先斷詞法
    static void 順斷(Get get,Set set,string sentence,List<string> seged)
    {
      int left = 0;
      while(left <= sentence.Length - 1)
      {
        int length = get.WordMaximumLength;
        for(; true; length--)
        {
          if(left + length <= sentence.Length)
          {
            var word = sentence.Substring(left,length);
            //交由IsWord做各式檢測，返回true則為詞，反之則不是
            if(length == 1 || get.IsWord(set,word) == true)
            {
              seged.Add(word);
              break;
            }
          }
        }
        left += length;
      }
    }
    #endregion

    #region 逆向最大長詞優先斷詞法
    static void 逆斷(Get get,Set set,string sentence,List<string> seged)
    {
      int right = sentence.Length - 1;
      while(right >= 0)
      {
        int length = get.WordMaximumLength;
        for(; true; length--)
        {
          if(right - length >= -1)
          {
            var word = sentence.Substring(right - length + 1,length);
            if(length == 1 || get.IsWord(set,word) == true)
            {
              seged.Insert(0,word);
              break;
            }
          }
        }
        right -= length;
      }
    }
    #endregion

    #region 
    static double correctionVarValue(double _value)
    {
      return Math.Pow((1.0 / (1.0 + _value)),2);
    }
    #endregion

    #region 
    static bool mathModel(Get get,Set set,int _wordModelLentgh,
        List<string> _segedA,List<string> _posedA,int _indexA,double _ratioA,
        List<string> _segedB,List<string> _posedB,int _indexB,double _ratioB)
    {
      List<double> valueA = new List<double>();
      List<double> valueB = new List<double>();

      valueA.Add((_indexA - 1 >= 0 && _posedA[_indexA - 1] != "notword") ? (PoSTw.PairFreq(_posedA[_indexA - 1],_posedA[_indexA])) : (PoSTw.BeginFreq(_posedA[_indexA])));//更前面的詞的連結頻率
      valueB.Add((_indexB - 1 >= 0 && _posedB[_indexB - 1] != "notword") ? (PoSTw.PairFreq(_posedB[_indexB - 1],_posedB[_indexB])) : (PoSTw.BeginFreq(_posedB[_indexB])));//更前面的詞的連結頻率

      while(_wordModelLentgh >= 1)
      {
        valueA.Add((_indexA + _wordModelLentgh < _segedA.Count && _posedA[_indexA + _wordModelLentgh] != "notword") ? (PoSTw.PairFreq(_posedA[_indexA + _wordModelLentgh - 1],_posedA[_indexA + _wordModelLentgh])) : (PoSTw.EndFreq(_posedA[_indexA + _wordModelLentgh - 1])));//詞的連結頻率
        valueB.Add((_indexB + _wordModelLentgh < _segedB.Count && _posedB[_indexB + _wordModelLentgh] != "notword") ? (PoSTw.PairFreq(_posedB[_indexB + _wordModelLentgh - 1],_posedB[_indexB + _wordModelLentgh])) : (PoSTw.EndFreq(_posedB[_indexB + _wordModelLentgh - 1])));//詞的連結頻率

        valueA.Add(get.PoSFreqAtWord(set,_segedA[_indexA + _wordModelLentgh - 1],_posedA[_indexA + _wordModelLentgh - 1]));//詞以詞性出現的頻率
        valueB.Add(get.PoSFreqAtWord(set,_segedB[_indexB + _wordModelLentgh - 1],_posedB[_indexB + _wordModelLentgh - 1]));//詞以詞性出現的頻率

        valueA.Add(1.0 / (Math.Pow(PoSTw.TotalFreq(_posedA[_indexA + _wordModelLentgh - 1]),2)));//除以總辭頻
        valueB.Add(1.0 / (Math.Pow(PoSTw.TotalFreq(_posedB[_indexB + _wordModelLentgh - 1]),2)));//除以總辭頻

        _wordModelLentgh--;
      }

      valueA.Add(_ratioA);
      valueB.Add(_ratioB);

      double goalA = 1.0;
      double goalB = 1.0;

      foreach(double item in valueA)
      {
        goalA *= item;
      }
      foreach(double item in valueB)
      {
        goalB *= item;
      }
      if(goalA <= 0.0 || goalB <= 0.0)
      {
        string oriString = "";
        foreach(string item in _segedA)
        {
          oriString += item;
        }
      }
      if(goalA < goalB)
      {
        return true;
      }
      else
      {
        return false;
      }
    }
    #endregion
  }
}
