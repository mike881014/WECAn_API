using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WECAnAPI.RuleBased
{   /// <summary>
    /// 原文 輕輕的 被斷成 輕 輕 的 處理 先把第二個字往前合併 把的刪掉 給予詞性
    /// </summary>
  static class MergeSpecialRule
  {
    public static bool Handle(Get get,Set set,List<string> seged,List<string> posed)
    {
      //注意條件式中若有字詞判斷其應包含簡體及繁體。
      bool haveHandle = false;
      for(int i = 0; i < seged.Count; i++)
      {
        #region 疊字合併(輕+輕=輕輕、嘿+嘿+嘿=嘿嘿嘿)
        if(set.MergeConsecutiveSingleWord == true && i + 1 < seged.Count)
        {
          if(seged[i].Length == 1 && seged[i + 1].Length == 1 && seged[i] == seged[i + 1])//兩詞相同
          {
            string originalPos = posed[i];
            int mergeCount; string mergeString = null;
            for(mergeCount = 0; i + mergeCount < seged.Count(); mergeCount++)
            {
              string pos = posed[i + mergeCount];
              if(pos == "Nb" || pos == "SHI" || pos == "DE" || pos == "T" || pos == "P" || pos == "notword" || seged[i] != seged[i + mergeCount])
              {
                break;
              }
              mergeString += seged[i];
            }
            mergeCount--;

            if(mergeCount > 0)
            {
              seged[i] = mergeString;
              seged.RemoveRange(i + 1,mergeCount);/*移除要合併組合的後面詞*/
              posed.RemoveRange(i,mergeCount + 1);/*移除要合併組合的後面詞性*/
              posed.Insert(i,originalPos);//給予原始詞性
              haveHandle = true;
            }
          }
        }
        #endregion

        #region 動詞方向詞合併(看過+來=看過來 或 放+回去=放回去)
        if(set.MergeVerbDirectionWord == true && i + 1 < seged.Count)
        {
          //'看過'+'來'='看過來'
          if(seged[i].Length == 2 && seged[i + 1].Length == 1)
          {
            if((seged[i][1] == '過' || seged[i][1] == '过' || seged[i][1] == '進' || seged[i][1] == '进' || seged[i][1] == '回' || seged[i][1] == '出' || seged[i][1] == '上' || seged[i][1] == '下')
                && (seged[i + 1] == "來" || seged[i + 1] == "来" || seged[i + 1] == "去"))
            {
              if(posed[i] == "VA" || posed[i] == "VB" || posed[i] == "VC" || posed[i] == "VD" || posed[i] == "VF")
              {
                seged[i] += seged[i + 1];
                seged.RemoveRange(i + 1,1);
                posed.RemoveRange(i,2);
                posed.Insert(i,"VA");
                haveHandle = true;
              }
            }
          }
          //'放'+'回去'='放回去'
          if(seged[i].Length == 1 && seged[i + 1].Length == 2 && seged[i] != "請" && seged[i] != "请")
          {
            if((seged[i + 1][0] == '過' || seged[i + 1][0] == '过' || seged[i + 1][0] == '進' || seged[i + 1][0] == '进' || seged[i + 1][0] == '回' || seged[i + 1][0] == '出' || seged[i + 1][0] == '上' || seged[i + 1][0] == '下') &&
            (seged[i + 1][1] == '來' || seged[i + 1][1] == '来' || seged[i + 1][1] == '去'))
            {
              if(posed[i] == "VA" || posed[i] == "VB" || posed[i] == "VC" || posed[i] == "VD" || posed[i] == "VF")
              {
                seged[i] += seged[i + 1];
                seged.RemoveRange(i + 1,1);
                posed.RemoveRange(i,2);
                posed.Insert(i,"VA");
                haveHandle = true;
              }
            }
          }
        }
        #endregion

        #region 會不會、有沒有、能不能
        if(i + 1 < seged.Count)
        {
          if(seged[i].Length == 1 && seged[i + 1].Length == 2)
          {
            if(seged[i] + seged[i + 1] == "會不會" ||//詞中有符合"會不會"、"有沒有"、"能不能"就合併
                seged[i] + seged[i + 1] == "会不会" ||
                seged[i] + seged[i + 1] == "有沒有" ||
                seged[i] + seged[i + 1] == "有没有" ||
                seged[i] + seged[i + 1] == "能不能")
            {
              seged[i] += seged[i + 1];
              seged.RemoveRange(i + 1,1);
              posed.RemoveRange(i,2);
              posed.Insert(i,"D");
              haveHandle = true;
            }
          }
        }
        #endregion

        #region 四字詞組合併(輕 + 輕鬆 + 鬆 = 輕輕鬆鬆)
        if(i + 2 < seged.Count)
        {
          if(seged[i].Length == 1 && seged[i + 1].Length == 2 && seged[i + 2].Length == 1)
          {
            if(seged[i][0] == seged[i + 1][0] && seged[i + 1][1] == seged[i + 2][0])//   輕  輕鬆  鬆，輕[0]==輕鬆[0]、輕鬆[1]==鬆[0]
            {
              seged[i] += seged[i + 1] + seged[i + 2];
              seged.RemoveRange(i + 1,2);
              posed.RemoveRange(i,3);
              posed.Insert(i,"VH");
              haveHandle = true;
            }
          }
        }
        #endregion

        #region 三字詞組合併(搖 + 搖頭 = 搖搖頭)
        if(set.MergeSpecialThreeWord == true && i + 1 < seged.Count)
        {
          if(seged[i].Length == 1 && seged[i + 1].Length == 2)/*1-2的三字情況*/
          {
            if(seged[i][0] == seged[i + 1][0] &&
                posed[i][0] != 'N' && posed[i + 1][0] != 'N' &&//詞性不能為N(Na.Nb.Nc...)、VH、VI、VJ、VL、D
                posed[i] != "VH" && posed[i + 1] != "VH" &&
                posed[i] != "VI" && posed[i + 1] != "VI" &&
                posed[i] != "VJ" && posed[i + 1] != "VJ" &&
                posed[i] != "VK" && posed[i + 1] != "VK" &&
                posed[i] != "VL" && posed[i + 1] != "VL" &&
                posed[i] != "D" && posed[i + 1] != "D")
            {
              seged[i] += seged[i + 1];
              seged.RemoveRange(i + 1,1);
              posed.RemoveRange(i,2);
              posed.Insert(i,"VA");
              haveHandle = true;
            }
          }
        }
        #endregion

        #region 相鄰相同詞(比對+ 比對 = 比對比對)+精簡化相鄰相同詞(好棒好棒=好棒)
        if(set.MergeSameWord == true && i + 1 < seged.Count)
        {
          if(seged[i].Length == 2 && seged[i + 1].Length == 2)
          {
            if(seged[i] == seged[i + 1] && posed[i] != "Na" && posed[i + 1] != "Na" && posed[i] != "Nc" && posed[i + 1] != "Nc")
            {
              string originalPos = posed[i];
              if(set.SameWordSimplify == false) { seged[i] += seged[i + 1]; }//如果精簡化不開啟，則疊加原來字組
              seged.RemoveRange(i + 1,1);
              posed.RemoveRange(i,2);
              posed.Insert(i,originalPos);
              haveHandle = true;
            }
          }
        }
        #endregion

        #region 量詞+疊字合併(一 + 棵棵 = 一棵棵)
        if(set.MergeSpecialNeqaWord == true && i + 1 < seged.Count)
        {
          if(seged[i].Length == 1 && seged[i + 1].Length == 2)
          {
            if(seged[i] == "一" && seged[i + 1][0] == seged[i + 1][1] && (posed[i + 1] == "Nf" || posed[i + 1] == "Neqa"))
            {
              seged[i] += seged[i + 1];
              seged.RemoveRange(i + 1,1);
              posed.RemoveRange(i,2);
              posed.Insert(i,"Neqa");
              haveHandle = true;
            }
          }
        }
        if(set.MergeSpecialNeqaWord == true && i - 1 >= 0)
        {
          if(seged[i - 1].Length == 1 && seged[i].Length == 2)
          {
            if(seged[i - 1] == "一" && seged[i][0] == seged[i][1] && (posed[i] == "Nf" || posed[i] == "Neqa"))
            {
              seged[i - 1] += seged[i];
              seged.RemoveRange(i,1);
              posed.RemoveRange(i - 1,2);
              posed.Insert(i - 1,"Neqa");
              haveHandle = true;
            }
          }
        }
        #endregion

        #region 四字合併
        //例如：忙東忙西。
        if(i + 3 < seged.Count && seged[i].Length == 1 && seged[i + 1].Length == 1 && seged[i + 2].Length == 1 && seged[i + 3].Length == 1)
        {//翻(VC)　東(Ncd)　翻(VC)　西(Ncd)
          if(seged[i] == seged[i + 2] && posed[i].First() == 'V' && posed[i + 2].First() == 'V'
            && posed[i] != "notword" && posed[i + 1] != "notword" && posed[i + 2] != "notword" && posed[i + 3] != "notword")
          {
            if(四字合併特殊字.Contains(seged[i + 1] + seged[i + 3]) == true)
            {
              seged[i] += seged[i + 1] + seged[i + 2] + seged[i + 3];
              seged.RemoveRange(i + 1,3);
              posed.RemoveRange(i,4);
              posed.Insert(i,"VA");
              haveHandle = true;
            }
          }
        }
        else if(i + 2 < seged.Count && seged[i].Length == 2 && seged[i + 1].Length == 1 && seged[i + 2].Length == 1)
        {//做東(VA)　做(VC)　西(Ncd)
          if(seged[i][0].ToString() == seged[i + 1] && posed[i].First() == 'V' && posed[i + 1].First() == 'V'
            && posed[i] != "notword" && posed[i + 1] != "notword" && posed[i + 2] != "notword")
          {
            if(四字合併特殊字.Contains(seged[i][1] + seged[i + 2]) == true)
            {
              seged[i] += seged[i + 1] + seged[i + 2];
              seged.RemoveRange(i + 1,2);
              posed.RemoveRange(i,3);
              posed.Insert(i,"VA");
              haveHandle = true;
            }
          }
        }
        #endregion

        #region "試試看"類合併
        if(i + 2 < seged.Count)//三字分開
        {
          if(seged[i].Length == 1 && seged[i + 1].Length == 1 && seged[i + 2].Length == 1
            && posed[i].First() == 'V' && posed[i + 1].First() == 'V' && posed[i] != "VH" && posed[i + 1] != "VH"
            && seged[i] == seged[i + 1] && seged[i + 2] == "看")
          {
            seged[i] += seged[i + 1] + seged[i + 2];
            seged.RemoveRange(i + 1,2);
            posed.RemoveRange(i + 1,2);
            haveHandle = true;
          }
        }

        if(i + 1 < seged.Count)//二字+一字
        {
          if(seged[i].Length == 2 && seged[i + 1].Length == 1
             && posed[i].First() == 'V' && posed[i] != "VH"
             && seged[i][0] == seged[i][1] && seged[i + 1] == "看")
          {
            seged[i] += seged[i + 1];
            seged.RemoveRange(i + 1,1);
            posed.RemoveRange(i + 1,1);
            haveHandle = true;
          }
        }
        #endregion

        #region O不O合併
        if(set.MergeO不O == true)
        {
          if(i + 2 < seged.Count && seged[i].Length == 1 && seged[i + 1].Length == 1 && seged[i + 2].Length == 1
            && posed[i] != "notword" && posed[i + 1] != "notword" && posed[i + 2] != "notword")
          {
            string word = seged[i] + seged[i + 1] + seged[i + 2];
            string pos = get.Get_O不O(set,word);
            if(pos != string.Empty)
            {
              seged[i] = word; posed[i] = pos;
              seged.RemoveRange(i + 1,2);
              posed.RemoveRange(i + 1,2);
              haveHandle = true;
            }
          }
          else if(i + 1 < seged.Count
            && ((seged[i].Length == 1 && seged[i + 1].Length == 2) || (seged[i].Length == 2 && seged[i + 1].Length == 1))
            && posed[i] != "notword" && posed[i + 1] != "notword")
          {
            string word = seged[i] + seged[i + 1];
            string pos = get.Get_O不O(set,word);
            if(pos != string.Empty)
            {
              seged[i] = word; posed[i] = pos;
              seged.RemoveRange(i + 1,1);
              posed.RemoveRange(i + 1,1);
              haveHandle = true;
            }
          }
        }
        #endregion

        #region 特殊否定詞合併
        //特殊否定詞合併: '不'+[任意詞]，例如：不能、不開心
        //[任意單字詞]+'不'+[任意單字詞]，例如：好不好、能不能
        //此為師大緊急的要求功能，未最佳化
        if(set.MergeSpecialNegativeWord == true)
        {
          //'不'+'開心'、'不'+'能'
          if(i + 1 < seged.Count && seged[i] == "不" && get.IsUnicodeBasicMultilingualPlaneChineseString(seged[i + 1]) == true)
          {
            seged[i] += seged[i + 1];
            seged.RemoveRange(i + 1,1);
            posed.RemoveRange(i,1);
            haveHandle = true;
          }

          //'好'+'不好'、'行'+'不行'、'棒'+'不'+'棒'
          if(i + 1 < seged.Count && seged[i].Length == 1 && seged[i + 1].Length == 2
             && seged[i + 1][0] == '不' && seged[i] == seged[i + 1][1].ToString())
          {
            seged[i] += seged[i + 1];
            seged.RemoveRange(i + 1,1);
            posed.RemoveRange(i,1);
            haveHandle = true;
          }

          if(i + 2 < seged.Count && seged[i].Length == 1 && seged[i + 1].Length == 1 && seged[i + 2].Length == 1
             && seged[i] == seged[i + 2] && seged[i + 1] == "不")
          {
            seged[i] += seged[i + 1] + seged[i + 2];
            seged.RemoveRange(i + 1,2);
            posed.RemoveRange(i + 2,1);
            posed.RemoveRange(i,1);
            haveHandle = true;
          }

          if(i + 1 < seged.Count && seged[i].Length == 2 && seged[i + 1].Length == 1
             && seged[i][1] == '不' && seged[i][0].ToString() == seged[i + 1])
          {
            seged[i] += seged[i + 1];
            seged.RemoveRange(i + 1,1);
            posed.RemoveRange(i,1);
            haveHandle = true;
          }
        }


        #endregion
      }
      return haveHandle;
    }

    //資料從中研院抽取經人工篩選而來
    static string[] 四字合併特殊字 = { "來去","上下","進出","東西","来去","上下","进出","东西" };

  }
}
