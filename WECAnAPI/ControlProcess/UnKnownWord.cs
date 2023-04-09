using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WECAnAPI
{
  static class UnKnownWordHandle
  {
    #region 未知詞處理介面
    public static void UnKnownWordFunction(Get get,Set set,List<List<string>> paperSegment,List<List<string>> paperPoS,List<string> 未知詞清單)/*虛引數所傳入詞與詞性的次數並歷經組合各種詞並計算次數、經由SPLR方法審視、置換原本傳入的資料後返回通過未知詞審查的詞陣列給呼叫處*///未知詞訓練+合併，主要函數
    {
      List<string> seged = new List<string>();
      List<string> posed = new List<string>();
      Dictionary<string,int> TextFreq = new Dictionary<string,int>();

      seged.AddRange(paperSegment[0]);
      posed.AddRange(paperPoS[0]);
      for(int forCount = 1; forCount < paperSegment.Count(); forCount++)
      {
        seged.Add("\n");
        posed.Add("notword");
        seged.AddRange(paperSegment[forCount]);
        posed.AddRange(paperPoS[forCount]);
      }

      UnKnownWordTraining(get,set,seged,posed,TextFreq);//建立詞與詞之間的組合資料
      UnKnownWordSearch(get,set,seged,posed,TextFreq,未知詞清單);
      UnKnownWordMerger(set,paperSegment,paperPoS,未知詞清單);
    }
    #endregion
    
    #region 詞之間的組合資料
    /// <summary>
    /// 以句為單位，將詞可能之組合存入TextFreq。
    /// </summary>
    /// <param name="get"></param>
    /// <param name="set"></param>
    /// <param name="seged"></param>
    /// <param name="posed"></param>
    /// <param name="TextFreq">詞可能的組合</param>
    static void UnKnownWordTraining(Get get,Set set,List<string> seged,List<string> posed,Dictionary<string,int> TextFreq)/*將字詞以相互組合的方式建立各種可能的頻率清單*//*藉由斷詞結果建立各種組合之字典檔*/
    {
      List<List<string>> Text_list = new List<List<string>>();
      //將文章分成一句一句以Text_list儲存
      for(int i = 0; i < seged.Count; i++)/*將全文組合的字詞陣列組合成句-詞的二維架構*/
      {//若遇到標點符號則存入一個新的list<string>
        if(i == 0) Text_list.Add(new List<string>());/*起始建立第一個「句」*/
        if(posed[i] != "notword") Text_list[Text_list.Count - 1].Add(seged[i]);/*不是標點符號表示這個詞屬於同一「句」，直接添加進「句」*/
        else/*否則則建立新的「句」*/
        {
          if(Text_list[Text_list.Count - 1].Count > 0) Text_list.Add(new List<string>());
        }
      }
      //將所有可能之排列組合存入TextFreq，其key參數格式較特別，為留住原始結構所以參數格式採用List<string>
      for(int i = 0; i < Text_list.Count; i++)
      {
        int num = set.K - 1;/*定義詞組合的單位數，但建立組合清單時採少一個字詞是因SPLR方法計算所需要用到的關係，所以建置TextFreq需要更小的組合以方便SPLR也可以使用到*/
        while(num <= Text_list[i].Count)/*統計各種字詞組合出現的次數並建立資料清單*/
        {//將句子做各種組合，若TextFreq沒有此種組合則加入
          for(int j = 0; j <= Text_list[i].Count - num; j++)
          {
            string key = null;
            for(int k = j; k < j + num; k++) key += Text_list[i][k];
            string tp = key;
            if(TextFreq.ContainsKey(tp) == false) TextFreq.Add(tp,1);
            else TextFreq[tp]++;
          }
          num++;
        }
      }
    }
    #endregion
    
    #region 將第一步的資料替除非未知詞的結果
    static void UnKnownWordSearch(Get get,Set set,List<string> seged,List<string> posed,Dictionary<string,int> TextFreq,List<string> NotKnowWord_list)/*藉由NotKnowWordTraining之字典檔，判斷文章中未知詞組合是否成立並建置通過審查符合未知詞特徵的List*/
    {
      List<List<string>> Text_list = new List<List<string>>();
      Dictionary<string,string> NotKnowWord_hash = new Dictionary<string,string>();
      System.IO.StreamWriter fo = null;
      if (set.LogNotInUnKnownWord == true) {
        fo = new System.IO.StreamWriter(set.CorpusPath + @"WECAnUser\NotInUnKnownWord.log", false, System.Text.Encoding.Unicode);
        fo.WriteLine(string.Format("K={0} C={1} D={2} E={3} U={4} V={5}", set.K, set.C, set.D, set.E, set.U, set.V)); 
        fo.WriteLine();
      }
      //將文章分成一句一句以Text_list儲存
      for(int i = 0; i < seged.Count; i++)/*將全文組合的字詞陣列組合成句-詞的二維架構*/
      {
        if(i == 0) Text_list.Add(new List<string>());/*起始建立第一個「句」*/
        if(posed[i] != "notword") Text_list[Text_list.Count - 1].Add(seged[i]);/*不是標點符號表示這個詞屬於同一「句」，直接添加進「句」*/
        else/*否則則建立新的「句」*/
        {
          if(Text_list[Text_list.Count - 1].Count > 0) Text_list.Add(new List<string>());
        }
      }
      //將所有可能之排列組合存入TextFreq，其key參數格式較特別，為留住原始結構所以參數格式採用List<string>
      for(int i = 0; i < Text_list.Count; i++)
      {
        int num = set.K;//跟NotKnowWordTraining不同，因tL、tR的關係，TextFreq建檔需要更小的組合，因此少一階(此處不需要)
        while(num <= Text_list[i].Count)
        {
          for(int j = 0; j <= Text_list[i].Count - num; j++)
          {
            List<string> tp = Text_list[i].GetRange(j,num);

            string key = null;
            for(int k = 0; k < tp.Count; k++) key += tp[k];
            if(NotKnowWord_hash.ContainsKey(key) == false)/*~~可能有問題，特定組合未通審查而未建置進清冊，但會在別句出現過而再度審查*///此tp是沒計算過的才計算，否則跳過
            {
              List<string> tw = Text_list[i];/*tw參考至要處理的這「句」*/
              if(PredictNotKnowWord(get,set,tp,tw,TextFreq, fo) == true)/*如果這組詞符合未知詞的特徵審查*/
              {
                if(NotKnowWord_hash.ContainsKey(key) == false) NotKnowWord_hash.Add(key,key);/*~~可能為多餘式*/
              }
            }
          }
          num++;
        }
      }
      KeyValuePair<string,string>[] NotKnowWord_pair = NotKnowWord_hash.ToArray();/*將建置出的未知清冊轉換為陣列*/
      for(int i = 0; i < NotKnowWord_pair.Length; i++)/*將陣列轉換為動態陣列後回傳給呼叫方法*/
      {
        string key = NotKnowWord_pair[i].Key;
        if (key.Length == 2 && get.CharPairFreq(set, key[0], key[1]) > 1e3) {//科學記號
          continue;
        }

        NotKnowWord_list.Add(key);
      }
      if (set.LogNotInUnKnownWord == true) {
        fo.Close();
      }
    }
    #endregion
    
    
    static void UnKnownWordMerger(Set set,List<List<string>> paperSegment,List<List<string>> paperPoS,List<string> 未知詞清單)/*利用NotKnowWordSearch來合併未知詞，藉由查詢所建置的未知詞集合，將斷詞與詞性的集合做出對應的替換修改*/
    {
      int 字詞最大長度 = int.MinValue;

      if(未知詞清單.Count <= 0)
      {
        return;
      }

      foreach(string word in 未知詞清單)
      {
        if(word.Length > 字詞最大長度)
        {
          字詞最大長度 = word.Length;
        }
      }

      for(int line = 0; line < paperSegment.Count(); line++)
      {
        for(int start = 0; start < paperSegment[line].Count; start++)
        {
          int wordLength = 字詞最大長度;
          if(start + wordLength > paperSegment[line].Count)
          {//爛透了，資料架構居然分不清楚
            wordLength = paperSegment[line].Count() - start;
          }
          while(wordLength > 1)//詞組合數2以上才有需要合併
          {
            string 拼湊字詞 = "";
            for(int shift = start; shift < start + wordLength; shift++)//拼湊目前指向詞的組合
            {
              拼湊字詞 += paperSegment[line][shift];
            }
            if(未知詞清單.Contains(拼湊字詞) == true)//發現未知詞則合併且做後續處理
            {
              paperSegment[line][start] = 拼湊字詞;
              paperPoS[line][start] = set.UnKnownPoS;
              paperSegment[line].RemoveRange(start + 1,wordLength - 1);
              paperPoS[line].RemoveRange(start + 1,wordLength - 1);
              break;
            }
            wordLength--;
          }
        }
      }
    }
    
    #region 更新字典
    public static void WriteToCDBU(Get get,Set set,List<string> UnKnownWord_list)/*虛引數傳入無路經或是使用者指定的路徑與未知詞的詞集合陣列，在字典大小小於16MB前提下，把不存在的詞依資料格式填入"null、0"到字典中並作時間標記，建置或是更新字典後離開*/
    {
      var list = new List<string>();
      for(int i = 0; i < UnKnownWord_list.Count; i++)/*如果想新增的擴充詞不在原本字典內的話就添加進字典*/
      {
        string word = UnKnownWord_list[i];
        if(get.UserWord.Contains(word) == false && get.IsWord(set, word) == false)
        {
          bool unique = true;
          if (set.JustUnique == true) {
            foreach (string other in UnKnownWord_list) {
              if (word.Length != other.Length && other.IndexOf(word) != -1) {
                unique = false;
                break;
              }
            }
          }
          if (unique == true) {
            // ofp.WriteLine(word + ";");
            list.Add(word);
          }
        }
      }

      if (
        list.Count() == 0 || 
        System.IO.File.Exists(set.CorpusPath + @"WECAnUser\UserWord.txt") == true &&
        new System.IO.FileInfo(set.CorpusPath + @"WECAnUser\UserWord.txt").Length > 16777216) {
        /*如果要擴充詞數過濾後為0，或是指定路徑或是執行可視角度存在擴充字典，且存在但內容大小超過16M就不執行擴充辭典*/
        return;
      }

      System.IO.StreamWriter ofp = new System.IO.StreamWriter(set.CorpusPath + @"WECAnUser\UserWord.txt",true,System.Text.Encoding.Unicode);
      ofp.WriteLine("/*");
      ofp.WriteLine("#####################################" + Environment.NewLine +
                    "###      " + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "      ###" + Environment.NewLine +
                    "#####################################");/*註記這次新增的時間資訊*/
      ofp.WriteLine("*/");
      foreach (string word in list) {
        ofp.Write(word
          .Replace(@";", @"\;")
          .Replace(@":", @"\:")
          .Replace(@"/", @"\/")
          );
        ofp.WriteLine(";");
      }
      ofp.Close();
      get.refreshUserWord();
    }
    #endregion
    
    #region 未知詞的特徵審查
    static bool PredictNotKnowWord(Get get,Set set,List<string> tp,List<string> tw,Dictionary<string,int> TextFreq, System.IO.StreamWriter fo)/*tp為所要處理特定大小以上的連續詞陣列，tw為所參照的「句」的詞陣列，將所要探討字詞組合帶入此方法計算，求出這個字詞組合是否在指定設定參數下符合所預期的未知詞特徵*/
    {
      int n = tp.Count;
      int tf_tp = tf(tp, TextFreq);
      int tf_tw = tf(tw, TextFreq);
      double SPLR_tp = SPLR(tp, TextFreq);
      double SPLR_tw = SPLR(tw, TextFreq);

      int flag = 0;
      if (set.LogNotInUnKnownWord == true) {
        fo.WriteLine(string.Join(null, tp));
        fo.Write(n < set.K ? "x": ""); 
        fo.WriteLine(string.Format("\t{0} >= set.K", n));
        fo.Write(tf_tp < set.C ? "x": ""); 
        fo.WriteLine(string.Format("\t{0} >= set.C", tf_tp));
        fo.Write(SPLR_tp < 1 - set.E && tf_tp < set.D ? "x": ""); 
        fo.WriteLine(string.Format("\t{0} >= 1 - set.E || {1} >= set.D", SPLR_tp, tf_tp));
        fo.Write(SPLR_tp < set.U * SPLR_tw && tf_tp < set.V * tf_tw ? "x": ""); 
        fo.WriteLine(string.Format("\t{0} >= set.U * {1} || {2} >= set.V * {3}", SPLR_tp, SPLR_tw, tf_tp, tf_tw));
        fo.WriteLine();
      }

      if(n >= set.K) flag++;/*字詞的組合數必須達到設定檔所設定以上才通過審查*//*這個條件幾乎是必過的，會帶入的tp都是經過篩選的*/
      if(tf_tp >= set.C) flag++;/*C門檻指達到所設定的出現次數以上才通過審查*/
      if(SPLR_tp >= 1 - set.E || tf_tp >= set.D) flag++;/*若是這種字詞與子組合出現的相依高，則可以推論這組字詞可能是一個不可分割的未知詞，例如「溫室.效應.會」中「溫室.效應」的次數預期會遠大於「溫室.效應.會」的次數，所以這個審查就很難通過，或是這個字詞組合出現的次數很大到超過數量D，則通過這個審查*/
      if(SPLR_tp >= set.U * SPLR_tw || tf_tp >= set.V * tf_tw) flag++;/*這裡有審查上有效性的疑問存在，若是指定字詞組合SPLR值大於整句SPLR值U倍，或是指定字詞組合的出現次數大於整句所組成字詞的出現次數V倍，則通過這個審查*/
      if(flag == 4) return true;/*在以上4個條件都通過審查時，回應呼叫方法true，否則回應false*/
      return false;
    }
    #endregion
    
    #region 返回此詞組合出現次數
    static int tf(List<string> Key_list,Dictionary<string,int> TextFreq)/*傳入字詞陣列，根據所統計出來各種組合的次數返回這個組合的出現次數*//*取得data中有多少keyword*/
    {
      string tp = null;
      for(int i = 0; i < Key_list.Count; i++) tp += Key_list[i];
      return TextFreq[tp];
    }
    #endregion
    
    #region SPLR算出未知詞的可能，回傳值為這個數值表示這個字詞組合與字詞子組合的分割關係
    static double SPLR(List<string> Key_list,Dictionary<string,int> TextFreq)/*由輸入的字詞組合計算而返回一個數值，這個數值表示這個字詞組合與字詞子組合的分割關係，算法為 (這個字詞組合的出現次數)/(字詞子組合出現次數較高的次數) ，由於字詞子組合的出現次數一定大於等於字詞父組合，所以若是這個數值接近數值1，則表示父序列與子序列在文中出現可說是綁定出現，很少以部分的字詞子組合出現，那可預期這種組合很有能是一個未知詞*/
    {
      string tp = null;
      for(int i = 0; i < Key_list.Count; i++) tp += Key_list[i];/*拼湊全長度的組合*/
      string tl = null;
      for(int i = 0; i < Key_list.Count - 1; i++) tl += Key_list[i];/*拼湊少最後一個詞的組合*/
      string tr = null;
      for(int i = 1; i < Key_list.Count; i++) tr += Key_list[i];/*拼湊少最前面一個詞的組合*/

      double tp_tf = Convert.ToDouble(TextFreq[tp]);
      double tl_tf = Convert.ToDouble(TextFreq[tl]);
      double tr_tf = Convert.ToDouble(TextFreq[tr]);
      double max_tf;
      if(tl_tf >= tr_tf) max_tf = tl_tf;/*取得次數出現較高的組合數*/
      else max_tf = tr_tf;
      return tp_tf / max_tf;/*返回SPLR值*/
    }
    #endregion
  }
}
