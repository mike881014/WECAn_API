using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Co_occurrence
{
  /// <summary>
  /// 取得共現次數 (Co_occurrence) 的類別
  /// </summary>
  class Co_occurrenceAppTimeString : Co_occurrenceAppTime
  {
    /// <summary>
    /// 表示自我總次數的符號
    /// </summary>
    private string totalSymbol;

    /// <summary>
    /// 檔案的統計資料
    /// </summary>
    private Dictionary<string,Dictionary<string,double>> fileData;

    /// <summary>
    /// <see cref="Co_occurrenceAppTimeString"/> 的建構子
    /// </summary>
    public Co_occurrenceAppTimeString(string filePath)
    {
      totalSymbol = stringTotalSymbol;
      fileData = new Dictionary<string,Dictionary<string,double>>();
      char[] splitChar = { mainKeyStartChar,pairStartChar,pairMiddleChar,pairEndChar };

      System.IO.StreamReader reader
          = new System.IO.StreamReader(filePath,System.Text.Encoding.Unicode);

      string readLine;
      while((readLine = reader.ReadLine()) != null)
      {
        if(readLine[0] != mainKeyStartChar)
        {
          throw new FormatException(readLine);
        }

        string[] split = readLine.Split(splitChar,StringSplitOptions.RemoveEmptyEntries);
        switch(split.Count())
        {
          case 2:
            SetValue(fileData,split[0],totalSymbol,Convert.ToDouble(split[1]));
            break;
          case 3:
            SetValue(fileData,split[0],split[1],Convert.ToDouble(split[2]));
            break;
          default:
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
      return getPairCount(fileData,item,totalSymbol);
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
