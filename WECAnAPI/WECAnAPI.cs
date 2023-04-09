using System.Collections.Generic;

namespace WECAnAPI
{
  //*****************************************************//
  //                       _oo0oo_                       //
  //                      o8888888o                      //
  //                      88" . "88                      //
  //                      (| -_- |)                      //
  //                      0\  =  /0                      //
  //                    ___/`---'\___                    //
  //                  .' \\|     |// '.                  //
  //                 / \\|||  :  |||// \                 //
  //                / _||||| -:- |||||- \                //
  //               |   | \\\  -  /// |   |               //
  //               | \_|  ''\---/''  |_/ |               //
  //               \  .-\__  '-'  ___/-. /               //
  //             ___'. .'  /--.--\  `. .'___             //
  //          ."" '<  `.___\_<|>_/___.' >' "".           //
  //         | | :  `- \`.;`\ _ /`;.`/ - ` : | |         //
  //         \  \ `_.   \_ __\ /__ _/   .-` /  /         //
  //     =====`-.____`.___ \_____/___.-`___.-'=====      //
  //                       `=---='                       //
  //     ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~     //
  //                                                     //
  //              佛祖保佑         言出法隨              //
  //                                                     //
  //*****************************************************//
  /// <summary>
  /// 
  /// Word Extraction for Chinese Analysis
  /// </summary>
  public class WECAnAPI
  {
    Get get;
    Set fileSet;

    #region WECAnAPI()， WECAnAPI(string USERpath) 建構子 
    /// <summary>
    /// 建構子
    /// </summary>
    public WECAnAPI()
    {
      get = new Get();
      fileSet = new Set();
      System.GC.Collect();
    }
    /// <summary>
    /// 自訂路徑建構子(當user需自訂語料路徑時，所使用的建構子)
    /// </summary>
    /// <param name="corpusPath">自訂路徑</param>
    public WECAnAPI(string corpusPath)
    {
      get = new Get(corpusPath);
      fileSet = new Set(corpusPath);
      System.GC.Collect();
    }
    #endregion
    
