using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Co_occurrence
{
  class Co_occurrenceAppSpace : Co_occurrenceApp
  {
    /// <summary>
    /// 查詢指定項目的出現總次數
    /// </summary>
    /// <typeparam name="T">執行期查詢目標的資料型態</typeparam>
    /// <param name="fileData">檔案的統計資料</param>
    /// <param name="item">要查詢的項目</param>
    /// <returns>總次數值</returns>
    protected double getTotalCount<T>(Dictionary<T,string> fileData,T item)
    {
      string value;
      if(fileData.ContainsKey(item)
          && (value = getStringBetweenChar(fileData[item],pairMiddleChar,pairEndChar,0)) != null)
      {
        return Convert.ToDouble(value);
      }
      else
      {
        return 0;
      }
    }

    /// <summary>
    /// 查詢兩項目間的共現次數(Co_occurrence)
    /// </summary>
    /// <typeparam name="T">執行期查詢目標的資料型態</typeparam>
    /// <param name="fileData">檔案的統計資料</param>
    /// <param name="itemA">要查詢的項目甲</param>
    /// <param name="itemB">要查詢的項目乙</param>
    /// <returns>共現次數值</returns>
    protected double getPairCount<T>(Dictionary<T,string> fileData,T itemA,T itemB)
    {
      int itemIndex;
      string value;
      if(fileData.ContainsKey(itemA)
          && (itemIndex = fileData[itemA].IndexOf(string.Concat(pairStartChar,itemB,pairMiddleChar),1)) > -1
          && (value = getStringBetweenChar(fileData[itemA],pairMiddleChar,pairEndChar,itemIndex)) != null)
      {
        return Convert.ToDouble(value);
      }
      else
      {
        return 0;
      }
    }

    /// <summary>
    /// 取得從指定索引開始，位於兩指定符號間的子字串
    /// </summary>
    /// <param name="searchString">搜尋目標字串</param>
    /// <param name="startIndex">搜尋起始索引</param>
    /// <param name="startSymbol">前分隔符號</param>
    /// <param name="endSybol">後分隔符號</param>
    /// <returns>分割出的子字串或表示無結果的null值</returns>
    protected string getStringBetweenChar(string searchString,char startSymbol,char endSybol,int startIndex)
    {
      int beginIndex;
      int endIndex;

      if((beginIndex = searchString.IndexOf(startSymbol,startIndex)) > -1
          && beginIndex + 1 < searchString.Length
          && (endIndex = searchString.IndexOf(endSybol,beginIndex + 1)) > -1)
      {
        return searchString.Substring(beginIndex + 1,endIndex - beginIndex - 1);
      }
      else
      {
        return null;
      }
    }
  }
}
