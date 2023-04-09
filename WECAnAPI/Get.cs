using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WECAnAPI
{
  partial class Get
  {
    public Get()
    {
      corpusPath = "";
      readGet();
    }
    public Get(string _corpusPath)
    {
      corpusPath = _corpusPath + @"\";
      readGet();
    }

    Co_occurrence.Co_occurrenceAppTimeString co_stringTw;
    Co_occurrence.Co_occurrenceAppTimeString co_stringCn;
    Co_occurrence.Co_occurrenceAppTimeString SinicaCo_occ;
    Co_occurrence.CharTw co_charTw;
    Co_occurrence.CharCn co_charCn;
    string corpusPath;

    public int WordMaximumLength = 1;

    public Dictionary<string,List<string>> MPS_Mix = new Dictionary<string,List<string>>();
    public Dictionary<string,HashSet<string>> MPSBeginEndTable = new Dictionary<string,HashSet<string>>();
    Dictionary<string,List<PoS.WordPoS>> wordPoSTw = new Dictionary<string,List<PoS.WordPoS>>();
    Dictionary<string,List<PoS.WordPoS>> wordPoSCn = new Dictionary<string,List<PoS.WordPoS>>();

    Dictionary<string,double[]> biesTw = new Dictionary<string,double[]>();
    Dictionary<string,double[]> CRFtw = new Dictionary<string,double[]>();
    Dictionary<string,double[]> biesCn = new Dictionary<string,double[]>();
    Dictionary<string,double[]> CRFcn = new Dictionary<string,double[]>();

    HashSet<string> Cindy = new HashSet<string>();
    HashSet<string> Simon = new HashSet<string>();

    HashSet<string> GigaWordCn = new HashSet<string>();
    public HashSet<string> UserWord = new HashSet<string>();
    HashSet<string> IdiomTw = new HashSet<string>();
    HashSet<string> IdiomCn = new HashSet<string>();

    internal HashSet<string> nameCache;

    internal Dictionary<string,int> Chinese百家姓Tw = new Dictionary<string,int>(5647);
    internal Dictionary<string,int> ChineseName_1Tw = new Dictionary<string,int>(6261);
    internal Dictionary<string,int> ChineseName_2Tw = new Dictionary<string,int>(6826);

    internal Dictionary<string,int> Chinese百家姓Cn = new Dictionary<string,int>(5647);
    internal Dictionary<string,int> ChineseName_1Cn = new Dictionary<string,int>(6261);
    internal Dictionary<string,int> ChineseName_2Cn = new Dictionary<string,int>(6826);

    public HashSet<string> ForceMergeWord = new HashSet<string>();
    public Dictionary<string, List<string>> ForceSplitWord = new Dictionary<string, List<string>>();
    public Dictionary<string,List<List<string>>> MaximumMatchingWordTw = new Dictionary<string,List<List<string>>>();
    public Dictionary<string,List<List<string>>> MaximumMatchingWordCn = new Dictionary<string,List<List<string>>>();

    Dictionary<string,string> O不OTw = new Dictionary<string,string>();
    Dictionary<string,string> O不OCn = new Dictionary<string,string>();

    public string Get_O不O(Set set,string _string)
    {
      if(set.SimplifiedChinese == false)
      {
        return O不OTw.ContainsKey(_string) == true ? O不OTw[_string] : string.Empty;//三原運算子，?為if，:為else
      }
      else
      {
        return O不OCn.ContainsKey(_string) == true ? O不OCn[_string] : string.Empty;
      }
    }

    public double Co_occ單雙模糊BeginFre(Set set,string _string)
    {
      return (set.SimplifiedChinese == false ? co_stringTw : co_stringCn).GetPairCount("●",_string);
    }
    public double Co_occ單雙模糊PairFre(Set set,string stringA,string stringB)
    {
      return (set.SimplifiedChinese == false ? co_stringTw : co_stringCn).GetPairCount(stringA,stringB);
    }
    public double Co_occ單雙模糊EndFre(Set set,string _string)
    {
      return (set.SimplifiedChinese == false ? co_stringTw : co_stringCn).GetPairCount(_string,"●");
    }
    public double Co_occ單雙模糊TotalFre(Set set,string _string)
    {
      return (set.SimplifiedChinese == false ? co_stringTw : co_stringCn).GetTotalCount(_string);
    }

    public double Co_occSinicaWordBeginFre(string _string)
    {
      return SinicaCo_occ.GetPairCount("●",_string);//擔心在算第一個字機率的時候，第一個字前面沒字，無法算相依機率
        }
    public double Co_occSinicaWordPairFre(string stringA,string stringB)
    {
      return SinicaCo_occ.GetPairCount(stringA,stringB);
    }
    public double Co_occSinicaWordEndFre(string _string)
    {
      return SinicaCo_occ.GetPairCount(_string,"●");//與前面相同，變成最後一個字
    }
    public double Co_occSinicaWordTotalFre(string _string)
    {
      return SinicaCo_occ.GetTotalCount(_string);
    }


    public double CharPairFreq(Set set, char _charA, char _charB)
    {
        if (set.SimplifiedChinese == false)
        {
          return co_charTw.PairFreq(_charA, _charB);
        } else {
          return co_charCn.PairFreq(_charA, _charB);
        }
    }

    public double[] BIES(Set set,string word)
    {
      if(set.SimplifiedChinese == false)
      {// 繁
        return biesTw[word];
      }
      else
      {// 簡
        return biesCn[word];
      }
    }
    public bool BIESContainsKey(Set set,string word)
    {
      if(set.SimplifiedChinese == false)
      {// 繁
        return biesTw.ContainsKey(word);
      }
      else
      {// 簡
        return biesCn.ContainsKey(word);
      }
    }
    public double[] TrainingData(Set set,string word)
    {
      if(set.SimplifiedChinese == false)
      {// 繁
        return CRFtw[word];
      }
      else
      {// 簡
        return CRFcn[word];
      }
    }
    public bool TrainingDataContainsKey(Set set,string word)
    {
      if(set.SimplifiedChinese == false)
      {// 繁
        return CRFtw.ContainsKey(word);
      }
      else
      {// 簡
        return CRFcn.ContainsKey(word);
      }
    }
    public void TrainingDataAdd(Set set,string word,double[] value)
    {
      if(set.SimplifiedChinese == false)
      {// 繁
        CRFtw.Add(word,value);
      }
      else
      {// 簡
        CRFcn.Add(word,value);
      }
    }

    #region IsWord偵測是否為詞，當中包含去除詞類b專用特殊詞檢測、人名，繁簡皆有判斷式。
    public bool IsWord(Set set,string word)
    {
      if(word == "去除詞類b專用特殊詞")
      {
        return true;
      }

      if(set.UserWord == true && UserWord.Contains(word))
      {
        return true;
      }


      if(set.SimplifiedChinese == false)
      {// 繁
        //人名重新斷詞
        if(set.NameTowPassHandle == true && nameCache.Contains(word) == true)
        {
          return true;
        }

        //檢測是否為詞
        if(set.Cindy == true && Cindy.Contains(word))
        {
          return true;
        }


        if(set.Idiom == true && IdiomTw.Contains(word))
        {
          return true;
        }
      }

      if(set.SimplifiedChinese == true)
      {// 簡

        //人名重新斷詞
        if(set.NameTowPassHandle == true && nameCache.Contains(word) == true)
        {
          return true;
        }

        if(set.Cindy == true && Simon.Contains(word))
        {
          return true;
        }


        if(set.Idiom == true && IdiomCn.Contains(word))
        {
          return true;
        }

        if(set.GigaWordSimplified == true && GigaWordCn.Contains(word))
        {
          return true;
        }
      }

      return false;
    }
    #endregion

    public List<string> WordSource(Set set,string word)
    {
      List<string> source = new List<string>();

      if(set.UserWord == true && UserWord.Contains(word))
      {
        source.Add("UserWord");
      }

      if(set.SimplifiedChinese == false)
      {// 繁
        //人名重新斷詞
        if(set.NameTowPassHandle == true && nameCache.Contains(word) == true)
        {
          source.Add("WECAn-Nb");
        }
        if(set.Cindy == true && Cindy.Contains(word))
        {
          source.Add("Cindy");
        }



        if(set.Idiom == true && IdiomTw.Contains(word))
        {
          source.Add("IdiomTw");
        }
      }

      if(set.SimplifiedChinese == true)
      {// 簡
        if(set.Cindy == true && Simon.Contains(word))
        {
          source.Add("Simon");
        }



        if(set.Idiom == true && IdiomCn.Contains(word))
        {
          source.Add("IdiomCn");
        }

        if(set.GigaWordSimplified == true && GigaWordCn.Contains(word))
        {
          source.Add("GigaWordCn");
        }
      }

      return source;
    }
    public HashSet<string> WordList(Set set)
    {
      var result = new HashSet<string>();

      if(set.UserWord == true)
      {
        result.UnionWith(UserWord);
      }

      if(set.SimplifiedChinese == false)
      {// 繁
        if(set.Cindy == true)
        {
          result.UnionWith(Cindy);
        }


        if(set.Idiom == true)
        {
          result.UnionWith(IdiomTw);
        }
      }

      if(set.SimplifiedChinese == true)
      {// 簡
        if(set.Cindy == true)
        {
          result.UnionWith(Simon);
        }


        if(set.Idiom == true)
        {
          result.UnionWith(IdiomCn);
        }

        if(set.GigaWordSimplified == true)
        {
          result.UnionWith(GigaWordCn);
        }
      }

      return result;
    }

    public List<PoS.WordPoS> WordPoSList(Set set,string word)
    {
      if(set.SimplifiedChinese == false)
      {// 繁
        return wordPoSTw[word];
      }
      else
      {// 簡
        return wordPoSCn[word];
      }
    }
    public double PoSTotalFreqAtWord(Set set,string word)/*虛引數傳入想查找總頻率的字詞，藉由呼叫WordSegemnt類別的字典並返回計算後的總頻率*///某詞彙的總詞頻
    {
      if(set.SimplifiedChinese == false)
      {// 繁
        double num = 0;
        if(wordPoSTw.ContainsKey(word) == true)
        {
          foreach(PoS.WordPoS item in wordPoSTw[word])
          {
            num += item.PoSNum;
          }
        }
        return num;
      }
      else
      {// 簡
        double num = 0;
        if(wordPoSCn.ContainsKey(word) == true)
        {
          foreach(PoS.WordPoS item in wordPoSCn[word])
          {
            num += item.PoSNum;
          }
        }
        return num;
      }
    }
    public double PoSFreqAtWord(Set set,string word,string pos)/*虛引數傳入想查找頻率的字詞與詞性，藉由呼叫WordSegemnt類別的字典並返回尋找到的頻率*///某詞彙的某詞性之詞頻
    {
      if(set.SimplifiedChinese == false)
      {// 繁
        double num = 0;
        if(wordPoSTw.ContainsKey(word) == true)
        {
          foreach(PoS.WordPoS item in wordPoSTw[word])
          {
            if(item.PoS == pos && item.PoSNum > num)
            {
              num = item.PoSNum;
            }
          }
        }
        return num;
      }
      else
      {// 簡
        double num = 0;
        if(wordPoSCn.ContainsKey(word) == true)
        {
          foreach(PoS.WordPoS item in wordPoSCn[word])
          {
            if(item.PoS == pos && item.PoSNum > num)
            {
              num = item.PoSNum;
            }
          }
        }
        return num;
      }
    }
    public List<string> WordMPS(string word)
    {//虛引數傳入欲查詢的詞，查詢注音資料庫返回對應的注音陣列，若是不存在則查詢AllWord，都不存在則傳送無詞性無的標記
      if(MPS_Mix.ContainsKey(word) == true)
      {
        return MPS_Mix[word];
      }
      else
      {
        return new List<string> { "null" };
      }
    }
    public List<List<string>> WordMPS(List<string> word)
    {//虛引數傳入欲查詢的詞陣列，查詢注音資料庫返回對應的注音陣列集合，若是不存在則查詢AllWord，都不存在則傳送無詞性無的標記
      List<List<string>> result = new List<List<string>>();

      for(int index = 0; index < word.Count; index++)
      {
        result.Add(WordMPS(word[index]));
      }

      return result;
    }
    public bool IsUnicodeBasicMultilingualPlaneChineseChar(char _char)
    {
      if(0x4E00 <= _char && _char <= 0x9FFF)
      {// 4E00-9FFF 中日韓統一表意文字
        return true;
      }
      else if(0x3400 <= _char && _char <= 0x4DBF)
      {// 3400-4DBF 中日韓統一表意文字擴充功能A
        return true;
      }
      else if(0xF900 <= _char && _char <= 0xFAFF)
      {// F900-FAFF 中日韓相容表意文字
        return true;
      }
      else
      {
        return false;
      }
    }

    public bool IsUnicodeBasicMultilingualPlaneChineseString(string _string)
    {
      foreach(var ch in _string)
      {
        if(IsUnicodeBasicMultilingualPlaneChineseChar(ch) == false) { return false; }
      }
      return _string != string.Empty;
    }

    /// <summary>
    /// 清除人名快取
    /// </summary>
    public void ClearNameCache() => nameCache?.Clear();
  }
}