    //基本的建構式，IOSegment與使用者輸入的對接口
    #region Main Interface(IOSegment)
    /// <summary>
        /// 檔案斷詞標記
        /// </summary>
        /// <param name="inputFilePath">輸入檔案路徑</param>
        /// <param name="outputFilePath">輸出檔案路徑</param>
    public void IOSegment(string inputFilePath,string outputFilePath)
    => IOSegment(inputFilePath,outputFilePath,System.Text.Encoding.Default,fileSet);
    /// <summary>
    /// 檔案斷詞標記(ANSI)
    /// </summary>
    /// <param name="inputFilePath">輸入檔案路徑</param>
    /// <param name="outputFilePath">輸出檔案路徑</param>
    public void IOSegment_ANSI(string inputFilePath,string outputFilePath)
    => IOSegment(inputFilePath,outputFilePath,System.Text.Encoding.Default,fileSet);
    /// <summary>
    /// 檔案斷詞標記(UTF-8)
    /// </summary>
    /// <param name="inputFilePath">輸入檔案路徑</param>
    /// <param name="outputFilePath">輸出檔案路徑</param>
    public void IOSegment_UTF8(string inputFilePath,string outputFilePath)
    => IOSegment(inputFilePath,outputFilePath,System.Text.Encoding.UTF8,fileSet);
    /// <summary>
    /// 檔案斷詞標記(Unicode或UTF-16)
    /// </summary>
    /// <param name="inputFilePath">輸入檔案路徑</param>
    /// <param name="outputFilePath">輸出檔案路徑</param>
    public void IOSegment_Unicode(string inputFilePath,string outputFilePath)
    => IOSegment(inputFilePath,outputFilePath,System.Text.Encoding.Unicode,fileSet);
    /// <summary>
    /// 檔案斷詞標記
    /// </summary>
    /// <param name="inputFilePath">輸入檔案路徑</param>
    /// <param name="outputFilePath">輸出檔案路徑</param>
    /// <param name="encoding">自訂編碼</param>
    public void IOSegment(string inputFilePath,string outputFilePath,System.Text.Encoding encoding)
    => IOSegment(inputFilePath,outputFilePath,encoding,fileSet);
    /// <summary>
    /// 檔案斷詞標記
    /// </summary>
    /// <param name="inputFilePath">輸入檔案路徑</param>
    /// <param name="outputFilePath">輸出檔案路徑</param>
    /// <param name="useSet">自訂設定</param>
    public void IOSegment(string inputFilePath,string outputFilePath,Set useSet)
    => IOSegment(inputFilePath,outputFilePath,System.Text.Encoding.Default,useSet);
    /// <summary>
    /// 檔案斷詞標記(ANSI)
    /// </summary>
    /// <param name="inputFilePath">輸入檔案路徑</param>
    /// <param name="outputFilePath">輸出檔案路徑</param>
    /// <param name="useSet">自訂設定</param>
    public void IOSegment_ANSI(string inputFilePath,string outputFilePath,Set useSet)
    => IOSegment(inputFilePath,outputFilePath,System.Text.Encoding.Default,useSet);
    /// <summary>
    /// 檔案斷詞標記(UTF-8)
    /// </summary>
    /// <param name="inputFilePath">輸入檔案路徑</param>
    /// <param name="outputFilePath">輸出檔案路徑</param>
    /// <param name="useSet">自訂設定</param>
    public void IOSegment_UTF8(string inputFilePath,string outputFilePath,Set useSet)
    => IOSegment(inputFilePath,outputFilePath,System.Text.Encoding.UTF8,useSet);
    /// <summary>
    /// 檔案斷詞標記(Unicode或UTF-16)
    /// </summary>
    /// <param name="inputFilePath">輸入檔案路徑</param>
    /// <param name="outputFilePath">輸出檔案路徑</param>
    /// <param name="useSet">自訂設定</param>
    public void IOSegment_Unicode(string inputFilePath,string outputFilePath,Set useSet)
    => IOSegment(inputFilePath,outputFilePath,System.Text.Encoding.Unicode,useSet);
    /// <summary>
    /// 檔案斷詞標記
    /// </summary>
    /// <param name="inputFilePath">輸入檔案路徑</param>
    /// <param name="outputFilePath">輸出檔案路徑</param>
    /// <param name="encoding">自訂編碼</param>
    /// <param name="useSet">自訂設定</param>
    public void IOSegment(string inputFilePath,string outputFilePath,System.Text.Encoding encoding,Set useSet)
    => ControlProcess.IOSegment(get,useSet,inputFilePath,outputFilePath,encoding);
        #endregion

