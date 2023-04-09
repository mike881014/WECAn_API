using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WECAnAPI.RuleBased
{//查表合併
  static class Neu
  {
    public static bool Handle(Get get,List<string> seged,List<string> posed)
    {
      bool haveHandle = false;
      for(int forCount = 0; forCount < seged.Count(); forCount++)
      {
        if(數字s.Contains(seged[forCount])
           || (forCount + 1 < seged.Count() && (seged[forCount] + seged[forCount + 1] == "好幾" || seged[forCount] + seged[forCount + 1] == "好几"))
           || (seged[forCount].Length == 2 && 數好幾接的數字c.Contains(seged[forCount][0]) && seged[forCount][1] == '分')
           || (seged[forCount].Length == 3 && 數好幾接的數字c.Contains(seged[forCount][0]) && seged[forCount].Substring(1,2) == "分之")
           || (seged[forCount].Length > 1 && posed[forCount] == "Neu" && (forCount + 1 < seged.Count() && seged[forCount + 1] != "多") && IsNeu(seged[forCount]) == true)
           || (seged[forCount] == "一點" && posed[forCount] != "Nd" && forCount + 1 < seged.Count() && IsNeu(seged[forCount + 1])))
        {
          int startIndex = forCount;
          int endIndex = forCount;
          string str = seged[forCount];
          while(true)
          {
            //數字
            if(endIndex + 1 < seged.Count() && IsPureNumber(seged[endIndex + 1]) == true)
            {
              str = str + seged[endIndex + 1];
              endIndex += 1;
              continue;
            }
            else if(endIndex + 2 < seged.Count() && (seged[endIndex + 1] + seged[endIndex + 2]).IndexOf("分之") == 0
                    && IsPureNumber((seged[endIndex + 1] + seged[endIndex + 2]).Replace("分之","")) == true)
            {
              str = str + seged[endIndex + 1] + seged[endIndex + 2];
              endIndex += 2;
              continue;
            }
            //連接詞+數字
            else if(endIndex + 2 < seged.Count() && 連結詞s.Contains(seged[endIndex + 1]) && IsPureNumber(seged[endIndex + 2]) == true)
            {
              str = str + seged[endIndex + 1] + seged[endIndex + 2];
              endIndex += 2;
              continue;
            }
            //連接詞+數字
            else if(endIndex + 1 < seged.Count() && seged[endIndex + 1].Length == 2
                    && 連結詞c.Contains(seged[endIndex + 1][0]) && 數字c.Contains(seged[endIndex + 1][1]))
            {
              str = str + seged[endIndex + 1];
              endIndex += 1;
              continue;
            }
            //"一點"+數字
            else if(seged[endIndex] == "一點")
            {
              str = str + seged[endIndex + 1];
              endIndex += 1;
              continue;
            }
            //分之
            else if(endIndex + 3 < seged.Count() && seged[endIndex + 1] == "分" && seged[endIndex + 2] == "之" && IsPureNumber(seged[endIndex + 3]))
            {
              str = str + seged[endIndex + 1] + seged[endIndex + 2] + seged[endIndex + 3];
              endIndex += 3;
              continue;
            }
            //成
            else if(endIndex + 1 < seged.Count() && seged[endIndex + 1] == "成")
            {
              if (endIndex + 2 < seged.Count() && IsPureNumber(seged[endIndex + 2])) {
                str = str + seged[endIndex + 1] + seged[endIndex + 2];
                endIndex += 2;
              } else {
                str = str + seged[endIndex + 1];
                endIndex += 1;
              }
              continue;
            }
            else
            {
              break;
            }
          }

          //結尾詞，三千"多"
          if(endIndex + 1 < seged.Count() && 結尾詞s.Contains(seged[endIndex + 1]))
          {
            str = str + seged[endIndex + 1];
            endIndex += 1;
          }
          //特殊結尾，三十好幾
          if(endIndex + 2 < seged.Count() && str.Length == 2 && 好幾前的數字c.Contains(str[0])
            && (str[1] == '十' || str[1] == '拾')
            && seged[endIndex + 1] == "好"
            && (seged[endIndex + 2] == "幾" || seged[endIndex + 2] == "几"))
          {
            str = str + seged[endIndex + 1] + seged[endIndex + 2];
            endIndex += 2;
          }
          //前詞為前導詞
          if(startIndex - 1 > -1 && 前導詞s.Contains(seged[startIndex - 1]))
          {
            //"數"要特別檢查避免"數一數"...這類例子被斷錯
            if(seged[startIndex - 1] == "數" || seged[startIndex - 1] == "数")
            {
              if(數好幾接的數字c.Contains(str[0]) == true)
              {
                str = seged[startIndex - 1] + str;
                startIndex -= 1;
              }
            }
            else
            {
              str = seged[startIndex - 1] + str;
              startIndex -= 1;
            }
          }

          //檢查"兩"是否為結尾、是否為量詞、前後是否為特定名詞
          if(forCount - 1 >= 0 && (str.Last() == '兩' || str.Last() == '两') && 量詞兩所接名詞.Contains(seged[forCount - 1]) == true && posed[forCount - 1] == "Na")
          {
            str = str.Remove(str.Length - 1);
            endIndex--;


          }
          else if(forCount + 1 < seged.Count() && (str.Last() == '兩' || str.Last() == '两') && 量詞兩所接名詞.Contains(seged[forCount + 1]) == true && posed[forCount + 1] == "Na")
          {
            str = str.Remove(str.Length - 1);
            endIndex--;
          }

          if(endIndex - startIndex > 0)
          {
            seged.RemoveRange(startIndex,endIndex - startIndex + 1);
            posed.RemoveRange(startIndex,endIndex - startIndex + 1);
            seged.Insert(startIndex,str);
            //2016-09-12：在[?分之?]的部分標記正確為Neqa，原本要成立新類別，不過因成本考量(成立新類別只為了少數幾行程式碼)，直接納入Neu類別
            if(IsNeqa(str) == true)//Neqa有 百分之、幾分之幾
            {
              posed.Insert(startIndex,"Neqa");
            }
            else
            {
              posed.Insert(startIndex,"Neu");
            }
            haveHandle = true;
          }
        }
      }
      return haveHandle;
    }
    public static bool IsNeu(string word)
    {
      if(word == null || word.Length == 0)
      {
        return false;
      }
      else
      {
        if(word.Length == 2)
        {
          if(word == "好幾" || word == "好几")
          {
            return true;//"好幾"本身就為Neu
          }
          else if(word == "幾多" || word == "几多")
          {
            return false;//"幾多"為Neqa
          }

        }
        for(int index = 0; index < word.Length; index++)
        {
          //前導詞，第一、數十...等("數"不能單獨出現)
          if(index == 0 && 前導詞c.Contains(word[index]) && (word != "數" && word != "数"))
          {
            continue;
          }
          //純數字("幾"不能單獨出現)
          else if(數字c.Contains(word[index]) /*&& (word != "几" && word != "幾")*/)
          {
            continue;
          }
          //連接詞+數字，三千多萬
          else if(index + 1 < word.Length && 連結詞c.Contains(word[index]) && 數字c.Contains(word[index + 1]))
          {
            index += 1;
            continue;
          }
          //"數"，數百、數億
          else if(index + 1 < word.Length && (word[index] == '數' || word[index] == '数') && 數好幾接的數字c.Contains(word[index + 1]))
          {
            index += 1;
            continue;
          }
          //好幾百、好幾千
          else if(index + 2 < word.Length && (word[index] == '好' && (word[index + 1] == '幾' || word[index + 1] == '几')) && 數好幾接的數字c.Contains(word[index + 2]))
          {
            index += 2;
            continue;
          }
          //三十好幾、二十好幾
          else if(index - 2 >= 0 && index + 1 < word.Length && 好幾前的數字c.Contains(word[index - 2]) &&
                  (word[index - 1] == '十' || word[index - 1] == '拾') &&
                  (word[index] == '好' && (word[index + 1] == '幾' || word[index + 1] == '几')))
          {
            index += 2;
            continue;
          }
          //其他結尾詞
          else if(index == word.Length - 1 && 結尾詞c.Contains(word[index]))
          {
            continue;
          }
          else
          {
            return false;
          }
        }
        return true;
      }
    }

    public static bool IsNeqa(string word)
    {
      return (word.Contains("分之") || word[word.Length - 1] == '%' || word[word.Length - 1] == '％' || word.Contains("成") && word[0] != '成');
    }
    public static bool IsPureNumber(string word) => word != string.Empty && word.Count(c => 數字c.Contains(c) == false) == 0;

    #region 數詞特定範圍
    public static bool IsCentury(string word) { return IsInRange(word,10,25); }
    public static bool IsYear(string word) { return IsInRange(word,50,2300); }
    public static bool IsMonth(string word)
    {
      //月份除了檢查範圍外，另有出現兩個月中沒有分隔符號的，收錄常見的8個，例如：每年七八月是夏天
      return IsInRange(word,1,12) || IsInCases(word,12,23,34,45,56,67,78,89);
    }
    public static bool IsDate(string word) { return IsInRange(word,1,31); }
    public static bool IsHour(string word) { return IsInRange(word,0,24); }
    public static bool IsMinuteSecond(string word) { return IsInRange(word,0,59); }
    /// <summary>
    /// 某一數字是否在指定範圍內
    /// </summary>
    /// <param name="word"></param>
    /// <param name="begin"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    private static bool IsInRange(string word,int begin,int end)
    {
      char splitWord = GetSplitWord(word);
      bool isRange = (splitWord != '\0');//是否為一個範圍，例如: 1992─1993年、3~4月等
      if(isRange == true)
      {
        var range = SplitRange(word,splitWord);
        return range != null && range[0] >= begin && range[0] <= end && range[1] >= begin & range[1] <= end;
      }
      else
      {
        int number = WordToInt(word);
        return number >= begin && number <= end;
      }
    }
    /// <summary>
    /// 將一個表示範圍的文字拆開並以整數回傳其範圍
    /// </summary>
    /// <param name="word"></param>
    /// <param name="splitWord"></param>
    /// <returns></returns>
    private static int[] SplitRange(string word,char splitWord)
    {
      var tmp = word.Split(splitWord);
      if(tmp.Length == 2)
      {
        return new int[] { WordToInt(tmp[0]),WordToInt(tmp[1]) };
      }
      else
      {
        return null;
      }
    }
    /// <summary>
    /// 取得分隔的字元，例如: 50~100  則回傳~
    /// </summary>
    /// <param name="word"></param>
    /// <returns></returns>
    private static char GetSplitWord(string word)
    {
      foreach(var ch in word)
      {
        if(連結詞c.Contains(ch) == true)
        {
          return ch;
        }
      }
      return '\0';
    }
    /// <summary>
    /// 純數字轉換，不含特殊單位(如"兩千萬"將不予轉換)
    /// </summary>
    /// <param name="word"></param>
    /// <returns></returns>
    private static int WordToInt(string word)
    {
      int number = 0;
      for(int chIdx = word.Length - 1, digit = 1; chIdx >= 0; chIdx--, digit *= 10)
      {
        if(數字c.Contains(word[chIdx]) == false) { return -1; }
        int currentNumber = ToInt(word[chIdx]);
        if(currentNumber == -1) { return -1; }
        if(currentNumber >= 10) // 大於10為特殊處理
        {
          if(chIdx - 1 >= 0)
          {
            int previousNumber = ToInt(word[chIdx - 1]);
            if(currentNumber == 10 && previousNumber >= 1 && previousNumber <= 9)//一十到九十    九十二=2+90
            {
              number += 10 * previousNumber;
              chIdx--;
            }
            else
            {
              return -1;
            }
          }
          else
          {
            number += currentNumber;//十二=10+2
          }

        }
        else//其他按位數處理
        {
          number += digit * currentNumber;
        }

      }
      return number;
    }
    /// <summary>
    /// 字元轉為整數
    /// </summary>
    /// <param name="word"></param>
    /// <returns></returns>
    private static int ToInt(char word)
    {
      switch(word)
      {
        case '１': case '1': case '一': case '壹': return 1;
        case '２': case '2': case '二': case '貳': case '兩': case '两': case '贰': return 2;
        case '３': case '3': case '三': case '参': case '叁': return 3;
        case '４': case '4': case '四': case '肆': return 4;
        case '５': case '5': case '五': case '伍': return 5;
        case '６': case '6': case '六': case '陸': case '陆': return 6;
        case '７': case '7': case '七': case '柒': return 7;
        case '８': case '8': case '八': case '捌': return 8;
        case '９': case '9': case '九': case '玖': return 9;
        case '０': case '0': case '零': case '○': return 0;
        case '十': case '拾': return 10;
        case '廿': return 20;
        case '卅': return 30;
        case '卌': return 40;
        default: return -1;
      }
    }
    /// <summary>
    /// 某一數字是否在指定集合中
    /// </summary>
    /// <param name="word"></param>
    /// <param name="numbers"></param>
    /// <returns></returns>
    private static bool IsInCases(string word,params int[] numbers)
    {
      return numbers.Contains(WordToInt(word));
    }
    #endregion

    //!修改時請注意繁體簡體
    static char[] 前導詞c = { '第','數','第','数' };
    static string[] 前導詞s = { "第","數","第","数" };

    static char[] 數好幾接的數字c = { '十','拾','百','佰','千','仟','萬','億','兆','十','拾','百','佰','千','仟','万','亿','兆' };

    static string[] 數好幾接的數字s = { "十","拾","百","佰","千","仟","萬","億","兆","十","拾","百","佰","千","仟","万","亿","兆" };


    static char[] 數字c = {'０','１','２','３','４','５','６','７','８','９','0','1','2','3','4','5','6','7','8','9',
                           '○','零','一','二','兩','三','四','五','六','七','八','九','十','壹','貳','参','肆','伍',
                            '陸','柒','捌','玖','拾','百','佰','千','仟','萬','億','兆','廿','卅','卌','幾',
                          '○','零','一','二','两','三','四','五','六','七','八','九','十','壹','贰','参','叁','肆','伍',
                           '陆','柒','捌','玖','拾','百','佰','千','仟','万','亿','兆','廿','卅','卌','几'};
    static string[] 數字s = {"０","１","２","３","４","５","６","７","８","９",
                            "0","1","2","3","4","5","6","7","8","9",
                            "○","零","一","二","兩","三","四","五","六","七","八","九","十","壹","貳","参","肆","伍",
                            "陸","柒","捌","玖","拾","百","佰","千","仟","萬","億","兆","廿","卅","卌","幾",
                            "○","零","一","二","两","三","四","五","六","七","八","九","十","壹","贰","参","叁","肆","伍",
                            "陆","柒","捌","玖","拾","百","佰","千","仟","万","亿","兆","廿","卅","卌","几"};

    static char[] 好幾前的數字c = {'一','二','兩','三','四','五','六','七','八','九','壹','貳','叁','肆','伍','陸','柒','捌','玖',
                                  '一','二','两','三','四','五','六','七','八','九','壹','贰','参','叁','肆','伍','陆','柒','捌','玖'};

    static string[] 好幾前的數字s = {"一","二","兩","三","四","五","六","七","八","九","壹","貳","叁","肆","伍","陸","柒","捌","玖",
                                    "一","二","两","三","四","五","六","七","八","九","壹","贰","参","叁","肆","伍","陆","柒","捌","玖"};

    static char[] 連結詞c = {'/','／','﹒','．','。','.','‧','-','—',
                            '之','點',
                            '之','点',
                            '餘','余','多',
                            '馀','余','多'};
    static string[] 連結詞s = {"/","／","﹒","．","。",".","‧","-","—",
                              "之","點",
                              "之","点",
                              "餘","余","多",
                              "馀","余","多"   };

    static char[] 結尾詞c = {'%','％',
                            '餘','余','多',
                            '馀','余','多'};
    static string[] 結尾詞s = {"%","％",
                               "餘","余","多",
                               "馀","余","多"};
    //目前只收錄有在中研院平衡語料出現過的且經過人工初步篩選。
    static string[] 量詞兩所接名詞 = { "錢"," 酒","白銀","銀子","黃金","銀","糧食","蝦仁","肉類","香薷","草決明","酸梅","仙查片","北茵藤","鳳尾草","蟹肉","售價","租金","金銀花","苦參","白蘚皮","益母草" 
       //由office word 繁體轉簡體，去掉重複的
      ,"钱","白银","银子","黄金","银","粮食","虾仁","肉类","草决明","凤尾草","售价","金银花","苦参","白藓皮" };

  }
}
