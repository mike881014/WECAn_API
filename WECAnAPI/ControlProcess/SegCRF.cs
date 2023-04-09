using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace WECAnAPI
{
  static class SegCRF
  {
    public struct value/*宣告結構*/
    {
      public double p;
      public string sign;
    }
    
    #region 無參考
    public static List<string> Segment(Set set,Get get,string data)/*虛引數傳入處理字串後先計算BIES標籤，並返回依據標籤所切割出來的詞陣列*/
    {
      List<string> seg = new List<string>();
      string sign = CountBIES(set,get,data);/*傳入要處理的字串，取得最高機率的可能BIES標籤字串*/
      int start = 0, count = 0;
      while(start + count < data.Length)/*依據BIES標籤切割字串成陣列*/
      {
        if(sign[start + count] == 'S')
        {
          seg.Add(data.Substring(start,1));
          start++;
          count = 0;
        }
        else if(sign[start + count] == 'B' || sign[start + count] == 'I')
        {
          count++;
        }
        else if(sign[start + count] == 'E')
        {
          count++;
          seg.Add(data.Substring(start,count));
          start += count;
          count = 0;
        }
      }
      return seg;
    }
    #endregion

    public static double Probability(Set set,Get get,char front,char rear,string t1,string t2)/*虛引數輸入兩個連續的單字，在指定BIES的條件計算可能的連結機率並返回數值*/
    {
      int Findex, Rindex;
      switch(front)
      {
        case 'B': Findex = 0; break;
        case 'I': Findex = 1; break;
        case 'E': Findex = 2; break;
        case 'S': Findex = 3; break;
        default: Findex = -1; break;
      }
      switch(rear)
      {
        case 'B': Rindex = 0; break;
        case 'I': Rindex = 1; break;
        case 'E': Rindex = 2; break;
        case 'S': Rindex = 3; break;
        default: Rindex = -1; break;
      }
      if(get.TrainingDataContainsKey(set,t1) == false) get.TrainingDataAdd(set,t1,new double[] { 0.25,0.25,0.25,0.25 });/*存有不確定性，許曜麒學長認為S的分數應該要偏高，可是他忘了後面註解的由來而不肯定*///凡是不存在的字就直接給於最高S
      if(get.TrainingDataContainsKey(set,t2) == false) get.TrainingDataAdd(set,t2,new double[] { 0.25,0.25,0.25,0.25 });
      return Math.Exp(get.BIES(set,front.ToString())[Rindex] + get.TrainingData(set,t1)[Findex] + get.TrainingData(set,t2)[Rindex]);/*ln(前字標籤連結後字標籤的機率+前字以某個BIES出現的機率+後字以某個BIES出現的機率)*/
    }
    public static value CallProbability(Set set,Get get,char key,string str,int index)/*虛引數傳入指定位置的索引、起始的BIES標籤以及原始字串，經由遞迴計算到句子結尾返回各分支兩兩比較中最佳的 數值-BIES 結構給呼叫處*/
    {
      value v;
      if(index >= str.Length) { v.p = -1; v.sign = ""; return v; }/*如果破表表示來到遞迴返回點*/
      if(get.TrainingDataContainsKey(set,str[index].ToString()) == false)/*如果該字元沒有以BIES出現的機率則離開*///不存在(意旨標點符號)，回傳特殊值並停止遞回繼續
      {
        v.p = -1;
        v.sign = "";
        return v;
      }
      else
      {
        value v1, v2;
        char rear1, rear2;
        switch(key)
        {
          case 'B':
            rear1 = 'I'; rear2 = 'E';
            break;
          case 'I':
            rear1 = 'E'; rear2 = 'I';
            break;
          case 'E':
            rear1 = 'B'; rear2 = 'S';
            break;
          case 'S':
            rear1 = 'B'; rear2 = 'S';
            break;
          default:
            rear1 = 'X'; rear2 = 'X';
            break;
        }
        v1 = CallProbability(set,get,rear1,str,index + 1);/*遞迴計算*/
        v2 = CallProbability(set,get,rear2,str,index + 1);
        if(v1.p == -1 && v2.p == -1 && v1.sign == "" && v2.sign == "")/*得到表示為結尾性字的資訊*///此key為結尾(下一個是標點符號)
        {//要全力防止這條機率成功所以要給予p極小值(因為B、I是不可能當作結尾的)
          if(key == 'B' || key == 'I') { v.p = -10000000; v.sign = key.ToString(); return v; }
          else { v.p = 0; v.sign = key.ToString(); return v; }//如果可以是結尾的，就回傳正常數值
        }
        if(v1.p >= v2.p)/*遞迴返回階段，選擇遞迴分支中連結機率較高的組合返回*/
        {
          v = v1;
          v.p += Probability(set,get,key,rear1,str[index].ToString(),str[index + 1].ToString());
          v.sign = key + v.sign;
        }
        else
        {
          v = v2;
          v.p += Probability(set,get,key,rear2,str[index].ToString(),str[index + 1].ToString());
          v.sign = key + v.sign;
        }
        return v;
      }
    }
    
    #region 將字串加上標籤並回傳
    public static string CountBIES(Set set,Get get,string data)/*虛引數傳入要處理的字串，經由標記標籤做取得最高機率的可能後返回BIES標籤字串*///CRF高階函式
    {
      List<string> list = regularWord(data);/*將文章由標點符號或是英文數字等切割成陣列*///經過regularWord()切割句子(以標點符號當分割基準)
      string label = null;
      for(int i = 0; i < list.Count; i++)
      {
        if(i < list.Count - 1) label += BidirectionBIESOfBestCombination(set,get,list[i]) + "S";/*呼叫方法傳入句子，得到BIES標籤字串並填入S做分隔*/
        else label += BidirectionBIESOfBestCombination(set,get,list[i]);/*呼叫方法傳入句子，得到BIES標籤字串*/
      }
      return label;
    }
    #endregion
    
    #region 將字串以英文數字，標點符號分開
    static List<string> regularWord(string input)/*虛引數輸入連續的字元序列並由將ENTER字元替換以及以非中文的符號分割後，回傳切割好的字串陣列*///標準分割式
    {
      List<string> word = new List<string>();
      //char[] c = { ' ', '　', '\0' };
      input = input.Replace("\r\n","，，");
      string pattern = @"(\d)?([a-z])?([A-Z])?([(，。.)：；‧！？／%=-_、「」…）（《》－─—Ⅲ．％℃])?(\*)?(\+)?(\$)?(\^)?(\[)?(\])?(\<)?(\>)?(\')?([ａｂｃｄｅｃｆｇｈｉｊｋｌｍｎｏｐｑｒｓｔｕｖｗｘｙｚＡＢＣＤＥＦＧＨＩＪＫＬＭＮＯＰＱＲＳＴＵＶＷＸＹＺ])?";
      //string 可能詞類 = Regex.Replace(input, pattern, "");
      char[] symbol = pattern.ToCharArray();//規則運算式
      string[] reg = input.Split(symbol);
      for(int i = 0; i < reg.Length; i++)
      {
        word.Add(reg[i]);
      }
      return word;
    }
    #endregion
    
    #region 無參考
    public static string CreateBIESOfBestCombination(Set set,Get get,string str)/*虛引數輸入字元後先以遞迴建立標籤組合，再計算組合的機率後回傳最有可能的標籤*/
    {
      List<string> Combination = new List<string>();
      PushLabel(str.Length - 1,"B",Combination);/*遞迴式建立標籤組合*/
      PushLabel(str.Length - 1,"S",Combination);
      double BestValue = -1;
      string BestLabel = null;
      for(int i = 0; i < Combination.Count; i++)
      {
        double reg_value = ReturnProbabilityValue(set,get,str,Combination[i]);/*求取各種情況的分數並保留最好的情況*/
        if(reg_value > BestValue)
        {
          BestValue = reg_value;
          BestLabel = Combination[i];
        }
      }
      return BestLabel;
    }
    #endregion 

    public static bool PushLabel(int index,string label,List<string> Combination)/*遞迴式，由BIES標籤進入遞迴組合各種標籤連結可能的情況至陣列內後結束遞迴跳出*/
    {
      if(index > 0 && (label[label.Length - 1] == 'E' || label[label.Length - 1] == 'S'))
      {
        PushLabel(index - 1,label + "B",Combination);
        PushLabel(index - 1,label + "S",Combination);
      }
      if(index > 0 && (label[label.Length - 1] == 'B' || label[label.Length - 1] == 'I'))
      {
        PushLabel(index - 1,label + "I",Combination);
        PushLabel(index - 1,label + "E",Combination);
      }
      if(index <= 1 && (label[label.Length - 1] == 'E' || label[label.Length - 1] == 'S'))
      {
        Combination.Add(label);
        return true;
      }
      else return false;
    }
    public static double ReturnProbabilityValue(Set set,Get get,string str,string label)/*虛引數傳入對應的字元序列與其中一種對應的BIES標籤，由字出現某標籤以及BIES相互連結的機率做連續運算並返回機率值*/
    {
      if(str.Length == label.Length)/*標籤的長度要是跟句長一樣才進入算分*/
      {
        double value = 1;
        for(int i = 0; i < str.Length; i++)
        {
          int Findex = -1, Rindex = -1;
          switch(label[i])
          {
            case 'B': Findex = 0; break;
            case 'I': Findex = 1; break;
            case 'E': Findex = 2; break;
            case 'S': Findex = 3; break;
            default: Findex = -1; break;
          }
          if(i != str.Length - 1)//會超出陣列範圍
          {
            switch(label[i + 1])
            {
              case 'B': Rindex = 0; break;
              case 'I': Rindex = 1; break;
              case 'E': Rindex = 2; break;
              case 'S': Rindex = 3; break;
              default: Rindex = -1; break;
            }
          }
          if(get.TrainingDataContainsKey(set,str[i].ToString()) == false) get.TrainingDataAdd(set,str[i].ToString(),new double[] { 0.25,0.25,0.25,0.25 });//凡是不存在的字就直接給於最高S
          if(i < str.Length - 1) value *= get.TrainingData(set,str[i].ToString())[Findex] * get.BIES(set,label[i].ToString())[Rindex];
          else value *= get.TrainingData(set,str[i].ToString())[Findex];/*某字以某個標籤出現的機率 * 某個標籤後連結某個標籤出現的機率*/
        }
        return value;
      }
      else return -1;
    }
    
    #region 未寫完，無參考
    public static List<string> BidirectionCombination(int index)/*(殘缺)虛引數傳入想組合的長度，迴圈組合得到BIES的各種可能合理的排列*/
    {
      List<string> front = new List<string>();
      List<string> rear = new List<string>();
      string[] label = { "I","E","B","S" };
      int mid = index / 2;
      //前半部
      for(int i = 0; i < index; i++)
      {
        if(i == 0) { front.Add("B"); front.Add("S"); }
        else
        {
          for(int j = 1; j < front.Count; j += 2)
          {
            front.Insert(j,front[j - 1]);
          }
          front.Add(front[front.Count - 1]);
          for(int j = 0; j < front.Count; j++)
          {
            front[j] += label[j % 4];
          }
        }
      }
      for(int i = 0; i < index; i++)
      {
        if(front[i][front[i].Length - 1] == 'B' || front[i][front[i].Length - 1] == 'I')
        {
          front.RemoveAt(i);
          i--;
        }
      }
      //後半部
      //for (int forCount = mid; forCount < forCount; forCount++)
      //{
      //    if (forCount == mid) { front.Add("E"); front.Add("S"); }
      //    else
      //}
      return front;
    }
    #endregion

    public static double CountLabelProbabilities(Set set,Get get,string str,string sign)/*虛引數傳入字元部分子序列與對應的標籤，返回由字元的BIES機率與BIES連結BIES雙重計算得到的數值*/
    {
      double value = 1;
      for(int i = 0; i < str.Length; i++)
      {
        int label = -1;
        switch(sign[i])
        {
          case 'B': label = 0; break;
          case 'I': label = 1; break;
          case 'E': label = 2; break;
          case 'S': label = 3; break;
        }
        if(get.TrainingDataContainsKey(set,str[i].ToString()) == true) value *= get.TrainingData(set,str[i].ToString())[label];/*如果這個字元在BIES機率字典則將分數乘上機率*/
        else/*如果字元不在BIES的機率字典內則給予一個各1/4機率的可能後再處理*/
        {
          get.TrainingDataAdd(set,str[i].ToString(),new double[] { 0.25,0.25,0.25,0.25 });
          value *= get.TrainingData(set,str[i].ToString())[label];
        }
      }
      for(int i = 0; i < str.Length - 1; i++)/*將字元以BIES出現的機率計算得到的數值，再以BIES連結BIES的機率做深入計算*/
      {
        int label = -1;
        switch(sign[i + 1])
        {
          case 'B': label = 0; break;
          case 'I': label = 1; break;
          case 'E': label = 2; break;
          case 'S': label = 3; break;
        }
        value *= get.BIES(set,sign[i].ToString())[label];
      }
      return value;/*返回連續序列經過(字元以BIES出現的機率)*(BIES連結BIES的機率)得到的數值*/
    }
    
    #region 結合處理分析中心
    public static string BidirectionBIESOfBestCombination(Set set,Get get,string str)/*虛引數傳入句子，返回BIES標籤字串，在這之中使用遞迴處理文句長度介於2~19字元的句子，過長的句子直接放棄處理*///計算各種組合，並選出最好的組合
    {
      SortedDictionary<double,string> FrontList = new SortedDictionary<double,string>();/*存放句子前半部BIES組合的集合*/
      SortedDictionary<double,string> RearList = new SortedDictionary<double,string>();/*存放句子後半部BIES組合的集合*/

      if(str.Length == 1) return "S";/*單字元直接下標記*/
      else if(str.Length == 0) return "";
      else if(str.Length >= 20)//當字串大於20時，CRF處理效能已變為極差，需要直接略過，否則無法順利斷詞
      {
        string singal = null;
        for(int i = 0; i < str.Length; i++) singal += "S";/*放棄處理直接填S*/
        return singal;
      }
      else
      {
        if(get.TrainingDataContainsKey(set,str[0].ToString()) == false) get.TrainingDataAdd(set,str[0].ToString(),new double[] { 0.25,0.25,0.25,0.25 });/*如果句子開頭第一個字不在字典中則各填入1/4的BIES機率*///當出現字不存在的情形時，自動新增初始的數值
        FrontTree(set,get,1,"B",str,get.TrainingData(set,str[0].ToString())[0],FrontList);/*句子從B開頭遞迴推算組合*/
        FrontTree(set,get,1,"S",str,get.TrainingData(set,str[0].ToString())[3],FrontList);/*句子從S開頭遞迴推算組合*/
        if(get.TrainingDataContainsKey(set,str[str.Length - 1].ToString()) == false) get.TrainingDataAdd(set,str[str.Length - 1].ToString(),new double[] { 0.25,0.25,0.25,0.25 });/*如果句子結尾最後一個字不在字典中則各填入各1/4的機率*/
        RearTree(set,get,str.Length - 2,"E",str,get.TrainingData(set,str[str.Length - 1].ToString())[2],RearList);/*句子從E結尾遞迴推算組合*/
        RearTree(set,get,str.Length - 2,"S",str,get.TrainingData(set,str[str.Length - 1].ToString())[3],RearList);/*句子從S結尾遞迴推算組合*/
        KeyValuePair<double,string>[] FrontList_pair = FrontList.ToArray();/*將從頭開始看的組合結果轉到陣列*/
        KeyValuePair<double,string>[] RearList_pair = RearList.ToArray();/*將結尾開始看的組合結果轉到陣列*/
        double best_double = 0;
        string best_string = null;
        int iMAX = FrontList_pair.Length / 2, jMAX = RearList_pair.Length / 2;

        for(int i = FrontList_pair.Length - 1; i >= iMAX; i--)/*句子前半部組合最高機率的半部*/
        {
          for(int j = RearList_pair.Length - 1; j >= jMAX; j--)/*句子後半部組合最高機率的半部*/
          {
            double reg = IsCombinationRight(set,get,FrontList_pair[i].Value,RearList_pair[j].Value,FrontList_pair[i].Key , RearList_pair[j].Key);/*計算前半部結合後半部的分數*/
            if(reg > 0 && i == FrontList_pair.Length - 1)/*與學長確認過，效率考量下前半部最高的可能只會配後半部一組*/
            {
              best_double = reg; best_string = FrontList_pair[i].Value + RearList_pair[j].Value;
              break;
            }
            else if(reg > best_double)
            {
              best_double = reg; best_string = FrontList_pair[i].Value + RearList_pair[j].Value;
              break;
            }
          }
          if(best_double == 0 && best_string == null && iMAX >= 0 && jMAX >= 0)
          {
            iMAX--; jMAX--;
          }
        }

        return best_string;
      }
    }
    #endregion
    
    #region 無參考
    public static KeyValuePair<string,double> BidirectionBIESOfBestCombination_LabelValue(Set set,Get get,string str)/*虛引數傳入句子，返回BIES標籤字串與機率數值對*///計算各種組合，並回傳最好的組合&機率值(for健良)
    {
      SortedDictionary<double,string> FrontList = new SortedDictionary<double,string>();
      SortedDictionary<double,string> RearList = new SortedDictionary<double,string>();

      if(str.Length == 1) return new KeyValuePair<string,double>("S",1.0);
      else if(str.Length == 0) return new KeyValuePair<string,double>("",0.0);
      else
      {
        if(get.TrainingDataContainsKey(set,str[0].ToString()) == false) get.TrainingDataAdd(set,str[0].ToString(),new double[] { 0.25,0.25,0.25,0.25 });//當出現字不存在的情形時，自動新增初始的數值
        FrontTree(set,get,1,"B",str,get.TrainingData(set,str[0].ToString())[0],FrontList);//分兩段機算機率值，加快計算速度
        FrontTree(set,get,1,"S",str,get.TrainingData(set,str[0].ToString())[3],FrontList);//此行為前段，採遞迴方式
        if(get.TrainingDataContainsKey(set,str[str.Length - 1].ToString()) == false) get.TrainingDataAdd(set,str[str.Length - 1].ToString(),new double[] { 0.25,0.25,0.25,0.25 });
        RearTree(set,get,str.Length - 2,"E",str,get.TrainingData(set,str[str.Length - 1].ToString())[2],RearList);//後段，也是遞迴
        RearTree(set,get,str.Length - 2,"S",str,get.TrainingData(set,str[str.Length - 1].ToString())[3],RearList);
        KeyValuePair<double,string>[] FrontList_pair = FrontList.ToArray();
        KeyValuePair<double,string>[] RearList_pair = RearList.ToArray();
        double best_double = 0;
        string best_string = null;
        int iMAX = FrontList_pair.Length / 2, jMAX = RearList_pair.Length / 2;//以前後段的組合拼湊最好的結果

        for(int i = FrontList_pair.Length - 1; i >= iMAX; i--)
        {
          for(int j = RearList_pair.Length - 1; j >= jMAX; j--)
          {
            double reg = IsCombinationRight(set,get,FrontList_pair[i].Value,RearList_pair[j].Value,FrontList_pair[i].Key,RearList_pair[j].Key);
            if(reg > 0 && i == FrontList_pair.Length - 1)
            {
              best_double = reg; best_string = FrontList_pair[i].Value + RearList_pair[j].Value;
              break;
            }
            else if(reg > best_double)
            {
              best_double = reg; best_string = FrontList_pair[i].Value + RearList_pair[j].Value;
              break;
            }
          }
          if(best_double == 0 && best_string == null && iMAX >= 0 && jMAX >= 0)
          {
            iMAX--; jMAX--;
          }
        }

        return new KeyValuePair<string,double>(best_string,best_double);
      }
    }
    #endregion
    
    #region 遞迴，將句子斷完詞後遞迴回這邊，例如:B開頭，會分成I跟E兩條支線，第一條BI，第二條BE以此類推
    public static bool FrontTree(Set set,Get get,int index,string label,string str,double value,SortedDictionary<double,string> FrontList)/*遞迴式，虛引數傳入當前索引值以及外層所傳入的已存在的標籤、原始字串、機率數值、存放結果的集合容器，經由遞迴由句子前端往後遞迴到句子的中間停止並建置結果到結果集合離開*/
    {
      if(index == str.Length / 2)/*如果索引值到達句子的一半長則遞迴結束開始建置進結果*/
      {
        while(FrontList.ContainsKey(value) == true) { value += 0.000000000001; }/*以數值當作索引所以避免重複*/
        FrontList.Add(value,label);
        return true;
      }
      else
      {
        if(get.TrainingDataContainsKey(set,str[index].ToString()) == false) get.TrainingDataAdd(set,str[index].ToString(),new double[] { 0.25,0.25,0.25,0.25 });
        if(label[label.Length - 1] == 'B' || label[label.Length - 1] == 'I')/*如果有後續連接性的BIES標籤*/
        {
          if(label.Length < 2)/*標籤長度2以下還不需要雙標籤連結下一個標籤的頻率*///當詞還未滿2個以前，還不需要*3標籤的BIES
          {
            FrontTree(set,get,index + 1,label + "I",str,value * get.TrainingData(set,str[index].ToString())[1]/* BIES[label[label.Length - 1].ToString()][1]*/,FrontList);/*將當前資訊帶入遞迴拼湊下一個連結可能，並將當前索引字以某個BIES機率的數值套入*/
            FrontTree(set,get,index + 1,label + "E",str,value * get.TrainingData(set,str[index].ToString())[2]/* BIES[label[label.Length - 1].ToString()][2]*/,FrontList);
          }
          else
          {
            FrontTree(set,get,index + 1,label + "I",str,value * get.TrainingData(set,str[index].ToString())[1]/* BIES[label[label.Length - 1].ToString()][1]*/ * get.BIES(set,label.Substring(label.Length - 2,2))[1],FrontList);/*將當前資訊帶入遞迴拼湊下一個連結可能，並將當前索引字以某個BIES機率的數值*BIES雙標籤連結下一個標籤的機率套入*/
            FrontTree(set,get,index + 1,label + "E",str,value * get.TrainingData(set,str[index].ToString())[2]/* BIES[label[label.Length - 1].ToString()][2]*/ * get.BIES(set,label.Substring(label.Length - 2,2))[2],FrontList);
          }
        }
        else if(label[label.Length - 1] == 'E' || label[label.Length - 1] == 'S')
        {
          if(label.Length < 2)//當詞還未滿2個以前，還不需要*3標籤的BIES
          {
            FrontTree(set,get,index + 1,label + "B",str,value * get.TrainingData(set,str[index].ToString())[0]/* BIES[label[label.Length - 1].ToString()][0]*/,FrontList);
            FrontTree(set,get,index + 1,label + "S",str,value * get.TrainingData(set,str[index].ToString())[3]/* BIES[label[label.Length - 1].ToString()][3]*/,FrontList);
          }
          else
          {
            FrontTree(set,get,index + 1,label + "B",str,value * get.TrainingData(set,str[index].ToString())[0]/* BIES[label[label.Length - 1].ToString()][0]*/ * get.BIES(set,label.Substring(label.Length - 2,2))[0],FrontList);
            FrontTree(set,get,index + 1,label + "S",str,value * get.TrainingData(set,str[index].ToString())[3]/* BIES[label[label.Length - 1].ToString()][3]*/ * get.BIES(set,label.Substring(label.Length - 2,2))[3],FrontList);
          }
        }
        return false;
      }
    }
    #endregion

    #region 另一半遞迴
    public static bool RearTree(Set set,Get get,int index,string label,string str,double value,SortedDictionary<double,string> RearList)/*遞迴式，虛引數傳入當前索引值以及外層所傳入的已存在的標籤、原始字串、機率數值、存放結果的集合容器，經由遞迴由句子尾端往前遞迴到句子的中間停止並建置結果到結果集合離開*/
    {
      if(index < str.Length / 2)
      {
        while(RearList.ContainsKey(value) == true) { value += 0.000000000001; }
        RearList.Add(value,label);
        return true;
      }
      else
      {
        int code_three = -1;
        if(label.Length >= 2)
        {
          switch(label[1])//之前label的code
          {
            case 'B': code_three = 0; break;
            case 'I': code_three = 1; break;
            case 'E': code_three = 2; break;
            case 'S': code_three = 3; break;
          }
        }
        if(get.TrainingDataContainsKey(set,str[index].ToString()) == false) get.TrainingDataAdd(set,str[index].ToString(),new double[] { 0.25,0.25,0.25,0.25 });
        if(label[0] == 'B' || label[0] == 'S')
        {
          if(label.Length < 2)//當詞還未滿2個以前，還不需要*3標籤的BIES
          {
            RearTree(set,get,index - 1,"E" + label,str,value * get.TrainingData(set,str[index].ToString())[2]/* BIES["E"][code]*/,RearList);
            RearTree(set,get,index - 1,"S" + label,str,value * get.TrainingData(set,str[index].ToString())[3]/* BIES["S"][code]*/,RearList);
          }
          else
          {
            RearTree(set,get,index - 1,"E" + label,str,value * get.TrainingData(set,str[index].ToString())[2]/* BIES["E"][code]*/ * get.BIES(set,"E" + label[0])[code_three],RearList);
            RearTree(set,get,index - 1,"S" + label,str,value * get.TrainingData(set,str[index].ToString())[3]/* BIES["S"][code]*/ * get.BIES(set,"S" + label[0])[code_three],RearList);
          }
        }
        else if(label[0] == 'I' || label[0] == 'E')
        {
          if(label.Length < 2)//當詞還未滿2個以前，還不需要*3標籤的BIES
          {
            RearTree(set,get,index - 1,"B" + label,str,value * get.TrainingData(set,str[index].ToString())[0]/* BIES["B"][code]*/,RearList);
            RearTree(set,get,index - 1,"I" + label,str,value * get.TrainingData(set,str[index].ToString())[1]/* BIES["I"][code]*/,RearList);
          }
          else
          {
            RearTree(set,get,index - 1,"B" + label,str,value * get.TrainingData(set,str[index].ToString())[0]/* BIES["B"][code]*/ * get.BIES(set,"B" + label[0])[code_three],RearList);
            RearTree(set,get,index - 1,"I" + label,str,value * get.TrainingData(set,str[index].ToString())[1]/* BIES["I"][code]*/ * get.BIES(set,"I" + label[0])[code_three],RearList);
          }
        }
        return false;
      }
    }
    #endregion

    public static double IsCombinationRight(Set set,Get get,string fs,string rs,double fv,double rv)/*虛引數傳入當前想判斷可能的前半部BIES標籤、後半部的BIES標籤，以及對應的半部所算出來的機率，並依照長度的情況套入各種連結機率的數學式子作耦合並回傳機率給呼叫處*/
    {
      //假設fs = BIES, rs = BEBE
      //則f1 = ES, f2 = SB
      //則r1 = B, r2 = E
      //回傳 fv * [ESB] * [SBE] * rv
      string f1, f2;
      char r1, r2;
      if(fs.Length == 1 && rs.Length == 1)/*長度各1則使用前面字的BIES連結後面字的BIES機率作耦合計算*/
      {
        f1 = fs[fs.Length - 1].ToString();
        r1 = rs[0];
        return fv * get.BIES(set,f1)[BIES_ToCode(r1)] * rv;
      }
      else if(fs.Length == 2 && rs.Length == 1)/*長度為2-1則使用前面字的雙位BIES連結後面字的BIES機率作耦合計算*/
      {
        f1 = fs[fs.Length - 2].ToString() + fs[fs.Length - 1].ToString();
        r1 = rs[0];
        return fv * get.BIES(set,f1)[BIES_ToCode(r1)] * rv;
      }
      else if(fs.Length == 1 && rs.Length == 2)/*長度為1-2則使用前面字的標籤與後面第一個字的標籤組成的雙位BIES連結最後面字的BIES機率作耦合計算*/
      {
        f2 = fs[fs.Length - 1].ToString() + rs[0].ToString();
        if(get.BIESContainsKey(set,f2) == false) return 0;//如果前後組合不可能存在，直接回傳機率值0
        r2 = rs[1];
        return fv * get.BIES(set,f2)[BIES_ToCode(r2)] * rv;
      }
      else/*如果前後組合都兩個字以上，則使用 (前半部最後兩個字的標籤連結後半部第一個字的標籤) * (前半部最後一個字的標籤與後面第一個字的標籤組成的雙位BIES連結後半部第二個字的BIES機率) 作耦合計算*/
      {
        f1 = fs[fs.Length - 2].ToString() + fs[fs.Length - 1].ToString();
        f2 = fs[fs.Length - 1].ToString() + rs[0].ToString();
        if(get.BIESContainsKey(set,f2) == false) return 0;//如果前後組合不可能存在，直接回傳機率值0
        r1 = rs[0];
        r2 = rs[1];
        return fv * get.BIES(set,f1)[BIES_ToCode(r1)] * get.BIES(set,f2)[BIES_ToCode(r2)] * rv;
      }
    }

    #region 無參考，將BIES標籤轉成數字，參數型態string
    public static int BIES_ToCode(string label)//將BIES標籤轉成數字，參數型態string
    {
      int code = -1;
      switch(label)
      {
        case "B": code = 0; break;
        case "I": code = 1; break;
        case "E": code = 2; break;
        case "S": code = 3; break;
      }
      return code;
    }
    #endregion
    
    #region 將BIES標籤轉成數字，參數型態char
    public static int BIES_ToCode(char label)//將BIES標籤轉成數字，參數型態char
    {
      int code = -1;
      switch(label)
      {
        case 'B': code = 0; break;
        case 'I': code = 1; break;
        case 'E': code = 2; break;
        case 'S': code = 3; break;
      }
      return code;
    }
    #endregion
  }
}