    //基本的建構式，Segment與使用者輸入的對接口
    #region Main Interface(Segment)
        /// <summary>
        /// 即時斷詞標記
        /// </summary>
        /// <param name="inSegment">輸入斷詞</param>
        /// <param name="outPoS">輸出標記</param>
        public void Segment(List<string> inSegment,List<string> outPoS)
    => Segment(null,inSegment,outPoS,null,fileSet);
    /// <summary>
    /// 即時斷詞標記
    /// </summary>
    /// <param name="sentence">輸入句子</param>
    /// <param name="outSegment">輸出斷詞</param>
    public void Segment(string sentence,List<string> outSegment)
    => Segment(sentence,outSegment,new List<string>(),null,fileSet);
    /// <summary>
    /// 即時斷詞標記
    /// </summary>
    /// <param name="sentence">輸入句子</param>
    /// <param name="outSegment">輸出斷詞</param>
    /// <param name="outSegmentSources">輸出字典資訊</param>
    public void Segment(string sentence,List<string> outSegment,List<List<string>> outSegmentSources)
    => Segment(sentence,outSegment,new List<string>(),outSegmentSources,fileSet);
    /// <summary>
    /// 即時斷詞標記
    /// </summary>
    /// <param name="sentence">輸入句子</param>
    /// <param name="outSegment">輸出斷詞</param>
    /// <param name="outPoS">輸出標記</param>
    public void Segment(string sentence,List<string> outSegment,List<string> outPoS)
    => Segment(sentence,outSegment,outPoS,null,fileSet);
    /// <summary>
    /// 即時斷詞標記
    /// </summary>
    /// <param name="sentence">輸入句子</param>
    /// <param name="outSegment">輸出斷詞</param>
    /// <param name="outPoS">輸出標記</param>
    /// <param name="outSegmentSources">輸出字典資訊</param>
    public void Segment(string sentence,List<string> outSegment,List<string> outPoS,List<List<string>> outSegmentSources)
    => Segment(sentence,outSegment,outPoS,outSegmentSources,fileSet);
    /// <summary>
    /// 即時斷詞標記
    /// </summary>
    /// <param name="inSegment">輸入斷詞</param>
    /// <param name="outPoS">輸出標記</param>
    /// <param name="useSet">自訂設定</param>
    public void Segment(List<string> inSegment,List<string> outPoS,Set useSet)
    => Segment(null,inSegment,outPoS,null,useSet);
    /// <summary>
    /// 即時斷詞標記
    /// </summary>
    /// <param name="sentence">輸入句子</param>
    /// <param name="outSegment">輸出斷詞</param>
    /// <param name="useSet">自訂設定</param>
    public void Segment(string sentence,List<string> outSegment,Set useSet)
    => Segment(sentence,outSegment,new List<string>(),null,useSet);
    /// <summary>
    /// 即時斷詞標記
    /// </summary>
    /// <param name="sentence">輸入句子</param>
    /// <param name="outSegment">輸出斷詞</param>
    /// <param name="outSegmentSources">輸出字典資訊</param>
    /// <param name="useSet">自訂設定</param>
    public void Segment(string sentence,List<string> outSegment,List<List<string>> outSegmentSources,Set useSet)
    => Segment(sentence,outSegment,new List<string>(),outSegmentSources,useSet);
    /// <summary>
    /// 即時斷詞標記
    /// </summary>
    /// <param name="sentence">輸入句子</param>
    /// <param name="outSegment">輸出斷詞</param>
    /// <param name="outPoS">輸出標記</param>
    /// <param name="useSet">自訂設定</param>
    public void Segment(string sentence,List<string> outSegment,List<string> outPoS,Set useSet)
    => Segment(sentence,outSegment,outPoS,null,useSet);
    /// <summary>
    /// 即時斷詞標記(呼叫ContralProcess)
    /// </summary>
    /// <param name="sentence">輸入句子</param>
    /// <param name="outSegment">輸出斷詞</param>
    /// <param name="outPoS">輸出標記</param>
    /// <param name="outSegmentSources">輸出字典資訊</param>
    /// <param name="useSet">自訂設定</param>
    public void Segment(string sentence,List<string> outSegment,List<string> outPoS,List<List<string>> outSegmentSources,Set useSet)
    => ControlProcess.Segment(get,useSet,sentence,outSegment,outPoS,outSegmentSources);
    #endregion

    #region Set
    /// <summary>
    /// 繁體簡體模式
    /// </summary>
    /// <param name="enable">false=純繁模式, true=純簡模式</param>
    public void SetSimplifiedChinese(bool enable) => fileSet.SimplifiedChinese = enable;
    /// <summary>
    /// 是否使用簡體GigaWord字典檔
    /// </summary>
    /// <param name="enable"></param>
    public void SetGigaWordSimplified(bool enable) => fileSet.GigaWordSimplified = enable;
    /// <summary>
    /// 是否使用自定義字典檔(\WECAnUser\UserWord.txt)
    /// </summary>
    /// <param name="enable"></param>
    public void SetUserWord(bool enable) => fileSet.UserWord = enable;

    /// <summary>
    /// 是否使用未知詞偵測模組
    /// </summary>
    /// <param name="enable"></param>
    public void SetUnKnownWord(bool enable) => fileSet.UnKnownWord = enable;
    /// <summary>
    /// 未知詞詞性
    /// </summary>
    /// <param name="enable"></param>
    public void SetUnKnownPoS(string pos) => fileSet.UnKnownPoS = pos;
    /// <summary>
    /// 是否匯出不被偵測為未知詞之計算結果
    /// </summary>
    /// <param name="enable"></param>
    public void SetLogNotInUnKnownWord(bool enable) => fileSet.LogNotInUnKnownWord = enable;
    /// <summary>
    /// 是否將未知詞偵測模組所偵測的結果寫檔至\WECAnUser\UserWord.txt
    /// </summary>
    /// <param name="enable"></param>
    public void SetAutoExpansionCDBU(bool enable) => fileSet.AutoExpansionCDBU = enable;
    /// <summary>
    /// 且是否只寫入無父子關係的未知詞
    /// </summary>
    /// <param name="enable"></param>
    public void SetJustUnique(bool enable) => fileSet.JustUnique = enable;

