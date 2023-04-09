using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WECAnAPI
{
  public partial class PoSCn : PoS
  {
    static PoSCn()
    {
      foreach(string readLine in PoSFreqTable())
      {
        string[] split = readLine.Split(new char[] { ' ',',' },StringSplitOptions.RemoveEmptyEntries);
        if(split.Length == 3)
        {
          Table.SetValue(co_occurrencePoS,split[0],split[1],double.Parse(split[2]),Table.SetValueOptions.Const);
        }
        else if(split.Length == 2)
        {
          Table.SetValue(co_occurrencePoSTotal,split[0],double.Parse(split[1]),Table.SetValueOptions.Const);
        }
        else
        {
          throw new ArgumentOutOfRangeException(readLine);
        }
      }
    }

    static Dictionary<string,Dictionary<string,double>> co_occurrencePoS = new Dictionary<string,Dictionary<string,double>>();
    static Dictionary<string,double> co_occurrencePoSTotal = new Dictionary<string,double>();
    public static double BeginFreq(string pos)
    {
      return Table.GetValue(co_occurrencePoS,"notword",ToSimplePoS(pos),0);
    }
    public static double PairFreq(string posA,string posB)
    {
      return Table.GetValue(co_occurrencePoS,ToSimplePoS(posA),ToSimplePoS(posB),0);
    }
    public static double EndFreq(string pos)
    {
      return Table.GetValue(co_occurrencePoS,ToSimplePoS(pos),"notword",0);
    }
    public static double TotalFreq(string pos)
    {
      return Table.GetValue(co_occurrencePoSTotal,ToSimplePoS(pos),0);
    }
  }
}
