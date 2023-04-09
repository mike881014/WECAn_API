using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WECAnAPI
{
  /// <summary>
  /// 詞性標記
  /// </summary>
  class ToPoS
  {
    #region Handle詞性標記介面，若為數字或英文取的可能詞類則無法取得任何詞類，將交由
    /// <summary>
    /// 詞性標記介面
    /// </summary>
    /// <param name="get">記憶體資料物件</param>
    /// <param name="set">套用設定物件</param>
    /// <param name="seged">斷詞集合</param>
    /// <param name="posed">詞性集合</param>
    /// <param name="toSimplePoS">是否轉為簡易詞性</param>
    public static void Handle(Get get,Set set,List<string> seged,List<string> posed,bool toSimplePoS)
    {
      posed.Clear();
      List<List<PoS.WordPoS>> 可能詞類 = new List<List<PoS.WordPoS>>();
      取得可能詞類(get,set,seged,可能詞類);
      計算詞類結果(get,seged,posed,可能詞類);
      if(toSimplePoS == true)
      {
        PoS.ToSimplePoS(posed);
      }
    }
        #endregion

    #region 取得可能詞類(CRF已不再使用)，當詞為英文或數字時，交由RuleBase做處理 備註:SMM會把數字與英文合併
    static void 取得可能詞類(Get get,Set set,List<string> seged,List<List<PoS.WordPoS>> 可能詞類)
    {//虛引數傳入被斷詞處理過得字串陣列，經由查表取得詞所對應的可能詞性列表、取得詞的詞性列表
      for(int segedIndex = 0; segedIndex < seged.Count; segedIndex++)
      {
        if(get.IsWord(set,seged[segedIndex]) == true)
        {
          if(set.NameTowPassHandle == true && get.nameCache.Contains(seged[segedIndex]) == true)
          {
            可能詞類.Add(new List<PoS.WordPoS>() { new PoS.WordPoS("Nb",1) });
          }
          else
          {
            可能詞類.Add(get.WordPoSList(set,seged[segedIndex]));
          }
        }
        else if(RuleBased.FW.IsFW(seged[segedIndex]))
        {
          可能詞類.Add(new List<PoS.WordPoS> { new PoS.WordPoS("FW",1) });
        }
        else if(RuleBased.Nd.IsNd(seged[segedIndex]))
        {
          可能詞類.Add(new List<PoS.WordPoS> { new PoS.WordPoS("Nd",1) });
        }
        else if(RuleBased.Neu.IsNeqa(seged[segedIndex]))
        {
          可能詞類.Add(new List<PoS.WordPoS> { new PoS.WordPoS("Neqa",1) });
        }
        else if(RuleBased.Neu.IsNeu(seged[segedIndex]))
        {
          可能詞類.Add(new List<PoS.WordPoS> { new PoS.WordPoS("Neu",1) });
        }
        else if(seged[segedIndex].Length > 1)
        {// CRF所產生的詞 當混出來的字沒有在語料裡
          可能詞類.Add(new List<PoS.WordPoS> { new PoS.WordPoS("b",1) });
        }
        else
        {// 標點符號
          可能詞類.Add(new List<PoS.WordPoS> { new PoS.WordPoS("notword",1) });
        }
      }
    }
    #endregion


    static void 計算詞類結果(Get get,List<string> seged,List<string> posed,List<List<PoS.WordPoS>> 可能詞類)
    {
      for(int wordIndex = 0; wordIndex < 可能詞類.Count(); wordIndex++)
      {
        if(可能詞類[wordIndex].Count() == 0)
        {
          throw new NotSupportedException();
        }
        else if(可能詞類[wordIndex].Count() == 1)
        {//一個直接標
          posed.Add(可能詞類[wordIndex][0].PoS);
        }
        else
        {
          bool forwardIsEdge = (wordIndex == 0);
          bool backwardIsEdge = (wordIndex == 可能詞類.Count() - 1);

          //bool forwardIsEdge = (wordIndex == 0 || 可能詞類[wordIndex - 1].Count() == 1 && 可能詞類[wordIndex - 1][0].PoS == "notword");
          //bool backwardIsEdge = (wordIndex == 可能詞類.Count() - 1 || 可能詞類[wordIndex + 1].Count() == 1 && 可能詞類[wordIndex + 1][0].PoS == "notword");

          double bestScore = 0.0;
          string bestPoS = "b";

          for(int selfPoSIndex = 0; selfPoSIndex < 可能詞類[wordIndex].Count(); selfPoSIndex++)
          {
            if(forwardIsEdge == false && backwardIsEdge == false)
            {// 句中
              for(int lastPoSIndex = 0; lastPoSIndex < 可能詞類[wordIndex + 1].Count(); lastPoSIndex++)
              {
                string 前詞 = PoS.ToSimplePoS(posed[wordIndex - 1]);
                string 本詞 = PoS.ToSimplePoS(可能詞類[wordIndex][selfPoSIndex].PoS);
                string 後詞 = PoS.ToSimplePoS(可能詞類[wordIndex + 1][lastPoSIndex].PoS);

                double forwardConditionalProbability = PoSTw.PairFreq(前詞,本詞) / PoSTw.TotalFreq(本詞);// 前詞與本詞的組數/本詞
                double backwardConditionalProbability = PoSTw.PairFreq(本詞,後詞) / PoSTw.TotalFreq(本詞);// 以此類推
                double selfMarginalProbability = 可能詞類[wordIndex][selfPoSIndex].PoSNum;// / get.PoSTotalFreqAtWord(seged[wordIndex]);
                double backwardMarginalProbability = 可能詞類[wordIndex + 1][lastPoSIndex].PoSNum;// / get.PoSTotalFreqAtWord(seged[wordIndex + 1]);

                double score =
                    (forwardConditionalProbability + 0.0001)// (前+本)/本 
                    * (backwardConditionalProbability + 0.0001)// (本+後)/本
                    * Math.Sqrt(selfMarginalProbability + 0.0001)// 根號 本
                    * Math.Sqrt(backwardMarginalProbability + 0.0001);// 根號 後

                if(score > bestScore)
                {
                  bestScore = score;
                  bestPoS = 可能詞類[wordIndex][selfPoSIndex].PoS;
                }
              }
            }
            else if(forwardIsEdge == true && backwardIsEdge == false)
            {// 句首
              for(int lastPoSIndex = 0; lastPoSIndex < 可能詞類[wordIndex + 1].Count(); lastPoSIndex++)
              {
                string 本詞 = PoS.ToSimplePoS(可能詞類[wordIndex][selfPoSIndex].PoS);
                string 後詞 = PoS.ToSimplePoS(可能詞類[wordIndex + 1][lastPoSIndex].PoS);

                double forwardConditionalProbability = PoSTw.BeginFreq(本詞) / PoSTw.TotalFreq(本詞);
                double backwardConditionalProbability = PoSTw.PairFreq(本詞,後詞) / PoSTw.TotalFreq(本詞);
                double selfMarginalProbability = 可能詞類[wordIndex][selfPoSIndex].PoSNum;// / get.PoSTotalFreqAtWord(seged[wordIndex]);
                double backwardMarginalProbability = 可能詞類[wordIndex + 1][lastPoSIndex].PoSNum;// / get.PoSTotalFreqAtWord(seged[wordIndex + 1]);

                double score =
                    (forwardConditionalProbability + 0.0001)
                    * (backwardConditionalProbability + 0.0001)
                    * Math.Sqrt(selfMarginalProbability + 0.0001)
                    * Math.Sqrt(backwardMarginalProbability + 0.0001);

                if(score > bestScore)
                {
                  bestScore = score;
                  bestPoS = 可能詞類[wordIndex][selfPoSIndex].PoS;
                }
              }
            }
            else if(forwardIsEdge == false && backwardIsEdge == true)
            {// 句尾
              string 前詞 = PoS.ToSimplePoS(posed[wordIndex - 1]);
              string 本詞 = PoS.ToSimplePoS(可能詞類[wordIndex][selfPoSIndex].PoS);

              double forwardConditionalProbability = PoSTw.PairFreq(前詞,本詞) / PoSTw.TotalFreq(本詞);
              double backwardConditionalProbability = PoSTw.EndFreq(本詞) / PoSTw.TotalFreq(本詞);
              double selfMarginalProbability = 可能詞類[wordIndex][selfPoSIndex].PoSNum;// / get.PoSTotalFreqAtWord(seged[wordIndex]);
              double backwardMarginalProbability = 1;

              double score =
                  (forwardConditionalProbability + 0.0001)
                  * (backwardConditionalProbability + 0.0001)
                  * Math.Sqrt(selfMarginalProbability + 0.0001)
                  * Math.Sqrt(backwardMarginalProbability + 0.0001);

              if(score > bestScore)
              {
                bestScore = score;
                bestPoS = 可能詞類[wordIndex][selfPoSIndex].PoS;
              }
            }
            else if(forwardIsEdge == true && backwardIsEdge == true)
            {// 獨立詞
              double forwardConditionalProbability = 1;
              double backwardConditionalProbability = 1;
              double selfMarginalProbability = 可能詞類[wordIndex][selfPoSIndex].PoSNum ;// / get.PoSTotalFreqAtWord(seged[wordIndex]);
              double backwardMarginalProbability = 1;

              double score =
                  (forwardConditionalProbability + 0.0001)
                  * (backwardConditionalProbability+0.0001)
                  * Math.Sqrt(selfMarginalProbability + 0.0001)
                  * Math.Sqrt(backwardMarginalProbability + 0.0001);

              if(可能詞類[wordIndex][selfPoSIndex].PoS[0] == 'N')
              {// 名詞類分數加權
                score *= 100;
              }

              if(score > bestScore)
              {
                bestScore = score;
                bestPoS = 可能詞類[wordIndex][selfPoSIndex].PoS;
              }
            }
            else
            {
              throw new NotSupportedException();
            }
          }

          posed.Add(bestPoS);
        }
      }
    }
  }
}
