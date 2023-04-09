using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WECAnAPI
{
  /// <summary>
  /// WECAnAPI.cs的建構式分別呼叫IOSegment(檔案斷詞)，或是Segment(及時斷詞)
  /// 進入本Class後，分別進行必要處理，再交給handle做斷詞等共同部分
  /// </summary>
  static class ControlProcess
  {
    #region Segment_Process(交由handle直接處理)
    public static void Segment(Get get,Set set,string sentence,List<string> outSegment,List<string> outPoS,List<List<string>> outSegmentSources)
    {
      handle(get,set,new List<string> { sentence },new List<List<string>> { outSegment },new List<List<string>> { outPoS },new List<List<List<string>>> { outSegmentSources });
    }
        #endregion

    #region IOSegment_Process(先斷成句在交由handle)
    public static void IOSegment(Get get,Set set,string inputFilePath,string outputFilePath,System.Text.Encoding encoding)
    {
      System.IO.StreamReader reader = new System.IO.StreamReader(inputFilePath,encoding);
      List<string> paper = (reader.ReadToEnd().Split(new char[] { '\r','\n' },StringSplitOptions.RemoveEmptyEntries)).ToList();
      reader.Close();
      List<List<string>> paperSegment = new List<List<string>>();
      List<List<string>> paperPoS = new List<List<string>>();
      List<List<List<string>>> paperSegmentSources = new List<List<List<string>>>();

      for(int line = 0; line < paper.Count(); line++)
      {
        paperSegment.Add(new List<string>());
        paperPoS.Add(new List<string>());
        paperSegmentSources.Add(null);
      }

      handle(get,set,paper,paperSegment,paperPoS,paperSegmentSources);

      System.IO.StreamWriter writer = new System.IO.StreamWriter(outputFilePath,false,encoding);

      for(int line = 0; line < paper.Count(); line++)
      {
        for(int forCount = 0; forCount < paperSegment[line].Count(); forCount++)
        {
          writer.Write(paperSegment[line][forCount] + "(" + paperPoS[line][forCount] + ")" + "　");
        }
        writer.Write("\n");
      }

      writer.Close();
    }
        #endregion

    #region Handle(負責接收的Interface，集中處理的Method，斷詞、詞性給予、未知詞處理、人名，依照順序處理。)
    static void handle(Get get,Set set,List<string> sentenceSet,List<List<string>> segedSet,List<List<string>> posedSet,List<List<List<string>>> sourcesSet)
    {
      for(int lineIndex = 0; lineIndex < sentenceSet.Count(); lineIndex++)
      {
        if(segedSet[lineIndex] == null) { segedSet[lineIndex] = new List<string>(); }
        if(posedSet[lineIndex] == null) { posedSet[lineIndex] = new List<string>(); }

        if(sentenceSet[lineIndex] == null)//正常不會進入true
        {
          ToPoS.Handle(get,set,segedSet[lineIndex],posedSet[lineIndex],set.SimplePOSTransfer);
        }
        else
        {//ToSeg中的詞性分析是為了斷詞，所以才需要做兩次。
          ToSeg.Handle(get,set,sentenceSet[lineIndex],segedSet[lineIndex]);
          ToPoS.Handle(get,set,segedSet[lineIndex],posedSet[lineIndex],set.SimplePOSTransfer);
          RuleBased.RuleBased.Handle(get,set,sentenceSet[lineIndex],segedSet[lineIndex],posedSet[lineIndex]);
          if (set.MixTwCn == true) {
            string line = sentenceSet[lineIndex];
            List<string> seged = segedSet[lineIndex];
            int read = 0, length = seged.Count();
            for (int id = 0; id < length; id++) {
              seged[id] = line.Substring(read, seged[id].Length);
              read += seged[id].Length;
            }
          }
        }
      }
      Console.Write('▌');
      List<string> SPLR未知詞清單 = new List<string>();
      if(set.UnKnownWord == true)
      {
        UnKnownWordHandle.UnKnownWordFunction(get,set,segedSet,posedSet,SPLR未知詞清單);
      }
      if(set.AutoExpansionCDBU == true && SPLR未知詞清單.Count() != 0)
      {
        UnKnownWordHandle.WriteToCDBU(get,set,SPLR未知詞清單);
      }
      Console.Write('▌');
      for(int lineIndex = 0; lineIndex < sentenceSet.Count(); lineIndex++)
      {
        if(sourcesSet[lineIndex] != null)
        {
          sourcesSet[lineIndex].Clear();
          foreach(string word in segedSet[lineIndex])
          {
            sourcesSet[lineIndex].Add(get.WordSource(set,word));
          }
        }
      }
      Console.Write('▌');
      for(int lineIndex = 0; lineIndex < sentenceSet.Count(); lineIndex++)
      {
        RemovePoSb.Handle(get,set,segedSet[lineIndex],posedSet[lineIndex]);
      }
      Console.Write('▌');
      
      //中日外人名獨立，若三個同時關閉，則不用偵測
      if(set.ChineseName == true /*|| set.JapaneseName==true || set.ForeignName==true*/)
      {
        get.nameCache?.Clear();

        #region 人名
        //pass1 偵測所有人名
        Nb.HandlePaper(get,set,segedSet,posedSet,sentenceSet,sourcesSet);
        //pass2 對含有人名的句子重新斷句
        set.NameTowPassHandle = true;
        for(int lineIndex = 0; lineIndex < sentenceSet.Count(); lineIndex++)
        {
          foreach(var name in get.nameCache)
          {

            if(sentenceSet[lineIndex].Contains(name) == true)//如果此句有人名，重斷
            {
              segedSet[lineIndex].Clear();
              posedSet[lineIndex].Clear();

              ToSeg.Handle(get,set,sentenceSet[lineIndex],segedSet[lineIndex]);
              ToPoS.Handle(get,set,segedSet[lineIndex],posedSet[lineIndex],set.SimplePOSTransfer);
              RuleBased.RuleBased.Handle(get,set,sentenceSet[lineIndex],segedSet[lineIndex],posedSet[lineIndex]);


              if(sourcesSet[lineIndex] != null)
              {
                sourcesSet[lineIndex].Clear();
                foreach(string word in segedSet[lineIndex])
                {
                  sourcesSet[lineIndex].Add(get.WordSource(set,word));
                }
              }

              RemovePoSb.Handle(get,set,segedSet[lineIndex],posedSet[lineIndex]);

              break;
            }
          }

        }
        set.NameTowPassHandle = false;

        //未知詞為整篇處理，無法單一句，故獨立
        SPLR未知詞清單.Clear();
        if(set.UnKnownWord == true)
        {
          UnKnownWordHandle.UnKnownWordFunction(get,set,segedSet,posedSet,SPLR未知詞清單);
        }
        if(set.AutoExpansionCDBU == true && SPLR未知詞清單.Count() != 0)
        {
          UnKnownWordHandle.WriteToCDBU(get,set,SPLR未知詞清單);
        }
        #endregion

      }
    }
    #endregion
  }
}
