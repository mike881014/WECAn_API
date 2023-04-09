using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WECAnAPI.RuleBased
{
  static class RuleBased
  {//RuleBased處理狀態
    public static void Handle(Get get,Set set,string sentence,List<string> seged,List<string> posed)
    {
      bool haveHandle = false;

      haveHandle |= FW.Handle(get,seged,posed);
      haveHandle = false;
      haveHandle |= Neu.Handle(get,seged,posed);
      haveHandle |= Nd.Handle人工(get,seged,posed);
      if(haveHandle == true)
      {
        posed.Clear();
        ToPoS.Handle(get,set,seged,posed,set.SimplePOSTransfer);
      }
      haveHandle |= MergeSpecialRule.Handle(get,set,seged,posed);
      haveHandle |= Patches.Handle(get,set,seged,posed);
    }
  }
}