    /// <summary>
    /// 是否使用成語與諺語字典檔
    /// </summary>
    /// <param name="enable"></param>
    public void SetIdiom(bool enable) => fileSet.Idiom = enable;

    /// <summary>
    /// 輸出詞性是否轉為簡易詞性
    /// </summary>
    /// <param name="enable"></param>
    public void SetSimplePOSTransfer(bool enable) => fileSet.SimplePOSTransfer = enable;
    /// <summary>
    /// 是否使用條件隨機場斷詞模組
    /// </summary>
    /// <param name="enable"></param>
    public void SetCRFSegment(bool enable) => fileSet.CRFSegment = enable;
    /// <summary>
    /// 是否使用中文姓名合併模組
    /// </summary>
    /// <param name="enable"></param>
    public void SetChineseName(bool enable) => fileSet.ChineseName = enable;
    ///// <summary>
    ///// 是否使用日本翻譯姓名合併模組
    ///// </summary>
    ///// <param name="enable"></param>
    //public void SetJapaneseName(bool enable) => fileSet.JapaneseName = enable;
    ///// <summary>
    ///// 是否使用外國翻譯姓名合併模組
    ///// </summary>
    ///// <param name="enable"></param>
    //public void SetForeignName(bool enable) => fileSet.ForeignName = enable;

    /// <summary>
    /// 是否使用自定義強制合併字典
    /// </summary>
    /// <param name="enable"></param>
    public void SetForceMergeWord(bool enable) => fileSet.ForceMergeWord = enable;
    /// <summary>
    /// 是否使用自定義強制分開字典
    /// </summary>
    /// <param name="enable"></param>
    public void SetForceSplitWord(bool enable) => fileSet.ForceSplitWord = enable;
    /// <summary>
    /// 是否合併英數混合字
    /// </summary>
    /// <param name="enable"></param>
    public void SetForceMergeNeuAndFW(bool enable) => fileSet.ForceMergeNeuAndFW = enable;
    /// <summary>
    /// 合併英數混合，且在空白處分開
    /// </summary>
    /// <param name="enable"></param>
    public void SetSeperateInSpace(bool enable) => fileSet.SeperateInSpace = enable;
    /// <summary>
    /// 是否為繁簡合併
    /// </summary>
    /// <param name="enable"></param>
    public void SetMixTwCn(bool enable) => fileSet.MixTwCn = enable;

    /// <summary>
    /// 疊字合併: '輕'+'輕'='輕輕'   、    '嘿'+'嘿'+'嘿'='嘿嘿嘿'
    /// </summary>
    /// <param name="enable"></param>
    public void SetMergeConsecutiveSingleWord(bool enable) => fileSet.MergeConsecutiveSingleWord = enable;
    /// <summary>
    /// 動詞方向詞合併: '看過'+'來'='看過來'   、   '放'+'回去'='放回去'
    /// </summary>
    /// <param name="enable"></param>
    public void SetMergeVerbDirectionWord(bool enable) => fileSet.MergeVerbDirectionWord = enable;
    /// <summary>
    /// 三字詞組合併: '搖'+'搖頭'='搖搖頭'
    /// </summary>
    /// <param name="enable"></param>
    public void SetMergeSpecialThreeWord(bool enable) => fileSet.MergeSpecialThreeWord = enable;
    /// <summary>
    /// 是否使用重複字詞合併功能，例如'好棒'+'好棒'會合併為'好棒好棒'*
    /// </summary>
    /// <param name="enable"></param>
    public void SetMergeSameWord(bool enable) => fileSet.MergeSameWord = enable;
    /// <summary>
    /// 精簡化相鄰相同詞: '好棒好棒'='好棒'   注意！精簡化功能會刪減輸出詞彙，請小心使用！
    /// </summary>
    /// <param name="enable"></param>
    public void SetSameWordSimplify(bool enable) => fileSet.SameWordSimplify = enable;
    /// <summary>
    /// 量詞疊字合併: '一'+'顆顆'='一顆顆'
    /// </summary>
    /// <param name="enable"></param>
    public void SetMergeSpecialNeqaWord(bool enable) => fileSet.MergeSpecialNeqaWord = enable;
    /// <summary>
    /// O不O合併: '看'+'不到'='看不到'
    /// </summary>
    /// <param name="enable"></param>
    public void SetMergeO不O(bool enable) => fileSet.MergeO不O = enable;
    /// <summary>
    /// 特殊否定詞合併: '不'+[任意詞]，例如：不能、不開心；[任意單字詞]+'不'+[任意單字詞]，例如：好不好、能不能
    /// </summary>
    /// <param name="enable"></param>
    public void SetMergeSpecialNegativeWord(bool enable) => fileSet.MergeSpecialNegativeWord = enable;


