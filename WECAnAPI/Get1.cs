using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WECAnAPI
{
  partial class Get
  {
    void readGet()
    {
      readDictionary();
      readTable();
      ReadNameData();
    }
    public void readTable()
    {
      System.IO.StreamReader reader;
      string line;
      int count;

      if(System.IO.File.Exists(corpusPath + @"WECAnUser\ForceMergeWord.txt") == true) {
        foreach(string commentLine in Table.ReadCommentFileToArray(corpusPath + @"WECAnUser\ForceMergeWord.txt",System.Text.Encoding.Unicode,new string[] { "\r","\n"," ","\t" },new char[] { ';' }))
        {
          if (commentLine.Length < 2) {
            continue;
          }
          ForceMergeWord.Add(commentLine);
        }
      }

      if(System.IO.File.Exists(corpusPath + @"WECAnUser\ForceSplitWord.txt") == true) {
        foreach(string commentLine in Table.ReadCommentFileToArray(corpusPath + @"WECAnUser\ForceSplitWord.txt",System.Text.Encoding.Unicode,new string[] { "\r","\n"," ","\t" },new char[] { ';' }))
        {
          // var list = commentLine.Split(new char[] { '+' },StringSplitOptions.RemoveEmptyEntries).ToList();
          var list = Table.SplitAndEscape(commentLine, new char[] { '+' });
          var word = string.Join("", list);
          if(ForceSplitWord.ContainsKey(word) == false)
          {
            ForceSplitWord[word] = list;
          }
        }
      }

      foreach(string commentLine in Table.ReadCommentFileToArray(corpusPath + @"WECAnCorpus\MaximumMatchingWordTw.txt",System.Text.Encoding.Unicode,new string[] { "\r","\n"," ","\t" },new char[] { ';' }))
      {
        if(MaximumMatchingWordTw.ContainsKey(commentLine.Replace("+","")) == false)
        {
          MaximumMatchingWordTw.Add(commentLine.Replace("+",""),new List<List<string>>());
        }

        MaximumMatchingWordTw[commentLine.Replace("+","")].Add(commentLine.Split(new char[] { '+' },StringSplitOptions.RemoveEmptyEntries).ToList());

      }

      foreach(string commentLine in Table.ReadCommentFileToArray(corpusPath + @"WECAnCorpus\MaximumMatchingWordCn.txt",System.Text.Encoding.Unicode,new string[] { "\r","\n"," ","\t" },new char[] { ';' }))
      {
        if(MaximumMatchingWordCn.ContainsKey(commentLine.Replace("+","")) == false)
        {
          MaximumMatchingWordCn.Add(commentLine.Replace("+",""),new List<List<string>>());
        }
        MaximumMatchingWordCn[commentLine.Replace("+","")].Add(commentLine.Split(new char[] { '+' },StringSplitOptions.RemoveEmptyEntries).ToList());
      }

      //單雙模糊Gigaword共現資料
      // 注意：此共現詞頻資料只有繁/簡的單雙模糊清單
      co_stringTw = new Co_occurrence.Co_occurrenceAppTimeString(corpusPath + @"WECAnCorpus\Co_occurrenceWordTimeMaximumMatchingWordMixTw.txt");
      co_stringCn = new Co_occurrence.Co_occurrenceAppTimeString(corpusPath + @"WECAnCorpus\Co_occurrenceWordTimeMaximumMatchingWordMixCn.txt");
      //平衡語料全詞共現資料
      SinicaCo_occ = new Co_occurrence.Co_occurrenceAppTimeString(corpusPath + @"WECAnCorpus\Co_occurrenceWordTw.txt");

      co_charTw = new Co_occurrence.CharTw(corpusPath + @"WECAnCorpus\Co_occurrenceCharTw.txt");
      co_charCn = new Co_occurrence.CharCn(corpusPath + @"WECAnCorpus\Co_occurrenceCharCn.txt");

      count = 0;
      reader = new System.IO.StreamReader(corpusPath + @"WECAnCorpus\ConditionRandomFieldTw.txt",System.Text.Encoding.Unicode);
      while((line = reader.ReadLine()) != null)
      {
        if(line.Length > 0 && line[0] != '#')//#表示註解
        {
          if(count < 12)/*前面12個是BIES連結BIES的機率*///前12個是BIES
          {
            string[] reg = line.Split(',');
            biesTw.Add(reg[0],new double[] { Convert.ToDouble(reg[1]),Convert.ToDouble(reg[2]),Convert.ToDouble(reg[3]),Convert.ToDouble(reg[4]) });
            count++;
          }
          else/*建置各字元以BIES出現的機率*/
          {
            string[] reg = line.Split(',');
            CRFtw.Add(reg[0],new double[] { Convert.ToDouble(reg[1]),Convert.ToDouble(reg[2]),Convert.ToDouble(reg[3]),Convert.ToDouble(reg[4]) });
          }
        }
      }
      reader.Close();

      count = 0;
      reader = new System.IO.StreamReader(corpusPath + @"WECAnCorpus\ConditionRandomFieldCn.txt",System.Text.Encoding.Unicode);
      while((line = reader.ReadLine()) != null)
      {
        if(line.Length > 0 && line[0] != '#')//#表示註解
        {
          if(count < 12)/*前面12個是BIES連結BIES的機率*///前12個是BIES
          {
            string[] reg = line.Split(',');
            biesCn.Add(reg[0],new double[] { Convert.ToDouble(reg[1]),Convert.ToDouble(reg[2]),Convert.ToDouble(reg[3]),Convert.ToDouble(reg[4]) });
            count++;
          }
          else/*建置各字元以BIES出現的機率*/
          {
            string[] reg = line.Split(',');
            CRFcn.Add(reg[0],new double[] { Convert.ToDouble(reg[1]),Convert.ToDouble(reg[2]),Convert.ToDouble(reg[3]),Convert.ToDouble(reg[4]) });
          }
        }
      }
      reader.Close();



      foreach(string data in GetTwO不O())
      {
        string[] partitions = data.Split('\t');
        if(partitions.Length != 2 || PoSTw.IsPoS(partitions[1]) == false) { throw new System.IO.InvalidDataException($"O不O資料錯誤"); }
        O不OTw.Add(partitions[0],partitions[1]);
      }


      foreach(string data in GetCnO不O())
      {
        string[] partitions = data.Split('\t');
        if(partitions.Length != 2 || PoSTw.IsPoS(partitions[1]) == false) { throw new System.IO.InvalidDataException($"O不O資料錯誤"); }
        O不OCn.Add(partitions[0],partitions[1]);
      }




    }

    private Dictionary<string,List<PoS.WordPoS>> modification_Tw = new Dictionary<string,List<PoS.WordPoS>>();
    private Dictionary<string,List<PoS.WordPoS>> modification_Cn = new Dictionary<string,List<PoS.WordPoS>>();

    #region 讀取字典的介面
    public void readDictionary()
    {
      readDictionaryWordDataModification(corpusPath + @"WECAnCorpus\Modification_Tw.txt",modification_Tw);
      readDictionaryWordDataModification(corpusPath + @"WECAnCorpus\Modification_Cn.txt",modification_Cn);
      wordPoSTw.Add("去除詞類b專用特殊詞",去除詞類b專用特殊詞繁體());
      wordPoSCn.Add("去除詞類b專用特殊詞",去除詞類b專用特殊詞簡體());
      readDictionaryWordData(corpusPath + @"WECAnCorpus\MPS_Mix.txt",null,null,null);

      readDictionaryWordData(corpusPath + @"WECAnCorpus\Cindy.txt",wordPoSTw,Cindy,modification_Tw);
      FilterLowFreqPosTw(ref wordPoSTw);

      readDictionaryWordData(corpusPath + @"WECAnCorpus\Simon.txt",wordPoSCn,Simon,modification_Cn);
      FilterLowFreqPosCn(ref wordPoSCn);

      readDictionaryWordData(corpusPath + @"WECAnCorpus\GigaWordCn.txt",wordPoSCn,GigaWordCn,modification_Cn);
      refreshUserWord();
      readDictionaryWordData(corpusPath + @"WECAnCorpus\IdiomTw.txt",wordPoSTw,IdiomTw,modification_Tw);
      readDictionaryWordData(corpusPath + @"WECAnCorpus\IdiomCn.txt",wordPoSCn,IdiomCn,modification_Cn);

      成語與諺語名稱擴展(IdiomTw,wordPoSTw);
      成語與諺語名稱擴展(IdiomCn,wordPoSCn);
      modification_Tw.Clear(); modification_Cn.Clear();
      modification_Tw = null; modification_Cn = null;
    }
    #endregion

    public void refreshUserWord () {
      if(System.IO.File.Exists(corpusPath + @"WECAnUser\UserWord.txt") == true)
      {
        readDictionaryWordData(corpusPath + @"WECAnUser\UserWord.txt",wordPoSTw,UserWord,null);
        readDictionaryWordData(corpusPath + @"WECAnUser\UserWord.txt",wordPoSCn,UserWord,null);
      }
    }
    
    #region 名字資料載入
    public void ReadNameData()
    {
      nameCache = new HashSet<string>();

      #region Chinese


      foreach(string name in Nb.getChineseName_0())
      {
        string[] split = name.Split('\t');
        if(int.TryParse(split.Last(),out int freq) == true)
        {
          Chinese百家姓Tw.Add(split.First(),freq);
          string simplifiedWord = SimplifiedChinese.ToSimplifiedChinese(split.First());
          if(Chinese百家姓Cn.ContainsKey(simplifiedWord) == false)
          { Chinese百家姓Cn.Add(simplifiedWord,freq); }
          else
          { Chinese百家姓Cn[simplifiedWord] += freq; }
        }
      }
      foreach(string name in Nb.getChineseName_1())
      {
        string[] split = name.Split('\t');
        if(int.TryParse(split.Last(),out int freq) == true)
        {
          ChineseName_1Tw.Add(split.First(),freq);
          string simplifiedWord = SimplifiedChinese.ToSimplifiedChinese(split.First());
          if(ChineseName_1Cn.ContainsKey(simplifiedWord) == false)
          { ChineseName_1Cn.Add(simplifiedWord,freq); }
          else
          { ChineseName_1Cn[simplifiedWord] += freq; }
        }
      }
      foreach(string name in Nb.getChineseName_2())
      {
        string[] split = name.Split('\t');
        if(int.TryParse(split.Last(),out int freq) == true)
        {
          ChineseName_2Tw.Add(split.First(),freq);
          string simplifiedWord = SimplifiedChinese.ToSimplifiedChinese(split.First());
          if(ChineseName_2Cn.ContainsKey(simplifiedWord) == false)
          { ChineseName_2Cn.Add(simplifiedWord,freq); }
          else
          { ChineseName_2Cn[simplifiedWord] += freq; }
        }
      }
      #endregion


    }
    #endregion

    void 成語與諺語名稱擴展(HashSet<string> Idiom,Dictionary<string,List<PoS.WordPoS>> wordPoS)
    {//這個擴展的意思是?
      foreach(string word in Idiom.ToArray())
      {
        foreach(string nameExpansionItem in IdiomExpansion(word))
        {
          if(nameExpansionItem.Length > 1)
          {
            if(Idiom.Contains(nameExpansionItem) == false)
            {
              Idiom.Add(nameExpansionItem);
            }
            if(wordPoS.ContainsKey(nameExpansionItem) == false)
            {
              wordPoS.Add(nameExpansionItem,wordPoS[word]);
            }
          }
        }
      }
    }
    
    #region 讀入CInDy，Simon等語料
    void readDictionaryWordData(string filePath,Dictionary<string,List<PoS.WordPoS>> wordPoS,HashSet<string> hashSet,Dictionary<string,List<PoS.WordPoS>> modifyWordPos)
    {
      foreach(string line in Table.ReadCommentFileToArray(filePath,System.Text.Encoding.Unicode,new string[] { "\r","\n"," ","\t" },new char[] { ';' }))
      {
        // string[] splitLine = line.Split(new char[] { ':' },StringSplitOptions.RemoveEmptyEntries);
        var splitLine = Table.SplitAndEscape(line, new char[] { ':' });
        List<PoS.WordPoS> WordPoSList = new List<PoS.WordPoS>();

        if(splitLine.Count > 2 || splitLine.Count < 1)
        {
          throw new System.FormatException("\n\n" + filePath + "\n無法辨識:\"" + line + "\"\n\n");
        }
        else if(splitLine.Count == 1)
        {
        }
        else if(splitLine.Count == 2)
        {
          foreach(string item in splitLine[1].Split(new char[] { ',' },StringSplitOptions.RemoveEmptyEntries))
          {
            if(item.ToLower().IndexOf("mps") == 0)
            {
              if(MPS_Mix.ContainsKey(splitLine[0]) == false)
              {
                string[] stringMPS;
                List<string> mpsList;

                if(item.Contains('=') == true &&
                    (stringMPS = (item.Split(new char[] { '=' },StringSplitOptions.RemoveEmptyEntries))).Count() == 2 &&
                    stringMPS[1].ToLower().Contains("nomps") == false &&

                    (
                     splitLine[0].Length == (mpsList = (stringMPS[1].Split(new char[] { '　' },StringSplitOptions.RemoveEmptyEntries)).ToList()).Count() ||
                    splitLine[0].Length == 1 && mpsList.Count() > 1
                    )

                    )
                {
                  foreach(string charMPS in mpsList)
                  {// 注音符號不明
                    foreach(char mps in charMPS)
                    {
                      if(isMPS(mps) == false)
                      {
                        throw new System.FormatException("\n\n" + filePath + "\n無法辨識:\"" + line + "\"\n\n");
                      }
                    }
                  }
                  string word = splitLine[0];
                  MPS_Mix.Add(word,mpsList);
                  if (word.Length == 1 && mpsList.Count() > 1) {// 破音字
                    // for (int i = mpsList.Count() - 1; i >= 0; i--) {
                    //   if (MPSBeginEndTable.ContainsKey(mpsList[i]) == false) {
                    //     MPSBeginEndTable[mpsList[i]] = new HashSet<string>();
                    //   }
                    //   MPSBeginEndTable[mpsList[i]].Add(word);
                    // }
                  } else if (word.Length == 1) {// 單一字
                    // 
                  } else {
                    string key = string.Format("{0}　{1}", mpsList[0], mpsList[mpsList.Count - 1]);
                    if (MPSBeginEndTable.ContainsKey(key) == false) {
                      MPSBeginEndTable[key] = new HashSet<string>();
                    }
                    MPSBeginEndTable[key].Add(word);
                  }
                }
                else
                {// mps格式不完整
                  if(item.ToLower().Contains("nomps") == true || splitLine[0].Contains("釕铞") == true)
                  {
                    continue;
                  }
                  else
                  {
                    throw new System.FormatException("\n\n" + filePath + "\n無法辨識:\"" + line + "\"\n\n");
                  }
                }
              }
            }
            else if(item.ToLower().IndexOf("strokes") == 0)
            {
            }
            else
            {
              string[] stringPoS;
              if(item.Contains('=') == true &&
                      (stringPoS = (item.Split(new char[] { '=' },StringSplitOptions.RemoveEmptyEntries))).Count() == 2)
              {// 詞類有數值
                if(PoS.IsPoS(stringPoS[0]) == false)
                {
                  throw new System.FormatException("\n\n" + filePath + "\n無法辨識:\"" + line + "\"\n\n");
                }
                double PoSValue;
                try
                {
                  PoSValue = double.Parse(stringPoS[1]);
                }
                catch(Exception)
                {
                  throw new System.FormatException("\n\n" + filePath + "\n無法辨識:\"" + line + "\"\n\n");
                }
                WordPoSList.Add(new PoS.WordPoS(
                   stringPoS[0],
              Math.Max(1,PoSValue)
                ));
              }
              else
              {// 詞類沒數值
                if(PoS.IsPoS(item) == false)
                {
                  throw new System.FormatException("\n\n" + filePath + "\n無法辨識:\"" + line + "\"\n\n");
                }
                WordPoSList.Add(new PoS.WordPoS(item,1));
              }
            }
          }
        }
        else
        {
          throw new NotSupportedException(line);
        }

        if(hashSet != null)
        {
          if(hashSet.Contains(splitLine[0]) == false)
          {
            if(modifyWordPos == null
             || modifyWordPos.ContainsKey(splitLine[0]) == false //不存在修正資料→新增
             || (modifyWordPos.ContainsKey(splitLine[0]) == true && modifyWordPos[splitLine[0]] != null))//存在於修正資料且不為空→新增

            {
              hashSet.Add(splitLine[0]);
            }
          }

          if(modifyWordPos == null || modifyWordPos.ContainsKey(splitLine[0]) == false)//不存在修正資料→新增
          {
            if(wordPoS.ContainsKey(splitLine[0]) == false)
            {
              if(splitLine[0].Length > WordMaximumLength)
              {
                WordMaximumLength = splitLine[0].Length;
              }
              if(WordPoSList.Count == 0)
              {
                WordPoSList.Add(new PoS.WordPoS("b",1));
              }
              wordPoS.Add(splitLine[0],WordPoSList);
            }
          }
          else if(modifyWordPos.ContainsKey(splitLine[0]) == true && modifyWordPos[splitLine[0]] != null)//存在修正資料且不為空→修正
          {
            wordPoS.Remove(splitLine[0]);
            if(splitLine[0].Length > WordMaximumLength)
            {
              WordMaximumLength = splitLine[0].Length;
            }
            WordPoSList.Clear();
            foreach(var posData in modifyWordPos[splitLine[0]])
            {
              WordPoSList.Add(new PoS.WordPoS(posData.PoS,posData.PoSNum));
            }

            if(WordPoSList.Count == 0) { WordPoSList.Add(new PoS.WordPoS("b",1)); }

            wordPoS.Add(splitLine[0],WordPoSList);
          }


        }
      }
    }
    #endregion
    
    #region 讀出路經中之修改檔(使用者修改檔)，並加以修改與料
    void readDictionaryWordDataModification(string filePath,Dictionary<string,List<PoS.WordPoS>> wordPoS)
    {
      foreach(string line in Table.ReadCommentFileToArray(filePath,System.Text.Encoding.Unicode,new string[] { "\r","\n"," ","\t" },new char[] { ';' }))
      {
        // string[] splitLine = line.Split(new char[] { ':' },StringSplitOptions.RemoveEmptyEntries);
        var splitLine = Table.SplitAndEscape(line, new char[] { ':' });
        
        List<PoS.WordPoS> WordPoSList = new List<PoS.WordPoS>();

        if(splitLine.Count() > 2 || splitLine.Count() < 1)
        {
          throw new System.FormatException("\n\n" + filePath + "\n無法辨識:\"" + line + "\"\n\n");
        }
        else if(splitLine.Count() == 1)
        {//用處是?
        }
        else if(splitLine.Count() == 2)//偵錯，以便錯誤發生彈出說明
        {
          foreach(string item in splitLine[1].Split(new char[] { ',' },StringSplitOptions.RemoveEmptyEntries))
          {
            if(item.ToLower().IndexOf("mps") == 0)
            {
              //修正讀取不處理注音資料
            }
            else if(item.ToLower().IndexOf("strokes") == 0)
            {
              //修正讀取不處理筆劃資料
            }
            else
            {
              string[] stringPoS;
              if(item.Contains('=') == true &&
                      (stringPoS = (item.Split(new char[] { '=' },StringSplitOptions.RemoveEmptyEntries))).Count() == 2)
              {// 詞類有數值
                if(PoS.IsPoS(stringPoS[0]) == false)
                {
                  throw new System.FormatException("\n\n" + filePath + "\n無法辨識:\"" + line + "\"\n\n");
                }
                double PoSValue;
                try
                {
                  PoSValue = double.Parse(stringPoS[1]);
                }
                catch(Exception)
                {
                  throw new System.FormatException("\n\n" + filePath + "\n無法辨識:\"" + line + "\"\n\n");
                }
                WordPoSList.Add(new PoS.WordPoS(
                   stringPoS[0],
              Math.Max(1,PoSValue)
                ));
              }
              else
              {// 詞類沒數值
                if(PoS.IsPoS(item) == false)
                {
                  throw new System.FormatException("\n\n" + filePath + "\n無法辨識:\"" + line + "\"\n\n");
                }
                WordPoSList.Add(new PoS.WordPoS(item,1));
              }
            }
          }
        }
        else
        {
          throw new NotSupportedException(line);
        }

        //真正開始新增與修正
        if(splitLine[0].ToUpper().IndexOf("(D)") >= 0)//刪除
        {
          string word = splitLine[0].Substring(3);

          if(wordPoS.ContainsKey(word) == false)
          {
            wordPoS.Add(word,null);//以null代表刪除
          }
          else
          { throw new System.FormatException("\n\n" + filePath + "\n同時新增刪除或重複字詞:\"" + word + "\"\n\n"); }

        }
        else//修正或新增
        {

          if(splitLine[0].Length > WordMaximumLength)
          {
            WordMaximumLength = splitLine[0].Length;
          }
          if(wordPoS.ContainsKey(splitLine[0]) == false)//含有詞性資料，新增
          {
            if(WordPoSList.Count == 0) { WordPoSList.Add(new PoS.WordPoS("b",1)); }

            wordPoS.Add(splitLine[0],new List<PoS.WordPoS>());
            foreach(var item in WordPoSList)
            {
              wordPoS[splitLine[0]].Add(new PoS.WordPoS(item.PoS,item.PoSNum));
            }
          }
          else
          {
            throw new System.FormatException("\n\n" + filePath + "\n同時新增刪除或重複字詞:\"" + splitLine[0] + "\"\n\n");
          }
        }





      }
    }
    #endregion

    /// <summary>
    /// 濾除低頻率詞性，也就是濾除原始語料誤標的詞性 極端
    /// </summary>
    /// <param name="source"></param>
    void FilterLowFreqPosTw(ref Dictionary<string,List<PoS.WordPoS>> source)
    {
      var tmp = source.ToDictionary(kvp => kvp.Key,kvp => kvp.Value);
      source.Clear();
      source = tmp.Where(kvp => kvp.Key == "去除詞類b專用特殊詞").ToDictionary(kvp => kvp.Key,kvp => kvp.Value);
      tmp.Remove("去除詞類b專用特殊詞");

      foreach(var wordData in tmp)
      {
        //取得最高詞頻
        double max = 0.0;
        foreach(var posData in wordData.Value)
        {
          if(Math.Max(posData.PoSNum,1.0) > max) { max = Math.Max(posData.PoSNum,1.0); }//取max為避免詞性為0情況
        }

        //門檻篩選採用低於該字詞主詞性1%且詞頻5次以下即濾除
        source.Add(wordData.Key,new List<PoS.WordPoS>());
        foreach(var posData in wordData.Value.Where(posData => Math.Max(posData.PoSNum,1.0) * 100.0 / max >= 1.0 || Math.Max(posData.PoSNum,1.0) >= 5))
        {
          source[wordData.Key].Add(posData);
        }


      }
    }

    /// <summary>
    /// 濾除低頻率詞性，也就是濾除原始語料誤標的詞性
    /// </summary>
    /// <param name="source"></param>
    void FilterLowFreqPosCn(ref Dictionary<string,List<PoS.WordPoS>> source)
    {
      var tmp = source.ToDictionary(kvp => kvp.Key,kvp => kvp.Value);
      source.Clear();
      source = tmp.Where(kvp => kvp.Key == "去除詞類b專用特殊詞").ToDictionary(kvp => kvp.Key,kvp => kvp.Value);
      tmp.Remove("去除詞類b專用特殊詞");

      foreach(var wordData in tmp)
      {
        //取得最高詞頻
        double max = 0.0;
        foreach(var posData in wordData.Value)
        {
          if(Math.Max(posData.PoSNum,1.0) > max) { max = Math.Max(posData.PoSNum,1.0); }//取max為避免詞性為0情況
        }

        //門檻篩選採用低於該字詞主詞性3%且詞頻5次以下即濾除
        source.Add(wordData.Key,new List<PoS.WordPoS>());
        foreach(var posData in wordData.Value.Where(posData => Math.Max(posData.PoSNum,1.0) * 100.0 / max >= 3.0 || Math.Max(posData.PoSNum,1.0) >= 5))
        {
          source[wordData.Key].Add(posData);
        }


      }
    }
    
    #region 將諺語或成語以各種符號斷開，遞迴結構，怕諺語中間有不同的符號，所以需要試試看各種可能
    char[] idiomSplitChar = new char[] { '，',',','、','；',';','：',':','。','.','‧','‒','–','—','―','-' };
    string[] idiomMergeChar = new string[] { "，",",","、","；",";","：",":","。",".","‧","‒","–","—","―","-","" };
    public List<string> IdiomExpansion(string name)
    {
      List<string> outputList = new List<string>();

      string[] nameSplit = name.Split(idiomSplitChar,StringSplitOptions.RemoveEmptyEntries);
      for(int _index = 0; _index < nameSplit.Length; _index++)
      {
        for(int length = nameSplit.Length - _index; length >= 1; length--)
        {
          nameRecursive(nameSplit,_index,length,"",outputList);
        }
      }
      return outputList;
    }
    #endregion

    public void nameRecursive(string[] nameSplit,int _index,int length,string tem,List<string> outputList)
    {
      if(length == 1)
      {
        outputList.Add(nameSplit[_index] + tem);
      }
      else
      {
        foreach(string connet in idiomMergeChar)
        {
          nameRecursive(nameSplit,_index,length - 1,connet + nameSplit[_index + length - 1] + tem,outputList);
        }
      }
    }

    #region 是否為注音符號，判斷方式為switch
    bool isMPS(char mps)
    {
      switch(mps)
      {
        case 'ㄅ':
        case 'ㄆ':
        case 'ㄇ':
        case 'ㄈ':
        case 'ㄉ':
        case 'ㄊ':
        case 'ㄋ':
        case 'ㄌ':
        case 'ㄍ':
        case 'ㄎ':
        case 'ㄏ':
        case 'ㄐ':
        case 'ㄑ':
        case 'ㄒ':
        case 'ㄓ':
        case 'ㄔ':
        case 'ㄕ':
        case 'ㄖ':
        case 'ㄗ':
        case 'ㄘ':
        case 'ㄙ':
        case 'ㄧ':
        case 'ㄨ':
        case 'ㄩ':
        case 'ㄚ':
        case 'ㄛ':
        case 'ㄜ':
        case 'ㄝ':
        case 'ㄞ':
        case 'ㄟ':
        case 'ㄠ':
        case 'ㄡ':
        case 'ㄢ':
        case 'ㄣ':
        case 'ㄤ':
        case 'ㄥ':
        case 'ㄦ':
        case 'ˇ':
        case 'ˋ':
        case 'ˊ':
        case '˙':
          return true;
        default:
          return false;
      }
    }
    #endregion

    #region 去除詞類b專用特殊詞List
    List<PoS.WordPoS> 去除詞類b專用特殊詞繁體()
    {// 替代詞性13個為師大日龢提供，詞頻是採用本身詞性出現的總頻率建置
      List<PoS.WordPoS> tem = new List<PoS.WordPoS>();
      tem.Add(new PoS.WordPoS("Na",2019222));
      tem.Add(new PoS.WordPoS("Nb",205773));
      tem.Add(new PoS.WordPoS("Nc",440399));
      tem.Add(new PoS.WordPoS("Nd",186913));
      tem.Add(new PoS.WordPoS("D",892751));
      //tem.Add(new PoS.WordPoS("FW",48604)) 2016-05-25 師大經討論廢除繁體FW
      tem.Add(new PoS.WordPoS("Nf",275493));
      tem.Add(new PoS.WordPoS("VA",189814));
      tem.Add(new PoS.WordPoS("VC",592450));
      tem.Add(new PoS.WordPoS("VCL",60727));
      tem.Add(new PoS.WordPoS("VH",568524));
      tem.Add(new PoS.WordPoS("VJ",155240));
      tem.Add(new PoS.WordPoS("Nv",92844));
      return tem;
    }

    List<PoS.WordPoS> 去除詞類b專用特殊詞簡體()
    {// 2016-05-18 書平討論結果，除FW之外，其他則沿用繁體數據。
      List<PoS.WordPoS> tem = new List<PoS.WordPoS>();
      tem.Add(new PoS.WordPoS("Na",2019222));
      tem.Add(new PoS.WordPoS("Nb",205773));
      tem.Add(new PoS.WordPoS("Nc",440399));
      tem.Add(new PoS.WordPoS("Nd",186913));
      tem.Add(new PoS.WordPoS("D",892751));
      tem.Add(new PoS.WordPoS("Nf",275493));
      tem.Add(new PoS.WordPoS("VA",189814));
      tem.Add(new PoS.WordPoS("VC",592450));
      tem.Add(new PoS.WordPoS("VCL",60727));
      tem.Add(new PoS.WordPoS("VH",568524));
      tem.Add(new PoS.WordPoS("VJ",155240));
      tem.Add(new PoS.WordPoS("Nv",92844));
      return tem;
    }
    #endregion

    #region 繁體0不0，例:"睡不著  VH"...等等，遇到直接代替
    static string[] GetTwO不O() => new string[] {
      "看不見	VC",
"看不出	VC",
"說不出	VE",
"睡不著	VH",
"看不清	VK",
"想不起	VE",
"受不到	VJ",
"提不起	VC",
"制不住	VC",
"抓不住	VC",
"出不來	VCL",
"認不出	VC",
"賣不出	VC",
"叫不出	VC",
"架不住	VC",
"幫不上	VC",
"提不出	VC",
"記不起	VK",
"睜不開	VC",
"收不到	VC",
"學不到	VC",
"握不住	VC",
"做不出	VC",
"生不出	VC",
"查不出	VE",
"起不來	VA",
"換不到	VC",
"講不出	VE",
"借不到	VC",
"望不見	VC",
"壓不住	VC",
"聞不到	VC",
"出不去	VCL",
"抽不出	VC",
"沾不上	VJ",
"跨不出	VCL",
"瞧不出	VE",
"吐不出	VC",
"接不上	VC",
"記不住	VK",
"選不上	VC",
"賺不到	VC",
"包不住	VC",
"抑不住	VC",
"打不開	VC",
"對不上	VC",
"問不出	VE",
"保不住	VC",
"守不住	VC",
"看不開	VH",
"停不住	VH",
"猜不出	VE",
"查不到	VC",
"蓋不住	VC",
"扶不起	VH",
"驗不出	VC",
"排不出	VC",
"顯不出	VK",
"使不出	VC",
"娶不到	VC",
"擠不出	VC",
"遮不住	VC",
"攔不住	VC",
"瞧不見	VE",
"養不出	VC",
"認不清	VK",
"瞞不住	VC",
"捏不住	VC",
"捕不到	VC",
"長不出	VJ",
"榨不出	VC",
"激不起	VC",
"拍不出	VC",
"關不住	VC",
"拿不起	VC",
"立不住	VH",
"取不出	VC",
"拔不出	VC",
"咬不動	VC",
"拖不動	VC",
"揮不出	VC",
"嚐不到	VC",
"頂不住	VH",
"搜不出	VC",
"測不出	VC",
"交不出	VC",
"栓不住	VC",
"放不出	VC",
"搞不出	VC",
"拋不開	VC",
"搜不到	VC",
"帶不動	VC",
"引不出	VC",
"張不開	VC",
"籌不到	VC",
"走不開	VH",
"接不到	VC",
"抗不住	VC",
"管不住	VC",
"鎖不住	VC",
"嗅不到	VC",
"訂不到	VC",
"鎮不住	VC",
"選不出	VC",
"移不開	VC",
"採不到	VC",
"轉不動	VAC",
"舉不出	VC",
"堵不住	VC",
"抽不到	VC",
"避不開	VC",
"超不出	VJ",
"說不動	VE",
"抬不動	VC",
"撿不到	VC",
"勸不住	VC",
"繫不住	VC",
"捉不住	VC",
"綑不住	VC",
"猜不到	VE",
"挑不出	VC",
"送不出	VD",
"抓不出	VC",
"彈不出	VC",
"揮不動	VC",
"襯不出	VJ",
"割不動	VC",
"奏不出	VC",
"問不到	VE",
"遇不見	VC",
"揪不出	VC",
"移不動	VH",
"打不動	VC",
"玩不動	VH",
"游不動	VH",
"掙不開	VC",
"登不出	VC",
"嘗不到	VC",
"孵不出	VC",
"嗅不出	VE"

    };
    #endregion

    #region 簡體0不0 例:"想不开	VH"...遇到直接代替
    static string[] GetCnO不O() => new string[] {
        "捺不住	VJ",
"说不过	VC",
"说不准	VH",
"提不出	VC",
"想不开	VH",
"记不得	VK",
"起不到	VC",
"透不过	VC",
"摸不透	VC",
"配不上	VC",
"拿不准	VC",
"形不成	VC",
"顶不住	VH",
"留不住	VC",
"做不出	VC",
"吃不起	VC",
"构不成	VC",
"提不起	VC",
"走不出	VCL",
"下不来	VA",
"交不起	VC",
"架不住	VC",
"看不过	VE",
"抽不出	VC",
"长不大	VH",
"呆不住	VH",
"担不起	VC",
"用不到	VC",
"收不回	VC",
"识不清	VK",
"拦不住	VC",
"斗不过	VC",
"弄不懂	VC",
"受不起	VC",
"称不上	VG",
"造不出	VC",
"握不住	VC",
"吃不准	VE",
"拍不动	VC",
"说不通	VH",
"摸不定	VC",
"撑不住	VH",
"关不住	VC",
"吃不住	VC",
"闲不住	VH",
"闻不到	VC",
"掩不住	VC",
"显不出	VK",
"睡不醒	VH",
"认不得	VC",
"争不过	VC",
"抓不到	VC",
"抵不上	VC",
"读不起	VC",
"接不上	VC",
"化不开	VHC",
"划不来	VH",
"收不住	VC",
"问不倒	VC",
"找不回	VC",
"供不起	VC",
"挑不出	VC",
"看不透	VJ",
"熬不过	VJ",
"分不到	VC",
"打不到	VC",
"打不破	VC",
"抹不掉	VC",
"穿不上	VC",
"逃不掉	VA",
"逃不脱	VA",
"下不去	VA",
"开不动	VA",
"忆不起	VK",
"立不住	VH",
"寻不到	VC",
"讲不通	VE",
"扯不断	VC",
"抢不到	VC",
"走不开	VH",
"挥不出	VC",
"哭不出	VC",
"难不住	VHC",
"推不动	VC",
"跑不掉	VA",
"治不好	VC",
"养不出	VC",
"映不出	VC",
"查不到	VC",
"结不出	VC",
"挪不动	VC",
"难不倒	VHC",
"饿不死	VHC",
"堵不住	VC",
"藏不住	VC",
"吃不开	VH",
"改不掉	VC",
"走不完	VCL",
"走不脱	VA",
"达不成	VC",
"刹不住	VH",
"拉不住	VC",
"拖不动	VC",
"画不出	VC",
"说不动	VE",
"修不起	VC",
"请不起	VC",
"推不开	VC",
"搞不出	VC",
"飞不动	VA",
"付不出	VC",
"打不动	VC",
"交不上	VC",
"免不掉	VC",
"坐不稳	VH",
"怀不上	VC",
"抹不去	VC",
"拉不动	VC",
"沾不上	VJ",
"挑不起	VC",
"挤不出	VC",
"看不准	VE",
"背不动	VC",
"闻不出	VE",
"请不动	VC",
"做不来	VH",
"掏不出	VC",
"量不出	VC",
"碰不上	VC",
"打不赢	VC",
"甩不掉	VC",
"吃不惯	VC",
"守不住	VC",
"抓不好	VC",
"抗不住	VJ",
"取不出	VC",
"拍不出	VC",
"放不开	VC",
"玩不起	VAC",
"闹不起	VAC",
"尝不到	VC",
"恨不起	VH",
"挑不动	VC",
"测不出	VC",
"挺不过	VH",
"敌不住	VC",
"顾不到	VC",
"售不出	VC",
"握不准	VC",
"摆不平	VC",
"腾不出	VC",
"落不下	VA",
"激不起	VC",
"瞧不出	VE",
"避不开	VC",
"加不满	VC",
"打不响	VAC",
"打不倒	VC",
"用不出	VC",
"讨不起	VC",
"问不出	VE",
"扭不过	VC",
"扯不开	VC",
"改不好	VC",
"卖不动	VA",
"变不出	VC",
"抱不动	VH",
"抵不过	VC",
"转不开	VC",
"举不出	VC",
"挤不上	VC",
"看不住	VC",
"修不好	VC",
"读不通	VH",
"谈不拢	VJ",
"除不掉	VC",
"顾不及	VE",
"够不到	VC",
"猜不中	VC",
"握不定	VC",
"搁不住	VC",
"硬不起	VC",
"超不过	VC",
"越不过	VCL",
"跑不脱	VA",
"搞不起	VC",
"碰不见	VC",
"察不出	VC",
"摸不准	VC",
"稳不住	VH",
"算不出	VC",
"算不准	VC",
"躺不住	VH",
"看不见	VC",
"找不到	VC",
"说不出	VE",
"离不开	VJ",
"数不清	VH",
"看不出	VC",
"看不清	VK",
"想不到	Dk",
"说不清	VE",
"得不到	VC",
"走不动	VH",
"赶不上	VJ",
"听不见	VE",
"分不开	VHC",
"听不懂	VK",
"看不到	VE",
"谈不上	VG",
"写不出	VC",
"听不到	VE",
"站不住	VH",
"想不出	VE",
"找不出	VC",
"过不去	VCL",
"抱不平	VI",
"进不来	VCL",
"顾不上	VK",
"出不来	VCL",
"弄不清	VE",
"受不住	VH",
"信不过	VJ",
"望不见	VC",
"睁不开	VC",
"买不到	VJ",
"挤不进	VCL",
"见不到	VE",
"用不完	VC",
"听不进	VC",
"学不会	VC",
"看不懂	VK",
"拿不出	VC",
"起不来	VA",
"搬不动	VJ",
"管不住	VC",
"认不出	VC",
"记不住	VK",
"说不尽	VC",
"说不完	VC",
"进不去	VCL",
"分不出	VG",
"引不起	VC",
"出不去	VCL",
"叫不出	VC",
"打不中	VC",
"交不出	VC",
"吃不完	VC",
"伸不出	VC",
"听不出	VE",
"听不清	VE",
"坐不住	VH",
"完不成	VC",
"爬不动	VH",
"挡不住	VC",
"流不出	VCL",
"看不惯	VK",
"追不上	VJ",
"逃不过	VC",
"猜不透	VK",
"通不过	VCL",
"跑不动	VH",
"想不起	VE",
"数不尽	VH",
"熬不住	VH",
"遮不住	VC",
"辨不出	VC",
"瞧不见	VE",
"买不起	VJ",
"卖不掉	VC",
"带不走	VC",
"赔不起	VJ",
"包不住	VC",
"发不出	VC",
"打不开	VC",
"用不尽	VC",
"长不出	VJ",
"长不高	VH",
"压不低	VHC",
"压不扁	VH",
"吃不到	VC",
"吓不倒	VJ",
"回不去	VCL",
"回不来	VCL",
"忘不掉	VK",
"抓不住	VC",
"走不过	VCL",
"过不来	VA",
"迈不开	VC",
"制不住	VC",
"学不到	VC",
"抬不动	VH",
"抵不住	VC",
"拆不开	VC",
"拉不开	VC",
"拔不出	VC",
"拔不动	VH",
"花不完	VC",
"轮不到	VC",
"保不住	VC",
"咬不动	VC",
"咬不住	VC",
"查不出	VE",
"洗不掉	VC",
"洗不清	VC",
"流不尽	VC",
"倒不出	VC",
"值不得	VHC",
"拿不动	VC",
"拿不走	VC",
"拿不定	VC",
"挺不住	VH",
"捉不住	VC",
"敌不过	VC",
"赶不走	VC",
"逃不开	VC",
"逃不出	VCL",
"选不出	VC",
"除不去	VC",
"唱不出	VC",
"梦不到	VC",
"猜不出	VE",
"盖不住	VC",
"离不掉	VJ",
"脱不掉	VC",
"透不出	VJ",
"喊不出	VE",
"揭不开	VC",
"答不出	VE",
"装不满	VC",
"填不满	VC",
"想不通	VH",
"搞不清	VC",
"溜不掉	VA",
"解不开	VC",
"躲不过	VC",
"敲不开	VC",
"榨不出	VC",
"憋不住	VH",
"靠不住	VH",
"嚼不动	VC",
"养不活	VC",
"卖不出	VC",
"尝不出	VC",
"尝不透	VJ",
"带不动	VC",
"张不开	VC",
"捞不到	VC",
"瞒不过	VC",
"绊不住	VC",
"腾不开	VC",
"觉不出	VC",
"认不清	VK",
"记不起	VK",
"讲不出	VE",
"读不懂	VC",
"辩不过	VC",
"锁不住	VC",
"闭不上	VC"

      };
  }
}
#endregion