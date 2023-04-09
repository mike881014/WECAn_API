using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WECAnAPI.RuleBased
{
  //此類別為累積式補丁，係自實際例子或已知問題無法統一性的修復或其他考量而以補釘取而代之
  class Patches
  {
    public static bool Handle(Get get,Set set,List<string> seged,List<string> posed)
    {
      bool haveHandle = false;
      for(int i = 0; i < seged.Count; i++)
      {
        #region 由+???
        if(i + 2 < seged.Count && seged[i + 1].Length == 2 && seged[i + 2].Length == 1 && seged[i] == "由" && seged[i + 1][1] == '向')
        {
          seged[i + 1] = seged[i + 1][0].ToString();
          seged.Insert(i + 2,"向");
          posed.Clear();
          ToPoS.Handle(get,set,seged,posed,set.SimplePOSTransfer);
          haveHandle = true;
        }
        #endregion

        #region 22斷詞問題
        //在校園內→依長詞優先法會斷成→在校+園內，此為錯誤斷詞，應斷為 在+校園+內
        //此補丁門檻值由平衡語料調整，在簡體模式中，可能不是那麼精準
        if(i + 1 < seged.Count && seged[i].Length == 2 && seged[i + 1].Length == 2
          && get.IsWord(set,seged[i][1].ToString() + seged[i + 1][0].ToString()) == true
          && posed[i] != "notword" && posed[i + 1] != "notword" && (seged[i][0] != seged[i][1] && seged[i + 1][0] != seged[i + 1][1]))
        {
          //在_校_園_內=s1_s2_s3_s4
          double s12 = get.PoSTotalFreqAtWord(set,seged[i]);
          double s34 = get.PoSTotalFreqAtWord(set,seged[i + 1]);
          double s1 = get.PoSTotalFreqAtWord(set,seged[i][0].ToString());
          double s23 = get.PoSTotalFreqAtWord(set,seged[i][1].ToString() + seged[i + 1][0].ToString());
          double s4 = get.PoSTotalFreqAtWord(set,seged[i + 1][1].ToString());

          //門檻值由測試平衡語料例子而定，條件核心為"分開的可能性"
          if(s12 > 0 && s34 > 0 && s1 > 0 && s23 > 0 && s4 > 0
            && (s12 < 33 && s34 < 30)
            && (s1 > 310 && s23 > 400 && s4 > 300))
          {
            seged.Insert(i + 1,seged[i][1].ToString() + seged[i + 1][0].ToString());
            seged[i] = seged[i][0].ToString();
            seged[i + 2] = seged[i + 2][1].ToString();
            posed.Clear();
            ToPoS.Handle(get,set,seged,posed,true);
          }
        }
        #endregion

        #region 好了
        if(i - 1 >= 0 && seged[i] == "好了" && seged[i - 1] != "不用" && (posed[i - 1] == "Dfa" || posed[i - 1] == "D"))
        {
          seged[i] = seged[i][0].ToString();
          seged.Insert(i + 1,"了");
          posed.Clear();
          ToPoS.Handle(get,set,seged,posed,set.SimplePOSTransfer);
          haveHandle = true;
        }
        #endregion

        #region 親愛的
        //分析語委空集合時發現
        if(i + 1 < posed.Count && (seged[i] == "親愛的" || seged[i] == "亲爱的") && (posed[i + 1] == "Na" || posed[i + 1] == "Nb"))
        {

          seged[i] = seged[i].Substring(0,2);
          seged.Insert(i + 1,"的");
          posed.Clear();
          ToPoS.Handle(get,set,seged,posed,set.SimplePOSTransfer);
          haveHandle = true;

        }
        #endregion
      }
      return haveHandle;
    }
  }
}
