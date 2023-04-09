using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Co_occurrence
{
  class Co_occurrenceAppTime : Co_occurrenceApp
  {
    /// <summary>
    /// 查詢兩項目間的共現次數(Co_occurrence)
    /// </summary>
    /// <typeparam name="T">執行期查詢目標的資料型態</typeparam>
    /// <param name="fileData">檔案的統計資料</param>
    /// <param name="itemA">要查詢的項目甲</param>
    /// <param name="itemB">要查詢的項目乙</param>
    /// <returns>共現次數值</returns>
    protected double getPairCount<T>(Dictionary<T,Dictionary<T,double>> fileData,T itemA,T itemB)
    {
      if(ContainsKey(fileData,itemA,itemB) == true)
      {
        return fileData[itemA][itemB];
      }
      else
      {
        return 0;
      }
    }
  }
}
