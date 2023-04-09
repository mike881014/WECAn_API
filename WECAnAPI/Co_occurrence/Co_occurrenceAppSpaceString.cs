using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Co_occurrence
{
  /// <summary>
  /// 取得共現次數 (Co_occurrence) 的類別
  /// </summary>
  class Co_occurrenceAppSpaceString : Co_occurrenceAppSpace
  {
    /// <summary>
    /// 表示自我總次數的符號
    /// </summary>
    private string totalSymbol;

    /// <summary>
    /// 檔案的統計資料
    /// </summary>
    private Dictionary<string,string> fileData;

    /// <summary>
    /// <see cref="Co_occurrenceAppSpaceString"/> 的建構子
    /// </summary>
    public Co_occurrenceAppSpaceString(string filePath)
    {
      totalSymbol = stringTotalSymbol;
      fileData = new Dictionary<string,string>();

      System.IO.StreamReader reader
          = new System.IO.StreamReader(filePath,System.Text.Encoding.Unicode);

      string readLine;
      while((readLine = reader.ReadLine()) != null)
      {
        string mainKey;
        if(readLine[0] == pairStartChar
            && (mainKey = getStringBetweenChar(readLine,pairStartChar,pairMiddleChar,0)) != null
            && mainKey.Length > 0)
        {
          fileData.Add(mainKey,readLine);
        }
        else
        {
          throw new FormatException(readLine);
        }
      }
      reader.Close();

    }

    /// <summary>
    /// 查詢指定項目的出現總次數
    /// </summary>
    /// <param name="item">要查詢的項目</param>
    /// <returns>總次數值</returns>
    public double GetTotalCount(string item)
    {
      return getTotalCount(fileData,item);
    }

    /// <summary>
    /// 查詢兩項目間的共現次數(Co_occurrence)
    /// </summary>
    /// <param name="itemA">要查詢的項目甲</param>
    /// <param name="itemB">要查詢的項目乙</param>
    /// <returns>共現次數值</returns>
    public double GetPairCount(string itemA,string itemB)
    {
      return getPairCount(fileData,itemA,itemB);
    }
  }
}
