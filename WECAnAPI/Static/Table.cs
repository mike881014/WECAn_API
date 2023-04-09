using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WECAnAPI
{
  public static class Table
  {
    public enum SetValueOptions
    {
      Append,
      Override,
      Const
    }
    #region 移除檔案中的描述
    public static string RemoveComment (string text) {
      if (text.Length < 2) {
        return text;
      }
      var list = new List<string> ();
      var l = 0; // left
      var r = 0; // right
      var p = 0; // previous right, be used to slice content
      var Skip_sign = false; // skip, to find next start without escaping
      var kl = 0; // skip-left, last escaping start + 1
      var end = text.Length;
      while (true) {
        // find next start comment mark from ptr
        // , if need skip, ptr is last start ofs + 1
        // , else ptr is last end comment mark
        l = text.IndexOf("/*", (Skip_sign ? kl : r)); 
        if (l > 0 && text[l - 1] == '\\') {
          Skip_sign = true;
          kl = l + 1; // set kl to last start + 1
          continue;
        }
        Skip_sign = false;
        if (l == -1) { // if not found, flush remaining text
          list.Add(text.Substring(r));
          break;
        }
        // after start comment mark, find end comment mark
        // , if arrive end, set right to -1
        // , else find end comment mark from left
        r = (l + 2 < end ? text.IndexOf("*/", l + 2) : -1);
        if (r == -1) {
          list.Add(text.Substring(p, l - p));
          break;
        } else {
          r += 2;
          list.Add(text.Substring(p, l - p));
          p = r;
        }
      }
      for (int i = list.Count - 1; i >= 0; i--) {
        list[i] = list[i].Replace(@"\/", @"/");
      }
      return string.Join("", list);
    }
    #endregion

    #region 將需更改的字詞分開 needChange.split(';').toList()
    public static List<string> SplitAndEscape (string text, char[] sepSet) {
      var sepofs = new List<int> (); // seperator offsets in text
      var list = new List<string> (); // output list
      var end = text.Length;
      foreach (var sep in sepSet) { // for each seperator
        var ptr = 0;
        while (true) {
          // find next sep in text
          // , if ptr already out of bound, set as not found
          // , else find sep from last sep's ofs
          ptr = (ptr < end ? text.IndexOf(sep, ptr) : -1);
          if (ptr == -1) { // not found, break
            break;
          }
          if (ptr > 0 && text[ptr - 1] == '\\') { // if sep's previous is '\', skip this
            ptr++;
            continue;
          }
          sepofs.Add(ptr); // put sep's offset in sepofs
          ptr++;
        }
      }
      sepofs.Sort();
      var pofs = 0; // previous offset
      foreach (var ofs in sepofs) { // for each offset in sepofs
        if (ofs - pofs > 0) { // if not empty segment
          list.Add(text.Substring(pofs, ofs - pofs));
        }
        pofs = ofs + 1;
      }
      if (text.Length - pofs > 0) { // last one, and not empty
        list.Add(text.Substring(pofs));
      }
      for (int i = 0, l = list.Count; i < l; i++) {
        foreach (var sep in sepSet) { // remove each escape of seperator
          list[i] = list[i].Replace(@"\" + sep, "" + sep);
        }
      }
      return list;
    }
    #endregion

    #region 初始化檔案，最初的第一步(第一個須讀的檔案為，需更改之字元Modification.TXT)
    public static List<string> ReadCommentFileToArray(string path,System.Text.Encoding encoding,string[] removeStringSet,char[] splitCharSet)
    {
      System.IO.StreamReader reader = new System.IO.StreamReader(path,encoding);
      string readToEnd = reader.ReadToEnd();
      reader.Close();
      readToEnd = RemoveComment(readToEnd);//只留下需更改資料
      foreach(string removeString in removeStringSet)
      {
        readToEnd = readToEnd.Replace(removeString,"");
      }
      return SplitAndEscape(readToEnd, splitCharSet);
    }
    #endregion
    
   
    public static bool ContainsKey<T0, T1>(Dictionary<T0,T1> dictionary,T0 key0)
    {
      if(dictionary.ContainsKey(key0) == true)
      {
        return true;
      }
      else
      {
        return false;
      }
    }
    public static bool ContainsKey<T0, T1, T2>(Dictionary<T0,Dictionary<T1,T2>> dictionary,T0 key0,T1 key1)
    {
      if(dictionary.ContainsKey(key0) == true && dictionary[key0].ContainsKey(key1) == true)
      {
        return true;
      }
      else
      {
        return false;
      }
    }
    public static bool ContainsKey<T0, T1, T2, T3>(Dictionary<T0,Dictionary<T1,Dictionary<T2,T3>>> dictionary,T0 key0,T1 key1,T2 key2)
    {
      if(dictionary.ContainsKey(key0) == true && dictionary[key0].ContainsKey(key1) == true && dictionary[key0][key1].ContainsKey(key2) == true)
      {
        return true;
      }
      else
      {
        return false;
      }
    }


    public static void SetValue<T0>(Dictionary<T0,double> dictionary,T0 key0,double value,SetValueOptions setValueOptions)
    {
      if(ContainsKey(dictionary,key0))
      {
        switch(setValueOptions)
        {
          case SetValueOptions.Append:
            dictionary[key0] += value;
            break;

          case SetValueOptions.Override:
            dictionary[key0] = value;
            break;

          case SetValueOptions.Const:
            throw new ArgumentException();
        }
      }
      else
      {
        dictionary.Add(key0,value);
      }
    }
    public static void SetValue<T0, T1>(Dictionary<T0,Dictionary<T1,double>> dictionary,T0 key0,T1 key1,double value,SetValueOptions setValueOptions)
    {
      if(ContainsKey(dictionary,key0,key1))
      {
        switch(setValueOptions)
        {
          case SetValueOptions.Append:
            dictionary[key0][key1] += value;
            break;

          case SetValueOptions.Override:
            dictionary[key0][key1] = value;
            break;

          case SetValueOptions.Const:
            throw new ArgumentException();
        }
      }
      else
      {
        if(dictionary.ContainsKey(key0) == false)
        {
          dictionary.Add(key0,new Dictionary<T1,double>());
        }
        dictionary[key0].Add(key1,value);
      }
    }
    public static void SetValue<T0, T1, T2>(Dictionary<T0,Dictionary<T1,Dictionary<T2,double>>> dictionary,T0 key0,T1 key1,T2 key2,double value,SetValueOptions setValueOptions)
    {
      if(ContainsKey(dictionary,key0,key1,key2))
      {
        switch(setValueOptions)
        {
          case SetValueOptions.Append:
            dictionary[key0][key1][key2] += value;
            break;

          case SetValueOptions.Override:
            dictionary[key0][key1][key2] = value;
            break;

          case SetValueOptions.Const:
            throw new ArgumentException();
        }
      }
      else
      {
        if(dictionary.ContainsKey(key0) == false)
        {
          dictionary.Add(key0,new Dictionary<T1,Dictionary<T2,double>>());
        }
        if(dictionary[key0].ContainsKey(key1) == false)
        {
          dictionary[key0].Add(key1,new Dictionary<T2,double>());
        }
        dictionary[key0][key1].Add(key2,value);
      }
    }
    public static double GetValue<T0>(Dictionary<T0,double> dictionary,T0 key0,double defaultValue)
    {
      if(ContainsKey(dictionary,key0))
      {
        return dictionary[key0];
      }
      else
      {
        return defaultValue;
      }
    }
    public static double GetValue<T0, T1>(Dictionary<T0,Dictionary<T1,double>> dictionary,T0 key0,T1 key1,double defaultValue)
    {
      if(ContainsKey(dictionary,key0,key1))
      {
        return dictionary[key0][key1];
      }
      else
      {
        return defaultValue;
      }
    }
    public static double GetValue<T0, T1, T2>(Dictionary<T0,Dictionary<T1,Dictionary<T2,double>>> dictionary,T0 key0,T1 key1,T2 key2,double defaultValue)
    {
      if(ContainsKey(dictionary,key0,key1,key2))
      {
        return dictionary[key0][key1][key2];
      }
      else
      {
        return defaultValue;
      }
    }
    public static void WriteTable<T0>(Dictionary<T0,double> dictionary,string path,System.Text.Encoding encoding,char splitChar)
    {
      System.IO.StreamWriter file = new System.IO.StreamWriter(path,false,encoding);
      foreach(KeyValuePair<T0,double> key0 in dictionary)
      {
        file.Write(key0.Key.ToString() + splitChar.ToString() + key0.Value.ToString() + ";\n");
      }
      file.Close();
    }
    public static void WriteTable<T0, T1>(Dictionary<T0,Dictionary<T1,double>> dictionary,string path,System.Text.Encoding encoding,char splitChar)
    {
      System.IO.StreamWriter file = new System.IO.StreamWriter(path,false,encoding);
      foreach(KeyValuePair<T0,Dictionary<T1,double>> key0 in dictionary)
      {
        foreach(KeyValuePair<T1,double> key1 in key0.Value)
        {
          file.Write(key0.Key.ToString() + splitChar.ToString() + key1.Key.ToString() + splitChar.ToString() + key1.Value.ToString() + ";\n");
        }
      }
      file.Close();
    }
    public static void WriteTable<T0, T1, T2>(Dictionary<T0,Dictionary<T1,Dictionary<T2,double>>> dictionary,string path,System.Text.Encoding encoding,char splitChar)
    {
      System.IO.StreamWriter file = new System.IO.StreamWriter(path,false,encoding);
      foreach(KeyValuePair<T0,Dictionary<T1,Dictionary<T2,double>>> key0 in dictionary)
      {
        foreach(KeyValuePair<T1,Dictionary<T2,double>> key1 in key0.Value)
        {
          foreach(KeyValuePair<T2,double> key2 in key1.Value)
          {
            file.Write(key0.Key.ToString() + splitChar.ToString() + key1.Key.ToString() + splitChar.ToString() + key2.Key.ToString() + splitChar.ToString() + key2.Value.ToString() + ";\n");
          }
        }
      }
      file.Close();
    }
    public static void ReadTable(Dictionary<char,double> dictionary,string path,System.Text.Encoding encoding,char[] splitCharSet)
    {
      foreach(string readLine in ReadCommentFileToArray(path,encoding,new string[] { "\r","\n" },new char[] { ';' }))
      {
        string[] split = readLine.Split(splitCharSet,StringSplitOptions.RemoveEmptyEntries);
        if(split.Length == 2 && split[0].Length == 1)
        {
          SetValue(dictionary,split[0][0],double.Parse(split[1]),SetValueOptions.Const);
        }
        else
        {
          throw new ArgumentOutOfRangeException("ReadTable(dictionary, path, encoding, char[] splitChar)");
        }
      }
    }
    public static void ReadTable(Dictionary<char,Dictionary<char,double>> dictionary,string path,System.Text.Encoding encoding,char[] splitCharSet)
    {
      foreach(string readLine in ReadCommentFileToArray(path,encoding,new string[] { "\r","\n" },new char[] { ';' }))
      {
        string[] split = readLine.Split(splitCharSet,StringSplitOptions.RemoveEmptyEntries);
        if(split.Length == 3 && split[0].Length == 1 && split[1].Length == 1)
        {
          SetValue(dictionary,split[0][0],split[1][0],double.Parse(split[2]),SetValueOptions.Const);
        }
        else
        {
          throw new ArgumentOutOfRangeException("ReadTable(dictionary, path, encoding, char[] splitChar)");
        }
      }
    }
    public static void ReadTable(Dictionary<char,Dictionary<char,double>> dictionary,Dictionary<char,double> subDictionary,string path,System.Text.Encoding encoding,char[] splitCharSet)
    {
      foreach(string readLine in ReadCommentFileToArray(path,encoding,new string[] { "\r","\n" },new char[] { ';' }))
      {
        string[] split = readLine.Split(splitCharSet,StringSplitOptions.RemoveEmptyEntries);
        if(split.Length == 3 && split[0].Length == 1 && split[1].Length == 1)
        {
          SetValue(dictionary,split[0][0],split[1][0],double.Parse(split[2]),SetValueOptions.Const);
        }
        else if(split.Length == 2 && split[0].Length == 1)
        {
          SetValue(subDictionary,split[0][0],double.Parse(split[1]),SetValueOptions.Const);
        }
        else
        {
          throw new ArgumentOutOfRangeException("ReadTable(dictionary, path, encoding, char[] splitChar)");
        }
      }
    }
    public static void ReadTable(Dictionary<char,Dictionary<char,Dictionary<char,double>>> dictionary,string path,System.Text.Encoding encoding,char[] splitCharSet)
    {
      foreach(string readLine in ReadCommentFileToArray(path,encoding,new string[] { "\r","\n" },new char[] { ';' }))
      {
        string[] split = readLine.Split(splitCharSet,StringSplitOptions.RemoveEmptyEntries);
        if(split.Length == 4 && split[0].Length == 1 && split[1].Length == 1 && split[2].Length == 1)
        {
          SetValue(dictionary,split[0][0],split[1][0],split[2][0],double.Parse(split[3]),SetValueOptions.Const);
        }
        else
        {
          throw new ArgumentOutOfRangeException("ReadTable(dictionary, path, encoding, char[] splitChar)");
        }
      }
    }
    public static void ReadTable(Dictionary<char,Dictionary<char,Dictionary<char,double>>> dictionary,Dictionary<char,Dictionary<char,double>> subDictionary,string path,System.Text.Encoding encoding,char[] splitCharSet)
    {
      foreach(string readLine in ReadCommentFileToArray(path,encoding,new string[] { "\r","\n" },new char[] { ';' }))
      {
        string[] split = readLine.Split(splitCharSet,StringSplitOptions.RemoveEmptyEntries);
        if(split.Length == 4 && split[0].Length == 1 && split[1].Length == 1 && split[2].Length == 1)
        {
          SetValue(dictionary,split[0][0],split[1][0],split[2][0],double.Parse(split[3]),SetValueOptions.Const);
        }
        else if(split.Length == 3 && split[0].Length == 1 && split[1].Length == 1)
        {
          SetValue(subDictionary,split[0][0],split[1][0],double.Parse(split[2]),SetValueOptions.Const);
        }
        else
        {
          throw new ArgumentOutOfRangeException("ReadTable(dictionary, path, encoding, char[] splitChar)");
        }
      }
    }
    public static void ReadTable(Dictionary<string,double> dictionary,string path,System.Text.Encoding encoding,char[] splitCharSet)
    {
      foreach(string readLine in ReadCommentFileToArray(path,encoding,new string[] { "\r","\n" },new char[] { ';' }))
      {
        string[] split = readLine.Split(splitCharSet,StringSplitOptions.RemoveEmptyEntries);
        if(split.Length == 2)
        {
          SetValue(dictionary,split[0],double.Parse(split[1]),SetValueOptions.Const);
        }
        else
        {
          throw new ArgumentOutOfRangeException("ReadTable(dictionary, path, encoding, char[] splitChar)");
        }
      }
    }
    public static void ReadTable(Dictionary<string,Dictionary<string,double>> dictionary,string path,System.Text.Encoding encoding,char[] splitCharSet)
    {
      foreach(string readLine in ReadCommentFileToArray(path,encoding,new string[] { "\r","\n" },new char[] { ';' }))
      {
        string[] split = readLine.Split(splitCharSet,StringSplitOptions.RemoveEmptyEntries);
        if(split.Length == 3)
        {
          SetValue(dictionary,split[0],split[1],double.Parse(split[2]),SetValueOptions.Const);
        }
        else
        {
          throw new ArgumentOutOfRangeException("ReadTable(dictionary, path, encoding, char[] splitChar)");
        }
      }
    }
    public static void ReadTable(Dictionary<string,Dictionary<string,double>> dictionary,Dictionary<string,double> subDictionary,string path,System.Text.Encoding encoding,char[] splitCharSet)
    {
      foreach(string readLine in ReadCommentFileToArray(path,encoding,new string[] { "\r","\n" },new char[] { ';' }))
      {
        string[] split = readLine.Split(splitCharSet,StringSplitOptions.RemoveEmptyEntries);
        if(split.Length == 3)
        {
          SetValue(dictionary,split[0],split[1],double.Parse(split[2]),SetValueOptions.Const);
        }
        else if(split.Length == 2)
        {
          SetValue(subDictionary,split[0],double.Parse(split[1]),SetValueOptions.Const);
        }
        else
        {
          throw new ArgumentOutOfRangeException("ReadTable(dictionary, path, encoding, char[] splitChar)");
        }
      }
    }
    public static void ReadTable(Dictionary<string,Dictionary<string,Dictionary<string,double>>> dictionary,string path,System.Text.Encoding encoding,char[] splitCharSet)
    {
      foreach(string readLine in ReadCommentFileToArray(path,encoding,new string[] { "\r","\n" },new char[] { ';' }))
      {
        string[] split = readLine.Split(splitCharSet,StringSplitOptions.RemoveEmptyEntries);
        if(split.Length == 4)
        {
          SetValue(dictionary,split[0],split[1],split[2],double.Parse(split[3]),SetValueOptions.Const);
        }
        else
        {
          throw new ArgumentOutOfRangeException("ReadTable(dictionary, path, encoding, char[] splitChar)");
        }
      }
    }
    public static void ReadTable(Dictionary<string,Dictionary<string,Dictionary<string,double>>> dictionary,Dictionary<string,Dictionary<string,double>> subDictionary,string path,System.Text.Encoding encoding,char[] splitCharSet)
    {
      foreach(string readLine in ReadCommentFileToArray(path,encoding,new string[] { "\r","\n" },new char[] { ';' }))
      {
        string[] split = readLine.Split(splitCharSet,StringSplitOptions.RemoveEmptyEntries);
        if(split.Length == 4)
        {
          SetValue(dictionary,split[0],split[1],split[2],double.Parse(split[3]),SetValueOptions.Const);
        }
        else if(split.Length == 3)
        {
          SetValue(subDictionary,split[0],split[1],double.Parse(split[2]),SetValueOptions.Const);
        }
        else
        {
          throw new ArgumentOutOfRangeException("ReadTable(dictionary, path, encoding, char[] splitChar)");
        }
      }
    }


    public static void SetValue<T0>(Dictionary<T0,int> dictionary,T0 key0,int value,SetValueOptions setValueOptions)
    {
      if(ContainsKey(dictionary,key0))
      {
        switch(setValueOptions)
        {
          case SetValueOptions.Append:
            dictionary[key0] += value;
            break;

          case SetValueOptions.Override:
            dictionary[key0] = value;
            break;

          case SetValueOptions.Const:
            throw new ArgumentException();
        }
      }
      else
      {
        dictionary.Add(key0,value);
      }
    }
    public static void SetValue<T0, T1>(Dictionary<T0,Dictionary<T1,int>> dictionary,T0 key0,T1 key1,int value,SetValueOptions setValueOptions)
    {
      if(ContainsKey(dictionary,key0,key1))
      {
        switch(setValueOptions)
        {
          case SetValueOptions.Append:
            dictionary[key0][key1] += value;
            break;

          case SetValueOptions.Override:
            dictionary[key0][key1] = value;
            break;

          case SetValueOptions.Const:
            throw new ArgumentException();
        }
      }
      else
      {
        if(dictionary.ContainsKey(key0) == false)
        {
          dictionary.Add(key0,new Dictionary<T1,int>());
        }
        dictionary[key0].Add(key1,value);
      }
    }
    public static void SetValue<T0, T1, T2>(Dictionary<T0,Dictionary<T1,Dictionary<T2,int>>> dictionary,T0 key0,T1 key1,T2 key2,int value,SetValueOptions setValueOptions)
    {
      if(ContainsKey(dictionary,key0,key1,key2))
      {
        switch(setValueOptions)
        {
          case SetValueOptions.Append:
            dictionary[key0][key1][key2] += value;
            break;

          case SetValueOptions.Override:
            dictionary[key0][key1][key2] = value;
            break;

          case SetValueOptions.Const:
            throw new ArgumentException();
        }
      }
      else
      {
        if(dictionary.ContainsKey(key0) == false)
        {
          dictionary.Add(key0,new Dictionary<T1,Dictionary<T2,int>>());
        }
        if(dictionary[key0].ContainsKey(key1) == false)
        {
          dictionary[key0].Add(key1,new Dictionary<T2,int>());
        }
        dictionary[key0][key1].Add(key2,value);
      }
    }
    public static int GetValue<T0>(Dictionary<T0,int> dictionary,T0 key0,int defaultValue)
    {
      if(ContainsKey(dictionary,key0))
      {
        return dictionary[key0];
      }
      else
      {
        return defaultValue;
      }
    }
    public static int GetValue<T0, T1>(Dictionary<T0,Dictionary<T1,int>> dictionary,T0 key0,T1 key1,int defaultValue)
    {
      if(ContainsKey(dictionary,key0,key1))
      {
        return dictionary[key0][key1];
      }
      else
      {
        return defaultValue;
      }
    }
    public static int GetValue<T0, T1, T2>(Dictionary<T0,Dictionary<T1,Dictionary<T2,int>>> dictionary,T0 key0,T1 key1,T2 key2,int defaultValue)
    {
      if(ContainsKey(dictionary,key0,key1,key2))
      {
        return dictionary[key0][key1][key2];
      }
      else
      {
        return defaultValue;
      }
    }
    public static void WriteTable<T0>(Dictionary<T0,int> dictionary,string path,System.Text.Encoding encoding,char splitChar)
    {
      System.IO.StreamWriter file = new System.IO.StreamWriter(path,false,encoding);
      foreach(KeyValuePair<T0,int> key0 in dictionary)
      {
        file.Write(key0.Key.ToString() + splitChar.ToString() + key0.Value.ToString() + ";\n");
      }
      file.Close();
    }
    public static void WriteTable<T0, T1>(Dictionary<T0,Dictionary<T1,int>> dictionary,string path,System.Text.Encoding encoding,char splitChar)
    {
      System.IO.StreamWriter file = new System.IO.StreamWriter(path,false,encoding);
      foreach(KeyValuePair<T0,Dictionary<T1,int>> key0 in dictionary)
      {
        foreach(KeyValuePair<T1,int> key1 in key0.Value)
        {
          file.Write(key0.Key.ToString() + splitChar.ToString() + key1.Key.ToString() + splitChar.ToString() + key1.Value.ToString() + ";\n");
        }
      }
      file.Close();
    }
    public static void WriteTable<T0, T1, T2>(Dictionary<T0,Dictionary<T1,Dictionary<T2,int>>> dictionary,string path,System.Text.Encoding encoding,char splitChar)
    {
      System.IO.StreamWriter file = new System.IO.StreamWriter(path,false,encoding);
      foreach(KeyValuePair<T0,Dictionary<T1,Dictionary<T2,int>>> key0 in dictionary)
      {
        foreach(KeyValuePair<T1,Dictionary<T2,int>> key1 in key0.Value)
        {
          foreach(KeyValuePair<T2,int> key2 in key1.Value)
          {
            file.Write(key0.Key.ToString() + splitChar.ToString() + key1.Key.ToString() + splitChar.ToString() + key2.Key.ToString() + splitChar.ToString() + key2.Value.ToString() + ";\n");
          }
        }
      }
      file.Close();
    }
    public static void ReadTable(Dictionary<char,int> dictionary,string path,System.Text.Encoding encoding,char[] splitCharSet)
    {
      foreach(string readLine in ReadCommentFileToArray(path,encoding,new string[] { "\r","\n" },new char[] { ';' }))
      {
        string[] split = readLine.Split(splitCharSet,StringSplitOptions.RemoveEmptyEntries);
        if(split.Length == 2 && split[0].Length == 1)
        {
          SetValue(dictionary,split[0][0],int.Parse(split[1]),SetValueOptions.Const);
        }
        else
        {
          throw new ArgumentOutOfRangeException("ReadTable(dictionary, path, encoding, char[] splitChar)");
        }
      }
    }
    public static void ReadTable(Dictionary<char,Dictionary<char,int>> dictionary,string path,System.Text.Encoding encoding,char[] splitCharSet)
    {
      foreach(string readLine in ReadCommentFileToArray(path,encoding,new string[] { "\r","\n" },new char[] { ';' }))
      {
        string[] split = readLine.Split(splitCharSet,StringSplitOptions.RemoveEmptyEntries);
        if(split.Length == 3 && split[0].Length == 1 && split[1].Length == 1)
        {
          SetValue(dictionary,split[0][0],split[1][0],int.Parse(split[2]),SetValueOptions.Const);
        }
        else
        {
          throw new ArgumentOutOfRangeException("ReadTable(dictionary, path, encoding, char[] splitChar)");
        }
      }
    }
    public static void ReadTable(Dictionary<char,Dictionary<char,int>> dictionary,Dictionary<char,int> subDictionary,string path,System.Text.Encoding encoding,char[] splitCharSet)
    {
      foreach(string readLine in ReadCommentFileToArray(path,encoding,new string[] { "\r","\n" },new char[] { ';' }))
      {
        string[] split = readLine.Split(splitCharSet,StringSplitOptions.RemoveEmptyEntries);
        if(split.Length == 3 && split[0].Length == 1 && split[1].Length == 1)
        {
          SetValue(dictionary,split[0][0],split[1][0],int.Parse(split[2]),SetValueOptions.Const);
        }
        else if(split.Length == 2 && split[0].Length == 1)
        {
          SetValue(subDictionary,split[0][0],int.Parse(split[1]),SetValueOptions.Const);
        }
        else
        {
          throw new ArgumentOutOfRangeException("ReadTable(dictionary, path, encoding, char[] splitChar)");
        }
      }
    }
    public static void ReadTable(Dictionary<char,Dictionary<char,Dictionary<char,int>>> dictionary,string path,System.Text.Encoding encoding,char[] splitCharSet)
    {
      foreach(string readLine in ReadCommentFileToArray(path,encoding,new string[] { "\r","\n" },new char[] { ';' }))
      {
        string[] split = readLine.Split(splitCharSet,StringSplitOptions.RemoveEmptyEntries);
        if(split.Length == 4 && split[0].Length == 1 && split[1].Length == 1 && split[2].Length == 1)
        {
          SetValue(dictionary,split[0][0],split[1][0],split[2][0],int.Parse(split[3]),SetValueOptions.Const);
        }
        else
        {
          throw new ArgumentOutOfRangeException("ReadTable(dictionary, path, encoding, char[] splitChar)");
        }
      }
    }
    public static void ReadTable(Dictionary<char,Dictionary<char,Dictionary<char,int>>> dictionary,Dictionary<char,Dictionary<char,int>> subDictionary,string path,System.Text.Encoding encoding,char[] splitCharSet)
    {
      foreach(string readLine in ReadCommentFileToArray(path,encoding,new string[] { "\r","\n" },new char[] { ';' }))
      {
        string[] split = readLine.Split(splitCharSet,StringSplitOptions.RemoveEmptyEntries);
        if(split.Length == 4 && split[0].Length == 1 && split[1].Length == 1 && split[2].Length == 1)
        {
          SetValue(dictionary,split[0][0],split[1][0],split[2][0],int.Parse(split[3]),SetValueOptions.Const);
        }
        else if(split.Length == 3 && split[0].Length == 1 && split[1].Length == 1)
        {
          SetValue(subDictionary,split[0][0],split[1][0],int.Parse(split[2]),SetValueOptions.Const);
        }
        else
        {
          throw new ArgumentOutOfRangeException("ReadTable(dictionary, path, encoding, char[] splitChar)");
        }
      }
    }
    public static void ReadTable(Dictionary<string,int> dictionary,string path,System.Text.Encoding encoding,char[] splitCharSet)
    {
      foreach(string readLine in ReadCommentFileToArray(path,encoding,new string[] { "\r","\n" },new char[] { ';' }))
      {
        string[] split = readLine.Split(splitCharSet,StringSplitOptions.RemoveEmptyEntries);
        if(split.Length == 2)
        {
          SetValue(dictionary,split[0],int.Parse(split[1]),SetValueOptions.Const);
        }
        else
        {
          throw new ArgumentOutOfRangeException("ReadTable(dictionary, path, encoding, char[] splitChar)");
        }
      }
    }
    public static void ReadTable(Dictionary<string,Dictionary<string,int>> dictionary,string path,System.Text.Encoding encoding,char[] splitCharSet)
    {
      foreach(string readLine in ReadCommentFileToArray(path,encoding,new string[] { "\r","\n" },new char[] { ';' }))
      {
        string[] split = readLine.Split(splitCharSet,StringSplitOptions.RemoveEmptyEntries);
        if(split.Length == 3)
        {
          SetValue(dictionary,split[0],split[1],int.Parse(split[2]),SetValueOptions.Const);
        }
        else
        {
          throw new ArgumentOutOfRangeException("ReadTable(dictionary, path, encoding, char[] splitChar)");
        }
      }
    }
    public static void ReadTable(Dictionary<string,Dictionary<string,int>> dictionary,Dictionary<string,int> subDictionary,string path,System.Text.Encoding encoding,char[] splitCharSet)
    {
      foreach(string readLine in ReadCommentFileToArray(path,encoding,new string[] { "\r","\n" },new char[] { ';' }))
      {
        string[] split = readLine.Split(splitCharSet,StringSplitOptions.RemoveEmptyEntries);
        if(split.Length == 3)
        {
          SetValue(dictionary,split[0],split[1],int.Parse(split[2]),SetValueOptions.Const);
        }
        else if(split.Length == 2)
        {
          SetValue(subDictionary,split[0],int.Parse(split[1]),SetValueOptions.Const);
        }
        else
        {
          throw new ArgumentOutOfRangeException("ReadTable(dictionary, path, encoding, char[] splitChar)");
        }
      }
    }
    public static void ReadTable(Dictionary<string,Dictionary<string,Dictionary<string,int>>> dictionary,string path,System.Text.Encoding encoding,char[] splitCharSet)
    {
      foreach(string readLine in ReadCommentFileToArray(path,encoding,new string[] { "\r","\n" },new char[] { ';' }))
      {
        string[] split = readLine.Split(splitCharSet,StringSplitOptions.RemoveEmptyEntries);
        if(split.Length == 4)
        {
          SetValue(dictionary,split[0],split[1],split[2],int.Parse(split[3]),SetValueOptions.Const);
        }
        else
        {
          throw new ArgumentOutOfRangeException("ReadTable(dictionary, path, encoding, char[] splitChar)");
        }
      }
    }
    public static void ReadTable(Dictionary<string,Dictionary<string,Dictionary<string,int>>> dictionary,Dictionary<string,Dictionary<string,int>> subDictionary,string path,System.Text.Encoding encoding,char[] splitCharSet)
    {
      foreach(string readLine in ReadCommentFileToArray(path,encoding,new string[] { "\r","\n" },new char[] { ';' }))
      {
        string[] split = readLine.Split(splitCharSet,StringSplitOptions.RemoveEmptyEntries);
        if(split.Length == 4)
        {
          SetValue(dictionary,split[0],split[1],split[2],int.Parse(split[3]),SetValueOptions.Const);
        }
        else if(split.Length == 3)
        {
          SetValue(subDictionary,split[0],split[1],int.Parse(split[2]),SetValueOptions.Const);
        }
        else
        {
          throw new ArgumentOutOfRangeException("ReadTable(dictionary, path, encoding, char[] splitChar)");
        }
      }
    }


    public static void SetValue<T0>(Dictionary<T0,string> dictionary,T0 key0,string value,SetValueOptions setValueOptions)
    {
      if(ContainsKey(dictionary,key0))
      {
        switch(setValueOptions)
        {
          case SetValueOptions.Append:
            dictionary[key0] += value;
            break;

          case SetValueOptions.Override:
            dictionary[key0] = value;
            break;

          case SetValueOptions.Const:
            throw new ArgumentException();
        }
      }
      else
      {
        dictionary.Add(key0,value);
      }
    }
    public static void SetValue<T0, T1>(Dictionary<T0,Dictionary<T1,string>> dictionary,T0 key0,T1 key1,string value,SetValueOptions setValueOptions)
    {
      if(ContainsKey(dictionary,key0,key1))
      {
        switch(setValueOptions)
        {
          case SetValueOptions.Append:
            dictionary[key0][key1] += value;
            break;

          case SetValueOptions.Override:
            dictionary[key0][key1] = value;
            break;

          case SetValueOptions.Const:
            throw new ArgumentException();
        }
      }
      else
      {
        if(dictionary.ContainsKey(key0) == false)
        {
          dictionary.Add(key0,new Dictionary<T1,string>());
        }
        dictionary[key0].Add(key1,value);
      }
    }
    public static void SetValue<T0, T1, T2>(Dictionary<T0,Dictionary<T1,Dictionary<T2,string>>> dictionary,T0 key0,T1 key1,T2 key2,string value,SetValueOptions setValueOptions)
    {
      if(ContainsKey(dictionary,key0,key1,key2))
      {
        switch(setValueOptions)
        {
          case SetValueOptions.Append:
            dictionary[key0][key1][key2] += value;
            break;

          case SetValueOptions.Override:
            dictionary[key0][key1][key2] = value;
            break;

          case SetValueOptions.Const:
            throw new ArgumentException();
        }
      }
      else
      {
        if(dictionary.ContainsKey(key0) == false)
        {
          dictionary.Add(key0,new Dictionary<T1,Dictionary<T2,string>>());
        }
        if(dictionary[key0].ContainsKey(key1) == false)
        {
          dictionary[key0].Add(key1,new Dictionary<T2,string>());
        }
        dictionary[key0][key1].Add(key2,value);
      }
    }
    public static string GetValue<T0>(Dictionary<T0,string> dictionary,T0 key0,string defaultValue)
    {
      if(ContainsKey(dictionary,key0))
      {
        return dictionary[key0];
      }
      else
      {
        return defaultValue;
      }
    }
    public static string GetValue<T0, T1>(Dictionary<T0,Dictionary<T1,string>> dictionary,T0 key0,T1 key1,string defaultValue)
    {
      if(ContainsKey(dictionary,key0,key1))
      {
        return dictionary[key0][key1];
      }
      else
      {
        return defaultValue;
      }
    }
    public static string GetValue<T0, T1, T2>(Dictionary<T0,Dictionary<T1,Dictionary<T2,string>>> dictionary,T0 key0,T1 key1,T2 key2,string defaultValue)
    {
      if(ContainsKey(dictionary,key0,key1,key2))
      {
        return dictionary[key0][key1][key2];
      }
      else
      {
        return defaultValue;
      }
    }
    public static void WriteTable<T0>(Dictionary<T0,string> dictionary,string path,System.Text.Encoding encoding,char splitChar)
    {
      System.IO.StreamWriter file = new System.IO.StreamWriter(path,false,encoding);
      foreach(KeyValuePair<T0,string> key0 in dictionary)
      {
        file.Write(key0.Key.ToString() + splitChar.ToString() + key0.Value.ToString() + ";\n");
      }
      file.Close();
    }
    public static void WriteTable<T0, T1>(Dictionary<T0,Dictionary<T1,string>> dictionary,string path,System.Text.Encoding encoding,char splitChar)
    {
      System.IO.StreamWriter file = new System.IO.StreamWriter(path,false,encoding);
      foreach(KeyValuePair<T0,Dictionary<T1,string>> key0 in dictionary)
      {
        foreach(KeyValuePair<T1,string> key1 in key0.Value)
        {
          file.Write(key0.Key.ToString() + splitChar.ToString() + key1.Key.ToString() + splitChar.ToString() + key1.Value.ToString() + ";\n");
        }
      }
      file.Close();
    }
    public static void WriteTable<T0, T1, T2>(Dictionary<T0,Dictionary<T1,Dictionary<T2,string>>> dictionary,string path,System.Text.Encoding encoding,char splitChar)
    {
      System.IO.StreamWriter file = new System.IO.StreamWriter(path,false,encoding);
      foreach(KeyValuePair<T0,Dictionary<T1,Dictionary<T2,string>>> key0 in dictionary)
      {
        foreach(KeyValuePair<T1,Dictionary<T2,string>> key1 in key0.Value)
        {
          foreach(KeyValuePair<T2,string> key2 in key1.Value)
          {
            file.Write(key0.Key.ToString() + splitChar.ToString() + key1.Key.ToString() + splitChar.ToString() + key2.Key.ToString() + splitChar.ToString() + key2.Value.ToString() + ";\n");
          }
        }
      }
      file.Close();
    }
    public static void ReadTable(Dictionary<char,string> dictionary,string path,System.Text.Encoding encoding,char[] splitCharSet)
    {
      foreach(string readLine in ReadCommentFileToArray(path,encoding,new string[] { "\r","\n" },new char[] { ';' }))
      {
        string[] split = readLine.Split(splitCharSet,StringSplitOptions.RemoveEmptyEntries);
        if(split.Length == 2 && split[0].Length == 1)
        {
          SetValue(dictionary,split[0][0],(split[1]),SetValueOptions.Const);
        }
        else
        {
          throw new ArgumentOutOfRangeException("ReadTable(dictionary, path, encoding, char[] splitChar)");
        }
      }
    }
    public static void ReadTable(Dictionary<char,Dictionary<char,string>> dictionary,string path,System.Text.Encoding encoding,char[] splitCharSet)
    {
      foreach(string readLine in ReadCommentFileToArray(path,encoding,new string[] { "\r","\n" },new char[] { ';' }))
      {
        string[] split = readLine.Split(splitCharSet,StringSplitOptions.RemoveEmptyEntries);
        if(split.Length == 3 && split[0].Length == 1 && split[1].Length == 1)
        {
          SetValue(dictionary,split[0][0],split[1][0],(split[2]),SetValueOptions.Const);
        }
        else
        {
          throw new ArgumentOutOfRangeException("ReadTable(dictionary, path, encoding, char[] splitChar)");
        }
      }
    }
    public static void ReadTable(Dictionary<char,Dictionary<char,string>> dictionary,Dictionary<char,string> subDictionary,string path,System.Text.Encoding encoding,char[] splitCharSet)
    {
      foreach(string readLine in ReadCommentFileToArray(path,encoding,new string[] { "\r","\n" },new char[] { ';' }))
      {
        string[] split = readLine.Split(splitCharSet,StringSplitOptions.RemoveEmptyEntries);
        if(split.Length == 3 && split[0].Length == 1 && split[1].Length == 1)
        {
          SetValue(dictionary,split[0][0],split[1][0],(split[2]),SetValueOptions.Const);
        }
        else if(split.Length == 2 && split[0].Length == 1)
        {
          SetValue(subDictionary,split[0][0],(split[1]),SetValueOptions.Const);
        }
        else
        {
          throw new ArgumentOutOfRangeException("ReadTable(dictionary, path, encoding, char[] splitChar)");
        }
      }
    }
    public static void ReadTable(Dictionary<char,Dictionary<char,Dictionary<char,string>>> dictionary,string path,System.Text.Encoding encoding,char[] splitCharSet)
    {
      foreach(string readLine in ReadCommentFileToArray(path,encoding,new string[] { "\r","\n" },new char[] { ';' }))
      {
        string[] split = readLine.Split(splitCharSet,StringSplitOptions.RemoveEmptyEntries);
        if(split.Length == 4 && split[0].Length == 1 && split[1].Length == 1 && split[2].Length == 1)
        {
          SetValue(dictionary,split[0][0],split[1][0],split[2][0],(split[3]),SetValueOptions.Const);
        }
        else
        {
          throw new ArgumentOutOfRangeException("ReadTable(dictionary, path, encoding, char[] splitChar)");
        }
      }
    }
    public static void ReadTable(Dictionary<char,Dictionary<char,Dictionary<char,string>>> dictionary,Dictionary<char,Dictionary<char,string>> subDictionary,string path,System.Text.Encoding encoding,char[] splitCharSet)
    {
      foreach(string readLine in ReadCommentFileToArray(path,encoding,new string[] { "\r","\n" },new char[] { ';' }))
      {
        string[] split = readLine.Split(splitCharSet,StringSplitOptions.RemoveEmptyEntries);
        if(split.Length == 4 && split[0].Length == 1 && split[1].Length == 1 && split[2].Length == 1)
        {
          SetValue(dictionary,split[0][0],split[1][0],split[2][0],(split[3]),SetValueOptions.Const);
        }
        else if(split.Length == 3 && split[0].Length == 1 && split[1].Length == 1)
        {
          SetValue(subDictionary,split[0][0],split[1][0],(split[2]),SetValueOptions.Const);
        }
        else
        {
          throw new ArgumentOutOfRangeException("ReadTable(dictionary, path, encoding, char[] splitChar)");
        }
      }
    }
    public static void ReadTable(Dictionary<string,string> dictionary,string path,System.Text.Encoding encoding,char[] splitCharSet)
    {
      foreach(string readLine in ReadCommentFileToArray(path,encoding,new string[] { "\r","\n" },new char[] { ';' }))
      {
        string[] split = readLine.Split(splitCharSet,StringSplitOptions.RemoveEmptyEntries);
        if(split.Length == 2)
        {
          SetValue(dictionary,split[0],(split[1]),SetValueOptions.Const);
        }
        else
        {
          throw new ArgumentOutOfRangeException("ReadTable(dictionary, path, encoding, char[] splitChar)");
        }
      }
    }
    public static void ReadTable(Dictionary<string,Dictionary<string,string>> dictionary,string path,System.Text.Encoding encoding,char[] splitCharSet)
    {
      foreach(string readLine in ReadCommentFileToArray(path,encoding,new string[] { "\r","\n" },new char[] { ';' }))
      {
        string[] split = readLine.Split(splitCharSet,StringSplitOptions.RemoveEmptyEntries);
        if(split.Length == 3)
        {
          SetValue(dictionary,split[0],split[1],(split[2]),SetValueOptions.Const);
        }
        else
        {
          throw new ArgumentOutOfRangeException("ReadTable(dictionary, path, encoding, char[] splitChar)");
        }
      }
    }
    public static void ReadTable(Dictionary<string,Dictionary<string,string>> dictionary,Dictionary<string,string> subDictionary,string path,System.Text.Encoding encoding,char[] splitCharSet)
    {
      foreach(string readLine in ReadCommentFileToArray(path,encoding,new string[] { "\r","\n" },new char[] { ';' }))
      {
        string[] split = readLine.Split(splitCharSet,StringSplitOptions.RemoveEmptyEntries);
        if(split.Length == 3)
        {
          SetValue(dictionary,split[0],split[1],(split[2]),SetValueOptions.Const);
        }
        else if(split.Length == 2)
        {
          SetValue(subDictionary,split[0],(split[1]),SetValueOptions.Const);
        }
        else
        {
          throw new ArgumentOutOfRangeException("ReadTable(dictionary, path, encoding, char[] splitChar)");
        }
      }
    }
    public static void ReadTable(Dictionary<string,Dictionary<string,Dictionary<string,string>>> dictionary,string path,System.Text.Encoding encoding,char[] splitCharSet)
    {
      foreach(string readLine in ReadCommentFileToArray(path,encoding,new string[] { "\r","\n" },new char[] { ';' }))
      {
        string[] split = readLine.Split(splitCharSet,StringSplitOptions.RemoveEmptyEntries);
        if(split.Length == 4)
        {
          SetValue(dictionary,split[0],split[1],split[2],(split[3]),SetValueOptions.Const);
        }
        else
        {
          throw new ArgumentOutOfRangeException("ReadTable(dictionary, path, encoding, char[] splitChar)");
        }
      }
    }
    public static void ReadTable(Dictionary<string,Dictionary<string,Dictionary<string,string>>> dictionary,Dictionary<string,Dictionary<string,string>> subDictionary,string path,System.Text.Encoding encoding,char[] splitCharSet)
    {
      foreach(string readLine in ReadCommentFileToArray(path,encoding,new string[] { "\r","\n" },new char[] { ';' }))
      {
        string[] split = readLine.Split(splitCharSet,StringSplitOptions.RemoveEmptyEntries);
        if(split.Length == 4)
        {
          SetValue(dictionary,split[0],split[1],split[2],(split[3]),SetValueOptions.Const);
        }
        else if(split.Length == 3)
        {
          SetValue(subDictionary,split[0],split[1],(split[2]),SetValueOptions.Const);
        }
        else
        {
          throw new ArgumentOutOfRangeException("ReadTable(dictionary, path, encoding, char[] splitChar)");
        }
      }
    }
  }
}