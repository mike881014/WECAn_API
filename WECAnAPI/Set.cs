using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WECAnAPI
{
  /// <summary>
  /// WECAn的基礎設定，以介面的方式
  /// </summary>
  public class Set
  {
    #region Set建構子
    public Set()
    {
      CorpusPath = "";
      readSet();
    }
    //WECAnAPI.cs透過fileSet呼叫Set的建構子，設定語料位置
    public Set(string _corpusPath)
    {
      CorpusPath = _corpusPath + @"\";
      readSet();
    }
    #endregion

    public string CorpusPath;//語料位置

    public bool Cindy = true;//是否啟用繁體Cindy

    public bool SimplifiedChinese = false;//是否啟用簡體
    public bool GigaWordSimplified = false;

    public bool UserWord = false;

    public bool UnKnownWord = false;//是否啟用未知詞處理，通常不啟用，原因:效率不好，效果也不好
    public string UnKnownPoS = "b";/*未知詞詞性*/
    public bool LogNotInUnKnownWord = false;/*是否匯出不被偵測為未知詞之計算結果*/
    public bool AutoExpansionCDBU = false;
    public bool JustUnique = true; /*是否只寫入無父子關係的未知詞*/

    public bool Idiom = false;//諺語

    public bool SimplePOSTransfer = true;
    public bool CRFSegment = false;
    public bool ChineseName = false;
    //  public bool JapaneseName = false;
    // public bool ForeignName = false;
    internal bool NameTowPassHandle = false;
    public bool ForceMergeWord = false; // 是否使用自定義強制合併字典
    public bool ForceSplitWord = false; // 是否使用自定義強制分開字典

    public bool ForceMergeNeuAndFW = true; // 是否合併英數混合字
    public bool SeperateInSpace = true; // 合併英數混合，且在空白處分開
    public bool MixTwCn = true; // 是否為繁簡合併

    public bool MergeConsecutiveSingleWord = true;/*疊字合併: '輕'+'輕'='輕輕'   、    '嘿'+'嘿'+'嘿'='嘿嘿嘿'*/
    public bool MergeVerbDirectionWord = true;/*動詞方向詞合併: '看過'+'來'='看過來'   、   '放'+'回去'='放回去'*/
    public bool MergeSpecialThreeWord = false;/*三字詞組合併: '搖'+'搖頭'='搖搖頭'*/
    public bool MergeSameWord = true; /*相鄰相同詞:  '好棒'+'好棒'='好棒好棒'*/
    public bool SameWordSimplify = false;/*精簡化相鄰相同詞: '好棒好棒'='好棒'   注意！精簡化功能會刪減輸出詞彙，請小心使用！*/
    public bool MergeSpecialNeqaWord = false;/*量詞疊字合併: '一'+'顆顆'='一顆顆'*/
    public bool MergeO不O=true;/*O不O合併: '看'+'不到'='看不到'*/
    public bool MergeSpecialNegativeWord = false; /*特殊否定詞合併: '不'+[任意詞]，例如：不能、不開心；[任意單字詞]+'不'+[任意單字詞]，例如：好不好、能不能*/

    public int K = 2;//BIES計算的初始值
    public int C = 10;
    public int D = 60;
    public double E = 0.1;
    public double U = 1.2;
    public double V = 1.5;

    #region 使用者設定檔讀取
    void readSet()
    {
      if(System.IO.File.Exists(CorpusPath + @"WECAnUser\UserConfig.txt") == true)//設定檔存在的情況下
      {
        foreach(string line in Table.ReadCommentFileToArray(CorpusPath + @"WECAnUser\UserConfig.txt",System.Text.Encoding.Unicode,new string[] { "\r","\n"," ","\t" },new char[] { ';' }))
        {
          string[] setField = line.Split(new char[] { '=' },StringSplitOptions.RemoveEmptyEntries);

          try
          {
            if(setField.Length != 2)//基本的確認語法是否正確
            {
              throw new System.FormatException("\n\n" + CorpusPath + @"WECAnUser\UserConfig.txt" + "\n無法辨識:\"" + line + "\"\n\n");
            }
            switch(setField[0])
            {
              case "SimplifiedChinese":
                SimplifiedChinese = bool.Parse(setField[1]);
                break;
              case "GigaWordSimplified":
                GigaWordSimplified = bool.Parse(setField[1]);
                break;

              case "UserWord":
                UserWord = bool.Parse(setField[1]);
                break;

              case "UnKnownWord":/*是否使用未知詞*/
                UnKnownWord = bool.Parse(setField[1]);
                break;
              case "UnKnownPoS":/*未知詞詞性*/
                UnKnownPoS = setField[1];
                break;
              case "LogNotInUnKnownWord":/*是否匯出不被偵測為未知詞之計算結果*/
                LogNotInUnKnownWord = bool.Parse(setField[1]);
                break;
              case "AutoExpansionCDBU":/*是否建立未知詞辭典*/
                AutoExpansionCDBU = bool.Parse(setField[1]);
                break;
              case "JustUnique":/*且是否只寫入無父子關係的未知詞*/
                JustUnique = bool.Parse(setField[1]);
                break;

              case "Idiom":
                Idiom = bool.Parse(setField[1]);
                break;

              case "SimplePOSTransfer":
                SimplePOSTransfer = bool.Parse(setField[1]);
                break;
              case "CRFSegment":/*是否使用CRF斷詞*/
                CRFSegment = bool.Parse(setField[1]);
                break;
              case "ChineseName":
                ChineseName = bool.Parse(setField[1]);
                break;
              //case "JapaneseName":
              //  JapaneseName = bool.Parse(setField[1]);
              //  break;
              //case "ForeignName":
              //  ForeignName = bool.Parse(setField[1]);
              //  break;

              case "ForceMergeNeuAndFW"://是否合併英數混合字
                ForceMergeNeuAndFW = bool.Parse(setField[1]);
                break;
              case "SeperateInSpace"://合併英數混合，且在空白處分開
                SeperateInSpace = bool.Parse(setField[1]);
                break;
              case "ForceMergeWord"://是否使用者強制合併字典
                ForceMergeWord = bool.Parse(setField[1]);
                break;
              case "ForceSplitWord"://是否使用者強制分開字典
                ForceSplitWord = bool.Parse(setField[1]);
                break;
              case "MixTwCn"://是否為繁簡合併
                MixTwCn = bool.Parse(setField[1]);
                break;

              case "MergeConsecutiveSingleWord"://疊字合併: '輕'+'輕'='輕輕'   、    '嘿'+'嘿'+'嘿'='嘿嘿嘿'
                MergeConsecutiveSingleWord = bool.Parse(setField[1]);
                break;
              case "MergeVerbDirectionWord"://動詞方向詞合併: '看過'+'來'='看過來'   、   '放'+'回去'='放回去'
                MergeVerbDirectionWord = bool.Parse(setField[1]);
                break;
              case "MergeSpecialThreeWord"://三字詞組合併: '搖'+'搖頭'='搖搖頭'
                MergeSpecialThreeWord = bool.Parse(setField[1]);
                break;
              case "MergeSameWord"://相鄰相同詞:  '好棒'+'好棒'='好棒好棒'
                MergeSameWord = bool.Parse(setField[1]);
                break;
              case "SameWordSimplify"://精簡化相鄰相同詞: '好棒好棒'='好棒'   注意！精簡化功能會刪減輸出詞彙，請小心使用！
                SameWordSimplify = bool.Parse(setField[1]);
                break;
              case "MergeSpecialNeqaWord"://量詞疊字合併: '一'+'顆顆'='一顆顆'
                MergeSpecialNeqaWord = bool.Parse(setField[1]);
                break;
              case "MergeO不O"://O不O合併: '看'+'不到'='看不到'
                MergeO不O = bool.Parse(setField[1]);
                break;
              case "MergeSpecialNegativeWord":/*特殊否定詞合併: '不'+[任意詞]，例如：不能、不開心；[任意單字詞]+'不'+[任意單字詞]，例如：好不好、能不能*/
                MergeSpecialNegativeWord = bool.Parse(setField[1]);
                break;

              case "K":
                K = int.Parse(setField[1]);
                break;
              case "C":
                C = int.Parse(setField[1]);
                break;
              case "D":
                D = int.Parse(setField[1]);
                break;
              case "E":
                E = double.Parse(setField[1]);
                break;
              case "U":
                U = double.Parse(setField[1]);
                break;
              case "V":
                V = double.Parse(setField[1]);
                break;
              default:
                throw new System.FormatException("\n\n" + CorpusPath + @"WECAnUser\UserConfig.txt" + "\n無法辨識:\"" + line + "\"\n\n");
            }
          }
          catch(System.FormatException)
          {
            throw new System.FormatException("\n\n" + CorpusPath + @"WECAnUser\UserConfig.txt" + "\n無法辨識:\"" + line + "\"\n\n");
          }
        }
      }
      NameTowPassHandle = false;
    }
    #endregion
    
    #region 特定人士設定檔(繁體)
    public void ToHanCheckerTwSet()
    {
      Cindy = false;
      

      SimplifiedChinese = false;
      GigaWordSimplified = false;

      UserWord = false;

      UnKnownWord = false;
      AutoExpansionCDBU = false;
      JustUnique = true;

      Idiom = false;

      SimplePOSTransfer = true;
      CRFSegment = false;
      ChineseName = false;


      MergeConsecutiveSingleWord = true;/*疊字合併: '輕'+'輕'='輕輕'   、    '嘿'+'嘿'+'嘿'='嘿嘿嘿'*/
      MergeVerbDirectionWord = true;/*動詞方向詞合併: '看過'+'來'='看過來'   、   '放'+'回去'='放回去'*/
      MergeSpecialThreeWord = false;/*三字詞組合併: '搖'+'搖頭'='搖搖頭'*/
      MergeSameWord = false; /*相鄰相同詞:  '好棒'+'好棒'='好棒好棒'*/
      SameWordSimplify = false;/*精簡化相鄰相同詞: '好棒好棒'='好棒'   注意！精簡化功能會刪減輸出詞彙，請小心使用！*/
      MergeSpecialNeqaWord = false;/*量詞疊字合併: '一'+'顆顆'='一顆顆'*/
      MergeO不O = true;/*O不O合併: '看'+'不到'='看不到'*/

      K = 2;
      C = 10;
      D = 60;
      E = 0.1;
      U = 1.2;
      V = 1.5;
    }
    #endregion

    #region 特定人士設定檔(簡體)
    public void ToHanCheckerCnSet()
    {
      Cindy = false;
    

      SimplifiedChinese = true;
      GigaWordSimplified = false;

      UserWord = false;

      UnKnownWord = false;
      AutoExpansionCDBU = false;
      JustUnique = true;

      Idiom = false;

      SimplePOSTransfer = true;
      CRFSegment = false;
      ChineseName = false;

      MergeConsecutiveSingleWord = true;/*疊字合併: '輕'+'輕'='輕輕'   、    '嘿'+'嘿'+'嘿'='嘿嘿嘿'*/
      MergeVerbDirectionWord = true;/*動詞方向詞合併: '看過'+'來'='看過來'   、   '放'+'回去'='放回去'*/
      MergeSpecialThreeWord = false;/*三字詞組合併: '搖'+'搖頭'='搖搖頭'*/
      MergeSameWord = false; /*相鄰相同詞:  '好棒'+'好棒'='好棒好棒'*/
      SameWordSimplify = false;/*精簡化相鄰相同詞: '好棒好棒'='好棒'   注意！精簡化功能會刪減輸出詞彙，請小心使用！*/
      MergeSpecialNeqaWord = false;/*量詞疊字合併: '一'+'顆顆'='一顆顆'*/
      MergeO不O = true;/*O不O合併: '看'+'不到'='看不到'*/

      K = 2;
      C = 10;
      D = 60;
      E = 0.1;
      U = 1.2;
      V = 1.5;
    }
    #endregion
  }
}
