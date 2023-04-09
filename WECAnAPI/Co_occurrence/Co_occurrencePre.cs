using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Co_occurrence
{
  class Co_occurrencePre : Co_occurrence
  {
    /// <summary>
    /// 統計序列內相鄰項目之間出現的共現次數(Co_occurrence)
    /// <para>此為有向統計，以序列&lt;0, 1, 2, 3&gt;為例，將如下列結果統計:</para>
    /// 0-1, 1-2, 2-3
    /// </summary>
    /// <typeparam name="T">執行期統計目標的資料型態</typeparam>
    /// <param name="countData">存放統計的結果</param>
    /// <param name="totalSymbol">表示自我總次數的符號</param>
    /// <param name="sequence">統計的有向序列</param>
    protected void DirectedSequenceCount<T>(Dictionary<T,Dictionary<T,int>> countData,T totalSymbol,List<T> sequence)
    {
      for(var index = 0; index < sequence.Count(); index++)
      {// 單一項目總次數
        SetValue(countData,sequence[index],totalSymbol,1);
      }

      for(var index = 0; index + 1 < sequence.Count(); index++)
      {// 共現次數
        SetValue(countData,sequence[index],sequence[index + 1],1);
      }
    }

    /// <summary>
    /// 統計序列內相鄰項目之間出現的共現次數(Co_occurrence)
    /// <para>此為無向統計，以序列&lt;0, 1, 2, 3&gt;為例，將如下列結果統計:</para>
    /// 0-1, 1-0, 1-2, 2-1, 2-3, 3-2
    /// </summary>
    /// <typeparam name="T">執行期統計目標的資料型態</typeparam>
    /// <param name="countData">存放統計的結果</param>
    /// <param name="totalSymbol">表示自我總次數的符號</param>
    /// <param name="sequence">統計的無向序列</param>
    protected void UndirectedSequenceCount<T>(Dictionary<T,Dictionary<T,int>> countData,T totalSymbol,List<T> sequence)
    {
      for(var index = 0; index < sequence.Count(); index++)
      {// 單一項目總次數
        SetValue(countData,sequence[index],totalSymbol,1);
      }

      for(var index = 0; index + 1 < sequence.Count(); index++)
      {// 共現次數
        SetValue(countData,sequence[index],sequence[index + 1],1);
        SetValue(countData,sequence[index + 1],sequence[index],1);
      }
    }

    /// <summary>
    /// 統計序列內任何項目之間出現的共現次數(Co_occurrence)
    /// <para>此為有向統計，以序列&lt;0, 1, 2, 3&gt;為例，將如下列結果統計:</para>
    /// 0-1, 0-2, 0-3, 1-2, 1-3, 2-3
    /// </summary>
    /// <typeparam name="T">執行期統計目標的資料型態</typeparam>
    /// <param name="countData">存放統計的結果</param>
    /// <param name="totalSymbol">表示自我總次數的符號</param>
    /// <param name="sequence">統計的有向序列</param>
    protected void DirectedFieldCount<T>(Dictionary<T,Dictionary<T,int>> countData,T totalSymbol,List<T> sequence)
    {
      for(var index = 0; index < sequence.Count(); index++)
      {// 單一項目總次數
        SetValue(countData,sequence[index],totalSymbol,1);
      }

      for(var index = 0; index < sequence.Count(); index++)
      {
        for(var nextIndex = index + 1; nextIndex < sequence.Count(); nextIndex++)
        {// 共現次數
          SetValue(countData,sequence[index],sequence[nextIndex],1);
        }
      }
    }
    /// <summary>
    /// 統計序列內任何項目之間出現的共現次數(Co_occurrence)
    /// <para>此為無向統計，以序列&lt;0, 1, 2, 3&gt;為例，將如下列結果統計:</para>
    /// 0-1, 1-0, 0-2, 2-0, 0-3, 3-0, 1-2, 2-1, 1-3, 3-1, 2-3, 3-2 
    /// </summary>
    /// <typeparam name="T">執行期統計目標的資料型態</typeparam>
    /// <param name="countData">存放統計的結果</param>
    /// <param name="totalSymbol">表示自我總次數的符號</param>
    /// <param name="sequence">統計的無向序列</param>
    protected void UndirectedFieldCount<T>(Dictionary<T,Dictionary<T,int>> countData,T totalSymbol,List<T> sequence)
    {
      for(var index = 0; index < sequence.Count(); index++)
      {// 單一項目總次數
        SetValue(countData,sequence[index],totalSymbol,1);
      }

      for(var index = 0; index < sequence.Count(); index++)
      {
        for(var nextIndex = index + 1; nextIndex < sequence.Count(); nextIndex++)
        {// 共現次數
          SetValue(countData,sequence[index],sequence[nextIndex],1);
          SetValue(countData,sequence[nextIndex],sequence[index],1);
        }
      }
    }

    /// <summary>
    /// 將統計資料的結果輸出寫檔 (Space 優化格式)
    /// </summary>
    /// <typeparam name="T">執行期統計目標的資料型態</typeparam>
    /// <param name="countData">存放統計的結果</param>
    /// <param name="totalSymbol">表示自我總次數的符號</param>
    /// <param name="outputFilePath">輸出檔案的路徑檔名</param>
    protected void WriteFileSpace<T>(Dictionary<T,Dictionary<T,int>> countData,T totalSymbol,string outputFilePath)
    {
      System.IO.StreamWriter writer
          = new System.IO.StreamWriter(outputFilePath,false,System.Text.Encoding.Unicode);

      foreach(var mainKey in countData.Keys.ToList())
      {
        writer.Write("{0}{1}{2}{3}{4}"
            ,pairStartChar,mainKey,pairMiddleChar,countData[mainKey][totalSymbol],pairEndChar);

        foreach(var pair in countData[mainKey].Keys.ToList())
        {
          if(object.Equals(pair,totalSymbol) == false)
          {
            writer.Write("{0}{1}{2}{3}{4}"
                ,pairStartChar,pair,pairMiddleChar,countData[mainKey][pair],pairEndChar);
          }
        }

        writer.WriteLine();
      }

      writer.Close();
    }

    /// <summary>
    /// 將統計資料的結果輸出寫檔 (Time 優化格式)
    /// </summary>
    /// <typeparam name="T">執行期統計目標的資料型態</typeparam>
    /// <param name="countData">存放統計的結果</param>
    /// <param name="totalSymbol">表示自我總次數的符號</param>
    /// <param name="outputFilePath">輸出檔案的路徑檔名</param>
    protected void WriteFileTime<T>(Dictionary<T,Dictionary<T,int>> countData,T totalSymbol,string outputFilePath)
    {
      System.IO.StreamWriter writer
          = new System.IO.StreamWriter(outputFilePath,false,System.Text.Encoding.Unicode);

      foreach(var mainKey in countData.Keys.ToList())
      {
        foreach(var pair in countData[mainKey].Keys.ToList())
        {
          if(Equals(pair,totalSymbol) == true)
          {
            writer.WriteLine("{0}{1}{2}{3}{4}"
           ,mainKeyStartChar,mainKey,pairMiddleChar,countData[mainKey][pair],pairEndChar);
          }
          else
          {
            writer.WriteLine("{0}{1}{2}{3}{4}{5}{6}"
                ,mainKeyStartChar,mainKey,pairStartChar,pair,pairMiddleChar,countData[mainKey][pair],pairEndChar);
          }
        }
      }

      writer.Close();
    }
  }
}
