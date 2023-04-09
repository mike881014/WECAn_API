using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Co_occurrence
{
  class Co_occurrence
  {
    /// <summary>
    /// 主項分隔字元，表示開頭
    /// </summary>
    protected char mainKeyStartChar { get; } = '\u0001';

    /// <summary>
    /// 連結項分隔字元，表示開頭
    /// </summary>
    protected char pairStartChar { get; } = '\u0002';

    /// <summary>
    /// 連結項分隔字元，表示中間
    /// </summary>
    protected char pairMiddleChar { get; } = '\u0003';

    /// <summary>
    /// 連結項分隔字元，表示結束
    /// </summary>
    protected char pairEndChar { get; } = '\u0004';

    /// <summary>
    /// 表示字元資料型態自我總次數的符號
    /// </summary>
    protected char charTotalSymbol = '\0';

    /// <summary>
    /// 表示字串資料型態自我總次數的符號
    /// </summary>
    protected string stringTotalSymbol = "";

    /// <summary>
    /// 查詢兩層 <typeparamref name="Dictionary"/> 容器是否含有指定的 key
    /// </summary>
    /// <typeparam name="T0">外層 <typeparamref name="Dictionary"/> key 的泛型資料型態</typeparam>
    /// <typeparam name="T1">內層 <typeparamref name="Dictionary"/> key 的泛型資料型態</typeparam>
    /// <typeparam name="T2">內層 <typeparamref name="Dictionary"/> value 的泛型資料型態</typeparam>
    /// <param name="dictionary">查詢的目標容器</param>
    /// <param name="key0">外層的查詢 key</param>
    /// <param name="key1">內層的查詢 key</param>
    /// <returns></returns>
    protected bool ContainsKey<T0, T1, T2>(Dictionary<T0,Dictionary<T1,T2>> dictionary,T0 key0,T1 key1)
    {
      if(dictionary.ContainsKey(key0) == true && dictionary[key0].ContainsKey(key1) == true)
      {
        return true;
      }
      else
      {
        return false;
      }
    }

    /// <summary>
    /// 附加指定的 key 與 value 到兩層 <typeparamref name="Dictionary"/> 容器
    /// </summary>
    /// <typeparam name="T0">外層 <typeparamref name="Dictionary"/> key 的泛型資料型態</typeparam>
    /// <typeparam name="T1">內層 <typeparamref name="Dictionary"/> key 的泛型資料型態</typeparam>
    /// <param name="dictionary">寫入的目標容器</param>
    /// <param name="key0">寫入外層的 key</param>
    /// <param name="key1">寫入內層的 key</param>
    /// <param name="value">寫入內層的 value</param>
    protected void SetValue<T0, T1>(Dictionary<T0,Dictionary<T1,int>> dictionary,T0 key0,T1 key1,int value)
    {
      if(ContainsKey(dictionary,key0,key1))
      {
        dictionary[key0][key1] += value;
      }
      else
      {
        if(dictionary.ContainsKey(key0) == false)
        {
          dictionary.Add(key0,new Dictionary<T1,int>());
        }
        dictionary[key0].Add(key1,value);
      }
    }

    /// <summary>
    /// 附加指定的 key 與 value 到兩層 <typeparamref name="Dictionary"/> 容器
    /// </summary>
    /// <typeparam name="T0">外層 <typeparamref name="Dictionary"/> key 的泛型資料型態</typeparam>
    /// <typeparam name="T1">內層 <typeparamref name="Dictionary"/> key 的泛型資料型態</typeparam>
    /// <param name="dictionary">寫入的目標容器</param>
    /// <param name="key0">寫入外層的 key</param>
    /// <param name="key1">寫入內層的 key</param>
    /// <param name="value">寫入內層的 value</param>
    protected void SetValue<T0, T1>(Dictionary<T0,Dictionary<T1,double>> dictionary,T0 key0,T1 key1,double value)
    {
      if(ContainsKey(dictionary,key0,key1))
      {
        dictionary[key0][key1] += value;
      }
      else
      {
        if(dictionary.ContainsKey(key0) == false)
        {
          dictionary.Add(key0,new Dictionary<T1,double>());
        }
        dictionary[key0].Add(key1,value);
      }
    }
  }

  class CharTw
  {
    public CharTw(string filePath)
    {
      this.filePath = filePath;
    }
    string filePath;
    Dictionary<char, Dictionary<char, int>> lineSet = null;
    char[] freqWithSep = new char[]{ ' ' };
    void readCharList()
    {
      lineSet = new Dictionary<char, Dictionary<char, int>>();
      foreach (string line in WECAnAPI.Table.ReadCommentFileToArray(filePath, System.Text.Encoding.Unicode, new string[] { "\r", "\n" }, new char[] { ';' }))
      {
        char first = line[0];
        int firstSep = line.IndexOf(' ');
        Dictionary<char, int> freqWith = null;
        if (lineSet.ContainsKey(first) == false) {
          lineSet[first] = freqWith = new Dictionary<char, int>();
          freqWith['_'] = int.Parse(line.Substring(2, firstSep - 2));
        } else {
          throw new Exception("發現重複共現資料。");
        }
        foreach (string pair in line.Substring(firstSep).Split(freqWithSep, StringSplitOptions.RemoveEmptyEntries)) {
          freqWith[pair[0]] = int.Parse(pair.Substring(1));
        }
      }
    }
    public double PairFreq(char _charA, char _charB)
    {
      if (lineSet == null) { readCharList(); }

      if (lineSet.ContainsKey(_charA) == true) {
        Dictionary<char, int> freqWith = lineSet[_charA];
        if (freqWith.ContainsKey(_charB) == true) {
          return freqWith[_charB];
        }
        return 0;
      }
      return 0;
    }
    public double TotalFreq(char _char)
    {
      if (lineSet == null) { readCharList(); }

      if (lineSet.ContainsKey(_char) == true) {
        Dictionary<char, int> freqWith = lineSet[_char];
        if (freqWith.ContainsKey('_') == true) {
          return freqWith['_'];
        }
        return 0;
      }
      return 0;
    }
  }

  class CharCn
  {
    public CharCn(string filePath)
    {
      this.filePath = filePath;
    }
    string filePath;
    Dictionary<char, Dictionary<char, int>> lineSet = null;
    char[] freqWithSep = new char[]{ ' ' };
    void readCharList()
    {
      lineSet = new Dictionary<char, Dictionary<char, int>>();
      foreach (string line in WECAnAPI.Table.ReadCommentFileToArray(filePath, System.Text.Encoding.Unicode, new string[] { "\r", "\n" }, new char[] { ';' }))
      {
        char first = line[0];
        int firstSep = line.IndexOf(' ');
        Dictionary<char, int> freqWith = null;
        if (lineSet.ContainsKey(first) == false) {
          lineSet[first] = freqWith = new Dictionary<char, int>();
          freqWith['_'] = int.Parse(line.Substring(2, firstSep - 2));
        } else {
          throw new Exception("發現重複共現資料。");
        }
        foreach (string pair in line.Substring(firstSep).Split(freqWithSep, StringSplitOptions.RemoveEmptyEntries)) {
          freqWith[pair[0]] = int.Parse(pair.Substring(1));
        }
      }
    }
    public double PairFreq(char _charA, char _charB)
    {
      if (lineSet == null) { readCharList(); }

      if (lineSet.ContainsKey(_charA) == true) {
        Dictionary<char, int> freqWith = lineSet[_charA];
        if (freqWith.ContainsKey(_charB) == true) {
          return freqWith[_charB];
        }
        return 0;
      }
      return 0;
    }
    public double TotalFreq(char _char)
    {
      if (lineSet == null) { readCharList(); }

      if (lineSet.ContainsKey(_char) == true) {
        Dictionary<char, int> freqWith = lineSet[_char];
        if (freqWith.ContainsKey('_') == true) {
          return freqWith['_'];
        }
        return 0;
      }
      return 0;
    }
  }
}
