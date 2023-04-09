using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WECAnAPI
{
  static class SplitNeu
  {//將數字拆開，Pos插入Neu
    public static bool Handle(Get get,Set set,List<string> seged)
    {
      Dictionary<string,List<List<string>>> MaximumMatchingWordPtr = set.SimplifiedChinese == false ? get.MaximumMatchingWordTw : get.MaximumMatchingWordCn;

      bool haveHandle = false;
      for(int segedIndex = 0; segedIndex + 1 < seged.Count; segedIndex++)
      {// 拆數字 [拾壹億(Neu) 元(Nf) /拾壹(Neu) 億元(A) ]
        if(seged[segedIndex + 1].Length > 1
            && MaximumMatchingWordPtr.ContainsKey(seged[segedIndex + 1]) == false
            && RuleBased.Neu.IsNeu(seged[segedIndex])
            && (RuleBased.Neu.IsNeu(seged[segedIndex + 1][0].ToString())))
        {
          List<string> posed = new List<string>();
          ToPoS.Handle(get,set,seged,posed,false);

          seged.Insert(segedIndex + 1,seged[segedIndex + 1].Substring(0,1));
          posed.Insert(segedIndex + 1,"Neu");
          seged[segedIndex + 2] = seged[segedIndex + 2].Substring(1);
          posed[segedIndex + 2] = "Na";
          haveHandle = true;
        }
      }
      return haveHandle;
    }
  }
}
