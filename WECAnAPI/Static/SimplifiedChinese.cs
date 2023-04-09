using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic;

namespace WECAnAPI
{//繁簡
  public static class SimplifiedChinese
  {
    public static void ToSimplifiedChinese(List<string> stringSet)
    {
      for(int forCount = 0; forCount < stringSet.Count(); forCount++)
      {
        stringSet[forCount] = ToSimplifiedChinese(stringSet[forCount]);
      }
    }
    public static string ToSimplifiedChinese(string _string)
    {
      return Strings.StrConv(_string,VbStrConv.SimplifiedChinese,2052);
    }
    public static string ToTraditionalChinese(string _string)
    {
      return Strings.StrConv(_string,VbStrConv.TraditionalChinese,2052);
    }
  }
}