    /// <summary>
    /// 未知詞偵測K參數
    /// </summary>
    /// <param name="valueK"></param>
    public void SetK(int valueK) => fileSet.K = valueK;
    /// <summary>
    /// 未知詞偵測C參數
    /// </summary>
    /// <param name="valueC"></param>
    public void SetC(int valueC) => fileSet.C = valueC;
    /// <summary>
    /// 未知詞偵測D參數
    /// </summary>
    /// <param name="valueD"></param>
    public void SetD(int valueD) => fileSet.D = valueD;
    /// <summary>
    /// 未知詞偵測E參數
    /// </summary>
    /// <param name="valueE"></param>
    public void SetE(double valueE) => fileSet.E = valueE;
    /// <summary>
    /// 未知詞偵測U參數
    /// </summary>
    /// <param name="valueU"></param>
    public void SetU(double valueU) => fileSet.U = valueU;
    /// <summary>
    /// 未知詞偵測V參數
    /// </summary>
    /// <param name="valueV"></param>
    public void SetV(double valueV) => fileSet.V = valueV;
    #endregion

    #region Get  
    /// <summary>
    /// 取得語料路徑
    /// </summary>
    /// <returns></returns>
    public string GetCourpusPath() => fileSet.CorpusPath;
    /// <summary>
    /// 取得注音資料
    /// </summary>
    /// <returns></returns>
    public Dictionary<string,List<string>> GetAllWordMPS() => get.MPS_Mix;
    /// <summary>
    /// 以注音取得文字表
    /// </summary>
    /// <returns></returns>
    public Dictionary<string,HashSet<string>> GetMPSBeginEndTable() => get.MPSBeginEndTable;
    /// <summary>
    /// 取得注音資料
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public List<List<string>> GetMPS(List<string> str) => get.WordMPS(str);
    /// <summary>
    /// 取得注音資料
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public List<string> GetMPS(string str) => get.WordMPS(str);
    /// <summary>
    /// 取得目前使用字詞清單
    /// </summary>
    /// <param name="useSet">自訂設定</param>
    /// <returns></returns>
    public HashSet<string> GetAllWord(Set useSet) => get.WordList(useSet);
    /// <summary>
    /// 取得該字或字詞是否存在於字典中
    /// </summary>
    /// <param name="useSet"></param>
    /// <param name="word"></param>
    /// <returns></returns>
    public bool IsWord(Set useSet,string word) => get.IsWord(useSet,word);
    /// <summary>
    /// 檢查字元是否符合基礎多語言平面(BMP)字符定義範圍
    /// </summary>
    /// <param name="_string"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    public bool IsUnicodeBasicMultilingualPlaneChineseChar(string _string,int index) => IsUnicodeBasicMultilingualPlaneChineseChar(_string[index]);
    /// <summary>
    /// 檢查字元是否符合基礎多語言平面(BMP)字符定義範圍
    /// </summary>
    /// <param name="_char"></param>
    /// <returns></returns>
    public bool IsUnicodeBasicMultilingualPlaneChineseChar(char _char) => get.IsUnicodeBasicMultilingualPlaneChineseChar(_char);
    /// <summary>
    /// 清除人名快取
    /// </summary>
    public void ClearNameCache() => get.ClearNameCache();
    #endregion
  }
}