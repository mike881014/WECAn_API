using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Co_occurrence
{
  /// <summary>
  /// 統計共現次數 (Co_occurrence) 的類別
  /// </summary>
  class Co_occurrencePreChar : Co_occurrencePre
  {
    /// <summary>
    /// 表示自我總次數的符號
    /// </summary>
    private char totalSymbol;

    /// <summary>
    /// 存放統計的結果
    /// </summary>
    private Dictionary<char,Dictionary<char,int>> countData;

    /// <summary>
    /// <see cref="Co_occurrencePreString"/> 的建構子
    /// </summary>
    public Co_occurrencePreChar()
    {
      totalSymbol = charTotalSymbol;
      countData = new Dictionary<char,Dictionary<char,int>>();
    }

    /// <summary>
    /// 將統計資料的結果輸出寫檔 (Space 優化格式)
    /// </summary>
    /// <param name="outputFilePath">輸出檔案的路徑檔名</param>
    public void WriteFileSpace(string outputFilePath)
    {
      WriteFileSpace(countData,totalSymbol,outputFilePath);
    }

    /// <summary>
    /// 將統計資料的結果輸出寫檔 (Time 優化格式)
    /// </summary>
    /// <param name="outputFilePath">輸出檔案的路徑檔名</param>
    public void WriteFileTime(string outputFilePath)
    {
      WriteFileTime(countData,totalSymbol,outputFilePath);
    }

    /// <summary>
    /// 統計序列內相鄰項目之間出現的共現次數(Co_occurrence)
    /// <para>此為有向統計，以序列&lt;0, 1, 2, 3&gt;為例，將如下列結果統計:</para>
    /// 0-1, 1-2, 2-3
    /// </summary>
    /// <param name="sequence">統計的有向序列</param>
    public void DirectedSequenceCount(List<char> sequence)
    {
      DirectedSequenceCount(countData,totalSymbol,sequence);
    }

    /// <summary>
    /// 統計序列內相鄰項目之間出現的共現次數(Co_occurrence)
    /// <para>此為無向統計，以序列&lt;0, 1, 2, 3&gt;為例，將如下列結果統計:</para>
    /// 0-1, 1-0, 1-2, 2-1, 2-3, 3-2
    /// </summary>
    /// <param name="sequence">統計的無向序列</param>
    public void UndirectedSequenceCount(List<char> sequence)
    {
      UndirectedSequenceCount(countData,totalSymbol,sequence);
    }

    /// <summary>
    /// 統計序列內任何項目之間出現的共現次數(Co_occurrence)
    /// <para>此為有向統計，以序列&lt;0, 1, 2, 3&gt;為例，將如下列結果統計:</para>
    /// 0-1, 0-2, 0-3, 1-2, 1-3, 2-3
    /// </summary>
    /// <param name="sequence">統計的有向序列</param>
    public void DirectedFieldCount(List<char> sequence)
    {
      DirectedFieldCount(countData,totalSymbol,sequence);
    }
    /// <summary>
    /// 統計序列內任何項目之間出現的共現次數(Co_occurrence)
    /// <para>此為無向統計，以序列&lt;0, 1, 2, 3&gt;為例，將如下列結果統計:</para>
    /// 0-1, 1-0, 0-2, 2-0, 0-3, 3-0, 1-2, 2-1, 1-3, 3-1, 2-3, 3-2 
    /// </summary>
    /// <param name="sequence">統計的無向序列</param>
    public void UndirectedFieldCount(List<char> sequence)
    {
      UndirectedFieldCount(countData,totalSymbol,sequence);
    }
  }
}
