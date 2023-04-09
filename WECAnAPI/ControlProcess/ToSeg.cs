using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WECAnAPI
{
  class ToSeg
  {
    public static void Handle(Get get,Set set,string sentence,List<string> seged)
    {
      if(set.CRFSegment == true)//以前的老方法，預設(Get.cs)為false
      {
        toSegMix(get,set,sentence,seged);
      }
      else
      {
        SegMaximumMatching.Handle(get,set,sentence,seged);
      }

      SplitNeu.Handle(get,set,seged);
      SplitNf.Handle(get,set,seged);
    }
    static void toSegMix(Get get,Set set,string sentence,List<string> seged)
    {
      seged.Clear();
      List<string> WC_list = new List<string>();
      SegMaximumMatching.Handle(get,set,sentence,WC_list);//先將字串使用長辭優先查表斷詞方法斷詞，得到斷詞的字串陣列*/
      string WC_sign = toBIES(WC_list);//將使用長辭優先查表得到的斷詞轉換成CRF的BIES格式以便之後做比較*/
      string CRF_sign = SegCRF.CountBIES(set,get,sentence);//將原始字串透過CRF的斷詞方法依照詞出現在句子前中後頻率做斷詞
      string final_sign = segMixSwitch(set,get,sentence,CRF_sign,WC_sign);//將長辭優先斷詞與CRF斷詞做綜合評斷得到最終的BIES結果*/
      segBIES(sentence,final_sign,seged);/*依據最後得到的BIES標記將原始字元序列做切割後返回字串陣列給呼叫方法*/
    }

    static string segMixSwitch(Set set,Get get,string str,string CRF,string WC)/*虛引數傳入原始字串、CRF斷詞得到的BIES標記、WECAN斷詞轉換的BIES標記，在這方法中返回由CRF與WECAN兩方所決策出的BIES標記，大多數情況下的決策會選擇WECAN的斷詞標籤，除非在CRF的BIES機率分數計算模式中WECAN概率低於CRF a倍才選擇CRF的標籤*/
    {
      string sign = null;
      int start = -1;
      for(int i = 0; i < CRF.Length; i++)
      {
        if(CRF[i] == 'S' && WC[i] == 'S')/*單字且相同直接處理*/
        {
          sign += "S";
          start = i + 1;
        }
        else if(start == -1 && (CRF[i] == 'B' || CRF[i] == 'S') && (WC[i] == 'B' || WC[i] == 'S'))/*若是雙方都出現開頭形式的標籤'B'或是單字標籤'S'，則認為這可能是一個詞的開頭，所以將前端指位旗標設定*///標籤出現B or S的時候，設定開頭索引值
        {
          start = i;
        }
        else if(start != -1 && (CRF[i] == 'E' || CRF[i] == 'S') && (WC[i] == 'E' || WC[i] == 'S'))/*若是雙方都出現結尾形式的標籤'E'或是單字標籤'S'，在前端指位標籤已被設定情勢下，認定這可能是一個詞的結尾，所以開始做出評分取捨以及統計上得到的特殊情況處理*///開頭索引值已被設定，標籤出現B or S，就算是找到共同切割點了，依照start~i進行切割
        {
          string C = CRF.Substring(start,i - start + 1);/*切割得到CRF的子標籤*/
          string W = WC.Substring(start,i - start + 1);/*切割得到WC的子標籤*/
          string S = str.Substring(start,i - start + 1);/*切割得到原始字元序列的部分字元子序列*/
          double a = 10;
          //單純相信詞數較少的，但字數至少要3個以上
          if(C == W) sign += W;/*雙方標籤相同則直接處理不必做評分比較*///當CRF與WeCAn答案相同時，應省去下列比較時間
          else if(W == "SBES" && C == "BEBE")/*特殊情況的例外處理，WECAN會有6分，CRF會有8分*///統計後的數據，這種情況WC較值得相信，但會因為WordTrustValue()的長度平方算法而相信CRF，所以做此特殊處理
          {
            sign += W;
          }
          else if(getWordTrustValue(W) >= getWordTrustValue(C) && W.Length >= 3 && C.Length >= 3)/*在雙方長度都至少為3的前提下WECAN分數高就選擇WECAN*///相信WeCan
          {
            sign += W;
          }
          else if(getWordTrustValue(W) < getWordTrustValue(C))//凡字數信任度小於CRF都要經過「偏袒WeCAn之檢測」
          {
            //大部分相信WeCan，除非CRF有十足把握(機率大過 WeCan a倍)
            if(SegCRF.CountLabelProbabilities(set,get,S,C) > a * SegCRF.CountLabelProbabilities(set,get,S,W))/*CRF的BIES機率分數必須要高過WECAN a倍才肯相信CRF的標籤，表示在CRF的BIES機率分數下，WECAN要在BIES機率分數中低於CRF斷詞才會被丟棄*/
            {
              sign += C;
            }
            else
            {
              sign += W;
            }
          }
          else//上述情況都未發生，相信WeCan
          {
            sign += W;
          }

          start = i + 1;//下一個切割點出現了
        }
        else { }/*不符合則不動作，for迴圈繼續跑往後切割判斷*/
      }
      return sign;
    }

    static int getWordTrustValue(string sign)/*傳入BIES標籤計算分數，依照詞長度的平方累加分數，所以這個分數相信長度較長的*///回傳字詞的信任程度
    {
      //分解標籤，BE BIE S S等等，存進List<string>
      List<string> seg = new List<string>();
      int start = 0, count = 0;
      while(start + count < sign.Length)/*採用先將BIES字元序列切割成陣列的方式，再計算長度分數*/
      {
        if(sign[start + count] == 'S')/*單字元詞直接處理*/
        {
          seg.Add(sign.Substring(start,1));
          start++;
          count = 0;
        }
        else if(sign[start + count] == 'B' || sign[start + count] == 'I')/*如果不是結尾標籤切割點再往後*/
        {
          count++;
        }
        else if(sign[start + count] == 'E')/*如果是表示結尾的標籤'E'就依此切割*/
        {
          count++;
          seg.Add(sign.Substring(start,count));/*切割得到子序列*/
          start += count;/*切割完成，前端指位標籤移動到切割點後一個字元*/
          count = 0;/*長度歸0*/
        }
      }
      //計算其信任值，上述範例為2*2 + 3*3 + 1*1 + 1*1 = 15，如果今天是BIIIIE則為7*7 = 49
      int value = 0;
      for(int i = 0; i < seg.Count; i++)/*計算平方分數*/
      {
        value += seg[i].Length * seg[i].Length;
      }
      return value;/*返回計算得到的平方分數*/
    }

    static void segBIES(string data,string sign,List<string> seged)/*虛引數傳入原始字元序列與BIES，將字元序列依據BIES標籤切割成字串陣列並返回*/
    {
      int start = 0, count = 0;/*宣告前端指位旗標與切割用的長度旗標*/
      while(start + count < data.Length)/*循環至字元序列截止*/
      {
        if(sign[start + count] == 'S')/*如果標籤表示這個字元為單一字詞則直接切割*/
        {
          seged.Add(data.Substring(start,1));
          start++;
          count = 0;
        }
        else if(sign[start + count] == 'B' || sign[start + count] == 'I')/*如果標籤表示這個字元屬於詞的中間則繼續尋找切割點*/
        {
          count++;
        }
        else if(sign[start + count] == 'E')/*標籤得到'E'表示此時已指到詞的結束字元*/
        {
          count++;
          seged.Add(data.Substring(start,count));
          start += count;
          count = 0;
        }
      }
    }

    static string toBIES(List<string> list)/*傳入詞陣列，並依據長度標記成BIES後回傳標籤序列*/
    {
      string reg = null;
      for(int i = 0; i < list.Count; i++)
      {
        if(list[i].Length == 1) reg += "S";
        else
        {
          for(int j = 0; j < list[i].Length; j++)
          {
            if(j == 0) reg += "B";
            else if(j == list[i].Length - 1) reg += "E";
            else reg += "I";
          }
        }
      }
      return reg;
    }
  }
}
