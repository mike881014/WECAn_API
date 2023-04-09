using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WECAnAPI.RuleBased
{//合併英文並給予pos
  static class FW
  {
    public static bool Handle(Get get,List<string> seged,List<string> posed)
    {
      bool haveHandle = false;
      for(int forCount = 0; forCount < seged.Count(); forCount++)
      {
        if(英文s.Contains(seged[forCount]))
        {
          int startIndex = forCount;
          int endIndex = forCount;
          string str = seged[forCount];
          while(true)
          {
            if(endIndex + 1 < seged.Count() && 英文s.Contains(seged[endIndex + 1]))
            {
              str = str + seged[endIndex + 1];
              endIndex += 1;
              continue;
            }
            else if(endIndex + 2 < seged.Count() && 英文連結詞s.Contains(seged[endIndex + 1]) && 英文s.Contains(seged[endIndex + 2]))
            {
              str = str + seged[endIndex + 1] + seged[endIndex + 2];
              endIndex += 2;
              continue;
            }
            else
            {
              break;
            }
          }
          if(endIndex - startIndex > 0)
          {
            seged.RemoveRange(startIndex,endIndex - startIndex + 1);
            posed.RemoveRange(startIndex,endIndex - startIndex + 1);
            seged.Insert(startIndex,str);
            posed.Insert(startIndex,"FW");
            haveHandle = true;
          }
        }
      }
      return haveHandle;
    }
    public static bool IsFW(string word)
    {
      for(int forCount = 0; forCount < word.Length; forCount++)
      {
        if(英文c.Contains(word[forCount]))
        {
          continue;
        }
        else if(forCount + 1 < word.Length && 英文連結詞c.Contains(word[forCount]) && 英文c.Contains(word[forCount + 1]))
        {
          forCount += 1;
          continue;
        }
        else
        {
          return false;
        }
      }
      return true;
    }
    static char[] 英文c = {
                                'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',
                              'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z',

'ａ','ｂ','ｃ','ｄ','ｅ','ｆ','ｇ','ｈ','ｉ','ｊ','ｋ','ｌ','ｍ','ｎ','ｏ','ｐ','ｑ','ｒ','ｓ','ｔ','ｕ','ｖ','ｗ','ｘ','ｙ','ｚ',

'Ａ','Ｂ','Ｃ','Ｄ','Ｅ','Ｆ','Ｇ','Ｈ','Ｉ','Ｊ','Ｋ','Ｌ','Ｍ','Ｎ','Ｏ','Ｐ','Ｑ','Ｒ','Ｓ','Ｔ','Ｕ','Ｖ','Ｗ','Ｘ','Ｙ','Ｚ'};
    static string[] 英文s = {
                                  "a","b","c","d","e","f","g","h","i","j","k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z",
                               "A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z",

"ａ","ｂ","ｃ","ｄ","ｅ","ｆ","ｇ","ｈ","ｉ","ｊ","ｋ","ｌ","ｍ","ｎ","ｏ","ｐ","ｑ","ｒ","ｓ","ｔ","ｕ","ｖ","ｗ","ｘ","ｙ","ｚ",

"Ａ","Ｂ","Ｃ","Ｄ","Ｅ","Ｆ","Ｇ","Ｈ","Ｉ","Ｊ","Ｋ","Ｌ","Ｍ","Ｎ","Ｏ","Ｐ","Ｑ","Ｒ","Ｓ","Ｔ","Ｕ","Ｖ","Ｗ","Ｘ","Ｙ","Ｚ"};

    static char[] 英文連結詞c = { '‧','-','—',};
    static string[] 英文連結詞s = { "‧","-","—",};
  }
}
