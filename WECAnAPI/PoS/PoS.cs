using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WECAnAPI
{
  public class PoS
  {
    public struct WordPoS
    {
      public string PoS;
      public double PoSNum;
      public WordPoS(string _PoS,double _PoSNum)
      {
        PoS = _PoS;
        PoSNum = _PoSNum;
      }
    }
    public struct SnP
    {
      public List<string> Seged;
      public List<string> PoSed;
      public SnP(List<string> _Seged,List<string> _PoSed)
      {
        Seged = _Seged;
        PoSed = _PoSed;
      }
    }
    public static void ReadFileSnP(string path,Encoding encoding,List<SnP> result)
    {
      result.Clear();

      foreach(string readLine in Table.ReadCommentFileToArray(path,encoding,new string[] { },new char[] { '\r','\n' }))
      {
        List<string> seged = new List<string>();
        List<string> posed = new List<string>();
        string[] readLineSplit = readLine.Split(new char[] { '(',')','　' },StringSplitOptions.RemoveEmptyEntries);

        for(int forCount = 0; forCount < readLineSplit.Count(); forCount += 2)
        {
          seged.Add(readLineSplit[forCount]);
          posed.Add(readLineSplit[forCount + 1]);
        }

        result.Add(new SnP(seged,posed));
      }
    }
    public static void ToSimplePoS(List<string> posed)
    {
      for(int forCount = 0; forCount < posed.Count(); forCount++)
      {
        posed[forCount] = ToSimplePoS(posed[forCount]);
      }
    }
    public static string ToSimplePoS(string pos)
    {
      switch(pos)
      {
        case "A":
          return "A";
        case "Caa":
          return "Caa";
        case "Cab":
          return "Cab";
        case "Cba":
        case "Cbab":
          return "Cba";
        case "Cbb":
        case "Cbaa":
        case "Cbba":
        case "Cbbb":
        case "Cbca":
        case "Cbcb":
          return "Cbb";
        case "D":
        case "Dab":
        case "Dbaa":
        case "Dbab":
        case "Dbb":
        case "Dbc":
        case "Dc":
        case "Dd":
        case "Dg":
        case "Dh":
        case "Dj":
          return "D";
        case "Da":
        case "Daa":
          return "Da";
        case "DE":
          return "DE";
        case "Dfa":
          return "Dfa";
        case "Dfb":
          return "Dfb";
        case "Di":
          return "Di";
        case "Dk":
          return "Dk";
        case "FW":
          return "FW";
        case "I":
          return "I";
        case "Na":
        case "Naa":
        case "Nab":
        case "Nac":
        case "Nad":
        case "Naea":
        case "Naeb":
          return "Na";
        case "Nb":
        case "Nba":
        case "Nbc":
          return "Nb";
        case "Nc":
        case "Nca":
        case "Ncb":
        case "Ncc":
        case "Nce":
          return "Nc";
        case "Ncd":
        case "Ncda":
        case "Ncdb":
          return "Ncd";
        case "Nd":
        case "Ndaa":
        case "Ndab":
        case "Ndc":
        case "Ndd":
          return "Nd";
        case "Nep":
          return "Nep";
        case "Neqa":
          return "Neqa";
        case "Neqb":
          return "Neqb";
        case "Nes":
          return "Nes";
        case "Neu":
          return "Neu";
        case "Nf":
        case "Nfa":
        case "Nfb":
        case "Nfc":
        case "Nfd":
        case "Nfe":
        case "Nfg":
        case "Nfh":
        case "Nfi":
          return "Nf";
        case "Ng":
          return "Ng";
        case "Nh":
        case "Nhaa":
        case "Nhab":
        case "Nhac":
        case "Nhb":
        case "Nhc":
          return "Nh";
        case "SHI":
        case "V_11":
          return "SHI";
        case "T":
        case "Ta":
        case "Tb":
        case "Tc":
        case "Td":
          return "T";
        case "VA":
        case "VA11":
        case "VA12":
        case "VA13":
        case "VA3":
        case "VA4":
          return "VA";
        case "VAC":
        case "VA2":
          return "VAC";
        case "VB":
        case "VB11":
        case "VB12":
        case "VB2":
          return "VB";
        case "VC":
        case "VC2":
        case "VC31":
        case "VC32":
        case "VC33":
          return "VC";
        case "VCL":
        case "VC1":
          return "VCL";
        case "VD":
        case "VD1":
        case "VD2":
          return "VD";
        case "VE":
        case "VE11":
        case "VE12":
        case "VE2":
          return "VE";
        case "VF":
        case "VF1":
        case "VF2":
          return "VF";
        case "VG":
        case "VG1":
        case "VG2":
          return "VG";
        case "VH":
        case "VH11":
        case "VH12":
        case "VH13":
        case "VH14":
        case "VH15":
        case "VH17":
        case "VH21":
          return "VH";
        case "VHC":
        case "VH16":
        case "VH22":
          return "VHC";
        case "VI":
        case "VI1":
        case "VI2":
        case "VI3":
          return "VI";
        case "VJ":
        case "VJ1":
        case "VJ2":
        case "VJ3":
          return "VJ";
        case "VK":
        case "VK1":
        case "VK2":
          return "VK";
        case "VL":
        case "VL1":
        case "VL2":
        case "VL3":
        case "VL4":
          return "VL";
        case "V_2":
          return "V_2";
        case "Nv":
        case "Nv1":
        case "Nv2":
        case "Nv3":
        case "Nv4":
          return "Nv";
        case "b":
          return "b";
        case "EXCLAMATIONCATEGORY":
        case "EXCLANATIONCATEGORY":
        case "PARENTHESISCATEGORY":
        case "SEMICOLONCATEGORY":
        case "QUESTIONCATEGORY":
        case "SPCHANGECATEGORY":
        case "PERIODCATEGORY":
        case "COLONCATEGORY":
        case "COMMACATEGORY":
        case "PAUSECATEGORY":
        case "DASHCATEGORY":
        case "ETCCATEGORY":
        case "DOTCATEGORY":
        case "notword":
          return "notword";
        case "P":
        case "P01":
        case "P02":
        case "P03":
        case "P04":
        case "P05":
        case "P06":
        case "P07":
        case "P08":
        case "P09":
        case "P10":
        case "P11":
        case "P12":
        case "P13":
        case "P14":
        case "P15":
        case "P16":
        case "P17":
        case "P18":
        case "P19":
        case "P20":
        case "P21":
        case "P22":
        case "P23":
        case "P24":
        case "P25":
        case "P26":
        case "P27":
        case "P28":
        case "P29":
        case "P30":
        case "P31":
        case "P32":
        case "P33":
        case "P34":
        case "P35":
        case "P36":
        case "P37":
        case "P38":
        case "P39":
        case "P40":
        case "P41":
        case "P42":
        case "P43":
        case "P44":
        case "P45":
        case "P46":
        case "P47":
        case "P48":
        case "P49":
        case "P50":
        case "P51":
        case "P52":
        case "P53":
        case "P54":
        case "P55":
        case "P56":
        case "P57":
        case "P58":
        case "P59":
        case "P60":
        case "P61":
        case "P62":
        case "P63":
        case "P64":
        case "P65":
        case "P66":
          return "P";
        default:
          return pos;
      }
    }
    public static bool IsPoS(string pos)
    {
      switch(pos)
      {
        case "A":
        case "Caa":
        case "Cab":
        case "Cba":
        case "Cbab":
        case "Cbb":
        case "Cbaa":
        case "Cbba":
        case "Cbbb":
        case "Cbca":
        case "Cbcb":
        case "D":
        case "Dab":
        case "Dbaa":
        case "Dbab":
        case "Dbb":
        case "Dbc":
        case "Dc":
        case "Dd":
        case "Dg":
        case "Dh":
        case "Dj":
        case "Da":
        case "Daa":
        case "DE":
        case "Dfa":
        case "Dfb":
        case "Di":
        case "Dk":
        case "FW":
        case "I":
        case "Na":
        case "Naa":
        case "Nab":
        case "Nac":
        case "Nad":
        case "Naea":
        case "Naeb":
        case "Nb":
        case "Nba":
        case "Nbc":
        case "Nc":
        case "Nca":
        case "Ncb":
        case "Ncc":
        case "Nce":
        case "Ncd":
        case "Ncda":
        case "Ncdb":
        case "Nd":
        case "Ndaa":
        case "Ndab":
        case "Ndc":
        case "Ndd":
        case "Nep":
        case "Neqa":
        case "Neqb":
        case "Nes":
        case "Neu":
        case "Nf":
        case "Nfa":
        case "Nfb":
        case "Nfc":
        case "Nfd":
        case "Nfe":
        case "Nfg":
        case "Nfh":
        case "Nfi":
        case "Ng":
        case "Nh":
        case "Nhaa":
        case "Nhab":
        case "Nhac":
        case "Nhb":
        case "Nhc":
        case "SHI":
        case "V_11":
        case "T":
        case "Ta":
        case "Tb":
        case "Tc":
        case "Td":
        case "VA":
        case "VA11":
        case "VA12":
        case "VA13":
        case "VA3":
        case "VA4":
        case "VAC":
        case "VA2":
        case "VB":
        case "VB11":
        case "VB12":
        case "VB2":
        case "VC":
        case "VC2":
        case "VC31":
        case "VC32":
        case "VC33":
        case "VCL":
        case "VC1":
        case "VD":
        case "VD1":
        case "VD2":
        case "VE":
        case "VE11":
        case "VE12":
        case "VE2":
        case "VF":
        case "VF1":
        case "VF2":
        case "VG":
        case "VG1":
        case "VG2":
        case "VH":
        case "VH11":
        case "VH12":
        case "VH13":
        case "VH14":
        case "VH15":
        case "VH17":
        case "VH21":
        case "VHC":
        case "VH16":
        case "VH22":
        case "VI":
        case "VI1":
        case "VI2":
        case "VI3":
        case "VJ":
        case "VJ1":
        case "VJ2":
        case "VJ3":
        case "VK":
        case "VK1":
        case "VK2":
        case "VL":
        case "VL1":
        case "VL2":
        case "VL3":
        case "VL4":
        case "V_2":
        case "Nv":
        case "Nv1":
        case "Nv2":
        case "Nv3":
        case "Nv4":
        case "b":
        case "EXCLAMATIONCATEGORY":
        case "EXCLANATIONCATEGORY":
        case "PARENTHESISCATEGORY":
        case "SEMICOLONCATEGORY":
        case "QUESTIONCATEGORY":
        case "SPCHANGECATEGORY":
        case "PERIODCATEGORY":
        case "COLONCATEGORY":
        case "COMMACATEGORY":
        case "PAUSECATEGORY":
        case "DASHCATEGORY":
        case "ETCCATEGORY":
        case "DOTCATEGORY":
        case "notword":
        case "P":
        case "P01":
        case "P02":
        case "P03":
        case "P04":
        case "P05":
        case "P06":
        case "P07":
        case "P08":
        case "P09":
        case "P10":
        case "P11":
        case "P12":
        case "P13":
        case "P14":
        case "P15":
        case "P16":
        case "P17":
        case "P18":
        case "P19":
        case "P20":
        case "P21":
        case "P22":
        case "P23":
        case "P24":
        case "P25":
        case "P26":
        case "P27":
        case "P28":
        case "P29":
        case "P30":
        case "P31":
        case "P32":
        case "P33":
        case "P34":
        case "P35":
        case "P36":
        case "P37":
        case "P38":
        case "P39":
        case "P40":
        case "P41":
        case "P42":
        case "P43":
        case "P44":
        case "P45":
        case "P46":
        case "P47":
        case "P48":
        case "P49":
        case "P50":
        case "P51":
        case "P52":
        case "P53":
        case "P54":
        case "P55":
        case "P56":
        case "P57":
        case "P58":
        case "P59":
        case "P60":
        case "P61":
        case "P62":
        case "P63":
        case "P64":
        case "P65":
        case "P66":
          return true;
        default:
          return false;
      }
    }
  }
}
