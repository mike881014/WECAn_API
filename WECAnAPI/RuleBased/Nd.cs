using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WECAnAPI.RuleBased
{//查表合併
  static class Nd
  {
    static Nd()
    {
      foreach(string item in 合併時間詞)
      {
        if(item.Length > 合併時間詞最大長度)
        {
          合併時間詞最大長度 = item.Length;
        }
      }
    }
    public static HashSet<string> 合併時間詞 = new HashSet<string>() {
      "學年度","學年","年度","年代","世紀","年","月","日","號","點","時","分","秒",
      "学年度","学年","年度","年代","世纪","年","月","日","号","点","时","分","秒",
      "月份","月分","月天","月半","時半","時多","時正","時餘","點半","點半鐘","點多","點多鐘","點鐘","年次",
      "月份","月分","月天","月半","时半","时多","时正","时余","点半","点半钟","点多","点多钟","点钟","年次"};
    public static HashSet<string> 年號表 = new HashSet<string>() { // https://zh.wikipedia.org/wiki/年號
      // 台灣年號表 
      "永曆","康熙","永和","雍正","乾隆","天運","順天","天運","嘉慶","光明","道光","天運","咸豐","祺祥","同治","光緒",
      "永清","明治","天運","大靖","大正","昭和","至元","元貞","大德","至大","皇慶","延祐","至治","泰定","致和","天順",
      "天曆","至順","元統","至元","至正","洪武","嘉靖","隆慶","萬曆","泰昌","天啟","西元","公元","民國","中華民國",
      // 中國年號表; "建國"，"文明" 可能較常造成錯誤判斷，因此先不加入
      "建元","元光","元朔","元狩","元鼎","元封","太初","天漢","太始","征和","後元","始元","元鳳","元平","本始","地節",
      "元康","神爵","五鳳","甘露","黃龍","初元","永光","建昭","竟寧","建始","河平","陽朔","鴻嘉","永始","元延","綏和",
      "建平","太初元將","元壽","元始","居攝","初始","始建國","天鳳","地皇","更始","漢復","龍興","建世","建武","建武中元",
      "永平","建初","元和","章和","永元","元興","延平","永初","元初","永寧","建光","延光","永建","陽嘉","永和","漢安",
      "建康","永憙","本初","建和","和平","元嘉","永興","永壽","延熹","永康","建寧","熹平","光和","中平","光熹","昭寧",
      "永漢","中平","初平","興平","建安","延康","神上","黃初","太和","青龍","景初","正始","嘉平","正元","甘露","景元",
      "咸熙","紹漢","章武","建興","延熙","景耀","炎興","黃武","黃龍","嘉禾","赤烏","太元","神鳳","建興","五鳳","太平",
      "永安","元興","甘露","寶鼎","建衡","鳳凰","天冊","天璽","天紀","泰始","咸寧","太康","太熙","永熙","永平","元康",
      "永康","永寧","太安","永安","建武","永興","光熙","永嘉","建興","太平","建始","神鳳","建武","大興","永昌","太寧",
      "咸和","咸康","建元","永和","昇平","隆和","興寧","太和","咸安","寧康","太元","隆安","元興","大亨","義熙","元熙",
      "建康","鳳凰","永始","天康","元熙","永鳳","河瑞","光興","嘉平","建元","麟嘉","漢昌","光初","太和","平趙","建初",
      "建興","晏平","玉衡","大武","玉恆","漢興","太和","嘉寧","建興","建興","建興","建興","和平","建興","昇平","昇平",
      "鳳凰","太和","建平","延熙","建武","太寧","永熙","延興","青龍","永寧","永興","龍興","燕元","元璽","光壽","建熙",
      "皇始","壽光","永興","甘露","建元","太安","太初","延初","建昌","黑龍","元光","白雀","建初","皇初","弘始","永和",
      "燕元","建興","永康","建平","長樂","光始","建始","建光","定鼎","建始","延平","青龍","燕興","更始","昌平","建明",
      "建平","建武","中興","建義","太初","更始","永康","建弘","永弘","太安","麟嘉","龍飛","承康","咸寧","神鼎","太初",
      "建和","弘昌","嘉平","燕平","建平","太上","太平","庚子","建初","嘉興","永建","龍昇","鳳翔","昌武","真興","承光",
      "勝光","正始","太平","太興","神璽","天璽","永安","玄始","真興","承玄","義和","承陽","緣禾","承和","太緣","建平",
      "承平","永初","景平","元嘉","孝建","大明","永光","景和","泰始","泰豫","元徽","昇明","泰始","建義","太初","建平",
      "永光","義嘉","建元","永明","隆昌","延興","建武","永泰","永元","中興","興平","建義","天監","普通","大通","中大通",
      "大同","中大同","太清","大寶","天正","承聖","天成","紹泰","太平","上願","永漢","天德","正平","太始","天正","天啟",
      "大定","天保","廣運","永定","天嘉","天康","光大","太建","至德","禎明",/*"建國",*/"登國","皇始","天興","天賜","永興",
      "神瑞","泰常","始光","神䴥","延和","太延","太平真君","正平","承平","興安","興光","太安","和平","天安","皇興",
      "延興","承明","太和","景明","正始","永平","延昌","熙平","神龜","正光","孝昌","武泰","建義","永安","建明","普泰",
      "中興","太昌","永興","永熙","建平","聖君","正始","建明","聖明","建平","大乘","真王","天建","天啟","真王","神嘉",
      "魯興","始建","廣安","天授","隆緒","天統","神獸","孝基","建武","更興","天平","元象","興和","武定","平都","大統",
      "乾明","天保","乾明","皇建","太寧","河清","天統","武平","隆化","德昌","承光","武平","安太","武成","武定","保定",
      "天和","建德","宣政","大成","大象","大定","石平","永康","太平","太安","始平","建昌","建初","承平","義熙","甘露",
      "章和","永平","和平","建昌","延昌","延和","義和","重光","延壽","白雀","永樂","開皇","仁壽","大業","義寧","皇泰",
      "白鳥","大世","昌達","始興","太平","丁丑","五鳳","永平","天興","正平","秦興","鳴鳳","安樂","通聖","永隆","武德",
      "貞觀","永徽","顯慶","龍朔","麟德","乾封","總章","咸亨","上元","儀鳳","調露","永隆","開耀","永淳","弘道","嗣聖",
      /*"文明",*/ "光宅","垂拱","永昌","載初","天授","如意","長壽","延載","証聖","天冊萬歲","萬歲登封","萬歲通天","神功",
      "聖曆","久視","大足","長安","神龍","景龍","唐隆","景雲","太極","延和","先天","開元","天寶","至德","乾元","上元",
      "寶應","廣德","永泰","大曆","建中","興元","貞元","永貞","元和","永新","長慶","寶曆","大和","開成","會昌","大中",
      "咸通","乾符","廣明","中和","光啟","文德","龍紀","大順","景福","乾寧","光化","天復","天祐","天壽","始興","法輪",
      "開明","延康","明政","天造","天明","乾德","進通","中元克復","聖武","載初","天成","應天","順天","顯聖","黃龍",
      "正德","寶勝","應天","天皇","武成","羅平","王霸","金統","建貞","順天","天壽","彝泰","同慶","天尊","中興","天興",
      "天壽","仁安","大興","寶曆","中興","正曆","永德","朱雀","太始","建興","咸和","甘露","元興","贊普鍾","長壽","見龍",
      "上元","元封","應道","龍興","全義","大豐","保和","天啟","建極","貞明","承智","大同","嵯耶","中興","安國","始元",
      "天瑞景星","安和","貞祐","初歷","孝治","天應","尊聖","興聖","大明","鼎新","光聖","文德","神武","文經","至治",
      "明德","廣德","順德","明政","廣明","明統","明聖","明德","明治","明法","明應","明運","明啟","乾興","明通","正治",
      "聖明","天明","保安","政安","正德","保德","太安","明侯","上德","廣安","上明","保立","建安","天祐","上治","天授",
      "明開","天政","文安","日新","文治","永嘉","保天","廣運","永貞","大寶","龍興","盛明","建德","利貞","盛德","嘉會",
      "元亨","定安","亨時","鳳歷","元壽","天開","天輔","仁壽","道隆","天定","順德","興正","至德","大本","鍾元","隆德",
      "永道","開平","乾化","鳳歷","乾化","貞明","龍德","應天","同光","天成","長興","應順","清泰","天福","開運","天福",
      "乾祐","廣順","顯德","天復","天祐","武義","順義","乾貞","大和","天祚","昇元","保大","中興","交泰","顯德","顯德",
      "建隆","乾德","開寶","天祐","天寶","鳳歷","乾化","貞明","龍德","寶大","寶正","廣初","正明","長興","應順","清泰",
      "天福","天福","開運","天福","乾祐","廣順","顯德","建隆","乾德","開寶","太平興國","天成","長興","長興","應順",
      "清泰","天福","開運","天福","乾祐","保大","開平","乾化","貞明","龍德","同光","天成","天成","長興","龍啟","永和",
      "通文","永隆","天德","乾亨","白龍","大有","光天","應乾","乾和","大寶","永樂","天復","武成","通正","天漢","光天",
      "乾德","咸康","明德","廣政","同光","天成","乾貞","乾貞","天成","長興","應順","清泰","天福","開運","天福","乾祐",
      "乾祐","廣順","顯德","建隆","建隆","乾祐","乾祐","天會","廣運","建隆","乾德","開寶","太平興國","雍熙","端拱",
      "淳化","至道","咸平","景德","大中祥符","天禧","乾興","天聖","明道","景祐","寶元","康定","慶曆","皇祐","至和",
      "嘉祐","治平","熙寧","元豐","元祐","紹聖","元符","建中靖國","崇寧","大觀","政和","重和","宣和","靖康","應運",
      "化順","得聖","景瑞","啟歷","端懿","大曆","隆興","永樂","建炎","紹興","隆興","乾道","淳熙","紹熙","慶元","嘉泰",
      "開禧","嘉定","寶慶","紹定","端平","嘉熙","淳祐","寶祐","開慶","景定","咸淳","德祐","景炎","祥興","明受","天載",
      "正法","人知","太平","阜昌","大聖天王","庚戌","羅平","乾貞","羅平","轉運","重德","天戰","龍興","天定","神冊",
      "天贊","天顯","會同","大同","天祿","應曆","保寧","乾亨","統和","開泰","太平","景福","重熙","清寧","咸雍","大康",
      "大安","壽昌","乾統","天慶","保大","建福","德興","神曆","天慶","隆基","天復","天嗣","延慶","康國","咸清","感天",
      "紹興","續興","穆興","崇福","皇德","重德","天禧","顯道","開運","廣運","大慶","天授禮法延祚","廣熙","廣民",
      "延嗣寧國","天祐垂聖","福聖承道","奲都","拱化","乾道","天賜禮盛國慶","大安","天安禮定","西安","天儀治平","天祐民安",
      "永安","貞觀","雍寧","元德","正德","大德","大慶","人慶","天盛","乾祐","天慶","應天","皇建","光定","乾定","寶義",
      "寶慶","廣僖","清平","收國","天輔","天會","天眷","皇統","天德","貞元","正隆","大定","興慶","明昌","承安","泰和",
      "天定","大安","崇慶","至寧","貞祐","興定","元光","正大","開興","天興","盛昌","天興","天統","天正","身聖","元統",
      "天賜","天順","天泰","大同","興隆","順天","天威","天祐","天德","中統","至元","元貞","大德","至大","皇慶","延祐",
      "至治","泰定","致和","天順","天曆","至順","元統","至元","至正","宣光","天元","萬乘","昌泰","延康","祥興","安定",
      "正治","赤符","治平","太平","天啟","天定","正朔","天祐","龍鳳","大義","大定","德壽","天統","開熙","洪武","建文",
      "永樂","洪熙","宣德","正統","景泰","天順","成化","弘治","正德","嘉靖","隆慶","萬曆","泰昌","天啟","崇禎","義興",
      "弘光","隆武","紹武","興業","監國","定武","永曆","東武","天定","龍鳳","永寧","永天","泰定","東陽","玄元","添元",
      "天順","天繡","武烈","德勝","明正","順德","平定","大順","天淵","造歷","龍飛","大寶","洪武","真混","瑞應","玄靜",
      "大成興勝","永興","興武","天運","永昌","義武","大順","圓明大寶","宏閏","湧安","天命","天聰","崇德","順治","康熙",
      "雍正","乾隆","嘉慶","道光","咸豐","祺祥","同治","光緒","宣統","重興","天定","永昌","清光","中興","永曆","隆武",
      "興朝","天正","天順","大慶","廣德","昭武","利用","洪化","裕民","文興","元興","永興","天德","永和","天運","順天",
      "天運","萬利","大康","晏朝","天運","太平天國","天德","洪順","天德","天運","江漢","洪德","順天","天縱","嗣統",
      "華漢","江漢","永清","大明國","漢德","共戴","洪憲","通志","宣統","大同","康德",};
    static HashSet<char> 天干 = new HashSet<char>() { '甲', '乙', '丙', '丁', '戊', '己', '庚', '辛', '壬', '癸' };
    static HashSet<char> 地支 = new HashSet<char>() { '子', '丑', '寅', '卯', '辰', '巳', '午', '未', '申', '酉', '戌', '亥' };
    static HashSet<char> 年月日 = new HashSet<char>() { '年', '月', '日' };
    public static bool IsNd(string word)
    {
      if(word == null || word.Length == 0) { return false; }
      switch (word.Length) { // 非 "<數字><單位>" 格式，選擇2或3字的詞
        case 3:
          if (天干.Contains(word[0]) && 地支.Contains(word[1]) && 年月日.Contains(word[2])) { return true; } // "子丑年"
          break;
        case 2:
          if (天干.Contains(word[0]) && 地支.Contains(word[1])) { return true; } // "子午"
          if (地支.Contains(word[0]) && word[1] == '時') { return true; } // "子時"
          break;
      }
      for(int neuLength = word.Length - 1; neuLength >= 1 && neuLength >= word.Length - 合併時間詞最大長度; neuLength--) {
        string num = word.Substring(0,neuLength);
        string unit = word.Substring(neuLength);
        if(Neu.IsNeu(num) == true && 合併時間詞.Contains(unit) == true) { // "<數字><單位>" 格式
          switch (unit) { // 選擇特定單位判斷其數字格式是否正確
            case "世紀": 
              return Neu.IsCentury(num); 
            case "年": 
              return Neu.IsYear(num);
            case "月": /*case "月中": */case "月份": case "月分": case "月天": case "月半": /*case "月底":*/
              return Neu.IsMonth(num); 
            case "日": case "號": case "号": 
              return Neu.IsDate(num);
            case "點": case "時": case "時半": case "時多": case "時正": case "時餘":case "點半": case "點半鐘": case "點多": case "點多鐘": case "點鐘": case "年次":
            case "点": case "时": case "时半": case "时多": case "时正": case "时余":case "点半": case "点半钟": case "点多": case "点多钟": case "点钟": 
              return Neu.IsHour(num);
            case "分": case "秒":
              return Neu.IsPureNumber(num) && Neu.IsMinuteSecond(num);
          }
          return true;
        }
      }
      return false;
    }

    public static bool compareCharAtString(string str,char[] charSet,bool head)//虛引數傳入要審視的字串跟想尋找的字元陣列，如果字串最前或最後存在字元陣列其中一個值則返回布林值真
    {
      foreach(char element in charSet)
      {
        if((head == true && str.IndexOf(element) == 0)
            || (head == false && str.LastIndexOf(element) == str.Length - 1))
        {
          return true;
        }
      }
      return false;
    }
    static bool 合併並標記Nd (List<string> seged, List<string> posed, string word, int i, int width)
    {
      seged[i] = word;/*合併詞*/
      seged.RemoveRange(i + 1, width - 1);/*移除要合併組合的後面詞*/
      posed[i] = "Nd";//給予Nd的詞性
      posed.RemoveRange(i + 1, width - 1);/*移除要合併組合的後面頻率*/
      return true;
    }
    public static bool Handle人工(Get get,List<string> seged,List<string> posed)
    {
      bool haveHandle = false;

      int time = 2;
      while(--time >= 0)//至(P)　seged[八十五(Neu)　年(Nf)　/八十五年(Nd)　]　三月(Nd)　十九日(Nd)　止(Ng)
      {
        for(int i = 0; i < seged.Count; i++)//時間詞合併
        {
          if ( // 甲_午_年
            i + 2 < seged.Count && 
            seged[i + 0].Length == 1 && 
            seged[i + 1].Length == 1 && 
            seged[i + 2].Length == 1 && 
            天干.Contains(seged[i + 0][0]) && 
            地支.Contains(seged[i + 1][0]) && 
            年月日.Contains(seged[i + 2][0])
            ) {
            合併並標記Nd(seged, posed, seged[i + 0] + seged[i + 1] + seged[i + 2], i, 3);
            continue;
          }
          if ( // 子_丑，子_時
            i + 1 < seged.Count && 
            seged[i + 0].Length == 1 && 
            seged[i + 1].Length == 1 && 
            天干.Contains(seged[i + 0][0]) && (
              地支.Contains(seged[i + 1][0]) ||
              seged[i + 1] == "時")
            ) {
            合併並標記Nd(seged, posed, seged[i + 0] + seged[i + 1], i, 2);
            continue;
          }
          if ( // N_點_半_鐘
            i + 3 < seged.Count && 
            IsNd(seged[i + 0] + seged[i + 1] + seged[i + 2] + seged[i + 3])
            ) {
            合併並標記Nd(seged, posed, seged[i + 0] + seged[i + 1] + seged[i + 2] + seged[i + 3], i, 4);
            continue;
          }
          if ( // N_點_鐘
            i + 2 < seged.Count && 
            IsNd(seged[i + 0] + seged[i + 1] + seged[i + 2])
            ) {
            合併並標記Nd(seged, posed, seged[i + 0] + seged[i + 1] + seged[i + 2], i, 3);
            continue;
          }
          if((posed[i] == "Neu" || Neu.IsPureNumber(seged[i]) == true) && i + 1 < seged.Count && 合併時間詞.Contains(seged[i + 1])) {
            bool link = true;
            bool 西元民國checked = false;
            if (
              i > 0 && (
                seged[i - 1].Length == 1 && compareCharAtString(seged[i - 1], new char[] {
                  '分', '再', '期', '前', '面', '凡', '了', '近', '每', '後', 
                  '分', '再', '期', '前', '面', '凡', '了', '近', '每', '后'}, false) || 
                seged[i - 1].Length == 2 && compareCharAtString(seged[i - 1], new char[] { 
                  '期', '面' }, false)) || 
              i + 2 < seged.Count && (
                seged[i + 2].Length == 1 && compareCharAtString(seged[i + 2], new char[] {
                  '內', '來', '前', '後', '裡', '多', '間' , 
                  '内', '来', '前', '後', '里', '多', '间' }, true) &&
                  i > 0 && posed[i - 1] != "Nd" ||
                seged[i + 2].Length == 2 && compareCharAtString(seged[i + 2], new char[] { 
                  '之','以','期' }, true))
              ) {
              switch (seged[i + 1]) {
                case "時": case "年代": case "年度": case "年次": case "世紀": 
                  link = true;
                  break;
                default: 
                  link = false;
                  break;
              }
            }

            if (
              i > 0 && (
                // posed[i - 1] == "Nf" || 
                posed[i - 1].IndexOf("Ne") == 0 || 
                posed[i - 1] == "V_2"
              )) {
              link = false;
            }

            if(seged[i][0] == '第'
                || seged[i][seged[i].Length - 1] == '多'
                || seged[i][seged[i].Length - 1] == '餘'
                || seged[i][seged[i].Length - 1] == '余')
            {
              link = false;
            }

            if((i - 2 >= 0 && 年號表.Contains(seged[i - 2]))
                || (i - 1 >= 0 && 年號表.Contains(seged[i - 1]))
                || (i + 2 <= seged.Count - 1 && (seged[i + 2] == "起" || seged[i + 2] == "止" || seged[i + 2] == "前後" || seged[i + 2] == "前后"))
                )
            {
              link = true;
              西元民國checked = true;
            }

            if(seged[i + 1] == "號" || seged[i + 1] == "号") { // N月N號
              if (i > 0 && posed[i - 1] == "Nd" && posed[i] == "Neu") {
                link = true;
              } else {
                link = false;
              }
            }
            
            string num = seged[i];
            if(link == true && Neu.IsPureNumber(num)) { //最後進行特定範圍檢查
              switch (seged[i + 1]) { // 選擇特定單位判斷其數字格式是否正確
                case "世紀": 
                  if (西元民國checked == false)
                    link = Neu.IsCentury(num); break;
                case "年": 
                  if (西元民國checked == false)
                    link = Neu.IsYear(num); break;
                case "月": /*case "月中": */case "月份": case "月分": case "月天": case "月半": /*case "月底":*/
                  link = Neu.IsMonth(num); break;
                case "日": case "號": case "号": 
                  link = Neu.IsDate(num); break;
                case "點": case "時": case "時半": case "時多": case "時正": case "時餘":case "點半": case "點半鐘": case "點多": case "點多鐘": case "點鐘": case "年次":
                case "点": case "时": case "时半": case "时多": case "时正": case "时余":case "点半": case "点半钟": case "点多": case "点多钟": case "点钟": 
                  link = Neu.IsHour(num); break;
                case "分": case "秒":
                  link = Neu.IsMinuteSecond(num); break;
              }
            } else {
              link = false;
            }

            if(link == true)
            {
              haveHandle = 合併並標記Nd(seged, posed, seged[i] + seged[i + 1], i, 2);
            }
          }
        }
      }
      return haveHandle;
    }

    public static bool Handle(Get get,List<string> seged,List<string> posed)
    {
      bool haveHandle = false;
      bool thisTimeHaveHandle = true;

      while(thisTimeHaveHandle)
      {//至(P)　seged[八十五(Neu)　年(Nf)　/八十五年(Nd)　]　三月(Nd)　十九日(Nd)　止(Ng)
        thisTimeHaveHandle = false;
        for(int forCount = 0; forCount < seged.Count; forCount++)
        {
          if(forCount + 1 < seged.Count && posed[forCount] == "Neu" && 合併時間詞.Contains(seged[forCount + 1]) == true)
          {
            if(J48(
                   ((forCount - 1 > -1) ? (posed[forCount - 1]) : ("begin")),
                   ((forCount + 2 < seged.Count) ? (posed[forCount + 2]) : ("end")),
                   ((seged[forCount][0] == '第' ||
                   seged[forCount][0] == '第' ||
                   seged[forCount][seged[forCount].Length - 1] == '餘' ||
                   seged[forCount][seged[forCount].Length - 1] == '余' ||
                   seged[forCount][seged[forCount].Length - 1] == '多' ||
                   seged[forCount][seged[forCount].Length - 1] == '馀' ||
                   seged[forCount][seged[forCount].Length - 1] == '余' ||
                   seged[forCount][seged[forCount].Length - 1] == '多'
                   ) ? (true) : (false))
                   ,0.6
                   ) == true)
            {
              string ndString = seged[forCount] + seged[forCount + 1];
              seged.RemoveRange(forCount,2);
              posed.RemoveRange(forCount,2);
              seged.Insert(forCount,ndString);
              posed.Insert(forCount,"Nd");
              thisTimeHaveHandle = true;
              haveHandle = true;
            }
          }
        }
      }
      return haveHandle;
    }
    static int 合併時間詞最大長度 = int.MinValue;
    static bool J48(string front1,string after1,bool moreNeu,double threshold)
    {
      if(front1 == "A") { if(0.809931506849315 >= threshold) { return false; } }//front1 = A: false (473.0/111.0)
      else if(front1 == "Caa")//front1 = Caa
      {
        if(after1 == "A") { if(0.88135593220339 >= threshold) { return false; } }//|   after1 = A: false (52.0/7.0)
        else if(after1 == "Caa") { if(0.833333333333333 >= threshold) { return true; } }//|   after1 = Caa: true (20.0/4.0)
        else if(after1 == "Cab") { if(0.705882352941177 >= threshold) { return true; } }//|   after1 = Cab: true (12.0/5.0)
        else if(after1 == "Cba") { if(0 >= threshold) { return false; } }//|   after1 = Cba: false (0.0)
        else if(after1 == "Cbab") { if(0 >= threshold) { return false; } }//|   after1 = Cbab: false (0.0)
        else if(after1 == "Cbb") { if(0.733333333333333 >= threshold) { return false; } }//|   after1 = Cbb: false (11.0/4.0)
        else if(after1 == "Cbaa") { if(0 >= threshold) { return false; } }//|   after1 = Cbaa: false (0.0)
        else if(after1 == "Cbba") { if(0 >= threshold) { return false; } }//|   after1 = Cbba: false (0.0)
        else if(after1 == "Cbbb") { if(0 >= threshold) { return false; } }//|   after1 = Cbbb: false (0.0)
        else if(after1 == "Cbca") { if(0 >= threshold) { return false; } }//|   after1 = Cbca: false (0.0)
        else if(after1 == "Cbcb") { if(0 >= threshold) { return false; } }//|   after1 = Cbcb: false (0.0)
        else if(after1 == "D")//|   after1 = D
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (9.0)
          else if(moreNeu == false) { if(0.742705570291777 >= threshold) { return true; } }//|   |   moreNeu = false: true (280.0/97.0)
        }
        else if(after1 == "Dab") { if(0 >= threshold) { return false; } }//|   after1 = Dab: false (0.0)
        else if(after1 == "Dbaa") { if(0 >= threshold) { return false; } }//|   after1 = Dbaa: false (0.0)
        else if(after1 == "Dbab") { if(0 >= threshold) { return false; } }//|   after1 = Dbab: false (0.0)
        else if(after1 == "Dbb") { if(0 >= threshold) { return false; } }//|   after1 = Dbb: false (0.0)
        else if(after1 == "Dbc") { if(0 >= threshold) { return false; } }//|   after1 = Dbc: false (0.0)
        else if(after1 == "Dc") { if(0 >= threshold) { return false; } }//|   after1 = Dc: false (0.0)
        else if(after1 == "Dd") { if(0 >= threshold) { return false; } }//|   after1 = Dd: false (0.0)
        else if(after1 == "Dg") { if(0 >= threshold) { return false; } }//|   after1 = Dg: false (0.0)
        else if(after1 == "Dh") { if(0 >= threshold) { return false; } }//|   after1 = Dh: false (0.0)
        else if(after1 == "Dj") { if(0 >= threshold) { return false; } }//|   after1 = Dj: false (0.0)
        else if(after1 == "Da") { if(0.703703703703704 >= threshold) { return true; } }//|   after1 = Da: true (19.0/8.0)
        else if(after1 == "Daa") { if(0 >= threshold) { return false; } }//|   after1 = Daa: false (0.0)
        else if(after1 == "DE") { if(0.67186067827681 >= threshold) { return false; } }//|   after1 = DE: false (733.0/358.0)
        else if(after1 == "Dfa") { if(0.685714285714286 >= threshold) { return true; } }//|   after1 = Dfa: true (24.0/11.0)
        else if(after1 == "Dfb") { if(0 >= threshold) { return false; } }//|   after1 = Dfb: false (0.0)
        else if(after1 == "Di") { if(0 >= threshold) { return false; } }//|   after1 = Di: false (0.0)
        else if(after1 == "Dk") { if(0 >= threshold) { return false; } }//|   after1 = Dk: false (0.0)
        else if(after1 == "FW") { if(0.815789473684211 >= threshold) { return false; } }//|   after1 = FW: false (31.0/7.0)
        else if(after1 == "I") { if(0 >= threshold) { return false; } }//|   after1 = I: false (0.0)
        else if(after1 == "Na") { if(0.79610538373425 >= threshold) { return false; } }//|   after1 = Na: false (1390.0/356.0)
        else if(after1 == "Naa") { if(0 >= threshold) { return false; } }//|   after1 = Naa: false (0.0)
        else if(after1 == "Nab") { if(0 >= threshold) { return false; } }//|   after1 = Nab: false (0.0)
        else if(after1 == "Nac") { if(0 >= threshold) { return false; } }//|   after1 = Nac: false (0.0)
        else if(after1 == "Nad") { if(0 >= threshold) { return false; } }//|   after1 = Nad: false (0.0)
        else if(after1 == "Naea") { if(0 >= threshold) { return false; } }//|   after1 = Naea: false (0.0)
        else if(after1 == "Naeb") { if(0 >= threshold) { return false; } }//|   after1 = Naeb: false (0.0)
        else if(after1 == "Nb")//|   after1 = Nb
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (2.0)
          else if(moreNeu == false) { if(0.727272727272727 >= threshold) { return true; } }//|   |   moreNeu = false: true (48.0/18.0)
        }
        else if(after1 == "Nba") { if(0 >= threshold) { return false; } }//|   after1 = Nba: false (0.0)
        else if(after1 == "Nbc") { if(0 >= threshold) { return false; } }//|   after1 = Nbc: false (0.0)
        else if(after1 == "Nc") { if(0.691082802547771 >= threshold) { return false; } }//|   after1 = Nc: false (217.0/97.0)
        else if(after1 == "Nca") { if(0 >= threshold) { return false; } }//|   after1 = Nca: false (0.0)
        else if(after1 == "Ncb") { if(0 >= threshold) { return false; } }//|   after1 = Ncb: false (0.0)
        else if(after1 == "Ncc") { if(0 >= threshold) { return false; } }//|   after1 = Ncc: false (0.0)
        else if(after1 == "Nce") { if(0 >= threshold) { return false; } }//|   after1 = Nce: false (0.0)
        else if(after1 == "Ncd") { if(0.733333333333333 >= threshold) { return true; } }//|   after1 = Ncd: true (11.0/4.0)
        else if(after1 == "Ncda") { if(0 >= threshold) { return false; } }//|   after1 = Ncda: false (0.0)
        else if(after1 == "Ncdb") { if(0 >= threshold) { return false; } }//|   after1 = Ncdb: false (0.0)
        else if(after1 == "Nd")//|   after1 = Nd
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (7.0)
          else if(moreNeu == false) { if(0.991150442477876 >= threshold) { return true; } }//|   |   moreNeu = false: true (560.0/5.0)
        }
        else if(after1 == "Ndaa") { if(0 >= threshold) { return false; } }//|   after1 = Ndaa: false (0.0)
        else if(after1 == "Ndab") { if(0 >= threshold) { return false; } }//|   after1 = Ndab: false (0.0)
        else if(after1 == "Ndc") { if(0 >= threshold) { return false; } }//|   after1 = Ndc: false (0.0)
        else if(after1 == "Ndd") { if(0 >= threshold) { return false; } }//|   after1 = Ndd: false (0.0)
        else if(after1 == "Nep") { if(0.68 >= threshold) { return true; } }//|   after1 = Nep: true (17.0/8.0)
        else if(after1 == "Neqa") { if(0.717948717948718 >= threshold) { return true; } }//|   after1 = Neqa: true (28.0/11.0)
        else if(after1 == "Neqb") { if(0.9375 >= threshold) { return false; } }//|   after1 = Neqb: false (30.0/2.0)
        else if(after1 == "Nes")//|   after1 = Nes
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (2.0)
          else if(moreNeu == false) { if(0.790697674418605 >= threshold) { return true; } }//|   |   moreNeu = false: true (34.0/9.0)
        }
        else if(after1 == "Neu") { if(0.686486486486486 >= threshold) { return false; } }//|   after1 = Neu: false (127.0/58.0)
        else if(after1 == "Nf") { if(1 >= threshold) { return false; } }//|   after1 = Nf: false (6.0)
        else if(after1 == "Nfa") { if(0 >= threshold) { return false; } }//|   after1 = Nfa: false (0.0)
        else if(after1 == "Nfb") { if(0 >= threshold) { return false; } }//|   after1 = Nfb: false (0.0)
        else if(after1 == "Nfc") { if(0 >= threshold) { return false; } }//|   after1 = Nfc: false (0.0)
        else if(after1 == "Nfd") { if(0 >= threshold) { return false; } }//|   after1 = Nfd: false (0.0)
        else if(after1 == "Nfe") { if(0 >= threshold) { return false; } }//|   after1 = Nfe: false (0.0)
        else if(after1 == "Nfg") { if(0 >= threshold) { return false; } }//|   after1 = Nfg: false (0.0)
        else if(after1 == "Nfh") { if(0 >= threshold) { return false; } }//|   after1 = Nfh: false (0.0)
        else if(after1 == "Nfi") { if(0 >= threshold) { return false; } }//|   after1 = Nfi: false (0.0)
        else if(after1 == "Ng") { if(0.732960893854749 >= threshold) { return false; } }//|   after1 = Ng: false (656.0/239.0)
        else if(after1 == "Nh") { if(0.72 >= threshold) { return true; } }//|   after1 = Nh: true (18.0/7.0)
        else if(after1 == "Nhaa") { if(0 >= threshold) { return false; } }//|   after1 = Nhaa: false (0.0)
        else if(after1 == "Nhab") { if(0 >= threshold) { return false; } }//|   after1 = Nhab: false (0.0)
        else if(after1 == "Nhac") { if(0 >= threshold) { return false; } }//|   after1 = Nhac: false (0.0)
        else if(after1 == "Nhb") { if(0 >= threshold) { return false; } }//|   after1 = Nhb: false (0.0)
        else if(after1 == "Nhc") { if(0 >= threshold) { return false; } }//|   after1 = Nhc: false (0.0)
        else if(after1 == "P")//|   after1 = P
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (3.0)
          else if(moreNeu == false) { if(0.886138613861386 >= threshold) { return true; } }//|   |   moreNeu = false: true (179.0/23.0)
        }
        else if(after1 == "SHI") { if(0.724137931034483 >= threshold) { return true; } }//|   after1 = SHI: true (42.0/16.0)
        else if(after1 == "V_11") { if(0 >= threshold) { return false; } }//|   after1 = V_11: false (0.0)
        else if(after1 == "T") { if(0.875 >= threshold) { return false; } }//|   after1 = T: false (7.0/1.0)
        else if(after1 == "Ta") { if(0 >= threshold) { return false; } }//|   after1 = Ta: false (0.0)
        else if(after1 == "Tb") { if(0 >= threshold) { return false; } }//|   after1 = Tb: false (0.0)
        else if(after1 == "Tc") { if(0 >= threshold) { return false; } }//|   after1 = Tc: false (0.0)
        else if(after1 == "Td") { if(0 >= threshold) { return false; } }//|   after1 = Td: false (0.0)
        else if(after1 == "VA")//|   after1 = VA
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (2.0)
          else if(moreNeu == false) { if(0.73134328358209 >= threshold) { return true; } }//|   |   moreNeu = false: true (98.0/36.0)
        }
        else if(after1 == "VA11") { if(0 >= threshold) { return false; } }//|   after1 = VA11: false (0.0)
        else if(after1 == "VA12") { if(0 >= threshold) { return false; } }//|   after1 = VA12: false (0.0)
        else if(after1 == "VA13") { if(0 >= threshold) { return false; } }//|   after1 = VA13: false (0.0)
        else if(after1 == "VA3") { if(0 >= threshold) { return false; } }//|   after1 = VA3: false (0.0)
        else if(after1 == "VA4") { if(0 >= threshold) { return false; } }//|   after1 = VA4: false (0.0)
        else if(after1 == "VAC") { if(0 >= threshold) { return false; } }//|   after1 = VAC: false (0.0)
        else if(after1 == "VA2") { if(0 >= threshold) { return false; } }//|   after1 = VA2: false (0.0)
        else if(after1 == "VB") { if(0.692307692307692 >= threshold) { return false; } }//|   after1 = VB: false (9.0/4.0)
        else if(after1 == "VB11") { if(0 >= threshold) { return false; } }//|   after1 = VB11: false (0.0)
        else if(after1 == "VB12") { if(0 >= threshold) { return false; } }//|   after1 = VB12: false (0.0)
        else if(after1 == "VB2") { if(0 >= threshold) { return false; } }//|   after1 = VB2: false (0.0)
        else if(after1 == "VC")//|   after1 = VC
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (6.0)
          else if(moreNeu == false) { if(0.75975975975976 >= threshold) { return true; } }//|   |   moreNeu = false: true (253.0/80.0)
        }
        else if(after1 == "VC2") { if(0 >= threshold) { return false; } }//|   after1 = VC2: false (0.0)
        else if(after1 == "VC31") { if(0 >= threshold) { return false; } }//|   after1 = VC31: false (0.0)
        else if(after1 == "VC32") { if(0 >= threshold) { return false; } }//|   after1 = VC32: false (0.0)
        else if(after1 == "VC33") { if(0 >= threshold) { return false; } }//|   after1 = VC33: false (0.0)
        else if(after1 == "VCL") { if(0.90625 >= threshold) { return true; } }//|   after1 = VCL: true (29.0/3.0)
        else if(after1 == "VC1") { if(0 >= threshold) { return false; } }//|   after1 = VC1: false (0.0)
        else if(after1 == "VD") { if(0.692307692307692 >= threshold) { return false; } }//|   after1 = VD: false (9.0/4.0)
        else if(after1 == "VD1") { if(0 >= threshold) { return false; } }//|   after1 = VD1: false (0.0)
        else if(after1 == "VD2") { if(0 >= threshold) { return false; } }//|   after1 = VD2: false (0.0)
        else if(after1 == "VE") { if(0.8125 >= threshold) { return true; } }//|   after1 = VE: true (26.0/6.0)
        else if(after1 == "VE11") { if(0 >= threshold) { return false; } }//|   after1 = VE11: false (0.0)
        else if(after1 == "VE12") { if(0 >= threshold) { return false; } }//|   after1 = VE12: false (0.0)
        else if(after1 == "VE2") { if(0 >= threshold) { return false; } }//|   after1 = VE2: false (0.0)
        else if(after1 == "VF") { if(0.875 >= threshold) { return true; } }//|   after1 = VF: true (7.0/1.0)
        else if(after1 == "VF1") { if(0 >= threshold) { return false; } }//|   after1 = VF1: false (0.0)
        else if(after1 == "VF2") { if(0 >= threshold) { return false; } }//|   after1 = VF2: false (0.0)
        else if(after1 == "VG")//|   after1 = VG
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (4.0)
          else if(moreNeu == false) { if(0.740740740740741 >= threshold) { return true; } }//|   |   moreNeu = false: true (40.0/14.0)
        }
        else if(after1 == "VG1") { if(0 >= threshold) { return false; } }//|   after1 = VG1: false (0.0)
        else if(after1 == "VG2") { if(0 >= threshold) { return false; } }//|   after1 = VG2: false (0.0)
        else if(after1 == "VH") { if(0.734042553191489 >= threshold) { return false; } }//|   after1 = VH: false (276.0/100.0)
        else if(after1 == "VH11") { if(0 >= threshold) { return false; } }//|   after1 = VH11: false (0.0)
        else if(after1 == "VH12") { if(0 >= threshold) { return false; } }//|   after1 = VH12: false (0.0)
        else if(after1 == "VH13") { if(0 >= threshold) { return false; } }//|   after1 = VH13: false (0.0)
        else if(after1 == "VH14") { if(0 >= threshold) { return false; } }//|   after1 = VH14: false (0.0)
        else if(after1 == "VH15") { if(0 >= threshold) { return false; } }//|   after1 = VH15: false (0.0)
        else if(after1 == "VH17") { if(0 >= threshold) { return false; } }//|   after1 = VH17: false (0.0)
        else if(after1 == "VH21") { if(0 >= threshold) { return false; } }//|   after1 = VH21: false (0.0)
        else if(after1 == "VHC") { if(0.777777777777778 >= threshold) { return false; } }//|   after1 = VHC: false (14.0/4.0)
        else if(after1 == "VH16") { if(0 >= threshold) { return false; } }//|   after1 = VH16: false (0.0)
        else if(after1 == "VH22") { if(0 >= threshold) { return false; } }//|   after1 = VH22: false (0.0)
        else if(after1 == "VI") { if(0.888888888888889 >= threshold) { return false; } }//|   after1 = VI: false (8.0/1.0)
        else if(after1 == "VI1") { if(0 >= threshold) { return false; } }//|   after1 = VI1: false (0.0)
        else if(after1 == "VI2") { if(0 >= threshold) { return false; } }//|   after1 = VI2: false (0.0)
        else if(after1 == "VI3") { if(0 >= threshold) { return false; } }//|   after1 = VI3: false (0.0)
        else if(after1 == "VJ") { if(0.694444444444444 >= threshold) { return true; } }//|   after1 = VJ: true (50.0/22.0)
        else if(after1 == "VJ1") { if(0 >= threshold) { return false; } }//|   after1 = VJ1: false (0.0)
        else if(after1 == "VJ2") { if(0 >= threshold) { return false; } }//|   after1 = VJ2: false (0.0)
        else if(after1 == "VJ3") { if(0 >= threshold) { return false; } }//|   after1 = VJ3: false (0.0)
        else if(after1 == "VK")//|   after1 = VK
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (2.0)
          else if(moreNeu == false) { if(0.727272727272727 >= threshold) { return true; } }//|   |   moreNeu = false: true (8.0/3.0)
        }
        else if(after1 == "VK1") { if(0 >= threshold) { return false; } }//|   after1 = VK1: false (0.0)
        else if(after1 == "VK2") { if(0 >= threshold) { return false; } }//|   after1 = VK2: false (0.0)
        else if(after1 == "VL") { if(0.6875 >= threshold) { return true; } }//|   after1 = VL: true (11.0/5.0)
        else if(after1 == "VL1") { if(0 >= threshold) { return false; } }//|   after1 = VL1: false (0.0)
        else if(after1 == "VL2") { if(0 >= threshold) { return false; } }//|   after1 = VL2: false (0.0)
        else if(after1 == "VL3") { if(0 >= threshold) { return false; } }//|   after1 = VL3: false (0.0)
        else if(after1 == "VL4") { if(0 >= threshold) { return false; } }//|   after1 = VL4: false (0.0)
        else if(after1 == "V_2") { if(0.72 >= threshold) { return true; } }//|   after1 = V_2: true (18.0/7.0)
        else if(after1 == "Nv") { if(0.75 >= threshold) { return false; } }//|   after1 = Nv: false (45.0/15.0)
        else if(after1 == "Nv1") { if(0 >= threshold) { return false; } }//|   after1 = Nv1: false (0.0)
        else if(after1 == "Nv2") { if(0 >= threshold) { return false; } }//|   after1 = Nv2: false (0.0)
        else if(after1 == "Nv3") { if(0 >= threshold) { return false; } }//|   after1 = Nv3: false (0.0)
        else if(after1 == "Nv4") { if(0 >= threshold) { return false; } }//|   after1 = Nv4: false (0.0)
        else if(after1 == "b") { if(0 >= threshold) { return false; } }//|   after1 = b: false (0.0)
        else if(after1 == "notword") { if(0.69339356295878 >= threshold) { return false; } }//|   after1 = notword: false (1228.0/543.0)
        else if(after1 == "begin") { if(0 >= threshold) { return false; } }//|   after1 = begin: false (0.0)
        else if(after1 == "end") { if(1 >= threshold) { return true; } }//|   after1 = end: true (2.0)
      }
      else if(front1 == "Cab") { if(0.933673469387755 >= threshold) { return false; } }//front1 = Cab: false (915.0/65.0)
      else if(front1 == "Cba") { if(0.666666666666667 >= threshold) { return true; } }//front1 = Cba: true (2.0/1.0)
      else if(front1 == "Cbab") { if(0 >= threshold) { return true; } }//front1 = Cbab: true (0.0)
      else if(front1 == "Cbb")//front1 = Cbb
      {
        if(moreNeu == true) { if(0.994444444444444 >= threshold) { return false; } }//|   moreNeu = true: false (179.0/1.0)
        else if(moreNeu == false) { if(0.813700918964077 >= threshold) { return true; } }//|   moreNeu = false: true (7792.0/1784.0)
      }
      else if(front1 == "Cbaa") { if(0 >= threshold) { return true; } }//front1 = Cbaa: true (0.0)
      else if(front1 == "Cbba") { if(0 >= threshold) { return true; } }//front1 = Cbba: true (0.0)
      else if(front1 == "Cbbb") { if(0 >= threshold) { return true; } }//front1 = Cbbb: true (0.0)
      else if(front1 == "Cbca") { if(0 >= threshold) { return true; } }//front1 = Cbca: true (0.0)
      else if(front1 == "Cbcb") { if(0 >= threshold) { return true; } }//front1 = Cbcb: true (0.0)
      else if(front1 == "D")//front1 = D
      {
        if(after1 == "A") { if(0.88 >= threshold) { return false; } }//|   after1 = A: false (22.0/3.0)
        else if(after1 == "Caa") { if(0.666666666666667 >= threshold) { return true; } }//|   after1 = Caa: true (28.0/14.0)
        else if(after1 == "Cab") { if(0 >= threshold) { return false; } }//|   after1 = Cab: false (0.0)
        else if(after1 == "Cba") { if(1 >= threshold) { return true; } }//|   after1 = Cba: true (3.0)
        else if(after1 == "Cbab") { if(0 >= threshold) { return false; } }//|   after1 = Cbab: false (0.0)
        else if(after1 == "Cbb") { if(0.67741935483871 >= threshold) { return false; } }//|   after1 = Cbb: false (21.0/10.0)
        else if(after1 == "Cbaa") { if(0 >= threshold) { return false; } }//|   after1 = Cbaa: false (0.0)
        else if(after1 == "Cbba") { if(0 >= threshold) { return false; } }//|   after1 = Cbba: false (0.0)
        else if(after1 == "Cbbb") { if(0 >= threshold) { return false; } }//|   after1 = Cbbb: false (0.0)
        else if(after1 == "Cbca") { if(0 >= threshold) { return false; } }//|   after1 = Cbca: false (0.0)
        else if(after1 == "Cbcb") { if(0 >= threshold) { return false; } }//|   after1 = Cbcb: false (0.0)
        else if(after1 == "D")//|   after1 = D
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (12.0)
          else if(moreNeu == false) { if(0.729001584786054 >= threshold) { return true; } }//|   |   moreNeu = false: true (460.0/171.0)
        }
        else if(after1 == "Dab") { if(0 >= threshold) { return false; } }//|   after1 = Dab: false (0.0)
        else if(after1 == "Dbaa") { if(0 >= threshold) { return false; } }//|   after1 = Dbaa: false (0.0)
        else if(after1 == "Dbab") { if(0 >= threshold) { return false; } }//|   after1 = Dbab: false (0.0)
        else if(after1 == "Dbb") { if(0 >= threshold) { return false; } }//|   after1 = Dbb: false (0.0)
        else if(after1 == "Dbc") { if(0 >= threshold) { return false; } }//|   after1 = Dbc: false (0.0)
        else if(after1 == "Dc") { if(0 >= threshold) { return false; } }//|   after1 = Dc: false (0.0)
        else if(after1 == "Dd") { if(0 >= threshold) { return false; } }//|   after1 = Dd: false (0.0)
        else if(after1 == "Dg") { if(0 >= threshold) { return false; } }//|   after1 = Dg: false (0.0)
        else if(after1 == "Dh") { if(0 >= threshold) { return false; } }//|   after1 = Dh: false (0.0)
        else if(after1 == "Dj") { if(0 >= threshold) { return false; } }//|   after1 = Dj: false (0.0)
        else if(after1 == "Da") { if(0.72 >= threshold) { return true; } }//|   after1 = Da: true (54.0/21.0)
        else if(after1 == "Daa") { if(0 >= threshold) { return false; } }//|   after1 = Daa: false (0.0)
        else if(after1 == "DE") { if(0.727477477477477 >= threshold) { return false; } }//|   after1 = DE: false (323.0/121.0)
        else if(after1 == "Dfa") { if(0.711111111111111 >= threshold) { return false; } }//|   after1 = Dfa: false (32.0/13.0)
        else if(after1 == "Dfb") { if(0 >= threshold) { return false; } }//|   after1 = Dfb: false (0.0)
        else if(after1 == "Di") { if(1 >= threshold) { return true; } }//|   after1 = Di: true (1.0)
        else if(after1 == "Dk") { if(0 >= threshold) { return false; } }//|   after1 = Dk: false (0.0)
        else if(after1 == "FW") { if(0.777777777777778 >= threshold) { return false; } }//|   after1 = FW: false (7.0/2.0)
        else if(after1 == "I") { if(1 >= threshold) { return false; } }//|   after1 = I: false (4.0)
        else if(after1 == "Na") { if(0.883074407195421 >= threshold) { return false; } }//|   after1 = Na: false (1080.0/143.0)
        else if(after1 == "Naa") { if(0 >= threshold) { return false; } }//|   after1 = Naa: false (0.0)
        else if(after1 == "Nab") { if(0 >= threshold) { return false; } }//|   after1 = Nab: false (0.0)
        else if(after1 == "Nac") { if(0 >= threshold) { return false; } }//|   after1 = Nac: false (0.0)
        else if(after1 == "Nad") { if(0 >= threshold) { return false; } }//|   after1 = Nad: false (0.0)
        else if(after1 == "Naea") { if(0 >= threshold) { return false; } }//|   after1 = Naea: false (0.0)
        else if(after1 == "Naeb") { if(0 >= threshold) { return false; } }//|   after1 = Naeb: false (0.0)
        else if(after1 == "Nb")//|   after1 = Nb
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (2.0)
          else if(moreNeu == false) { if(0.717391304347826 >= threshold) { return true; } }//|   |   moreNeu = false: true (33.0/13.0)
        }
        else if(after1 == "Nba") { if(0 >= threshold) { return false; } }//|   after1 = Nba: false (0.0)
        else if(after1 == "Nbc") { if(0 >= threshold) { return false; } }//|   after1 = Nbc: false (0.0)
        else if(after1 == "Nc") { if(0.71969696969697 >= threshold) { return true; } }//|   after1 = Nc: true (95.0/37.0)
        else if(after1 == "Nca") { if(0 >= threshold) { return false; } }//|   after1 = Nca: false (0.0)
        else if(after1 == "Ncb") { if(0 >= threshold) { return false; } }//|   after1 = Ncb: false (0.0)
        else if(after1 == "Ncc") { if(0 >= threshold) { return false; } }//|   after1 = Ncc: false (0.0)
        else if(after1 == "Nce") { if(0 >= threshold) { return false; } }//|   after1 = Nce: false (0.0)
        else if(after1 == "Ncd") { if(0.833333333333333 >= threshold) { return false; } }//|   after1 = Ncd: false (5.0/1.0)
        else if(after1 == "Ncda") { if(0 >= threshold) { return false; } }//|   after1 = Ncda: false (0.0)
        else if(after1 == "Ncdb") { if(0 >= threshold) { return false; } }//|   after1 = Ncdb: false (0.0)
        else if(after1 == "Nd") { if(0.875 >= threshold) { return true; } }//|   after1 = Nd: true (119.0/17.0)
        else if(after1 == "Ndaa") { if(0 >= threshold) { return false; } }//|   after1 = Ndaa: false (0.0)
        else if(after1 == "Ndab") { if(0 >= threshold) { return false; } }//|   after1 = Ndab: false (0.0)
        else if(after1 == "Ndc") { if(0 >= threshold) { return false; } }//|   after1 = Ndc: false (0.0)
        else if(after1 == "Ndd") { if(0 >= threshold) { return false; } }//|   after1 = Ndd: false (0.0)
        else if(after1 == "Nep") { if(0.789473684210526 >= threshold) { return true; } }//|   after1 = Nep: true (15.0/4.0)
        else if(after1 == "Neqa") { if(0.680851063829787 >= threshold) { return true; } }//|   after1 = Neqa: true (32.0/15.0)
        else if(after1 == "Neqb") { if(0.931506849315068 >= threshold) { return false; } }//|   after1 = Neqb: false (68.0/5.0)
        else if(after1 == "Nes") { if(0.923076923076923 >= threshold) { return true; } }//|   after1 = Nes: true (12.0/1.0)
        else if(after1 == "Neu") { if(0.745454545454545 >= threshold) { return false; } }//|   after1 = Neu: false (123.0/42.0)
        else if(after1 == "Nf") { if(0.727272727272727 >= threshold) { return false; } }//|   after1 = Nf: false (8.0/3.0)
        else if(after1 == "Nfa") { if(0 >= threshold) { return false; } }//|   after1 = Nfa: false (0.0)
        else if(after1 == "Nfb") { if(0 >= threshold) { return false; } }//|   after1 = Nfb: false (0.0)
        else if(after1 == "Nfc") { if(0 >= threshold) { return false; } }//|   after1 = Nfc: false (0.0)
        else if(after1 == "Nfd") { if(0 >= threshold) { return false; } }//|   after1 = Nfd: false (0.0)
        else if(after1 == "Nfe") { if(0 >= threshold) { return false; } }//|   after1 = Nfe: false (0.0)
        else if(after1 == "Nfg") { if(0 >= threshold) { return false; } }//|   after1 = Nfg: false (0.0)
        else if(after1 == "Nfh") { if(0 >= threshold) { return false; } }//|   after1 = Nfh: false (0.0)
        else if(after1 == "Nfi") { if(0 >= threshold) { return false; } }//|   after1 = Nfi: false (0.0)
        else if(after1 == "Ng") { if(0.748936170212766 >= threshold) { return false; } }//|   after1 = Ng: false (176.0/59.0)
        else if(after1 == "Nh") { if(0.888888888888889 >= threshold) { return true; } }//|   after1 = Nh: true (80.0/10.0)
        else if(after1 == "Nhaa") { if(0 >= threshold) { return false; } }//|   after1 = Nhaa: false (0.0)
        else if(after1 == "Nhab") { if(0 >= threshold) { return false; } }//|   after1 = Nhab: false (0.0)
        else if(after1 == "Nhac") { if(0 >= threshold) { return false; } }//|   after1 = Nhac: false (0.0)
        else if(after1 == "Nhb") { if(0 >= threshold) { return false; } }//|   after1 = Nhb: false (0.0)
        else if(after1 == "Nhc") { if(0 >= threshold) { return false; } }//|   after1 = Nhc: false (0.0)
        else if(after1 == "P")//|   after1 = P
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (7.0)
          else if(moreNeu == false) { if(0.674418604651163 >= threshold) { return true; } }//|   |   moreNeu = false: true (203.0/98.0)
        }
        else if(after1 == "SHI")//|   after1 = SHI
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (3.0)
          else if(moreNeu == false) { if(0.790697674418605 >= threshold) { return true; } }//|   |   moreNeu = false: true (34.0/9.0)
        }
        else if(after1 == "V_11") { if(0 >= threshold) { return false; } }//|   after1 = V_11: false (0.0)
        else if(after1 == "T") { if(0.845714285714286 >= threshold) { return false; } }//|   after1 = T: false (148.0/27.0)
        else if(after1 == "Ta") { if(0 >= threshold) { return false; } }//|   after1 = Ta: false (0.0)
        else if(after1 == "Tb") { if(0 >= threshold) { return false; } }//|   after1 = Tb: false (0.0)
        else if(after1 == "Tc") { if(0 >= threshold) { return false; } }//|   after1 = Tc: false (0.0)
        else if(after1 == "Td") { if(0 >= threshold) { return false; } }//|   after1 = Td: false (0.0)
        else if(after1 == "VA")//|   after1 = VA
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (8.0)
          else if(moreNeu == false) { if(0.682051282051282 >= threshold) { return true; } }//|   |   moreNeu = false: true (133.0/62.0)
        }
        else if(after1 == "VA11") { if(0 >= threshold) { return false; } }//|   after1 = VA11: false (0.0)
        else if(after1 == "VA12") { if(0 >= threshold) { return false; } }//|   after1 = VA12: false (0.0)
        else if(after1 == "VA13") { if(0 >= threshold) { return false; } }//|   after1 = VA13: false (0.0)
        else if(after1 == "VA3") { if(0 >= threshold) { return false; } }//|   after1 = VA3: false (0.0)
        else if(after1 == "VA4") { if(0 >= threshold) { return false; } }//|   after1 = VA4: false (0.0)
        else if(after1 == "VAC") { if(0.833333333333333 >= threshold) { return false; } }//|   after1 = VAC: false (5.0/1.0)
        else if(after1 == "VA2") { if(0 >= threshold) { return false; } }//|   after1 = VA2: false (0.0)
        else if(after1 == "VB")//|   after1 = VB
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (2.0)
          else if(moreNeu == false) { if(0.705882352941177 >= threshold) { return true; } }//|   |   moreNeu = false: true (12.0/5.0)
        }
        else if(after1 == "VB11") { if(0 >= threshold) { return false; } }//|   after1 = VB11: false (0.0)
        else if(after1 == "VB12") { if(0 >= threshold) { return false; } }//|   after1 = VB12: false (0.0)
        else if(after1 == "VB2") { if(0 >= threshold) { return false; } }//|   after1 = VB2: false (0.0)
        else if(after1 == "VC")//|   after1 = VC
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (13.0)
          else if(moreNeu == false) { if(0.748735244519393 >= threshold) { return true; } }//|   |   moreNeu = false: true (444.0/149.0)
        }
        else if(after1 == "VC2") { if(0 >= threshold) { return false; } }//|   after1 = VC2: false (0.0)
        else if(after1 == "VC31") { if(0 >= threshold) { return false; } }//|   after1 = VC31: false (0.0)
        else if(after1 == "VC32") { if(0 >= threshold) { return false; } }//|   after1 = VC32: false (0.0)
        else if(after1 == "VC33") { if(0 >= threshold) { return false; } }//|   after1 = VC33: false (0.0)
        else if(after1 == "VCL") { if(0.702380952380952 >= threshold) { return false; } }//|   after1 = VCL: false (59.0/25.0)
        else if(after1 == "VC1") { if(0 >= threshold) { return false; } }//|   after1 = VC1: false (0.0)
        else if(after1 == "VD") { if(0.769230769230769 >= threshold) { return true; } }//|   after1 = VD: true (20.0/6.0)
        else if(after1 == "VD1") { if(0 >= threshold) { return false; } }//|   after1 = VD1: false (0.0)
        else if(after1 == "VD2") { if(0 >= threshold) { return false; } }//|   after1 = VD2: false (0.0)
        else if(after1 == "VE")//|   after1 = VE
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (15.0)
          else if(moreNeu == false) { if(0.794326241134752 >= threshold) { return true; } }//|   |   moreNeu = false: true (112.0/29.0)
        }
        else if(after1 == "VE11") { if(0 >= threshold) { return false; } }//|   after1 = VE11: false (0.0)
        else if(after1 == "VE12") { if(0 >= threshold) { return false; } }//|   after1 = VE12: false (0.0)
        else if(after1 == "VE2") { if(0 >= threshold) { return false; } }//|   after1 = VE2: false (0.0)
        else if(after1 == "VF")//|   after1 = VF
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (2.0)
          else if(moreNeu == false) { if(0.7 >= threshold) { return true; } }//|   |   moreNeu = false: true (14.0/6.0)
        }
        else if(after1 == "VF1") { if(0 >= threshold) { return false; } }//|   after1 = VF1: false (0.0)
        else if(after1 == "VF2") { if(0 >= threshold) { return false; } }//|   after1 = VF2: false (0.0)
        else if(after1 == "VG")//|   after1 = VG
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (2.0)
          else if(moreNeu == false) { if(0.739130434782609 >= threshold) { return true; } }//|   |   moreNeu = false: true (34.0/12.0)
        }
        else if(after1 == "VG1") { if(0 >= threshold) { return false; } }//|   after1 = VG1: false (0.0)
        else if(after1 == "VG2") { if(0 >= threshold) { return false; } }//|   after1 = VG2: false (0.0)
        else if(after1 == "VH") { if(0.805496828752643 >= threshold) { return false; } }//|   after1 = VH: false (381.0/92.0)
        else if(after1 == "VH11") { if(0 >= threshold) { return false; } }//|   after1 = VH11: false (0.0)
        else if(after1 == "VH12") { if(0 >= threshold) { return false; } }//|   after1 = VH12: false (0.0)
        else if(after1 == "VH13") { if(0 >= threshold) { return false; } }//|   after1 = VH13: false (0.0)
        else if(after1 == "VH14") { if(0 >= threshold) { return false; } }//|   after1 = VH14: false (0.0)
        else if(after1 == "VH15") { if(0 >= threshold) { return false; } }//|   after1 = VH15: false (0.0)
        else if(after1 == "VH17") { if(0 >= threshold) { return false; } }//|   after1 = VH17: false (0.0)
        else if(after1 == "VH21") { if(0 >= threshold) { return false; } }//|   after1 = VH21: false (0.0)
        else if(after1 == "VHC") { if(0.739130434782609 >= threshold) { return false; } }//|   after1 = VHC: false (17.0/6.0)
        else if(after1 == "VH16") { if(0 >= threshold) { return false; } }//|   after1 = VH16: false (0.0)
        else if(after1 == "VH22") { if(0 >= threshold) { return false; } }//|   after1 = VH22: false (0.0)
        else if(after1 == "VI") { if(1 >= threshold) { return true; } }//|   after1 = VI: true (3.0)
        else if(after1 == "VI1") { if(0 >= threshold) { return false; } }//|   after1 = VI1: false (0.0)
        else if(after1 == "VI2") { if(0 >= threshold) { return false; } }//|   after1 = VI2: false (0.0)
        else if(after1 == "VI3") { if(0 >= threshold) { return false; } }//|   after1 = VI3: false (0.0)
        else if(after1 == "VJ")//|   after1 = VJ
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (4.0)
          else if(moreNeu == false) { if(0.76530612244898 >= threshold) { return true; } }//|   |   moreNeu = false: true (150.0/46.0)
        }
        else if(after1 == "VJ1") { if(0 >= threshold) { return false; } }//|   after1 = VJ1: false (0.0)
        else if(after1 == "VJ2") { if(0 >= threshold) { return false; } }//|   after1 = VJ2: false (0.0)
        else if(after1 == "VJ3") { if(0 >= threshold) { return false; } }//|   after1 = VJ3: false (0.0)
        else if(after1 == "VK") { if(0.709677419354839 >= threshold) { return true; } }//|   after1 = VK: true (44.0/18.0)
        else if(after1 == "VK1") { if(0 >= threshold) { return false; } }//|   after1 = VK1: false (0.0)
        else if(after1 == "VK2") { if(0 >= threshold) { return false; } }//|   after1 = VK2: false (0.0)
        else if(after1 == "VL") { if(0.761904761904762 >= threshold) { return true; } }//|   after1 = VL: true (16.0/5.0)
        else if(after1 == "VL1") { if(0 >= threshold) { return false; } }//|   after1 = VL1: false (0.0)
        else if(after1 == "VL2") { if(0 >= threshold) { return false; } }//|   after1 = VL2: false (0.0)
        else if(after1 == "VL3") { if(0 >= threshold) { return false; } }//|   after1 = VL3: false (0.0)
        else if(after1 == "VL4") { if(0 >= threshold) { return false; } }//|   after1 = VL4: false (0.0)
        else if(after1 == "V_2") { if(0.836734693877551 >= threshold) { return true; } }//|   after1 = V_2: true (41.0/8.0)
        else if(after1 == "Nv") { if(0.870967741935484 >= threshold) { return false; } }//|   after1 = Nv: false (27.0/4.0)
        else if(after1 == "Nv1") { if(0 >= threshold) { return false; } }//|   after1 = Nv1: false (0.0)
        else if(after1 == "Nv2") { if(0 >= threshold) { return false; } }//|   after1 = Nv2: false (0.0)
        else if(after1 == "Nv3") { if(0 >= threshold) { return false; } }//|   after1 = Nv3: false (0.0)
        else if(after1 == "Nv4") { if(0 >= threshold) { return false; } }//|   after1 = Nv4: false (0.0)
        else if(after1 == "b") { if(0 >= threshold) { return false; } }//|   after1 = b: false (0.0)
        else if(after1 == "notword") { if(0.808909730363423 >= threshold) { return false; } }//|   after1 = notword: false (690.0/163.0)
        else if(after1 == "begin") { if(0 >= threshold) { return false; } }//|   after1 = begin: false (0.0)
        else if(after1 == "end") { if(0 >= threshold) { return false; } }//|   after1 = end: false (0.0)
      }
      else if(front1 == "Dab") { if(0 >= threshold) { return true; } }//front1 = Dab: true (0.0)
      else if(front1 == "Dbaa") { if(0 >= threshold) { return true; } }//front1 = Dbaa: true (0.0)
      else if(front1 == "Dbab") { if(0 >= threshold) { return true; } }//front1 = Dbab: true (0.0)
      else if(front1 == "Dbb") { if(0 >= threshold) { return true; } }//front1 = Dbb: true (0.0)
      else if(front1 == "Dbc") { if(0 >= threshold) { return true; } }//front1 = Dbc: true (0.0)
      else if(front1 == "Dc") { if(0 >= threshold) { return true; } }//front1 = Dc: true (0.0)
      else if(front1 == "Dd") { if(0 >= threshold) { return true; } }//front1 = Dd: true (0.0)
      else if(front1 == "Dg") { if(0 >= threshold) { return true; } }//front1 = Dg: true (0.0)
      else if(front1 == "Dh") { if(0 >= threshold) { return true; } }//front1 = Dh: true (0.0)
      else if(front1 == "Dj") { if(0 >= threshold) { return true; } }//front1 = Dj: true (0.0)
      else if(front1 == "Da") { if(0.946756568239944 >= threshold) { return false; } }//front1 = Da: false (4072.0/229.0)
      else if(front1 == "Daa") { if(1 >= threshold) { return false; } }//front1 = Daa: false (1.0)
      else if(front1 == "DE")//front1 = DE
      {
        if(after1 == "A") { if(0.963768115942029 >= threshold) { return false; } }//|   after1 = A: false (133.0/5.0)
        else if(after1 == "Caa")//|   after1 = Caa
        {
          if(moreNeu == true) { if(0.8 >= threshold) { return false; } }//|   |   moreNeu = true: false (4.0/1.0)
          else if(moreNeu == false) { if(0.73109243697479 >= threshold) { return true; } }//|   |   moreNeu = false: true (87.0/32.0)
        }
        else if(after1 == "Cab") { if(1 >= threshold) { return true; } }//|   after1 = Cab: true (2.0)
        else if(after1 == "Cba") { if(1 >= threshold) { return true; } }//|   after1 = Cba: true (1.0)
        else if(after1 == "Cbab") { if(0 >= threshold) { return false; } }//|   after1 = Cbab: false (0.0)
        else if(after1 == "Cbb")//|   after1 = Cbb
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (2.0)
          else if(moreNeu == false) { if(0.794117647058823 >= threshold) { return true; } }//|   |   moreNeu = false: true (27.0/7.0)
        }
        else if(after1 == "Cbaa") { if(0 >= threshold) { return false; } }//|   after1 = Cbaa: false (0.0)
        else if(after1 == "Cbba") { if(0 >= threshold) { return false; } }//|   after1 = Cbba: false (0.0)
        else if(after1 == "Cbbb") { if(0 >= threshold) { return false; } }//|   after1 = Cbbb: false (0.0)
        else if(after1 == "Cbca") { if(0 >= threshold) { return false; } }//|   after1 = Cbca: false (0.0)
        else if(after1 == "Cbcb") { if(0 >= threshold) { return false; } }//|   after1 = Cbcb: false (0.0)
        else if(after1 == "D")//|   after1 = D
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (31.0)
          else if(moreNeu == false) { if(0.748299319727891 >= threshold) { return true; } }//|   |   moreNeu = false: true (440.0/148.0)
        }
        else if(after1 == "Dab") { if(0 >= threshold) { return false; } }//|   after1 = Dab: false (0.0)
        else if(after1 == "Dbaa") { if(0 >= threshold) { return false; } }//|   after1 = Dbaa: false (0.0)
        else if(after1 == "Dbab") { if(0 >= threshold) { return false; } }//|   after1 = Dbab: false (0.0)
        else if(after1 == "Dbb") { if(0 >= threshold) { return false; } }//|   after1 = Dbb: false (0.0)
        else if(after1 == "Dbc") { if(0 >= threshold) { return false; } }//|   after1 = Dbc: false (0.0)
        else if(after1 == "Dc") { if(0 >= threshold) { return false; } }//|   after1 = Dc: false (0.0)
        else if(after1 == "Dd") { if(0 >= threshold) { return false; } }//|   after1 = Dd: false (0.0)
        else if(after1 == "Dg") { if(0 >= threshold) { return false; } }//|   after1 = Dg: false (0.0)
        else if(after1 == "Dh") { if(0 >= threshold) { return false; } }//|   after1 = Dh: false (0.0)
        else if(after1 == "Dj") { if(0 >= threshold) { return false; } }//|   after1 = Dj: false (0.0)
        else if(after1 == "Da")//|   after1 = Da
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (4.0)
          else if(moreNeu == false) { if(0.75 >= threshold) { return true; } }//|   |   moreNeu = false: true (24.0/8.0)
        }
        else if(after1 == "Daa") { if(0 >= threshold) { return false; } }//|   after1 = Daa: false (0.0)
        else if(after1 == "DE")//|   after1 = DE
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (6.0)
          else if(moreNeu == false) { if(0.764705882352941 >= threshold) { return true; } }//|   |   moreNeu = false: true (117.0/36.0)
        }
        else if(after1 == "Dfa") { if(0.704761904761905 >= threshold) { return false; } }//|   after1 = Dfa: false (74.0/31.0)
        else if(after1 == "Dfb") { if(0 >= threshold) { return false; } }//|   after1 = Dfb: false (0.0)
        else if(after1 == "Di") { if(0 >= threshold) { return false; } }//|   after1 = Di: false (0.0)
        else if(after1 == "Dk") { if(0.666666666666667 >= threshold) { return true; } }//|   after1 = Dk: true (2.0/1.0)
        else if(after1 == "FW") { if(0.909090909090909 >= threshold) { return false; } }//|   after1 = FW: false (40.0/4.0)
        else if(after1 == "I") { if(1 >= threshold) { return false; } }//|   after1 = I: false (2.0)
        else if(after1 == "Na") { if(0.863985476888703 >= threshold) { return false; } }//|   after1 = Na: false (6187.0/974.0)
        else if(after1 == "Naa") { if(0 >= threshold) { return false; } }//|   after1 = Naa: false (0.0)
        else if(after1 == "Nab") { if(0 >= threshold) { return false; } }//|   after1 = Nab: false (0.0)
        else if(after1 == "Nac") { if(0 >= threshold) { return false; } }//|   after1 = Nac: false (0.0)
        else if(after1 == "Nad") { if(0 >= threshold) { return false; } }//|   after1 = Nad: false (0.0)
        else if(after1 == "Naea") { if(0 >= threshold) { return false; } }//|   after1 = Naea: false (0.0)
        else if(after1 == "Naeb") { if(0 >= threshold) { return false; } }//|   after1 = Naeb: false (0.0)
        else if(after1 == "Nb") { if(0.821138211382114 >= threshold) { return false; } }//|   after1 = Nb: false (101.0/22.0)
        else if(after1 == "Nba") { if(0 >= threshold) { return false; } }//|   after1 = Nba: false (0.0)
        else if(after1 == "Nbc") { if(0 >= threshold) { return false; } }//|   after1 = Nbc: false (0.0)
        else if(after1 == "Nc") { if(0.869753979739508 >= threshold) { return false; } }//|   after1 = Nc: false (601.0/90.0)
        else if(after1 == "Nca") { if(0 >= threshold) { return false; } }//|   after1 = Nca: false (0.0)
        else if(after1 == "Ncb") { if(0 >= threshold) { return false; } }//|   after1 = Ncb: false (0.0)
        else if(after1 == "Ncc") { if(0 >= threshold) { return false; } }//|   after1 = Ncc: false (0.0)
        else if(after1 == "Nce") { if(0 >= threshold) { return false; } }//|   after1 = Nce: false (0.0)
        else if(after1 == "Ncd") { if(0.725 >= threshold) { return true; } }//|   after1 = Ncd: true (29.0/11.0)
        else if(after1 == "Ncda") { if(0 >= threshold) { return false; } }//|   after1 = Ncda: false (0.0)
        else if(after1 == "Ncdb") { if(0 >= threshold) { return false; } }//|   after1 = Ncdb: false (0.0)
        else if(after1 == "Nd")//|   after1 = Nd
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (24.0)
          else if(moreNeu == false) { if(0.844236760124611 >= threshold) { return true; } }//|   |   moreNeu = false: true (271.0/50.0)
        }
        else if(after1 == "Ndaa") { if(0 >= threshold) { return false; } }//|   after1 = Ndaa: false (0.0)
        else if(after1 == "Ndab") { if(0 >= threshold) { return false; } }//|   after1 = Ndab: false (0.0)
        else if(after1 == "Ndc") { if(0 >= threshold) { return false; } }//|   after1 = Ndc: false (0.0)
        else if(after1 == "Ndd") { if(0 >= threshold) { return false; } }//|   after1 = Ndd: false (0.0)
        else if(after1 == "Nep") { if(0.714285714285714 >= threshold) { return false; } }//|   after1 = Nep: false (10.0/4.0)
        else if(after1 == "Neqa") { if(0.842105263157895 >= threshold) { return false; } }//|   after1 = Neqa: false (48.0/9.0)
        else if(after1 == "Neqb") { if(1 >= threshold) { return false; } }//|   after1 = Neqb: false (18.0)
        else if(after1 == "Nes") { if(0.789473684210526 >= threshold) { return false; } }//|   after1 = Nes: false (15.0/4.0)
        else if(after1 == "Neu")//|   after1 = Neu
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (12.0)
          else if(moreNeu == false) { if(0.736196319018405 >= threshold) { return true; } }//|   |   moreNeu = false: true (240.0/86.0)
        }
        else if(after1 == "Nf") { if(0.7 >= threshold) { return false; } }//|   after1 = Nf: false (14.0/6.0)
        else if(after1 == "Nfa") { if(0 >= threshold) { return false; } }//|   after1 = Nfa: false (0.0)
        else if(after1 == "Nfb") { if(0 >= threshold) { return false; } }//|   after1 = Nfb: false (0.0)
        else if(after1 == "Nfc") { if(0 >= threshold) { return false; } }//|   after1 = Nfc: false (0.0)
        else if(after1 == "Nfd") { if(0 >= threshold) { return false; } }//|   after1 = Nfd: false (0.0)
        else if(after1 == "Nfe") { if(0 >= threshold) { return false; } }//|   after1 = Nfe: false (0.0)
        else if(after1 == "Nfg") { if(0 >= threshold) { return false; } }//|   after1 = Nfg: false (0.0)
        else if(after1 == "Nfh") { if(0 >= threshold) { return false; } }//|   after1 = Nfh: false (0.0)
        else if(after1 == "Nfi") { if(0 >= threshold) { return false; } }//|   after1 = Nfi: false (0.0)
        else if(after1 == "Ng") { if(0.692913385826772 >= threshold) { return false; } }//|   after1 = Ng: false (352.0/156.0)
        else if(after1 == "Nh")//|   after1 = Nh
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (4.0)
          else if(moreNeu == false) { if(0.704545454545455 >= threshold) { return true; } }//|   |   moreNeu = false: true (31.0/13.0)
        }
        else if(after1 == "Nhaa") { if(0 >= threshold) { return false; } }//|   after1 = Nhaa: false (0.0)
        else if(after1 == "Nhab") { if(0 >= threshold) { return false; } }//|   after1 = Nhab: false (0.0)
        else if(after1 == "Nhac") { if(0 >= threshold) { return false; } }//|   after1 = Nhac: false (0.0)
        else if(after1 == "Nhb") { if(0 >= threshold) { return false; } }//|   after1 = Nhb: false (0.0)
        else if(after1 == "Nhc") { if(0 >= threshold) { return false; } }//|   after1 = Nhc: false (0.0)
        else if(after1 == "P")//|   after1 = P
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (5.0)
          else if(moreNeu == false) { if(0.7 >= threshold) { return true; } }//|   |   moreNeu = false: true (105.0/45.0)
        }
        else if(after1 == "SHI") { if(0.769230769230769 >= threshold) { return false; } }//|   after1 = SHI: false (80.0/24.0)
        else if(after1 == "V_11") { if(0 >= threshold) { return false; } }//|   after1 = V_11: false (0.0)
        else if(after1 == "T")//|   after1 = T
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (7.0)
          else if(moreNeu == false) { if(0.72 >= threshold) { return true; } }//|   |   moreNeu = false: true (54.0/21.0)
        }
        else if(after1 == "Ta") { if(0 >= threshold) { return false; } }//|   after1 = Ta: false (0.0)
        else if(after1 == "Tb") { if(0 >= threshold) { return false; } }//|   after1 = Tb: false (0.0)
        else if(after1 == "Tc") { if(0 >= threshold) { return false; } }//|   after1 = Tc: false (0.0)
        else if(after1 == "Td") { if(0 >= threshold) { return false; } }//|   after1 = Td: false (0.0)
        else if(after1 == "VA") { if(0.714953271028037 >= threshold) { return false; } }//|   after1 = VA: false (153.0/61.0)
        else if(after1 == "VA11") { if(0 >= threshold) { return false; } }//|   after1 = VA11: false (0.0)
        else if(after1 == "VA12") { if(0 >= threshold) { return false; } }//|   after1 = VA12: false (0.0)
        else if(after1 == "VA13") { if(0 >= threshold) { return false; } }//|   after1 = VA13: false (0.0)
        else if(after1 == "VA3") { if(0 >= threshold) { return false; } }//|   after1 = VA3: false (0.0)
        else if(after1 == "VA4") { if(0 >= threshold) { return false; } }//|   after1 = VA4: false (0.0)
        else if(after1 == "VAC") { if(1 >= threshold) { return false; } }//|   after1 = VAC: false (3.0)
        else if(after1 == "VA2") { if(0 >= threshold) { return false; } }//|   after1 = VA2: false (0.0)
        else if(after1 == "VB") { if(0.769230769230769 >= threshold) { return false; } }//|   after1 = VB: false (20.0/6.0)
        else if(after1 == "VB11") { if(0 >= threshold) { return false; } }//|   after1 = VB11: false (0.0)
        else if(after1 == "VB12") { if(0 >= threshold) { return false; } }//|   after1 = VB12: false (0.0)
        else if(after1 == "VB2") { if(0 >= threshold) { return false; } }//|   after1 = VB2: false (0.0)
        else if(after1 == "VC") { if(0.710900473933649 >= threshold) { return false; } }//|   after1 = VC: false (300.0/122.0)
        else if(after1 == "VC2") { if(0 >= threshold) { return false; } }//|   after1 = VC2: false (0.0)
        else if(after1 == "VC31") { if(0 >= threshold) { return false; } }//|   after1 = VC31: false (0.0)
        else if(after1 == "VC32") { if(0 >= threshold) { return false; } }//|   after1 = VC32: false (0.0)
        else if(after1 == "VC33") { if(0 >= threshold) { return false; } }//|   after1 = VC33: false (0.0)
        else if(after1 == "VCL") { if(0.69811320754717 >= threshold) { return false; } }//|   after1 = VCL: false (37.0/16.0)
        else if(after1 == "VC1") { if(0 >= threshold) { return false; } }//|   after1 = VC1: false (0.0)
        else if(after1 == "VD") { if(0.857142857142857 >= threshold) { return false; } }//|   after1 = VD: false (18.0/3.0)
        else if(after1 == "VD1") { if(0 >= threshold) { return false; } }//|   after1 = VD1: false (0.0)
        else if(after1 == "VD2") { if(0 >= threshold) { return false; } }//|   after1 = VD2: false (0.0)
        else if(after1 == "VE") { if(0.694736842105263 >= threshold) { return false; } }//|   after1 = VE: false (66.0/29.0)
        else if(after1 == "VE11") { if(0 >= threshold) { return false; } }//|   after1 = VE11: false (0.0)
        else if(after1 == "VE12") { if(0 >= threshold) { return false; } }//|   after1 = VE12: false (0.0)
        else if(after1 == "VE2") { if(0 >= threshold) { return false; } }//|   after1 = VE2: false (0.0)
        else if(after1 == "VF") { if(1 >= threshold) { return true; } }//|   after1 = VF: true (2.0)
        else if(after1 == "VF1") { if(0 >= threshold) { return false; } }//|   after1 = VF1: false (0.0)
        else if(after1 == "VF2") { if(0 >= threshold) { return false; } }//|   after1 = VF2: false (0.0)
        else if(after1 == "VG") { if(0.83695652173913 >= threshold) { return false; } }//|   after1 = VG: false (77.0/15.0)
        else if(after1 == "VG1") { if(0 >= threshold) { return false; } }//|   after1 = VG1: false (0.0)
        else if(after1 == "VG2") { if(0 >= threshold) { return false; } }//|   after1 = VG2: false (0.0)
        else if(after1 == "VH") { if(0.903010033444816 >= threshold) { return false; } }//|   after1 = VH: false (810.0/87.0)
        else if(after1 == "VH11") { if(0 >= threshold) { return false; } }//|   after1 = VH11: false (0.0)
        else if(after1 == "VH12") { if(0 >= threshold) { return false; } }//|   after1 = VH12: false (0.0)
        else if(after1 == "VH13") { if(0 >= threshold) { return false; } }//|   after1 = VH13: false (0.0)
        else if(after1 == "VH14") { if(0 >= threshold) { return false; } }//|   after1 = VH14: false (0.0)
        else if(after1 == "VH15") { if(0 >= threshold) { return false; } }//|   after1 = VH15: false (0.0)
        else if(after1 == "VH17") { if(0 >= threshold) { return false; } }//|   after1 = VH17: false (0.0)
        else if(after1 == "VH21") { if(0 >= threshold) { return false; } }//|   after1 = VH21: false (0.0)
        else if(after1 == "VHC") { if(0.864864864864865 >= threshold) { return false; } }//|   after1 = VHC: false (32.0/5.0)
        else if(after1 == "VH16") { if(0 >= threshold) { return false; } }//|   after1 = VH16: false (0.0)
        else if(after1 == "VH22") { if(0 >= threshold) { return false; } }//|   after1 = VH22: false (0.0)
        else if(after1 == "VI") { if(0.9 >= threshold) { return false; } }//|   after1 = VI: false (9.0/1.0)
        else if(after1 == "VI1") { if(0 >= threshold) { return false; } }//|   after1 = VI1: false (0.0)
        else if(after1 == "VI2") { if(0 >= threshold) { return false; } }//|   after1 = VI2: false (0.0)
        else if(after1 == "VI3") { if(0 >= threshold) { return false; } }//|   after1 = VI3: false (0.0)
        else if(after1 == "VJ") { if(0.745283018867924 >= threshold) { return false; } }//|   after1 = VJ: false (79.0/27.0)
        else if(after1 == "VJ1") { if(0 >= threshold) { return false; } }//|   after1 = VJ1: false (0.0)
        else if(after1 == "VJ2") { if(0 >= threshold) { return false; } }//|   after1 = VJ2: false (0.0)
        else if(after1 == "VJ3") { if(0 >= threshold) { return false; } }//|   after1 = VJ3: false (0.0)
        else if(after1 == "VK") { if(0.666666666666667 >= threshold) { return true; } }//|   after1 = VK: true (24.0/12.0)
        else if(after1 == "VK1") { if(0 >= threshold) { return false; } }//|   after1 = VK1: false (0.0)
        else if(after1 == "VK2") { if(0 >= threshold) { return false; } }//|   after1 = VK2: false (0.0)
        else if(after1 == "VL") { if(0.708333333333333 >= threshold) { return false; } }//|   after1 = VL: false (17.0/7.0)
        else if(after1 == "VL1") { if(0 >= threshold) { return false; } }//|   after1 = VL1: false (0.0)
        else if(after1 == "VL2") { if(0 >= threshold) { return false; } }//|   after1 = VL2: false (0.0)
        else if(after1 == "VL3") { if(0 >= threshold) { return false; } }//|   after1 = VL3: false (0.0)
        else if(after1 == "VL4") { if(0 >= threshold) { return false; } }//|   after1 = VL4: false (0.0)
        else if(after1 == "V_2") { if(0.72972972972973 >= threshold) { return true; } }//|   after1 = V_2: true (27.0/10.0)
        else if(after1 == "Nv") { if(0.777158774373259 >= threshold) { return false; } }//|   after1 = Nv: false (279.0/80.0)
        else if(after1 == "Nv1") { if(0 >= threshold) { return false; } }//|   after1 = Nv1: false (0.0)
        else if(after1 == "Nv2") { if(0 >= threshold) { return false; } }//|   after1 = Nv2: false (0.0)
        else if(after1 == "Nv3") { if(0 >= threshold) { return false; } }//|   after1 = Nv3: false (0.0)
        else if(after1 == "Nv4") { if(0 >= threshold) { return false; } }//|   after1 = Nv4: false (0.0)
        else if(after1 == "b") { if(0 >= threshold) { return false; } }//|   after1 = b: false (0.0)
        else if(after1 == "notword")//|   after1 = notword
        {
          if(moreNeu == true) { if(0.985714285714286 >= threshold) { return false; } }//|   |   moreNeu = true: false (345.0/5.0)
          else if(moreNeu == false) { if(0.74721889440356 >= threshold) { return true; } }//|   |   moreNeu = false: true (4366.0/1477.0)
        }
        else if(after1 == "begin") { if(0 >= threshold) { return false; } }//|   after1 = begin: false (0.0)
        else if(after1 == "end") { if(1 >= threshold) { return true; } }//|   after1 = end: true (1.0)
      }
      else if(front1 == "Dfa") { if(0.815789473684211 >= threshold) { return true; } }//front1 = Dfa: true (62.0/14.0)
      else if(front1 == "Dfb") { if(0.875 >= threshold) { return true; } }//front1 = Dfb: true (28.0/4.0)
      else if(front1 == "Di") { if(0.855699855699856 >= threshold) { return false; } }//front1 = Di: false (13046.0/2200.0)
      else if(front1 == "Dk")//front1 = Dk
      {
        if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   moreNeu = true: false (46.0)
        else if(moreNeu == false) { if(0.844155844155844 >= threshold) { return true; } }//|   moreNeu = false: true (325.0/60.0)
      }
      else if(front1 == "FW")//front1 = FW
      {
        if(after1 == "A") { if(1 >= threshold) { return false; } }//|   after1 = A: false (3.0)
        else if(after1 == "Caa") { if(0.666666666666667 >= threshold) { return true; } }//|   after1 = Caa: true (10.0/5.0)
        else if(after1 == "Cab") { if(0 >= threshold) { return true; } }//|   after1 = Cab: true (0.0)
        else if(after1 == "Cba") { if(0 >= threshold) { return true; } }//|   after1 = Cba: true (0.0)
        else if(after1 == "Cbab") { if(0 >= threshold) { return true; } }//|   after1 = Cbab: true (0.0)
        else if(after1 == "Cbb") { if(1 >= threshold) { return false; } }//|   after1 = Cbb: false (2.0)
        else if(after1 == "Cbaa") { if(0 >= threshold) { return true; } }//|   after1 = Cbaa: true (0.0)
        else if(after1 == "Cbba") { if(0 >= threshold) { return true; } }//|   after1 = Cbba: true (0.0)
        else if(after1 == "Cbbb") { if(0 >= threshold) { return true; } }//|   after1 = Cbbb: true (0.0)
        else if(after1 == "Cbca") { if(0 >= threshold) { return true; } }//|   after1 = Cbca: true (0.0)
        else if(after1 == "Cbcb") { if(0 >= threshold) { return true; } }//|   after1 = Cbcb: true (0.0)
        else if(after1 == "D") { if(0.903225806451613 >= threshold) { return true; } }//|   after1 = D: true (56.0/6.0)
        else if(after1 == "Dab") { if(0 >= threshold) { return true; } }//|   after1 = Dab: true (0.0)
        else if(after1 == "Dbaa") { if(0 >= threshold) { return true; } }//|   after1 = Dbaa: true (0.0)
        else if(after1 == "Dbab") { if(0 >= threshold) { return true; } }//|   after1 = Dbab: true (0.0)
        else if(after1 == "Dbb") { if(0 >= threshold) { return true; } }//|   after1 = Dbb: true (0.0)
        else if(after1 == "Dbc") { if(0 >= threshold) { return true; } }//|   after1 = Dbc: true (0.0)
        else if(after1 == "Dc") { if(0 >= threshold) { return true; } }//|   after1 = Dc: true (0.0)
        else if(after1 == "Dd") { if(0 >= threshold) { return true; } }//|   after1 = Dd: true (0.0)
        else if(after1 == "Dg") { if(0 >= threshold) { return true; } }//|   after1 = Dg: true (0.0)
        else if(after1 == "Dh") { if(0 >= threshold) { return true; } }//|   after1 = Dh: true (0.0)
        else if(after1 == "Dj") { if(0 >= threshold) { return true; } }//|   after1 = Dj: true (0.0)
        else if(after1 == "Da") { if(0.833333333333333 >= threshold) { return true; } }//|   after1 = Da: true (5.0/1.0)
        else if(after1 == "Daa") { if(0 >= threshold) { return true; } }//|   after1 = Daa: true (0.0)
        else if(after1 == "DE") { if(0.71875 >= threshold) { return true; } }//|   after1 = DE: true (46.0/18.0)
        else if(after1 == "Dfa") { if(0.8 >= threshold) { return true; } }//|   after1 = Dfa: true (4.0/1.0)
        else if(after1 == "Dfb") { if(0 >= threshold) { return true; } }//|   after1 = Dfb: true (0.0)
        else if(after1 == "Di") { if(0 >= threshold) { return true; } }//|   after1 = Di: true (0.0)
        else if(after1 == "Dk") { if(0 >= threshold) { return true; } }//|   after1 = Dk: true (0.0)
        else if(after1 == "FW") { if(0.838709677419355 >= threshold) { return true; } }//|   after1 = FW: true (26.0/5.0)
        else if(after1 == "I") { if(0 >= threshold) { return true; } }//|   after1 = I: true (0.0)
        else if(after1 == "Na") { if(0.826446280991736 >= threshold) { return false; } }//|   after1 = Na: false (100.0/21.0)
        else if(after1 == "Naa") { if(0 >= threshold) { return true; } }//|   after1 = Naa: true (0.0)
        else if(after1 == "Nab") { if(0 >= threshold) { return true; } }//|   after1 = Nab: true (0.0)
        else if(after1 == "Nac") { if(0 >= threshold) { return true; } }//|   after1 = Nac: true (0.0)
        else if(after1 == "Nad") { if(0 >= threshold) { return true; } }//|   after1 = Nad: true (0.0)
        else if(after1 == "Naea") { if(0 >= threshold) { return true; } }//|   after1 = Naea: true (0.0)
        else if(after1 == "Naeb") { if(0 >= threshold) { return true; } }//|   after1 = Naeb: true (0.0)
        else if(after1 == "Nb") { if(0.769230769230769 >= threshold) { return true; } }//|   after1 = Nb: true (10.0/3.0)
        else if(after1 == "Nba") { if(0 >= threshold) { return true; } }//|   after1 = Nba: true (0.0)
        else if(after1 == "Nbc") { if(0 >= threshold) { return true; } }//|   after1 = Nbc: true (0.0)
        else if(after1 == "Nc") { if(0.703703703703704 >= threshold) { return false; } }//|   after1 = Nc: false (19.0/8.0)
        else if(after1 == "Nca") { if(0 >= threshold) { return true; } }//|   after1 = Nca: true (0.0)
        else if(after1 == "Ncb") { if(0 >= threshold) { return true; } }//|   after1 = Ncb: true (0.0)
        else if(after1 == "Ncc") { if(0 >= threshold) { return true; } }//|   after1 = Ncc: true (0.0)
        else if(after1 == "Nce") { if(0 >= threshold) { return true; } }//|   after1 = Nce: true (0.0)
        else if(after1 == "Ncd") { if(0 >= threshold) { return true; } }//|   after1 = Ncd: true (0.0)
        else if(after1 == "Ncda") { if(0 >= threshold) { return true; } }//|   after1 = Ncda: true (0.0)
        else if(after1 == "Ncdb") { if(0 >= threshold) { return true; } }//|   after1 = Ncdb: true (0.0)
        else if(after1 == "Nd") { if(0.909090909090909 >= threshold) { return true; } }//|   after1 = Nd: true (20.0/2.0)
        else if(after1 == "Ndaa") { if(0 >= threshold) { return true; } }//|   after1 = Ndaa: true (0.0)
        else if(after1 == "Ndab") { if(0 >= threshold) { return true; } }//|   after1 = Ndab: true (0.0)
        else if(after1 == "Ndc") { if(0 >= threshold) { return true; } }//|   after1 = Ndc: true (0.0)
        else if(after1 == "Ndd") { if(0 >= threshold) { return true; } }//|   after1 = Ndd: true (0.0)
        else if(after1 == "Nep") { if(0.666666666666667 >= threshold) { return true; } }//|   after1 = Nep: true (2.0/1.0)
        else if(after1 == "Neqa") { if(0.75 >= threshold) { return true; } }//|   after1 = Neqa: true (3.0/1.0)
        else if(after1 == "Neqb") { if(1 >= threshold) { return false; } }//|   after1 = Neqb: false (2.0)
        else if(after1 == "Nes") { if(1 >= threshold) { return false; } }//|   after1 = Nes: false (1.0)
        else if(after1 == "Neu") { if(0.684210526315789 >= threshold) { return true; } }//|   after1 = Neu: true (13.0/6.0)
        else if(after1 == "Nf") { if(1 >= threshold) { return false; } }//|   after1 = Nf: false (2.0)
        else if(after1 == "Nfa") { if(0 >= threshold) { return true; } }//|   after1 = Nfa: true (0.0)
        else if(after1 == "Nfb") { if(0 >= threshold) { return true; } }//|   after1 = Nfb: true (0.0)
        else if(after1 == "Nfc") { if(0 >= threshold) { return true; } }//|   after1 = Nfc: true (0.0)
        else if(after1 == "Nfd") { if(0 >= threshold) { return true; } }//|   after1 = Nfd: true (0.0)
        else if(after1 == "Nfe") { if(0 >= threshold) { return true; } }//|   after1 = Nfe: true (0.0)
        else if(after1 == "Nfg") { if(0 >= threshold) { return true; } }//|   after1 = Nfg: true (0.0)
        else if(after1 == "Nfh") { if(0 >= threshold) { return true; } }//|   after1 = Nfh: true (0.0)
        else if(after1 == "Nfi") { if(0 >= threshold) { return true; } }//|   after1 = Nfi: true (0.0)
        else if(after1 == "Ng") { if(0.741935483870968 >= threshold) { return false; } }//|   after1 = Ng: false (23.0/8.0)
        else if(after1 == "Nh") { if(0.818181818181818 >= threshold) { return true; } }//|   after1 = Nh: true (9.0/2.0)
        else if(after1 == "Nhaa") { if(0 >= threshold) { return true; } }//|   after1 = Nhaa: true (0.0)
        else if(after1 == "Nhab") { if(0 >= threshold) { return true; } }//|   after1 = Nhab: true (0.0)
        else if(after1 == "Nhac") { if(0 >= threshold) { return true; } }//|   after1 = Nhac: true (0.0)
        else if(after1 == "Nhb") { if(0 >= threshold) { return true; } }//|   after1 = Nhb: true (0.0)
        else if(after1 == "Nhc") { if(0 >= threshold) { return true; } }//|   after1 = Nhc: true (0.0)
        else if(after1 == "P") { if(0.931034482758621 >= threshold) { return true; } }//|   after1 = P: true (27.0/2.0)
        else if(after1 == "SHI") { if(0 >= threshold) { return true; } }//|   after1 = SHI: true (0.0)
        else if(after1 == "V_11") { if(0 >= threshold) { return true; } }//|   after1 = V_11: true (0.0)
        else if(after1 == "T") { if(0 >= threshold) { return true; } }//|   after1 = T: true (0.0)
        else if(after1 == "Ta") { if(0 >= threshold) { return true; } }//|   after1 = Ta: true (0.0)
        else if(after1 == "Tb") { if(0 >= threshold) { return true; } }//|   after1 = Tb: true (0.0)
        else if(after1 == "Tc") { if(0 >= threshold) { return true; } }//|   after1 = Tc: true (0.0)
        else if(after1 == "Td") { if(0 >= threshold) { return true; } }//|   after1 = Td: true (0.0)
        else if(after1 == "VA") { if(0.75 >= threshold) { return false; } }//|   after1 = VA: false (6.0/2.0)
        else if(after1 == "VA11") { if(0 >= threshold) { return true; } }//|   after1 = VA11: true (0.0)
        else if(after1 == "VA12") { if(0 >= threshold) { return true; } }//|   after1 = VA12: true (0.0)
        else if(after1 == "VA13") { if(0 >= threshold) { return true; } }//|   after1 = VA13: true (0.0)
        else if(after1 == "VA3") { if(0 >= threshold) { return true; } }//|   after1 = VA3: true (0.0)
        else if(after1 == "VA4") { if(0 >= threshold) { return true; } }//|   after1 = VA4: true (0.0)
        else if(after1 == "VAC") { if(1 >= threshold) { return true; } }//|   after1 = VAC: true (1.0)
        else if(after1 == "VA2") { if(0 >= threshold) { return true; } }//|   after1 = VA2: true (0.0)
        else if(after1 == "VB") { if(1 >= threshold) { return false; } }//|   after1 = VB: false (1.0)
        else if(after1 == "VB11") { if(0 >= threshold) { return true; } }//|   after1 = VB11: true (0.0)
        else if(after1 == "VB12") { if(0 >= threshold) { return true; } }//|   after1 = VB12: true (0.0)
        else if(after1 == "VB2") { if(0 >= threshold) { return true; } }//|   after1 = VB2: true (0.0)
        else if(after1 == "VC")//|   after1 = VC
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (3.0)
          else if(moreNeu == false) { if(0.727272727272727 >= threshold) { return true; } }//|   |   moreNeu = false: true (32.0/12.0)
        }
        else if(after1 == "VC2") { if(0 >= threshold) { return true; } }//|   after1 = VC2: true (0.0)
        else if(after1 == "VC31") { if(0 >= threshold) { return true; } }//|   after1 = VC31: true (0.0)
        else if(after1 == "VC32") { if(0 >= threshold) { return true; } }//|   after1 = VC32: true (0.0)
        else if(after1 == "VC33") { if(0 >= threshold) { return true; } }//|   after1 = VC33: true (0.0)
        else if(after1 == "VCL") { if(0.75 >= threshold) { return true; } }//|   after1 = VCL: true (3.0/1.0)
        else if(after1 == "VC1") { if(0 >= threshold) { return true; } }//|   after1 = VC1: true (0.0)
        else if(after1 == "VD") { if(1 >= threshold) { return false; } }//|   after1 = VD: false (1.0)
        else if(after1 == "VD1") { if(0 >= threshold) { return true; } }//|   after1 = VD1: true (0.0)
        else if(after1 == "VD2") { if(0 >= threshold) { return true; } }//|   after1 = VD2: true (0.0)
        else if(after1 == "VE") { if(1 >= threshold) { return true; } }//|   after1 = VE: true (9.0)
        else if(after1 == "VE11") { if(0 >= threshold) { return true; } }//|   after1 = VE11: true (0.0)
        else if(after1 == "VE12") { if(0 >= threshold) { return true; } }//|   after1 = VE12: true (0.0)
        else if(after1 == "VE2") { if(0 >= threshold) { return true; } }//|   after1 = VE2: true (0.0)
        else if(after1 == "VF") { if(1 >= threshold) { return true; } }//|   after1 = VF: true (1.0)
        else if(after1 == "VF1") { if(0 >= threshold) { return true; } }//|   after1 = VF1: true (0.0)
        else if(after1 == "VF2") { if(0 >= threshold) { return true; } }//|   after1 = VF2: true (0.0)
        else if(after1 == "VG") { if(0.714285714285714 >= threshold) { return false; } }//|   after1 = VG: false (5.0/2.0)
        else if(after1 == "VG1") { if(0 >= threshold) { return true; } }//|   after1 = VG1: true (0.0)
        else if(after1 == "VG2") { if(0 >= threshold) { return true; } }//|   after1 = VG2: true (0.0)
        else if(after1 == "VH") { if(0.789473684210526 >= threshold) { return false; } }//|   after1 = VH: false (30.0/8.0)
        else if(after1 == "VH11") { if(0 >= threshold) { return true; } }//|   after1 = VH11: true (0.0)
        else if(after1 == "VH12") { if(0 >= threshold) { return true; } }//|   after1 = VH12: true (0.0)
        else if(after1 == "VH13") { if(0 >= threshold) { return true; } }//|   after1 = VH13: true (0.0)
        else if(after1 == "VH14") { if(0 >= threshold) { return true; } }//|   after1 = VH14: true (0.0)
        else if(after1 == "VH15") { if(0 >= threshold) { return true; } }//|   after1 = VH15: true (0.0)
        else if(after1 == "VH17") { if(0 >= threshold) { return true; } }//|   after1 = VH17: true (0.0)
        else if(after1 == "VH21") { if(0 >= threshold) { return true; } }//|   after1 = VH21: true (0.0)
        else if(after1 == "VHC") { if(0.666666666666667 >= threshold) { return true; } }//|   after1 = VHC: true (2.0/1.0)
        else if(after1 == "VH16") { if(0 >= threshold) { return true; } }//|   after1 = VH16: true (0.0)
        else if(after1 == "VH22") { if(0 >= threshold) { return true; } }//|   after1 = VH22: true (0.0)
        else if(after1 == "VI") { if(0 >= threshold) { return true; } }//|   after1 = VI: true (0.0)
        else if(after1 == "VI1") { if(0 >= threshold) { return true; } }//|   after1 = VI1: true (0.0)
        else if(after1 == "VI2") { if(0 >= threshold) { return true; } }//|   after1 = VI2: true (0.0)
        else if(after1 == "VI3") { if(0 >= threshold) { return true; } }//|   after1 = VI3: true (0.0)
        else if(after1 == "VJ") { if(0.8 >= threshold) { return true; } }//|   after1 = VJ: true (4.0/1.0)
        else if(after1 == "VJ1") { if(0 >= threshold) { return true; } }//|   after1 = VJ1: true (0.0)
        else if(after1 == "VJ2") { if(0 >= threshold) { return true; } }//|   after1 = VJ2: true (0.0)
        else if(after1 == "VJ3") { if(0 >= threshold) { return true; } }//|   after1 = VJ3: true (0.0)
        else if(after1 == "VK") { if(0 >= threshold) { return true; } }//|   after1 = VK: true (0.0)
        else if(after1 == "VK1") { if(0 >= threshold) { return true; } }//|   after1 = VK1: true (0.0)
        else if(after1 == "VK2") { if(0 >= threshold) { return true; } }//|   after1 = VK2: true (0.0)
        else if(after1 == "VL") { if(0 >= threshold) { return true; } }//|   after1 = VL: true (0.0)
        else if(after1 == "VL1") { if(0 >= threshold) { return true; } }//|   after1 = VL1: true (0.0)
        else if(after1 == "VL2") { if(0 >= threshold) { return true; } }//|   after1 = VL2: true (0.0)
        else if(after1 == "VL3") { if(0 >= threshold) { return true; } }//|   after1 = VL3: true (0.0)
        else if(after1 == "VL4") { if(0 >= threshold) { return true; } }//|   after1 = VL4: true (0.0)
        else if(after1 == "V_2") { if(1 >= threshold) { return true; } }//|   after1 = V_2: true (1.0)
        else if(after1 == "Nv") { if(0 >= threshold) { return true; } }//|   after1 = Nv: true (0.0)
        else if(after1 == "Nv1") { if(0 >= threshold) { return true; } }//|   after1 = Nv1: true (0.0)
        else if(after1 == "Nv2") { if(0 >= threshold) { return true; } }//|   after1 = Nv2: true (0.0)
        else if(after1 == "Nv3") { if(0 >= threshold) { return true; } }//|   after1 = Nv3: true (0.0)
        else if(after1 == "Nv4") { if(0 >= threshold) { return true; } }//|   after1 = Nv4: true (0.0)
        else if(after1 == "b") { if(0 >= threshold) { return true; } }//|   after1 = b: true (0.0)
        else if(after1 == "notword") { if(0.718592964824121 >= threshold) { return true; } }//|   after1 = notword: true (143.0/56.0)
        else if(after1 == "begin") { if(0 >= threshold) { return true; } }//|   after1 = begin: true (0.0)
        else if(after1 == "end") { if(0 >= threshold) { return true; } }//|   after1 = end: true (0.0)
      }
      else if(front1 == "I") { if(0.733333333333333 >= threshold) { return false; } }//front1 = I: false (33.0/12.0)
      else if(front1 == "Na")//front1 = Na
      {
        if(after1 == "A") { if(0.759493670886076 >= threshold) { return false; } }//|   after1 = A: false (60.0/19.0)
        else if(after1 == "Caa") { if(0.791095890410959 >= threshold) { return false; } }//|   after1 = Caa: false (231.0/61.0)
        else if(after1 == "Cab") { if(0.75 >= threshold) { return false; } }//|   after1 = Cab: false (18.0/6.0)
        else if(after1 == "Cba") { if(1 >= threshold) { return true; } }//|   after1 = Cba: true (1.0)
        else if(after1 == "Cbab") { if(0 >= threshold) { return false; } }//|   after1 = Cbab: false (0.0)
        else if(after1 == "Cbb")//|   after1 = Cbb
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (5.0)
          else if(moreNeu == false) { if(0.84 >= threshold) { return true; } }//|   |   moreNeu = false: true (84.0/16.0)
        }
        else if(after1 == "Cbaa") { if(0 >= threshold) { return false; } }//|   after1 = Cbaa: false (0.0)
        else if(after1 == "Cbba") { if(0 >= threshold) { return false; } }//|   after1 = Cbba: false (0.0)
        else if(after1 == "Cbbb") { if(0 >= threshold) { return false; } }//|   after1 = Cbbb: false (0.0)
        else if(after1 == "Cbca") { if(0 >= threshold) { return false; } }//|   after1 = Cbca: false (0.0)
        else if(after1 == "Cbcb") { if(0 >= threshold) { return false; } }//|   after1 = Cbcb: false (0.0)
        else if(after1 == "D")//|   after1 = D
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (74.0)
          else if(moreNeu == false) { if(0.862035697057405 >= threshold) { return true; } }//|   |   moreNeu = false: true (1787.0/286.0)
        }
        else if(after1 == "Dab") { if(0 >= threshold) { return false; } }//|   after1 = Dab: false (0.0)
        else if(after1 == "Dbaa") { if(0 >= threshold) { return false; } }//|   after1 = Dbaa: false (0.0)
        else if(after1 == "Dbab") { if(0 >= threshold) { return false; } }//|   after1 = Dbab: false (0.0)
        else if(after1 == "Dbb") { if(0 >= threshold) { return false; } }//|   after1 = Dbb: false (0.0)
        else if(after1 == "Dbc") { if(0 >= threshold) { return false; } }//|   after1 = Dbc: false (0.0)
        else if(after1 == "Dc") { if(0 >= threshold) { return false; } }//|   after1 = Dc: false (0.0)
        else if(after1 == "Dd") { if(0 >= threshold) { return false; } }//|   after1 = Dd: false (0.0)
        else if(after1 == "Dg") { if(0 >= threshold) { return false; } }//|   after1 = Dg: false (0.0)
        else if(after1 == "Dh") { if(0 >= threshold) { return false; } }//|   after1 = Dh: false (0.0)
        else if(after1 == "Dj") { if(0 >= threshold) { return false; } }//|   after1 = Dj: false (0.0)
        else if(after1 == "Da")//|   after1 = Da
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (4.0)
          else if(moreNeu == false) { if(0.72020725388601 >= threshold) { return true; } }//|   |   moreNeu = false: true (139.0/54.0)
        }
        else if(after1 == "Daa") { if(0 >= threshold) { return false; } }//|   after1 = Daa: false (0.0)
        else if(after1 == "DE") { if(0.701644923425978 >= threshold) { return false; } }//|   after1 = DE: false (1237.0/526.0)
        else if(after1 == "Dfa") { if(0.687116564417178 >= threshold) { return true; } }//|   after1 = Dfa: true (112.0/51.0)
        else if(after1 == "Dfb") { if(0 >= threshold) { return false; } }//|   after1 = Dfb: false (0.0)
        else if(after1 == "Di") { if(0 >= threshold) { return false; } }//|   after1 = Di: false (0.0)
        else if(after1 == "Dk") { if(1 >= threshold) { return true; } }//|   after1 = Dk: true (2.0)
        else if(after1 == "FW") { if(0.833333333333333 >= threshold) { return false; } }//|   after1 = FW: false (20.0/4.0)
        else if(after1 == "I") { if(0 >= threshold) { return false; } }//|   after1 = I: false (0.0)
        else if(after1 == "Na") { if(0.807476017201455 >= threshold) { return false; } }//|   after1 = Na: false (2441.0/582.0)
        else if(after1 == "Naa") { if(0 >= threshold) { return false; } }//|   after1 = Naa: false (0.0)
        else if(after1 == "Nab") { if(0 >= threshold) { return false; } }//|   after1 = Nab: false (0.0)
        else if(after1 == "Nac") { if(0 >= threshold) { return false; } }//|   after1 = Nac: false (0.0)
        else if(after1 == "Nad") { if(0 >= threshold) { return false; } }//|   after1 = Nad: false (0.0)
        else if(after1 == "Naea") { if(0 >= threshold) { return false; } }//|   after1 = Naea: false (0.0)
        else if(after1 == "Naeb") { if(0 >= threshold) { return false; } }//|   after1 = Naeb: false (0.0)
        else if(after1 == "Nb")//|   after1 = Nb
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (8.0)
          else if(moreNeu == false) { if(0.682539682539683 >= threshold) { return true; } }//|   |   moreNeu = false: true (43.0/20.0)
        }
        else if(after1 == "Nba") { if(0 >= threshold) { return false; } }//|   after1 = Nba: false (0.0)
        else if(after1 == "Nbc") { if(0 >= threshold) { return false; } }//|   after1 = Nbc: false (0.0)
        else if(after1 == "Nc") { if(0.741444866920152 >= threshold) { return false; } }//|   after1 = Nc: false (195.0/68.0)
        else if(after1 == "Nca") { if(0 >= threshold) { return false; } }//|   after1 = Nca: false (0.0)
        else if(after1 == "Ncb") { if(0 >= threshold) { return false; } }//|   after1 = Ncb: false (0.0)
        else if(after1 == "Ncc") { if(0 >= threshold) { return false; } }//|   after1 = Ncc: false (0.0)
        else if(after1 == "Nce") { if(0 >= threshold) { return false; } }//|   after1 = Nce: false (0.0)
        else if(after1 == "Ncd") { if(0.785714285714286 >= threshold) { return false; } }//|   after1 = Ncd: false (22.0/6.0)
        else if(after1 == "Ncda") { if(0 >= threshold) { return false; } }//|   after1 = Ncda: false (0.0)
        else if(after1 == "Ncdb") { if(0 >= threshold) { return false; } }//|   after1 = Ncdb: false (0.0)
        else if(after1 == "Nd")//|   after1 = Nd
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (9.0)
          else if(moreNeu == false) { if(0.981049562682216 >= threshold) { return true; } }//|   |   moreNeu = false: true (673.0/13.0)
        }
        else if(after1 == "Ndaa") { if(0 >= threshold) { return false; } }//|   after1 = Ndaa: false (0.0)
        else if(after1 == "Ndab") { if(0 >= threshold) { return false; } }//|   after1 = Ndab: false (0.0)
        else if(after1 == "Ndc") { if(0 >= threshold) { return false; } }//|   after1 = Ndc: false (0.0)
        else if(after1 == "Ndd") { if(0 >= threshold) { return false; } }//|   after1 = Ndd: false (0.0)
        else if(after1 == "Nep") { if(0.818181818181818 >= threshold) { return false; } }//|   after1 = Nep: false (9.0/2.0)
        else if(after1 == "Neqa") { if(0.739130434782609 >= threshold) { return true; } }//|   after1 = Neqa: true (51.0/18.0)
        else if(after1 == "Neqb") { if(0.968152866242038 >= threshold) { return false; } }//|   after1 = Neqb: false (152.0/5.0)
        else if(after1 == "Nes")//|   after1 = Nes
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (3.0)
          else if(moreNeu == false) { if(0.82 >= threshold) { return true; } }//|   |   moreNeu = false: true (41.0/9.0)
        }
        else if(after1 == "Neu") { if(0.738341968911917 >= threshold) { return false; } }//|   after1 = Neu: false (570.0/202.0)
        else if(after1 == "Nf") { if(0.966666666666667 >= threshold) { return false; } }//|   after1 = Nf: false (29.0/1.0)
        else if(after1 == "Nfa") { if(0 >= threshold) { return false; } }//|   after1 = Nfa: false (0.0)
        else if(after1 == "Nfb") { if(0 >= threshold) { return false; } }//|   after1 = Nfb: false (0.0)
        else if(after1 == "Nfc") { if(0 >= threshold) { return false; } }//|   after1 = Nfc: false (0.0)
        else if(after1 == "Nfd") { if(0 >= threshold) { return false; } }//|   after1 = Nfd: false (0.0)
        else if(after1 == "Nfe") { if(0 >= threshold) { return false; } }//|   after1 = Nfe: false (0.0)
        else if(after1 == "Nfg") { if(0 >= threshold) { return false; } }//|   after1 = Nfg: false (0.0)
        else if(after1 == "Nfh") { if(0 >= threshold) { return false; } }//|   after1 = Nfh: false (0.0)
        else if(after1 == "Nfi") { if(0 >= threshold) { return false; } }//|   after1 = Nfi: false (0.0)
        else if(after1 == "Ng") { if(0.707513416815742 >= threshold) { return false; } }//|   after1 = Ng: false (791.0/327.0)
        else if(after1 == "Nh") { if(0.777777777777778 >= threshold) { return true; } }//|   after1 = Nh: true (35.0/10.0)
        else if(after1 == "Nhaa") { if(0 >= threshold) { return false; } }//|   after1 = Nhaa: false (0.0)
        else if(after1 == "Nhab") { if(0 >= threshold) { return false; } }//|   after1 = Nhab: false (0.0)
        else if(after1 == "Nhac") { if(0 >= threshold) { return false; } }//|   after1 = Nhac: false (0.0)
        else if(after1 == "Nhb") { if(0 >= threshold) { return false; } }//|   after1 = Nhb: false (0.0)
        else if(after1 == "Nhc") { if(0 >= threshold) { return false; } }//|   after1 = Nhc: false (0.0)
        else if(after1 == "P")//|   after1 = P
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (35.0)
          else if(moreNeu == false) { if(0.882470119521912 >= threshold) { return true; } }//|   |   moreNeu = false: true (886.0/118.0)
        }
        else if(after1 == "SHI")//|   after1 = SHI
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (10.0)
          else if(moreNeu == false) { if(0.836065573770492 >= threshold) { return true; } }//|   |   moreNeu = false: true (102.0/20.0)
        }
        else if(after1 == "V_11") { if(0 >= threshold) { return false; } }//|   after1 = V_11: false (0.0)
        else if(after1 == "T") { if(0.843137254901961 >= threshold) { return false; } }//|   after1 = T: false (43.0/8.0)
        else if(after1 == "Ta") { if(0 >= threshold) { return false; } }//|   after1 = Ta: false (0.0)
        else if(after1 == "Tb") { if(0 >= threshold) { return false; } }//|   after1 = Tb: false (0.0)
        else if(after1 == "Tc") { if(0 >= threshold) { return false; } }//|   after1 = Tc: false (0.0)
        else if(after1 == "Td") { if(0 >= threshold) { return false; } }//|   after1 = Td: false (0.0)
        else if(after1 == "VA")//|   after1 = VA
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (26.0)
          else if(moreNeu == false) { if(0.828389830508475 >= threshold) { return true; } }//|   |   moreNeu = false: true (391.0/81.0)
        }
        else if(after1 == "VA11") { if(0 >= threshold) { return false; } }//|   after1 = VA11: false (0.0)
        else if(after1 == "VA12") { if(0 >= threshold) { return false; } }//|   after1 = VA12: false (0.0)
        else if(after1 == "VA13") { if(0 >= threshold) { return false; } }//|   after1 = VA13: false (0.0)
        else if(after1 == "VA3") { if(0 >= threshold) { return false; } }//|   after1 = VA3: false (0.0)
        else if(after1 == "VA4") { if(0 >= threshold) { return false; } }//|   after1 = VA4: false (0.0)
        else if(after1 == "VAC") { if(0.928571428571429 >= threshold) { return true; } }//|   after1 = VAC: true (13.0/1.0)
        else if(after1 == "VA2") { if(0 >= threshold) { return false; } }//|   after1 = VA2: false (0.0)
        else if(after1 == "VB")//|   after1 = VB
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (2.0)
          else if(moreNeu == false) { if(0.714285714285714 >= threshold) { return true; } }//|   |   moreNeu = false: true (30.0/12.0)
        }
        else if(after1 == "VB11") { if(0 >= threshold) { return false; } }//|   after1 = VB11: false (0.0)
        else if(after1 == "VB12") { if(0 >= threshold) { return false; } }//|   after1 = VB12: false (0.0)
        else if(after1 == "VB2") { if(0 >= threshold) { return false; } }//|   after1 = VB2: false (0.0)
        else if(after1 == "VC")//|   after1 = VC
        {
          if(moreNeu == true) { if(0.986301369863014 >= threshold) { return false; } }//|   |   moreNeu = true: false (72.0/1.0)
          else if(moreNeu == false) { if(0.803063457330416 >= threshold) { return true; } }//|   |   moreNeu = false: true (1101.0/270.0)
        }
        else if(after1 == "VC2") { if(0 >= threshold) { return false; } }//|   after1 = VC2: false (0.0)
        else if(after1 == "VC31") { if(0 >= threshold) { return false; } }//|   after1 = VC31: false (0.0)
        else if(after1 == "VC32") { if(0 >= threshold) { return false; } }//|   after1 = VC32: false (0.0)
        else if(after1 == "VC33") { if(0 >= threshold) { return false; } }//|   after1 = VC33: false (0.0)
        else if(after1 == "VCL")//|   after1 = VCL
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (16.0)
          else if(moreNeu == false) { if(0.847926267281106 >= threshold) { return true; } }//|   |   moreNeu = false: true (184.0/33.0)
        }
        else if(after1 == "VC1") { if(0 >= threshold) { return false; } }//|   after1 = VC1: false (0.0)
        else if(after1 == "VD")//|   after1 = VD
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (3.0)
          else if(moreNeu == false) { if(0.746031746031746 >= threshold) { return true; } }//|   |   moreNeu = false: true (47.0/16.0)
        }
        else if(after1 == "VD1") { if(0 >= threshold) { return false; } }//|   after1 = VD1: false (0.0)
        else if(after1 == "VD2") { if(0 >= threshold) { return false; } }//|   after1 = VD2: false (0.0)
        else if(after1 == "VE")//|   after1 = VE
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (48.0)
          else if(moreNeu == false) { if(0.924485125858124 >= threshold) { return true; } }//|   |   moreNeu = false: true (404.0/33.0)
        }
        else if(after1 == "VE11") { if(0 >= threshold) { return false; } }//|   after1 = VE11: false (0.0)
        else if(after1 == "VE12") { if(0 >= threshold) { return false; } }//|   after1 = VE12: false (0.0)
        else if(after1 == "VE2") { if(0 >= threshold) { return false; } }//|   after1 = VE2: false (0.0)
        else if(after1 == "VF")//|   after1 = VF
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (2.0)
          else if(moreNeu == false) { if(0.818181818181818 >= threshold) { return true; } }//|   |   moreNeu = false: true (54.0/12.0)
        }
        else if(after1 == "VF1") { if(0 >= threshold) { return false; } }//|   after1 = VF1: false (0.0)
        else if(after1 == "VF2") { if(0 >= threshold) { return false; } }//|   after1 = VF2: false (0.0)
        else if(after1 == "VG")//|   after1 = VG
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (6.0)
          else if(moreNeu == false) { if(0.684782608695652 >= threshold) { return true; } }//|   |   moreNeu = false: true (126.0/58.0)
        }
        else if(after1 == "VG1") { if(0 >= threshold) { return false; } }//|   after1 = VG1: false (0.0)
        else if(after1 == "VG2") { if(0 >= threshold) { return false; } }//|   after1 = VG2: false (0.0)
        else if(after1 == "VH") { if(0.674782608695652 >= threshold) { return false; } }//|   after1 = VH: false (776.0/374.0)
        else if(after1 == "VH11") { if(0 >= threshold) { return false; } }//|   after1 = VH11: false (0.0)
        else if(after1 == "VH12") { if(0 >= threshold) { return false; } }//|   after1 = VH12: false (0.0)
        else if(after1 == "VH13") { if(0 >= threshold) { return false; } }//|   after1 = VH13: false (0.0)
        else if(after1 == "VH14") { if(0 >= threshold) { return false; } }//|   after1 = VH14: false (0.0)
        else if(after1 == "VH15") { if(0 >= threshold) { return false; } }//|   after1 = VH15: false (0.0)
        else if(after1 == "VH17") { if(0 >= threshold) { return false; } }//|   after1 = VH17: false (0.0)
        else if(after1 == "VH21") { if(0 >= threshold) { return false; } }//|   after1 = VH21: false (0.0)
        else if(after1 == "VHC")//|   after1 = VHC
        {
          if(moreNeu == true) { if(0.833333333333333 >= threshold) { return false; } }//|   |   moreNeu = true: false (5.0/1.0)
          else if(moreNeu == false) { if(0.727272727272727 >= threshold) { return true; } }//|   |   moreNeu = false: true (48.0/18.0)
        }
        else if(after1 == "VH16") { if(0 >= threshold) { return false; } }//|   after1 = VH16: false (0.0)
        else if(after1 == "VH22") { if(0 >= threshold) { return false; } }//|   after1 = VH22: false (0.0)
        else if(after1 == "VI") { if(0.681818181818182 >= threshold) { return false; } }//|   after1 = VI: false (15.0/7.0)
        else if(after1 == "VI1") { if(0 >= threshold) { return false; } }//|   after1 = VI1: false (0.0)
        else if(after1 == "VI2") { if(0 >= threshold) { return false; } }//|   after1 = VI2: false (0.0)
        else if(after1 == "VI3") { if(0 >= threshold) { return false; } }//|   after1 = VI3: false (0.0)
        else if(after1 == "VJ")//|   after1 = VJ
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (14.0)
          else if(moreNeu == false) { if(0.744186046511628 >= threshold) { return true; } }//|   |   moreNeu = false: true (224.0/77.0)
        }
        else if(after1 == "VJ1") { if(0 >= threshold) { return false; } }//|   after1 = VJ1: false (0.0)
        else if(after1 == "VJ2") { if(0 >= threshold) { return false; } }//|   after1 = VJ2: false (0.0)
        else if(after1 == "VJ3") { if(0 >= threshold) { return false; } }//|   after1 = VJ3: false (0.0)
        else if(after1 == "VK") { if(0.770833333333333 >= threshold) { return true; } }//|   after1 = VK: true (74.0/22.0)
        else if(after1 == "VK1") { if(0 >= threshold) { return false; } }//|   after1 = VK1: false (0.0)
        else if(after1 == "VK2") { if(0 >= threshold) { return false; } }//|   after1 = VK2: false (0.0)
        else if(after1 == "VL") { if(0.863636363636364 >= threshold) { return true; } }//|   after1 = VL: true (57.0/9.0)
        else if(after1 == "VL1") { if(0 >= threshold) { return false; } }//|   after1 = VL1: false (0.0)
        else if(after1 == "VL2") { if(0 >= threshold) { return false; } }//|   after1 = VL2: false (0.0)
        else if(after1 == "VL3") { if(0 >= threshold) { return false; } }//|   after1 = VL3: false (0.0)
        else if(after1 == "VL4") { if(0 >= threshold) { return false; } }//|   after1 = VL4: false (0.0)
        else if(after1 == "V_2")//|   after1 = V_2
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (7.0)
          else if(moreNeu == false) { if(0.865671641791045 >= threshold) { return true; } }//|   |   moreNeu = false: true (58.0/9.0)
        }
        else if(after1 == "Nv") { if(0.718518518518519 >= threshold) { return false; } }//|   after1 = Nv: false (97.0/38.0)
        else if(after1 == "Nv1") { if(0 >= threshold) { return false; } }//|   after1 = Nv1: false (0.0)
        else if(after1 == "Nv2") { if(0 >= threshold) { return false; } }//|   after1 = Nv2: false (0.0)
        else if(after1 == "Nv3") { if(0 >= threshold) { return false; } }//|   after1 = Nv3: false (0.0)
        else if(after1 == "Nv4") { if(0 >= threshold) { return false; } }//|   after1 = Nv4: false (0.0)
        else if(after1 == "b") { if(0 >= threshold) { return false; } }//|   after1 = b: false (0.0)
        else if(after1 == "notword") { if(0.886719505122753 >= threshold) { return false; } }//|   after1 = notword: false (4587.0/586.0)
        else if(after1 == "begin") { if(0 >= threshold) { return false; } }//|   after1 = begin: false (0.0)
        else if(after1 == "end") { if(1 >= threshold) { return false; } }//|   after1 = end: false (1.0)
      }
      else if(front1 == "Naa") { if(0 >= threshold) { return true; } }//front1 = Naa: true (0.0)
      else if(front1 == "Nab") { if(0 >= threshold) { return true; } }//front1 = Nab: true (0.0)
      else if(front1 == "Nac") { if(1 >= threshold) { return false; } }//front1 = Nac: false (1.0)
      else if(front1 == "Nad") { if(0 >= threshold) { return true; } }//front1 = Nad: true (0.0)
      else if(front1 == "Naea") { if(0 >= threshold) { return true; } }//front1 = Naea: true (0.0)
      else if(front1 == "Naeb") { if(0 >= threshold) { return true; } }//front1 = Naeb: true (0.0)
      else if(front1 == "Nb")//front1 = Nb
      {
        if(after1 == "A") { if(0.782608695652174 >= threshold) { return false; } }//|   after1 = A: false (18.0/5.0)
        else if(after1 == "Caa")//|   after1 = Caa
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (2.0)
          else if(moreNeu == false) { if(0.722222222222222 >= threshold) { return true; } }//|   |   moreNeu = false: true (65.0/25.0)
        }
        else if(after1 == "Cab") { if(0 >= threshold) { return true; } }//|   after1 = Cab: true (0.0)
        else if(after1 == "Cba") { if(0 >= threshold) { return true; } }//|   after1 = Cba: true (0.0)
        else if(after1 == "Cbab") { if(0 >= threshold) { return true; } }//|   after1 = Cbab: true (0.0)
        else if(after1 == "Cbb") { if(0.964285714285714 >= threshold) { return true; } }//|   after1 = Cbb: true (54.0/2.0)
        else if(after1 == "Cbaa") { if(0 >= threshold) { return true; } }//|   after1 = Cbaa: true (0.0)
        else if(after1 == "Cbba") { if(0 >= threshold) { return true; } }//|   after1 = Cbba: true (0.0)
        else if(after1 == "Cbbb") { if(0 >= threshold) { return true; } }//|   after1 = Cbbb: true (0.0)
        else if(after1 == "Cbca") { if(0 >= threshold) { return true; } }//|   after1 = Cbca: true (0.0)
        else if(after1 == "Cbcb") { if(0 >= threshold) { return true; } }//|   after1 = Cbcb: true (0.0)
        else if(after1 == "D")//|   after1 = D
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (10.0)
          else if(moreNeu == false) { if(0.932741116751269 >= threshold) { return true; } }//|   |   moreNeu = false: true (735.0/53.0)
        }
        else if(after1 == "Dab") { if(0 >= threshold) { return true; } }//|   after1 = Dab: true (0.0)
        else if(after1 == "Dbaa") { if(0 >= threshold) { return true; } }//|   after1 = Dbaa: true (0.0)
        else if(after1 == "Dbab") { if(0 >= threshold) { return true; } }//|   after1 = Dbab: true (0.0)
        else if(after1 == "Dbb") { if(0 >= threshold) { return true; } }//|   after1 = Dbb: true (0.0)
        else if(after1 == "Dbc") { if(0 >= threshold) { return true; } }//|   after1 = Dbc: true (0.0)
        else if(after1 == "Dc") { if(0 >= threshold) { return true; } }//|   after1 = Dc: true (0.0)
        else if(after1 == "Dd") { if(0 >= threshold) { return true; } }//|   after1 = Dd: true (0.0)
        else if(after1 == "Dg") { if(0 >= threshold) { return true; } }//|   after1 = Dg: true (0.0)
        else if(after1 == "Dh") { if(0 >= threshold) { return true; } }//|   after1 = Dh: true (0.0)
        else if(after1 == "Dj") { if(0 >= threshold) { return true; } }//|   after1 = Dj: true (0.0)
        else if(after1 == "Da")//|   after1 = Da
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (2.0)
          else if(moreNeu == false) { if(0.944444444444444 >= threshold) { return true; } }//|   |   moreNeu = false: true (34.0/2.0)
        }
        else if(after1 == "Daa") { if(0 >= threshold) { return true; } }//|   after1 = Daa: true (0.0)
        else if(after1 == "DE")//|   after1 = DE
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (16.0)
          else if(moreNeu == false) { if(0.825545171339564 >= threshold) { return true; } }//|   |   moreNeu = false: true (265.0/56.0)
        }
        else if(after1 == "Dfa") { if(0.795918367346939 >= threshold) { return true; } }//|   after1 = Dfa: true (39.0/10.0)
        else if(after1 == "Dfb") { if(0 >= threshold) { return true; } }//|   after1 = Dfb: true (0.0)
        else if(after1 == "Di") { if(0 >= threshold) { return true; } }//|   after1 = Di: true (0.0)
        else if(after1 == "Dk") { if(0 >= threshold) { return true; } }//|   after1 = Dk: true (0.0)
        else if(after1 == "FW") { if(0.857142857142857 >= threshold) { return true; } }//|   after1 = FW: true (6.0/1.0)
        else if(after1 == "I") { if(0 >= threshold) { return true; } }//|   after1 = I: true (0.0)
        else if(after1 == "Na") { if(0.762948207171315 >= threshold) { return false; } }//|   after1 = Na: false (766.0/238.0)
        else if(after1 == "Naa") { if(0 >= threshold) { return true; } }//|   after1 = Naa: true (0.0)
        else if(after1 == "Nab") { if(0 >= threshold) { return true; } }//|   after1 = Nab: true (0.0)
        else if(after1 == "Nac") { if(0 >= threshold) { return true; } }//|   after1 = Nac: true (0.0)
        else if(after1 == "Nad") { if(0 >= threshold) { return true; } }//|   after1 = Nad: true (0.0)
        else if(after1 == "Naea") { if(0 >= threshold) { return true; } }//|   after1 = Naea: true (0.0)
        else if(after1 == "Naeb") { if(0 >= threshold) { return true; } }//|   after1 = Naeb: true (0.0)
        else if(after1 == "Nb") { if(0.727272727272727 >= threshold) { return false; } }//|   after1 = Nb: false (16.0/6.0)
        else if(after1 == "Nba") { if(0 >= threshold) { return true; } }//|   after1 = Nba: true (0.0)
        else if(after1 == "Nbc") { if(0 >= threshold) { return true; } }//|   after1 = Nbc: true (0.0)
        else if(after1 == "Nc") { if(0.742857142857143 >= threshold) { return false; } }//|   after1 = Nc: false (78.0/27.0)
        else if(after1 == "Nca") { if(0 >= threshold) { return true; } }//|   after1 = Nca: true (0.0)
        else if(after1 == "Ncb") { if(0 >= threshold) { return true; } }//|   after1 = Ncb: true (0.0)
        else if(after1 == "Ncc") { if(0 >= threshold) { return true; } }//|   after1 = Ncc: true (0.0)
        else if(after1 == "Nce") { if(0 >= threshold) { return true; } }//|   after1 = Nce: true (0.0)
        else if(after1 == "Ncd") { if(0.714285714285714 >= threshold) { return false; } }//|   after1 = Ncd: false (5.0/2.0)
        else if(after1 == "Ncda") { if(0 >= threshold) { return true; } }//|   after1 = Ncda: true (0.0)
        else if(after1 == "Ncdb") { if(0 >= threshold) { return true; } }//|   after1 = Ncdb: true (0.0)
        else if(after1 == "Nd")//|   after1 = Nd
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (3.0)
          else if(moreNeu == false) { if(0.988950276243094 >= threshold) { return true; } }//|   |   moreNeu = false: true (716.0/8.0)
        }
        else if(after1 == "Ndaa") { if(0 >= threshold) { return true; } }//|   after1 = Ndaa: true (0.0)
        else if(after1 == "Ndab") { if(0 >= threshold) { return true; } }//|   after1 = Ndab: true (0.0)
        else if(after1 == "Ndc") { if(0 >= threshold) { return true; } }//|   after1 = Ndc: true (0.0)
        else if(after1 == "Ndd") { if(0 >= threshold) { return true; } }//|   after1 = Ndd: true (0.0)
        else if(after1 == "Nep") { if(0.785714285714286 >= threshold) { return false; } }//|   after1 = Nep: false (11.0/3.0)
        else if(after1 == "Neqa")//|   after1 = Neqa
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (2.0)
          else if(moreNeu == false) { if(0.733333333333333 >= threshold) { return true; } }//|   |   moreNeu = false: true (11.0/4.0)
        }
        else if(after1 == "Neqb") { if(0.875 >= threshold) { return false; } }//|   after1 = Neqb: false (7.0/1.0)
        else if(after1 == "Nes") { if(0.85 >= threshold) { return true; } }//|   after1 = Nes: true (17.0/3.0)
        else if(after1 == "Neu")//|   after1 = Neu
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (4.0)
          else if(moreNeu == false) { if(0.758139534883721 >= threshold) { return true; } }//|   |   moreNeu = false: true (163.0/52.0)
        }
        else if(after1 == "Nf") { if(0.833333333333333 >= threshold) { return true; } }//|   after1 = Nf: true (5.0/1.0)
        else if(after1 == "Nfa") { if(0 >= threshold) { return true; } }//|   after1 = Nfa: true (0.0)
        else if(after1 == "Nfb") { if(0 >= threshold) { return true; } }//|   after1 = Nfb: true (0.0)
        else if(after1 == "Nfc") { if(0 >= threshold) { return true; } }//|   after1 = Nfc: true (0.0)
        else if(after1 == "Nfd") { if(0 >= threshold) { return true; } }//|   after1 = Nfd: true (0.0)
        else if(after1 == "Nfe") { if(0 >= threshold) { return true; } }//|   after1 = Nfe: true (0.0)
        else if(after1 == "Nfg") { if(0 >= threshold) { return true; } }//|   after1 = Nfg: true (0.0)
        else if(after1 == "Nfh") { if(0 >= threshold) { return true; } }//|   after1 = Nfh: true (0.0)
        else if(after1 == "Nfi") { if(0 >= threshold) { return true; } }//|   after1 = Nfi: true (0.0)
        else if(after1 == "Ng") { if(0.6875 >= threshold) { return false; } }//|   after1 = Ng: false (176.0/80.0)
        else if(after1 == "Nh") { if(0.8 >= threshold) { return true; } }//|   after1 = Nh: true (4.0/1.0)
        else if(after1 == "Nhaa") { if(0 >= threshold) { return true; } }//|   after1 = Nhaa: true (0.0)
        else if(after1 == "Nhab") { if(0 >= threshold) { return true; } }//|   after1 = Nhab: true (0.0)
        else if(after1 == "Nhac") { if(0 >= threshold) { return true; } }//|   after1 = Nhac: true (0.0)
        else if(after1 == "Nhb") { if(0 >= threshold) { return true; } }//|   after1 = Nhb: true (0.0)
        else if(after1 == "Nhc") { if(0 >= threshold) { return true; } }//|   after1 = Nhc: true (0.0)
        else if(after1 == "P")//|   after1 = P
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (12.0)
          else if(moreNeu == false) { if(0.958271236959762 >= threshold) { return true; } }//|   |   moreNeu = false: true (643.0/28.0)
        }
        else if(after1 == "SHI") { if(0.962962962962963 >= threshold) { return true; } }//|   after1 = SHI: true (52.0/2.0)
        else if(after1 == "V_11") { if(0 >= threshold) { return true; } }//|   after1 = V_11: true (0.0)
        else if(after1 == "T") { if(0.666666666666667 >= threshold) { return true; } }//|   after1 = T: true (6.0/3.0)
        else if(after1 == "Ta") { if(0 >= threshold) { return true; } }//|   after1 = Ta: true (0.0)
        else if(after1 == "Tb") { if(0 >= threshold) { return true; } }//|   after1 = Tb: true (0.0)
        else if(after1 == "Tc") { if(0 >= threshold) { return true; } }//|   after1 = Tc: true (0.0)
        else if(after1 == "Td") { if(0 >= threshold) { return true; } }//|   after1 = Td: true (0.0)
        else if(after1 == "VA")//|   after1 = VA
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (23.0)
          else if(moreNeu == false) { if(0.7847533632287 >= threshold) { return true; } }//|   |   moreNeu = false: true (175.0/48.0)
        }
        else if(after1 == "VA11") { if(0 >= threshold) { return true; } }//|   after1 = VA11: true (0.0)
        else if(after1 == "VA12") { if(0 >= threshold) { return true; } }//|   after1 = VA12: true (0.0)
        else if(after1 == "VA13") { if(0 >= threshold) { return true; } }//|   after1 = VA13: true (0.0)
        else if(after1 == "VA3") { if(0 >= threshold) { return true; } }//|   after1 = VA3: true (0.0)
        else if(after1 == "VA4") { if(0 >= threshold) { return true; } }//|   after1 = VA4: true (0.0)
        else if(after1 == "VAC") { if(1 >= threshold) { return true; } }//|   after1 = VAC: true (1.0)
        else if(after1 == "VA2") { if(0 >= threshold) { return true; } }//|   after1 = VA2: true (0.0)
        else if(after1 == "VB") { if(0.8 >= threshold) { return false; } }//|   after1 = VB: false (8.0/2.0)
        else if(after1 == "VB11") { if(0 >= threshold) { return true; } }//|   after1 = VB11: true (0.0)
        else if(after1 == "VB12") { if(0 >= threshold) { return true; } }//|   after1 = VB12: true (0.0)
        else if(after1 == "VB2") { if(0 >= threshold) { return true; } }//|   after1 = VB2: true (0.0)
        else if(after1 == "VC")//|   after1 = VC
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (21.0)
          else if(moreNeu == false) { if(0.914076782449726 >= threshold) { return true; } }//|   |   moreNeu = false: true (500.0/47.0)
        }
        else if(after1 == "VC2") { if(0 >= threshold) { return true; } }//|   after1 = VC2: true (0.0)
        else if(after1 == "VC31") { if(0 >= threshold) { return true; } }//|   after1 = VC31: true (0.0)
        else if(after1 == "VC32") { if(0 >= threshold) { return true; } }//|   after1 = VC32: true (0.0)
        else if(after1 == "VC33") { if(0 >= threshold) { return true; } }//|   after1 = VC33: true (0.0)
        else if(after1 == "VCL")//|   after1 = VCL
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (10.0)
          else if(moreNeu == false) { if(0.838095238095238 >= threshold) { return true; } }//|   |   moreNeu = false: true (88.0/17.0)
        }
        else if(after1 == "VC1") { if(0 >= threshold) { return true; } }//|   after1 = VC1: true (0.0)
        else if(after1 == "VD") { if(1 >= threshold) { return true; } }//|   after1 = VD: true (9.0)
        else if(after1 == "VD1") { if(0 >= threshold) { return true; } }//|   after1 = VD1: true (0.0)
        else if(after1 == "VD2") { if(0 >= threshold) { return true; } }//|   after1 = VD2: true (0.0)
        else if(after1 == "VE")//|   after1 = VE
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (5.0)
          else if(moreNeu == false) { if(0.980769230769231 >= threshold) { return true; } }//|   |   moreNeu = false: true (612.0/12.0)
        }
        else if(after1 == "VE11") { if(0 >= threshold) { return true; } }//|   after1 = VE11: true (0.0)
        else if(after1 == "VE12") { if(0 >= threshold) { return true; } }//|   after1 = VE12: true (0.0)
        else if(after1 == "VE2") { if(0 >= threshold) { return true; } }//|   after1 = VE2: true (0.0)
        else if(after1 == "VF") { if(0.868421052631579 >= threshold) { return true; } }//|   after1 = VF: true (33.0/5.0)
        else if(after1 == "VF1") { if(0 >= threshold) { return true; } }//|   after1 = VF1: true (0.0)
        else if(after1 == "VF2") { if(0 >= threshold) { return true; } }//|   after1 = VF2: true (0.0)
        else if(after1 == "VG")//|   after1 = VG
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (4.0)
          else if(moreNeu == false) { if(0.875 >= threshold) { return true; } }//|   |   moreNeu = false: true (35.0/5.0)
        }
        else if(after1 == "VG1") { if(0 >= threshold) { return true; } }//|   after1 = VG1: true (0.0)
        else if(after1 == "VG2") { if(0 >= threshold) { return true; } }//|   after1 = VG2: true (0.0)
        else if(after1 == "VH")//|   after1 = VH
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (8.0)
          else if(moreNeu == false) { if(0.768211920529801 >= threshold) { return true; } }//|   |   moreNeu = false: true (232.0/70.0)
        }
        else if(after1 == "VH11") { if(0 >= threshold) { return true; } }//|   after1 = VH11: true (0.0)
        else if(after1 == "VH12") { if(0 >= threshold) { return true; } }//|   after1 = VH12: true (0.0)
        else if(after1 == "VH13") { if(0 >= threshold) { return true; } }//|   after1 = VH13: true (0.0)
        else if(after1 == "VH14") { if(0 >= threshold) { return true; } }//|   after1 = VH14: true (0.0)
        else if(after1 == "VH15") { if(0 >= threshold) { return true; } }//|   after1 = VH15: true (0.0)
        else if(after1 == "VH17") { if(0 >= threshold) { return true; } }//|   after1 = VH17: true (0.0)
        else if(after1 == "VH21") { if(0 >= threshold) { return true; } }//|   after1 = VH21: true (0.0)
        else if(after1 == "VHC")//|   after1 = VHC
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (2.0)
          else if(moreNeu == false) { if(0.928571428571429 >= threshold) { return true; } }//|   |   moreNeu = false: true (13.0/1.0)
        }
        else if(after1 == "VH16") { if(0 >= threshold) { return true; } }//|   after1 = VH16: true (0.0)
        else if(after1 == "VH22") { if(0 >= threshold) { return true; } }//|   after1 = VH22: true (0.0)
        else if(after1 == "VI") { if(1 >= threshold) { return true; } }//|   after1 = VI: true (1.0)
        else if(after1 == "VI1") { if(0 >= threshold) { return true; } }//|   after1 = VI1: true (0.0)
        else if(after1 == "VI2") { if(0 >= threshold) { return true; } }//|   after1 = VI2: true (0.0)
        else if(after1 == "VI3") { if(0 >= threshold) { return true; } }//|   after1 = VI3: true (0.0)
        else if(after1 == "VJ")//|   after1 = VJ
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (5.0)
          else if(moreNeu == false) { if(0.859375 >= threshold) { return true; } }//|   |   moreNeu = false: true (55.0/9.0)
        }
        else if(after1 == "VJ1") { if(0 >= threshold) { return true; } }//|   after1 = VJ1: true (0.0)
        else if(after1 == "VJ2") { if(0 >= threshold) { return true; } }//|   after1 = VJ2: true (0.0)
        else if(after1 == "VJ3") { if(0 >= threshold) { return true; } }//|   after1 = VJ3: true (0.0)
        else if(after1 == "VK") { if(0.885714285714286 >= threshold) { return true; } }//|   after1 = VK: true (31.0/4.0)
        else if(after1 == "VK1") { if(0 >= threshold) { return true; } }//|   after1 = VK1: true (0.0)
        else if(after1 == "VK2") { if(0 >= threshold) { return true; } }//|   after1 = VK2: true (0.0)
        else if(after1 == "VL") { if(0.904761904761905 >= threshold) { return true; } }//|   after1 = VL: true (19.0/2.0)
        else if(after1 == "VL1") { if(0 >= threshold) { return true; } }//|   after1 = VL1: true (0.0)
        else if(after1 == "VL2") { if(0 >= threshold) { return true; } }//|   after1 = VL2: true (0.0)
        else if(after1 == "VL3") { if(0 >= threshold) { return true; } }//|   after1 = VL3: true (0.0)
        else if(after1 == "VL4") { if(0 >= threshold) { return true; } }//|   after1 = VL4: true (0.0)
        else if(after1 == "V_2")//|   after1 = V_2
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (2.0)
          else if(moreNeu == false) { if(1 >= threshold) { return true; } }//|   |   moreNeu = false: true (16.0)
        }
        else if(after1 == "Nv") { if(0.8125 >= threshold) { return false; } }//|   after1 = Nv: false (13.0/3.0)
        else if(after1 == "Nv1") { if(0 >= threshold) { return true; } }//|   after1 = Nv1: true (0.0)
        else if(after1 == "Nv2") { if(0 >= threshold) { return true; } }//|   after1 = Nv2: true (0.0)
        else if(after1 == "Nv3") { if(0 >= threshold) { return true; } }//|   after1 = Nv3: true (0.0)
        else if(after1 == "Nv4") { if(0 >= threshold) { return true; } }//|   after1 = Nv4: true (0.0)
        else if(after1 == "b") { if(0 >= threshold) { return true; } }//|   after1 = b: true (0.0)
        else if(after1 == "notword") { if(0.718146718146718 >= threshold) { return false; } }//|   after1 = notword: false (558.0/219.0)
        else if(after1 == "begin") { if(0 >= threshold) { return true; } }//|   after1 = begin: true (0.0)
        else if(after1 == "end") { if(0 >= threshold) { return true; } }//|   after1 = end: true (0.0)
      }
      else if(front1 == "Nba") { if(0 >= threshold) { return true; } }//front1 = Nba: true (0.0)
      else if(front1 == "Nbc") { if(0 >= threshold) { return true; } }//front1 = Nbc: true (0.0)
      else if(front1 == "Nc")//front1 = Nc
      {
        if(after1 == "A") { if(0.798701298701299 >= threshold) { return false; } }//|   after1 = A: false (123.0/31.0)
        else if(after1 == "Caa")//|   after1 = Caa
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (2.0)
          else if(moreNeu == false) { if(0.739130434782609 >= threshold) { return true; } }//|   |   moreNeu = false: true (68.0/24.0)
        }
        else if(after1 == "Cab") { if(0.8 >= threshold) { return false; } }//|   after1 = Cab: false (4.0/1.0)
        else if(after1 == "Cba") { if(0 >= threshold) { return true; } }//|   after1 = Cba: true (0.0)
        else if(after1 == "Cbab") { if(0 >= threshold) { return true; } }//|   after1 = Cbab: true (0.0)
        else if(after1 == "Cbb") { if(0.896551724137931 >= threshold) { return true; } }//|   after1 = Cbb: true (52.0/6.0)
        else if(after1 == "Cbaa") { if(0 >= threshold) { return true; } }//|   after1 = Cbaa: true (0.0)
        else if(after1 == "Cbba") { if(0 >= threshold) { return true; } }//|   after1 = Cbba: true (0.0)
        else if(after1 == "Cbbb") { if(0 >= threshold) { return true; } }//|   after1 = Cbbb: true (0.0)
        else if(after1 == "Cbca") { if(0 >= threshold) { return true; } }//|   after1 = Cbca: true (0.0)
        else if(after1 == "Cbcb") { if(0 >= threshold) { return true; } }//|   after1 = Cbcb: true (0.0)
        else if(after1 == "D")//|   after1 = D
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (11.0)
          else if(moreNeu == false) { if(0.939071566731141 >= threshold) { return true; } }//|   |   moreNeu = false: true (971.0/63.0)
        }
        else if(after1 == "Dab") { if(0 >= threshold) { return true; } }//|   after1 = Dab: true (0.0)
        else if(after1 == "Dbaa") { if(0 >= threshold) { return true; } }//|   after1 = Dbaa: true (0.0)
        else if(after1 == "Dbab") { if(0 >= threshold) { return true; } }//|   after1 = Dbab: true (0.0)
        else if(after1 == "Dbb") { if(0 >= threshold) { return true; } }//|   after1 = Dbb: true (0.0)
        else if(after1 == "Dbc") { if(0 >= threshold) { return true; } }//|   after1 = Dbc: true (0.0)
        else if(after1 == "Dc") { if(0 >= threshold) { return true; } }//|   after1 = Dc: true (0.0)
        else if(after1 == "Dd") { if(0 >= threshold) { return true; } }//|   after1 = Dd: true (0.0)
        else if(after1 == "Dg") { if(0 >= threshold) { return true; } }//|   after1 = Dg: true (0.0)
        else if(after1 == "Dh") { if(0 >= threshold) { return true; } }//|   after1 = Dh: true (0.0)
        else if(after1 == "Dj") { if(0 >= threshold) { return true; } }//|   after1 = Dj: true (0.0)
        else if(after1 == "Da") { if(0.802083333333333 >= threshold) { return true; } }//|   after1 = Da: true (77.0/19.0)
        else if(after1 == "Daa") { if(0 >= threshold) { return true; } }//|   after1 = Daa: true (0.0)
        else if(after1 == "DE")//|   after1 = DE
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (42.0)
          else if(moreNeu == false) { if(0.814814814814815 >= threshold) { return true; } }//|   |   moreNeu = false: true (682.0/155.0)
        }
        else if(after1 == "Dfa") { if(0.735042735042735 >= threshold) { return true; } }//|   after1 = Dfa: true (86.0/31.0)
        else if(after1 == "Dfb") { if(0 >= threshold) { return true; } }//|   after1 = Dfb: true (0.0)
        else if(after1 == "Di") { if(0 >= threshold) { return true; } }//|   after1 = Di: true (0.0)
        else if(after1 == "Dk") { if(1 >= threshold) { return false; } }//|   after1 = Dk: false (1.0)
        else if(after1 == "FW") { if(0.761904761904762 >= threshold) { return false; } }//|   after1 = FW: false (16.0/5.0)
        else if(after1 == "I") { if(0 >= threshold) { return true; } }//|   after1 = I: true (0.0)
        else if(after1 == "Na") { if(0.734806629834254 >= threshold) { return false; } }//|   after1 = Na: false (1995.0/720.0)
        else if(after1 == "Naa") { if(0 >= threshold) { return true; } }//|   after1 = Naa: true (0.0)
        else if(after1 == "Nab") { if(0 >= threshold) { return true; } }//|   after1 = Nab: true (0.0)
        else if(after1 == "Nac") { if(0 >= threshold) { return true; } }//|   after1 = Nac: true (0.0)
        else if(after1 == "Nad") { if(0 >= threshold) { return true; } }//|   after1 = Nad: true (0.0)
        else if(after1 == "Naea") { if(0 >= threshold) { return true; } }//|   after1 = Naea: true (0.0)
        else if(after1 == "Naeb") { if(0 >= threshold) { return true; } }//|   after1 = Naeb: true (0.0)
        else if(after1 == "Nb") { if(0.772151898734177 >= threshold) { return false; } }//|   after1 = Nb: false (61.0/18.0)
        else if(after1 == "Nba") { if(0 >= threshold) { return true; } }//|   after1 = Nba: true (0.0)
        else if(after1 == "Nbc") { if(0 >= threshold) { return true; } }//|   after1 = Nbc: true (0.0)
        else if(after1 == "Nc") { if(0.835913312693498 >= threshold) { return false; } }//|   after1 = Nc: false (540.0/106.0)
        else if(after1 == "Nca") { if(0 >= threshold) { return true; } }//|   after1 = Nca: true (0.0)
        else if(after1 == "Ncb") { if(0 >= threshold) { return true; } }//|   after1 = Ncb: true (0.0)
        else if(after1 == "Ncc") { if(0 >= threshold) { return true; } }//|   after1 = Ncc: true (0.0)
        else if(after1 == "Nce") { if(0 >= threshold) { return true; } }//|   after1 = Nce: true (0.0)
        else if(after1 == "Ncd") { if(0.75 >= threshold) { return false; } }//|   after1 = Ncd: false (9.0/3.0)
        else if(after1 == "Ncda") { if(0 >= threshold) { return true; } }//|   after1 = Ncda: true (0.0)
        else if(after1 == "Ncdb") { if(0 >= threshold) { return true; } }//|   after1 = Ncdb: true (0.0)
        else if(after1 == "Nd")//|   after1 = Nd
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (8.0)
          else if(moreNeu == false) { if(0.962450592885375 >= threshold) { return true; } }//|   |   moreNeu = false: true (487.0/19.0)
        }
        else if(after1 == "Ndaa") { if(0 >= threshold) { return true; } }//|   after1 = Ndaa: true (0.0)
        else if(after1 == "Ndab") { if(0 >= threshold) { return true; } }//|   after1 = Ndab: true (0.0)
        else if(after1 == "Ndc") { if(0 >= threshold) { return true; } }//|   after1 = Ndc: true (0.0)
        else if(after1 == "Ndd") { if(0 >= threshold) { return true; } }//|   after1 = Ndd: true (0.0)
        else if(after1 == "Nep") { if(0.928571428571429 >= threshold) { return true; } }//|   after1 = Nep: true (13.0/1.0)
        else if(after1 == "Neqa") { if(0.869565217391304 >= threshold) { return true; } }//|   after1 = Neqa: true (40.0/6.0)
        else if(after1 == "Neqb") { if(0.942857142857143 >= threshold) { return false; } }//|   after1 = Neqb: false (33.0/2.0)
        else if(after1 == "Nes")//|   after1 = Nes
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (2.0)
          else if(moreNeu == false) { if(0.955555555555556 >= threshold) { return true; } }//|   |   moreNeu = false: true (43.0/2.0)
        }
        else if(after1 == "Neu")//|   after1 = Neu
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (14.0)
          else if(moreNeu == false) { if(0.811643835616438 >= threshold) { return true; } }//|   |   moreNeu = false: true (237.0/55.0)
        }
        else if(after1 == "Nf")//|   after1 = Nf
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (2.0)
          else if(moreNeu == false) { if(0.75 >= threshold) { return true; } }//|   |   moreNeu = false: true (6.0/2.0)
        }
        else if(after1 == "Nfa") { if(0 >= threshold) { return true; } }//|   after1 = Nfa: true (0.0)
        else if(after1 == "Nfb") { if(0 >= threshold) { return true; } }//|   after1 = Nfb: true (0.0)
        else if(after1 == "Nfc") { if(0 >= threshold) { return true; } }//|   after1 = Nfc: true (0.0)
        else if(after1 == "Nfd") { if(0 >= threshold) { return true; } }//|   after1 = Nfd: true (0.0)
        else if(after1 == "Nfe") { if(0 >= threshold) { return true; } }//|   after1 = Nfe: true (0.0)
        else if(after1 == "Nfg") { if(0 >= threshold) { return true; } }//|   after1 = Nfg: true (0.0)
        else if(after1 == "Nfh") { if(0 >= threshold) { return true; } }//|   after1 = Nfh: true (0.0)
        else if(after1 == "Nfi") { if(0 >= threshold) { return true; } }//|   after1 = Nfi: true (0.0)
        else if(after1 == "Ng")//|   after1 = Ng
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (16.0)
          else if(moreNeu == false) { if(0.693121693121693 >= threshold) { return true; } }//|   |   moreNeu = false: true (393.0/174.0)
        }
        else if(after1 == "Nh") { if(0.666666666666667 >= threshold) { return true; } }//|   after1 = Nh: true (12.0/6.0)
        else if(after1 == "Nhaa") { if(0 >= threshold) { return true; } }//|   after1 = Nhaa: true (0.0)
        else if(after1 == "Nhab") { if(0 >= threshold) { return true; } }//|   after1 = Nhab: true (0.0)
        else if(after1 == "Nhac") { if(0 >= threshold) { return true; } }//|   after1 = Nhac: true (0.0)
        else if(after1 == "Nhb") { if(0 >= threshold) { return true; } }//|   after1 = Nhb: true (0.0)
        else if(after1 == "Nhc") { if(0 >= threshold) { return true; } }//|   after1 = Nhc: true (0.0)
        else if(after1 == "P")//|   after1 = P
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (30.0)
          else if(moreNeu == false) { if(0.928735632183908 >= threshold) { return true; } }//|   |   moreNeu = false: true (404.0/31.0)
        }
        else if(after1 == "SHI") { if(0.952380952380952 >= threshold) { return true; } }//|   after1 = SHI: true (40.0/2.0)
        else if(after1 == "V_11") { if(0 >= threshold) { return true; } }//|   after1 = V_11: true (0.0)
        else if(after1 == "T") { if(0.7 >= threshold) { return false; } }//|   after1 = T: false (14.0/6.0)
        else if(after1 == "Ta") { if(0 >= threshold) { return true; } }//|   after1 = Ta: true (0.0)
        else if(after1 == "Tb") { if(0 >= threshold) { return true; } }//|   after1 = Tb: true (0.0)
        else if(after1 == "Tc") { if(0 >= threshold) { return true; } }//|   after1 = Tc: true (0.0)
        else if(after1 == "Td") { if(0 >= threshold) { return true; } }//|   after1 = Td: true (0.0)
        else if(after1 == "VA")//|   after1 = VA
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (17.0)
          else if(moreNeu == false) { if(0.826315789473684 >= threshold) { return true; } }//|   |   moreNeu = false: true (157.0/33.0)
        }
        else if(after1 == "VA11") { if(0 >= threshold) { return true; } }//|   after1 = VA11: true (0.0)
        else if(after1 == "VA12") { if(0 >= threshold) { return true; } }//|   after1 = VA12: true (0.0)
        else if(after1 == "VA13") { if(0 >= threshold) { return true; } }//|   after1 = VA13: true (0.0)
        else if(after1 == "VA3") { if(0 >= threshold) { return true; } }//|   after1 = VA3: true (0.0)
        else if(after1 == "VA4") { if(0 >= threshold) { return true; } }//|   after1 = VA4: true (0.0)
        else if(after1 == "VAC") { if(0.75 >= threshold) { return true; } }//|   after1 = VAC: true (3.0/1.0)
        else if(after1 == "VA2") { if(0 >= threshold) { return true; } }//|   after1 = VA2: true (0.0)
        else if(after1 == "VB") { if(0.764705882352941 >= threshold) { return true; } }//|   after1 = VB: true (13.0/4.0)
        else if(after1 == "VB11") { if(0 >= threshold) { return true; } }//|   after1 = VB11: true (0.0)
        else if(after1 == "VB12") { if(0 >= threshold) { return true; } }//|   after1 = VB12: true (0.0)
        else if(after1 == "VB2") { if(0 >= threshold) { return true; } }//|   after1 = VB2: true (0.0)
        else if(after1 == "VC")//|   after1 = VC
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (63.0)
          else if(moreNeu == false) { if(0.907945736434108 >= threshold) { return true; } }//|   |   moreNeu = false: true (937.0/95.0)
        }
        else if(after1 == "VC2") { if(0 >= threshold) { return true; } }//|   after1 = VC2: true (0.0)
        else if(after1 == "VC31") { if(0 >= threshold) { return true; } }//|   after1 = VC31: true (0.0)
        else if(after1 == "VC32") { if(0 >= threshold) { return true; } }//|   after1 = VC32: true (0.0)
        else if(after1 == "VC33") { if(0 >= threshold) { return true; } }//|   after1 = VC33: true (0.0)
        else if(after1 == "VCL") { if(0.679611650485437 >= threshold) { return false; } }//|   after1 = VCL: false (70.0/33.0)
        else if(after1 == "VC1") { if(0 >= threshold) { return true; } }//|   after1 = VC1: true (0.0)
        else if(after1 == "VD")//|   after1 = VD
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (4.0)
          else if(moreNeu == false) { if(0.708333333333333 >= threshold) { return true; } }//|   |   moreNeu = false: true (34.0/14.0)
        }
        else if(after1 == "VD1") { if(0 >= threshold) { return true; } }//|   after1 = VD1: true (0.0)
        else if(after1 == "VD2") { if(0 >= threshold) { return true; } }//|   after1 = VD2: true (0.0)
        else if(after1 == "VE")//|   after1 = VE
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (5.0)
          else if(moreNeu == false) { if(0.977941176470588 >= threshold) { return true; } }//|   |   moreNeu = false: true (532.0/12.0)
        }
        else if(after1 == "VE11") { if(0 >= threshold) { return true; } }//|   after1 = VE11: true (0.0)
        else if(after1 == "VE12") { if(0 >= threshold) { return true; } }//|   after1 = VE12: true (0.0)
        else if(after1 == "VE2") { if(0 >= threshold) { return true; } }//|   after1 = VE2: true (0.0)
        else if(after1 == "VF")//|   after1 = VF
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (3.0)
          else if(moreNeu == false) { if(0.963636363636364 >= threshold) { return true; } }//|   |   moreNeu = false: true (53.0/2.0)
        }
        else if(after1 == "VF1") { if(0 >= threshold) { return true; } }//|   after1 = VF1: true (0.0)
        else if(after1 == "VF2") { if(0 >= threshold) { return true; } }//|   after1 = VF2: true (0.0)
        else if(after1 == "VG") { if(0.676470588235294 >= threshold) { return true; } }//|   after1 = VG: true (46.0/22.0)
        else if(after1 == "VG1") { if(0 >= threshold) { return true; } }//|   after1 = VG1: true (0.0)
        else if(after1 == "VG2") { if(0 >= threshold) { return true; } }//|   after1 = VG2: true (0.0)
        else if(after1 == "VH") { if(0.693389592123769 >= threshold) { return false; } }//|   after1 = VH: false (493.0/218.0)
        else if(after1 == "VH11") { if(0 >= threshold) { return true; } }//|   after1 = VH11: true (0.0)
        else if(after1 == "VH12") { if(0 >= threshold) { return true; } }//|   after1 = VH12: true (0.0)
        else if(after1 == "VH13") { if(0 >= threshold) { return true; } }//|   after1 = VH13: true (0.0)
        else if(after1 == "VH14") { if(0 >= threshold) { return true; } }//|   after1 = VH14: true (0.0)
        else if(after1 == "VH15") { if(0 >= threshold) { return true; } }//|   after1 = VH15: true (0.0)
        else if(after1 == "VH17") { if(0 >= threshold) { return true; } }//|   after1 = VH17: true (0.0)
        else if(after1 == "VH21") { if(0 >= threshold) { return true; } }//|   after1 = VH21: true (0.0)
        else if(after1 == "VHC")//|   after1 = VHC
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (7.0)
          else if(moreNeu == false) { if(0.84375 >= threshold) { return true; } }//|   |   moreNeu = false: true (27.0/5.0)
        }
        else if(after1 == "VH16") { if(0 >= threshold) { return true; } }//|   after1 = VH16: true (0.0)
        else if(after1 == "VH22") { if(0 >= threshold) { return true; } }//|   after1 = VH22: true (0.0)
        else if(after1 == "VI") { if(1 >= threshold) { return false; } }//|   after1 = VI: false (1.0)
        else if(after1 == "VI1") { if(0 >= threshold) { return true; } }//|   after1 = VI1: true (0.0)
        else if(after1 == "VI2") { if(0 >= threshold) { return true; } }//|   after1 = VI2: true (0.0)
        else if(after1 == "VI3") { if(0 >= threshold) { return true; } }//|   after1 = VI3: true (0.0)
        else if(after1 == "VJ")//|   after1 = VJ
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (14.0)
          else if(moreNeu == false) { if(0.852564102564103 >= threshold) { return true; } }//|   |   moreNeu = false: true (133.0/23.0)
        }
        else if(after1 == "VJ1") { if(0 >= threshold) { return true; } }//|   after1 = VJ1: true (0.0)
        else if(after1 == "VJ2") { if(0 >= threshold) { return true; } }//|   after1 = VJ2: true (0.0)
        else if(after1 == "VJ3") { if(0 >= threshold) { return true; } }//|   after1 = VJ3: true (0.0)
        else if(after1 == "VK")//|   after1 = VK
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (2.0)
          else if(moreNeu == false) { if(0.9 >= threshold) { return true; } }//|   |   moreNeu = false: true (36.0/4.0)
        }
        else if(after1 == "VK1") { if(0 >= threshold) { return true; } }//|   after1 = VK1: true (0.0)
        else if(after1 == "VK2") { if(0 >= threshold) { return true; } }//|   after1 = VK2: true (0.0)
        else if(after1 == "VL")//|   after1 = VL
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (5.0)
          else if(moreNeu == false) { if(0.823529411764706 >= threshold) { return true; } }//|   |   moreNeu = false: true (28.0/6.0)
        }
        else if(after1 == "VL1") { if(0 >= threshold) { return true; } }//|   after1 = VL1: true (0.0)
        else if(after1 == "VL2") { if(0 >= threshold) { return true; } }//|   after1 = VL2: true (0.0)
        else if(after1 == "VL3") { if(0 >= threshold) { return true; } }//|   after1 = VL3: true (0.0)
        else if(after1 == "VL4") { if(0 >= threshold) { return true; } }//|   after1 = VL4: true (0.0)
        else if(after1 == "V_2")//|   after1 = V_2
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (2.0)
          else if(moreNeu == false) { if(0.882352941176471 >= threshold) { return true; } }//|   |   moreNeu = false: true (75.0/10.0)
        }
        else if(after1 == "Nv") { if(0.704918032786885 >= threshold) { return false; } }//|   after1 = Nv: false (86.0/36.0)
        else if(after1 == "Nv1") { if(0 >= threshold) { return true; } }//|   after1 = Nv1: true (0.0)
        else if(after1 == "Nv2") { if(0 >= threshold) { return true; } }//|   after1 = Nv2: true (0.0)
        else if(after1 == "Nv3") { if(0 >= threshold) { return true; } }//|   after1 = Nv3: true (0.0)
        else if(after1 == "Nv4") { if(0 >= threshold) { return true; } }//|   after1 = Nv4: true (0.0)
        else if(after1 == "b") { if(0 >= threshold) { return true; } }//|   after1 = b: true (0.0)
        else if(after1 == "notword") { if(0.774193548387097 >= threshold) { return false; } }//|   after1 = notword: false (816.0/238.0)
        else if(after1 == "begin") { if(0 >= threshold) { return true; } }//|   after1 = begin: true (0.0)
        else if(after1 == "end") { if(0.75 >= threshold) { return true; } }//|   after1 = end: true (3.0/1.0)
      }
      else if(front1 == "Nca") { if(0 >= threshold) { return true; } }//front1 = Nca: true (0.0)
      else if(front1 == "Ncb") { if(0 >= threshold) { return true; } }//front1 = Ncb: true (0.0)
      else if(front1 == "Ncc") { if(0 >= threshold) { return true; } }//front1 = Ncc: true (0.0)
      else if(front1 == "Nce") { if(0 >= threshold) { return true; } }//front1 = Nce: true (0.0)
      else if(front1 == "Ncd") { if(0.879690949227373 >= threshold) { return false; } }//front1 = Ncd: false (1594.0/218.0)
      else if(front1 == "Ncda") { if(0 >= threshold) { return true; } }//front1 = Ncda: true (0.0)
      else if(front1 == "Ncdb") { if(0 >= threshold) { return true; } }//front1 = Ncdb: true (0.0)
      else if(front1 == "Nd")//front1 = Nd
      {
        if(moreNeu == true) { if(0.8515625 >= threshold) { return false; } }//|   moreNeu = true: false (327.0/57.0)
        else if(moreNeu == false) { if(0.906261641098581 >= threshold) { return true; } }//|   moreNeu = false: true (26761.0/2768.0)
      }
      else if(front1 == "Ndaa") { if(0 >= threshold) { return true; } }//front1 = Ndaa: true (0.0)
      else if(front1 == "Ndab") { if(0 >= threshold) { return true; } }//front1 = Ndab: true (0.0)
      else if(front1 == "Ndc") { if(0 >= threshold) { return true; } }//front1 = Ndc: true (0.0)
      else if(front1 == "Ndd") { if(0 >= threshold) { return true; } }//front1 = Ndd: true (0.0)
      else if(front1 == "Nep") { if(0.938789868667917 >= threshold) { return false; } }//front1 = Nep: false (8006.0/522.0)
      else if(front1 == "Neqa")//front1 = Neqa
      {
        if(after1 == "A") { if(1 >= threshold) { return false; } }//|   after1 = A: false (12.0)
        else if(after1 == "Caa") { if(0.705882352941177 >= threshold) { return true; } }//|   after1 = Caa: true (12.0/5.0)
        else if(after1 == "Cab") { if(0 >= threshold) { return false; } }//|   after1 = Cab: false (0.0)
        else if(after1 == "Cba") { if(0 >= threshold) { return false; } }//|   after1 = Cba: false (0.0)
        else if(after1 == "Cbab") { if(0 >= threshold) { return false; } }//|   after1 = Cbab: false (0.0)
        else if(after1 == "Cbb") { if(1 >= threshold) { return true; } }//|   after1 = Cbb: true (3.0)
        else if(after1 == "Cbaa") { if(0 >= threshold) { return false; } }//|   after1 = Cbaa: false (0.0)
        else if(after1 == "Cbba") { if(0 >= threshold) { return false; } }//|   after1 = Cbba: false (0.0)
        else if(after1 == "Cbbb") { if(0 >= threshold) { return false; } }//|   after1 = Cbbb: false (0.0)
        else if(after1 == "Cbca") { if(0 >= threshold) { return false; } }//|   after1 = Cbca: false (0.0)
        else if(after1 == "Cbcb") { if(0 >= threshold) { return false; } }//|   after1 = Cbcb: false (0.0)
        else if(after1 == "D")//|   after1 = D
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (2.0)
          else if(moreNeu == false) { if(0.678571428571429 >= threshold) { return true; } }//|   |   moreNeu = false: true (133.0/63.0)
        }
        else if(after1 == "Dab") { if(0 >= threshold) { return false; } }//|   after1 = Dab: false (0.0)
        else if(after1 == "Dbaa") { if(0 >= threshold) { return false; } }//|   after1 = Dbaa: false (0.0)
        else if(after1 == "Dbab") { if(0 >= threshold) { return false; } }//|   after1 = Dbab: false (0.0)
        else if(after1 == "Dbb") { if(0 >= threshold) { return false; } }//|   after1 = Dbb: false (0.0)
        else if(after1 == "Dbc") { if(0 >= threshold) { return false; } }//|   after1 = Dbc: false (0.0)
        else if(after1 == "Dc") { if(0 >= threshold) { return false; } }//|   after1 = Dc: false (0.0)
        else if(after1 == "Dd") { if(0 >= threshold) { return false; } }//|   after1 = Dd: false (0.0)
        else if(after1 == "Dg") { if(0 >= threshold) { return false; } }//|   after1 = Dg: false (0.0)
        else if(after1 == "Dh") { if(0 >= threshold) { return false; } }//|   after1 = Dh: false (0.0)
        else if(after1 == "Dj") { if(0 >= threshold) { return false; } }//|   after1 = Dj: false (0.0)
        else if(after1 == "Da") { if(0.833333333333333 >= threshold) { return true; } }//|   after1 = Da: true (5.0/1.0)
        else if(after1 == "Daa") { if(0 >= threshold) { return false; } }//|   after1 = Daa: false (0.0)
        else if(after1 == "DE")//|   after1 = DE
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (2.0)
          else if(moreNeu == false) { if(0.779816513761468 >= threshold) { return true; } }//|   |   moreNeu = false: true (85.0/24.0)
        }
        else if(after1 == "Dfa") { if(0.666666666666667 >= threshold) { return true; } }//|   after1 = Dfa: true (2.0/1.0)
        else if(after1 == "Dfb") { if(0 >= threshold) { return false; } }//|   after1 = Dfb: false (0.0)
        else if(after1 == "Di") { if(0 >= threshold) { return false; } }//|   after1 = Di: false (0.0)
        else if(after1 == "Dk") { if(0 >= threshold) { return false; } }//|   after1 = Dk: false (0.0)
        else if(after1 == "FW") { if(0.666666666666667 >= threshold) { return true; } }//|   after1 = FW: true (2.0/1.0)
        else if(after1 == "I") { if(0 >= threshold) { return false; } }//|   after1 = I: false (0.0)
        else if(after1 == "Na") { if(0.856581532416503 >= threshold) { return false; } }//|   after1 = Na: false (436.0/73.0)
        else if(after1 == "Naa") { if(0 >= threshold) { return false; } }//|   after1 = Naa: false (0.0)
        else if(after1 == "Nab") { if(0 >= threshold) { return false; } }//|   after1 = Nab: false (0.0)
        else if(after1 == "Nac") { if(0 >= threshold) { return false; } }//|   after1 = Nac: false (0.0)
        else if(after1 == "Nad") { if(0 >= threshold) { return false; } }//|   after1 = Nad: false (0.0)
        else if(after1 == "Naea") { if(0 >= threshold) { return false; } }//|   after1 = Naea: false (0.0)
        else if(after1 == "Naeb") { if(0 >= threshold) { return false; } }//|   after1 = Naeb: false (0.0)
        else if(after1 == "Nb") { if(1 >= threshold) { return true; } }//|   after1 = Nb: true (5.0)
        else if(after1 == "Nba") { if(0 >= threshold) { return false; } }//|   after1 = Nba: false (0.0)
        else if(after1 == "Nbc") { if(0 >= threshold) { return false; } }//|   after1 = Nbc: false (0.0)
        else if(after1 == "Nc") { if(0.888888888888889 >= threshold) { return false; } }//|   after1 = Nc: false (56.0/7.0)
        else if(after1 == "Nca") { if(0 >= threshold) { return false; } }//|   after1 = Nca: false (0.0)
        else if(after1 == "Ncb") { if(0 >= threshold) { return false; } }//|   after1 = Ncb: false (0.0)
        else if(after1 == "Ncc") { if(0 >= threshold) { return false; } }//|   after1 = Ncc: false (0.0)
        else if(after1 == "Nce") { if(0 >= threshold) { return false; } }//|   after1 = Nce: false (0.0)
        else if(after1 == "Ncd") { if(0.714285714285714 >= threshold) { return true; } }//|   after1 = Ncd: true (5.0/2.0)
        else if(after1 == "Ncda") { if(0 >= threshold) { return false; } }//|   after1 = Ncda: false (0.0)
        else if(after1 == "Ncdb") { if(0 >= threshold) { return false; } }//|   after1 = Ncdb: false (0.0)
        else if(after1 == "Nd") { if(0.894736842105263 >= threshold) { return true; } }//|   after1 = Nd: true (17.0/2.0)
        else if(after1 == "Ndaa") { if(0 >= threshold) { return false; } }//|   after1 = Ndaa: false (0.0)
        else if(after1 == "Ndab") { if(0 >= threshold) { return false; } }//|   after1 = Ndab: false (0.0)
        else if(after1 == "Ndc") { if(0 >= threshold) { return false; } }//|   after1 = Ndc: false (0.0)
        else if(after1 == "Ndd") { if(0 >= threshold) { return false; } }//|   after1 = Ndd: false (0.0)
        else if(after1 == "Nep") { if(0 >= threshold) { return false; } }//|   after1 = Nep: false (0.0)
        else if(after1 == "Neqa") { if(1 >= threshold) { return true; } }//|   after1 = Neqa: true (3.0)
        else if(after1 == "Neqb") { if(1 >= threshold) { return false; } }//|   after1 = Neqb: false (2.0)
        else if(after1 == "Nes") { if(1 >= threshold) { return true; } }//|   after1 = Nes: true (1.0)
        else if(after1 == "Neu") { if(0.75 >= threshold) { return false; } }//|   after1 = Neu: false (9.0/3.0)
        else if(after1 == "Nf") { if(0 >= threshold) { return false; } }//|   after1 = Nf: false (0.0)
        else if(after1 == "Nfa") { if(0 >= threshold) { return false; } }//|   after1 = Nfa: false (0.0)
        else if(after1 == "Nfb") { if(0 >= threshold) { return false; } }//|   after1 = Nfb: false (0.0)
        else if(after1 == "Nfc") { if(0 >= threshold) { return false; } }//|   after1 = Nfc: false (0.0)
        else if(after1 == "Nfd") { if(0 >= threshold) { return false; } }//|   after1 = Nfd: false (0.0)
        else if(after1 == "Nfe") { if(0 >= threshold) { return false; } }//|   after1 = Nfe: false (0.0)
        else if(after1 == "Nfg") { if(0 >= threshold) { return false; } }//|   after1 = Nfg: false (0.0)
        else if(after1 == "Nfh") { if(0 >= threshold) { return false; } }//|   after1 = Nfh: false (0.0)
        else if(after1 == "Nfi") { if(0 >= threshold) { return false; } }//|   after1 = Nfi: false (0.0)
        else if(after1 == "Ng")//|   after1 = Ng
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (2.0)
          else if(moreNeu == false) { if(0.708333333333333 >= threshold) { return true; } }//|   |   moreNeu = false: true (17.0/7.0)
        }
        else if(after1 == "Nh") { if(1 >= threshold) { return true; } }//|   after1 = Nh: true (7.0)
        else if(after1 == "Nhaa") { if(0 >= threshold) { return false; } }//|   after1 = Nhaa: false (0.0)
        else if(after1 == "Nhab") { if(0 >= threshold) { return false; } }//|   after1 = Nhab: false (0.0)
        else if(after1 == "Nhac") { if(0 >= threshold) { return false; } }//|   after1 = Nhac: false (0.0)
        else if(after1 == "Nhb") { if(0 >= threshold) { return false; } }//|   after1 = Nhb: false (0.0)
        else if(after1 == "Nhc") { if(0 >= threshold) { return false; } }//|   after1 = Nhc: false (0.0)
        else if(after1 == "P") { if(0.743589743589744 >= threshold) { return true; } }//|   after1 = P: true (29.0/10.0)
        else if(after1 == "SHI") { if(0.7 >= threshold) { return false; } }//|   after1 = SHI: false (7.0/3.0)
        else if(after1 == "V_11") { if(0 >= threshold) { return false; } }//|   after1 = V_11: false (0.0)
        else if(after1 == "T") { if(1 >= threshold) { return false; } }//|   after1 = T: false (3.0)
        else if(after1 == "Ta") { if(0 >= threshold) { return false; } }//|   after1 = Ta: false (0.0)
        else if(after1 == "Tb") { if(0 >= threshold) { return false; } }//|   after1 = Tb: false (0.0)
        else if(after1 == "Tc") { if(0 >= threshold) { return false; } }//|   after1 = Tc: false (0.0)
        else if(after1 == "Td") { if(0 >= threshold) { return false; } }//|   after1 = Td: false (0.0)
        else if(after1 == "VA") { if(0.666666666666667 >= threshold) { return true; } }//|   after1 = VA: true (22.0/11.0)
        else if(after1 == "VA11") { if(0 >= threshold) { return false; } }//|   after1 = VA11: false (0.0)
        else if(after1 == "VA12") { if(0 >= threshold) { return false; } }//|   after1 = VA12: false (0.0)
        else if(after1 == "VA13") { if(0 >= threshold) { return false; } }//|   after1 = VA13: false (0.0)
        else if(after1 == "VA3") { if(0 >= threshold) { return false; } }//|   after1 = VA3: false (0.0)
        else if(after1 == "VA4") { if(0 >= threshold) { return false; } }//|   after1 = VA4: false (0.0)
        else if(after1 == "VAC") { if(1 >= threshold) { return false; } }//|   after1 = VAC: false (1.0)
        else if(after1 == "VA2") { if(0 >= threshold) { return false; } }//|   after1 = VA2: false (0.0)
        else if(after1 == "VB") { if(0.8 >= threshold) { return true; } }//|   after1 = VB: true (4.0/1.0)
        else if(after1 == "VB11") { if(0 >= threshold) { return false; } }//|   after1 = VB11: false (0.0)
        else if(after1 == "VB12") { if(0 >= threshold) { return false; } }//|   after1 = VB12: false (0.0)
        else if(after1 == "VB2") { if(0 >= threshold) { return false; } }//|   after1 = VB2: false (0.0)
        else if(after1 == "VC")//|   after1 = VC
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (2.0)
          else if(moreNeu == false) { if(0.725806451612903 >= threshold) { return true; } }//|   |   moreNeu = false: true (45.0/17.0)
        }
        else if(after1 == "VC2") { if(0 >= threshold) { return false; } }//|   after1 = VC2: false (0.0)
        else if(after1 == "VC31") { if(0 >= threshold) { return false; } }//|   after1 = VC31: false (0.0)
        else if(after1 == "VC32") { if(0 >= threshold) { return false; } }//|   after1 = VC32: false (0.0)
        else if(after1 == "VC33") { if(0 >= threshold) { return false; } }//|   after1 = VC33: false (0.0)
        else if(after1 == "VCL") { if(0.666666666666667 >= threshold) { return true; } }//|   after1 = VCL: true (6.0/3.0)
        else if(after1 == "VC1") { if(0 >= threshold) { return false; } }//|   after1 = VC1: false (0.0)
        else if(after1 == "VD") { if(0.75 >= threshold) { return true; } }//|   after1 = VD: true (3.0/1.0)
        else if(after1 == "VD1") { if(0 >= threshold) { return false; } }//|   after1 = VD1: false (0.0)
        else if(after1 == "VD2") { if(0 >= threshold) { return false; } }//|   after1 = VD2: false (0.0)
        else if(after1 == "VE") { if(0.692307692307692 >= threshold) { return true; } }//|   after1 = VE: true (9.0/4.0)
        else if(after1 == "VE11") { if(0 >= threshold) { return false; } }//|   after1 = VE11: false (0.0)
        else if(after1 == "VE12") { if(0 >= threshold) { return false; } }//|   after1 = VE12: false (0.0)
        else if(after1 == "VE2") { if(0 >= threshold) { return false; } }//|   after1 = VE2: false (0.0)
        else if(after1 == "VF") { if(0 >= threshold) { return false; } }//|   after1 = VF: false (0.0)
        else if(after1 == "VF1") { if(0 >= threshold) { return false; } }//|   after1 = VF1: false (0.0)
        else if(after1 == "VF2") { if(0 >= threshold) { return false; } }//|   after1 = VF2: false (0.0)
        else if(after1 == "VG") { if(1 >= threshold) { return true; } }//|   after1 = VG: true (4.0)
        else if(after1 == "VG1") { if(0 >= threshold) { return false; } }//|   after1 = VG1: false (0.0)
        else if(after1 == "VG2") { if(0 >= threshold) { return false; } }//|   after1 = VG2: false (0.0)
        else if(after1 == "VH") { if(0.724137931034483 >= threshold) { return false; } }//|   after1 = VH: false (63.0/24.0)
        else if(after1 == "VH11") { if(0 >= threshold) { return false; } }//|   after1 = VH11: false (0.0)
        else if(after1 == "VH12") { if(0 >= threshold) { return false; } }//|   after1 = VH12: false (0.0)
        else if(after1 == "VH13") { if(0 >= threshold) { return false; } }//|   after1 = VH13: false (0.0)
        else if(after1 == "VH14") { if(0 >= threshold) { return false; } }//|   after1 = VH14: false (0.0)
        else if(after1 == "VH15") { if(0 >= threshold) { return false; } }//|   after1 = VH15: false (0.0)
        else if(after1 == "VH17") { if(0 >= threshold) { return false; } }//|   after1 = VH17: false (0.0)
        else if(after1 == "VH21") { if(0 >= threshold) { return false; } }//|   after1 = VH21: false (0.0)
        else if(after1 == "VHC") { if(0.714285714285714 >= threshold) { return true; } }//|   after1 = VHC: true (5.0/2.0)
        else if(after1 == "VH16") { if(0 >= threshold) { return false; } }//|   after1 = VH16: false (0.0)
        else if(after1 == "VH22") { if(0 >= threshold) { return false; } }//|   after1 = VH22: false (0.0)
        else if(after1 == "VI") { if(0.666666666666667 >= threshold) { return true; } }//|   after1 = VI: true (2.0/1.0)
        else if(after1 == "VI1") { if(0 >= threshold) { return false; } }//|   after1 = VI1: false (0.0)
        else if(after1 == "VI2") { if(0 >= threshold) { return false; } }//|   after1 = VI2: false (0.0)
        else if(after1 == "VI3") { if(0 >= threshold) { return false; } }//|   after1 = VI3: false (0.0)
        else if(after1 == "VJ") { if(0.739130434782609 >= threshold) { return false; } }//|   after1 = VJ: false (17.0/6.0)
        else if(after1 == "VJ1") { if(0 >= threshold) { return false; } }//|   after1 = VJ1: false (0.0)
        else if(after1 == "VJ2") { if(0 >= threshold) { return false; } }//|   after1 = VJ2: false (0.0)
        else if(after1 == "VJ3") { if(0 >= threshold) { return false; } }//|   after1 = VJ3: false (0.0)
        else if(after1 == "VK") { if(0.75 >= threshold) { return true; } }//|   after1 = VK: true (3.0/1.0)
        else if(after1 == "VK1") { if(0 >= threshold) { return false; } }//|   after1 = VK1: false (0.0)
        else if(after1 == "VK2") { if(0 >= threshold) { return false; } }//|   after1 = VK2: false (0.0)
        else if(after1 == "VL") { if(0.666666666666667 >= threshold) { return true; } }//|   after1 = VL: true (4.0/2.0)
        else if(after1 == "VL1") { if(0 >= threshold) { return false; } }//|   after1 = VL1: false (0.0)
        else if(after1 == "VL2") { if(0 >= threshold) { return false; } }//|   after1 = VL2: false (0.0)
        else if(after1 == "VL3") { if(0 >= threshold) { return false; } }//|   after1 = VL3: false (0.0)
        else if(after1 == "VL4") { if(0 >= threshold) { return false; } }//|   after1 = VL4: false (0.0)
        else if(after1 == "V_2") { if(0.777777777777778 >= threshold) { return false; } }//|   after1 = V_2: false (7.0/2.0)
        else if(after1 == "Nv") { if(0.666666666666667 >= threshold) { return true; } }//|   after1 = Nv: true (8.0/4.0)
        else if(after1 == "Nv1") { if(0 >= threshold) { return false; } }//|   after1 = Nv1: false (0.0)
        else if(after1 == "Nv2") { if(0 >= threshold) { return false; } }//|   after1 = Nv2: false (0.0)
        else if(after1 == "Nv3") { if(0 >= threshold) { return false; } }//|   after1 = Nv3: false (0.0)
        else if(after1 == "Nv4") { if(0 >= threshold) { return false; } }//|   after1 = Nv4: false (0.0)
        else if(after1 == "b") { if(0 >= threshold) { return false; } }//|   after1 = b: false (0.0)
        else if(after1 == "notword") { if(0.705882352941177 >= threshold) { return true; } }//|   after1 = notword: true (84.0/35.0)
        else if(after1 == "begin") { if(0 >= threshold) { return false; } }//|   after1 = begin: false (0.0)
        else if(after1 == "end") { if(0 >= threshold) { return false; } }//|   after1 = end: false (0.0)
      }
      else if(front1 == "Neqb") { if(0.807692307692308 >= threshold) { return false; } }//front1 = Neqb: false (21.0/5.0)
      else if(front1 == "Nes") { if(0.881062767475036 >= threshold) { return false; } }//front1 = Nes: false (9882.0/1334.0)
      else if(front1 == "Neu") { if(0.797549967762734 >= threshold) { return true; } }//front1 = Neu: true (2474.0/628.0)
      else if(front1 == "Nf")//front1 = Nf
      {
        if(after1 == "A") { if(0.846153846153846 >= threshold) { return false; } }//|   after1 = A: false (11.0/2.0)
        else if(after1 == "Caa")//|   after1 = Caa
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (7.0)
          else if(moreNeu == false) { if(0.77027027027027 >= threshold) { return true; } }//|   |   moreNeu = false: true (57.0/17.0)
        }
        else if(after1 == "Cab") { if(0.666666666666667 >= threshold) { return true; } }//|   after1 = Cab: true (2.0/1.0)
        else if(after1 == "Cba") { if(1 >= threshold) { return false; } }//|   after1 = Cba: false (1.0)
        else if(after1 == "Cbab") { if(0 >= threshold) { return false; } }//|   after1 = Cbab: false (0.0)
        else if(after1 == "Cbb") { if(0.866666666666667 >= threshold) { return true; } }//|   after1 = Cbb: true (13.0/2.0)
        else if(after1 == "Cbaa") { if(0 >= threshold) { return false; } }//|   after1 = Cbaa: false (0.0)
        else if(after1 == "Cbba") { if(0 >= threshold) { return false; } }//|   after1 = Cbba: false (0.0)
        else if(after1 == "Cbbb") { if(0 >= threshold) { return false; } }//|   after1 = Cbbb: false (0.0)
        else if(after1 == "Cbca") { if(0 >= threshold) { return false; } }//|   after1 = Cbca: false (0.0)
        else if(after1 == "Cbcb") { if(0 >= threshold) { return false; } }//|   after1 = Cbcb: false (0.0)
        else if(after1 == "D")//|   after1 = D
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (22.0)
          else if(moreNeu == false) { if(0.779166666666667 >= threshold) { return true; } }//|   |   moreNeu = false: true (374.0/106.0)
        }
        else if(after1 == "Dab") { if(0 >= threshold) { return false; } }//|   after1 = Dab: false (0.0)
        else if(after1 == "Dbaa") { if(0 >= threshold) { return false; } }//|   after1 = Dbaa: false (0.0)
        else if(after1 == "Dbab") { if(0 >= threshold) { return false; } }//|   after1 = Dbab: false (0.0)
        else if(after1 == "Dbb") { if(0 >= threshold) { return false; } }//|   after1 = Dbb: false (0.0)
        else if(after1 == "Dbc") { if(0 >= threshold) { return false; } }//|   after1 = Dbc: false (0.0)
        else if(after1 == "Dc") { if(0 >= threshold) { return false; } }//|   after1 = Dc: false (0.0)
        else if(after1 == "Dd") { if(0 >= threshold) { return false; } }//|   after1 = Dd: false (0.0)
        else if(after1 == "Dg") { if(0 >= threshold) { return false; } }//|   after1 = Dg: false (0.0)
        else if(after1 == "Dh") { if(0 >= threshold) { return false; } }//|   after1 = Dh: false (0.0)
        else if(after1 == "Dj") { if(0 >= threshold) { return false; } }//|   after1 = Dj: false (0.0)
        else if(after1 == "Da") { if(0.7 >= threshold) { return true; } }//|   after1 = Da: true (28.0/12.0)
        else if(after1 == "Daa") { if(0 >= threshold) { return false; } }//|   after1 = Daa: false (0.0)
        else if(after1 == "DE") { if(0.824081313526192 >= threshold) { return false; } }//|   after1 = DE: false (1054.0/225.0)
        else if(after1 == "Dfa") { if(0.717391304347826 >= threshold) { return true; } }//|   after1 = Dfa: true (33.0/13.0)
        else if(after1 == "Dfb") { if(0 >= threshold) { return false; } }//|   after1 = Dfb: false (0.0)
        else if(after1 == "Di") { if(1 >= threshold) { return false; } }//|   after1 = Di: false (1.0)
        else if(after1 == "Dk") { if(0 >= threshold) { return false; } }//|   after1 = Dk: false (0.0)
        else if(after1 == "FW") { if(0.866666666666667 >= threshold) { return false; } }//|   after1 = FW: false (13.0/2.0)
        else if(after1 == "I") { if(0 >= threshold) { return false; } }//|   after1 = I: false (0.0)
        else if(after1 == "Na") { if(0.791477787851315 >= threshold) { return false; } }//|   after1 = Na: false (873.0/230.0)
        else if(after1 == "Naa") { if(0 >= threshold) { return false; } }//|   after1 = Naa: false (0.0)
        else if(after1 == "Nab") { if(0 >= threshold) { return false; } }//|   after1 = Nab: false (0.0)
        else if(after1 == "Nac") { if(0 >= threshold) { return false; } }//|   after1 = Nac: false (0.0)
        else if(after1 == "Nad") { if(0 >= threshold) { return false; } }//|   after1 = Nad: false (0.0)
        else if(after1 == "Naea") { if(0 >= threshold) { return false; } }//|   after1 = Naea: false (0.0)
        else if(after1 == "Naeb") { if(0 >= threshold) { return false; } }//|   after1 = Naeb: false (0.0)
        else if(after1 == "Nb")//|   after1 = Nb
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (2.0)
          else if(moreNeu == false) { if(0.716981132075472 >= threshold) { return true; } }//|   |   moreNeu = false: true (38.0/15.0)
        }
        else if(after1 == "Nba") { if(0 >= threshold) { return false; } }//|   after1 = Nba: false (0.0)
        else if(after1 == "Nbc") { if(0 >= threshold) { return false; } }//|   after1 = Nbc: false (0.0)
        else if(after1 == "Nc") { if(0.683823529411765 >= threshold) { return false; } }//|   after1 = Nc: false (93.0/43.0)
        else if(after1 == "Nca") { if(0 >= threshold) { return false; } }//|   after1 = Nca: false (0.0)
        else if(after1 == "Ncb") { if(0 >= threshold) { return false; } }//|   after1 = Ncb: false (0.0)
        else if(after1 == "Ncc") { if(0 >= threshold) { return false; } }//|   after1 = Ncc: false (0.0)
        else if(after1 == "Nce") { if(0 >= threshold) { return false; } }//|   after1 = Nce: false (0.0)
        else if(after1 == "Ncd") { if(0.785714285714286 >= threshold) { return true; } }//|   after1 = Ncd: true (11.0/3.0)
        else if(after1 == "Ncda") { if(0 >= threshold) { return false; } }//|   after1 = Ncda: false (0.0)
        else if(after1 == "Ncdb") { if(0 >= threshold) { return false; } }//|   after1 = Ncdb: false (0.0)
        else if(after1 == "Nd") { if(0.985454545454545 >= threshold) { return true; } }//|   after1 = Nd: true (271.0/4.0)
        else if(after1 == "Ndaa") { if(0 >= threshold) { return false; } }//|   after1 = Ndaa: false (0.0)
        else if(after1 == "Ndab") { if(0 >= threshold) { return false; } }//|   after1 = Ndab: false (0.0)
        else if(after1 == "Ndc") { if(0 >= threshold) { return false; } }//|   after1 = Ndc: false (0.0)
        else if(after1 == "Ndd") { if(0 >= threshold) { return false; } }//|   after1 = Ndd: false (0.0)
        else if(after1 == "Nep") { if(0.75 >= threshold) { return true; } }//|   after1 = Nep: true (9.0/3.0)
        else if(after1 == "Neqa") { if(0.740740740740741 >= threshold) { return false; } }//|   after1 = Neqa: false (20.0/7.0)
        else if(after1 == "Neqb") { if(1 >= threshold) { return false; } }//|   after1 = Neqb: false (43.0)
        else if(after1 == "Nes")//|   after1 = Nes
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (2.0)
          else if(moreNeu == false) { if(0.9 >= threshold) { return true; } }//|   |   moreNeu = false: true (9.0/1.0)
        }
        else if(after1 == "Neu") { if(0.795454545454545 >= threshold) { return false; } }//|   after1 = Neu: false (140.0/36.0)
        else if(after1 == "Nf") { if(1 >= threshold) { return false; } }//|   after1 = Nf: false (7.0)
        else if(after1 == "Nfa") { if(0 >= threshold) { return false; } }//|   after1 = Nfa: false (0.0)
        else if(after1 == "Nfb") { if(0 >= threshold) { return false; } }//|   after1 = Nfb: false (0.0)
        else if(after1 == "Nfc") { if(0 >= threshold) { return false; } }//|   after1 = Nfc: false (0.0)
        else if(after1 == "Nfd") { if(0 >= threshold) { return false; } }//|   after1 = Nfd: false (0.0)
        else if(after1 == "Nfe") { if(0 >= threshold) { return false; } }//|   after1 = Nfe: false (0.0)
        else if(after1 == "Nfg") { if(0 >= threshold) { return false; } }//|   after1 = Nfg: false (0.0)
        else if(after1 == "Nfh") { if(0 >= threshold) { return false; } }//|   after1 = Nfh: false (0.0)
        else if(after1 == "Nfi") { if(0 >= threshold) { return false; } }//|   after1 = Nfi: false (0.0)
        else if(after1 == "Ng")//|   after1 = Ng
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (12.0)
          else if(moreNeu == false) { if(0.687763713080169 >= threshold) { return true; } }//|   |   moreNeu = false: true (163.0/74.0)
        }
        else if(after1 == "Nh") { if(0.971428571428571 >= threshold) { return true; } }//|   after1 = Nh: true (68.0/2.0)
        else if(after1 == "Nhaa") { if(0 >= threshold) { return false; } }//|   after1 = Nhaa: false (0.0)
        else if(after1 == "Nhab") { if(0 >= threshold) { return false; } }//|   after1 = Nhab: false (0.0)
        else if(after1 == "Nhac") { if(0 >= threshold) { return false; } }//|   after1 = Nhac: false (0.0)
        else if(after1 == "Nhb") { if(0 >= threshold) { return false; } }//|   after1 = Nhb: false (0.0)
        else if(after1 == "Nhc") { if(0 >= threshold) { return false; } }//|   after1 = Nhc: false (0.0)
        else if(after1 == "P")//|   after1 = P
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (3.0)
          else if(moreNeu == false) { if(0.724137931034483 >= threshold) { return true; } }//|   |   moreNeu = false: true (126.0/48.0)
        }
        else if(after1 == "SHI")//|   after1 = SHI
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (3.0)
          else if(moreNeu == false) { if(0.791666666666667 >= threshold) { return true; } }//|   |   moreNeu = false: true (19.0/5.0)
        }
        else if(after1 == "V_11") { if(0 >= threshold) { return false; } }//|   after1 = V_11: false (0.0)
        else if(after1 == "T") { if(0.67741935483871 >= threshold) { return false; } }//|   after1 = T: false (42.0/20.0)
        else if(after1 == "Ta") { if(0 >= threshold) { return false; } }//|   after1 = Ta: false (0.0)
        else if(after1 == "Tb") { if(0 >= threshold) { return false; } }//|   after1 = Tb: false (0.0)
        else if(after1 == "Tc") { if(0 >= threshold) { return false; } }//|   after1 = Tc: false (0.0)
        else if(after1 == "Td") { if(0 >= threshold) { return false; } }//|   after1 = Td: false (0.0)
        else if(after1 == "VA") { if(0.736842105263158 >= threshold) { return true; } }//|   after1 = VA: true (126.0/45.0)
        else if(after1 == "VA11") { if(0 >= threshold) { return false; } }//|   after1 = VA11: false (0.0)
        else if(after1 == "VA12") { if(0 >= threshold) { return false; } }//|   after1 = VA12: false (0.0)
        else if(after1 == "VA13") { if(0 >= threshold) { return false; } }//|   after1 = VA13: false (0.0)
        else if(after1 == "VA3") { if(0 >= threshold) { return false; } }//|   after1 = VA3: false (0.0)
        else if(after1 == "VA4") { if(0 >= threshold) { return false; } }//|   after1 = VA4: false (0.0)
        else if(after1 == "VAC") { if(0.714285714285714 >= threshold) { return false; } }//|   after1 = VAC: false (5.0/2.0)
        else if(after1 == "VA2") { if(0 >= threshold) { return false; } }//|   after1 = VA2: false (0.0)
        else if(after1 == "VB") { if(0.8 >= threshold) { return false; } }//|   after1 = VB: false (4.0/1.0)
        else if(after1 == "VB11") { if(0 >= threshold) { return false; } }//|   after1 = VB11: false (0.0)
        else if(after1 == "VB12") { if(0 >= threshold) { return false; } }//|   after1 = VB12: false (0.0)
        else if(after1 == "VB2") { if(0 >= threshold) { return false; } }//|   after1 = VB2: false (0.0)
        else if(after1 == "VC")//|   after1 = VC
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (12.0)
          else if(moreNeu == false) { if(0.70392749244713 >= threshold) { return true; } }//|   |   moreNeu = false: true (233.0/98.0)
        }
        else if(after1 == "VC2") { if(0 >= threshold) { return false; } }//|   after1 = VC2: false (0.0)
        else if(after1 == "VC31") { if(0 >= threshold) { return false; } }//|   after1 = VC31: false (0.0)
        else if(after1 == "VC32") { if(0 >= threshold) { return false; } }//|   after1 = VC32: false (0.0)
        else if(after1 == "VC33") { if(0 >= threshold) { return false; } }//|   after1 = VC33: false (0.0)
        else if(after1 == "VCL")//|   after1 = VCL
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (3.0)
          else if(moreNeu == false) { if(0.703125 >= threshold) { return true; } }//|   |   moreNeu = false: true (45.0/19.0)
        }
        else if(after1 == "VC1") { if(0 >= threshold) { return false; } }//|   after1 = VC1: false (0.0)
        else if(after1 == "VD") { if(0.705882352941177 >= threshold) { return false; } }//|   after1 = VD: false (12.0/5.0)
        else if(after1 == "VD1") { if(0 >= threshold) { return false; } }//|   after1 = VD1: false (0.0)
        else if(after1 == "VD2") { if(0 >= threshold) { return false; } }//|   after1 = VD2: false (0.0)
        else if(after1 == "VE")//|   after1 = VE
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (9.0)
          else if(moreNeu == false) { if(0.793103448275862 >= threshold) { return true; } }//|   |   moreNeu = false: true (23.0/6.0)
        }
        else if(after1 == "VE11") { if(0 >= threshold) { return false; } }//|   after1 = VE11: false (0.0)
        else if(after1 == "VE12") { if(0 >= threshold) { return false; } }//|   after1 = VE12: false (0.0)
        else if(after1 == "VE2") { if(0 >= threshold) { return false; } }//|   after1 = VE2: false (0.0)
        else if(after1 == "VF") { if(0.714285714285714 >= threshold) { return true; } }//|   after1 = VF: true (5.0/2.0)
        else if(after1 == "VF1") { if(0 >= threshold) { return false; } }//|   after1 = VF1: false (0.0)
        else if(after1 == "VF2") { if(0 >= threshold) { return false; } }//|   after1 = VF2: false (0.0)
        else if(after1 == "VG") { if(0.717948717948718 >= threshold) { return true; } }//|   after1 = VG: true (28.0/11.0)
        else if(after1 == "VG1") { if(0 >= threshold) { return false; } }//|   after1 = VG1: false (0.0)
        else if(after1 == "VG2") { if(0 >= threshold) { return false; } }//|   after1 = VG2: false (0.0)
        else if(after1 == "VH") { if(0.765243902439024 >= threshold) { return false; } }//|   after1 = VH: false (251.0/77.0)
        else if(after1 == "VH11") { if(0 >= threshold) { return false; } }//|   after1 = VH11: false (0.0)
        else if(after1 == "VH12") { if(0 >= threshold) { return false; } }//|   after1 = VH12: false (0.0)
        else if(after1 == "VH13") { if(0 >= threshold) { return false; } }//|   after1 = VH13: false (0.0)
        else if(after1 == "VH14") { if(0 >= threshold) { return false; } }//|   after1 = VH14: false (0.0)
        else if(after1 == "VH15") { if(0 >= threshold) { return false; } }//|   after1 = VH15: false (0.0)
        else if(after1 == "VH17") { if(0 >= threshold) { return false; } }//|   after1 = VH17: false (0.0)
        else if(after1 == "VH21") { if(0 >= threshold) { return false; } }//|   after1 = VH21: false (0.0)
        else if(after1 == "VHC") { if(0.708333333333333 >= threshold) { return false; } }//|   after1 = VHC: false (17.0/7.0)
        else if(after1 == "VH16") { if(0 >= threshold) { return false; } }//|   after1 = VH16: false (0.0)
        else if(after1 == "VH22") { if(0 >= threshold) { return false; } }//|   after1 = VH22: false (0.0)
        else if(after1 == "VI") { if(1 >= threshold) { return false; } }//|   after1 = VI: false (1.0)
        else if(after1 == "VI1") { if(0 >= threshold) { return false; } }//|   after1 = VI1: false (0.0)
        else if(after1 == "VI2") { if(0 >= threshold) { return false; } }//|   after1 = VI2: false (0.0)
        else if(after1 == "VI3") { if(0 >= threshold) { return false; } }//|   after1 = VI3: false (0.0)
        else if(after1 == "VJ")//|   after1 = VJ
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (3.0)
          else if(moreNeu == false) { if(0.675675675675676 >= threshold) { return true; } }//|   |   moreNeu = false: true (50.0/24.0)
        }
        else if(after1 == "VJ1") { if(0 >= threshold) { return false; } }//|   after1 = VJ1: false (0.0)
        else if(after1 == "VJ2") { if(0 >= threshold) { return false; } }//|   after1 = VJ2: false (0.0)
        else if(after1 == "VJ3") { if(0 >= threshold) { return false; } }//|   after1 = VJ3: false (0.0)
        else if(after1 == "VK") { if(0.727272727272727 >= threshold) { return false; } }//|   after1 = VK: false (8.0/3.0)
        else if(after1 == "VK1") { if(0 >= threshold) { return false; } }//|   after1 = VK1: false (0.0)
        else if(after1 == "VK2") { if(0 >= threshold) { return false; } }//|   after1 = VK2: false (0.0)
        else if(after1 == "VL") { if(0.857142857142857 >= threshold) { return false; } }//|   after1 = VL: false (6.0/1.0)
        else if(after1 == "VL1") { if(0 >= threshold) { return false; } }//|   after1 = VL1: false (0.0)
        else if(after1 == "VL2") { if(0 >= threshold) { return false; } }//|   after1 = VL2: false (0.0)
        else if(after1 == "VL3") { if(0 >= threshold) { return false; } }//|   after1 = VL3: false (0.0)
        else if(after1 == "VL4") { if(0 >= threshold) { return false; } }//|   after1 = VL4: false (0.0)
        else if(after1 == "V_2") { if(0.866666666666667 >= threshold) { return true; } }//|   after1 = V_2: true (13.0/2.0)
        else if(after1 == "Nv") { if(0.711111111111111 >= threshold) { return false; } }//|   after1 = Nv: false (32.0/13.0)
        else if(after1 == "Nv1") { if(0 >= threshold) { return false; } }//|   after1 = Nv1: false (0.0)
        else if(after1 == "Nv2") { if(0 >= threshold) { return false; } }//|   after1 = Nv2: false (0.0)
        else if(after1 == "Nv3") { if(0 >= threshold) { return false; } }//|   after1 = Nv3: false (0.0)
        else if(after1 == "Nv4") { if(0 >= threshold) { return false; } }//|   after1 = Nv4: false (0.0)
        else if(after1 == "b") { if(0 >= threshold) { return false; } }//|   after1 = b: false (0.0)
        else if(after1 == "notword")//|   after1 = notword
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (53.0)
          else if(moreNeu == false) { if(0.673944687045124 >= threshold) { return true; } }//|   |   moreNeu = false: true (1389.0/672.0)
        }
        else if(after1 == "begin") { if(0 >= threshold) { return false; } }//|   after1 = begin: false (0.0)
        else if(after1 == "end") { if(1 >= threshold) { return false; } }//|   after1 = end: false (3.0)
      }
      else if(front1 == "Nfa") { if(0 >= threshold) { return true; } }//front1 = Nfa: true (0.0)
      else if(front1 == "Nfb") { if(0 >= threshold) { return true; } }//front1 = Nfb: true (0.0)
      else if(front1 == "Nfc") { if(0 >= threshold) { return true; } }//front1 = Nfc: true (0.0)
      else if(front1 == "Nfd") { if(0 >= threshold) { return true; } }//front1 = Nfd: true (0.0)
      else if(front1 == "Nfe") { if(0 >= threshold) { return true; } }//front1 = Nfe: true (0.0)
      else if(front1 == "Nfg") { if(0 >= threshold) { return true; } }//front1 = Nfg: true (0.0)
      else if(front1 == "Nfh") { if(0 >= threshold) { return true; } }//front1 = Nfh: true (0.0)
      else if(front1 == "Nfi") { if(0 >= threshold) { return true; } }//front1 = Nfi: true (0.0)
      else if(front1 == "Ng") { if(0.750297265160523 >= threshold) { return false; } }//front1 = Ng: false (1262.0/420.0)
      else if(front1 == "Nh")//front1 = Nh
      {
        if(after1 == "A") { if(0.6875 >= threshold) { return false; } }//|   after1 = A: false (22.0/10.0)
        else if(after1 == "Caa") { if(0.75 >= threshold) { return true; } }//|   after1 = Caa: true (12.0/4.0)
        else if(after1 == "Cab") { if(1 >= threshold) { return false; } }//|   after1 = Cab: false (1.0)
        else if(after1 == "Cba") { if(1 >= threshold) { return true; } }//|   after1 = Cba: true (2.0)
        else if(after1 == "Cbab") { if(0 >= threshold) { return true; } }//|   after1 = Cbab: true (0.0)
        else if(after1 == "Cbb") { if(0.902439024390244 >= threshold) { return true; } }//|   after1 = Cbb: true (37.0/4.0)
        else if(after1 == "Cbaa") { if(0 >= threshold) { return true; } }//|   after1 = Cbaa: true (0.0)
        else if(after1 == "Cbba") { if(0 >= threshold) { return true; } }//|   after1 = Cbba: true (0.0)
        else if(after1 == "Cbbb") { if(0 >= threshold) { return true; } }//|   after1 = Cbbb: true (0.0)
        else if(after1 == "Cbca") { if(0 >= threshold) { return true; } }//|   after1 = Cbca: true (0.0)
        else if(after1 == "Cbcb") { if(0 >= threshold) { return true; } }//|   after1 = Cbcb: true (0.0)
        else if(after1 == "D")//|   after1 = D
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (31.0)
          else if(moreNeu == false) { if(0.885462555066079 >= threshold) { return true; } }//|   |   moreNeu = false: true (1407.0/182.0)
        }
        else if(after1 == "Dab") { if(0 >= threshold) { return true; } }//|   after1 = Dab: true (0.0)
        else if(after1 == "Dbaa") { if(0 >= threshold) { return true; } }//|   after1 = Dbaa: true (0.0)
        else if(after1 == "Dbab") { if(0 >= threshold) { return true; } }//|   after1 = Dbab: true (0.0)
        else if(after1 == "Dbb") { if(0 >= threshold) { return true; } }//|   after1 = Dbb: true (0.0)
        else if(after1 == "Dbc") { if(0 >= threshold) { return true; } }//|   after1 = Dbc: true (0.0)
        else if(after1 == "Dc") { if(0 >= threshold) { return true; } }//|   after1 = Dc: true (0.0)
        else if(after1 == "Dd") { if(0 >= threshold) { return true; } }//|   after1 = Dd: true (0.0)
        else if(after1 == "Dg") { if(0 >= threshold) { return true; } }//|   after1 = Dg: true (0.0)
        else if(after1 == "Dh") { if(0 >= threshold) { return true; } }//|   after1 = Dh: true (0.0)
        else if(after1 == "Dj") { if(0 >= threshold) { return true; } }//|   after1 = Dj: true (0.0)
        else if(after1 == "Da") { if(0.853932584269663 >= threshold) { return true; } }//|   after1 = Da: true (76.0/13.0)
        else if(after1 == "Daa") { if(0 >= threshold) { return true; } }//|   after1 = Daa: true (0.0)
        else if(after1 == "DE")//|   after1 = DE
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (13.0)
          else if(moreNeu == false) { if(0.843220338983051 >= threshold) { return true; } }//|   |   moreNeu = false: true (597.0/111.0)
        }
        else if(after1 == "Dfa") { if(0.775147928994083 >= threshold) { return true; } }//|   after1 = Dfa: true (131.0/38.0)
        else if(after1 == "Dfb") { if(0 >= threshold) { return true; } }//|   after1 = Dfb: true (0.0)
        else if(after1 == "Di") { if(0 >= threshold) { return true; } }//|   after1 = Di: true (0.0)
        else if(after1 == "Dk") { if(1 >= threshold) { return false; } }//|   after1 = Dk: false (1.0)
        else if(after1 == "FW") { if(1 >= threshold) { return true; } }//|   after1 = FW: true (3.0)
        else if(after1 == "I") { if(0.75 >= threshold) { return false; } }//|   after1 = I: false (3.0/1.0)
        else if(after1 == "Na") { if(0.891081294396212 >= threshold) { return false; } }//|   after1 = Na: false (1129.0/138.0)
        else if(after1 == "Naa") { if(0 >= threshold) { return true; } }//|   after1 = Naa: true (0.0)
        else if(after1 == "Nab") { if(0 >= threshold) { return true; } }//|   after1 = Nab: true (0.0)
        else if(after1 == "Nac") { if(0 >= threshold) { return true; } }//|   after1 = Nac: true (0.0)
        else if(after1 == "Nad") { if(0 >= threshold) { return true; } }//|   after1 = Nad: true (0.0)
        else if(after1 == "Naea") { if(0 >= threshold) { return true; } }//|   after1 = Naea: true (0.0)
        else if(after1 == "Naeb") { if(0 >= threshold) { return true; } }//|   after1 = Naeb: true (0.0)
        else if(after1 == "Nb") { if(0.928571428571429 >= threshold) { return false; } }//|   after1 = Nb: false (13.0/1.0)
        else if(after1 == "Nba") { if(0 >= threshold) { return true; } }//|   after1 = Nba: true (0.0)
        else if(after1 == "Nbc") { if(0 >= threshold) { return true; } }//|   after1 = Nbc: true (0.0)
        else if(after1 == "Nc")//|   after1 = Nc
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (6.0)
          else if(moreNeu == false) { if(0.736842105263158 >= threshold) { return true; } }//|   |   moreNeu = false: true (42.0/15.0)
        }
        else if(after1 == "Nca") { if(0 >= threshold) { return true; } }//|   after1 = Nca: true (0.0)
        else if(after1 == "Ncb") { if(0 >= threshold) { return true; } }//|   after1 = Ncb: true (0.0)
        else if(after1 == "Ncc") { if(0 >= threshold) { return true; } }//|   after1 = Ncc: true (0.0)
        else if(after1 == "Nce") { if(0 >= threshold) { return true; } }//|   after1 = Nce: true (0.0)
        else if(after1 == "Ncd") { if(0.833333333333333 >= threshold) { return true; } }//|   after1 = Ncd: true (5.0/1.0)
        else if(after1 == "Ncda") { if(0 >= threshold) { return true; } }//|   after1 = Ncda: true (0.0)
        else if(after1 == "Ncdb") { if(0 >= threshold) { return true; } }//|   after1 = Ncdb: true (0.0)
        else if(after1 == "Nd")//|   after1 = Nd
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (3.0)
          else if(moreNeu == false) { if(0.91304347826087 >= threshold) { return true; } }//|   |   moreNeu = false: true (105.0/10.0)
        }
        else if(after1 == "Ndaa") { if(0 >= threshold) { return true; } }//|   after1 = Ndaa: true (0.0)
        else if(after1 == "Ndab") { if(0 >= threshold) { return true; } }//|   after1 = Ndab: true (0.0)
        else if(after1 == "Ndc") { if(0 >= threshold) { return true; } }//|   after1 = Ndc: true (0.0)
        else if(after1 == "Ndd") { if(0 >= threshold) { return true; } }//|   after1 = Ndd: true (0.0)
        else if(after1 == "Nep") { if(0.806451612903226 >= threshold) { return true; } }//|   after1 = Nep: true (50.0/12.0)
        else if(after1 == "Neqa") { if(0.862068965517241 >= threshold) { return true; } }//|   after1 = Neqa: true (25.0/4.0)
        else if(after1 == "Neqb") { if(0.72 >= threshold) { return false; } }//|   after1 = Neqb: false (18.0/7.0)
        else if(after1 == "Nes") { if(0.923076923076923 >= threshold) { return true; } }//|   after1 = Nes: true (12.0/1.0)
        else if(after1 == "Neu")//|   after1 = Neu
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (2.0)
          else if(moreNeu == false) { if(0.718181818181818 >= threshold) { return true; } }//|   |   moreNeu = false: true (158.0/62.0)
        }
        else if(after1 == "Nf") { if(0.875 >= threshold) { return true; } }//|   after1 = Nf: true (7.0/1.0)
        else if(after1 == "Nfa") { if(0 >= threshold) { return true; } }//|   after1 = Nfa: true (0.0)
        else if(after1 == "Nfb") { if(0 >= threshold) { return true; } }//|   after1 = Nfb: true (0.0)
        else if(after1 == "Nfc") { if(0 >= threshold) { return true; } }//|   after1 = Nfc: true (0.0)
        else if(after1 == "Nfd") { if(0 >= threshold) { return true; } }//|   after1 = Nfd: true (0.0)
        else if(after1 == "Nfe") { if(0 >= threshold) { return true; } }//|   after1 = Nfe: true (0.0)
        else if(after1 == "Nfg") { if(0 >= threshold) { return true; } }//|   after1 = Nfg: true (0.0)
        else if(after1 == "Nfh") { if(0 >= threshold) { return true; } }//|   after1 = Nfh: true (0.0)
        else if(after1 == "Nfi") { if(0 >= threshold) { return true; } }//|   after1 = Nfi: true (0.0)
        else if(after1 == "Ng") { if(0.830409356725146 >= threshold) { return false; } }//|   after1 = Ng: false (142.0/29.0)
        else if(after1 == "Nh")//|   after1 = Nh
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (5.0)
          else if(moreNeu == false) { if(0.72 >= threshold) { return true; } }//|   |   moreNeu = false: true (54.0/21.0)
        }
        else if(after1 == "Nhaa") { if(0 >= threshold) { return true; } }//|   after1 = Nhaa: true (0.0)
        else if(after1 == "Nhab") { if(0 >= threshold) { return true; } }//|   after1 = Nhab: true (0.0)
        else if(after1 == "Nhac") { if(0 >= threshold) { return true; } }//|   after1 = Nhac: true (0.0)
        else if(after1 == "Nhb") { if(0 >= threshold) { return true; } }//|   after1 = Nhb: true (0.0)
        else if(after1 == "Nhc") { if(0 >= threshold) { return true; } }//|   after1 = Nhc: true (0.0)
        else if(after1 == "P")//|   after1 = P
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (24.0)
          else if(moreNeu == false) { if(0.945121951219512 >= threshold) { return true; } }//|   |   moreNeu = false: true (310.0/18.0)
        }
        else if(after1 == "SHI") { if(0.907692307692308 >= threshold) { return true; } }//|   after1 = SHI: true (118.0/12.0)
        else if(after1 == "V_11") { if(0 >= threshold) { return true; } }//|   after1 = V_11: true (0.0)
        else if(after1 == "T") { if(0.763888888888889 >= threshold) { return false; } }//|   after1 = T: false (55.0/17.0)
        else if(after1 == "Ta") { if(0 >= threshold) { return true; } }//|   after1 = Ta: true (0.0)
        else if(after1 == "Tb") { if(0 >= threshold) { return true; } }//|   after1 = Tb: true (0.0)
        else if(after1 == "Tc") { if(0 >= threshold) { return true; } }//|   after1 = Tc: true (0.0)
        else if(after1 == "Td") { if(0 >= threshold) { return true; } }//|   after1 = Td: true (0.0)
        else if(after1 == "VA")//|   after1 = VA
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (28.0)
          else if(moreNeu == false) { if(0.815668202764977 >= threshold) { return true; } }//|   |   moreNeu = false: true (177.0/40.0)
        }
        else if(after1 == "VA11") { if(0 >= threshold) { return true; } }//|   after1 = VA11: true (0.0)
        else if(after1 == "VA12") { if(0 >= threshold) { return true; } }//|   after1 = VA12: true (0.0)
        else if(after1 == "VA13") { if(0 >= threshold) { return true; } }//|   after1 = VA13: true (0.0)
        else if(after1 == "VA3") { if(0 >= threshold) { return true; } }//|   after1 = VA3: true (0.0)
        else if(after1 == "VA4") { if(0 >= threshold) { return true; } }//|   after1 = VA4: true (0.0)
        else if(after1 == "VAC") { if(0.75 >= threshold) { return true; } }//|   after1 = VAC: true (3.0/1.0)
        else if(after1 == "VA2") { if(0 >= threshold) { return true; } }//|   after1 = VA2: true (0.0)
        else if(after1 == "VB") { if(0.833333333333333 >= threshold) { return true; } }//|   after1 = VB: true (10.0/2.0)
        else if(after1 == "VB11") { if(0 >= threshold) { return true; } }//|   after1 = VB11: true (0.0)
        else if(after1 == "VB12") { if(0 >= threshold) { return true; } }//|   after1 = VB12: true (0.0)
        else if(after1 == "VB2") { if(0 >= threshold) { return true; } }//|   after1 = VB2: true (0.0)
        else if(after1 == "VC")//|   after1 = VC
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (42.0)
          else if(moreNeu == false) { if(0.863157894736842 >= threshold) { return true; } }//|   |   moreNeu = false: true (410.0/65.0)
        }
        else if(after1 == "VC2") { if(0 >= threshold) { return true; } }//|   after1 = VC2: true (0.0)
        else if(after1 == "VC31") { if(0 >= threshold) { return true; } }//|   after1 = VC31: true (0.0)
        else if(after1 == "VC32") { if(0 >= threshold) { return true; } }//|   after1 = VC32: true (0.0)
        else if(after1 == "VC33") { if(0 >= threshold) { return true; } }//|   after1 = VC33: true (0.0)
        else if(after1 == "VCL")//|   after1 = VCL
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (18.0)
          else if(moreNeu == false) { if(0.882882882882883 >= threshold) { return true; } }//|   |   moreNeu = false: true (98.0/13.0)
        }
        else if(after1 == "VC1") { if(0 >= threshold) { return true; } }//|   after1 = VC1: true (0.0)
        else if(after1 == "VD") { if(0.772727272727273 >= threshold) { return true; } }//|   after1 = VD: true (17.0/5.0)
        else if(after1 == "VD1") { if(0 >= threshold) { return true; } }//|   after1 = VD1: true (0.0)
        else if(after1 == "VD2") { if(0 >= threshold) { return true; } }//|   after1 = VD2: true (0.0)
        else if(after1 == "VE")//|   after1 = VE
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (40.0)
          else if(moreNeu == false) { if(0.929889298892989 >= threshold) { return true; } }//|   |   moreNeu = false: true (252.0/19.0)
        }
        else if(after1 == "VE11") { if(0 >= threshold) { return true; } }//|   after1 = VE11: true (0.0)
        else if(after1 == "VE12") { if(0 >= threshold) { return true; } }//|   after1 = VE12: true (0.0)
        else if(after1 == "VE2") { if(0 >= threshold) { return true; } }//|   after1 = VE2: true (0.0)
        else if(after1 == "VF") { if(0.777777777777778 >= threshold) { return true; } }//|   after1 = VF: true (28.0/8.0)
        else if(after1 == "VF1") { if(0 >= threshold) { return true; } }//|   after1 = VF1: true (0.0)
        else if(after1 == "VF2") { if(0 >= threshold) { return true; } }//|   after1 = VF2: true (0.0)
        else if(after1 == "VG")//|   after1 = VG
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (2.0)
          else if(moreNeu == false) { if(0.857142857142857 >= threshold) { return true; } }//|   |   moreNeu = false: true (48.0/8.0)
        }
        else if(after1 == "VG1") { if(0 >= threshold) { return true; } }//|   after1 = VG1: true (0.0)
        else if(after1 == "VG2") { if(0 >= threshold) { return true; } }//|   after1 = VG2: true (0.0)
        else if(after1 == "VH") { if(0.694505494505494 >= threshold) { return false; } }//|   after1 = VH: false (316.0/139.0)
        else if(after1 == "VH11") { if(0 >= threshold) { return true; } }//|   after1 = VH11: true (0.0)
        else if(after1 == "VH12") { if(0 >= threshold) { return true; } }//|   after1 = VH12: true (0.0)
        else if(after1 == "VH13") { if(0 >= threshold) { return true; } }//|   after1 = VH13: true (0.0)
        else if(after1 == "VH14") { if(0 >= threshold) { return true; } }//|   after1 = VH14: true (0.0)
        else if(after1 == "VH15") { if(0 >= threshold) { return true; } }//|   after1 = VH15: true (0.0)
        else if(after1 == "VH17") { if(0 >= threshold) { return true; } }//|   after1 = VH17: true (0.0)
        else if(after1 == "VH21") { if(0 >= threshold) { return true; } }//|   after1 = VH21: true (0.0)
        else if(after1 == "VHC") { if(0.695652173913043 >= threshold) { return false; } }//|   after1 = VHC: false (16.0/7.0)
        else if(after1 == "VH16") { if(0 >= threshold) { return true; } }//|   after1 = VH16: true (0.0)
        else if(after1 == "VH22") { if(0 >= threshold) { return true; } }//|   after1 = VH22: true (0.0)
        else if(after1 == "VI") { if(0.8 >= threshold) { return true; } }//|   after1 = VI: true (4.0/1.0)
        else if(after1 == "VI1") { if(0 >= threshold) { return true; } }//|   after1 = VI1: true (0.0)
        else if(after1 == "VI2") { if(0 >= threshold) { return true; } }//|   after1 = VI2: true (0.0)
        else if(after1 == "VI3") { if(0 >= threshold) { return true; } }//|   after1 = VI3: true (0.0)
        else if(after1 == "VJ")//|   after1 = VJ
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (10.0)
          else if(moreNeu == false) { if(0.884210526315789 >= threshold) { return true; } }//|   |   moreNeu = false: true (84.0/11.0)
        }
        else if(after1 == "VJ1") { if(0 >= threshold) { return true; } }//|   after1 = VJ1: true (0.0)
        else if(after1 == "VJ2") { if(0 >= threshold) { return true; } }//|   after1 = VJ2: true (0.0)
        else if(after1 == "VJ3") { if(0 >= threshold) { return true; } }//|   after1 = VJ3: true (0.0)
        else if(after1 == "VK")//|   after1 = VK
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (6.0)
          else if(moreNeu == false) { if(0.917647058823529 >= threshold) { return true; } }//|   |   moreNeu = false: true (78.0/7.0)
        }
        else if(after1 == "VK1") { if(0 >= threshold) { return true; } }//|   after1 = VK1: true (0.0)
        else if(after1 == "VK2") { if(0 >= threshold) { return true; } }//|   after1 = VK2: true (0.0)
        else if(after1 == "VL") { if(0.8 >= threshold) { return true; } }//|   after1 = VL: true (28.0/7.0)
        else if(after1 == "VL1") { if(0 >= threshold) { return true; } }//|   after1 = VL1: true (0.0)
        else if(after1 == "VL2") { if(0 >= threshold) { return true; } }//|   after1 = VL2: true (0.0)
        else if(after1 == "VL3") { if(0 >= threshold) { return true; } }//|   after1 = VL3: true (0.0)
        else if(after1 == "VL4") { if(0 >= threshold) { return true; } }//|   after1 = VL4: true (0.0)
        else if(after1 == "V_2") { if(0.938461538461538 >= threshold) { return true; } }//|   after1 = V_2: true (61.0/4.0)
        else if(after1 == "Nv") { if(0.772727272727273 >= threshold) { return false; } }//|   after1 = Nv: false (17.0/5.0)
        else if(after1 == "Nv1") { if(0 >= threshold) { return true; } }//|   after1 = Nv1: true (0.0)
        else if(after1 == "Nv2") { if(0 >= threshold) { return true; } }//|   after1 = Nv2: true (0.0)
        else if(after1 == "Nv3") { if(0 >= threshold) { return true; } }//|   after1 = Nv3: true (0.0)
        else if(after1 == "Nv4") { if(0 >= threshold) { return true; } }//|   after1 = Nv4: true (0.0)
        else if(after1 == "b") { if(0 >= threshold) { return true; } }//|   after1 = b: true (0.0)
        else if(after1 == "notword") { if(0.793854033290653 >= threshold) { return false; } }//|   after1 = notword: false (620.0/161.0)
        else if(after1 == "begin") { if(0 >= threshold) { return true; } }//|   after1 = begin: true (0.0)
        else if(after1 == "end") { if(0 >= threshold) { return true; } }//|   after1 = end: true (0.0)
      }
      else if(front1 == "Nhaa") { if(0 >= threshold) { return true; } }//front1 = Nhaa: true (0.0)
      else if(front1 == "Nhab") { if(0 >= threshold) { return true; } }//front1 = Nhab: true (0.0)
      else if(front1 == "Nhac") { if(0 >= threshold) { return true; } }//front1 = Nhac: true (0.0)
      else if(front1 == "Nhb") { if(0 >= threshold) { return true; } }//front1 = Nhb: true (0.0)
      else if(front1 == "Nhc") { if(0 >= threshold) { return true; } }//front1 = Nhc: true (0.0)
      else if(front1 == "P")//front1 = P
      {
        if(after1 == "A") { if(0.708661417322835 >= threshold) { return false; } }//|   after1 = A: false (180.0/74.0)
        else if(after1 == "Caa")//|   after1 = Caa
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (11.0)
          else if(moreNeu == false) { if(0.767080745341615 >= threshold) { return true; } }//|   |   moreNeu = false: true (494.0/150.0)
        }
        else if(after1 == "Cab") { if(1 >= threshold) { return true; } }//|   after1 = Cab: true (3.0)
        else if(after1 == "Cba") { if(1 >= threshold) { return true; } }//|   after1 = Cba: true (5.0)
        else if(after1 == "Cbab") { if(0 >= threshold) { return true; } }//|   after1 = Cbab: true (0.0)
        else if(after1 == "Cbb") { if(0.916666666666667 >= threshold) { return true; } }//|   after1 = Cbb: true (77.0/7.0)
        else if(after1 == "Cbaa") { if(0 >= threshold) { return true; } }//|   after1 = Cbaa: true (0.0)
        else if(after1 == "Cbba") { if(0 >= threshold) { return true; } }//|   after1 = Cbba: true (0.0)
        else if(after1 == "Cbbb") { if(0 >= threshold) { return true; } }//|   after1 = Cbbb: true (0.0)
        else if(after1 == "Cbca") { if(0 >= threshold) { return true; } }//|   after1 = Cbca: true (0.0)
        else if(after1 == "Cbcb") { if(0 >= threshold) { return true; } }//|   after1 = Cbcb: true (0.0)
        else if(after1 == "D")//|   after1 = D
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (36.0)
          else if(moreNeu == false) { if(0.872463768115942 >= threshold) { return true; } }//|   |   moreNeu = false: true (2107.0/308.0)
        }
        else if(after1 == "Dab") { if(0 >= threshold) { return true; } }//|   after1 = Dab: true (0.0)
        else if(after1 == "Dbaa") { if(0 >= threshold) { return true; } }//|   after1 = Dbaa: true (0.0)
        else if(after1 == "Dbab") { if(0 >= threshold) { return true; } }//|   after1 = Dbab: true (0.0)
        else if(after1 == "Dbb") { if(0 >= threshold) { return true; } }//|   after1 = Dbb: true (0.0)
        else if(after1 == "Dbc") { if(0 >= threshold) { return true; } }//|   after1 = Dbc: true (0.0)
        else if(after1 == "Dc") { if(0 >= threshold) { return true; } }//|   after1 = Dc: true (0.0)
        else if(after1 == "Dd") { if(0 >= threshold) { return true; } }//|   after1 = Dd: true (0.0)
        else if(after1 == "Dg") { if(0 >= threshold) { return true; } }//|   after1 = Dg: true (0.0)
        else if(after1 == "Dh") { if(0 >= threshold) { return true; } }//|   after1 = Dh: true (0.0)
        else if(after1 == "Dj") { if(0 >= threshold) { return true; } }//|   after1 = Dj: true (0.0)
        else if(after1 == "Da")//|   after1 = Da
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (8.0)
          else if(moreNeu == false) { if(0.918367346938776 >= threshold) { return true; } }//|   |   moreNeu = false: true (270.0/24.0)
        }
        else if(after1 == "Daa") { if(0 >= threshold) { return true; } }//|   after1 = Daa: true (0.0)
        else if(after1 == "DE")//|   after1 = DE
        {
          if(moreNeu == true) { if(0.906976744186046 >= threshold) { return false; } }//|   |   moreNeu = true: false (78.0/8.0)
          else if(moreNeu == false) { if(0.870571428571429 >= threshold) { return true; } }//|   |   moreNeu = false: true (3047.0/453.0)
        }
        else if(after1 == "Dfa") { if(0.744186046511628 >= threshold) { return true; } }//|   after1 = Dfa: true (256.0/88.0)
        else if(after1 == "Dfb") { if(0 >= threshold) { return true; } }//|   after1 = Dfb: true (0.0)
        else if(after1 == "Di") { if(1 >= threshold) { return true; } }//|   after1 = Di: true (2.0)
        else if(after1 == "Dk") { if(0.75 >= threshold) { return false; } }//|   after1 = Dk: false (3.0/1.0)
        else if(after1 == "FW")//|   after1 = FW
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (2.0)
          else if(moreNeu == false) { if(0.72289156626506 >= threshold) { return true; } }//|   |   moreNeu = false: true (60.0/23.0)
        }
        else if(after1 == "I") { if(1 >= threshold) { return true; } }//|   after1 = I: true (4.0)
        else if(after1 == "Na") { if(0.728332891598198 >= threshold) { return false; } }//|   after1 = Na: false (5496.0/2050.0)
        else if(after1 == "Naa") { if(0 >= threshold) { return true; } }//|   after1 = Naa: true (0.0)
        else if(after1 == "Nab") { if(0 >= threshold) { return true; } }//|   after1 = Nab: true (0.0)
        else if(after1 == "Nac") { if(0 >= threshold) { return true; } }//|   after1 = Nac: true (0.0)
        else if(after1 == "Nad") { if(0 >= threshold) { return true; } }//|   after1 = Nad: true (0.0)
        else if(after1 == "Naea") { if(0 >= threshold) { return true; } }//|   after1 = Naea: true (0.0)
        else if(after1 == "Naeb") { if(0 >= threshold) { return true; } }//|   after1 = Naeb: true (0.0)
        else if(after1 == "Nb")//|   after1 = Nb
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (5.0)
          else if(moreNeu == false) { if(0.86046511627907 >= threshold) { return true; } }//|   |   moreNeu = false: true (370.0/60.0)
        }
        else if(after1 == "Nba") { if(0 >= threshold) { return true; } }//|   after1 = Nba: true (0.0)
        else if(after1 == "Nbc") { if(0 >= threshold) { return true; } }//|   after1 = Nbc: true (0.0)
        else if(after1 == "Nc")//|   after1 = Nc
        {
          if(moreNeu == true) { if(0.971830985915493 >= threshold) { return false; } }//|   |   moreNeu = true: false (69.0/2.0)
          else if(moreNeu == false) { if(0.75089179548157 >= threshold) { return true; } }//|   |   moreNeu = false: true (1263.0/419.0)
        }
        else if(after1 == "Nca") { if(0 >= threshold) { return true; } }//|   after1 = Nca: true (0.0)
        else if(after1 == "Ncb") { if(0 >= threshold) { return true; } }//|   after1 = Ncb: true (0.0)
        else if(after1 == "Ncc") { if(0 >= threshold) { return true; } }//|   after1 = Ncc: true (0.0)
        else if(after1 == "Nce") { if(0 >= threshold) { return true; } }//|   after1 = Nce: true (0.0)
        else if(after1 == "Ncd")//|   after1 = Ncd
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (3.0)
          else if(moreNeu == false) { if(0.721804511278195 >= threshold) { return true; } }//|   |   moreNeu = false: true (96.0/37.0)
        }
        else if(after1 == "Ncda") { if(0 >= threshold) { return true; } }//|   after1 = Ncda: true (0.0)
        else if(after1 == "Ncdb") { if(0 >= threshold) { return true; } }//|   after1 = Ncdb: true (0.0)
        else if(after1 == "Nd")//|   after1 = Nd
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (21.0)
          else if(moreNeu == false) { if(0.988216232140227 >= threshold) { return true; } }//|   |   moreNeu = false: true (6709.0/80.0)
        }
        else if(after1 == "Ndaa") { if(0 >= threshold) { return true; } }//|   after1 = Ndaa: true (0.0)
        else if(after1 == "Ndab") { if(0 >= threshold) { return true; } }//|   after1 = Ndab: true (0.0)
        else if(after1 == "Ndc") { if(0 >= threshold) { return true; } }//|   after1 = Ndc: true (0.0)
        else if(after1 == "Ndd") { if(0 >= threshold) { return true; } }//|   after1 = Ndd: true (0.0)
        else if(after1 == "Nep") { if(0.869109947643979 >= threshold) { return true; } }//|   after1 = Nep: true (166.0/25.0)
        else if(after1 == "Neqa")//|   after1 = Neqa
        {
          if(moreNeu == true) { if(0.857142857142857 >= threshold) { return false; } }//|   |   moreNeu = true: false (6.0/1.0)
          else if(moreNeu == false) { if(0.829493087557604 >= threshold) { return true; } }//|   |   moreNeu = false: true (180.0/37.0)
        }
        else if(after1 == "Neqb") { if(0.925233644859813 >= threshold) { return false; } }//|   after1 = Neqb: false (198.0/16.0)
        else if(after1 == "Nes")//|   after1 = Nes
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (11.0)
          else if(moreNeu == false) { if(0.921225382932166 >= threshold) { return true; } }//|   |   moreNeu = false: true (421.0/36.0)
        }
        else if(after1 == "Neu")//|   after1 = Neu
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (32.0)
          else if(moreNeu == false) { if(0.824750830564784 >= threshold) { return true; } }//|   |   moreNeu = false: true (993.0/211.0)
        }
        else if(after1 == "Nf") { if(0.682926829268293 >= threshold) { return true; } }//|   after1 = Nf: true (28.0/13.0)
        else if(after1 == "Nfa") { if(0 >= threshold) { return true; } }//|   after1 = Nfa: true (0.0)
        else if(after1 == "Nfb") { if(0 >= threshold) { return true; } }//|   after1 = Nfb: true (0.0)
        else if(after1 == "Nfc") { if(0 >= threshold) { return true; } }//|   after1 = Nfc: true (0.0)
        else if(after1 == "Nfd") { if(0 >= threshold) { return true; } }//|   after1 = Nfd: true (0.0)
        else if(after1 == "Nfe") { if(0 >= threshold) { return true; } }//|   after1 = Nfe: true (0.0)
        else if(after1 == "Nfg") { if(0 >= threshold) { return true; } }//|   after1 = Nfg: true (0.0)
        else if(after1 == "Nfh") { if(0 >= threshold) { return true; } }//|   after1 = Nfh: true (0.0)
        else if(after1 == "Nfi") { if(0 >= threshold) { return true; } }//|   after1 = Nfi: true (0.0)
        else if(after1 == "Ng")//|   after1 = Ng
        {
          if(moreNeu == true) { if(0.991379310344828 >= threshold) { return false; } }//|   |   moreNeu = true: false (115.0/1.0)
          else if(moreNeu == false) { if(0.757403379621884 >= threshold) { return true; } }//|   |   moreNeu = false: true (4527.0/1450.0)
        }
        else if(after1 == "Nh")//|   after1 = Nh
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (3.0)
          else if(moreNeu == false) { if(0.75 >= threshold) { return true; } }//|   |   moreNeu = false: true (273.0/91.0)
        }
        else if(after1 == "Nhaa") { if(0 >= threshold) { return true; } }//|   after1 = Nhaa: true (0.0)
        else if(after1 == "Nhab") { if(0 >= threshold) { return true; } }//|   after1 = Nhab: true (0.0)
        else if(after1 == "Nhac") { if(0 >= threshold) { return true; } }//|   after1 = Nhac: true (0.0)
        else if(after1 == "Nhb") { if(0 >= threshold) { return true; } }//|   after1 = Nhb: true (0.0)
        else if(after1 == "Nhc") { if(0 >= threshold) { return true; } }//|   after1 = Nhc: true (0.0)
        else if(after1 == "P")//|   after1 = P
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (24.0)
          else if(moreNeu == false) { if(0.84526558891455 >= threshold) { return true; } }//|   |   moreNeu = false: true (1098.0/201.0)
        }
        else if(after1 == "SHI") { if(0.859154929577465 >= threshold) { return true; } }//|   after1 = SHI: true (61.0/10.0)
        else if(after1 == "V_11") { if(0 >= threshold) { return true; } }//|   after1 = V_11: true (0.0)
        else if(after1 == "T")//|   after1 = T
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (2.0)
          else if(moreNeu == false) { if(0.771929824561403 >= threshold) { return true; } }//|   |   moreNeu = false: true (44.0/13.0)
        }
        else if(after1 == "Ta") { if(0 >= threshold) { return true; } }//|   after1 = Ta: true (0.0)
        else if(after1 == "Tb") { if(0 >= threshold) { return true; } }//|   after1 = Tb: true (0.0)
        else if(after1 == "Tc") { if(0 >= threshold) { return true; } }//|   after1 = Tc: true (0.0)
        else if(after1 == "Td") { if(0 >= threshold) { return true; } }//|   after1 = Td: true (0.0)
        else if(after1 == "VA")//|   after1 = VA
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (25.0)
          else if(moreNeu == false) { if(0.812200956937799 >= threshold) { return true; } }//|   |   moreNeu = false: true (679.0/157.0)
        }
        else if(after1 == "VA11") { if(0 >= threshold) { return true; } }//|   after1 = VA11: true (0.0)
        else if(after1 == "VA12") { if(0 >= threshold) { return true; } }//|   after1 = VA12: true (0.0)
        else if(after1 == "VA13") { if(0 >= threshold) { return true; } }//|   after1 = VA13: true (0.0)
        else if(after1 == "VA3") { if(0 >= threshold) { return true; } }//|   after1 = VA3: true (0.0)
        else if(after1 == "VA4") { if(0 >= threshold) { return true; } }//|   after1 = VA4: true (0.0)
        else if(after1 == "VAC") { if(0.714285714285714 >= threshold) { return true; } }//|   after1 = VAC: true (10.0/4.0)
        else if(after1 == "VA2") { if(0 >= threshold) { return true; } }//|   after1 = VA2: true (0.0)
        else if(after1 == "VB")//|   after1 = VB
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (2.0)
          else if(moreNeu == false) { if(0.748091603053435 >= threshold) { return true; } }//|   |   moreNeu = false: true (98.0/33.0)
        }
        else if(after1 == "VB11") { if(0 >= threshold) { return true; } }//|   after1 = VB11: true (0.0)
        else if(after1 == "VB12") { if(0 >= threshold) { return true; } }//|   after1 = VB12: true (0.0)
        else if(after1 == "VB2") { if(0 >= threshold) { return true; } }//|   after1 = VB2: true (0.0)
        else if(after1 == "VC")//|   after1 = VC
        {
          if(moreNeu == true) { if(0.9375 >= threshold) { return false; } }//|   |   moreNeu = true: false (75.0/5.0)
          else if(moreNeu == false) { if(0.853082465972778 >= threshold) { return true; } }//|   |   moreNeu = false: true (2131.0/367.0)
        }
        else if(after1 == "VC2") { if(0 >= threshold) { return true; } }//|   after1 = VC2: true (0.0)
        else if(after1 == "VC31") { if(0 >= threshold) { return true; } }//|   after1 = VC31: true (0.0)
        else if(after1 == "VC32") { if(0 >= threshold) { return true; } }//|   after1 = VC32: true (0.0)
        else if(after1 == "VC33") { if(0 >= threshold) { return true; } }//|   after1 = VC33: true (0.0)
        else if(after1 == "VCL")//|   after1 = VCL
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (6.0)
          else if(moreNeu == false) { if(0.856209150326797 >= threshold) { return true; } }//|   |   moreNeu = false: true (262.0/44.0)
        }
        else if(after1 == "VC1") { if(0 >= threshold) { return true; } }//|   after1 = VC1: true (0.0)
        else if(after1 == "VD")//|   after1 = VD
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (2.0)
          else if(moreNeu == false) { if(0.885714285714286 >= threshold) { return true; } }//|   |   moreNeu = false: true (62.0/8.0)
        }
        else if(after1 == "VD1") { if(0 >= threshold) { return true; } }//|   after1 = VD1: true (0.0)
        else if(after1 == "VD2") { if(0 >= threshold) { return true; } }//|   after1 = VD2: true (0.0)
        else if(after1 == "VE")//|   after1 = VE
        {
          if(moreNeu == true) { if(0.9 >= threshold) { return false; } }//|   |   moreNeu = true: false (9.0/1.0)
          else if(moreNeu == false) { if(0.874643874643875 >= threshold) { return true; } }//|   |   moreNeu = false: true (307.0/44.0)
        }
        else if(after1 == "VE11") { if(0 >= threshold) { return true; } }//|   after1 = VE11: true (0.0)
        else if(after1 == "VE12") { if(0 >= threshold) { return true; } }//|   after1 = VE12: true (0.0)
        else if(after1 == "VE2") { if(0 >= threshold) { return true; } }//|   after1 = VE2: true (0.0)
        else if(after1 == "VF")//|   after1 = VF
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (3.0)
          else if(moreNeu == false) { if(0.91358024691358 >= threshold) { return true; } }//|   |   moreNeu = false: true (74.0/7.0)
        }
        else if(after1 == "VF1") { if(0 >= threshold) { return true; } }//|   after1 = VF1: true (0.0)
        else if(after1 == "VF2") { if(0 >= threshold) { return true; } }//|   after1 = VF2: true (0.0)
        else if(after1 == "VG") { if(0.672043010752688 >= threshold) { return false; } }//|   after1 = VG: false (375.0/183.0)
        else if(after1 == "VG1") { if(0 >= threshold) { return true; } }//|   after1 = VG1: true (0.0)
        else if(after1 == "VG2") { if(0 >= threshold) { return true; } }//|   after1 = VG2: true (0.0)
        else if(after1 == "VH")//|   after1 = VH
        {
          if(moreNeu == true) { if(0.975 >= threshold) { return false; } }//|   |   moreNeu = true: false (39.0/1.0)
          else if(moreNeu == false) { if(0.678869621066153 >= threshold) { return true; } }//|   |   moreNeu = false: true (2114.0/1000.0)
        }
        else if(after1 == "VH11") { if(0 >= threshold) { return true; } }//|   after1 = VH11: true (0.0)
        else if(after1 == "VH12") { if(0 >= threshold) { return true; } }//|   after1 = VH12: true (0.0)
        else if(after1 == "VH13") { if(0 >= threshold) { return true; } }//|   after1 = VH13: true (0.0)
        else if(after1 == "VH14") { if(0 >= threshold) { return true; } }//|   after1 = VH14: true (0.0)
        else if(after1 == "VH15") { if(0 >= threshold) { return true; } }//|   after1 = VH15: true (0.0)
        else if(after1 == "VH17") { if(0 >= threshold) { return true; } }//|   after1 = VH17: true (0.0)
        else if(after1 == "VH21") { if(0 >= threshold) { return true; } }//|   after1 = VH21: true (0.0)
        else if(after1 == "VHC")//|   after1 = VHC
        {
          if(moreNeu == true) { if(0.833333333333333 >= threshold) { return false; } }//|   |   moreNeu = true: false (10.0/2.0)
          else if(moreNeu == false) { if(0.820610687022901 >= threshold) { return true; } }//|   |   moreNeu = false: true (215.0/47.0)
        }
        else if(after1 == "VH16") { if(0 >= threshold) { return true; } }//|   after1 = VH16: true (0.0)
        else if(after1 == "VH22") { if(0 >= threshold) { return true; } }//|   after1 = VH22: true (0.0)
        else if(after1 == "VI") { if(0.830508474576271 >= threshold) { return false; } }//|   after1 = VI: false (49.0/10.0)
        else if(after1 == "VI1") { if(0 >= threshold) { return true; } }//|   after1 = VI1: true (0.0)
        else if(after1 == "VI2") { if(0 >= threshold) { return true; } }//|   after1 = VI2: true (0.0)
        else if(after1 == "VI3") { if(0 >= threshold) { return true; } }//|   after1 = VI3: true (0.0)
        else if(after1 == "VJ")//|   after1 = VJ
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (13.0)
          else if(moreNeu == false) { if(0.769811320754717 >= threshold) { return true; } }//|   |   moreNeu = false: true (408.0/122.0)
        }
        else if(after1 == "VJ1") { if(0 >= threshold) { return true; } }//|   after1 = VJ1: true (0.0)
        else if(after1 == "VJ2") { if(0 >= threshold) { return true; } }//|   after1 = VJ2: true (0.0)
        else if(after1 == "VJ3") { if(0 >= threshold) { return true; } }//|   after1 = VJ3: true (0.0)
        else if(after1 == "VK") { if(0.752136752136752 >= threshold) { return true; } }//|   after1 = VK: true (88.0/29.0)
        else if(after1 == "VK1") { if(0 >= threshold) { return true; } }//|   after1 = VK1: true (0.0)
        else if(after1 == "VK2") { if(0 >= threshold) { return true; } }//|   after1 = VK2: true (0.0)
        else if(after1 == "VL")//|   after1 = VL
        {
          if(moreNeu == true) { if(0.8 >= threshold) { return false; } }//|   |   moreNeu = true: false (4.0/1.0)
          else if(moreNeu == false) { if(0.837606837606838 >= threshold) { return true; } }//|   |   moreNeu = false: true (196.0/38.0)
        }
        else if(after1 == "VL1") { if(0 >= threshold) { return true; } }//|   after1 = VL1: true (0.0)
        else if(after1 == "VL2") { if(0 >= threshold) { return true; } }//|   after1 = VL2: true (0.0)
        else if(after1 == "VL3") { if(0 >= threshold) { return true; } }//|   after1 = VL3: true (0.0)
        else if(after1 == "VL4") { if(0 >= threshold) { return true; } }//|   after1 = VL4: true (0.0)
        else if(after1 == "V_2")//|   after1 = V_2
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (3.0)
          else if(moreNeu == false) { if(0.778846153846154 >= threshold) { return true; } }//|   |   moreNeu = false: true (81.0/23.0)
        }
        else if(after1 == "Nv") { if(0.689655172413793 >= threshold) { return false; } }//|   after1 = Nv: false (160.0/72.0)
        else if(after1 == "Nv1") { if(0 >= threshold) { return true; } }//|   after1 = Nv1: true (0.0)
        else if(after1 == "Nv2") { if(0 >= threshold) { return true; } }//|   after1 = Nv2: true (0.0)
        else if(after1 == "Nv3") { if(0 >= threshold) { return true; } }//|   after1 = Nv3: true (0.0)
        else if(after1 == "Nv4") { if(0 >= threshold) { return true; } }//|   after1 = Nv4: true (0.0)
        else if(after1 == "b") { if(1 >= threshold) { return false; } }//|   after1 = b: false (1.0)
        else if(after1 == "notword")//|   after1 = notword
        {
          if(moreNeu == true) { if(0.991150442477876 >= threshold) { return false; } }//|   |   moreNeu = true: false (112.0/1.0)
          else if(moreNeu == false) { if(0.775289380976346 >= threshold) { return true; } }//|   |   moreNeu = false: true (3081.0/893.0)
        }
        else if(after1 == "begin") { if(0 >= threshold) { return true; } }//|   after1 = begin: true (0.0)
        else if(after1 == "end") { if(1 >= threshold) { return true; } }//|   after1 = end: true (2.0)
      }
      else if(front1 == "SHI")//front1 = SHI
      {
        if(after1 == "A") { if(0.860544217687075 >= threshold) { return false; } }//|   after1 = A: false (253.0/41.0)
        else if(after1 == "Caa") { if(0.697247706422018 >= threshold) { return false; } }//|   after1 = Caa: false (76.0/33.0)
        else if(after1 == "Cab") { if(0 >= threshold) { return false; } }//|   after1 = Cab: false (0.0)
        else if(after1 == "Cba") { if(0 >= threshold) { return false; } }//|   after1 = Cba: false (0.0)
        else if(after1 == "Cbab") { if(0 >= threshold) { return false; } }//|   after1 = Cbab: false (0.0)
        else if(after1 == "Cbb") { if(0.764705882352941 >= threshold) { return false; } }//|   after1 = Cbb: false (13.0/4.0)
        else if(after1 == "Cbaa") { if(0 >= threshold) { return false; } }//|   after1 = Cbaa: false (0.0)
        else if(after1 == "Cbba") { if(0 >= threshold) { return false; } }//|   after1 = Cbba: false (0.0)
        else if(after1 == "Cbbb") { if(0 >= threshold) { return false; } }//|   after1 = Cbbb: false (0.0)
        else if(after1 == "Cbca") { if(0 >= threshold) { return false; } }//|   after1 = Cbca: false (0.0)
        else if(after1 == "Cbcb") { if(0 >= threshold) { return false; } }//|   after1 = Cbcb: false (0.0)
        else if(after1 == "D") { if(0.79291553133515 >= threshold) { return false; } }//|   after1 = D: false (582.0/152.0)
        else if(after1 == "Dab") { if(0 >= threshold) { return false; } }//|   after1 = Dab: false (0.0)
        else if(after1 == "Dbaa") { if(0 >= threshold) { return false; } }//|   after1 = Dbaa: false (0.0)
        else if(after1 == "Dbab") { if(0 >= threshold) { return false; } }//|   after1 = Dbab: false (0.0)
        else if(after1 == "Dbb") { if(0 >= threshold) { return false; } }//|   after1 = Dbb: false (0.0)
        else if(after1 == "Dbc") { if(0 >= threshold) { return false; } }//|   after1 = Dbc: false (0.0)
        else if(after1 == "Dc") { if(0 >= threshold) { return false; } }//|   after1 = Dc: false (0.0)
        else if(after1 == "Dd") { if(0 >= threshold) { return false; } }//|   after1 = Dd: false (0.0)
        else if(after1 == "Dg") { if(0 >= threshold) { return false; } }//|   after1 = Dg: false (0.0)
        else if(after1 == "Dh") { if(0 >= threshold) { return false; } }//|   after1 = Dh: false (0.0)
        else if(after1 == "Dj") { if(0 >= threshold) { return false; } }//|   after1 = Dj: false (0.0)
        else if(after1 == "Da") { if(0.736842105263158 >= threshold) { return true; } }//|   after1 = Da: true (42.0/15.0)
        else if(after1 == "Daa") { if(0 >= threshold) { return false; } }//|   after1 = Daa: false (0.0)
        else if(after1 == "DE")//|   after1 = DE
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (19.0)
          else if(moreNeu == false) { if(0.850352112676056 >= threshold) { return true; } }//|   |   moreNeu = false: true (483.0/85.0)
        }
        else if(after1 == "Dfa") { if(0.880541871921182 >= threshold) { return false; } }//|   after1 = Dfa: false (715.0/97.0)
        else if(after1 == "Dfb") { if(1 >= threshold) { return false; } }//|   after1 = Dfb: false (2.0)
        else if(after1 == "Di") { if(0 >= threshold) { return false; } }//|   after1 = Di: false (0.0)
        else if(after1 == "Dk") { if(1 >= threshold) { return true; } }//|   after1 = Dk: true (2.0)
        else if(after1 == "FW") { if(0.777777777777778 >= threshold) { return false; } }//|   after1 = FW: false (28.0/8.0)
        else if(after1 == "I") { if(1 >= threshold) { return false; } }//|   after1 = I: false (1.0)
        else if(after1 == "Na") { if(0.871762948207171 >= threshold) { return false; } }//|   after1 = Na: false (3501.0/515.0)
        else if(after1 == "Naa") { if(0 >= threshold) { return false; } }//|   after1 = Naa: false (0.0)
        else if(after1 == "Nab") { if(0 >= threshold) { return false; } }//|   after1 = Nab: false (0.0)
        else if(after1 == "Nac") { if(0 >= threshold) { return false; } }//|   after1 = Nac: false (0.0)
        else if(after1 == "Nad") { if(0 >= threshold) { return false; } }//|   after1 = Nad: false (0.0)
        else if(after1 == "Naea") { if(0 >= threshold) { return false; } }//|   after1 = Naea: false (0.0)
        else if(after1 == "Naeb") { if(0 >= threshold) { return false; } }//|   after1 = Naeb: false (0.0)
        else if(after1 == "Nb") { if(0.726256983240223 >= threshold) { return true; } }//|   after1 = Nb: true (130.0/49.0)
        else if(after1 == "Nba") { if(0 >= threshold) { return false; } }//|   after1 = Nba: false (0.0)
        else if(after1 == "Nbc") { if(0 >= threshold) { return false; } }//|   after1 = Nbc: false (0.0)
        else if(after1 == "Nc")//|   after1 = Nc
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (10.0)
          else if(moreNeu == false) { if(0.710948905109489 >= threshold) { return true; } }//|   |   moreNeu = false: true (487.0/198.0)
        }
        else if(after1 == "Nca") { if(0 >= threshold) { return false; } }//|   after1 = Nca: false (0.0)
        else if(after1 == "Ncb") { if(0 >= threshold) { return false; } }//|   after1 = Ncb: false (0.0)
        else if(after1 == "Ncc") { if(0 >= threshold) { return false; } }//|   after1 = Ncc: false (0.0)
        else if(after1 == "Nce") { if(0 >= threshold) { return false; } }//|   after1 = Nce: false (0.0)
        else if(after1 == "Ncd") { if(0.680851063829787 >= threshold) { return false; } }//|   after1 = Ncd: false (32.0/15.0)
        else if(after1 == "Ncda") { if(0 >= threshold) { return false; } }//|   after1 = Ncda: false (0.0)
        else if(after1 == "Ncdb") { if(0 >= threshold) { return false; } }//|   after1 = Ncdb: false (0.0)
        else if(after1 == "Nd")//|   after1 = Nd
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (3.0)
          else if(moreNeu == false) { if(0.895491803278688 >= threshold) { return true; } }//|   |   moreNeu = false: true (437.0/51.0)
        }
        else if(after1 == "Ndaa") { if(0 >= threshold) { return false; } }//|   after1 = Ndaa: false (0.0)
        else if(after1 == "Ndab") { if(0 >= threshold) { return false; } }//|   after1 = Ndab: false (0.0)
        else if(after1 == "Ndc") { if(0 >= threshold) { return false; } }//|   after1 = Ndc: false (0.0)
        else if(after1 == "Ndd") { if(0 >= threshold) { return false; } }//|   after1 = Ndd: false (0.0)
        else if(after1 == "Nep") { if(0.670886075949367 >= threshold) { return true; } }//|   after1 = Nep: true (53.0/26.0)
        else if(after1 == "Neqa")//|   after1 = Neqa
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (2.0)
          else if(moreNeu == false) { if(0.69620253164557 >= threshold) { return true; } }//|   |   moreNeu = false: true (55.0/24.0)
        }
        else if(after1 == "Neqb") { if(0.9 >= threshold) { return false; } }//|   after1 = Neqb: false (27.0/3.0)
        else if(after1 == "Nes") { if(0.846153846153846 >= threshold) { return true; } }//|   after1 = Nes: true (33.0/6.0)
        else if(after1 == "Neu")//|   after1 = Neu
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (2.0)
          else if(moreNeu == false) { if(0.687664041994751 >= threshold) { return true; } }//|   |   moreNeu = false: true (262.0/119.0)
        }
        else if(after1 == "Nf") { if(0.714285714285714 >= threshold) { return true; } }//|   after1 = Nf: true (5.0/2.0)
        else if(after1 == "Nfa") { if(0 >= threshold) { return false; } }//|   after1 = Nfa: false (0.0)
        else if(after1 == "Nfb") { if(0 >= threshold) { return false; } }//|   after1 = Nfb: false (0.0)
        else if(after1 == "Nfc") { if(0 >= threshold) { return false; } }//|   after1 = Nfc: false (0.0)
        else if(after1 == "Nfd") { if(0 >= threshold) { return false; } }//|   after1 = Nfd: false (0.0)
        else if(after1 == "Nfe") { if(0 >= threshold) { return false; } }//|   after1 = Nfe: false (0.0)
        else if(after1 == "Nfg") { if(0 >= threshold) { return false; } }//|   after1 = Nfg: false (0.0)
        else if(after1 == "Nfh") { if(0 >= threshold) { return false; } }//|   after1 = Nfh: false (0.0)
        else if(after1 == "Nfi") { if(0 >= threshold) { return false; } }//|   after1 = Nfi: false (0.0)
        else if(after1 == "Ng") { if(0.720558882235529 >= threshold) { return false; } }//|   after1 = Ng: false (361.0/140.0)
        else if(after1 == "Nh") { if(0.693430656934307 >= threshold) { return false; } }//|   after1 = Nh: false (95.0/42.0)
        else if(after1 == "Nhaa") { if(0 >= threshold) { return false; } }//|   after1 = Nhaa: false (0.0)
        else if(after1 == "Nhab") { if(0 >= threshold) { return false; } }//|   after1 = Nhab: false (0.0)
        else if(after1 == "Nhac") { if(0 >= threshold) { return false; } }//|   after1 = Nhac: false (0.0)
        else if(after1 == "Nhb") { if(0 >= threshold) { return false; } }//|   after1 = Nhb: false (0.0)
        else if(after1 == "Nhc") { if(0 >= threshold) { return false; } }//|   after1 = Nhc: false (0.0)
        else if(after1 == "P") { if(0.787418655097614 >= threshold) { return false; } }//|   after1 = P: false (363.0/98.0)
        else if(after1 == "SHI")//|   after1 = SHI
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (2.0)
          else if(moreNeu == false) { if(0.8 >= threshold) { return true; } }//|   |   moreNeu = false: true (4.0/1.0)
        }
        else if(after1 == "V_11") { if(0 >= threshold) { return false; } }//|   after1 = V_11: false (0.0)
        else if(after1 == "T")//|   after1 = T
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (13.0)
          else if(moreNeu == false) { if(0.786666666666667 >= threshold) { return true; } }//|   |   moreNeu = false: true (59.0/16.0)
        }
        else if(after1 == "Ta") { if(0 >= threshold) { return false; } }//|   after1 = Ta: false (0.0)
        else if(after1 == "Tb") { if(0 >= threshold) { return false; } }//|   after1 = Tb: false (0.0)
        else if(after1 == "Tc") { if(0 >= threshold) { return false; } }//|   after1 = Tc: false (0.0)
        else if(after1 == "Td") { if(0 >= threshold) { return false; } }//|   after1 = Td: false (0.0)
        else if(after1 == "VA") { if(0.718849840255591 >= threshold) { return false; } }//|   after1 = VA: false (225.0/88.0)
        else if(after1 == "VA11") { if(0 >= threshold) { return false; } }//|   after1 = VA11: false (0.0)
        else if(after1 == "VA12") { if(0 >= threshold) { return false; } }//|   after1 = VA12: false (0.0)
        else if(after1 == "VA13") { if(0 >= threshold) { return false; } }//|   after1 = VA13: false (0.0)
        else if(after1 == "VA3") { if(0 >= threshold) { return false; } }//|   after1 = VA3: false (0.0)
        else if(after1 == "VA4") { if(0 >= threshold) { return false; } }//|   after1 = VA4: false (0.0)
        else if(after1 == "VAC") { if(0.75 >= threshold) { return false; } }//|   after1 = VAC: false (3.0/1.0)
        else if(after1 == "VA2") { if(0 >= threshold) { return false; } }//|   after1 = VA2: false (0.0)
        else if(after1 == "VB") { if(0.785714285714286 >= threshold) { return false; } }//|   after1 = VB: false (11.0/3.0)
        else if(after1 == "VB11") { if(0 >= threshold) { return false; } }//|   after1 = VB11: false (0.0)
        else if(after1 == "VB12") { if(0 >= threshold) { return false; } }//|   after1 = VB12: false (0.0)
        else if(after1 == "VB2") { if(0 >= threshold) { return false; } }//|   after1 = VB2: false (0.0)
        else if(after1 == "VC") { if(0.775453277545328 >= threshold) { return false; } }//|   after1 = VC: false (556.0/161.0)
        else if(after1 == "VC2") { if(0 >= threshold) { return false; } }//|   after1 = VC2: false (0.0)
        else if(after1 == "VC31") { if(0 >= threshold) { return false; } }//|   after1 = VC31: false (0.0)
        else if(after1 == "VC32") { if(0 >= threshold) { return false; } }//|   after1 = VC32: false (0.0)
        else if(after1 == "VC33") { if(0 >= threshold) { return false; } }//|   after1 = VC33: false (0.0)
        else if(after1 == "VCL")//|   after1 = VCL
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (13.0)
          else if(moreNeu == false) { if(0.677966101694915 >= threshold) { return true; } }//|   |   moreNeu = false: true (40.0/19.0)
        }
        else if(after1 == "VC1") { if(0 >= threshold) { return false; } }//|   after1 = VC1: false (0.0)
        else if(after1 == "VD") { if(0.733333333333333 >= threshold) { return false; } }//|   after1 = VD: false (11.0/4.0)
        else if(after1 == "VD1") { if(0 >= threshold) { return false; } }//|   after1 = VD1: false (0.0)
        else if(after1 == "VD2") { if(0 >= threshold) { return false; } }//|   after1 = VD2: false (0.0)
        else if(after1 == "VE") { if(0.774774774774775 >= threshold) { return false; } }//|   after1 = VE: false (86.0/25.0)
        else if(after1 == "VE11") { if(0 >= threshold) { return false; } }//|   after1 = VE11: false (0.0)
        else if(after1 == "VE12") { if(0 >= threshold) { return false; } }//|   after1 = VE12: false (0.0)
        else if(after1 == "VE2") { if(0 >= threshold) { return false; } }//|   after1 = VE2: false (0.0)
        else if(after1 == "VF") { if(0.709677419354839 >= threshold) { return false; } }//|   after1 = VF: false (22.0/9.0)
        else if(after1 == "VF1") { if(0 >= threshold) { return false; } }//|   after1 = VF1: false (0.0)
        else if(after1 == "VF2") { if(0 >= threshold) { return false; } }//|   after1 = VF2: false (0.0)
        else if(after1 == "VG") { if(0.842857142857143 >= threshold) { return false; } }//|   after1 = VG: false (59.0/11.0)
        else if(after1 == "VG1") { if(0 >= threshold) { return false; } }//|   after1 = VG1: false (0.0)
        else if(after1 == "VG2") { if(0 >= threshold) { return false; } }//|   after1 = VG2: false (0.0)
        else if(after1 == "VH") { if(0.932312252964427 >= threshold) { return false; } }//|   after1 = VH: false (1887.0/137.0)
        else if(after1 == "VH11") { if(0 >= threshold) { return false; } }//|   after1 = VH11: false (0.0)
        else if(after1 == "VH12") { if(0 >= threshold) { return false; } }//|   after1 = VH12: false (0.0)
        else if(after1 == "VH13") { if(0 >= threshold) { return false; } }//|   after1 = VH13: false (0.0)
        else if(after1 == "VH14") { if(0 >= threshold) { return false; } }//|   after1 = VH14: false (0.0)
        else if(after1 == "VH15") { if(0 >= threshold) { return false; } }//|   after1 = VH15: false (0.0)
        else if(after1 == "VH17") { if(0 >= threshold) { return false; } }//|   after1 = VH17: false (0.0)
        else if(after1 == "VH21") { if(0 >= threshold) { return false; } }//|   after1 = VH21: false (0.0)
        else if(after1 == "VHC") { if(0.946428571428571 >= threshold) { return false; } }//|   after1 = VHC: false (53.0/3.0)
        else if(after1 == "VH16") { if(0 >= threshold) { return false; } }//|   after1 = VH16: false (0.0)
        else if(after1 == "VH22") { if(0 >= threshold) { return false; } }//|   after1 = VH22: false (0.0)
        else if(after1 == "VI") { if(0.909090909090909 >= threshold) { return false; } }//|   after1 = VI: false (10.0/1.0)
        else if(after1 == "VI1") { if(0 >= threshold) { return false; } }//|   after1 = VI1: false (0.0)
        else if(after1 == "VI2") { if(0 >= threshold) { return false; } }//|   after1 = VI2: false (0.0)
        else if(after1 == "VI3") { if(0 >= threshold) { return false; } }//|   after1 = VI3: false (0.0)
        else if(after1 == "VJ") { if(0.900709219858156 >= threshold) { return false; } }//|   after1 = VJ: false (254.0/28.0)
        else if(after1 == "VJ1") { if(0 >= threshold) { return false; } }//|   after1 = VJ1: false (0.0)
        else if(after1 == "VJ2") { if(0 >= threshold) { return false; } }//|   after1 = VJ2: false (0.0)
        else if(after1 == "VJ3") { if(0 >= threshold) { return false; } }//|   after1 = VJ3: false (0.0)
        else if(after1 == "VK") { if(0.898989898989899 >= threshold) { return false; } }//|   after1 = VK: false (89.0/10.0)
        else if(after1 == "VK1") { if(0 >= threshold) { return false; } }//|   after1 = VK1: false (0.0)
        else if(after1 == "VK2") { if(0 >= threshold) { return false; } }//|   after1 = VK2: false (0.0)
        else if(after1 == "VL") { if(0.850877192982456 >= threshold) { return false; } }//|   after1 = VL: false (97.0/17.0)
        else if(after1 == "VL1") { if(0 >= threshold) { return false; } }//|   after1 = VL1: false (0.0)
        else if(after1 == "VL2") { if(0 >= threshold) { return false; } }//|   after1 = VL2: false (0.0)
        else if(after1 == "VL3") { if(0 >= threshold) { return false; } }//|   after1 = VL3: false (0.0)
        else if(after1 == "VL4") { if(0 >= threshold) { return false; } }//|   after1 = VL4: false (0.0)
        else if(after1 == "V_2") { if(0.83495145631068 >= threshold) { return false; } }//|   after1 = V_2: false (86.0/17.0)
        else if(after1 == "Nv") { if(0.916299559471366 >= threshold) { return false; } }//|   after1 = Nv: false (208.0/19.0)
        else if(after1 == "Nv1") { if(0 >= threshold) { return false; } }//|   after1 = Nv1: false (0.0)
        else if(after1 == "Nv2") { if(0 >= threshold) { return false; } }//|   after1 = Nv2: false (0.0)
        else if(after1 == "Nv3") { if(0 >= threshold) { return false; } }//|   after1 = Nv3: false (0.0)
        else if(after1 == "Nv4") { if(0 >= threshold) { return false; } }//|   after1 = Nv4: false (0.0)
        else if(after1 == "b") { if(0 >= threshold) { return false; } }//|   after1 = b: false (0.0)
        else if(after1 == "notword") { if(0.78834808259587 >= threshold) { return false; } }//|   after1 = notword: false (1069.0/287.0)
        else if(after1 == "begin") { if(0 >= threshold) { return false; } }//|   after1 = begin: false (0.0)
        else if(after1 == "end") { if(1 >= threshold) { return true; } }//|   after1 = end: true (1.0)
      }
      else if(front1 == "V_11") { if(0 >= threshold) { return true; } }//front1 = V_11: true (0.0)
      else if(front1 == "T") { if(0.666666666666667 >= threshold) { return true; } }//front1 = T: true (38.0/19.0)
      else if(front1 == "Ta") { if(0 >= threshold) { return true; } }//front1 = Ta: true (0.0)
      else if(front1 == "Tb") { if(0 >= threshold) { return true; } }//front1 = Tb: true (0.0)
      else if(front1 == "Tc") { if(0 >= threshold) { return true; } }//front1 = Tc: true (0.0)
      else if(front1 == "Td") { if(0 >= threshold) { return true; } }//front1 = Td: true (0.0)
      else if(front1 == "VA") { if(0.820367368989735 >= threshold) { return false; } }//front1 = VA: false (3037.0/665.0)
      else if(front1 == "VA11") { if(0 >= threshold) { return true; } }//front1 = VA11: true (0.0)
      else if(front1 == "VA12") { if(0 >= threshold) { return true; } }//front1 = VA12: true (0.0)
      else if(front1 == "VA13") { if(0 >= threshold) { return true; } }//front1 = VA13: true (0.0)
      else if(front1 == "VA3") { if(0 >= threshold) { return true; } }//front1 = VA3: true (0.0)
      else if(front1 == "VA4") { if(0 >= threshold) { return true; } }//front1 = VA4: true (0.0)
      else if(front1 == "VAC") { if(0.953125 >= threshold) { return false; } }//front1 = VAC: false (122.0/6.0)
      else if(front1 == "VA2") { if(0 >= threshold) { return true; } }//front1 = VA2: true (0.0)
      else if(front1 == "VB") { if(0.830409356725146 >= threshold) { return false; } }//front1 = VB: false (284.0/58.0)
      else if(front1 == "VB11") { if(0 >= threshold) { return true; } }//front1 = VB11: true (0.0)
      else if(front1 == "VB12") { if(0 >= threshold) { return true; } }//front1 = VB12: true (0.0)
      else if(front1 == "VB2") { if(0 >= threshold) { return true; } }//front1 = VB2: true (0.0)
      else if(front1 == "VC")//front1 = VC
      {
        if(after1 == "A") { if(0.905063291139241 >= threshold) { return false; } }//|   after1 = A: false (286.0/30.0)
        else if(after1 == "Caa") { if(0.752032520325203 >= threshold) { return false; } }//|   after1 = Caa: false (185.0/61.0)
        else if(after1 == "Cab") { if(0.75 >= threshold) { return true; } }//|   after1 = Cab: true (3.0/1.0)
        else if(after1 == "Cba") { if(1 >= threshold) { return false; } }//|   after1 = Cba: false (3.0)
        else if(after1 == "Cbab") { if(0 >= threshold) { return false; } }//|   after1 = Cbab: false (0.0)
        else if(after1 == "Cbb") { if(0.785714285714286 >= threshold) { return false; } }//|   after1 = Cbb: false (44.0/12.0)
        else if(after1 == "Cbaa") { if(0 >= threshold) { return false; } }//|   after1 = Cbaa: false (0.0)
        else if(after1 == "Cbba") { if(0 >= threshold) { return false; } }//|   after1 = Cbba: false (0.0)
        else if(after1 == "Cbbb") { if(0 >= threshold) { return false; } }//|   after1 = Cbbb: false (0.0)
        else if(after1 == "Cbca") { if(0 >= threshold) { return false; } }//|   after1 = Cbca: false (0.0)
        else if(after1 == "Cbcb") { if(0 >= threshold) { return false; } }//|   after1 = Cbcb: false (0.0)
        else if(after1 == "D") { if(0.784934497816594 >= threshold) { return false; } }//|   after1 = D: false (719.0/197.0)
        else if(after1 == "Dab") { if(0 >= threshold) { return false; } }//|   after1 = Dab: false (0.0)
        else if(after1 == "Dbaa") { if(0 >= threshold) { return false; } }//|   after1 = Dbaa: false (0.0)
        else if(after1 == "Dbab") { if(0 >= threshold) { return false; } }//|   after1 = Dbab: false (0.0)
        else if(after1 == "Dbb") { if(0 >= threshold) { return false; } }//|   after1 = Dbb: false (0.0)
        else if(after1 == "Dbc") { if(0 >= threshold) { return false; } }//|   after1 = Dbc: false (0.0)
        else if(after1 == "Dc") { if(0 >= threshold) { return false; } }//|   after1 = Dc: false (0.0)
        else if(after1 == "Dd") { if(0 >= threshold) { return false; } }//|   after1 = Dd: false (0.0)
        else if(after1 == "Dg") { if(0 >= threshold) { return false; } }//|   after1 = Dg: false (0.0)
        else if(after1 == "Dh") { if(0 >= threshold) { return false; } }//|   after1 = Dh: false (0.0)
        else if(after1 == "Dj") { if(0 >= threshold) { return false; } }//|   after1 = Dj: false (0.0)
        else if(after1 == "Da") { if(0.776470588235294 >= threshold) { return false; } }//|   after1 = Da: false (66.0/19.0)
        else if(after1 == "Daa") { if(0 >= threshold) { return false; } }//|   after1 = Daa: false (0.0)
        else if(after1 == "DE") { if(0.682368193604149 >= threshold) { return false; } }//|   after1 = DE: false (1579.0/735.0)
        else if(after1 == "Dfa") { if(0.874509803921569 >= threshold) { return false; } }//|   after1 = Dfa: false (223.0/32.0)
        else if(after1 == "Dfb") { if(0 >= threshold) { return false; } }//|   after1 = Dfb: false (0.0)
        else if(after1 == "Di") { if(1 >= threshold) { return false; } }//|   after1 = Di: false (2.0)
        else if(after1 == "Dk") { if(1 >= threshold) { return false; } }//|   after1 = Dk: false (3.0)
        else if(after1 == "FW") { if(0.897637795275591 >= threshold) { return false; } }//|   after1 = FW: false (114.0/13.0)
        else if(after1 == "I") { if(0.875 >= threshold) { return false; } }//|   after1 = I: false (7.0/1.0)
        else if(after1 == "Na") { if(0.889369331364691 >= threshold) { return false; } }//|   after1 = Na: false (8433.0/1049.0)
        else if(after1 == "Naa") { if(0 >= threshold) { return false; } }//|   after1 = Naa: false (0.0)
        else if(after1 == "Nab") { if(0 >= threshold) { return false; } }//|   after1 = Nab: false (0.0)
        else if(after1 == "Nac") { if(0 >= threshold) { return false; } }//|   after1 = Nac: false (0.0)
        else if(after1 == "Nad") { if(0 >= threshold) { return false; } }//|   after1 = Nad: false (0.0)
        else if(after1 == "Naea") { if(0 >= threshold) { return false; } }//|   after1 = Naea: false (0.0)
        else if(after1 == "Naeb") { if(0 >= threshold) { return false; } }//|   after1 = Naeb: false (0.0)
        else if(after1 == "Nb") { if(0.710106382978723 >= threshold) { return false; } }//|   after1 = Nb: false (267.0/109.0)
        else if(after1 == "Nba") { if(0 >= threshold) { return false; } }//|   after1 = Nba: false (0.0)
        else if(after1 == "Nbc") { if(0 >= threshold) { return false; } }//|   after1 = Nbc: false (0.0)
        else if(after1 == "Nc") { if(0.783505154639175 >= threshold) { return false; } }//|   after1 = Nc: false (912.0/252.0)
        else if(after1 == "Nca") { if(0 >= threshold) { return false; } }//|   after1 = Nca: false (0.0)
        else if(after1 == "Ncb") { if(0 >= threshold) { return false; } }//|   after1 = Ncb: false (0.0)
        else if(after1 == "Ncc") { if(0 >= threshold) { return false; } }//|   after1 = Ncc: false (0.0)
        else if(after1 == "Nce") { if(0 >= threshold) { return false; } }//|   after1 = Nce: false (0.0)
        else if(after1 == "Ncd") { if(0.731707317073171 >= threshold) { return false; } }//|   after1 = Ncd: false (30.0/11.0)
        else if(after1 == "Ncda") { if(0 >= threshold) { return false; } }//|   after1 = Ncda: false (0.0)
        else if(after1 == "Ncdb") { if(0 >= threshold) { return false; } }//|   after1 = Ncdb: false (0.0)
        else if(after1 == "Nd")//|   after1 = Nd
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (4.0)
          else if(moreNeu == false) { if(0.87402799377916 >= threshold) { return true; } }//|   |   moreNeu = false: true (562.0/81.0)
        }
        else if(after1 == "Ndaa") { if(0 >= threshold) { return false; } }//|   after1 = Ndaa: false (0.0)
        else if(after1 == "Ndab") { if(0 >= threshold) { return false; } }//|   after1 = Ndab: false (0.0)
        else if(after1 == "Ndc") { if(0 >= threshold) { return false; } }//|   after1 = Ndc: false (0.0)
        else if(after1 == "Ndd") { if(0 >= threshold) { return false; } }//|   after1 = Ndd: false (0.0)
        else if(after1 == "Nep") { if(0.71264367816092 >= threshold) { return true; } }//|   after1 = Nep: true (62.0/25.0)
        else if(after1 == "Neqa") { if(0.75 >= threshold) { return false; } }//|   after1 = Neqa: false (96.0/32.0)
        else if(after1 == "Neqb") { if(0.986111111111111 >= threshold) { return false; } }//|   after1 = Neqb: false (142.0/2.0)
        else if(after1 == "Nes")//|   after1 = Nes
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (6.0)
          else if(moreNeu == false) { if(0.676056338028169 >= threshold) { return true; } }//|   |   moreNeu = false: true (48.0/23.0)
        }
        else if(after1 == "Neu") { if(0.763688760806916 >= threshold) { return false; } }//|   after1 = Neu: false (530.0/164.0)
        else if(after1 == "Nf") { if(0.9 >= threshold) { return false; } }//|   after1 = Nf: false (27.0/3.0)
        else if(after1 == "Nfa") { if(0 >= threshold) { return false; } }//|   after1 = Nfa: false (0.0)
        else if(after1 == "Nfb") { if(0 >= threshold) { return false; } }//|   after1 = Nfb: false (0.0)
        else if(after1 == "Nfc") { if(0 >= threshold) { return false; } }//|   after1 = Nfc: false (0.0)
        else if(after1 == "Nfd") { if(0 >= threshold) { return false; } }//|   after1 = Nfd: false (0.0)
        else if(after1 == "Nfe") { if(0 >= threshold) { return false; } }//|   after1 = Nfe: false (0.0)
        else if(after1 == "Nfg") { if(0 >= threshold) { return false; } }//|   after1 = Nfg: false (0.0)
        else if(after1 == "Nfh") { if(0 >= threshold) { return false; } }//|   after1 = Nfh: false (0.0)
        else if(after1 == "Nfi") { if(0 >= threshold) { return false; } }//|   after1 = Nfi: false (0.0)
        else if(after1 == "Ng") { if(0.802556818181818 >= threshold) { return false; } }//|   after1 = Ng: false (565.0/139.0)
        else if(after1 == "Nh") { if(0.721393034825871 >= threshold) { return false; } }//|   after1 = Nh: false (145.0/56.0)
        else if(after1 == "Nhaa") { if(0 >= threshold) { return false; } }//|   after1 = Nhaa: false (0.0)
        else if(after1 == "Nhab") { if(0 >= threshold) { return false; } }//|   after1 = Nhab: false (0.0)
        else if(after1 == "Nhac") { if(0 >= threshold) { return false; } }//|   after1 = Nhac: false (0.0)
        else if(after1 == "Nhb") { if(0 >= threshold) { return false; } }//|   after1 = Nhb: false (0.0)
        else if(after1 == "Nhc") { if(0 >= threshold) { return false; } }//|   after1 = Nhc: false (0.0)
        else if(after1 == "P") { if(0.763636363636364 >= threshold) { return false; } }//|   after1 = P: false (378.0/117.0)
        else if(after1 == "SHI") { if(0.72972972972973 >= threshold) { return false; } }//|   after1 = SHI: false (27.0/10.0)
        else if(after1 == "V_11") { if(0 >= threshold) { return false; } }//|   after1 = V_11: false (0.0)
        else if(after1 == "T") { if(0.781818181818182 >= threshold) { return false; } }//|   after1 = T: false (129.0/36.0)
        else if(after1 == "Ta") { if(0 >= threshold) { return false; } }//|   after1 = Ta: false (0.0)
        else if(after1 == "Tb") { if(0 >= threshold) { return false; } }//|   after1 = Tb: false (0.0)
        else if(after1 == "Tc") { if(0 >= threshold) { return false; } }//|   after1 = Tc: false (0.0)
        else if(after1 == "Td") { if(0 >= threshold) { return false; } }//|   after1 = Td: false (0.0)
        else if(after1 == "VA") { if(0.739514348785872 >= threshold) { return false; } }//|   after1 = VA: false (335.0/118.0)
        else if(after1 == "VA11") { if(0 >= threshold) { return false; } }//|   after1 = VA11: false (0.0)
        else if(after1 == "VA12") { if(0 >= threshold) { return false; } }//|   after1 = VA12: false (0.0)
        else if(after1 == "VA13") { if(0 >= threshold) { return false; } }//|   after1 = VA13: false (0.0)
        else if(after1 == "VA3") { if(0 >= threshold) { return false; } }//|   after1 = VA3: false (0.0)
        else if(after1 == "VA4") { if(0 >= threshold) { return false; } }//|   after1 = VA4: false (0.0)
        else if(after1 == "VAC") { if(0.714285714285714 >= threshold) { return false; } }//|   after1 = VAC: false (5.0/2.0)
        else if(after1 == "VA2") { if(0 >= threshold) { return false; } }//|   after1 = VA2: false (0.0)
        else if(after1 == "VB") { if(0.770833333333333 >= threshold) { return false; } }//|   after1 = VB: false (37.0/11.0)
        else if(after1 == "VB11") { if(0 >= threshold) { return false; } }//|   after1 = VB11: false (0.0)
        else if(after1 == "VB12") { if(0 >= threshold) { return false; } }//|   after1 = VB12: false (0.0)
        else if(after1 == "VB2") { if(0 >= threshold) { return false; } }//|   after1 = VB2: false (0.0)
        else if(after1 == "VC") { if(0.749755620723363 >= threshold) { return false; } }//|   after1 = VC: false (767.0/256.0)
        else if(after1 == "VC2") { if(0 >= threshold) { return false; } }//|   after1 = VC2: false (0.0)
        else if(after1 == "VC31") { if(0 >= threshold) { return false; } }//|   after1 = VC31: false (0.0)
        else if(after1 == "VC32") { if(0 >= threshold) { return false; } }//|   after1 = VC32: false (0.0)
        else if(after1 == "VC33") { if(0 >= threshold) { return false; } }//|   after1 = VC33: false (0.0)
        else if(after1 == "VCL") { if(0.722772277227723 >= threshold) { return false; } }//|   after1 = VCL: false (73.0/28.0)
        else if(after1 == "VC1") { if(0 >= threshold) { return false; } }//|   after1 = VC1: false (0.0)
        else if(after1 == "VD") { if(0.892857142857143 >= threshold) { return false; } }//|   after1 = VD: false (50.0/6.0)
        else if(after1 == "VD1") { if(0 >= threshold) { return false; } }//|   after1 = VD1: false (0.0)
        else if(after1 == "VD2") { if(0 >= threshold) { return false; } }//|   after1 = VD2: false (0.0)
        else if(after1 == "VE") { if(0.768211920529801 >= threshold) { return false; } }//|   after1 = VE: false (116.0/35.0)
        else if(after1 == "VE11") { if(0 >= threshold) { return false; } }//|   after1 = VE11: false (0.0)
        else if(after1 == "VE12") { if(0 >= threshold) { return false; } }//|   after1 = VE12: false (0.0)
        else if(after1 == "VE2") { if(0 >= threshold) { return false; } }//|   after1 = VE2: false (0.0)
        else if(after1 == "VF") { if(0.810810810810811 >= threshold) { return false; } }//|   after1 = VF: false (30.0/7.0)
        else if(after1 == "VF1") { if(0 >= threshold) { return false; } }//|   after1 = VF1: false (0.0)
        else if(after1 == "VF2") { if(0 >= threshold) { return false; } }//|   after1 = VF2: false (0.0)
        else if(after1 == "VG") { if(0.876470588235294 >= threshold) { return false; } }//|   after1 = VG: false (149.0/21.0)
        else if(after1 == "VG1") { if(0 >= threshold) { return false; } }//|   after1 = VG1: false (0.0)
        else if(after1 == "VG2") { if(0 >= threshold) { return false; } }//|   after1 = VG2: false (0.0)
        else if(after1 == "VH") { if(0.907883082373782 >= threshold) { return false; } }//|   after1 = VH: false (2050.0/208.0)
        else if(after1 == "VH11") { if(0 >= threshold) { return false; } }//|   after1 = VH11: false (0.0)
        else if(after1 == "VH12") { if(0 >= threshold) { return false; } }//|   after1 = VH12: false (0.0)
        else if(after1 == "VH13") { if(0 >= threshold) { return false; } }//|   after1 = VH13: false (0.0)
        else if(after1 == "VH14") { if(0 >= threshold) { return false; } }//|   after1 = VH14: false (0.0)
        else if(after1 == "VH15") { if(0 >= threshold) { return false; } }//|   after1 = VH15: false (0.0)
        else if(after1 == "VH17") { if(0 >= threshold) { return false; } }//|   after1 = VH17: false (0.0)
        else if(after1 == "VH21") { if(0 >= threshold) { return false; } }//|   after1 = VH21: false (0.0)
        else if(after1 == "VHC") { if(0.847826086956522 >= threshold) { return false; } }//|   after1 = VHC: false (78.0/14.0)
        else if(after1 == "VH16") { if(0 >= threshold) { return false; } }//|   after1 = VH16: false (0.0)
        else if(after1 == "VH22") { if(0 >= threshold) { return false; } }//|   after1 = VH22: false (0.0)
        else if(after1 == "VI") { if(0.933333333333333 >= threshold) { return false; } }//|   after1 = VI: false (14.0/1.0)
        else if(after1 == "VI1") { if(0 >= threshold) { return false; } }//|   after1 = VI1: false (0.0)
        else if(after1 == "VI2") { if(0 >= threshold) { return false; } }//|   after1 = VI2: false (0.0)
        else if(after1 == "VI3") { if(0 >= threshold) { return false; } }//|   after1 = VI3: false (0.0)
        else if(after1 == "VJ") { if(0.854609929078014 >= threshold) { return false; } }//|   after1 = VJ: false (241.0/41.0)
        else if(after1 == "VJ1") { if(0 >= threshold) { return false; } }//|   after1 = VJ1: false (0.0)
        else if(after1 == "VJ2") { if(0 >= threshold) { return false; } }//|   after1 = VJ2: false (0.0)
        else if(after1 == "VJ3") { if(0 >= threshold) { return false; } }//|   after1 = VJ3: false (0.0)
        else if(after1 == "VK") { if(0.777777777777778 >= threshold) { return false; } }//|   after1 = VK: false (63.0/18.0)
        else if(after1 == "VK1") { if(0 >= threshold) { return false; } }//|   after1 = VK1: false (0.0)
        else if(after1 == "VK2") { if(0 >= threshold) { return false; } }//|   after1 = VK2: false (0.0)
        else if(after1 == "VL") { if(0.885245901639344 >= threshold) { return false; } }//|   after1 = VL: false (54.0/7.0)
        else if(after1 == "VL1") { if(0 >= threshold) { return false; } }//|   after1 = VL1: false (0.0)
        else if(after1 == "VL2") { if(0 >= threshold) { return false; } }//|   after1 = VL2: false (0.0)
        else if(after1 == "VL3") { if(0 >= threshold) { return false; } }//|   after1 = VL3: false (0.0)
        else if(after1 == "VL4") { if(0 >= threshold) { return false; } }//|   after1 = VL4: false (0.0)
        else if(after1 == "V_2") { if(0.888888888888889 >= threshold) { return false; } }//|   after1 = V_2: false (64.0/8.0)
        else if(after1 == "Nv") { if(0.829059829059829 >= threshold) { return false; } }//|   after1 = Nv: false (291.0/60.0)
        else if(after1 == "Nv1") { if(0 >= threshold) { return false; } }//|   after1 = Nv1: false (0.0)
        else if(after1 == "Nv2") { if(0 >= threshold) { return false; } }//|   after1 = Nv2: false (0.0)
        else if(after1 == "Nv3") { if(0 >= threshold) { return false; } }//|   after1 = Nv3: false (0.0)
        else if(after1 == "Nv4") { if(0 >= threshold) { return false; } }//|   after1 = Nv4: false (0.0)
        else if(after1 == "b") { if(1 >= threshold) { return false; } }//|   after1 = b: false (1.0)
        else if(after1 == "notword") { if(0.844282238442822 >= threshold) { return false; } }//|   after1 = notword: false (4164.0/768.0)
        else if(after1 == "begin") { if(0 >= threshold) { return false; } }//|   after1 = begin: false (0.0)
        else if(after1 == "end") { if(1 >= threshold) { return false; } }//|   after1 = end: false (1.0)
      }
      else if(front1 == "VC2") { if(0 >= threshold) { return true; } }//front1 = VC2: true (0.0)
      else if(front1 == "VC31") { if(0 >= threshold) { return true; } }//front1 = VC31: true (0.0)
      else if(front1 == "VC32") { if(0 >= threshold) { return true; } }//front1 = VC32: true (0.0)
      else if(front1 == "VC33") { if(0 >= threshold) { return true; } }//front1 = VC33: true (0.0)
      else if(front1 == "VCL")//front1 = VCL
      {
        if(after1 == "A") { if(0.923076923076923 >= threshold) { return false; } }//|   after1 = A: false (12.0/1.0)
        else if(after1 == "Caa") { if(0.725 >= threshold) { return false; } }//|   after1 = Caa: false (29.0/11.0)
        else if(after1 == "Cab") { if(0 >= threshold) { return false; } }//|   after1 = Cab: false (0.0)
        else if(after1 == "Cba") { if(1 >= threshold) { return false; } }//|   after1 = Cba: false (1.0)
        else if(after1 == "Cbab") { if(0 >= threshold) { return false; } }//|   after1 = Cbab: false (0.0)
        else if(after1 == "Cbb") { if(1 >= threshold) { return true; } }//|   after1 = Cbb: true (3.0)
        else if(after1 == "Cbaa") { if(0 >= threshold) { return false; } }//|   after1 = Cbaa: false (0.0)
        else if(after1 == "Cbba") { if(0 >= threshold) { return false; } }//|   after1 = Cbba: false (0.0)
        else if(after1 == "Cbbb") { if(0 >= threshold) { return false; } }//|   after1 = Cbbb: false (0.0)
        else if(after1 == "Cbca") { if(0 >= threshold) { return false; } }//|   after1 = Cbca: false (0.0)
        else if(after1 == "Cbcb") { if(0 >= threshold) { return false; } }//|   after1 = Cbcb: false (0.0)
        else if(after1 == "D") { if(0.724867724867725 >= threshold) { return false; } }//|   after1 = D: false (137.0/52.0)
        else if(after1 == "Dab") { if(0 >= threshold) { return false; } }//|   after1 = Dab: false (0.0)
        else if(after1 == "Dbaa") { if(0 >= threshold) { return false; } }//|   after1 = Dbaa: false (0.0)
        else if(after1 == "Dbab") { if(0 >= threshold) { return false; } }//|   after1 = Dbab: false (0.0)
        else if(after1 == "Dbb") { if(0 >= threshold) { return false; } }//|   after1 = Dbb: false (0.0)
        else if(after1 == "Dbc") { if(0 >= threshold) { return false; } }//|   after1 = Dbc: false (0.0)
        else if(after1 == "Dc") { if(0 >= threshold) { return false; } }//|   after1 = Dc: false (0.0)
        else if(after1 == "Dd") { if(0 >= threshold) { return false; } }//|   after1 = Dd: false (0.0)
        else if(after1 == "Dg") { if(0 >= threshold) { return false; } }//|   after1 = Dg: false (0.0)
        else if(after1 == "Dh") { if(0 >= threshold) { return false; } }//|   after1 = Dh: false (0.0)
        else if(after1 == "Dj") { if(0 >= threshold) { return false; } }//|   after1 = Dj: false (0.0)
        else if(after1 == "Da") { if(0.703703703703704 >= threshold) { return true; } }//|   after1 = Da: true (19.0/8.0)
        else if(after1 == "Daa") { if(0 >= threshold) { return false; } }//|   after1 = Daa: false (0.0)
        else if(after1 == "DE")//|   after1 = DE
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (33.0)
          else if(moreNeu == false) { if(0.670967741935484 >= threshold) { return true; } }//|   |   moreNeu = false: true (312.0/153.0)
        }
        else if(after1 == "Dfa") { if(0.793103448275862 >= threshold) { return false; } }//|   after1 = Dfa: false (23.0/6.0)
        else if(after1 == "Dfb") { if(1 >= threshold) { return false; } }//|   after1 = Dfb: false (1.0)
        else if(after1 == "Di") { if(0 >= threshold) { return false; } }//|   after1 = Di: false (0.0)
        else if(after1 == "Dk") { if(0 >= threshold) { return false; } }//|   after1 = Dk: false (0.0)
        else if(after1 == "FW") { if(0 >= threshold) { return false; } }//|   after1 = FW: false (0.0)
        else if(after1 == "I") { if(0 >= threshold) { return false; } }//|   after1 = I: false (0.0)
        else if(after1 == "Na") { if(0.893617021276596 >= threshold) { return false; } }//|   after1 = Na: false (588.0/70.0)
        else if(after1 == "Naa") { if(0 >= threshold) { return false; } }//|   after1 = Naa: false (0.0)
        else if(after1 == "Nab") { if(0 >= threshold) { return false; } }//|   after1 = Nab: false (0.0)
        else if(after1 == "Nac") { if(0 >= threshold) { return false; } }//|   after1 = Nac: false (0.0)
        else if(after1 == "Nad") { if(0 >= threshold) { return false; } }//|   after1 = Nad: false (0.0)
        else if(after1 == "Naea") { if(0 >= threshold) { return false; } }//|   after1 = Naea: false (0.0)
        else if(after1 == "Naeb") { if(0 >= threshold) { return false; } }//|   after1 = Naeb: false (0.0)
        else if(after1 == "Nb") { if(0.705882352941177 >= threshold) { return false; } }//|   after1 = Nb: false (12.0/5.0)
        else if(after1 == "Nba") { if(0 >= threshold) { return false; } }//|   after1 = Nba: false (0.0)
        else if(after1 == "Nbc") { if(0 >= threshold) { return false; } }//|   after1 = Nbc: false (0.0)
        else if(after1 == "Nc") { if(0.861702127659574 >= threshold) { return false; } }//|   after1 = Nc: false (162.0/26.0)
        else if(after1 == "Nca") { if(0 >= threshold) { return false; } }//|   after1 = Nca: false (0.0)
        else if(after1 == "Ncb") { if(0 >= threshold) { return false; } }//|   after1 = Ncb: false (0.0)
        else if(after1 == "Ncc") { if(0 >= threshold) { return false; } }//|   after1 = Ncc: false (0.0)
        else if(after1 == "Nce") { if(0 >= threshold) { return false; } }//|   after1 = Nce: false (0.0)
        else if(after1 == "Ncd") { if(0.777777777777778 >= threshold) { return false; } }//|   after1 = Ncd: false (7.0/2.0)
        else if(after1 == "Ncda") { if(0 >= threshold) { return false; } }//|   after1 = Ncda: false (0.0)
        else if(after1 == "Ncdb") { if(0 >= threshold) { return false; } }//|   after1 = Ncdb: false (0.0)
        else if(after1 == "Nd")//|   after1 = Nd
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (11.0)
          else if(moreNeu == false) { if(0.840425531914894 >= threshold) { return true; } }//|   |   moreNeu = false: true (79.0/15.0)
        }
        else if(after1 == "Ndaa") { if(0 >= threshold) { return false; } }//|   after1 = Ndaa: false (0.0)
        else if(after1 == "Ndab") { if(0 >= threshold) { return false; } }//|   after1 = Ndab: false (0.0)
        else if(after1 == "Ndc") { if(0 >= threshold) { return false; } }//|   after1 = Ndc: false (0.0)
        else if(after1 == "Ndd") { if(0 >= threshold) { return false; } }//|   after1 = Ndd: false (0.0)
        else if(after1 == "Nep") { if(0.8125 >= threshold) { return true; } }//|   after1 = Nep: true (13.0/3.0)
        else if(after1 == "Neqa") { if(0.916666666666667 >= threshold) { return false; } }//|   after1 = Neqa: false (11.0/1.0)
        else if(after1 == "Neqb") { if(0.986486486486487 >= threshold) { return false; } }//|   after1 = Neqb: false (73.0/1.0)
        else if(after1 == "Nes") { if(1 >= threshold) { return true; } }//|   after1 = Nes: true (5.0)
        else if(after1 == "Neu") { if(0.734939759036145 >= threshold) { return false; } }//|   after1 = Neu: false (61.0/22.0)
        else if(after1 == "Nf") { if(0.666666666666667 >= threshold) { return true; } }//|   after1 = Nf: true (6.0/3.0)
        else if(after1 == "Nfa") { if(0 >= threshold) { return false; } }//|   after1 = Nfa: false (0.0)
        else if(after1 == "Nfb") { if(0 >= threshold) { return false; } }//|   after1 = Nfb: false (0.0)
        else if(after1 == "Nfc") { if(0 >= threshold) { return false; } }//|   after1 = Nfc: false (0.0)
        else if(after1 == "Nfd") { if(0 >= threshold) { return false; } }//|   after1 = Nfd: false (0.0)
        else if(after1 == "Nfe") { if(0 >= threshold) { return false; } }//|   after1 = Nfe: false (0.0)
        else if(after1 == "Nfg") { if(0 >= threshold) { return false; } }//|   after1 = Nfg: false (0.0)
        else if(after1 == "Nfh") { if(0 >= threshold) { return false; } }//|   after1 = Nfh: false (0.0)
        else if(after1 == "Nfi") { if(0 >= threshold) { return false; } }//|   after1 = Nfi: false (0.0)
        else if(after1 == "Ng") { if(0.742063492063492 >= threshold) { return false; } }//|   after1 = Ng: false (187.0/65.0)
        else if(after1 == "Nh") { if(0.741935483870968 >= threshold) { return false; } }//|   after1 = Nh: false (23.0/8.0)
        else if(after1 == "Nhaa") { if(0 >= threshold) { return false; } }//|   after1 = Nhaa: false (0.0)
        else if(after1 == "Nhab") { if(0 >= threshold) { return false; } }//|   after1 = Nhab: false (0.0)
        else if(after1 == "Nhac") { if(0 >= threshold) { return false; } }//|   after1 = Nhac: false (0.0)
        else if(after1 == "Nhb") { if(0 >= threshold) { return false; } }//|   after1 = Nhb: false (0.0)
        else if(after1 == "Nhc") { if(0 >= threshold) { return false; } }//|   after1 = Nhc: false (0.0)
        else if(after1 == "P")//|   after1 = P
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (2.0)
          else if(moreNeu == false) { if(0.682926829268293 >= threshold) { return true; } }//|   |   moreNeu = false: true (28.0/13.0)
        }
        else if(after1 == "SHI") { if(0.75 >= threshold) { return false; } }//|   after1 = SHI: false (6.0/2.0)
        else if(after1 == "V_11") { if(0 >= threshold) { return false; } }//|   after1 = V_11: false (0.0)
        else if(after1 == "T") { if(0.714285714285714 >= threshold) { return false; } }//|   after1 = T: false (20.0/8.0)
        else if(after1 == "Ta") { if(0 >= threshold) { return false; } }//|   after1 = Ta: false (0.0)
        else if(after1 == "Tb") { if(0 >= threshold) { return false; } }//|   after1 = Tb: false (0.0)
        else if(after1 == "Tc") { if(0 >= threshold) { return false; } }//|   after1 = Tc: false (0.0)
        else if(after1 == "Td") { if(0 >= threshold) { return false; } }//|   after1 = Td: false (0.0)
        else if(after1 == "VA") { if(0.709090909090909 >= threshold) { return false; } }//|   after1 = VA: false (39.0/16.0)
        else if(after1 == "VA11") { if(0 >= threshold) { return false; } }//|   after1 = VA11: false (0.0)
        else if(after1 == "VA12") { if(0 >= threshold) { return false; } }//|   after1 = VA12: false (0.0)
        else if(after1 == "VA13") { if(0 >= threshold) { return false; } }//|   after1 = VA13: false (0.0)
        else if(after1 == "VA3") { if(0 >= threshold) { return false; } }//|   after1 = VA3: false (0.0)
        else if(after1 == "VA4") { if(0 >= threshold) { return false; } }//|   after1 = VA4: false (0.0)
        else if(after1 == "VAC") { if(0 >= threshold) { return false; } }//|   after1 = VAC: false (0.0)
        else if(after1 == "VA2") { if(0 >= threshold) { return false; } }//|   after1 = VA2: false (0.0)
        else if(after1 == "VB") { if(1 >= threshold) { return false; } }//|   after1 = VB: false (1.0)
        else if(after1 == "VB11") { if(0 >= threshold) { return false; } }//|   after1 = VB11: false (0.0)
        else if(after1 == "VB12") { if(0 >= threshold) { return false; } }//|   after1 = VB12: false (0.0)
        else if(after1 == "VB2") { if(0 >= threshold) { return false; } }//|   after1 = VB2: false (0.0)
        else if(after1 == "VC") { if(0.734939759036145 >= threshold) { return false; } }//|   after1 = VC: false (61.0/22.0)
        else if(after1 == "VC2") { if(0 >= threshold) { return false; } }//|   after1 = VC2: false (0.0)
        else if(after1 == "VC31") { if(0 >= threshold) { return false; } }//|   after1 = VC31: false (0.0)
        else if(after1 == "VC32") { if(0 >= threshold) { return false; } }//|   after1 = VC32: false (0.0)
        else if(after1 == "VC33") { if(0 >= threshold) { return false; } }//|   after1 = VC33: false (0.0)
        else if(after1 == "VCL") { if(0.727272727272727 >= threshold) { return true; } }//|   after1 = VCL: true (16.0/6.0)
        else if(after1 == "VC1") { if(0 >= threshold) { return false; } }//|   after1 = VC1: false (0.0)
        else if(after1 == "VD") { if(0.666666666666667 >= threshold) { return true; } }//|   after1 = VD: true (4.0/2.0)
        else if(after1 == "VD1") { if(0 >= threshold) { return false; } }//|   after1 = VD1: false (0.0)
        else if(after1 == "VD2") { if(0 >= threshold) { return false; } }//|   after1 = VD2: false (0.0)
        else if(after1 == "VE") { if(0.708333333333333 >= threshold) { return false; } }//|   after1 = VE: false (17.0/7.0)
        else if(after1 == "VE11") { if(0 >= threshold) { return false; } }//|   after1 = VE11: false (0.0)
        else if(after1 == "VE12") { if(0 >= threshold) { return false; } }//|   after1 = VE12: false (0.0)
        else if(after1 == "VE2") { if(0 >= threshold) { return false; } }//|   after1 = VE2: false (0.0)
        else if(after1 == "VF") { if(0 >= threshold) { return false; } }//|   after1 = VF: false (0.0)
        else if(after1 == "VF1") { if(0 >= threshold) { return false; } }//|   after1 = VF1: false (0.0)
        else if(after1 == "VF2") { if(0 >= threshold) { return false; } }//|   after1 = VF2: false (0.0)
        else if(after1 == "VG") { if(0.882352941176471 >= threshold) { return false; } }//|   after1 = VG: false (15.0/2.0)
        else if(after1 == "VG1") { if(0 >= threshold) { return false; } }//|   after1 = VG1: false (0.0)
        else if(after1 == "VG2") { if(0 >= threshold) { return false; } }//|   after1 = VG2: false (0.0)
        else if(after1 == "VH") { if(0.911196911196911 >= threshold) { return false; } }//|   after1 = VH: false (236.0/23.0)
        else if(after1 == "VH11") { if(0 >= threshold) { return false; } }//|   after1 = VH11: false (0.0)
        else if(after1 == "VH12") { if(0 >= threshold) { return false; } }//|   after1 = VH12: false (0.0)
        else if(after1 == "VH13") { if(0 >= threshold) { return false; } }//|   after1 = VH13: false (0.0)
        else if(after1 == "VH14") { if(0 >= threshold) { return false; } }//|   after1 = VH14: false (0.0)
        else if(after1 == "VH15") { if(0 >= threshold) { return false; } }//|   after1 = VH15: false (0.0)
        else if(after1 == "VH17") { if(0 >= threshold) { return false; } }//|   after1 = VH17: false (0.0)
        else if(after1 == "VH21") { if(0 >= threshold) { return false; } }//|   after1 = VH21: false (0.0)
        else if(after1 == "VHC") { if(0.818181818181818 >= threshold) { return false; } }//|   after1 = VHC: false (9.0/2.0)
        else if(after1 == "VH16") { if(0 >= threshold) { return false; } }//|   after1 = VH16: false (0.0)
        else if(after1 == "VH22") { if(0 >= threshold) { return false; } }//|   after1 = VH22: false (0.0)
        else if(after1 == "VI") { if(1 >= threshold) { return false; } }//|   after1 = VI: false (2.0)
        else if(after1 == "VI1") { if(0 >= threshold) { return false; } }//|   after1 = VI1: false (0.0)
        else if(after1 == "VI2") { if(0 >= threshold) { return false; } }//|   after1 = VI2: false (0.0)
        else if(after1 == "VI3") { if(0 >= threshold) { return false; } }//|   after1 = VI3: false (0.0)
        else if(after1 == "VJ") { if(0.961538461538462 >= threshold) { return false; } }//|   after1 = VJ: false (25.0/1.0)
        else if(after1 == "VJ1") { if(0 >= threshold) { return false; } }//|   after1 = VJ1: false (0.0)
        else if(after1 == "VJ2") { if(0 >= threshold) { return false; } }//|   after1 = VJ2: false (0.0)
        else if(after1 == "VJ3") { if(0 >= threshold) { return false; } }//|   after1 = VJ3: false (0.0)
        else if(after1 == "VK") { if(0.875 >= threshold) { return false; } }//|   after1 = VK: false (7.0/1.0)
        else if(after1 == "VK1") { if(0 >= threshold) { return false; } }//|   after1 = VK1: false (0.0)
        else if(after1 == "VK2") { if(0 >= threshold) { return false; } }//|   after1 = VK2: false (0.0)
        else if(after1 == "VL") { if(0.75 >= threshold) { return false; } }//|   after1 = VL: false (3.0/1.0)
        else if(after1 == "VL1") { if(0 >= threshold) { return false; } }//|   after1 = VL1: false (0.0)
        else if(after1 == "VL2") { if(0 >= threshold) { return false; } }//|   after1 = VL2: false (0.0)
        else if(after1 == "VL3") { if(0 >= threshold) { return false; } }//|   after1 = VL3: false (0.0)
        else if(after1 == "VL4") { if(0 >= threshold) { return false; } }//|   after1 = VL4: false (0.0)
        else if(after1 == "V_2") { if(0.714285714285714 >= threshold) { return true; } }//|   after1 = V_2: true (5.0/2.0)
        else if(after1 == "Nv") { if(0.961538461538462 >= threshold) { return false; } }//|   after1 = Nv: false (25.0/1.0)
        else if(after1 == "Nv1") { if(0 >= threshold) { return false; } }//|   after1 = Nv1: false (0.0)
        else if(after1 == "Nv2") { if(0 >= threshold) { return false; } }//|   after1 = Nv2: false (0.0)
        else if(after1 == "Nv3") { if(0 >= threshold) { return false; } }//|   after1 = Nv3: false (0.0)
        else if(after1 == "Nv4") { if(0 >= threshold) { return false; } }//|   after1 = Nv4: false (0.0)
        else if(after1 == "b") { if(0 >= threshold) { return false; } }//|   after1 = b: false (0.0)
        else if(after1 == "notword")//|   after1 = notword
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (59.0)
          else if(moreNeu == false) { if(0.680781758957655 >= threshold) { return true; } }//|   |   moreNeu = false: true (627.0/294.0)
        }
        else if(after1 == "begin") { if(0 >= threshold) { return false; } }//|   after1 = begin: false (0.0)
        else if(after1 == "end") { if(0 >= threshold) { return false; } }//|   after1 = end: false (0.0)
      }
      else if(front1 == "VC1") { if(0 >= threshold) { return true; } }//front1 = VC1: true (0.0)
      else if(front1 == "VD") { if(0.913475177304965 >= threshold) { return false; } }//front1 = VD: false (1288.0/122.0)
      else if(front1 == "VD1") { if(0 >= threshold) { return true; } }//front1 = VD1: true (0.0)
      else if(front1 == "VD2") { if(0 >= threshold) { return true; } }//front1 = VD2: true (0.0)
      else if(front1 == "VE")//front1 = VE
      {
        if(after1 == "A") { if(0.80952380952381 >= threshold) { return false; } }//|   after1 = A: false (34.0/8.0)
        else if(after1 == "Caa") { if(0.76 >= threshold) { return true; } }//|   after1 = Caa: true (38.0/12.0)
        else if(after1 == "Cab") { if(1 >= threshold) { return true; } }//|   after1 = Cab: true (2.0)
        else if(after1 == "Cba") { if(0 >= threshold) { return true; } }//|   after1 = Cba: true (0.0)
        else if(after1 == "Cbab") { if(0 >= threshold) { return true; } }//|   after1 = Cbab: true (0.0)
        else if(after1 == "Cbb") { if(0.777777777777778 >= threshold) { return true; } }//|   after1 = Cbb: true (21.0/6.0)
        else if(after1 == "Cbaa") { if(0 >= threshold) { return true; } }//|   after1 = Cbaa: true (0.0)
        else if(after1 == "Cbba") { if(0 >= threshold) { return true; } }//|   after1 = Cbba: true (0.0)
        else if(after1 == "Cbbb") { if(0 >= threshold) { return true; } }//|   after1 = Cbbb: true (0.0)
        else if(after1 == "Cbca") { if(0 >= threshold) { return true; } }//|   after1 = Cbca: true (0.0)
        else if(after1 == "Cbcb") { if(0 >= threshold) { return true; } }//|   after1 = Cbcb: true (0.0)
        else if(after1 == "D")//|   after1 = D
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (6.0)
          else if(moreNeu == false) { if(0.82435129740519 >= threshold) { return true; } }//|   |   moreNeu = false: true (413.0/88.0)
        }
        else if(after1 == "Dab") { if(0 >= threshold) { return true; } }//|   after1 = Dab: true (0.0)
        else if(after1 == "Dbaa") { if(0 >= threshold) { return true; } }//|   after1 = Dbaa: true (0.0)
        else if(after1 == "Dbab") { if(0 >= threshold) { return true; } }//|   after1 = Dbab: true (0.0)
        else if(after1 == "Dbb") { if(0 >= threshold) { return true; } }//|   after1 = Dbb: true (0.0)
        else if(after1 == "Dbc") { if(0 >= threshold) { return true; } }//|   after1 = Dbc: true (0.0)
        else if(after1 == "Dc") { if(0 >= threshold) { return true; } }//|   after1 = Dc: true (0.0)
        else if(after1 == "Dd") { if(0 >= threshold) { return true; } }//|   after1 = Dd: true (0.0)
        else if(after1 == "Dg") { if(0 >= threshold) { return true; } }//|   after1 = Dg: true (0.0)
        else if(after1 == "Dh") { if(0 >= threshold) { return true; } }//|   after1 = Dh: true (0.0)
        else if(after1 == "Dj") { if(0 >= threshold) { return true; } }//|   after1 = Dj: true (0.0)
        else if(after1 == "Da") { if(0.78125 >= threshold) { return true; } }//|   after1 = Da: true (25.0/7.0)
        else if(after1 == "Daa") { if(0 >= threshold) { return true; } }//|   after1 = Daa: true (0.0)
        else if(after1 == "DE")//|   after1 = DE
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (5.0)
          else if(moreNeu == false) { if(0.901345291479821 >= threshold) { return true; } }//|   |   moreNeu = false: true (402.0/44.0)
        }
        else if(after1 == "Dfa") { if(0.769230769230769 >= threshold) { return false; } }//|   after1 = Dfa: false (60.0/18.0)
        else if(after1 == "Dfb") { if(0 >= threshold) { return true; } }//|   after1 = Dfb: true (0.0)
        else if(after1 == "Di") { if(1 >= threshold) { return false; } }//|   after1 = Di: false (1.0)
        else if(after1 == "Dk") { if(0.666666666666667 >= threshold) { return true; } }//|   after1 = Dk: true (2.0/1.0)
        else if(after1 == "FW") { if(0.666666666666667 >= threshold) { return true; } }//|   after1 = FW: true (16.0/8.0)
        else if(after1 == "I") { if(1 >= threshold) { return true; } }//|   after1 = I: true (5.0)
        else if(after1 == "Na") { if(0.785919540229885 >= threshold) { return false; } }//|   after1 = Na: false (1641.0/447.0)
        else if(after1 == "Naa") { if(0 >= threshold) { return true; } }//|   after1 = Naa: true (0.0)
        else if(after1 == "Nab") { if(0 >= threshold) { return true; } }//|   after1 = Nab: true (0.0)
        else if(after1 == "Nac") { if(0 >= threshold) { return true; } }//|   after1 = Nac: true (0.0)
        else if(after1 == "Nad") { if(0 >= threshold) { return true; } }//|   after1 = Nad: true (0.0)
        else if(after1 == "Naea") { if(0 >= threshold) { return true; } }//|   after1 = Naea: true (0.0)
        else if(after1 == "Naeb") { if(0 >= threshold) { return true; } }//|   after1 = Naeb: true (0.0)
        else if(after1 == "Nb") { if(0.8 >= threshold) { return true; } }//|   after1 = Nb: true (64.0/16.0)
        else if(after1 == "Nba") { if(0 >= threshold) { return true; } }//|   after1 = Nba: true (0.0)
        else if(after1 == "Nbc") { if(0 >= threshold) { return true; } }//|   after1 = Nbc: true (0.0)
        else if(after1 == "Nc")//|   after1 = Nc
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (4.0)
          else if(moreNeu == false) { if(0.762177650429799 >= threshold) { return true; } }//|   |   moreNeu = false: true (266.0/83.0)
        }
        else if(after1 == "Nca") { if(0 >= threshold) { return true; } }//|   after1 = Nca: true (0.0)
        else if(after1 == "Ncb") { if(0 >= threshold) { return true; } }//|   after1 = Ncb: true (0.0)
        else if(after1 == "Ncc") { if(0 >= threshold) { return true; } }//|   after1 = Ncc: true (0.0)
        else if(after1 == "Nce") { if(0 >= threshold) { return true; } }//|   after1 = Nce: true (0.0)
        else if(after1 == "Ncd") { if(0.785714285714286 >= threshold) { return true; } }//|   after1 = Ncd: true (11.0/3.0)
        else if(after1 == "Ncda") { if(0 >= threshold) { return true; } }//|   after1 = Ncda: true (0.0)
        else if(after1 == "Ncdb") { if(0 >= threshold) { return true; } }//|   after1 = Ncdb: true (0.0)
        else if(after1 == "Nd")//|   after1 = Nd
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (3.0)
          else if(moreNeu == false) { if(0.961630695443645 >= threshold) { return true; } }//|   |   moreNeu = false: true (401.0/16.0)
        }
        else if(after1 == "Ndaa") { if(0 >= threshold) { return true; } }//|   after1 = Ndaa: true (0.0)
        else if(after1 == "Ndab") { if(0 >= threshold) { return true; } }//|   after1 = Ndab: true (0.0)
        else if(after1 == "Ndc") { if(0 >= threshold) { return true; } }//|   after1 = Ndc: true (0.0)
        else if(after1 == "Ndd") { if(0 >= threshold) { return true; } }//|   after1 = Ndd: true (0.0)
        else if(after1 == "Nep") { if(0.8 >= threshold) { return true; } }//|   after1 = Nep: true (28.0/7.0)
        else if(after1 == "Neqa") { if(0.792452830188679 >= threshold) { return true; } }//|   after1 = Neqa: true (42.0/11.0)
        else if(after1 == "Neqb") { if(0.875 >= threshold) { return false; } }//|   after1 = Neqb: false (21.0/3.0)
        else if(after1 == "Nes") { if(0.857142857142857 >= threshold) { return true; } }//|   after1 = Nes: true (36.0/6.0)
        else if(after1 == "Neu")//|   after1 = Neu
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (4.0)
          else if(moreNeu == false) { if(0.735955056179775 >= threshold) { return true; } }//|   |   moreNeu = false: true (131.0/47.0)
        }
        else if(after1 == "Nf") { if(0.714285714285714 >= threshold) { return false; } }//|   after1 = Nf: false (5.0/2.0)
        else if(after1 == "Nfa") { if(0 >= threshold) { return true; } }//|   after1 = Nfa: true (0.0)
        else if(after1 == "Nfb") { if(0 >= threshold) { return true; } }//|   after1 = Nfb: true (0.0)
        else if(after1 == "Nfc") { if(0 >= threshold) { return true; } }//|   after1 = Nfc: true (0.0)
        else if(after1 == "Nfd") { if(0 >= threshold) { return true; } }//|   after1 = Nfd: true (0.0)
        else if(after1 == "Nfe") { if(0 >= threshold) { return true; } }//|   after1 = Nfe: true (0.0)
        else if(after1 == "Nfg") { if(0 >= threshold) { return true; } }//|   after1 = Nfg: true (0.0)
        else if(after1 == "Nfh") { if(0 >= threshold) { return true; } }//|   after1 = Nfh: true (0.0)
        else if(after1 == "Nfi") { if(0 >= threshold) { return true; } }//|   after1 = Nfi: true (0.0)
        else if(after1 == "Ng")//|   after1 = Ng
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (9.0)
          else if(moreNeu == false) { if(0.684931506849315 >= threshold) { return true; } }//|   |   moreNeu = false: true (350.0/161.0)
        }
        else if(after1 == "Nh") { if(0.833333333333333 >= threshold) { return true; } }//|   after1 = Nh: true (90.0/18.0)
        else if(after1 == "Nhaa") { if(0 >= threshold) { return true; } }//|   after1 = Nhaa: true (0.0)
        else if(after1 == "Nhab") { if(0 >= threshold) { return true; } }//|   after1 = Nhab: true (0.0)
        else if(after1 == "Nhac") { if(0 >= threshold) { return true; } }//|   after1 = Nhac: true (0.0)
        else if(after1 == "Nhb") { if(0 >= threshold) { return true; } }//|   after1 = Nhb: true (0.0)
        else if(after1 == "Nhc") { if(0 >= threshold) { return true; } }//|   after1 = Nhc: true (0.0)
        else if(after1 == "P")//|   after1 = P
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (3.0)
          else if(moreNeu == false) { if(0.771573604060914 >= threshold) { return true; } }//|   |   moreNeu = false: true (152.0/45.0)
        }
        else if(after1 == "SHI")//|   after1 = SHI
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (6.0)
          else if(moreNeu == false) { if(0.911111111111111 >= threshold) { return true; } }//|   |   moreNeu = false: true (41.0/4.0)
        }
        else if(after1 == "V_11") { if(0 >= threshold) { return true; } }//|   after1 = V_11: true (0.0)
        else if(after1 == "T")//|   after1 = T
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (3.0)
          else if(moreNeu == false) { if(0.742857142857143 >= threshold) { return true; } }//|   |   moreNeu = false: true (26.0/9.0)
        }
        else if(after1 == "Ta") { if(0 >= threshold) { return true; } }//|   after1 = Ta: true (0.0)
        else if(after1 == "Tb") { if(0 >= threshold) { return true; } }//|   after1 = Tb: true (0.0)
        else if(after1 == "Tc") { if(0 >= threshold) { return true; } }//|   after1 = Tc: true (0.0)
        else if(after1 == "Td") { if(0 >= threshold) { return true; } }//|   after1 = Td: true (0.0)
        else if(after1 == "VA")//|   after1 = VA
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (6.0)
          else if(moreNeu == false) { if(0.753424657534247 >= threshold) { return true; } }//|   |   moreNeu = false: true (110.0/36.0)
        }
        else if(after1 == "VA11") { if(0 >= threshold) { return true; } }//|   after1 = VA11: true (0.0)
        else if(after1 == "VA12") { if(0 >= threshold) { return true; } }//|   after1 = VA12: true (0.0)
        else if(after1 == "VA13") { if(0 >= threshold) { return true; } }//|   after1 = VA13: true (0.0)
        else if(after1 == "VA3") { if(0 >= threshold) { return true; } }//|   after1 = VA3: true (0.0)
        else if(after1 == "VA4") { if(0 >= threshold) { return true; } }//|   after1 = VA4: true (0.0)
        else if(after1 == "VAC") { if(0.666666666666667 >= threshold) { return true; } }//|   after1 = VAC: true (2.0/1.0)
        else if(after1 == "VA2") { if(0 >= threshold) { return true; } }//|   after1 = VA2: true (0.0)
        else if(after1 == "VB") { if(0.722222222222222 >= threshold) { return true; } }//|   after1 = VB: true (13.0/5.0)
        else if(after1 == "VB11") { if(0 >= threshold) { return true; } }//|   after1 = VB11: true (0.0)
        else if(after1 == "VB12") { if(0 >= threshold) { return true; } }//|   after1 = VB12: true (0.0)
        else if(after1 == "VB2") { if(0 >= threshold) { return true; } }//|   after1 = VB2: true (0.0)
        else if(after1 == "VC")//|   after1 = VC
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (8.0)
          else if(moreNeu == false) { if(0.759036144578313 >= threshold) { return true; } }//|   |   moreNeu = false: true (252.0/80.0)
        }
        else if(after1 == "VC2") { if(0 >= threshold) { return true; } }//|   after1 = VC2: true (0.0)
        else if(after1 == "VC31") { if(0 >= threshold) { return true; } }//|   after1 = VC31: true (0.0)
        else if(after1 == "VC32") { if(0 >= threshold) { return true; } }//|   after1 = VC32: true (0.0)
        else if(after1 == "VC33") { if(0 >= threshold) { return true; } }//|   after1 = VC33: true (0.0)
        else if(after1 == "VCL") { if(0.804878048780488 >= threshold) { return true; } }//|   after1 = VCL: true (33.0/8.0)
        else if(after1 == "VC1") { if(0 >= threshold) { return true; } }//|   after1 = VC1: true (0.0)
        else if(after1 == "VD") { if(0.777777777777778 >= threshold) { return true; } }//|   after1 = VD: true (7.0/2.0)
        else if(after1 == "VD1") { if(0 >= threshold) { return true; } }//|   after1 = VD1: true (0.0)
        else if(after1 == "VD2") { if(0 >= threshold) { return true; } }//|   after1 = VD2: true (0.0)
        else if(after1 == "VE")//|   after1 = VE
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (4.0)
          else if(moreNeu == false) { if(0.796296296296296 >= threshold) { return true; } }//|   |   moreNeu = false: true (43.0/11.0)
        }
        else if(after1 == "VE11") { if(0 >= threshold) { return true; } }//|   after1 = VE11: true (0.0)
        else if(after1 == "VE12") { if(0 >= threshold) { return true; } }//|   after1 = VE12: true (0.0)
        else if(after1 == "VE2") { if(0 >= threshold) { return true; } }//|   after1 = VE2: true (0.0)
        else if(after1 == "VF") { if(0.8 >= threshold) { return true; } }//|   after1 = VF: true (8.0/2.0)
        else if(after1 == "VF1") { if(0 >= threshold) { return true; } }//|   after1 = VF1: true (0.0)
        else if(after1 == "VF2") { if(0 >= threshold) { return true; } }//|   after1 = VF2: true (0.0)
        else if(after1 == "VG") { if(0.686567164179104 >= threshold) { return false; } }//|   after1 = VG: false (46.0/21.0)
        else if(after1 == "VG1") { if(0 >= threshold) { return true; } }//|   after1 = VG1: true (0.0)
        else if(after1 == "VG2") { if(0 >= threshold) { return true; } }//|   after1 = VG2: true (0.0)
        else if(after1 == "VH") { if(0.790224032586558 >= threshold) { return false; } }//|   after1 = VH: false (388.0/103.0)
        else if(after1 == "VH11") { if(0 >= threshold) { return true; } }//|   after1 = VH11: true (0.0)
        else if(after1 == "VH12") { if(0 >= threshold) { return true; } }//|   after1 = VH12: true (0.0)
        else if(after1 == "VH13") { if(0 >= threshold) { return true; } }//|   after1 = VH13: true (0.0)
        else if(after1 == "VH14") { if(0 >= threshold) { return true; } }//|   after1 = VH14: true (0.0)
        else if(after1 == "VH15") { if(0 >= threshold) { return true; } }//|   after1 = VH15: true (0.0)
        else if(after1 == "VH17") { if(0 >= threshold) { return true; } }//|   after1 = VH17: true (0.0)
        else if(after1 == "VH21") { if(0 >= threshold) { return true; } }//|   after1 = VH21: true (0.0)
        else if(after1 == "VHC") { if(0.708333333333333 >= threshold) { return false; } }//|   after1 = VHC: false (17.0/7.0)
        else if(after1 == "VH16") { if(0 >= threshold) { return true; } }//|   after1 = VH16: true (0.0)
        else if(after1 == "VH22") { if(0 >= threshold) { return true; } }//|   after1 = VH22: true (0.0)
        else if(after1 == "VI") { if(1 >= threshold) { return true; } }//|   after1 = VI: true (1.0)
        else if(after1 == "VI1") { if(0 >= threshold) { return true; } }//|   after1 = VI1: true (0.0)
        else if(after1 == "VI2") { if(0 >= threshold) { return true; } }//|   after1 = VI2: true (0.0)
        else if(after1 == "VI3") { if(0 >= threshold) { return true; } }//|   after1 = VI3: true (0.0)
        else if(after1 == "VJ") { if(0.684210526315789 >= threshold) { return false; } }//|   after1 = VJ: false (52.0/24.0)
        else if(after1 == "VJ1") { if(0 >= threshold) { return true; } }//|   after1 = VJ1: true (0.0)
        else if(after1 == "VJ2") { if(0 >= threshold) { return true; } }//|   after1 = VJ2: true (0.0)
        else if(after1 == "VJ3") { if(0 >= threshold) { return true; } }//|   after1 = VJ3: true (0.0)
        else if(after1 == "VK") { if(0.666666666666667 >= threshold) { return true; } }//|   after1 = VK: true (24.0/12.0)
        else if(after1 == "VK1") { if(0 >= threshold) { return true; } }//|   after1 = VK1: true (0.0)
        else if(after1 == "VK2") { if(0 >= threshold) { return true; } }//|   after1 = VK2: true (0.0)
        else if(after1 == "VL") { if(0.684210526315789 >= threshold) { return true; } }//|   after1 = VL: true (26.0/12.0)
        else if(after1 == "VL1") { if(0 >= threshold) { return true; } }//|   after1 = VL1: true (0.0)
        else if(after1 == "VL2") { if(0 >= threshold) { return true; } }//|   after1 = VL2: true (0.0)
        else if(after1 == "VL3") { if(0 >= threshold) { return true; } }//|   after1 = VL3: true (0.0)
        else if(after1 == "VL4") { if(0 >= threshold) { return true; } }//|   after1 = VL4: true (0.0)
        else if(after1 == "V_2") { if(0.838709677419355 >= threshold) { return true; } }//|   after1 = V_2: true (26.0/5.0)
        else if(after1 == "Nv")//|   after1 = Nv
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (4.0)
          else if(moreNeu == false) { if(0.67741935483871 >= threshold) { return true; } }//|   |   moreNeu = false: true (42.0/20.0)
        }
        else if(after1 == "Nv1") { if(0 >= threshold) { return true; } }//|   after1 = Nv1: true (0.0)
        else if(after1 == "Nv2") { if(0 >= threshold) { return true; } }//|   after1 = Nv2: true (0.0)
        else if(after1 == "Nv3") { if(0 >= threshold) { return true; } }//|   after1 = Nv3: true (0.0)
        else if(after1 == "Nv4") { if(0 >= threshold) { return true; } }//|   after1 = Nv4: true (0.0)
        else if(after1 == "b") { if(0 >= threshold) { return true; } }//|   after1 = b: true (0.0)
        else if(after1 == "notword") { if(0.671618451915559 >= threshold) { return false; } }//|   after1 = notword: false (859.0/420.0)
        else if(after1 == "begin") { if(0 >= threshold) { return true; } }//|   after1 = begin: true (0.0)
        else if(after1 == "end") { if(1 >= threshold) { return true; } }//|   after1 = end: true (1.0)
      }
      else if(front1 == "VE11") { if(0 >= threshold) { return true; } }//front1 = VE11: true (0.0)
      else if(front1 == "VE12") { if(0 >= threshold) { return true; } }//front1 = VE12: true (0.0)
      else if(front1 == "VE2") { if(0 >= threshold) { return true; } }//front1 = VE2: true (0.0)
      else if(front1 == "VF")//front1 = VF
      {
        if(after1 == "A") { if(1 >= threshold) { return false; } }//|   after1 = A: false (11.0)
        else if(after1 == "Caa") { if(0 >= threshold) { return false; } }//|   after1 = Caa: false (0.0)
        else if(after1 == "Cab") { if(1 >= threshold) { return true; } }//|   after1 = Cab: true (1.0)
        else if(after1 == "Cba") { if(1 >= threshold) { return false; } }//|   after1 = Cba: false (2.0)
        else if(after1 == "Cbab") { if(0 >= threshold) { return false; } }//|   after1 = Cbab: false (0.0)
        else if(after1 == "Cbb") { if(0 >= threshold) { return false; } }//|   after1 = Cbb: false (0.0)
        else if(after1 == "Cbaa") { if(0 >= threshold) { return false; } }//|   after1 = Cbaa: false (0.0)
        else if(after1 == "Cbba") { if(0 >= threshold) { return false; } }//|   after1 = Cbba: false (0.0)
        else if(after1 == "Cbbb") { if(0 >= threshold) { return false; } }//|   after1 = Cbbb: false (0.0)
        else if(after1 == "Cbca") { if(0 >= threshold) { return false; } }//|   after1 = Cbca: false (0.0)
        else if(after1 == "Cbcb") { if(0 >= threshold) { return false; } }//|   after1 = Cbcb: false (0.0)
        else if(after1 == "D") { if(0.679245283018868 >= threshold) { return true; } }//|   after1 = D: true (36.0/17.0)
        else if(after1 == "Dab") { if(0 >= threshold) { return false; } }//|   after1 = Dab: false (0.0)
        else if(after1 == "Dbaa") { if(0 >= threshold) { return false; } }//|   after1 = Dbaa: false (0.0)
        else if(after1 == "Dbab") { if(0 >= threshold) { return false; } }//|   after1 = Dbab: false (0.0)
        else if(after1 == "Dbb") { if(0 >= threshold) { return false; } }//|   after1 = Dbb: false (0.0)
        else if(after1 == "Dbc") { if(0 >= threshold) { return false; } }//|   after1 = Dbc: false (0.0)
        else if(after1 == "Dc") { if(0 >= threshold) { return false; } }//|   after1 = Dc: false (0.0)
        else if(after1 == "Dd") { if(0 >= threshold) { return false; } }//|   after1 = Dd: false (0.0)
        else if(after1 == "Dg") { if(0 >= threshold) { return false; } }//|   after1 = Dg: false (0.0)
        else if(after1 == "Dh") { if(0 >= threshold) { return false; } }//|   after1 = Dh: false (0.0)
        else if(after1 == "Dj") { if(0 >= threshold) { return false; } }//|   after1 = Dj: false (0.0)
        else if(after1 == "Da") { if(0.8 >= threshold) { return false; } }//|   after1 = Da: false (4.0/1.0)
        else if(after1 == "Daa") { if(0 >= threshold) { return false; } }//|   after1 = Daa: false (0.0)
        else if(after1 == "DE")//|   after1 = DE
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (3.0)
          else if(moreNeu == false) { if(0.771428571428571 >= threshold) { return true; } }//|   |   moreNeu = false: true (27.0/8.0)
        }
        else if(after1 == "Dfa") { if(0.75 >= threshold) { return false; } }//|   after1 = Dfa: false (3.0/1.0)
        else if(after1 == "Dfb") { if(0 >= threshold) { return false; } }//|   after1 = Dfb: false (0.0)
        else if(after1 == "Di") { if(1 >= threshold) { return false; } }//|   after1 = Di: false (2.0)
        else if(after1 == "Dk") { if(0 >= threshold) { return false; } }//|   after1 = Dk: false (0.0)
        else if(after1 == "FW") { if(1 >= threshold) { return false; } }//|   after1 = FW: false (1.0)
        else if(after1 == "I") { if(0 >= threshold) { return false; } }//|   after1 = I: false (0.0)
        else if(after1 == "Na") { if(0.92156862745098 >= threshold) { return false; } }//|   after1 = Na: false (188.0/16.0)
        else if(after1 == "Naa") { if(0 >= threshold) { return false; } }//|   after1 = Naa: false (0.0)
        else if(after1 == "Nab") { if(0 >= threshold) { return false; } }//|   after1 = Nab: false (0.0)
        else if(after1 == "Nac") { if(0 >= threshold) { return false; } }//|   after1 = Nac: false (0.0)
        else if(after1 == "Nad") { if(0 >= threshold) { return false; } }//|   after1 = Nad: false (0.0)
        else if(after1 == "Naea") { if(0 >= threshold) { return false; } }//|   after1 = Naea: false (0.0)
        else if(after1 == "Naeb") { if(0 >= threshold) { return false; } }//|   after1 = Naeb: false (0.0)
        else if(after1 == "Nb") { if(0.666666666666667 >= threshold) { return true; } }//|   after1 = Nb: true (4.0/2.0)
        else if(after1 == "Nba") { if(0 >= threshold) { return false; } }//|   after1 = Nba: false (0.0)
        else if(after1 == "Nbc") { if(0 >= threshold) { return false; } }//|   after1 = Nbc: false (0.0)
        else if(after1 == "Nc") { if(0.85 >= threshold) { return false; } }//|   after1 = Nc: false (17.0/3.0)
        else if(after1 == "Nca") { if(0 >= threshold) { return false; } }//|   after1 = Nca: false (0.0)
        else if(after1 == "Ncb") { if(0 >= threshold) { return false; } }//|   after1 = Ncb: false (0.0)
        else if(after1 == "Ncc") { if(0 >= threshold) { return false; } }//|   after1 = Ncc: false (0.0)
        else if(after1 == "Nce") { if(0 >= threshold) { return false; } }//|   after1 = Nce: false (0.0)
        else if(after1 == "Ncd") { if(0 >= threshold) { return false; } }//|   after1 = Ncd: false (0.0)
        else if(after1 == "Ncda") { if(0 >= threshold) { return false; } }//|   after1 = Ncda: false (0.0)
        else if(after1 == "Ncdb") { if(0 >= threshold) { return false; } }//|   after1 = Ncdb: false (0.0)
        else if(after1 == "Nd") { if(0.9 >= threshold) { return true; } }//|   after1 = Nd: true (18.0/2.0)
        else if(after1 == "Ndaa") { if(0 >= threshold) { return false; } }//|   after1 = Ndaa: false (0.0)
        else if(after1 == "Ndab") { if(0 >= threshold) { return false; } }//|   after1 = Ndab: false (0.0)
        else if(after1 == "Ndc") { if(0 >= threshold) { return false; } }//|   after1 = Ndc: false (0.0)
        else if(after1 == "Ndd") { if(0 >= threshold) { return false; } }//|   after1 = Ndd: false (0.0)
        else if(after1 == "Nep") { if(0 >= threshold) { return false; } }//|   after1 = Nep: false (0.0)
        else if(after1 == "Neqa") { if(1 >= threshold) { return false; } }//|   after1 = Neqa: false (1.0)
        else if(after1 == "Neqb") { if(1 >= threshold) { return false; } }//|   after1 = Neqb: false (4.0)
        else if(after1 == "Nes") { if(0.8 >= threshold) { return true; } }//|   after1 = Nes: true (4.0/1.0)
        else if(after1 == "Neu") { if(0.75 >= threshold) { return false; } }//|   after1 = Neu: false (9.0/3.0)
        else if(after1 == "Nf") { if(1 >= threshold) { return false; } }//|   after1 = Nf: false (1.0)
        else if(after1 == "Nfa") { if(0 >= threshold) { return false; } }//|   after1 = Nfa: false (0.0)
        else if(after1 == "Nfb") { if(0 >= threshold) { return false; } }//|   after1 = Nfb: false (0.0)
        else if(after1 == "Nfc") { if(0 >= threshold) { return false; } }//|   after1 = Nfc: false (0.0)
        else if(after1 == "Nfd") { if(0 >= threshold) { return false; } }//|   after1 = Nfd: false (0.0)
        else if(after1 == "Nfe") { if(0 >= threshold) { return false; } }//|   after1 = Nfe: false (0.0)
        else if(after1 == "Nfg") { if(0 >= threshold) { return false; } }//|   after1 = Nfg: false (0.0)
        else if(after1 == "Nfh") { if(0 >= threshold) { return false; } }//|   after1 = Nfh: false (0.0)
        else if(after1 == "Nfi") { if(0 >= threshold) { return false; } }//|   after1 = Nfi: false (0.0)
        else if(after1 == "Ng") { if(0.6875 >= threshold) { return false; } }//|   after1 = Ng: false (33.0/15.0)
        else if(after1 == "Nh") { if(1 >= threshold) { return false; } }//|   after1 = Nh: false (1.0)
        else if(after1 == "Nhaa") { if(0 >= threshold) { return false; } }//|   after1 = Nhaa: false (0.0)
        else if(after1 == "Nhab") { if(0 >= threshold) { return false; } }//|   after1 = Nhab: false (0.0)
        else if(after1 == "Nhac") { if(0 >= threshold) { return false; } }//|   after1 = Nhac: false (0.0)
        else if(after1 == "Nhb") { if(0 >= threshold) { return false; } }//|   after1 = Nhb: false (0.0)
        else if(after1 == "Nhc") { if(0 >= threshold) { return false; } }//|   after1 = Nhc: false (0.0)
        else if(after1 == "P") { if(0.7 >= threshold) { return true; } }//|   after1 = P: true (7.0/3.0)
        else if(after1 == "SHI") { if(1 >= threshold) { return true; } }//|   after1 = SHI: true (1.0)
        else if(after1 == "V_11") { if(0 >= threshold) { return false; } }//|   after1 = V_11: false (0.0)
        else if(after1 == "T") { if(1 >= threshold) { return false; } }//|   after1 = T: false (1.0)
        else if(after1 == "Ta") { if(0 >= threshold) { return false; } }//|   after1 = Ta: false (0.0)
        else if(after1 == "Tb") { if(0 >= threshold) { return false; } }//|   after1 = Tb: false (0.0)
        else if(after1 == "Tc") { if(0 >= threshold) { return false; } }//|   after1 = Tc: false (0.0)
        else if(after1 == "Td") { if(0 >= threshold) { return false; } }//|   after1 = Td: false (0.0)
        else if(after1 == "VA") { if(0.8 >= threshold) { return true; } }//|   after1 = VA: true (32.0/8.0)
        else if(after1 == "VA11") { if(0 >= threshold) { return false; } }//|   after1 = VA11: false (0.0)
        else if(after1 == "VA12") { if(0 >= threshold) { return false; } }//|   after1 = VA12: false (0.0)
        else if(after1 == "VA13") { if(0 >= threshold) { return false; } }//|   after1 = VA13: false (0.0)
        else if(after1 == "VA3") { if(0 >= threshold) { return false; } }//|   after1 = VA3: false (0.0)
        else if(after1 == "VA4") { if(0 >= threshold) { return false; } }//|   after1 = VA4: false (0.0)
        else if(after1 == "VAC") { if(0 >= threshold) { return false; } }//|   after1 = VAC: false (0.0)
        else if(after1 == "VA2") { if(0 >= threshold) { return false; } }//|   after1 = VA2: false (0.0)
        else if(after1 == "VB") { if(1 >= threshold) { return false; } }//|   after1 = VB: false (2.0)
        else if(after1 == "VB11") { if(0 >= threshold) { return false; } }//|   after1 = VB11: false (0.0)
        else if(after1 == "VB12") { if(0 >= threshold) { return false; } }//|   after1 = VB12: false (0.0)
        else if(after1 == "VB2") { if(0 >= threshold) { return false; } }//|   after1 = VB2: false (0.0)
        else if(after1 == "VC")//|   after1 = VC
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (2.0)
          else if(moreNeu == false) { if(0.731707317073171 >= threshold) { return true; } }//|   |   moreNeu = false: true (30.0/11.0)
        }
        else if(after1 == "VC2") { if(0 >= threshold) { return false; } }//|   after1 = VC2: false (0.0)
        else if(after1 == "VC31") { if(0 >= threshold) { return false; } }//|   after1 = VC31: false (0.0)
        else if(after1 == "VC32") { if(0 >= threshold) { return false; } }//|   after1 = VC32: false (0.0)
        else if(after1 == "VC33") { if(0 >= threshold) { return false; } }//|   after1 = VC33: false (0.0)
        else if(after1 == "VCL") { if(0.733333333333333 >= threshold) { return true; } }//|   after1 = VCL: true (11.0/4.0)
        else if(after1 == "VC1") { if(0 >= threshold) { return false; } }//|   after1 = VC1: false (0.0)
        else if(after1 == "VD") { if(0.75 >= threshold) { return true; } }//|   after1 = VD: true (3.0/1.0)
        else if(after1 == "VD1") { if(0 >= threshold) { return false; } }//|   after1 = VD1: false (0.0)
        else if(after1 == "VD2") { if(0 >= threshold) { return false; } }//|   after1 = VD2: false (0.0)
        else if(after1 == "VE") { if(1 >= threshold) { return true; } }//|   after1 = VE: true (1.0)
        else if(after1 == "VE11") { if(0 >= threshold) { return false; } }//|   after1 = VE11: false (0.0)
        else if(after1 == "VE12") { if(0 >= threshold) { return false; } }//|   after1 = VE12: false (0.0)
        else if(after1 == "VE2") { if(0 >= threshold) { return false; } }//|   after1 = VE2: false (0.0)
        else if(after1 == "VF") { if(0 >= threshold) { return false; } }//|   after1 = VF: false (0.0)
        else if(after1 == "VF1") { if(0 >= threshold) { return false; } }//|   after1 = VF1: false (0.0)
        else if(after1 == "VF2") { if(0 >= threshold) { return false; } }//|   after1 = VF2: false (0.0)
        else if(after1 == "VG") { if(1 >= threshold) { return true; } }//|   after1 = VG: true (1.0)
        else if(after1 == "VG1") { if(0 >= threshold) { return false; } }//|   after1 = VG1: false (0.0)
        else if(after1 == "VG2") { if(0 >= threshold) { return false; } }//|   after1 = VG2: false (0.0)
        else if(after1 == "VH") { if(0.862745098039216 >= threshold) { return false; } }//|   after1 = VH: false (44.0/7.0)
        else if(after1 == "VH11") { if(0 >= threshold) { return false; } }//|   after1 = VH11: false (0.0)
        else if(after1 == "VH12") { if(0 >= threshold) { return false; } }//|   after1 = VH12: false (0.0)
        else if(after1 == "VH13") { if(0 >= threshold) { return false; } }//|   after1 = VH13: false (0.0)
        else if(after1 == "VH14") { if(0 >= threshold) { return false; } }//|   after1 = VH14: false (0.0)
        else if(after1 == "VH15") { if(0 >= threshold) { return false; } }//|   after1 = VH15: false (0.0)
        else if(after1 == "VH17") { if(0 >= threshold) { return false; } }//|   after1 = VH17: false (0.0)
        else if(after1 == "VH21") { if(0 >= threshold) { return false; } }//|   after1 = VH21: false (0.0)
        else if(after1 == "VHC") { if(0 >= threshold) { return false; } }//|   after1 = VHC: false (0.0)
        else if(after1 == "VH16") { if(0 >= threshold) { return false; } }//|   after1 = VH16: false (0.0)
        else if(after1 == "VH22") { if(0 >= threshold) { return false; } }//|   after1 = VH22: false (0.0)
        else if(after1 == "VI") { if(1 >= threshold) { return false; } }//|   after1 = VI: false (1.0)
        else if(after1 == "VI1") { if(0 >= threshold) { return false; } }//|   after1 = VI1: false (0.0)
        else if(after1 == "VI2") { if(0 >= threshold) { return false; } }//|   after1 = VI2: false (0.0)
        else if(after1 == "VI3") { if(0 >= threshold) { return false; } }//|   after1 = VI3: false (0.0)
        else if(after1 == "VJ") { if(0.733333333333333 >= threshold) { return false; } }//|   after1 = VJ: false (11.0/4.0)
        else if(after1 == "VJ1") { if(0 >= threshold) { return false; } }//|   after1 = VJ1: false (0.0)
        else if(after1 == "VJ2") { if(0 >= threshold) { return false; } }//|   after1 = VJ2: false (0.0)
        else if(after1 == "VJ3") { if(0 >= threshold) { return false; } }//|   after1 = VJ3: false (0.0)
        else if(after1 == "VK") { if(0 >= threshold) { return false; } }//|   after1 = VK: false (0.0)
        else if(after1 == "VK1") { if(0 >= threshold) { return false; } }//|   after1 = VK1: false (0.0)
        else if(after1 == "VK2") { if(0 >= threshold) { return false; } }//|   after1 = VK2: false (0.0)
        else if(after1 == "VL") { if(0 >= threshold) { return false; } }//|   after1 = VL: false (0.0)
        else if(after1 == "VL1") { if(0 >= threshold) { return false; } }//|   after1 = VL1: false (0.0)
        else if(after1 == "VL2") { if(0 >= threshold) { return false; } }//|   after1 = VL2: false (0.0)
        else if(after1 == "VL3") { if(0 >= threshold) { return false; } }//|   after1 = VL3: false (0.0)
        else if(after1 == "VL4") { if(0 >= threshold) { return false; } }//|   after1 = VL4: false (0.0)
        else if(after1 == "V_2") { if(1 >= threshold) { return false; } }//|   after1 = V_2: false (1.0)
        else if(after1 == "Nv") { if(1 >= threshold) { return false; } }//|   after1 = Nv: false (6.0)
        else if(after1 == "Nv1") { if(0 >= threshold) { return false; } }//|   after1 = Nv1: false (0.0)
        else if(after1 == "Nv2") { if(0 >= threshold) { return false; } }//|   after1 = Nv2: false (0.0)
        else if(after1 == "Nv3") { if(0 >= threshold) { return false; } }//|   after1 = Nv3: false (0.0)
        else if(after1 == "Nv4") { if(0 >= threshold) { return false; } }//|   after1 = Nv4: false (0.0)
        else if(after1 == "b") { if(0 >= threshold) { return false; } }//|   after1 = b: false (0.0)
        else if(after1 == "notword") { if(0.76 >= threshold) { return false; } }//|   after1 = notword: false (57.0/18.0)
        else if(after1 == "begin") { if(0 >= threshold) { return false; } }//|   after1 = begin: false (0.0)
        else if(after1 == "end") { if(0 >= threshold) { return false; } }//|   after1 = end: false (0.0)
      }
      else if(front1 == "VF1") { if(0 >= threshold) { return true; } }//front1 = VF1: true (0.0)
      else if(front1 == "VF2") { if(0 >= threshold) { return true; } }//front1 = VF2: true (0.0)
      else if(front1 == "VG")//front1 = VG
      {
        if(after1 == "A") { if(0.859154929577465 >= threshold) { return false; } }//|   after1 = A: false (122.0/20.0)
        else if(after1 == "Caa") { if(0.805194805194805 >= threshold) { return false; } }//|   after1 = Caa: false (62.0/15.0)
        else if(after1 == "Cab") { if(1 >= threshold) { return false; } }//|   after1 = Cab: false (1.0)
        else if(after1 == "Cba") { if(1 >= threshold) { return false; } }//|   after1 = Cba: false (1.0)
        else if(after1 == "Cbab") { if(0 >= threshold) { return false; } }//|   after1 = Cbab: false (0.0)
        else if(after1 == "Cbb") { if(0.777777777777778 >= threshold) { return false; } }//|   after1 = Cbb: false (7.0/2.0)
        else if(after1 == "Cbaa") { if(0 >= threshold) { return false; } }//|   after1 = Cbaa: false (0.0)
        else if(after1 == "Cbba") { if(0 >= threshold) { return false; } }//|   after1 = Cbba: false (0.0)
        else if(after1 == "Cbbb") { if(0 >= threshold) { return false; } }//|   after1 = Cbbb: false (0.0)
        else if(after1 == "Cbca") { if(0 >= threshold) { return false; } }//|   after1 = Cbca: false (0.0)
        else if(after1 == "Cbcb") { if(0 >= threshold) { return false; } }//|   after1 = Cbcb: false (0.0)
        else if(after1 == "D") { if(0.805084745762712 >= threshold) { return false; } }//|   after1 = D: false (190.0/46.0)
        else if(after1 == "Dab") { if(0 >= threshold) { return false; } }//|   after1 = Dab: false (0.0)
        else if(after1 == "Dbaa") { if(0 >= threshold) { return false; } }//|   after1 = Dbaa: false (0.0)
        else if(after1 == "Dbab") { if(0 >= threshold) { return false; } }//|   after1 = Dbab: false (0.0)
        else if(after1 == "Dbb") { if(0 >= threshold) { return false; } }//|   after1 = Dbb: false (0.0)
        else if(after1 == "Dbc") { if(0 >= threshold) { return false; } }//|   after1 = Dbc: false (0.0)
        else if(after1 == "Dc") { if(0 >= threshold) { return false; } }//|   after1 = Dc: false (0.0)
        else if(after1 == "Dd") { if(0 >= threshold) { return false; } }//|   after1 = Dd: false (0.0)
        else if(after1 == "Dg") { if(0 >= threshold) { return false; } }//|   after1 = Dg: false (0.0)
        else if(after1 == "Dh") { if(0 >= threshold) { return false; } }//|   after1 = Dh: false (0.0)
        else if(after1 == "Dj") { if(0 >= threshold) { return false; } }//|   after1 = Dj: false (0.0)
        else if(after1 == "Da") { if(0.8 >= threshold) { return false; } }//|   after1 = Da: false (8.0/2.0)
        else if(after1 == "Daa") { if(0 >= threshold) { return false; } }//|   after1 = Daa: false (0.0)
        else if(after1 == "DE")//|   after1 = DE
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (18.0)
          else if(moreNeu == false) { if(0.769072164948454 >= threshold) { return true; } }//|   |   moreNeu = false: true (373.0/112.0)
        }
        else if(after1 == "Dfa") { if(0.78 >= threshold) { return false; } }//|   after1 = Dfa: false (117.0/33.0)
        else if(after1 == "Dfb") { if(0 >= threshold) { return false; } }//|   after1 = Dfb: false (0.0)
        else if(after1 == "Di") { if(1 >= threshold) { return true; } }//|   after1 = Di: true (1.0)
        else if(after1 == "Dk") { if(1 >= threshold) { return false; } }//|   after1 = Dk: false (1.0)
        else if(after1 == "FW") { if(0.875 >= threshold) { return false; } }//|   after1 = FW: false (28.0/4.0)
        else if(after1 == "I") { if(1 >= threshold) { return false; } }//|   after1 = I: false (1.0)
        else if(after1 == "Na") { if(0.888050314465409 >= threshold) { return false; } }//|   after1 = Na: false (2118.0/267.0)
        else if(after1 == "Naa") { if(0 >= threshold) { return false; } }//|   after1 = Naa: false (0.0)
        else if(after1 == "Nab") { if(0 >= threshold) { return false; } }//|   after1 = Nab: false (0.0)
        else if(after1 == "Nac") { if(0 >= threshold) { return false; } }//|   after1 = Nac: false (0.0)
        else if(after1 == "Nad") { if(0 >= threshold) { return false; } }//|   after1 = Nad: false (0.0)
        else if(after1 == "Naea") { if(0 >= threshold) { return false; } }//|   after1 = Naea: false (0.0)
        else if(after1 == "Naeb") { if(0 >= threshold) { return false; } }//|   after1 = Naeb: false (0.0)
        else if(after1 == "Nb")//|   after1 = Nb
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (3.0)
          else if(moreNeu == false) { if(0.764705882352941 >= threshold) { return true; } }//|   |   moreNeu = false: true (39.0/12.0)
        }
        else if(after1 == "Nba") { if(0 >= threshold) { return false; } }//|   after1 = Nba: false (0.0)
        else if(after1 == "Nbc") { if(0 >= threshold) { return false; } }//|   after1 = Nbc: false (0.0)
        else if(after1 == "Nc") { if(0.685598377281947 >= threshold) { return false; } }//|   after1 = Nc: false (338.0/155.0)
        else if(after1 == "Nca") { if(0 >= threshold) { return false; } }//|   after1 = Nca: false (0.0)
        else if(after1 == "Ncb") { if(0 >= threshold) { return false; } }//|   after1 = Ncb: false (0.0)
        else if(after1 == "Ncc") { if(0 >= threshold) { return false; } }//|   after1 = Ncc: false (0.0)
        else if(after1 == "Nce") { if(0 >= threshold) { return false; } }//|   after1 = Nce: false (0.0)
        else if(after1 == "Ncd") { if(0.75 >= threshold) { return false; } }//|   after1 = Ncd: false (12.0/4.0)
        else if(after1 == "Ncda") { if(0 >= threshold) { return false; } }//|   after1 = Ncda: false (0.0)
        else if(after1 == "Ncdb") { if(0 >= threshold) { return false; } }//|   after1 = Ncdb: false (0.0)
        else if(after1 == "Nd") { if(0.935344827586207 >= threshold) { return true; } }//|   after1 = Nd: true (217.0/15.0)
        else if(after1 == "Ndaa") { if(0 >= threshold) { return false; } }//|   after1 = Ndaa: false (0.0)
        else if(after1 == "Ndab") { if(0 >= threshold) { return false; } }//|   after1 = Ndab: false (0.0)
        else if(after1 == "Ndc") { if(0 >= threshold) { return false; } }//|   after1 = Ndc: false (0.0)
        else if(after1 == "Ndd") { if(0 >= threshold) { return false; } }//|   after1 = Ndd: false (0.0)
        else if(after1 == "Nep") { if(0.7 >= threshold) { return true; } }//|   after1 = Nep: true (14.0/6.0)
        else if(after1 == "Neqa") { if(0.735849056603774 >= threshold) { return false; } }//|   after1 = Neqa: false (39.0/14.0)
        else if(after1 == "Neqb") { if(1 >= threshold) { return false; } }//|   after1 = Neqb: false (27.0)
        else if(after1 == "Nes") { if(0.8 >= threshold) { return true; } }//|   after1 = Nes: true (16.0/4.0)
        else if(after1 == "Neu") { if(0.712041884816754 >= threshold) { return false; } }//|   after1 = Neu: false (136.0/55.0)
        else if(after1 == "Nf") { if(0.888888888888889 >= threshold) { return false; } }//|   after1 = Nf: false (8.0/1.0)
        else if(after1 == "Nfa") { if(0 >= threshold) { return false; } }//|   after1 = Nfa: false (0.0)
        else if(after1 == "Nfb") { if(0 >= threshold) { return false; } }//|   after1 = Nfb: false (0.0)
        else if(after1 == "Nfc") { if(0 >= threshold) { return false; } }//|   after1 = Nfc: false (0.0)
        else if(after1 == "Nfd") { if(0 >= threshold) { return false; } }//|   after1 = Nfd: false (0.0)
        else if(after1 == "Nfe") { if(0 >= threshold) { return false; } }//|   after1 = Nfe: false (0.0)
        else if(after1 == "Nfg") { if(0 >= threshold) { return false; } }//|   after1 = Nfg: false (0.0)
        else if(after1 == "Nfh") { if(0 >= threshold) { return false; } }//|   after1 = Nfh: false (0.0)
        else if(after1 == "Nfi") { if(0 >= threshold) { return false; } }//|   after1 = Nfi: false (0.0)
        else if(after1 == "Ng") { if(0.766917293233083 >= threshold) { return false; } }//|   after1 = Ng: false (204.0/62.0)
        else if(after1 == "Nh") { if(0.680851063829787 >= threshold) { return false; } }//|   after1 = Nh: false (32.0/15.0)
        else if(after1 == "Nhaa") { if(0 >= threshold) { return false; } }//|   after1 = Nhaa: false (0.0)
        else if(after1 == "Nhab") { if(0 >= threshold) { return false; } }//|   after1 = Nhab: false (0.0)
        else if(after1 == "Nhac") { if(0 >= threshold) { return false; } }//|   after1 = Nhac: false (0.0)
        else if(after1 == "Nhb") { if(0 >= threshold) { return false; } }//|   after1 = Nhb: false (0.0)
        else if(after1 == "Nhc") { if(0 >= threshold) { return false; } }//|   after1 = Nhc: false (0.0)
        else if(after1 == "P") { if(0.761467889908257 >= threshold) { return false; } }//|   after1 = P: false (83.0/26.0)
        else if(after1 == "SHI") { if(0.875 >= threshold) { return true; } }//|   after1 = SHI: true (7.0/1.0)
        else if(after1 == "V_11") { if(0 >= threshold) { return false; } }//|   after1 = V_11: false (0.0)
        else if(after1 == "T") { if(0.866666666666667 >= threshold) { return false; } }//|   after1 = T: false (13.0/2.0)
        else if(after1 == "Ta") { if(0 >= threshold) { return false; } }//|   after1 = Ta: false (0.0)
        else if(after1 == "Tb") { if(0 >= threshold) { return false; } }//|   after1 = Tb: false (0.0)
        else if(after1 == "Tc") { if(0 >= threshold) { return false; } }//|   after1 = Tc: false (0.0)
        else if(after1 == "Td") { if(0 >= threshold) { return false; } }//|   after1 = Td: false (0.0)
        else if(after1 == "VA") { if(0.778571428571429 >= threshold) { return false; } }//|   after1 = VA: false (109.0/31.0)
        else if(after1 == "VA11") { if(0 >= threshold) { return false; } }//|   after1 = VA11: false (0.0)
        else if(after1 == "VA12") { if(0 >= threshold) { return false; } }//|   after1 = VA12: false (0.0)
        else if(after1 == "VA13") { if(0 >= threshold) { return false; } }//|   after1 = VA13: false (0.0)
        else if(after1 == "VA3") { if(0 >= threshold) { return false; } }//|   after1 = VA3: false (0.0)
        else if(after1 == "VA4") { if(0 >= threshold) { return false; } }//|   after1 = VA4: false (0.0)
        else if(after1 == "VAC") { if(1 >= threshold) { return false; } }//|   after1 = VAC: false (3.0)
        else if(after1 == "VA2") { if(0 >= threshold) { return false; } }//|   after1 = VA2: false (0.0)
        else if(after1 == "VB") { if(0.777777777777778 >= threshold) { return false; } }//|   after1 = VB: false (7.0/2.0)
        else if(after1 == "VB11") { if(0 >= threshold) { return false; } }//|   after1 = VB11: false (0.0)
        else if(after1 == "VB12") { if(0 >= threshold) { return false; } }//|   after1 = VB12: false (0.0)
        else if(after1 == "VB2") { if(0 >= threshold) { return false; } }//|   after1 = VB2: false (0.0)
        else if(after1 == "VC") { if(0.764900662251656 >= threshold) { return false; } }//|   after1 = VC: false (231.0/71.0)
        else if(after1 == "VC2") { if(0 >= threshold) { return false; } }//|   after1 = VC2: false (0.0)
        else if(after1 == "VC31") { if(0 >= threshold) { return false; } }//|   after1 = VC31: false (0.0)
        else if(after1 == "VC32") { if(0 >= threshold) { return false; } }//|   after1 = VC32: false (0.0)
        else if(after1 == "VC33") { if(0 >= threshold) { return false; } }//|   after1 = VC33: false (0.0)
        else if(after1 == "VCL") { if(0.714285714285714 >= threshold) { return false; } }//|   after1 = VCL: false (15.0/6.0)
        else if(after1 == "VC1") { if(0 >= threshold) { return false; } }//|   after1 = VC1: false (0.0)
        else if(after1 == "VD") { if(1 >= threshold) { return false; } }//|   after1 = VD: false (9.0)
        else if(after1 == "VD1") { if(0 >= threshold) { return false; } }//|   after1 = VD1: false (0.0)
        else if(after1 == "VD2") { if(0 >= threshold) { return false; } }//|   after1 = VD2: false (0.0)
        else if(after1 == "VE") { if(0.730769230769231 >= threshold) { return false; } }//|   after1 = VE: false (19.0/7.0)
        else if(after1 == "VE11") { if(0 >= threshold) { return false; } }//|   after1 = VE11: false (0.0)
        else if(after1 == "VE12") { if(0 >= threshold) { return false; } }//|   after1 = VE12: false (0.0)
        else if(after1 == "VE2") { if(0 >= threshold) { return false; } }//|   after1 = VE2: false (0.0)
        else if(after1 == "VF") { if(1 >= threshold) { return false; } }//|   after1 = VF: false (1.0)
        else if(after1 == "VF1") { if(0 >= threshold) { return false; } }//|   after1 = VF1: false (0.0)
        else if(after1 == "VF2") { if(0 >= threshold) { return false; } }//|   after1 = VF2: false (0.0)
        else if(after1 == "VG") { if(0.794117647058823 >= threshold) { return false; } }//|   after1 = VG: false (27.0/7.0)
        else if(after1 == "VG1") { if(0 >= threshold) { return false; } }//|   after1 = VG1: false (0.0)
        else if(after1 == "VG2") { if(0 >= threshold) { return false; } }//|   after1 = VG2: false (0.0)
        else if(after1 == "VH") { if(0.91289966923925 >= threshold) { return false; } }//|   after1 = VH: false (828.0/79.0)
        else if(after1 == "VH11") { if(0 >= threshold) { return false; } }//|   after1 = VH11: false (0.0)
        else if(after1 == "VH12") { if(0 >= threshold) { return false; } }//|   after1 = VH12: false (0.0)
        else if(after1 == "VH13") { if(0 >= threshold) { return false; } }//|   after1 = VH13: false (0.0)
        else if(after1 == "VH14") { if(0 >= threshold) { return false; } }//|   after1 = VH14: false (0.0)
        else if(after1 == "VH15") { if(0 >= threshold) { return false; } }//|   after1 = VH15: false (0.0)
        else if(after1 == "VH17") { if(0 >= threshold) { return false; } }//|   after1 = VH17: false (0.0)
        else if(after1 == "VH21") { if(0 >= threshold) { return false; } }//|   after1 = VH21: false (0.0)
        else if(after1 == "VHC") { if(0.942857142857143 >= threshold) { return false; } }//|   after1 = VHC: false (33.0/2.0)
        else if(after1 == "VH16") { if(0 >= threshold) { return false; } }//|   after1 = VH16: false (0.0)
        else if(after1 == "VH22") { if(0 >= threshold) { return false; } }//|   after1 = VH22: false (0.0)
        else if(after1 == "VI") { if(1 >= threshold) { return false; } }//|   after1 = VI: false (4.0)
        else if(after1 == "VI1") { if(0 >= threshold) { return false; } }//|   after1 = VI1: false (0.0)
        else if(after1 == "VI2") { if(0 >= threshold) { return false; } }//|   after1 = VI2: false (0.0)
        else if(after1 == "VI3") { if(0 >= threshold) { return false; } }//|   after1 = VI3: false (0.0)
        else if(after1 == "VJ") { if(0.926470588235294 >= threshold) { return false; } }//|   after1 = VJ: false (63.0/5.0)
        else if(after1 == "VJ1") { if(0 >= threshold) { return false; } }//|   after1 = VJ1: false (0.0)
        else if(after1 == "VJ2") { if(0 >= threshold) { return false; } }//|   after1 = VJ2: false (0.0)
        else if(after1 == "VJ3") { if(0 >= threshold) { return false; } }//|   after1 = VJ3: false (0.0)
        else if(after1 == "VK") { if(0.85 >= threshold) { return false; } }//|   after1 = VK: false (17.0/3.0)
        else if(after1 == "VK1") { if(0 >= threshold) { return false; } }//|   after1 = VK1: false (0.0)
        else if(after1 == "VK2") { if(0 >= threshold) { return false; } }//|   after1 = VK2: false (0.0)
        else if(after1 == "VL") { if(0.866666666666667 >= threshold) { return false; } }//|   after1 = VL: false (13.0/2.0)
        else if(after1 == "VL1") { if(0 >= threshold) { return false; } }//|   after1 = VL1: false (0.0)
        else if(after1 == "VL2") { if(0 >= threshold) { return false; } }//|   after1 = VL2: false (0.0)
        else if(after1 == "VL3") { if(0 >= threshold) { return false; } }//|   after1 = VL3: false (0.0)
        else if(after1 == "VL4") { if(0 >= threshold) { return false; } }//|   after1 = VL4: false (0.0)
        else if(after1 == "V_2") { if(0.96 >= threshold) { return false; } }//|   after1 = V_2: false (24.0/1.0)
        else if(after1 == "Nv") { if(0.808510638297872 >= threshold) { return false; } }//|   after1 = Nv: false (76.0/18.0)
        else if(after1 == "Nv1") { if(0 >= threshold) { return false; } }//|   after1 = Nv1: false (0.0)
        else if(after1 == "Nv2") { if(0 >= threshold) { return false; } }//|   after1 = Nv2: false (0.0)
        else if(after1 == "Nv3") { if(0 >= threshold) { return false; } }//|   after1 = Nv3: false (0.0)
        else if(after1 == "Nv4") { if(0 >= threshold) { return false; } }//|   after1 = Nv4: false (0.0)
        else if(after1 == "b") { if(0 >= threshold) { return false; } }//|   after1 = b: false (0.0)
        else if(after1 == "notword") { if(0.928705440900563 >= threshold) { return false; } }//|   after1 = notword: false (1980.0/152.0)
        else if(after1 == "begin") { if(0 >= threshold) { return false; } }//|   after1 = begin: false (0.0)
        else if(after1 == "end") { if(0.666666666666667 >= threshold) { return true; } }//|   after1 = end: true (2.0/1.0)
      }
      else if(front1 == "VG1") { if(0 >= threshold) { return true; } }//front1 = VG1: true (0.0)
      else if(front1 == "VG2") { if(0 >= threshold) { return true; } }//front1 = VG2: true (0.0)
      else if(front1 == "VH") { if(0.81524506683641 >= threshold) { return false; } }//front1 = VH: false (5123.0/1161.0)
      else if(front1 == "VH11") { if(0 >= threshold) { return true; } }//front1 = VH11: true (0.0)
      else if(front1 == "VH12") { if(0 >= threshold) { return true; } }//front1 = VH12: true (0.0)
      else if(front1 == "VH13") { if(0 >= threshold) { return true; } }//front1 = VH13: true (0.0)
      else if(front1 == "VH14") { if(0 >= threshold) { return true; } }//front1 = VH14: true (0.0)
      else if(front1 == "VH15") { if(0 >= threshold) { return true; } }//front1 = VH15: true (0.0)
      else if(front1 == "VH17") { if(0 >= threshold) { return true; } }//front1 = VH17: true (0.0)
      else if(front1 == "VH21") { if(0 >= threshold) { return true; } }//front1 = VH21: true (0.0)
      else if(front1 == "VHC") { if(0.775531914893617 >= threshold) { return false; } }//front1 = VHC: false (729.0/211.0)
      else if(front1 == "VH16") { if(0 >= threshold) { return true; } }//front1 = VH16: true (0.0)
      else if(front1 == "VH22") { if(0 >= threshold) { return true; } }//front1 = VH22: true (0.0)
      else if(front1 == "VI")//front1 = VI
      {
        if(after1 == "A") { if(0 >= threshold) { return false; } }//|   after1 = A: false (0.0)
        else if(after1 == "Caa") { if(0 >= threshold) { return false; } }//|   after1 = Caa: false (0.0)
        else if(after1 == "Cab") { if(0 >= threshold) { return false; } }//|   after1 = Cab: false (0.0)
        else if(after1 == "Cba") { if(0 >= threshold) { return false; } }//|   after1 = Cba: false (0.0)
        else if(after1 == "Cbab") { if(0 >= threshold) { return false; } }//|   after1 = Cbab: false (0.0)
        else if(after1 == "Cbb") { if(0 >= threshold) { return false; } }//|   after1 = Cbb: false (0.0)
        else if(after1 == "Cbaa") { if(0 >= threshold) { return false; } }//|   after1 = Cbaa: false (0.0)
        else if(after1 == "Cbba") { if(0 >= threshold) { return false; } }//|   after1 = Cbba: false (0.0)
        else if(after1 == "Cbbb") { if(0 >= threshold) { return false; } }//|   after1 = Cbbb: false (0.0)
        else if(after1 == "Cbca") { if(0 >= threshold) { return false; } }//|   after1 = Cbca: false (0.0)
        else if(after1 == "Cbcb") { if(0 >= threshold) { return false; } }//|   after1 = Cbcb: false (0.0)
        else if(after1 == "D") { if(1 >= threshold) { return false; } }//|   after1 = D: false (2.0)
        else if(after1 == "Dab") { if(0 >= threshold) { return false; } }//|   after1 = Dab: false (0.0)
        else if(after1 == "Dbaa") { if(0 >= threshold) { return false; } }//|   after1 = Dbaa: false (0.0)
        else if(after1 == "Dbab") { if(0 >= threshold) { return false; } }//|   after1 = Dbab: false (0.0)
        else if(after1 == "Dbb") { if(0 >= threshold) { return false; } }//|   after1 = Dbb: false (0.0)
        else if(after1 == "Dbc") { if(0 >= threshold) { return false; } }//|   after1 = Dbc: false (0.0)
        else if(after1 == "Dc") { if(0 >= threshold) { return false; } }//|   after1 = Dc: false (0.0)
        else if(after1 == "Dd") { if(0 >= threshold) { return false; } }//|   after1 = Dd: false (0.0)
        else if(after1 == "Dg") { if(0 >= threshold) { return false; } }//|   after1 = Dg: false (0.0)
        else if(after1 == "Dh") { if(0 >= threshold) { return false; } }//|   after1 = Dh: false (0.0)
        else if(after1 == "Dj") { if(0 >= threshold) { return false; } }//|   after1 = Dj: false (0.0)
        else if(after1 == "Da") { if(0 >= threshold) { return false; } }//|   after1 = Da: false (0.0)
        else if(after1 == "Daa") { if(0 >= threshold) { return false; } }//|   after1 = Daa: false (0.0)
        else if(after1 == "DE") { if(0.75 >= threshold) { return true; } }//|   after1 = DE: true (6.0/2.0)
        else if(after1 == "Dfa") { if(0 >= threshold) { return false; } }//|   after1 = Dfa: false (0.0)
        else if(after1 == "Dfb") { if(0 >= threshold) { return false; } }//|   after1 = Dfb: false (0.0)
        else if(after1 == "Di") { if(0 >= threshold) { return false; } }//|   after1 = Di: false (0.0)
        else if(after1 == "Dk") { if(0 >= threshold) { return false; } }//|   after1 = Dk: false (0.0)
        else if(after1 == "FW") { if(0 >= threshold) { return false; } }//|   after1 = FW: false (0.0)
        else if(after1 == "I") { if(0 >= threshold) { return false; } }//|   after1 = I: false (0.0)
        else if(after1 == "Na") { if(0.888888888888889 >= threshold) { return false; } }//|   after1 = Na: false (8.0/1.0)
        else if(after1 == "Naa") { if(0 >= threshold) { return false; } }//|   after1 = Naa: false (0.0)
        else if(after1 == "Nab") { if(0 >= threshold) { return false; } }//|   after1 = Nab: false (0.0)
        else if(after1 == "Nac") { if(0 >= threshold) { return false; } }//|   after1 = Nac: false (0.0)
        else if(after1 == "Nad") { if(0 >= threshold) { return false; } }//|   after1 = Nad: false (0.0)
        else if(after1 == "Naea") { if(0 >= threshold) { return false; } }//|   after1 = Naea: false (0.0)
        else if(after1 == "Naeb") { if(0 >= threshold) { return false; } }//|   after1 = Naeb: false (0.0)
        else if(after1 == "Nb") { if(1 >= threshold) { return false; } }//|   after1 = Nb: false (1.0)
        else if(after1 == "Nba") { if(0 >= threshold) { return false; } }//|   after1 = Nba: false (0.0)
        else if(after1 == "Nbc") { if(0 >= threshold) { return false; } }//|   after1 = Nbc: false (0.0)
        else if(after1 == "Nc") { if(1 >= threshold) { return false; } }//|   after1 = Nc: false (1.0)
        else if(after1 == "Nca") { if(0 >= threshold) { return false; } }//|   after1 = Nca: false (0.0)
        else if(after1 == "Ncb") { if(0 >= threshold) { return false; } }//|   after1 = Ncb: false (0.0)
        else if(after1 == "Ncc") { if(0 >= threshold) { return false; } }//|   after1 = Ncc: false (0.0)
        else if(after1 == "Nce") { if(0 >= threshold) { return false; } }//|   after1 = Nce: false (0.0)
        else if(after1 == "Ncd") { if(0 >= threshold) { return false; } }//|   after1 = Ncd: false (0.0)
        else if(after1 == "Ncda") { if(0 >= threshold) { return false; } }//|   after1 = Ncda: false (0.0)
        else if(after1 == "Ncdb") { if(0 >= threshold) { return false; } }//|   after1 = Ncdb: false (0.0)
        else if(after1 == "Nd") { if(1 >= threshold) { return true; } }//|   after1 = Nd: true (3.0)
        else if(after1 == "Ndaa") { if(0 >= threshold) { return false; } }//|   after1 = Ndaa: false (0.0)
        else if(after1 == "Ndab") { if(0 >= threshold) { return false; } }//|   after1 = Ndab: false (0.0)
        else if(after1 == "Ndc") { if(0 >= threshold) { return false; } }//|   after1 = Ndc: false (0.0)
        else if(after1 == "Ndd") { if(0 >= threshold) { return false; } }//|   after1 = Ndd: false (0.0)
        else if(after1 == "Nep") { if(0 >= threshold) { return false; } }//|   after1 = Nep: false (0.0)
        else if(after1 == "Neqa") { if(0 >= threshold) { return false; } }//|   after1 = Neqa: false (0.0)
        else if(after1 == "Neqb") { if(1 >= threshold) { return false; } }//|   after1 = Neqb: false (1.0)
        else if(after1 == "Nes") { if(0 >= threshold) { return false; } }//|   after1 = Nes: false (0.0)
        else if(after1 == "Neu") { if(0 >= threshold) { return false; } }//|   after1 = Neu: false (0.0)
        else if(after1 == "Nf") { if(0 >= threshold) { return false; } }//|   after1 = Nf: false (0.0)
        else if(after1 == "Nfa") { if(0 >= threshold) { return false; } }//|   after1 = Nfa: false (0.0)
        else if(after1 == "Nfb") { if(0 >= threshold) { return false; } }//|   after1 = Nfb: false (0.0)
        else if(after1 == "Nfc") { if(0 >= threshold) { return false; } }//|   after1 = Nfc: false (0.0)
        else if(after1 == "Nfd") { if(0 >= threshold) { return false; } }//|   after1 = Nfd: false (0.0)
        else if(after1 == "Nfe") { if(0 >= threshold) { return false; } }//|   after1 = Nfe: false (0.0)
        else if(after1 == "Nfg") { if(0 >= threshold) { return false; } }//|   after1 = Nfg: false (0.0)
        else if(after1 == "Nfh") { if(0 >= threshold) { return false; } }//|   after1 = Nfh: false (0.0)
        else if(after1 == "Nfi") { if(0 >= threshold) { return false; } }//|   after1 = Nfi: false (0.0)
        else if(after1 == "Ng") { if(1 >= threshold) { return false; } }//|   after1 = Ng: false (2.0)
        else if(after1 == "Nh") { if(1 >= threshold) { return true; } }//|   after1 = Nh: true (1.0)
        else if(after1 == "Nhaa") { if(0 >= threshold) { return false; } }//|   after1 = Nhaa: false (0.0)
        else if(after1 == "Nhab") { if(0 >= threshold) { return false; } }//|   after1 = Nhab: false (0.0)
        else if(after1 == "Nhac") { if(0 >= threshold) { return false; } }//|   after1 = Nhac: false (0.0)
        else if(after1 == "Nhb") { if(0 >= threshold) { return false; } }//|   after1 = Nhb: false (0.0)
        else if(after1 == "Nhc") { if(0 >= threshold) { return false; } }//|   after1 = Nhc: false (0.0)
        else if(after1 == "P") { if(1 >= threshold) { return true; } }//|   after1 = P: true (1.0)
        else if(after1 == "SHI") { if(0 >= threshold) { return false; } }//|   after1 = SHI: false (0.0)
        else if(after1 == "V_11") { if(0 >= threshold) { return false; } }//|   after1 = V_11: false (0.0)
        else if(after1 == "T") { if(1 >= threshold) { return false; } }//|   after1 = T: false (1.0)
        else if(after1 == "Ta") { if(0 >= threshold) { return false; } }//|   after1 = Ta: false (0.0)
        else if(after1 == "Tb") { if(0 >= threshold) { return false; } }//|   after1 = Tb: false (0.0)
        else if(after1 == "Tc") { if(0 >= threshold) { return false; } }//|   after1 = Tc: false (0.0)
        else if(after1 == "Td") { if(0 >= threshold) { return false; } }//|   after1 = Td: false (0.0)
        else if(after1 == "VA") { if(0 >= threshold) { return false; } }//|   after1 = VA: false (0.0)
        else if(after1 == "VA11") { if(0 >= threshold) { return false; } }//|   after1 = VA11: false (0.0)
        else if(after1 == "VA12") { if(0 >= threshold) { return false; } }//|   after1 = VA12: false (0.0)
        else if(after1 == "VA13") { if(0 >= threshold) { return false; } }//|   after1 = VA13: false (0.0)
        else if(after1 == "VA3") { if(0 >= threshold) { return false; } }//|   after1 = VA3: false (0.0)
        else if(after1 == "VA4") { if(0 >= threshold) { return false; } }//|   after1 = VA4: false (0.0)
        else if(after1 == "VAC") { if(1 >= threshold) { return true; } }//|   after1 = VAC: true (1.0)
        else if(after1 == "VA2") { if(0 >= threshold) { return false; } }//|   after1 = VA2: false (0.0)
        else if(after1 == "VB") { if(0 >= threshold) { return false; } }//|   after1 = VB: false (0.0)
        else if(after1 == "VB11") { if(0 >= threshold) { return false; } }//|   after1 = VB11: false (0.0)
        else if(after1 == "VB12") { if(0 >= threshold) { return false; } }//|   after1 = VB12: false (0.0)
        else if(after1 == "VB2") { if(0 >= threshold) { return false; } }//|   after1 = VB2: false (0.0)
        else if(after1 == "VC") { if(0 >= threshold) { return false; } }//|   after1 = VC: false (0.0)
        else if(after1 == "VC2") { if(0 >= threshold) { return false; } }//|   after1 = VC2: false (0.0)
        else if(after1 == "VC31") { if(0 >= threshold) { return false; } }//|   after1 = VC31: false (0.0)
        else if(after1 == "VC32") { if(0 >= threshold) { return false; } }//|   after1 = VC32: false (0.0)
        else if(after1 == "VC33") { if(0 >= threshold) { return false; } }//|   after1 = VC33: false (0.0)
        else if(after1 == "VCL") { if(0 >= threshold) { return false; } }//|   after1 = VCL: false (0.0)
        else if(after1 == "VC1") { if(0 >= threshold) { return false; } }//|   after1 = VC1: false (0.0)
        else if(after1 == "VD") { if(0 >= threshold) { return false; } }//|   after1 = VD: false (0.0)
        else if(after1 == "VD1") { if(0 >= threshold) { return false; } }//|   after1 = VD1: false (0.0)
        else if(after1 == "VD2") { if(0 >= threshold) { return false; } }//|   after1 = VD2: false (0.0)
        else if(after1 == "VE") { if(0 >= threshold) { return false; } }//|   after1 = VE: false (0.0)
        else if(after1 == "VE11") { if(0 >= threshold) { return false; } }//|   after1 = VE11: false (0.0)
        else if(after1 == "VE12") { if(0 >= threshold) { return false; } }//|   after1 = VE12: false (0.0)
        else if(after1 == "VE2") { if(0 >= threshold) { return false; } }//|   after1 = VE2: false (0.0)
        else if(after1 == "VF") { if(0 >= threshold) { return false; } }//|   after1 = VF: false (0.0)
        else if(after1 == "VF1") { if(0 >= threshold) { return false; } }//|   after1 = VF1: false (0.0)
        else if(after1 == "VF2") { if(0 >= threshold) { return false; } }//|   after1 = VF2: false (0.0)
        else if(after1 == "VG") { if(1 >= threshold) { return false; } }//|   after1 = VG: false (1.0)
        else if(after1 == "VG1") { if(0 >= threshold) { return false; } }//|   after1 = VG1: false (0.0)
        else if(after1 == "VG2") { if(0 >= threshold) { return false; } }//|   after1 = VG2: false (0.0)
        else if(after1 == "VH") { if(0.75 >= threshold) { return false; } }//|   after1 = VH: false (3.0/1.0)
        else if(after1 == "VH11") { if(0 >= threshold) { return false; } }//|   after1 = VH11: false (0.0)
        else if(after1 == "VH12") { if(0 >= threshold) { return false; } }//|   after1 = VH12: false (0.0)
        else if(after1 == "VH13") { if(0 >= threshold) { return false; } }//|   after1 = VH13: false (0.0)
        else if(after1 == "VH14") { if(0 >= threshold) { return false; } }//|   after1 = VH14: false (0.0)
        else if(after1 == "VH15") { if(0 >= threshold) { return false; } }//|   after1 = VH15: false (0.0)
        else if(after1 == "VH17") { if(0 >= threshold) { return false; } }//|   after1 = VH17: false (0.0)
        else if(after1 == "VH21") { if(0 >= threshold) { return false; } }//|   after1 = VH21: false (0.0)
        else if(after1 == "VHC") { if(0 >= threshold) { return false; } }//|   after1 = VHC: false (0.0)
        else if(after1 == "VH16") { if(0 >= threshold) { return false; } }//|   after1 = VH16: false (0.0)
        else if(after1 == "VH22") { if(0 >= threshold) { return false; } }//|   after1 = VH22: false (0.0)
        else if(after1 == "VI") { if(0 >= threshold) { return false; } }//|   after1 = VI: false (0.0)
        else if(after1 == "VI1") { if(0 >= threshold) { return false; } }//|   after1 = VI1: false (0.0)
        else if(after1 == "VI2") { if(0 >= threshold) { return false; } }//|   after1 = VI2: false (0.0)
        else if(after1 == "VI3") { if(0 >= threshold) { return false; } }//|   after1 = VI3: false (0.0)
        else if(after1 == "VJ") { if(0 >= threshold) { return false; } }//|   after1 = VJ: false (0.0)
        else if(after1 == "VJ1") { if(0 >= threshold) { return false; } }//|   after1 = VJ1: false (0.0)
        else if(after1 == "VJ2") { if(0 >= threshold) { return false; } }//|   after1 = VJ2: false (0.0)
        else if(after1 == "VJ3") { if(0 >= threshold) { return false; } }//|   after1 = VJ3: false (0.0)
        else if(after1 == "VK") { if(0 >= threshold) { return false; } }//|   after1 = VK: false (0.0)
        else if(after1 == "VK1") { if(0 >= threshold) { return false; } }//|   after1 = VK1: false (0.0)
        else if(after1 == "VK2") { if(0 >= threshold) { return false; } }//|   after1 = VK2: false (0.0)
        else if(after1 == "VL") { if(0 >= threshold) { return false; } }//|   after1 = VL: false (0.0)
        else if(after1 == "VL1") { if(0 >= threshold) { return false; } }//|   after1 = VL1: false (0.0)
        else if(after1 == "VL2") { if(0 >= threshold) { return false; } }//|   after1 = VL2: false (0.0)
        else if(after1 == "VL3") { if(0 >= threshold) { return false; } }//|   after1 = VL3: false (0.0)
        else if(after1 == "VL4") { if(0 >= threshold) { return false; } }//|   after1 = VL4: false (0.0)
        else if(after1 == "V_2") { if(0 >= threshold) { return false; } }//|   after1 = V_2: false (0.0)
        else if(after1 == "Nv") { if(0 >= threshold) { return false; } }//|   after1 = Nv: false (0.0)
        else if(after1 == "Nv1") { if(0 >= threshold) { return false; } }//|   after1 = Nv1: false (0.0)
        else if(after1 == "Nv2") { if(0 >= threshold) { return false; } }//|   after1 = Nv2: false (0.0)
        else if(after1 == "Nv3") { if(0 >= threshold) { return false; } }//|   after1 = Nv3: false (0.0)
        else if(after1 == "Nv4") { if(0 >= threshold) { return false; } }//|   after1 = Nv4: false (0.0)
        else if(after1 == "b") { if(0 >= threshold) { return false; } }//|   after1 = b: false (0.0)
        else if(after1 == "notword") { if(1 >= threshold) { return true; } }//|   after1 = notword: true (4.0)
        else if(after1 == "begin") { if(0 >= threshold) { return false; } }//|   after1 = begin: false (0.0)
        else if(after1 == "end") { if(0 >= threshold) { return false; } }//|   after1 = end: false (0.0)
      }
      else if(front1 == "VI1") { if(0 >= threshold) { return true; } }//front1 = VI1: true (0.0)
      else if(front1 == "VI2") { if(0 >= threshold) { return true; } }//front1 = VI2: true (0.0)
      else if(front1 == "VI3") { if(0 >= threshold) { return true; } }//front1 = VI3: true (0.0)
      else if(front1 == "VJ")//front1 = VJ
      {
        if(after1 == "A") { if(0.85 >= threshold) { return false; } }//|   after1 = A: false (68.0/12.0)
        else if(after1 == "Caa") { if(0.814159292035398 >= threshold) { return false; } }//|   after1 = Caa: false (92.0/21.0)
        else if(after1 == "Cab") { if(1 >= threshold) { return false; } }//|   after1 = Cab: false (3.0)
        else if(after1 == "Cba") { if(0 >= threshold) { return false; } }//|   after1 = Cba: false (0.0)
        else if(after1 == "Cbab") { if(0 >= threshold) { return false; } }//|   after1 = Cbab: false (0.0)
        else if(after1 == "Cbb") { if(0.875 >= threshold) { return false; } }//|   after1 = Cbb: false (21.0/3.0)
        else if(after1 == "Cbaa") { if(0 >= threshold) { return false; } }//|   after1 = Cbaa: false (0.0)
        else if(after1 == "Cbba") { if(0 >= threshold) { return false; } }//|   after1 = Cbba: false (0.0)
        else if(after1 == "Cbbb") { if(0 >= threshold) { return false; } }//|   after1 = Cbbb: false (0.0)
        else if(after1 == "Cbca") { if(0 >= threshold) { return false; } }//|   after1 = Cbca: false (0.0)
        else if(after1 == "Cbcb") { if(0 >= threshold) { return false; } }//|   after1 = Cbcb: false (0.0)
        else if(after1 == "D") { if(0.784574468085106 >= threshold) { return false; } }//|   after1 = D: false (295.0/81.0)
        else if(after1 == "Dab") { if(0 >= threshold) { return false; } }//|   after1 = Dab: false (0.0)
        else if(after1 == "Dbaa") { if(0 >= threshold) { return false; } }//|   after1 = Dbaa: false (0.0)
        else if(after1 == "Dbab") { if(0 >= threshold) { return false; } }//|   after1 = Dbab: false (0.0)
        else if(after1 == "Dbb") { if(0 >= threshold) { return false; } }//|   after1 = Dbb: false (0.0)
        else if(after1 == "Dbc") { if(0 >= threshold) { return false; } }//|   after1 = Dbc: false (0.0)
        else if(after1 == "Dc") { if(0 >= threshold) { return false; } }//|   after1 = Dc: false (0.0)
        else if(after1 == "Dd") { if(0 >= threshold) { return false; } }//|   after1 = Dd: false (0.0)
        else if(after1 == "Dg") { if(0 >= threshold) { return false; } }//|   after1 = Dg: false (0.0)
        else if(after1 == "Dh") { if(0 >= threshold) { return false; } }//|   after1 = Dh: false (0.0)
        else if(after1 == "Dj") { if(0 >= threshold) { return false; } }//|   after1 = Dj: false (0.0)
        else if(after1 == "Da") { if(0.774193548387097 >= threshold) { return false; } }//|   after1 = Da: false (24.0/7.0)
        else if(after1 == "Daa") { if(0 >= threshold) { return false; } }//|   after1 = Daa: false (0.0)
        else if(after1 == "DE") { if(0.784651992861392 >= threshold) { return false; } }//|   after1 = DE: false (1319.0/362.0)
        else if(after1 == "Dfa") { if(0.806451612903226 >= threshold) { return false; } }//|   after1 = Dfa: false (50.0/12.0)
        else if(after1 == "Dfb") { if(0 >= threshold) { return false; } }//|   after1 = Dfb: false (0.0)
        else if(after1 == "Di") { if(1 >= threshold) { return false; } }//|   after1 = Di: false (1.0)
        else if(after1 == "Dk") { if(0 >= threshold) { return false; } }//|   after1 = Dk: false (0.0)
        else if(after1 == "FW") { if(0.904761904761905 >= threshold) { return false; } }//|   after1 = FW: false (38.0/4.0)
        else if(after1 == "I") { if(1 >= threshold) { return false; } }//|   after1 = I: false (2.0)
        else if(after1 == "Na") { if(0.886421550782672 >= threshold) { return false; } }//|   after1 = Na: false (2435.0/312.0)
        else if(after1 == "Naa") { if(0 >= threshold) { return false; } }//|   after1 = Naa: false (0.0)
        else if(after1 == "Nab") { if(0 >= threshold) { return false; } }//|   after1 = Nab: false (0.0)
        else if(after1 == "Nac") { if(0 >= threshold) { return false; } }//|   after1 = Nac: false (0.0)
        else if(after1 == "Nad") { if(0 >= threshold) { return false; } }//|   after1 = Nad: false (0.0)
        else if(after1 == "Naea") { if(0 >= threshold) { return false; } }//|   after1 = Naea: false (0.0)
        else if(after1 == "Naeb") { if(0 >= threshold) { return false; } }//|   after1 = Naeb: false (0.0)
        else if(after1 == "Nb")//|   after1 = Nb
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (8.0)
          else if(moreNeu == false) { if(0.682352941176471 >= threshold) { return true; } }//|   |   moreNeu = false: true (58.0/27.0)
        }
        else if(after1 == "Nba") { if(0 >= threshold) { return false; } }//|   after1 = Nba: false (0.0)
        else if(after1 == "Nbc") { if(0 >= threshold) { return false; } }//|   after1 = Nbc: false (0.0)
        else if(after1 == "Nc") { if(0.755263157894737 >= threshold) { return false; } }//|   after1 = Nc: false (287.0/93.0)
        else if(after1 == "Nca") { if(0 >= threshold) { return false; } }//|   after1 = Nca: false (0.0)
        else if(after1 == "Ncb") { if(1 >= threshold) { return false; } }//|   after1 = Ncb: false (1.0)
        else if(after1 == "Ncc") { if(0 >= threshold) { return false; } }//|   after1 = Ncc: false (0.0)
        else if(after1 == "Nce") { if(0 >= threshold) { return false; } }//|   after1 = Nce: false (0.0)
        else if(after1 == "Ncd") { if(0.818181818181818 >= threshold) { return true; } }//|   after1 = Ncd: true (9.0/2.0)
        else if(after1 == "Ncda") { if(0 >= threshold) { return false; } }//|   after1 = Ncda: false (0.0)
        else if(after1 == "Ncdb") { if(0 >= threshold) { return false; } }//|   after1 = Ncdb: false (0.0)
        else if(after1 == "Nd")//|   after1 = Nd
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (2.0)
          else if(moreNeu == false) { if(0.865497076023392 >= threshold) { return true; } }//|   |   moreNeu = false: true (148.0/23.0)
        }
        else if(after1 == "Ndaa") { if(0 >= threshold) { return false; } }//|   after1 = Ndaa: false (0.0)
        else if(after1 == "Ndab") { if(0 >= threshold) { return false; } }//|   after1 = Ndab: false (0.0)
        else if(after1 == "Ndc") { if(0 >= threshold) { return false; } }//|   after1 = Ndc: false (0.0)
        else if(after1 == "Ndd") { if(0 >= threshold) { return false; } }//|   after1 = Ndd: false (0.0)
        else if(after1 == "Nep") { if(0.777777777777778 >= threshold) { return true; } }//|   after1 = Nep: true (21.0/6.0)
        else if(after1 == "Neqa") { if(0.716981132075472 >= threshold) { return false; } }//|   after1 = Neqa: false (38.0/15.0)
        else if(after1 == "Neqb") { if(0.989304812834225 >= threshold) { return false; } }//|   after1 = Neqb: false (185.0/2.0)
        else if(after1 == "Nes") { if(0.793103448275862 >= threshold) { return true; } }//|   after1 = Nes: true (23.0/6.0)
        else if(after1 == "Neu") { if(0.699453551912568 >= threshold) { return false; } }//|   after1 = Neu: false (128.0/55.0)
        else if(after1 == "Nf") { if(0.88 >= threshold) { return false; } }//|   after1 = Nf: false (22.0/3.0)
        else if(after1 == "Nfa") { if(0 >= threshold) { return false; } }//|   after1 = Nfa: false (0.0)
        else if(after1 == "Nfb") { if(0 >= threshold) { return false; } }//|   after1 = Nfb: false (0.0)
        else if(after1 == "Nfc") { if(0 >= threshold) { return false; } }//|   after1 = Nfc: false (0.0)
        else if(after1 == "Nfd") { if(0 >= threshold) { return false; } }//|   after1 = Nfd: false (0.0)
        else if(after1 == "Nfe") { if(0 >= threshold) { return false; } }//|   after1 = Nfe: false (0.0)
        else if(after1 == "Nfg") { if(0 >= threshold) { return false; } }//|   after1 = Nfg: false (0.0)
        else if(after1 == "Nfh") { if(0 >= threshold) { return false; } }//|   after1 = Nfh: false (0.0)
        else if(after1 == "Nfi") { if(0 >= threshold) { return false; } }//|   after1 = Nfi: false (0.0)
        else if(after1 == "Ng") { if(0.837037037037037 >= threshold) { return false; } }//|   after1 = Ng: false (339.0/66.0)
        else if(after1 == "Nh") { if(0.67741935483871 >= threshold) { return true; } }//|   after1 = Nh: true (42.0/20.0)
        else if(after1 == "Nhaa") { if(0 >= threshold) { return false; } }//|   after1 = Nhaa: false (0.0)
        else if(after1 == "Nhab") { if(0 >= threshold) { return false; } }//|   after1 = Nhab: false (0.0)
        else if(after1 == "Nhac") { if(0 >= threshold) { return false; } }//|   after1 = Nhac: false (0.0)
        else if(after1 == "Nhb") { if(0 >= threshold) { return false; } }//|   after1 = Nhb: false (0.0)
        else if(after1 == "Nhc") { if(0 >= threshold) { return false; } }//|   after1 = Nhc: false (0.0)
        else if(after1 == "P") { if(0.722222222222222 >= threshold) { return false; } }//|   after1 = P: false (91.0/35.0)
        else if(after1 == "SHI") { if(0.882352941176471 >= threshold) { return false; } }//|   after1 = SHI: false (30.0/4.0)
        else if(after1 == "V_11") { if(0 >= threshold) { return false; } }//|   after1 = V_11: false (0.0)
        else if(after1 == "T") { if(0.78 >= threshold) { return false; } }//|   after1 = T: false (78.0/22.0)
        else if(after1 == "Ta") { if(0 >= threshold) { return false; } }//|   after1 = Ta: false (0.0)
        else if(after1 == "Tb") { if(0 >= threshold) { return false; } }//|   after1 = Tb: false (0.0)
        else if(after1 == "Tc") { if(0 >= threshold) { return false; } }//|   after1 = Tc: false (0.0)
        else if(after1 == "Td") { if(0 >= threshold) { return false; } }//|   after1 = Td: false (0.0)
        else if(after1 == "VA") { if(0.758333333333333 >= threshold) { return false; } }//|   after1 = VA: false (91.0/29.0)
        else if(after1 == "VA11") { if(0 >= threshold) { return false; } }//|   after1 = VA11: false (0.0)
        else if(after1 == "VA12") { if(0 >= threshold) { return false; } }//|   after1 = VA12: false (0.0)
        else if(after1 == "VA13") { if(0 >= threshold) { return false; } }//|   after1 = VA13: false (0.0)
        else if(after1 == "VA3") { if(0 >= threshold) { return false; } }//|   after1 = VA3: false (0.0)
        else if(after1 == "VA4") { if(0 >= threshold) { return false; } }//|   after1 = VA4: false (0.0)
        else if(after1 == "VAC") { if(0 >= threshold) { return false; } }//|   after1 = VAC: false (0.0)
        else if(after1 == "VA2") { if(0 >= threshold) { return false; } }//|   after1 = VA2: false (0.0)
        else if(after1 == "VB") { if(0.857142857142857 >= threshold) { return false; } }//|   after1 = VB: false (6.0/1.0)
        else if(after1 == "VB11") { if(0 >= threshold) { return false; } }//|   after1 = VB11: false (0.0)
        else if(after1 == "VB12") { if(0 >= threshold) { return false; } }//|   after1 = VB12: false (0.0)
        else if(after1 == "VB2") { if(0 >= threshold) { return false; } }//|   after1 = VB2: false (0.0)
        else if(after1 == "VC") { if(0.762645914396887 >= threshold) { return false; } }//|   after1 = VC: false (196.0/61.0)
        else if(after1 == "VC2") { if(0 >= threshold) { return false; } }//|   after1 = VC2: false (0.0)
        else if(after1 == "VC31") { if(0 >= threshold) { return false; } }//|   after1 = VC31: false (0.0)
        else if(after1 == "VC32") { if(0 >= threshold) { return false; } }//|   after1 = VC32: false (0.0)
        else if(after1 == "VC33") { if(0 >= threshold) { return false; } }//|   after1 = VC33: false (0.0)
        else if(after1 == "VCL") { if(0.8 >= threshold) { return false; } }//|   after1 = VCL: false (16.0/4.0)
        else if(after1 == "VC1") { if(0 >= threshold) { return false; } }//|   after1 = VC1: false (0.0)
        else if(after1 == "VD") { if(0.916666666666667 >= threshold) { return false; } }//|   after1 = VD: false (22.0/2.0)
        else if(after1 == "VD1") { if(0 >= threshold) { return false; } }//|   after1 = VD1: false (0.0)
        else if(after1 == "VD2") { if(0 >= threshold) { return false; } }//|   after1 = VD2: false (0.0)
        else if(after1 == "VE") { if(0.704545454545455 >= threshold) { return false; } }//|   after1 = VE: false (31.0/13.0)
        else if(after1 == "VE11") { if(0 >= threshold) { return false; } }//|   after1 = VE11: false (0.0)
        else if(after1 == "VE12") { if(0 >= threshold) { return false; } }//|   after1 = VE12: false (0.0)
        else if(after1 == "VE2") { if(0 >= threshold) { return false; } }//|   after1 = VE2: false (0.0)
        else if(after1 == "VF") { if(0.875 >= threshold) { return false; } }//|   after1 = VF: false (7.0/1.0)
        else if(after1 == "VF1") { if(0 >= threshold) { return false; } }//|   after1 = VF1: false (0.0)
        else if(after1 == "VF2") { if(0 >= threshold) { return false; } }//|   after1 = VF2: false (0.0)
        else if(after1 == "VG") { if(0.866666666666667 >= threshold) { return false; } }//|   after1 = VG: false (39.0/6.0)
        else if(after1 == "VG1") { if(0 >= threshold) { return false; } }//|   after1 = VG1: false (0.0)
        else if(after1 == "VG2") { if(0 >= threshold) { return false; } }//|   after1 = VG2: false (0.0)
        else if(after1 == "VH") { if(0.861924686192469 >= threshold) { return false; } }//|   after1 = VH: false (618.0/99.0)
        else if(after1 == "VH11") { if(0 >= threshold) { return false; } }//|   after1 = VH11: false (0.0)
        else if(after1 == "VH12") { if(0 >= threshold) { return false; } }//|   after1 = VH12: false (0.0)
        else if(after1 == "VH13") { if(0 >= threshold) { return false; } }//|   after1 = VH13: false (0.0)
        else if(after1 == "VH14") { if(0 >= threshold) { return false; } }//|   after1 = VH14: false (0.0)
        else if(after1 == "VH15") { if(0 >= threshold) { return false; } }//|   after1 = VH15: false (0.0)
        else if(after1 == "VH17") { if(0 >= threshold) { return false; } }//|   after1 = VH17: false (0.0)
        else if(after1 == "VH21") { if(0 >= threshold) { return false; } }//|   after1 = VH21: false (0.0)
        else if(after1 == "VHC") { if(0.730769230769231 >= threshold) { return false; } }//|   after1 = VHC: false (38.0/14.0)
        else if(after1 == "VH16") { if(0 >= threshold) { return false; } }//|   after1 = VH16: false (0.0)
        else if(after1 == "VH22") { if(0 >= threshold) { return false; } }//|   after1 = VH22: false (0.0)
        else if(after1 == "VI") { if(1 >= threshold) { return false; } }//|   after1 = VI: false (7.0)
        else if(after1 == "VI1") { if(0 >= threshold) { return false; } }//|   after1 = VI1: false (0.0)
        else if(after1 == "VI2") { if(0 >= threshold) { return false; } }//|   after1 = VI2: false (0.0)
        else if(after1 == "VI3") { if(0 >= threshold) { return false; } }//|   after1 = VI3: false (0.0)
        else if(after1 == "VJ") { if(0.838709677419355 >= threshold) { return false; } }//|   after1 = VJ: false (78.0/15.0)
        else if(after1 == "VJ1") { if(0 >= threshold) { return false; } }//|   after1 = VJ1: false (0.0)
        else if(after1 == "VJ2") { if(0 >= threshold) { return false; } }//|   after1 = VJ2: false (0.0)
        else if(after1 == "VJ3") { if(0 >= threshold) { return false; } }//|   after1 = VJ3: false (0.0)
        else if(after1 == "VK") { if(0.794117647058823 >= threshold) { return false; } }//|   after1 = VK: false (27.0/7.0)
        else if(after1 == "VK1") { if(0 >= threshold) { return false; } }//|   after1 = VK1: false (0.0)
        else if(after1 == "VK2") { if(0 >= threshold) { return false; } }//|   after1 = VK2: false (0.0)
        else if(after1 == "VL") { if(0.91304347826087 >= threshold) { return false; } }//|   after1 = VL: false (21.0/2.0)
        else if(after1 == "VL1") { if(0 >= threshold) { return false; } }//|   after1 = VL1: false (0.0)
        else if(after1 == "VL2") { if(0 >= threshold) { return false; } }//|   after1 = VL2: false (0.0)
        else if(after1 == "VL3") { if(0 >= threshold) { return false; } }//|   after1 = VL3: false (0.0)
        else if(after1 == "VL4") { if(0 >= threshold) { return false; } }//|   after1 = VL4: false (0.0)
        else if(after1 == "V_2") { if(0.823529411764706 >= threshold) { return false; } }//|   after1 = V_2: false (14.0/3.0)
        else if(after1 == "Nv") { if(0.835051546391753 >= threshold) { return false; } }//|   after1 = Nv: false (81.0/16.0)
        else if(after1 == "Nv1") { if(0 >= threshold) { return false; } }//|   after1 = Nv1: false (0.0)
        else if(after1 == "Nv2") { if(0 >= threshold) { return false; } }//|   after1 = Nv2: false (0.0)
        else if(after1 == "Nv3") { if(0 >= threshold) { return false; } }//|   after1 = Nv3: false (0.0)
        else if(after1 == "Nv4") { if(0 >= threshold) { return false; } }//|   after1 = Nv4: false (0.0)
        else if(after1 == "b") { if(0 >= threshold) { return false; } }//|   after1 = b: false (0.0)
        else if(after1 == "notword") { if(0.890726681127983 >= threshold) { return false; } }//|   after1 = notword: false (3285.0/403.0)
        else if(after1 == "begin") { if(0 >= threshold) { return false; } }//|   after1 = begin: false (0.0)
        else if(after1 == "end") { if(0 >= threshold) { return false; } }//|   after1 = end: false (0.0)
      }
      else if(front1 == "VJ1") { if(0 >= threshold) { return true; } }//front1 = VJ1: true (0.0)
      else if(front1 == "VJ2") { if(0 >= threshold) { return true; } }//front1 = VJ2: true (0.0)
      else if(front1 == "VJ3") { if(0 >= threshold) { return true; } }//front1 = VJ3: true (0.0)
      else if(front1 == "VK")//front1 = VK
      {
        if(after1 == "A") { if(0.826086956521739 >= threshold) { return false; } }//|   after1 = A: false (19.0/4.0)
        else if(after1 == "Caa") { if(0.75 >= threshold) { return true; } }//|   after1 = Caa: true (27.0/9.0)
        else if(after1 == "Cab") { if(1 >= threshold) { return false; } }//|   after1 = Cab: false (1.0)
        else if(after1 == "Cba") { if(0 >= threshold) { return true; } }//|   after1 = Cba: true (0.0)
        else if(after1 == "Cbab") { if(0 >= threshold) { return true; } }//|   after1 = Cbab: true (0.0)
        else if(after1 == "Cbb") { if(0.75 >= threshold) { return true; } }//|   after1 = Cbb: true (6.0/2.0)
        else if(after1 == "Cbaa") { if(0 >= threshold) { return true; } }//|   after1 = Cbaa: true (0.0)
        else if(after1 == "Cbba") { if(0 >= threshold) { return true; } }//|   after1 = Cbba: true (0.0)
        else if(after1 == "Cbbb") { if(0 >= threshold) { return true; } }//|   after1 = Cbbb: true (0.0)
        else if(after1 == "Cbca") { if(0 >= threshold) { return true; } }//|   after1 = Cbca: true (0.0)
        else if(after1 == "Cbcb") { if(0 >= threshold) { return true; } }//|   after1 = Cbcb: true (0.0)
        else if(after1 == "D")//|   after1 = D
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (3.0)
          else if(moreNeu == false) { if(0.809815950920245 >= threshold) { return true; } }//|   |   moreNeu = false: true (264.0/62.0)
        }
        else if(after1 == "Dab") { if(0 >= threshold) { return true; } }//|   after1 = Dab: true (0.0)
        else if(after1 == "Dbaa") { if(0 >= threshold) { return true; } }//|   after1 = Dbaa: true (0.0)
        else if(after1 == "Dbab") { if(0 >= threshold) { return true; } }//|   after1 = Dbab: true (0.0)
        else if(after1 == "Dbb") { if(0 >= threshold) { return true; } }//|   after1 = Dbb: true (0.0)
        else if(after1 == "Dbc") { if(0 >= threshold) { return true; } }//|   after1 = Dbc: true (0.0)
        else if(after1 == "Dc") { if(0 >= threshold) { return true; } }//|   after1 = Dc: true (0.0)
        else if(after1 == "Dd") { if(0 >= threshold) { return true; } }//|   after1 = Dd: true (0.0)
        else if(after1 == "Dg") { if(0 >= threshold) { return true; } }//|   after1 = Dg: true (0.0)
        else if(after1 == "Dh") { if(0 >= threshold) { return true; } }//|   after1 = Dh: true (0.0)
        else if(after1 == "Dj") { if(0 >= threshold) { return true; } }//|   after1 = Dj: true (0.0)
        else if(after1 == "Da") { if(0.7 >= threshold) { return true; } }//|   after1 = Da: true (14.0/6.0)
        else if(after1 == "Daa") { if(0 >= threshold) { return true; } }//|   after1 = Daa: true (0.0)
        else if(after1 == "DE")//|   after1 = DE
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (9.0)
          else if(moreNeu == false) { if(0.865102639296188 >= threshold) { return true; } }//|   |   moreNeu = false: true (295.0/46.0)
        }
        else if(after1 == "Dfa") { if(0.770833333333333 >= threshold) { return false; } }//|   after1 = Dfa: false (37.0/11.0)
        else if(after1 == "Dfb") { if(0 >= threshold) { return true; } }//|   after1 = Dfb: true (0.0)
        else if(after1 == "Di") { if(0 >= threshold) { return true; } }//|   after1 = Di: true (0.0)
        else if(after1 == "Dk") { if(0 >= threshold) { return true; } }//|   after1 = Dk: true (0.0)
        else if(after1 == "FW") { if(0.666666666666667 >= threshold) { return true; } }//|   after1 = FW: true (6.0/3.0)
        else if(after1 == "I") { if(0 >= threshold) { return true; } }//|   after1 = I: true (0.0)
        else if(after1 == "Na") { if(0.763500931098696 >= threshold) { return false; } }//|   after1 = Na: false (820.0/254.0)
        else if(after1 == "Naa") { if(0 >= threshold) { return true; } }//|   after1 = Naa: true (0.0)
        else if(after1 == "Nab") { if(0 >= threshold) { return true; } }//|   after1 = Nab: true (0.0)
        else if(after1 == "Nac") { if(0 >= threshold) { return true; } }//|   after1 = Nac: true (0.0)
        else if(after1 == "Nad") { if(0 >= threshold) { return true; } }//|   after1 = Nad: true (0.0)
        else if(after1 == "Naea") { if(0 >= threshold) { return true; } }//|   after1 = Naea: true (0.0)
        else if(after1 == "Naeb") { if(0 >= threshold) { return true; } }//|   after1 = Naeb: true (0.0)
        else if(after1 == "Nb") { if(0.857142857142857 >= threshold) { return true; } }//|   after1 = Nb: true (30.0/5.0)
        else if(after1 == "Nba") { if(0 >= threshold) { return true; } }//|   after1 = Nba: true (0.0)
        else if(after1 == "Nbc") { if(0 >= threshold) { return true; } }//|   after1 = Nbc: true (0.0)
        else if(after1 == "Nc") { if(0.742424242424242 >= threshold) { return true; } }//|   after1 = Nc: true (147.0/51.0)
        else if(after1 == "Nca") { if(0 >= threshold) { return true; } }//|   after1 = Nca: true (0.0)
        else if(after1 == "Ncb") { if(0 >= threshold) { return true; } }//|   after1 = Ncb: true (0.0)
        else if(after1 == "Ncc") { if(0 >= threshold) { return true; } }//|   after1 = Ncc: true (0.0)
        else if(after1 == "Nce") { if(0 >= threshold) { return true; } }//|   after1 = Nce: true (0.0)
        else if(after1 == "Ncd") { if(0.692307692307692 >= threshold) { return true; } }//|   after1 = Ncd: true (9.0/4.0)
        else if(after1 == "Ncda") { if(0 >= threshold) { return true; } }//|   after1 = Ncda: true (0.0)
        else if(after1 == "Ncdb") { if(0 >= threshold) { return true; } }//|   after1 = Ncdb: true (0.0)
        else if(after1 == "Nd") { if(0.954022988505747 >= threshold) { return true; } }//|   after1 = Nd: true (83.0/4.0)
        else if(after1 == "Ndaa") { if(0 >= threshold) { return true; } }//|   after1 = Ndaa: true (0.0)
        else if(after1 == "Ndab") { if(0 >= threshold) { return true; } }//|   after1 = Ndab: true (0.0)
        else if(after1 == "Ndc") { if(0 >= threshold) { return true; } }//|   after1 = Ndc: true (0.0)
        else if(after1 == "Ndd") { if(0 >= threshold) { return true; } }//|   after1 = Ndd: true (0.0)
        else if(after1 == "Nep") { if(0.825 >= threshold) { return true; } }//|   after1 = Nep: true (33.0/7.0)
        else if(after1 == "Neqa") { if(0.785714285714286 >= threshold) { return true; } }//|   after1 = Neqa: true (22.0/6.0)
        else if(after1 == "Neqb") { if(1 >= threshold) { return false; } }//|   after1 = Neqb: false (16.0)
        else if(after1 == "Nes") { if(0.866666666666667 >= threshold) { return true; } }//|   after1 = Nes: true (13.0/2.0)
        else if(after1 == "Neu") { if(0.688311688311688 >= threshold) { return false; } }//|   after1 = Neu: false (53.0/24.0)
        else if(after1 == "Nf") { if(0.75 >= threshold) { return false; } }//|   after1 = Nf: false (3.0/1.0)
        else if(after1 == "Nfa") { if(0 >= threshold) { return true; } }//|   after1 = Nfa: true (0.0)
        else if(after1 == "Nfb") { if(0 >= threshold) { return true; } }//|   after1 = Nfb: true (0.0)
        else if(after1 == "Nfc") { if(0 >= threshold) { return true; } }//|   after1 = Nfc: true (0.0)
        else if(after1 == "Nfd") { if(0 >= threshold) { return true; } }//|   after1 = Nfd: true (0.0)
        else if(after1 == "Nfe") { if(0 >= threshold) { return true; } }//|   after1 = Nfe: true (0.0)
        else if(after1 == "Nfg") { if(0 >= threshold) { return true; } }//|   after1 = Nfg: true (0.0)
        else if(after1 == "Nfh") { if(0 >= threshold) { return true; } }//|   after1 = Nfh: true (0.0)
        else if(after1 == "Nfi") { if(0 >= threshold) { return true; } }//|   after1 = Nfi: true (0.0)
        else if(after1 == "Ng") { if(0.730538922155689 >= threshold) { return false; } }//|   after1 = Ng: false (122.0/45.0)
        else if(after1 == "Nh")//|   after1 = Nh
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (2.0)
          else if(moreNeu == false) { if(0.953125 >= threshold) { return true; } }//|   |   moreNeu = false: true (61.0/3.0)
        }
        else if(after1 == "Nhaa") { if(0 >= threshold) { return true; } }//|   after1 = Nhaa: true (0.0)
        else if(after1 == "Nhab") { if(0 >= threshold) { return true; } }//|   after1 = Nhab: true (0.0)
        else if(after1 == "Nhac") { if(0 >= threshold) { return true; } }//|   after1 = Nhac: true (0.0)
        else if(after1 == "Nhb") { if(0 >= threshold) { return true; } }//|   after1 = Nhb: true (0.0)
        else if(after1 == "Nhc") { if(0 >= threshold) { return true; } }//|   after1 = Nhc: true (0.0)
        else if(after1 == "P")//|   after1 = P
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (9.0)
          else if(moreNeu == false) { if(0.829787234042553 >= threshold) { return true; } }//|   |   moreNeu = false: true (78.0/16.0)
        }
        else if(after1 == "SHI") { if(0.942857142857143 >= threshold) { return true; } }//|   after1 = SHI: true (33.0/2.0)
        else if(after1 == "V_11") { if(0 >= threshold) { return true; } }//|   after1 = V_11: true (0.0)
        else if(after1 == "T") { if(0.764705882352941 >= threshold) { return true; } }//|   after1 = T: true (13.0/4.0)
        else if(after1 == "Ta") { if(0 >= threshold) { return true; } }//|   after1 = Ta: true (0.0)
        else if(after1 == "Tb") { if(0 >= threshold) { return true; } }//|   after1 = Tb: true (0.0)
        else if(after1 == "Tc") { if(0 >= threshold) { return true; } }//|   after1 = Tc: true (0.0)
        else if(after1 == "Td") { if(0 >= threshold) { return true; } }//|   after1 = Td: true (0.0)
        else if(after1 == "VA")//|   after1 = VA
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (3.0)
          else if(moreNeu == false) { if(0.775 >= threshold) { return true; } }//|   |   moreNeu = false: true (62.0/18.0)
        }
        else if(after1 == "VA11") { if(0 >= threshold) { return true; } }//|   after1 = VA11: true (0.0)
        else if(after1 == "VA12") { if(0 >= threshold) { return true; } }//|   after1 = VA12: true (0.0)
        else if(after1 == "VA13") { if(0 >= threshold) { return true; } }//|   after1 = VA13: true (0.0)
        else if(after1 == "VA3") { if(0 >= threshold) { return true; } }//|   after1 = VA3: true (0.0)
        else if(after1 == "VA4") { if(0 >= threshold) { return true; } }//|   after1 = VA4: true (0.0)
        else if(after1 == "VAC") { if(1 >= threshold) { return true; } }//|   after1 = VAC: true (1.0)
        else if(after1 == "VA2") { if(0 >= threshold) { return true; } }//|   after1 = VA2: true (0.0)
        else if(after1 == "VB") { if(1 >= threshold) { return true; } }//|   after1 = VB: true (2.0)
        else if(after1 == "VB11") { if(0 >= threshold) { return true; } }//|   after1 = VB11: true (0.0)
        else if(after1 == "VB12") { if(0 >= threshold) { return true; } }//|   after1 = VB12: true (0.0)
        else if(after1 == "VB2") { if(0 >= threshold) { return true; } }//|   after1 = VB2: true (0.0)
        else if(after1 == "VC")//|   after1 = VC
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (4.0)
          else if(moreNeu == false) { if(0.823008849557522 >= threshold) { return true; } }//|   |   moreNeu = false: true (93.0/20.0)
        }
        else if(after1 == "VC2") { if(0 >= threshold) { return true; } }//|   after1 = VC2: true (0.0)
        else if(after1 == "VC31") { if(0 >= threshold) { return true; } }//|   after1 = VC31: true (0.0)
        else if(after1 == "VC32") { if(0 >= threshold) { return true; } }//|   after1 = VC32: true (0.0)
        else if(after1 == "VC33") { if(0 >= threshold) { return true; } }//|   after1 = VC33: true (0.0)
        else if(after1 == "VCL")//|   after1 = VCL
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (5.0)
          else if(moreNeu == false) { if(0.823529411764706 >= threshold) { return true; } }//|   |   moreNeu = false: true (14.0/3.0)
        }
        else if(after1 == "VC1") { if(0 >= threshold) { return true; } }//|   after1 = VC1: true (0.0)
        else if(after1 == "VD") { if(1 >= threshold) { return true; } }//|   after1 = VD: true (3.0)
        else if(after1 == "VD1") { if(0 >= threshold) { return true; } }//|   after1 = VD1: true (0.0)
        else if(after1 == "VD2") { if(0 >= threshold) { return true; } }//|   after1 = VD2: true (0.0)
        else if(after1 == "VE")//|   after1 = VE
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (4.0)
          else if(moreNeu == false) { if(0.8 >= threshold) { return true; } }//|   |   moreNeu = false: true (12.0/3.0)
        }
        else if(after1 == "VE11") { if(0 >= threshold) { return true; } }//|   after1 = VE11: true (0.0)
        else if(after1 == "VE12") { if(0 >= threshold) { return true; } }//|   after1 = VE12: true (0.0)
        else if(after1 == "VE2") { if(0 >= threshold) { return true; } }//|   after1 = VE2: true (0.0)
        else if(after1 == "VF") { if(0.714285714285714 >= threshold) { return true; } }//|   after1 = VF: true (5.0/2.0)
        else if(after1 == "VF1") { if(0 >= threshold) { return true; } }//|   after1 = VF1: true (0.0)
        else if(after1 == "VF2") { if(0 >= threshold) { return true; } }//|   after1 = VF2: true (0.0)
        else if(after1 == "VG") { if(0.709677419354839 >= threshold) { return false; } }//|   after1 = VG: false (22.0/9.0)
        else if(after1 == "VG1") { if(0 >= threshold) { return true; } }//|   after1 = VG1: true (0.0)
        else if(after1 == "VG2") { if(0 >= threshold) { return true; } }//|   after1 = VG2: true (0.0)
        else if(after1 == "VH") { if(0.78021978021978 >= threshold) { return false; } }//|   after1 = VH: false (213.0/60.0)
        else if(after1 == "VH11") { if(0 >= threshold) { return true; } }//|   after1 = VH11: true (0.0)
        else if(after1 == "VH12") { if(0 >= threshold) { return true; } }//|   after1 = VH12: true (0.0)
        else if(after1 == "VH13") { if(0 >= threshold) { return true; } }//|   after1 = VH13: true (0.0)
        else if(after1 == "VH14") { if(0 >= threshold) { return true; } }//|   after1 = VH14: true (0.0)
        else if(after1 == "VH15") { if(0 >= threshold) { return true; } }//|   after1 = VH15: true (0.0)
        else if(after1 == "VH17") { if(0 >= threshold) { return true; } }//|   after1 = VH17: true (0.0)
        else if(after1 == "VH21") { if(0 >= threshold) { return true; } }//|   after1 = VH21: true (0.0)
        else if(after1 == "VHC") { if(0.764705882352941 >= threshold) { return false; } }//|   after1 = VHC: false (13.0/4.0)
        else if(after1 == "VH16") { if(0 >= threshold) { return true; } }//|   after1 = VH16: true (0.0)
        else if(after1 == "VH22") { if(0 >= threshold) { return true; } }//|   after1 = VH22: true (0.0)
        else if(after1 == "VI") { if(0.666666666666667 >= threshold) { return true; } }//|   after1 = VI: true (2.0/1.0)
        else if(after1 == "VI1") { if(0 >= threshold) { return true; } }//|   after1 = VI1: true (0.0)
        else if(after1 == "VI2") { if(0 >= threshold) { return true; } }//|   after1 = VI2: true (0.0)
        else if(after1 == "VI3") { if(0 >= threshold) { return true; } }//|   after1 = VI3: true (0.0)
        else if(after1 == "VJ") { if(0.714285714285714 >= threshold) { return true; } }//|   after1 = VJ: true (30.0/12.0)
        else if(after1 == "VJ1") { if(0 >= threshold) { return true; } }//|   after1 = VJ1: true (0.0)
        else if(after1 == "VJ2") { if(0 >= threshold) { return true; } }//|   after1 = VJ2: true (0.0)
        else if(after1 == "VJ3") { if(0 >= threshold) { return true; } }//|   after1 = VJ3: true (0.0)
        else if(after1 == "VK") { if(0.761904761904762 >= threshold) { return false; } }//|   after1 = VK: false (16.0/5.0)
        else if(after1 == "VK1") { if(0 >= threshold) { return true; } }//|   after1 = VK1: true (0.0)
        else if(after1 == "VK2") { if(0 >= threshold) { return true; } }//|   after1 = VK2: true (0.0)
        else if(after1 == "VL") { if(0.777777777777778 >= threshold) { return true; } }//|   after1 = VL: true (7.0/2.0)
        else if(after1 == "VL1") { if(0 >= threshold) { return true; } }//|   after1 = VL1: true (0.0)
        else if(after1 == "VL2") { if(0 >= threshold) { return true; } }//|   after1 = VL2: true (0.0)
        else if(after1 == "VL3") { if(0 >= threshold) { return true; } }//|   after1 = VL3: true (0.0)
        else if(after1 == "VL4") { if(0 >= threshold) { return true; } }//|   after1 = VL4: true (0.0)
        else if(after1 == "V_2") { if(0.928571428571429 >= threshold) { return true; } }//|   after1 = V_2: true (26.0/2.0)
        else if(after1 == "Nv") { if(0.821428571428571 >= threshold) { return false; } }//|   after1 = Nv: false (23.0/5.0)
        else if(after1 == "Nv1") { if(0 >= threshold) { return true; } }//|   after1 = Nv1: true (0.0)
        else if(after1 == "Nv2") { if(0 >= threshold) { return true; } }//|   after1 = Nv2: true (0.0)
        else if(after1 == "Nv3") { if(0 >= threshold) { return true; } }//|   after1 = Nv3: true (0.0)
        else if(after1 == "Nv4") { if(0 >= threshold) { return true; } }//|   after1 = Nv4: true (0.0)
        else if(after1 == "b") { if(0 >= threshold) { return true; } }//|   after1 = b: true (0.0)
        else if(after1 == "notword")//|   after1 = notword
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (9.0)
          else if(moreNeu == false) { if(0.68936170212766 >= threshold) { return true; } }//|   |   moreNeu = false: true (324.0/146.0)
        }
        else if(after1 == "begin") { if(0 >= threshold) { return true; } }//|   after1 = begin: true (0.0)
        else if(after1 == "end") { if(0 >= threshold) { return true; } }//|   after1 = end: true (0.0)
      }
      else if(front1 == "VK1") { if(0 >= threshold) { return true; } }//front1 = VK1: true (0.0)
      else if(front1 == "VK2") { if(0 >= threshold) { return true; } }//front1 = VK2: true (0.0)
      else if(front1 == "VL")//front1 = VL
      {
        if(after1 == "A") { if(0.8 >= threshold) { return false; } }//|   after1 = A: false (8.0/2.0)
        else if(after1 == "Caa") { if(0.818181818181818 >= threshold) { return false; } }//|   after1 = Caa: false (9.0/2.0)
        else if(after1 == "Cab") { if(0 >= threshold) { return false; } }//|   after1 = Cab: false (0.0)
        else if(after1 == "Cba") { if(0 >= threshold) { return false; } }//|   after1 = Cba: false (0.0)
        else if(after1 == "Cbab") { if(0 >= threshold) { return false; } }//|   after1 = Cbab: false (0.0)
        else if(after1 == "Cbb") { if(1 >= threshold) { return true; } }//|   after1 = Cbb: true (1.0)
        else if(after1 == "Cbaa") { if(0 >= threshold) { return false; } }//|   after1 = Cbaa: false (0.0)
        else if(after1 == "Cbba") { if(0 >= threshold) { return false; } }//|   after1 = Cbba: false (0.0)
        else if(after1 == "Cbbb") { if(0 >= threshold) { return false; } }//|   after1 = Cbbb: false (0.0)
        else if(after1 == "Cbca") { if(0 >= threshold) { return false; } }//|   after1 = Cbca: false (0.0)
        else if(after1 == "Cbcb") { if(0 >= threshold) { return false; } }//|   after1 = Cbcb: false (0.0)
        else if(after1 == "D") { if(0.74 >= threshold) { return true; } }//|   after1 = D: true (37.0/13.0)
        else if(after1 == "Dab") { if(0 >= threshold) { return false; } }//|   after1 = Dab: false (0.0)
        else if(after1 == "Dbaa") { if(0 >= threshold) { return false; } }//|   after1 = Dbaa: false (0.0)
        else if(after1 == "Dbab") { if(0 >= threshold) { return false; } }//|   after1 = Dbab: false (0.0)
        else if(after1 == "Dbb") { if(0 >= threshold) { return false; } }//|   after1 = Dbb: false (0.0)
        else if(after1 == "Dbc") { if(0 >= threshold) { return false; } }//|   after1 = Dbc: false (0.0)
        else if(after1 == "Dc") { if(0 >= threshold) { return false; } }//|   after1 = Dc: false (0.0)
        else if(after1 == "Dd") { if(0 >= threshold) { return false; } }//|   after1 = Dd: false (0.0)
        else if(after1 == "Dg") { if(0 >= threshold) { return false; } }//|   after1 = Dg: false (0.0)
        else if(after1 == "Dh") { if(0 >= threshold) { return false; } }//|   after1 = Dh: false (0.0)
        else if(after1 == "Dj") { if(0 >= threshold) { return false; } }//|   after1 = Dj: false (0.0)
        else if(after1 == "Da") { if(1 >= threshold) { return true; } }//|   after1 = Da: true (1.0)
        else if(after1 == "Daa") { if(0 >= threshold) { return false; } }//|   after1 = Daa: false (0.0)
        else if(after1 == "DE")//|   after1 = DE
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (3.0)
          else if(moreNeu == false) { if(0.806451612903226 >= threshold) { return true; } }//|   |   moreNeu = false: true (100.0/24.0)
        }
        else if(after1 == "Dfa") { if(1 >= threshold) { return true; } }//|   after1 = Dfa: true (1.0)
        else if(after1 == "Dfb") { if(0 >= threshold) { return false; } }//|   after1 = Dfb: false (0.0)
        else if(after1 == "Di") { if(0 >= threshold) { return false; } }//|   after1 = Di: false (0.0)
        else if(after1 == "Dk") { if(0 >= threshold) { return false; } }//|   after1 = Dk: false (0.0)
        else if(after1 == "FW") { if(1 >= threshold) { return false; } }//|   after1 = FW: false (1.0)
        else if(after1 == "I") { if(0 >= threshold) { return false; } }//|   after1 = I: false (0.0)
        else if(after1 == "Na") { if(0.786585365853659 >= threshold) { return false; } }//|   after1 = Na: false (258.0/70.0)
        else if(after1 == "Naa") { if(0 >= threshold) { return false; } }//|   after1 = Naa: false (0.0)
        else if(after1 == "Nab") { if(0 >= threshold) { return false; } }//|   after1 = Nab: false (0.0)
        else if(after1 == "Nac") { if(0 >= threshold) { return false; } }//|   after1 = Nac: false (0.0)
        else if(after1 == "Nad") { if(0 >= threshold) { return false; } }//|   after1 = Nad: false (0.0)
        else if(after1 == "Naea") { if(0 >= threshold) { return false; } }//|   after1 = Naea: false (0.0)
        else if(after1 == "Naeb") { if(0 >= threshold) { return false; } }//|   after1 = Naeb: false (0.0)
        else if(after1 == "Nb") { if(0.875 >= threshold) { return true; } }//|   after1 = Nb: true (7.0/1.0)
        else if(after1 == "Nba") { if(0 >= threshold) { return false; } }//|   after1 = Nba: false (0.0)
        else if(after1 == "Nbc") { if(0 >= threshold) { return false; } }//|   after1 = Nbc: false (0.0)
        else if(after1 == "Nc") { if(0.72972972972973 >= threshold) { return false; } }//|   after1 = Nc: false (27.0/10.0)
        else if(after1 == "Nca") { if(0 >= threshold) { return false; } }//|   after1 = Nca: false (0.0)
        else if(after1 == "Ncb") { if(0 >= threshold) { return false; } }//|   after1 = Ncb: false (0.0)
        else if(after1 == "Ncc") { if(0 >= threshold) { return false; } }//|   after1 = Ncc: false (0.0)
        else if(after1 == "Nce") { if(0 >= threshold) { return false; } }//|   after1 = Nce: false (0.0)
        else if(after1 == "Ncd") { if(1 >= threshold) { return true; } }//|   after1 = Ncd: true (1.0)
        else if(after1 == "Ncda") { if(0 >= threshold) { return false; } }//|   after1 = Ncda: false (0.0)
        else if(after1 == "Ncdb") { if(0 >= threshold) { return false; } }//|   after1 = Ncdb: false (0.0)
        else if(after1 == "Nd")//|   after1 = Nd
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (2.0)
          else if(moreNeu == false) { if(1 >= threshold) { return true; } }//|   |   moreNeu = false: true (25.0)
        }
        else if(after1 == "Ndaa") { if(0 >= threshold) { return false; } }//|   after1 = Ndaa: false (0.0)
        else if(after1 == "Ndab") { if(0 >= threshold) { return false; } }//|   after1 = Ndab: false (0.0)
        else if(after1 == "Ndc") { if(0 >= threshold) { return false; } }//|   after1 = Ndc: false (0.0)
        else if(after1 == "Ndd") { if(0 >= threshold) { return false; } }//|   after1 = Ndd: false (0.0)
        else if(after1 == "Nep") { if(1 >= threshold) { return true; } }//|   after1 = Nep: true (2.0)
        else if(after1 == "Neqa") { if(0.666666666666667 >= threshold) { return true; } }//|   after1 = Neqa: true (2.0/1.0)
        else if(after1 == "Neqb") { if(1 >= threshold) { return false; } }//|   after1 = Neqb: false (9.0)
        else if(after1 == "Nes") { if(0.888888888888889 >= threshold) { return true; } }//|   after1 = Nes: true (8.0/1.0)
        else if(after1 == "Neu") { if(0.678571428571429 >= threshold) { return true; } }//|   after1 = Neu: true (19.0/9.0)
        else if(after1 == "Nf") { if(0 >= threshold) { return false; } }//|   after1 = Nf: false (0.0)
        else if(after1 == "Nfa") { if(0 >= threshold) { return false; } }//|   after1 = Nfa: false (0.0)
        else if(after1 == "Nfb") { if(0 >= threshold) { return false; } }//|   after1 = Nfb: false (0.0)
        else if(after1 == "Nfc") { if(0 >= threshold) { return false; } }//|   after1 = Nfc: false (0.0)
        else if(after1 == "Nfd") { if(0 >= threshold) { return false; } }//|   after1 = Nfd: false (0.0)
        else if(after1 == "Nfe") { if(0 >= threshold) { return false; } }//|   after1 = Nfe: false (0.0)
        else if(after1 == "Nfg") { if(0 >= threshold) { return false; } }//|   after1 = Nfg: false (0.0)
        else if(after1 == "Nfh") { if(0 >= threshold) { return false; } }//|   after1 = Nfh: false (0.0)
        else if(after1 == "Nfi") { if(0 >= threshold) { return false; } }//|   after1 = Nfi: false (0.0)
        else if(after1 == "Ng") { if(0.684210526315789 >= threshold) { return false; } }//|   after1 = Ng: false (26.0/12.0)
        else if(after1 == "Nh") { if(1 >= threshold) { return true; } }//|   after1 = Nh: true (3.0)
        else if(after1 == "Nhaa") { if(0 >= threshold) { return false; } }//|   after1 = Nhaa: false (0.0)
        else if(after1 == "Nhab") { if(0 >= threshold) { return false; } }//|   after1 = Nhab: false (0.0)
        else if(after1 == "Nhac") { if(0 >= threshold) { return false; } }//|   after1 = Nhac: false (0.0)
        else if(after1 == "Nhb") { if(0 >= threshold) { return false; } }//|   after1 = Nhb: false (0.0)
        else if(after1 == "Nhc") { if(0 >= threshold) { return false; } }//|   after1 = Nhc: false (0.0)
        else if(after1 == "P") { if(0.875 >= threshold) { return true; } }//|   after1 = P: true (21.0/3.0)
        else if(after1 == "SHI") { if(0 >= threshold) { return false; } }//|   after1 = SHI: false (0.0)
        else if(after1 == "V_11") { if(0 >= threshold) { return false; } }//|   after1 = V_11: false (0.0)
        else if(after1 == "T") { if(0.75 >= threshold) { return true; } }//|   after1 = T: true (6.0/2.0)
        else if(after1 == "Ta") { if(0 >= threshold) { return false; } }//|   after1 = Ta: false (0.0)
        else if(after1 == "Tb") { if(0 >= threshold) { return false; } }//|   after1 = Tb: false (0.0)
        else if(after1 == "Tc") { if(0 >= threshold) { return false; } }//|   after1 = Tc: false (0.0)
        else if(after1 == "Td") { if(0 >= threshold) { return false; } }//|   after1 = Td: false (0.0)
        else if(after1 == "VA") { if(0.8125 >= threshold) { return true; } }//|   after1 = VA: true (13.0/3.0)
        else if(after1 == "VA11") { if(0 >= threshold) { return false; } }//|   after1 = VA11: false (0.0)
        else if(after1 == "VA12") { if(0 >= threshold) { return false; } }//|   after1 = VA12: false (0.0)
        else if(after1 == "VA13") { if(0 >= threshold) { return false; } }//|   after1 = VA13: false (0.0)
        else if(after1 == "VA3") { if(0 >= threshold) { return false; } }//|   after1 = VA3: false (0.0)
        else if(after1 == "VA4") { if(0 >= threshold) { return false; } }//|   after1 = VA4: false (0.0)
        else if(after1 == "VAC") { if(0 >= threshold) { return false; } }//|   after1 = VAC: false (0.0)
        else if(after1 == "VA2") { if(0 >= threshold) { return false; } }//|   after1 = VA2: false (0.0)
        else if(after1 == "VB") { if(1 >= threshold) { return true; } }//|   after1 = VB: true (1.0)
        else if(after1 == "VB11") { if(0 >= threshold) { return false; } }//|   after1 = VB11: false (0.0)
        else if(after1 == "VB12") { if(0 >= threshold) { return false; } }//|   after1 = VB12: false (0.0)
        else if(after1 == "VB2") { if(0 >= threshold) { return false; } }//|   after1 = VB2: false (0.0)
        else if(after1 == "VC")//|   after1 = VC
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (2.0)
          else if(moreNeu == false) { if(0.814814814814815 >= threshold) { return true; } }//|   |   moreNeu = false: true (22.0/5.0)
        }
        else if(after1 == "VC2") { if(0 >= threshold) { return false; } }//|   after1 = VC2: false (0.0)
        else if(after1 == "VC31") { if(0 >= threshold) { return false; } }//|   after1 = VC31: false (0.0)
        else if(after1 == "VC32") { if(0 >= threshold) { return false; } }//|   after1 = VC32: false (0.0)
        else if(after1 == "VC33") { if(0 >= threshold) { return false; } }//|   after1 = VC33: false (0.0)
        else if(after1 == "VCL") { if(0.777777777777778 >= threshold) { return true; } }//|   after1 = VCL: true (7.0/2.0)
        else if(after1 == "VC1") { if(0 >= threshold) { return false; } }//|   after1 = VC1: false (0.0)
        else if(after1 == "VD") { if(0 >= threshold) { return false; } }//|   after1 = VD: false (0.0)
        else if(after1 == "VD1") { if(0 >= threshold) { return false; } }//|   after1 = VD1: false (0.0)
        else if(after1 == "VD2") { if(0 >= threshold) { return false; } }//|   after1 = VD2: false (0.0)
        else if(after1 == "VE") { if(0.777777777777778 >= threshold) { return true; } }//|   after1 = VE: true (7.0/2.0)
        else if(after1 == "VE11") { if(0 >= threshold) { return false; } }//|   after1 = VE11: false (0.0)
        else if(after1 == "VE12") { if(0 >= threshold) { return false; } }//|   after1 = VE12: false (0.0)
        else if(after1 == "VE2") { if(0 >= threshold) { return false; } }//|   after1 = VE2: false (0.0)
        else if(after1 == "VF") { if(0 >= threshold) { return false; } }//|   after1 = VF: false (0.0)
        else if(after1 == "VF1") { if(0 >= threshold) { return false; } }//|   after1 = VF1: false (0.0)
        else if(after1 == "VF2") { if(0 >= threshold) { return false; } }//|   after1 = VF2: false (0.0)
        else if(after1 == "VG") { if(0.857142857142857 >= threshold) { return true; } }//|   after1 = VG: true (6.0/1.0)
        else if(after1 == "VG1") { if(0 >= threshold) { return false; } }//|   after1 = VG1: false (0.0)
        else if(after1 == "VG2") { if(0 >= threshold) { return false; } }//|   after1 = VG2: false (0.0)
        else if(after1 == "VH") { if(0.815384615384615 >= threshold) { return false; } }//|   after1 = VH: false (53.0/12.0)
        else if(after1 == "VH11") { if(0 >= threshold) { return false; } }//|   after1 = VH11: false (0.0)
        else if(after1 == "VH12") { if(0 >= threshold) { return false; } }//|   after1 = VH12: false (0.0)
        else if(after1 == "VH13") { if(0 >= threshold) { return false; } }//|   after1 = VH13: false (0.0)
        else if(after1 == "VH14") { if(0 >= threshold) { return false; } }//|   after1 = VH14: false (0.0)
        else if(after1 == "VH15") { if(0 >= threshold) { return false; } }//|   after1 = VH15: false (0.0)
        else if(after1 == "VH17") { if(0 >= threshold) { return false; } }//|   after1 = VH17: false (0.0)
        else if(after1 == "VH21") { if(0 >= threshold) { return false; } }//|   after1 = VH21: false (0.0)
        else if(after1 == "VHC") { if(1 >= threshold) { return false; } }//|   after1 = VHC: false (1.0)
        else if(after1 == "VH16") { if(0 >= threshold) { return false; } }//|   after1 = VH16: false (0.0)
        else if(after1 == "VH22") { if(0 >= threshold) { return false; } }//|   after1 = VH22: false (0.0)
        else if(after1 == "VI") { if(1 >= threshold) { return true; } }//|   after1 = VI: true (1.0)
        else if(after1 == "VI1") { if(0 >= threshold) { return false; } }//|   after1 = VI1: false (0.0)
        else if(after1 == "VI2") { if(0 >= threshold) { return false; } }//|   after1 = VI2: false (0.0)
        else if(after1 == "VI3") { if(0 >= threshold) { return false; } }//|   after1 = VI3: false (0.0)
        else if(after1 == "VJ") { if(0.777777777777778 >= threshold) { return true; } }//|   after1 = VJ: true (7.0/2.0)
        else if(after1 == "VJ1") { if(0 >= threshold) { return false; } }//|   after1 = VJ1: false (0.0)
        else if(after1 == "VJ2") { if(0 >= threshold) { return false; } }//|   after1 = VJ2: false (0.0)
        else if(after1 == "VJ3") { if(0 >= threshold) { return false; } }//|   after1 = VJ3: false (0.0)
        else if(after1 == "VK") { if(1 >= threshold) { return true; } }//|   after1 = VK: true (2.0)
        else if(after1 == "VK1") { if(0 >= threshold) { return false; } }//|   after1 = VK1: false (0.0)
        else if(after1 == "VK2") { if(0 >= threshold) { return false; } }//|   after1 = VK2: false (0.0)
        else if(after1 == "VL") { if(0.75 >= threshold) { return true; } }//|   after1 = VL: true (3.0/1.0)
        else if(after1 == "VL1") { if(0 >= threshold) { return false; } }//|   after1 = VL1: false (0.0)
        else if(after1 == "VL2") { if(0 >= threshold) { return false; } }//|   after1 = VL2: false (0.0)
        else if(after1 == "VL3") { if(0 >= threshold) { return false; } }//|   after1 = VL3: false (0.0)
        else if(after1 == "VL4") { if(0 >= threshold) { return false; } }//|   after1 = VL4: false (0.0)
        else if(after1 == "V_2") { if(0.8 >= threshold) { return false; } }//|   after1 = V_2: false (4.0/1.0)
        else if(after1 == "Nv") { if(0.7 >= threshold) { return false; } }//|   after1 = Nv: false (7.0/3.0)
        else if(after1 == "Nv1") { if(0 >= threshold) { return false; } }//|   after1 = Nv1: false (0.0)
        else if(after1 == "Nv2") { if(0 >= threshold) { return false; } }//|   after1 = Nv2: false (0.0)
        else if(after1 == "Nv3") { if(0 >= threshold) { return false; } }//|   after1 = Nv3: false (0.0)
        else if(after1 == "Nv4") { if(0 >= threshold) { return false; } }//|   after1 = Nv4: false (0.0)
        else if(after1 == "b") { if(0 >= threshold) { return false; } }//|   after1 = b: false (0.0)
        else if(after1 == "notword") { if(0.767123287671233 >= threshold) { return false; } }//|   after1 = notword: false (56.0/17.0)
        else if(after1 == "begin") { if(0 >= threshold) { return false; } }//|   after1 = begin: false (0.0)
        else if(after1 == "end") { if(0 >= threshold) { return false; } }//|   after1 = end: false (0.0)
      }
      else if(front1 == "VL1") { if(0 >= threshold) { return true; } }//front1 = VL1: true (0.0)
      else if(front1 == "VL2") { if(0 >= threshold) { return true; } }//front1 = VL2: true (0.0)
      else if(front1 == "VL3") { if(0 >= threshold) { return true; } }//front1 = VL3: true (0.0)
      else if(front1 == "VL4") { if(0 >= threshold) { return true; } }//front1 = VL4: true (0.0)
      else if(front1 == "V_2") { if(0.966912062130896 >= threshold) { return false; } }//front1 = V_2: false (11952.0/409.0)
      else if(front1 == "Nv")//front1 = Nv
      {
        if(after1 == "A") { if(1 >= threshold) { return false; } }//|   after1 = A: false (5.0)
        else if(after1 == "Caa") { if(0.666666666666667 >= threshold) { return true; } }//|   after1 = Caa: true (2.0/1.0)
        else if(after1 == "Cab") { if(0 >= threshold) { return true; } }//|   after1 = Cab: true (0.0)
        else if(after1 == "Cba") { if(0 >= threshold) { return true; } }//|   after1 = Cba: true (0.0)
        else if(after1 == "Cbab") { if(0 >= threshold) { return true; } }//|   after1 = Cbab: true (0.0)
        else if(after1 == "Cbb") { if(0 >= threshold) { return true; } }//|   after1 = Cbb: true (0.0)
        else if(after1 == "Cbaa") { if(0 >= threshold) { return true; } }//|   after1 = Cbaa: true (0.0)
        else if(after1 == "Cbba") { if(0 >= threshold) { return true; } }//|   after1 = Cbba: true (0.0)
        else if(after1 == "Cbbb") { if(0 >= threshold) { return true; } }//|   after1 = Cbbb: true (0.0)
        else if(after1 == "Cbca") { if(0 >= threshold) { return true; } }//|   after1 = Cbca: true (0.0)
        else if(after1 == "Cbcb") { if(0 >= threshold) { return true; } }//|   after1 = Cbcb: true (0.0)
        else if(after1 == "D") { if(0.911764705882353 >= threshold) { return true; } }//|   after1 = D: true (31.0/3.0)
        else if(after1 == "Dab") { if(0 >= threshold) { return true; } }//|   after1 = Dab: true (0.0)
        else if(after1 == "Dbaa") { if(0 >= threshold) { return true; } }//|   after1 = Dbaa: true (0.0)
        else if(after1 == "Dbab") { if(0 >= threshold) { return true; } }//|   after1 = Dbab: true (0.0)
        else if(after1 == "Dbb") { if(0 >= threshold) { return true; } }//|   after1 = Dbb: true (0.0)
        else if(after1 == "Dbc") { if(0 >= threshold) { return true; } }//|   after1 = Dbc: true (0.0)
        else if(after1 == "Dc") { if(0 >= threshold) { return true; } }//|   after1 = Dc: true (0.0)
        else if(after1 == "Dd") { if(0 >= threshold) { return true; } }//|   after1 = Dd: true (0.0)
        else if(after1 == "Dg") { if(0 >= threshold) { return true; } }//|   after1 = Dg: true (0.0)
        else if(after1 == "Dh") { if(0 >= threshold) { return true; } }//|   after1 = Dh: true (0.0)
        else if(after1 == "Dj") { if(0 >= threshold) { return true; } }//|   after1 = Dj: true (0.0)
        else if(after1 == "Da") { if(1 >= threshold) { return true; } }//|   after1 = Da: true (1.0)
        else if(after1 == "Daa") { if(0 >= threshold) { return true; } }//|   after1 = Daa: true (0.0)
        else if(after1 == "DE")//|   after1 = DE
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (3.0)
          else if(moreNeu == false) { if(0.791666666666667 >= threshold) { return true; } }//|   |   moreNeu = false: true (19.0/5.0)
        }
        else if(after1 == "Dfa") { if(0.666666666666667 >= threshold) { return true; } }//|   after1 = Dfa: true (2.0/1.0)
        else if(after1 == "Dfb") { if(0 >= threshold) { return true; } }//|   after1 = Dfb: true (0.0)
        else if(after1 == "Di") { if(0 >= threshold) { return true; } }//|   after1 = Di: true (0.0)
        else if(after1 == "Dk") { if(0 >= threshold) { return true; } }//|   after1 = Dk: true (0.0)
        else if(after1 == "FW") { if(0 >= threshold) { return true; } }//|   after1 = FW: true (0.0)
        else if(after1 == "I") { if(0 >= threshold) { return true; } }//|   after1 = I: true (0.0)
        else if(after1 == "Na") { if(0.862745098039216 >= threshold) { return false; } }//|   after1 = Na: false (44.0/7.0)
        else if(after1 == "Naa") { if(0 >= threshold) { return true; } }//|   after1 = Naa: true (0.0)
        else if(after1 == "Nab") { if(0 >= threshold) { return true; } }//|   after1 = Nab: true (0.0)
        else if(after1 == "Nac") { if(0 >= threshold) { return true; } }//|   after1 = Nac: true (0.0)
        else if(after1 == "Nad") { if(0 >= threshold) { return true; } }//|   after1 = Nad: true (0.0)
        else if(after1 == "Naea") { if(0 >= threshold) { return true; } }//|   after1 = Naea: true (0.0)
        else if(after1 == "Naeb") { if(0 >= threshold) { return true; } }//|   after1 = Naeb: true (0.0)
        else if(after1 == "Nb") { if(0.666666666666667 >= threshold) { return true; } }//|   after1 = Nb: true (2.0/1.0)
        else if(after1 == "Nba") { if(0 >= threshold) { return true; } }//|   after1 = Nba: true (0.0)
        else if(after1 == "Nbc") { if(0 >= threshold) { return true; } }//|   after1 = Nbc: true (0.0)
        else if(after1 == "Nc") { if(0.777777777777778 >= threshold) { return true; } }//|   after1 = Nc: true (7.0/2.0)
        else if(after1 == "Nca") { if(0 >= threshold) { return true; } }//|   after1 = Nca: true (0.0)
        else if(after1 == "Ncb") { if(0 >= threshold) { return true; } }//|   after1 = Ncb: true (0.0)
        else if(after1 == "Ncc") { if(0 >= threshold) { return true; } }//|   after1 = Ncc: true (0.0)
        else if(after1 == "Nce") { if(0 >= threshold) { return true; } }//|   after1 = Nce: true (0.0)
        else if(after1 == "Ncd") { if(0 >= threshold) { return true; } }//|   after1 = Ncd: true (0.0)
        else if(after1 == "Ncda") { if(0 >= threshold) { return true; } }//|   after1 = Ncda: true (0.0)
        else if(after1 == "Ncdb") { if(0 >= threshold) { return true; } }//|   after1 = Ncdb: true (0.0)
        else if(after1 == "Nd") { if(1 >= threshold) { return true; } }//|   after1 = Nd: true (2.0)
        else if(after1 == "Ndaa") { if(0 >= threshold) { return true; } }//|   after1 = Ndaa: true (0.0)
        else if(after1 == "Ndab") { if(0 >= threshold) { return true; } }//|   after1 = Ndab: true (0.0)
        else if(after1 == "Ndc") { if(0 >= threshold) { return true; } }//|   after1 = Ndc: true (0.0)
        else if(after1 == "Ndd") { if(0 >= threshold) { return true; } }//|   after1 = Ndd: true (0.0)
        else if(after1 == "Nep") { if(0.666666666666667 >= threshold) { return true; } }//|   after1 = Nep: true (2.0/1.0)
        else if(after1 == "Neqa") { if(0.75 >= threshold) { return false; } }//|   after1 = Neqa: false (3.0/1.0)
        else if(after1 == "Neqb") { if(0 >= threshold) { return true; } }//|   after1 = Neqb: true (0.0)
        else if(after1 == "Nes") { if(1 >= threshold) { return false; } }//|   after1 = Nes: false (1.0)
        else if(after1 == "Neu") { if(1 >= threshold) { return false; } }//|   after1 = Neu: false (4.0)
        else if(after1 == "Nf") { if(0 >= threshold) { return true; } }//|   after1 = Nf: true (0.0)
        else if(after1 == "Nfa") { if(0 >= threshold) { return true; } }//|   after1 = Nfa: true (0.0)
        else if(after1 == "Nfb") { if(0 >= threshold) { return true; } }//|   after1 = Nfb: true (0.0)
        else if(after1 == "Nfc") { if(0 >= threshold) { return true; } }//|   after1 = Nfc: true (0.0)
        else if(after1 == "Nfd") { if(0 >= threshold) { return true; } }//|   after1 = Nfd: true (0.0)
        else if(after1 == "Nfe") { if(0 >= threshold) { return true; } }//|   after1 = Nfe: true (0.0)
        else if(after1 == "Nfg") { if(0 >= threshold) { return true; } }//|   after1 = Nfg: true (0.0)
        else if(after1 == "Nfh") { if(0 >= threshold) { return true; } }//|   after1 = Nfh: true (0.0)
        else if(after1 == "Nfi") { if(0 >= threshold) { return true; } }//|   after1 = Nfi: true (0.0)
        else if(after1 == "Ng") { if(0.678571428571429 >= threshold) { return true; } }//|   after1 = Ng: true (19.0/9.0)
        else if(after1 == "Nh") { if(1 >= threshold) { return true; } }//|   after1 = Nh: true (1.0)
        else if(after1 == "Nhaa") { if(0 >= threshold) { return true; } }//|   after1 = Nhaa: true (0.0)
        else if(after1 == "Nhab") { if(0 >= threshold) { return true; } }//|   after1 = Nhab: true (0.0)
        else if(after1 == "Nhac") { if(0 >= threshold) { return true; } }//|   after1 = Nhac: true (0.0)
        else if(after1 == "Nhb") { if(0 >= threshold) { return true; } }//|   after1 = Nhb: true (0.0)
        else if(after1 == "Nhc") { if(0 >= threshold) { return true; } }//|   after1 = Nhc: true (0.0)
        else if(after1 == "P") { if(0.8 >= threshold) { return true; } }//|   after1 = P: true (12.0/3.0)
        else if(after1 == "SHI") { if(0.8 >= threshold) { return true; } }//|   after1 = SHI: true (4.0/1.0)
        else if(after1 == "V_11") { if(0 >= threshold) { return true; } }//|   after1 = V_11: true (0.0)
        else if(after1 == "T") { if(1 >= threshold) { return false; } }//|   after1 = T: false (1.0)
        else if(after1 == "Ta") { if(0 >= threshold) { return true; } }//|   after1 = Ta: true (0.0)
        else if(after1 == "Tb") { if(0 >= threshold) { return true; } }//|   after1 = Tb: true (0.0)
        else if(after1 == "Tc") { if(0 >= threshold) { return true; } }//|   after1 = Tc: true (0.0)
        else if(after1 == "Td") { if(0 >= threshold) { return true; } }//|   after1 = Td: true (0.0)
        else if(after1 == "VA") { if(1 >= threshold) { return false; } }//|   after1 = VA: false (1.0)
        else if(after1 == "VA11") { if(0 >= threshold) { return true; } }//|   after1 = VA11: true (0.0)
        else if(after1 == "VA12") { if(0 >= threshold) { return true; } }//|   after1 = VA12: true (0.0)
        else if(after1 == "VA13") { if(0 >= threshold) { return true; } }//|   after1 = VA13: true (0.0)
        else if(after1 == "VA3") { if(0 >= threshold) { return true; } }//|   after1 = VA3: true (0.0)
        else if(after1 == "VA4") { if(0 >= threshold) { return true; } }//|   after1 = VA4: true (0.0)
        else if(after1 == "VAC") { if(0 >= threshold) { return true; } }//|   after1 = VAC: true (0.0)
        else if(after1 == "VA2") { if(0 >= threshold) { return true; } }//|   after1 = VA2: true (0.0)
        else if(after1 == "VB") { if(0 >= threshold) { return true; } }//|   after1 = VB: true (0.0)
        else if(after1 == "VB11") { if(0 >= threshold) { return true; } }//|   after1 = VB11: true (0.0)
        else if(after1 == "VB12") { if(0 >= threshold) { return true; } }//|   after1 = VB12: true (0.0)
        else if(after1 == "VB2") { if(0 >= threshold) { return true; } }//|   after1 = VB2: true (0.0)
        else if(after1 == "VC") { if(0.823529411764706 >= threshold) { return true; } }//|   after1 = VC: true (14.0/3.0)
        else if(after1 == "VC2") { if(0 >= threshold) { return true; } }//|   after1 = VC2: true (0.0)
        else if(after1 == "VC31") { if(0 >= threshold) { return true; } }//|   after1 = VC31: true (0.0)
        else if(after1 == "VC32") { if(0 >= threshold) { return true; } }//|   after1 = VC32: true (0.0)
        else if(after1 == "VC33") { if(0 >= threshold) { return true; } }//|   after1 = VC33: true (0.0)
        else if(after1 == "VCL") { if(0 >= threshold) { return true; } }//|   after1 = VCL: true (0.0)
        else if(after1 == "VC1") { if(0 >= threshold) { return true; } }//|   after1 = VC1: true (0.0)
        else if(after1 == "VD") { if(0 >= threshold) { return true; } }//|   after1 = VD: true (0.0)
        else if(after1 == "VD1") { if(0 >= threshold) { return true; } }//|   after1 = VD1: true (0.0)
        else if(after1 == "VD2") { if(0 >= threshold) { return true; } }//|   after1 = VD2: true (0.0)
        else if(after1 == "VE") { if(0.833333333333333 >= threshold) { return true; } }//|   after1 = VE: true (5.0/1.0)
        else if(after1 == "VE11") { if(0 >= threshold) { return true; } }//|   after1 = VE11: true (0.0)
        else if(after1 == "VE12") { if(0 >= threshold) { return true; } }//|   after1 = VE12: true (0.0)
        else if(after1 == "VE2") { if(0 >= threshold) { return true; } }//|   after1 = VE2: true (0.0)
        else if(after1 == "VF") { if(0 >= threshold) { return true; } }//|   after1 = VF: true (0.0)
        else if(after1 == "VF1") { if(0 >= threshold) { return true; } }//|   after1 = VF1: true (0.0)
        else if(after1 == "VF2") { if(0 >= threshold) { return true; } }//|   after1 = VF2: true (0.0)
        else if(after1 == "VG") { if(1 >= threshold) { return false; } }//|   after1 = VG: false (3.0)
        else if(after1 == "VG1") { if(0 >= threshold) { return true; } }//|   after1 = VG1: true (0.0)
        else if(after1 == "VG2") { if(0 >= threshold) { return true; } }//|   after1 = VG2: true (0.0)
        else if(after1 == "VH") { if(0.75 >= threshold) { return true; } }//|   after1 = VH: true (9.0/3.0)
        else if(after1 == "VH11") { if(0 >= threshold) { return true; } }//|   after1 = VH11: true (0.0)
        else if(after1 == "VH12") { if(0 >= threshold) { return true; } }//|   after1 = VH12: true (0.0)
        else if(after1 == "VH13") { if(0 >= threshold) { return true; } }//|   after1 = VH13: true (0.0)
        else if(after1 == "VH14") { if(0 >= threshold) { return true; } }//|   after1 = VH14: true (0.0)
        else if(after1 == "VH15") { if(0 >= threshold) { return true; } }//|   after1 = VH15: true (0.0)
        else if(after1 == "VH17") { if(0 >= threshold) { return true; } }//|   after1 = VH17: true (0.0)
        else if(after1 == "VH21") { if(0 >= threshold) { return true; } }//|   after1 = VH21: true (0.0)
        else if(after1 == "VHC") { if(0 >= threshold) { return true; } }//|   after1 = VHC: true (0.0)
        else if(after1 == "VH16") { if(0 >= threshold) { return true; } }//|   after1 = VH16: true (0.0)
        else if(after1 == "VH22") { if(0 >= threshold) { return true; } }//|   after1 = VH22: true (0.0)
        else if(after1 == "VI") { if(0 >= threshold) { return true; } }//|   after1 = VI: true (0.0)
        else if(after1 == "VI1") { if(0 >= threshold) { return true; } }//|   after1 = VI1: true (0.0)
        else if(after1 == "VI2") { if(0 >= threshold) { return true; } }//|   after1 = VI2: true (0.0)
        else if(after1 == "VI3") { if(0 >= threshold) { return true; } }//|   after1 = VI3: true (0.0)
        else if(after1 == "VJ") { if(0 >= threshold) { return true; } }//|   after1 = VJ: true (0.0)
        else if(after1 == "VJ1") { if(0 >= threshold) { return true; } }//|   after1 = VJ1: true (0.0)
        else if(after1 == "VJ2") { if(0 >= threshold) { return true; } }//|   after1 = VJ2: true (0.0)
        else if(after1 == "VJ3") { if(0 >= threshold) { return true; } }//|   after1 = VJ3: true (0.0)
        else if(after1 == "VK") { if(1 >= threshold) { return true; } }//|   after1 = VK: true (2.0)
        else if(after1 == "VK1") { if(0 >= threshold) { return true; } }//|   after1 = VK1: true (0.0)
        else if(after1 == "VK2") { if(0 >= threshold) { return true; } }//|   after1 = VK2: true (0.0)
        else if(after1 == "VL") { if(0 >= threshold) { return true; } }//|   after1 = VL: true (0.0)
        else if(after1 == "VL1") { if(0 >= threshold) { return true; } }//|   after1 = VL1: true (0.0)
        else if(after1 == "VL2") { if(0 >= threshold) { return true; } }//|   after1 = VL2: true (0.0)
        else if(after1 == "VL3") { if(0 >= threshold) { return true; } }//|   after1 = VL3: true (0.0)
        else if(after1 == "VL4") { if(0 >= threshold) { return true; } }//|   after1 = VL4: true (0.0)
        else if(after1 == "V_2") { if(0.8 >= threshold) { return true; } }//|   after1 = V_2: true (4.0/1.0)
        else if(after1 == "Nv") { if(0.875 >= threshold) { return false; } }//|   after1 = Nv: false (7.0/1.0)
        else if(after1 == "Nv1") { if(0 >= threshold) { return true; } }//|   after1 = Nv1: true (0.0)
        else if(after1 == "Nv2") { if(0 >= threshold) { return true; } }//|   after1 = Nv2: true (0.0)
        else if(after1 == "Nv3") { if(0 >= threshold) { return true; } }//|   after1 = Nv3: true (0.0)
        else if(after1 == "Nv4") { if(0 >= threshold) { return true; } }//|   after1 = Nv4: true (0.0)
        else if(after1 == "b") { if(0 >= threshold) { return true; } }//|   after1 = b: true (0.0)
        else if(after1 == "notword")//|   after1 = notword
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (3.0)
          else if(moreNeu == false) { if(0.755102040816326 >= threshold) { return true; } }//|   |   moreNeu = false: true (74.0/24.0)
        }
        else if(after1 == "begin") { if(0 >= threshold) { return true; } }//|   after1 = begin: true (0.0)
        else if(after1 == "end") { if(0 >= threshold) { return true; } }//|   after1 = end: true (0.0)
      }
      else if(front1 == "Nv1") { if(0 >= threshold) { return true; } }//front1 = Nv1: true (0.0)
      else if(front1 == "Nv2") { if(0 >= threshold) { return true; } }//front1 = Nv2: true (0.0)
      else if(front1 == "Nv3") { if(0 >= threshold) { return true; } }//front1 = Nv3: true (0.0)
      else if(front1 == "Nv4") { if(0 >= threshold) { return true; } }//front1 = Nv4: true (0.0)
      else if(front1 == "b") { if(0.727272727272727 >= threshold) { return false; } }//front1 = b: false (8.0/3.0)
      else if(front1 == "notword")//front1 = notword
      {
        if(after1 == "A") { if(0.707865168539326 >= threshold) { return false; } }//|   after1 = A: false (63.0/26.0)
        else if(after1 == "Caa")//|   after1 = Caa
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (10.0)
          else if(moreNeu == false) { if(0.776315789473684 >= threshold) { return true; } }//|   |   moreNeu = false: true (177.0/51.0)
        }
        else if(after1 == "Cab") { if(0.80952380952381 >= threshold) { return true; } }//|   after1 = Cab: true (17.0/4.0)
        else if(after1 == "Cba") { if(1 >= threshold) { return true; } }//|   after1 = Cba: true (2.0)
        else if(after1 == "Cbab") { if(0 >= threshold) { return true; } }//|   after1 = Cbab: true (0.0)
        else if(after1 == "Cbb") { if(0.833333333333333 >= threshold) { return true; } }//|   after1 = Cbb: true (45.0/9.0)
        else if(after1 == "Cbaa") { if(0 >= threshold) { return true; } }//|   after1 = Cbaa: true (0.0)
        else if(after1 == "Cbba") { if(0 >= threshold) { return true; } }//|   after1 = Cbba: true (0.0)
        else if(after1 == "Cbbb") { if(0 >= threshold) { return true; } }//|   after1 = Cbbb: true (0.0)
        else if(after1 == "Cbca") { if(0 >= threshold) { return true; } }//|   after1 = Cbca: true (0.0)
        else if(after1 == "Cbcb") { if(0 >= threshold) { return true; } }//|   after1 = Cbcb: true (0.0)
        else if(after1 == "D")//|   after1 = D
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (21.0)
          else if(moreNeu == false) { if(0.767991407089151 >= threshold) { return true; } }//|   |   moreNeu = false: true (715.0/216.0)
        }
        else if(after1 == "Dab") { if(0 >= threshold) { return true; } }//|   after1 = Dab: true (0.0)
        else if(after1 == "Dbaa") { if(0 >= threshold) { return true; } }//|   after1 = Dbaa: true (0.0)
        else if(after1 == "Dbab") { if(0 >= threshold) { return true; } }//|   after1 = Dbab: true (0.0)
        else if(after1 == "Dbb") { if(0 >= threshold) { return true; } }//|   after1 = Dbb: true (0.0)
        else if(after1 == "Dbc") { if(0 >= threshold) { return true; } }//|   after1 = Dbc: true (0.0)
        else if(after1 == "Dc") { if(0 >= threshold) { return true; } }//|   after1 = Dc: true (0.0)
        else if(after1 == "Dd") { if(0 >= threshold) { return true; } }//|   after1 = Dd: true (0.0)
        else if(after1 == "Dg") { if(0 >= threshold) { return true; } }//|   after1 = Dg: true (0.0)
        else if(after1 == "Dh") { if(0 >= threshold) { return true; } }//|   after1 = Dh: true (0.0)
        else if(after1 == "Dj") { if(0 >= threshold) { return true; } }//|   after1 = Dj: true (0.0)
        else if(after1 == "Da") { if(0.703703703703704 >= threshold) { return true; } }//|   after1 = Da: true (76.0/32.0)
        else if(after1 == "Daa") { if(0 >= threshold) { return true; } }//|   after1 = Daa: true (0.0)
        else if(after1 == "DE")//|   after1 = DE
        {
          if(moreNeu == true) { if(0.96 >= threshold) { return false; } }//|   |   moreNeu = true: false (24.0/1.0)
          else if(moreNeu == false) { if(0.693106004447739 >= threshold) { return true; } }//|   |   moreNeu = false: true (935.0/414.0)
        }
        else if(after1 == "Dfa")//|   after1 = Dfa
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (10.0)
          else if(moreNeu == false) { if(0.757575757575758 >= threshold) { return true; } }//|   |   moreNeu = false: true (50.0/16.0)
        }
        else if(after1 == "Dfb") { if(0 >= threshold) { return true; } }//|   after1 = Dfb: true (0.0)
        else if(after1 == "Di") { if(1 >= threshold) { return false; } }//|   after1 = Di: false (2.0)
        else if(after1 == "Dk") { if(1 >= threshold) { return true; } }//|   after1 = Dk: true (1.0)
        else if(after1 == "FW")//|   after1 = FW
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (3.0)
          else if(moreNeu == false) { if(0.735294117647059 >= threshold) { return true; } }//|   |   moreNeu = false: true (50.0/18.0)
        }
        else if(after1 == "I") { if(0.75 >= threshold) { return false; } }//|   after1 = I: false (3.0/1.0)
        else if(after1 == "Na") { if(0.764983351831299 >= threshold) { return false; } }//|   after1 = Na: false (2757.0/847.0)
        else if(after1 == "Naa") { if(0 >= threshold) { return true; } }//|   after1 = Naa: true (0.0)
        else if(after1 == "Nab") { if(0 >= threshold) { return true; } }//|   after1 = Nab: true (0.0)
        else if(after1 == "Nac") { if(0 >= threshold) { return true; } }//|   after1 = Nac: true (0.0)
        else if(after1 == "Nad") { if(0 >= threshold) { return true; } }//|   after1 = Nad: true (0.0)
        else if(after1 == "Naea") { if(0 >= threshold) { return true; } }//|   after1 = Naea: true (0.0)
        else if(after1 == "Naeb") { if(0 >= threshold) { return true; } }//|   after1 = Naeb: true (0.0)
        else if(after1 == "Nb")//|   after1 = Nb
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (15.0)
          else if(moreNeu == false) { if(0.870646766169154 >= threshold) { return true; } }//|   |   moreNeu = false: true (175.0/26.0)
        }
        else if(after1 == "Nba") { if(0 >= threshold) { return true; } }//|   after1 = Nba: true (0.0)
        else if(after1 == "Nbc") { if(0 >= threshold) { return true; } }//|   after1 = Nbc: true (0.0)
        else if(after1 == "Nc")//|   after1 = Nc
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (43.0)
          else if(moreNeu == false) { if(0.736059479553903 >= threshold) { return true; } }//|   |   moreNeu = false: true (594.0/213.0)
        }
        else if(after1 == "Nca") { if(0 >= threshold) { return true; } }//|   after1 = Nca: true (0.0)
        else if(after1 == "Ncb") { if(0 >= threshold) { return true; } }//|   after1 = Ncb: true (0.0)
        else if(after1 == "Ncc") { if(0 >= threshold) { return true; } }//|   after1 = Ncc: true (0.0)
        else if(after1 == "Nce") { if(0 >= threshold) { return true; } }//|   after1 = Nce: true (0.0)
        else if(after1 == "Ncd") { if(0.7 >= threshold) { return false; } }//|   after1 = Ncd: false (14.0/6.0)
        else if(after1 == "Ncda") { if(0 >= threshold) { return true; } }//|   after1 = Ncda: true (0.0)
        else if(after1 == "Ncdb") { if(0 >= threshold) { return true; } }//|   after1 = Ncdb: true (0.0)
        else if(after1 == "Nd")//|   after1 = Nd
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (8.0)
          else if(moreNeu == false) { if(0.978142076502732 >= threshold) { return true; } }//|   |   moreNeu = false: true (895.0/20.0)
        }
        else if(after1 == "Ndaa") { if(0 >= threshold) { return true; } }//|   after1 = Ndaa: true (0.0)
        else if(after1 == "Ndab") { if(0 >= threshold) { return true; } }//|   after1 = Ndab: true (0.0)
        else if(after1 == "Ndc") { if(0 >= threshold) { return true; } }//|   after1 = Ndc: true (0.0)
        else if(after1 == "Ndd") { if(0 >= threshold) { return true; } }//|   after1 = Ndd: true (0.0)
        else if(after1 == "Nep") { if(0.849056603773585 >= threshold) { return true; } }//|   after1 = Nep: true (45.0/8.0)
        else if(after1 == "Neqa")//|   after1 = Neqa
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (2.0)
          else if(moreNeu == false) { if(0.771428571428571 >= threshold) { return true; } }//|   |   moreNeu = false: true (54.0/16.0)
        }
        else if(after1 == "Neqb") { if(0.944444444444444 >= threshold) { return false; } }//|   after1 = Neqb: false (34.0/2.0)
        else if(after1 == "Nes")//|   after1 = Nes
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (2.0)
          else if(moreNeu == false) { if(0.857142857142857 >= threshold) { return true; } }//|   |   moreNeu = false: true (54.0/9.0)
        }
        else if(after1 == "Neu")//|   after1 = Neu
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (26.0)
          else if(moreNeu == false) { if(0.754491017964072 >= threshold) { return true; } }//|   |   moreNeu = false: true (378.0/123.0)
        }
        else if(after1 == "Nf") { if(0.730769230769231 >= threshold) { return false; } }//|   after1 = Nf: false (19.0/7.0)
        else if(after1 == "Nfa") { if(0 >= threshold) { return true; } }//|   after1 = Nfa: true (0.0)
        else if(after1 == "Nfb") { if(0 >= threshold) { return true; } }//|   after1 = Nfb: true (0.0)
        else if(after1 == "Nfc") { if(0 >= threshold) { return true; } }//|   after1 = Nfc: true (0.0)
        else if(after1 == "Nfd") { if(0 >= threshold) { return true; } }//|   after1 = Nfd: true (0.0)
        else if(after1 == "Nfe") { if(0 >= threshold) { return true; } }//|   after1 = Nfe: true (0.0)
        else if(after1 == "Nfg") { if(0 >= threshold) { return true; } }//|   after1 = Nfg: true (0.0)
        else if(after1 == "Nfh") { if(0 >= threshold) { return true; } }//|   after1 = Nfh: true (0.0)
        else if(after1 == "Nfi") { if(0 >= threshold) { return true; } }//|   after1 = Nfi: true (0.0)
        else if(after1 == "Ng") { if(0.731107205623902 >= threshold) { return false; } }//|   after1 = Ng: false (832.0/306.0)
        else if(after1 == "Nh")//|   after1 = Nh
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (4.0)
          else if(moreNeu == false) { if(0.953488372093023 >= threshold) { return true; } }//|   |   moreNeu = false: true (287.0/14.0)
        }
        else if(after1 == "Nhaa") { if(0 >= threshold) { return true; } }//|   after1 = Nhaa: true (0.0)
        else if(after1 == "Nhab") { if(0 >= threshold) { return true; } }//|   after1 = Nhab: true (0.0)
        else if(after1 == "Nhac") { if(0 >= threshold) { return true; } }//|   after1 = Nhac: true (0.0)
        else if(after1 == "Nhb") { if(0 >= threshold) { return true; } }//|   after1 = Nhb: true (0.0)
        else if(after1 == "Nhc") { if(0 >= threshold) { return true; } }//|   after1 = Nhc: true (0.0)
        else if(after1 == "P")//|   after1 = P
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (7.0)
          else if(moreNeu == false) { if(0.847826086956522 >= threshold) { return true; } }//|   |   moreNeu = false: true (312.0/56.0)
        }
        else if(after1 == "SHI")//|   after1 = SHI
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (59.0)
          else if(moreNeu == false) { if(0.814814814814815 >= threshold) { return true; } }//|   |   moreNeu = false: true (132.0/30.0)
        }
        else if(after1 == "V_11") { if(0 >= threshold) { return true; } }//|   after1 = V_11: true (0.0)
        else if(after1 == "T") { if(0.729166666666667 >= threshold) { return false; } }//|   after1 = T: false (70.0/26.0)
        else if(after1 == "Ta") { if(0 >= threshold) { return true; } }//|   after1 = Ta: true (0.0)
        else if(after1 == "Tb") { if(0 >= threshold) { return true; } }//|   after1 = Tb: true (0.0)
        else if(after1 == "Tc") { if(0 >= threshold) { return true; } }//|   after1 = Tc: true (0.0)
        else if(after1 == "Td") { if(0 >= threshold) { return true; } }//|   after1 = Td: true (0.0)
        else if(after1 == "VA")//|   after1 = VA
        {
          if(moreNeu == true) { if(0.909090909090909 >= threshold) { return false; } }//|   |   moreNeu = true: false (10.0/1.0)
          else if(moreNeu == false) { if(0.750809061488673 >= threshold) { return true; } }//|   |   moreNeu = false: true (232.0/77.0)
        }
        else if(after1 == "VA11") { if(0 >= threshold) { return true; } }//|   after1 = VA11: true (0.0)
        else if(after1 == "VA12") { if(0 >= threshold) { return true; } }//|   after1 = VA12: true (0.0)
        else if(after1 == "VA13") { if(0 >= threshold) { return true; } }//|   after1 = VA13: true (0.0)
        else if(after1 == "VA3") { if(0 >= threshold) { return true; } }//|   after1 = VA3: true (0.0)
        else if(after1 == "VA4") { if(0 >= threshold) { return true; } }//|   after1 = VA4: true (0.0)
        else if(after1 == "VAC") { if(1 >= threshold) { return true; } }//|   after1 = VAC: true (2.0)
        else if(after1 == "VA2") { if(0 >= threshold) { return true; } }//|   after1 = VA2: true (0.0)
        else if(after1 == "VB") { if(0.68 >= threshold) { return true; } }//|   after1 = VB: true (17.0/8.0)
        else if(after1 == "VB11") { if(0 >= threshold) { return true; } }//|   after1 = VB11: true (0.0)
        else if(after1 == "VB12") { if(0 >= threshold) { return true; } }//|   after1 = VB12: true (0.0)
        else if(after1 == "VB2") { if(0 >= threshold) { return true; } }//|   after1 = VB2: true (0.0)
        else if(after1 == "VC")//|   after1 = VC
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (25.0)
          else if(moreNeu == false) { if(0.764900662251656 >= threshold) { return true; } }//|   |   moreNeu = false: true (462.0/142.0)
        }
        else if(after1 == "VC2") { if(0 >= threshold) { return true; } }//|   after1 = VC2: true (0.0)
        else if(after1 == "VC31") { if(0 >= threshold) { return true; } }//|   after1 = VC31: true (0.0)
        else if(after1 == "VC32") { if(0 >= threshold) { return true; } }//|   after1 = VC32: true (0.0)
        else if(after1 == "VC33") { if(0 >= threshold) { return true; } }//|   after1 = VC33: true (0.0)
        else if(after1 == "VCL") { if(0.727272727272727 >= threshold) { return true; } }//|   after1 = VCL: true (80.0/30.0)
        else if(after1 == "VC1") { if(0 >= threshold) { return true; } }//|   after1 = VC1: true (0.0)
        else if(after1 == "VD") { if(0.7 >= threshold) { return true; } }//|   after1 = VD: true (14.0/6.0)
        else if(after1 == "VD1") { if(0 >= threshold) { return true; } }//|   after1 = VD1: true (0.0)
        else if(after1 == "VD2") { if(0 >= threshold) { return true; } }//|   after1 = VD2: true (0.0)
        else if(after1 == "VE")//|   after1 = VE
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (7.0)
          else if(moreNeu == false) { if(0.805031446540881 >= threshold) { return true; } }//|   |   moreNeu = false: true (128.0/31.0)
        }
        else if(after1 == "VE11") { if(0 >= threshold) { return true; } }//|   after1 = VE11: true (0.0)
        else if(after1 == "VE12") { if(0 >= threshold) { return true; } }//|   after1 = VE12: true (0.0)
        else if(after1 == "VE2") { if(0 >= threshold) { return true; } }//|   after1 = VE2: true (0.0)
        else if(after1 == "VF") { if(0.875 >= threshold) { return true; } }//|   after1 = VF: true (14.0/2.0)
        else if(after1 == "VF1") { if(0 >= threshold) { return true; } }//|   after1 = VF1: true (0.0)
        else if(after1 == "VF2") { if(0 >= threshold) { return true; } }//|   after1 = VF2: true (0.0)
        else if(after1 == "VG")//|   after1 = VG
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (2.0)
          else if(moreNeu == false) { if(0.732824427480916 >= threshold) { return true; } }//|   |   moreNeu = false: true (96.0/35.0)
        }
        else if(after1 == "VG1") { if(0 >= threshold) { return true; } }//|   after1 = VG1: true (0.0)
        else if(after1 == "VG2") { if(0 >= threshold) { return true; } }//|   after1 = VG2: true (0.0)
        else if(after1 == "VH") { if(0.73159509202454 >= threshold) { return false; } }//|   after1 = VH: false (477.0/175.0)
        else if(after1 == "VH11") { if(0 >= threshold) { return true; } }//|   after1 = VH11: true (0.0)
        else if(after1 == "VH12") { if(0 >= threshold) { return true; } }//|   after1 = VH12: true (0.0)
        else if(after1 == "VH13") { if(0 >= threshold) { return true; } }//|   after1 = VH13: true (0.0)
        else if(after1 == "VH14") { if(0 >= threshold) { return true; } }//|   after1 = VH14: true (0.0)
        else if(after1 == "VH15") { if(0 >= threshold) { return true; } }//|   after1 = VH15: true (0.0)
        else if(after1 == "VH17") { if(0 >= threshold) { return true; } }//|   after1 = VH17: true (0.0)
        else if(after1 == "VH21") { if(0 >= threshold) { return true; } }//|   after1 = VH21: true (0.0)
        else if(after1 == "VHC") { if(0.724137931034483 >= threshold) { return true; } }//|   after1 = VHC: true (21.0/8.0)
        else if(after1 == "VH16") { if(0 >= threshold) { return true; } }//|   after1 = VH16: true (0.0)
        else if(after1 == "VH22") { if(0 >= threshold) { return true; } }//|   after1 = VH22: true (0.0)
        else if(after1 == "VI") { if(0.8 >= threshold) { return false; } }//|   after1 = VI: false (4.0/1.0)
        else if(after1 == "VI1") { if(0 >= threshold) { return true; } }//|   after1 = VI1: true (0.0)
        else if(after1 == "VI2") { if(0 >= threshold) { return true; } }//|   after1 = VI2: true (0.0)
        else if(after1 == "VI3") { if(0 >= threshold) { return true; } }//|   after1 = VI3: true (0.0)
        else if(after1 == "VJ") { if(0.700680272108844 >= threshold) { return true; } }//|   after1 = VJ: true (103.0/44.0)
        else if(after1 == "VJ1") { if(0 >= threshold) { return true; } }//|   after1 = VJ1: true (0.0)
        else if(after1 == "VJ2") { if(0 >= threshold) { return true; } }//|   after1 = VJ2: true (0.0)
        else if(after1 == "VJ3") { if(0 >= threshold) { return true; } }//|   after1 = VJ3: true (0.0)
        else if(after1 == "VK") { if(0.7 >= threshold) { return false; } }//|   after1 = VK: false (35.0/15.0)
        else if(after1 == "VK1") { if(0 >= threshold) { return true; } }//|   after1 = VK1: true (0.0)
        else if(after1 == "VK2") { if(0 >= threshold) { return true; } }//|   after1 = VK2: true (0.0)
        else if(after1 == "VL") { if(0.708333333333333 >= threshold) { return false; } }//|   after1 = VL: false (17.0/7.0)
        else if(after1 == "VL1") { if(0 >= threshold) { return true; } }//|   after1 = VL1: true (0.0)
        else if(after1 == "VL2") { if(0 >= threshold) { return true; } }//|   after1 = VL2: true (0.0)
        else if(after1 == "VL3") { if(0 >= threshold) { return true; } }//|   after1 = VL3: true (0.0)
        else if(after1 == "VL4") { if(0 >= threshold) { return true; } }//|   after1 = VL4: true (0.0)
        else if(after1 == "V_2")//|   after1 = V_2
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (2.0)
          else if(moreNeu == false) { if(0.878787878787879 >= threshold) { return true; } }//|   |   moreNeu = false: true (58.0/8.0)
        }
        else if(after1 == "Nv") { if(0.793478260869565 >= threshold) { return false; } }//|   after1 = Nv: false (73.0/19.0)
        else if(after1 == "Nv1") { if(0 >= threshold) { return true; } }//|   after1 = Nv1: true (0.0)
        else if(after1 == "Nv2") { if(0 >= threshold) { return true; } }//|   after1 = Nv2: true (0.0)
        else if(after1 == "Nv3") { if(0 >= threshold) { return true; } }//|   after1 = Nv3: true (0.0)
        else if(after1 == "Nv4") { if(0 >= threshold) { return true; } }//|   after1 = Nv4: true (0.0)
        else if(after1 == "b") { if(0 >= threshold) { return true; } }//|   after1 = b: true (0.0)
        else if(after1 == "notword")//|   after1 = notword
        {
          if(moreNeu == true) { if(0.993464052287582 >= threshold) { return false; } }//|   |   moreNeu = true: false (152.0/1.0)
          else if(moreNeu == false) { if(0.70452691680261 >= threshold) { return true; } }//|   |   moreNeu = false: true (3455.0/1449.0)
        }
        else if(after1 == "begin") { if(0 >= threshold) { return true; } }//|   after1 = begin: true (0.0)
        else if(after1 == "end") { if(0.75 >= threshold) { return false; } }//|   after1 = end: false (3.0/1.0)
      }
      else if(front1 == "begin")//front1 = begin
      {
        if(after1 == "A")//|   after1 = A
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (20.0)
          else if(moreNeu == false) { if(0.719101123595506 >= threshold) { return true; } }//|   |   moreNeu = false: true (320.0/125.0)
        }
        else if(after1 == "Caa")//|   after1 = Caa
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (12.0)
          else if(moreNeu == false) { if(0.856275303643725 >= threshold) { return true; } }//|   |   moreNeu = false: true (423.0/71.0)
        }
        else if(after1 == "Cab") { if(0.75 >= threshold) { return false; } }//|   after1 = Cab: false (6.0/2.0)
        else if(after1 == "Cba") { if(0.833333333333333 >= threshold) { return false; } }//|   after1 = Cba: false (5.0/1.0)
        else if(after1 == "Cbab") { if(0 >= threshold) { return true; } }//|   after1 = Cbab: true (0.0)
        else if(after1 == "Cbb")//|   after1 = Cbb
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (12.0)
          else if(moreNeu == false) { if(0.981308411214953 >= threshold) { return true; } }//|   |   moreNeu = false: true (945.0/18.0)
        }
        else if(after1 == "Cbaa") { if(0 >= threshold) { return true; } }//|   after1 = Cbaa: true (0.0)
        else if(after1 == "Cbba") { if(0 >= threshold) { return true; } }//|   after1 = Cbba: true (0.0)
        else if(after1 == "Cbbb") { if(0 >= threshold) { return true; } }//|   after1 = Cbbb: true (0.0)
        else if(after1 == "Cbca") { if(0 >= threshold) { return true; } }//|   after1 = Cbca: true (0.0)
        else if(after1 == "Cbcb") { if(0 >= threshold) { return true; } }//|   after1 = Cbcb: true (0.0)
        else if(after1 == "D")//|   after1 = D
        {
          if(moreNeu == true) { if(0.990196078431373 >= threshold) { return false; } }//|   |   moreNeu = true: false (303.0/3.0)
          else if(moreNeu == false) { if(0.912438707094966 >= threshold) { return true; } }//|   |   moreNeu = false: true (9118.0/875.0)
        }
        else if(after1 == "Dab") { if(0 >= threshold) { return true; } }//|   after1 = Dab: true (0.0)
        else if(after1 == "Dbaa") { if(0 >= threshold) { return true; } }//|   after1 = Dbaa: true (0.0)
        else if(after1 == "Dbab") { if(0 >= threshold) { return true; } }//|   after1 = Dbab: true (0.0)
        else if(after1 == "Dbb") { if(0 >= threshold) { return true; } }//|   after1 = Dbb: true (0.0)
        else if(after1 == "Dbc") { if(0 >= threshold) { return true; } }//|   after1 = Dbc: true (0.0)
        else if(after1 == "Dc") { if(0 >= threshold) { return true; } }//|   after1 = Dc: true (0.0)
        else if(after1 == "Dd") { if(0 >= threshold) { return true; } }//|   after1 = Dd: true (0.0)
        else if(after1 == "Dg") { if(0 >= threshold) { return true; } }//|   after1 = Dg: true (0.0)
        else if(after1 == "Dh") { if(0 >= threshold) { return true; } }//|   after1 = Dh: true (0.0)
        else if(after1 == "Dj") { if(0 >= threshold) { return true; } }//|   after1 = Dj: true (0.0)
        else if(after1 == "Da")//|   after1 = Da
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (13.0)
          else if(moreNeu == false) { if(0.87960687960688 >= threshold) { return true; } }//|   |   moreNeu = false: true (716.0/98.0)
        }
        else if(after1 == "Daa") { if(0 >= threshold) { return true; } }//|   after1 = Daa: true (0.0)
        else if(after1 == "DE")//|   after1 = DE
        {
          if(moreNeu == true) { if(0.978102189781022 >= threshold) { return false; } }//|   |   moreNeu = true: false (134.0/3.0)
          else if(moreNeu == false) { if(0.854926299456943 >= threshold) { return true; } }//|   |   moreNeu = false: true (3306.0/561.0)
        }
        else if(after1 == "Dfa")//|   after1 = Dfa
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (5.0)
          else if(moreNeu == false) { if(0.860254083484574 >= threshold) { return true; } }//|   |   moreNeu = false: true (474.0/77.0)
        }
        else if(after1 == "Dfb") { if(0 >= threshold) { return true; } }//|   after1 = Dfb: true (0.0)
        else if(after1 == "Di") { if(0.833333333333333 >= threshold) { return false; } }//|   after1 = Di: false (5.0/1.0)
        else if(after1 == "Dk")//|   after1 = Dk
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (4.0)
          else if(moreNeu == false) { if(0.833333333333333 >= threshold) { return true; } }//|   |   moreNeu = false: true (15.0/3.0)
        }
        else if(after1 == "FW")//|   after1 = FW
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (10.0)
          else if(moreNeu == false) { if(0.844961240310077 >= threshold) { return true; } }//|   |   moreNeu = false: true (218.0/40.0)
        }
        else if(after1 == "I") { if(0.75 >= threshold) { return true; } }//|   after1 = I: true (9.0/3.0)
        else if(after1 == "Na") { if(0.684040838259001 >= threshold) { return false; } }//|   after1 = Na: false (10184.0/4704.0)
        else if(after1 == "Naa") { if(0 >= threshold) { return true; } }//|   after1 = Naa: true (0.0)
        else if(after1 == "Nab") { if(0 >= threshold) { return true; } }//|   after1 = Nab: true (0.0)
        else if(after1 == "Nac") { if(0 >= threshold) { return true; } }//|   after1 = Nac: true (0.0)
        else if(after1 == "Nad") { if(0 >= threshold) { return true; } }//|   after1 = Nad: true (0.0)
        else if(after1 == "Naea") { if(0 >= threshold) { return true; } }//|   after1 = Naea: true (0.0)
        else if(after1 == "Naeb") { if(0 >= threshold) { return true; } }//|   after1 = Naeb: true (0.0)
        else if(after1 == "Nb")//|   after1 = Nb
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (58.0)
          else if(moreNeu == false) { if(0.93159977388355 >= threshold) { return true; } }//|   |   moreNeu = false: true (1648.0/121.0)
        }
        else if(after1 == "Nba") { if(0 >= threshold) { return true; } }//|   after1 = Nba: true (0.0)
        else if(after1 == "Nbc") { if(0 >= threshold) { return true; } }//|   after1 = Nbc: true (0.0)
        else if(after1 == "Nc")//|   after1 = Nc
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (154.0)
          else if(moreNeu == false) { if(0.88799811365244 >= threshold) { return true; } }//|   |   moreNeu = false: true (3766.0/475.0)
        }
        else if(after1 == "Nca") { if(0 >= threshold) { return true; } }//|   after1 = Nca: true (0.0)
        else if(after1 == "Ncb") { if(0 >= threshold) { return true; } }//|   after1 = Ncb: true (0.0)
        else if(after1 == "Ncc") { if(0 >= threshold) { return true; } }//|   after1 = Ncc: true (0.0)
        else if(after1 == "Nce") { if(0 >= threshold) { return true; } }//|   after1 = Nce: true (0.0)
        else if(after1 == "Ncd")//|   after1 = Ncd
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (3.0)
          else if(moreNeu == false) { if(0.835714285714286 >= threshold) { return true; } }//|   |   moreNeu = false: true (117.0/23.0)
        }
        else if(after1 == "Ncda") { if(0 >= threshold) { return true; } }//|   after1 = Ncda: true (0.0)
        else if(after1 == "Ncdb") { if(0 >= threshold) { return true; } }//|   after1 = Ncdb: true (0.0)
        else if(after1 == "Nd")//|   after1 = Nd
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (80.0)
          else if(moreNeu == false) { if(0.975796621974578 >= threshold) { return true; } }//|   |   moreNeu = false: true (5604.0/139.0)
        }
        else if(after1 == "Ndaa") { if(0 >= threshold) { return true; } }//|   after1 = Ndaa: true (0.0)
        else if(after1 == "Ndab") { if(0 >= threshold) { return true; } }//|   after1 = Ndab: true (0.0)
        else if(after1 == "Ndc") { if(0 >= threshold) { return true; } }//|   after1 = Ndc: true (0.0)
        else if(after1 == "Ndd") { if(0 >= threshold) { return true; } }//|   after1 = Ndd: true (0.0)
        else if(after1 == "Nep")//|   after1 = Nep
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (3.0)
          else if(moreNeu == false) { if(0.897435897435897 >= threshold) { return true; } }//|   |   moreNeu = false: true (420.0/48.0)
        }
        else if(after1 == "Neqa")//|   after1 = Neqa
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (13.0)
          else if(moreNeu == false) { if(0.925531914893617 >= threshold) { return true; } }//|   |   moreNeu = false: true (609.0/49.0)
        }
        else if(after1 == "Neqb") { if(0.881057268722467 >= threshold) { return false; } }//|   after1 = Neqb: false (200.0/27.0)
        else if(after1 == "Nes")//|   after1 = Nes
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (12.0)
          else if(moreNeu == false) { if(0.926646706586826 >= threshold) { return true; } }//|   |   moreNeu = false: true (619.0/49.0)
        }
        else if(after1 == "Neu")//|   after1 = Neu
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (31.0)
          else if(moreNeu == false) { if(0.754069511658601 >= threshold) { return true; } }//|   |   moreNeu = false: true (1714.0/559.0)
        }
        else if(after1 == "Nf") { if(0.680555555555556 >= threshold) { return false; } }//|   after1 = Nf: false (49.0/23.0)
        else if(after1 == "Nfa") { if(0 >= threshold) { return true; } }//|   after1 = Nfa: true (0.0)
        else if(after1 == "Nfb") { if(0 >= threshold) { return true; } }//|   after1 = Nfb: true (0.0)
        else if(after1 == "Nfc") { if(0 >= threshold) { return true; } }//|   after1 = Nfc: true (0.0)
        else if(after1 == "Nfd") { if(0 >= threshold) { return true; } }//|   after1 = Nfd: true (0.0)
        else if(after1 == "Nfe") { if(0 >= threshold) { return true; } }//|   after1 = Nfe: true (0.0)
        else if(after1 == "Nfg") { if(0 >= threshold) { return true; } }//|   after1 = Nfg: true (0.0)
        else if(after1 == "Nfh") { if(0 >= threshold) { return true; } }//|   after1 = Nfh: true (0.0)
        else if(after1 == "Nfi") { if(0 >= threshold) { return true; } }//|   after1 = Nfi: true (0.0)
        else if(after1 == "Ng")//|   after1 = Ng
        {
          if(moreNeu == true) { if(0.983695652173913 >= threshold) { return false; } }//|   |   moreNeu = true: false (181.0/3.0)
          else if(moreNeu == false) { if(0.678813693011516 >= threshold) { return true; } }//|   |   moreNeu = false: true (4303.0/2036.0)
        }
        else if(after1 == "Nh")//|   after1 = Nh
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (58.0)
          else if(moreNeu == false) { if(0.980820866896816 >= threshold) { return true; } }//|   |   moreNeu = false: true (2557.0/50.0)
        }
        else if(after1 == "Nhaa") { if(0 >= threshold) { return true; } }//|   after1 = Nhaa: true (0.0)
        else if(after1 == "Nhab") { if(0 >= threshold) { return true; } }//|   after1 = Nhab: true (0.0)
        else if(after1 == "Nhac") { if(0 >= threshold) { return true; } }//|   after1 = Nhac: true (0.0)
        else if(after1 == "Nhb") { if(0 >= threshold) { return true; } }//|   after1 = Nhb: true (0.0)
        else if(after1 == "Nhc") { if(0 >= threshold) { return true; } }//|   after1 = Nhc: true (0.0)
        else if(after1 == "P")//|   after1 = P
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (106.0)
          else if(moreNeu == false) { if(0.906241838600157 >= threshold) { return true; } }//|   |   moreNeu = false: true (3470.0/359.0)
        }
        else if(after1 == "SHI")//|   after1 = SHI
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (353.0)
          else if(moreNeu == false) { if(0.738941261783901 >= threshold) { return true; } }//|   |   moreNeu = false: true (1019.0/360.0)
        }
        else if(after1 == "V_11") { if(0 >= threshold) { return true; } }//|   after1 = V_11: true (0.0)
        else if(after1 == "T")//|   after1 = T
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (35.0)
          else if(moreNeu == false) { if(0.757847533632287 >= threshold) { return true; } }//|   |   moreNeu = false: true (169.0/54.0)
        }
        else if(after1 == "Ta") { if(0 >= threshold) { return true; } }//|   after1 = Ta: true (0.0)
        else if(after1 == "Tb") { if(0 >= threshold) { return true; } }//|   after1 = Tb: true (0.0)
        else if(after1 == "Tc") { if(0 >= threshold) { return true; } }//|   after1 = Tc: true (0.0)
        else if(after1 == "Td") { if(0 >= threshold) { return true; } }//|   after1 = Td: true (0.0)
        else if(after1 == "VA")//|   after1 = VA
        {
          if(moreNeu == true) { if(0.970588235294118 >= threshold) { return false; } }//|   |   moreNeu = true: false (66.0/2.0)
          else if(moreNeu == false) { if(0.8264 >= threshold) { return true; } }//|   |   moreNeu = false: true (1033.0/217.0)
        }
        else if(after1 == "VA11") { if(0 >= threshold) { return true; } }//|   after1 = VA11: true (0.0)
        else if(after1 == "VA12") { if(0 >= threshold) { return true; } }//|   after1 = VA12: true (0.0)
        else if(after1 == "VA13") { if(0 >= threshold) { return true; } }//|   after1 = VA13: true (0.0)
        else if(after1 == "VA3") { if(0 >= threshold) { return true; } }//|   after1 = VA3: true (0.0)
        else if(after1 == "VA4") { if(0 >= threshold) { return true; } }//|   after1 = VA4: true (0.0)
        else if(after1 == "VAC") { if(0.769230769230769 >= threshold) { return true; } }//|   after1 = VAC: true (20.0/6.0)
        else if(after1 == "VA2") { if(0 >= threshold) { return true; } }//|   after1 = VA2: true (0.0)
        else if(after1 == "VB")//|   after1 = VB
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (5.0)
          else if(moreNeu == false) { if(0.857142857142857 >= threshold) { return true; } }//|   |   moreNeu = false: true (66.0/11.0)
        }
        else if(after1 == "VB11") { if(0 >= threshold) { return true; } }//|   after1 = VB11: true (0.0)
        else if(after1 == "VB12") { if(0 >= threshold) { return true; } }//|   after1 = VB12: true (0.0)
        else if(after1 == "VB2") { if(0 >= threshold) { return true; } }//|   after1 = VB2: true (0.0)
        else if(after1 == "VC")//|   after1 = VC
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (141.0)
          else if(moreNeu == false) { if(0.848401826484018 >= threshold) { return true; } }//|   |   moreNeu = false: true (2787.0/498.0)
        }
        else if(after1 == "VC2") { if(0 >= threshold) { return true; } }//|   after1 = VC2: true (0.0)
        else if(after1 == "VC31") { if(0 >= threshold) { return true; } }//|   after1 = VC31: true (0.0)
        else if(after1 == "VC32") { if(0 >= threshold) { return true; } }//|   after1 = VC32: true (0.0)
        else if(after1 == "VC33") { if(0 >= threshold) { return true; } }//|   after1 = VC33: true (0.0)
        else if(after1 == "VCL")//|   after1 = VCL
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (52.0)
          else if(moreNeu == false) { if(0.842666666666667 >= threshold) { return true; } }//|   |   moreNeu = false: true (632.0/118.0)
        }
        else if(after1 == "VC1") { if(0 >= threshold) { return true; } }//|   after1 = VC1: true (0.0)
        else if(after1 == "VD")//|   after1 = VD
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (4.0)
          else if(moreNeu == false) { if(0.823529411764706 >= threshold) { return true; } }//|   |   moreNeu = false: true (140.0/30.0)
        }
        else if(after1 == "VD1") { if(0 >= threshold) { return true; } }//|   after1 = VD1: true (0.0)
        else if(after1 == "VD2") { if(0 >= threshold) { return true; } }//|   after1 = VD2: true (0.0)
        else if(after1 == "VE")//|   after1 = VE
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (59.0)
          else if(moreNeu == false) { if(0.895522388059702 >= threshold) { return true; } }//|   |   moreNeu = false: true (840.0/98.0)
        }
        else if(after1 == "VE11") { if(0 >= threshold) { return true; } }//|   after1 = VE11: true (0.0)
        else if(after1 == "VE12") { if(0 >= threshold) { return true; } }//|   after1 = VE12: true (0.0)
        else if(after1 == "VE2") { if(0 >= threshold) { return true; } }//|   after1 = VE2: true (0.0)
        else if(after1 == "VF")//|   after1 = VF
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (9.0)
          else if(moreNeu == false) { if(0.876777251184834 >= threshold) { return true; } }//|   |   moreNeu = false: true (185.0/26.0)
        }
        else if(after1 == "VF1") { if(0 >= threshold) { return true; } }//|   after1 = VF1: true (0.0)
        else if(after1 == "VF2") { if(0 >= threshold) { return true; } }//|   after1 = VF2: true (0.0)
        else if(after1 == "VG")//|   after1 = VG
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (58.0)
          else if(moreNeu == false) { if(0.799418604651163 >= threshold) { return true; } }//|   |   moreNeu = false: true (550.0/138.0)
        }
        else if(after1 == "VG1") { if(0 >= threshold) { return true; } }//|   after1 = VG1: true (0.0)
        else if(after1 == "VG2") { if(0 >= threshold) { return true; } }//|   after1 = VG2: true (0.0)
        else if(after1 == "VH") { if(0.680418197230856 >= threshold) { return false; } }//|   after1 = VH: false (2408.0/1131.0)
        else if(after1 == "VH11") { if(0 >= threshold) { return true; } }//|   after1 = VH11: true (0.0)
        else if(after1 == "VH12") { if(0 >= threshold) { return true; } }//|   after1 = VH12: true (0.0)
        else if(after1 == "VH13") { if(0 >= threshold) { return true; } }//|   after1 = VH13: true (0.0)
        else if(after1 == "VH14") { if(0 >= threshold) { return true; } }//|   after1 = VH14: true (0.0)
        else if(after1 == "VH15") { if(0 >= threshold) { return true; } }//|   after1 = VH15: true (0.0)
        else if(after1 == "VH17") { if(0 >= threshold) { return true; } }//|   after1 = VH17: true (0.0)
        else if(after1 == "VH21") { if(0 >= threshold) { return true; } }//|   after1 = VH21: true (0.0)
        else if(after1 == "VHC")//|   after1 = VHC
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (7.0)
          else if(moreNeu == false) { if(0.802816901408451 >= threshold) { return true; } }//|   |   moreNeu = false: true (171.0/42.0)
        }
        else if(after1 == "VH16") { if(0 >= threshold) { return true; } }//|   after1 = VH16: true (0.0)
        else if(after1 == "VH22") { if(0 >= threshold) { return true; } }//|   after1 = VH22: true (0.0)
        else if(after1 == "VI") { if(0.740740740740741 >= threshold) { return false; } }//|   after1 = VI: false (20.0/7.0)
        else if(after1 == "VI1") { if(0 >= threshold) { return true; } }//|   after1 = VI1: true (0.0)
        else if(after1 == "VI2") { if(0 >= threshold) { return true; } }//|   after1 = VI2: true (0.0)
        else if(after1 == "VI3") { if(0 >= threshold) { return true; } }//|   after1 = VI3: true (0.0)
        else if(after1 == "VJ")//|   after1 = VJ
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (29.0)
          else if(moreNeu == false) { if(0.784313725490196 >= threshold) { return true; } }//|   |   moreNeu = false: true (800.0/220.0)
        }
        else if(after1 == "VJ1") { if(0 >= threshold) { return true; } }//|   after1 = VJ1: true (0.0)
        else if(after1 == "VJ2") { if(0 >= threshold) { return true; } }//|   after1 = VJ2: true (0.0)
        else if(after1 == "VJ3") { if(0 >= threshold) { return true; } }//|   after1 = VJ3: true (0.0)
        else if(after1 == "VK")//|   after1 = VK
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (14.0)
          else if(moreNeu == false) { if(0.874659400544959 >= threshold) { return true; } }//|   |   moreNeu = false: true (321.0/46.0)
        }
        else if(after1 == "VK1") { if(0 >= threshold) { return true; } }//|   after1 = VK1: true (0.0)
        else if(after1 == "VK2") { if(0 >= threshold) { return true; } }//|   after1 = VK2: true (0.0)
        else if(after1 == "VL")//|   after1 = VL
        {
          if(moreNeu == true) { if(0.9 >= threshold) { return false; } }//|   |   moreNeu = true: false (9.0/1.0)
          else if(moreNeu == false) { if(0.820846905537459 >= threshold) { return true; } }//|   |   moreNeu = false: true (252.0/55.0)
        }
        else if(after1 == "VL1") { if(0 >= threshold) { return true; } }//|   after1 = VL1: true (0.0)
        else if(after1 == "VL2") { if(0 >= threshold) { return true; } }//|   after1 = VL2: true (0.0)
        else if(after1 == "VL3") { if(0 >= threshold) { return true; } }//|   after1 = VL3: true (0.0)
        else if(after1 == "VL4") { if(0 >= threshold) { return true; } }//|   after1 = VL4: true (0.0)
        else if(after1 == "V_2")//|   after1 = V_2
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (13.0)
          else if(moreNeu == false) { if(0.904400606980273 >= threshold) { return true; } }//|   |   moreNeu = false: true (596.0/63.0)
        }
        else if(after1 == "Nv")//|   after1 = Nv
        {
          if(moreNeu == true) { if(1 >= threshold) { return false; } }//|   |   moreNeu = true: false (21.0)
          else if(moreNeu == false) { if(0.695538057742782 >= threshold) { return true; } }//|   |   moreNeu = false: true (265.0/116.0)
        }
        else if(after1 == "Nv1") { if(0 >= threshold) { return true; } }//|   after1 = Nv1: true (0.0)
        else if(after1 == "Nv2") { if(0 >= threshold) { return true; } }//|   after1 = Nv2: true (0.0)
        else if(after1 == "Nv3") { if(0 >= threshold) { return true; } }//|   after1 = Nv3: true (0.0)
        else if(after1 == "Nv4") { if(0 >= threshold) { return true; } }//|   after1 = Nv4: true (0.0)
        else if(after1 == "b") { if(1 >= threshold) { return false; } }//|   after1 = b: false (2.0)
        else if(after1 == "notword")//|   after1 = notword
        {
          if(moreNeu == true) { if(0.99344262295082 >= threshold) { return false; } }//|   |   moreNeu = true: false (303.0/2.0)
          else if(moreNeu == false) { if(0.891354246365723 >= threshold) { return true; } }//|   |   moreNeu = false: true (5825.0/710.0)
        }
        else if(after1 == "begin") { if(0 >= threshold) { return true; } }//|   after1 = begin: true (0.0)
        else if(after1 == "end") { if(0.857142857142857 >= threshold) { return true; } }//|   after1 = end: true (6.0/1.0)
      }
      else if(front1 == "end") { if(0 >= threshold) { return true; } }//front1 = end: true (0.0)
      return false;
    }
    static bool Apriori(string front1,string after1,bool moreNeu,double threshold)
    {
      if(front1 == "Nc" && moreNeu == true) { if(1 >= threshold) { return false; } }//    42. front1=Nc moreNeu=true 775 ==> self=false 775    conf:(1)
      if(front1 == "DE" && after1 == "Na" && moreNeu == true) { if(1 >= threshold) { return false; } }//    56. front1=DE after1=Na moreNeu=true 597 ==> self=false 597    conf:(1)
      if(after1 == "SHI" && moreNeu == true) { if(1 >= threshold) { return false; } }//    77. after1=SHI moreNeu=true 472 ==> self=false 472    conf:(1)
      if(front1 == "V_2" && moreNeu == true) { if(1 >= threshold) { return false; } }//    79. front1=V_2 moreNeu=true 457 ==> self=false 457    conf:(1)
      if(front1 == "SHI" && moreNeu == true) { if(1 >= threshold) { return false; } }//    83. front1=SHI moreNeu=true 442 ==> self=false 442    conf:(1)
      if(front1 == "VJ" && moreNeu == true) { if(1 >= threshold) { return false; } }//   167. front1=VJ moreNeu=true 497 ==> self=false 496    conf:(1)
      if(front1 == "Di" && moreNeu == true) { if(1 >= threshold) { return false; } }//   171. front1=Di moreNeu=true 443 ==> self=false 442    conf:(1)
      if(after1 == "Nc" && moreNeu == true) { if(1 >= threshold) { return false; } }//   194. after1=Nc moreNeu=true 661 ==> self=false 658    conf:(1)
      if(front1 == "Na" && moreNeu == true) { if(1 >= threshold) { return false; } }//   196. front1=Na moreNeu=true 1302 ==> self=false 1296    conf:(1)
      if(front1 == "Nd" && after1 == "Nd" && moreNeu == false) { if(0.99 >= threshold) { return true; } }//   203. front1=Nd after1=Nd moreNeu=false 5398 ==> self=true 5366    conf:(0.99)
      if(front1 == "begin" && moreNeu == true) { if(0.99 >= threshold) { return false; } }//   205. front1=begin moreNeu=true 3111 ==> self=false 3091    conf:(0.99)
      if(front1 == "notword" && moreNeu == true) { if(0.99 >= threshold) { return false; } }//   207. front1=notword moreNeu=true 613 ==> self=false 609    conf:(0.99)
      if(after1 == "Na" && moreNeu == true) { if(0.99 >= threshold) { return false; } }//   210. after1=Na moreNeu=true 3829 ==> self=false 3803    conf:(0.99)
      if(front1 == "Nd" && after1 == "Nd") { if(0.99 >= threshold) { return true; } }//   212. front1=Nd after1=Nd 5404 ==> self=true 5367    conf:(0.99)
      if(front1 == "DE" && moreNeu == true) { if(0.99 >= threshold) { return false; } }//   213. front1=DE moreNeu=true 1311 ==> self=false 1302    conf:(0.99)
      if(after1 == "P" && moreNeu == true) { if(0.99 >= threshold) { return false; } }//   221. after1=P moreNeu=true 386 ==> self=false 383    conf:(0.99)
      if(front1 == "begin" && after1 == "Na" && moreNeu == true) { if(0.99 >= threshold) { return false; } }//   223. front1=begin after1=Na moreNeu=true 625 ==> self=false 620    conf:(0.99)
      if(front1 == "Caa" && after1 == "Nd" && moreNeu == false) { if(0.99 >= threshold) { return true; } }//   229. front1=Caa after1=Nd moreNeu=false 560 ==> self=true 555    conf:(0.99)
      if(after1 == "notword" && moreNeu == true) { if(0.99 >= threshold) { return false; } }//   234. after1=notword moreNeu=true 2820 ==> self=false 2793    conf:(0.99)
      if(moreNeu == true) { if(0.99 >= threshold) { return false; } }//   238. moreNeu=true 14511 ==> self=false 14353    conf:(0.99)
      if(front1 == "Nb" && after1 == "Nd" && moreNeu == false) { if(0.99 >= threshold) { return true; } }//   240. front1=Nb after1=Nd moreNeu=false 716 ==> self=true 708    conf:(0.99)
      if(front1 == "V_2" && after1 == "VH") { if(0.99 >= threshold) { return false; } }//   242. front1=V_2 after1=VH 1315 ==> self=false 1300    conf:(0.99)
      if(front1 == "V_2" && after1 == "VH" && moreNeu == false) { if(0.99 >= threshold) { return false; } }//   244. front1=V_2 after1=VH moreNeu=false 1296 ==> self=false 1281    conf:(0.99)
      if(front1 == "V_2" && after1 == "Na") { if(0.99 >= threshold) { return false; } }//   245. front1=V_2 after1=Na 4485 ==> self=false 4433    conf:(0.99)
      if(front1 == "P" && after1 == "Nd" && moreNeu == false) { if(0.99 >= threshold) { return true; } }//   246. front1=P after1=Nd moreNeu=false 6709 ==> self=true 6629    conf:(0.99)
      if(front1 == "V_2" && after1 == "Na" && moreNeu == false) { if(0.99 >= threshold) { return false; } }//   248. front1=V_2 after1=Na moreNeu=false 4314 ==> self=false 4262    conf:(0.99)
      if(after1 == "D" && moreNeu == true) { if(0.99 >= threshold) { return false; } }//   255. after1=D moreNeu=true 707 ==> self=false 698    conf:(0.99)
      if(front1 == "Nep" && after1 == "Na") { if(0.99 >= threshold) { return false; } }//   271. front1=Nep after1=Na 3024 ==> self=false 2979    conf:(0.99)
      if(front1 == "P" && after1 == "Nd") { if(0.98 >= threshold) { return true; } }//   272. front1=P after1=Nd 6730 ==> self=true 6629    conf:(0.98)
      if(front1 == "Nep" && after1 == "Na" && moreNeu == false) { if(0.98 >= threshold) { return false; } }//   274. front1=Nep after1=Na moreNeu=false 2986 ==> self=false 2941    conf:(0.98)
      if(front1 == "Nb" && after1 == "Nd") { if(0.98 >= threshold) { return true; } }//   278. front1=Nb after1=Nd 719 ==> self=true 708    conf:(0.98)
      if(after1 == "VH" && moreNeu == true) { if(0.98 >= threshold) { return false; } }//   292. after1=VH moreNeu=true 451 ==> self=false 443    conf:(0.98)
      if(front1 == "Da" && after1 == "Na") { if(0.98 >= threshold) { return false; } }//   300. front1=Da after1=Na 862 ==> self=false 846    conf:(0.98)
      if(after1 == "DE" && moreNeu == true) { if(0.98 >= threshold) { return false; } }//   302. after1=DE moreNeu=true 1006 ==> self=false 987    conf:(0.98)
      if(front1 == "begin" && after1 == "Cbb" && moreNeu == false) { if(0.98 >= threshold) { return true; } }//   304. front1=begin after1=Cbb moreNeu=false 945 ==> self=true 927    conf:(0.98)
      if(front1 == "Da" && after1 == "Na" && moreNeu == false) { if(0.98 >= threshold) { return false; } }//   307. front1=Da after1=Na moreNeu=false 835 ==> self=false 819    conf:(0.98)
      if(front1 == "Na" && after1 == "Nd" && moreNeu == false) { if(0.98 >= threshold) { return true; } }//   308. front1=Na after1=Nd moreNeu=false 673 ==> self=true 660    conf:(0.98)
      if(front1 == "begin" && after1 == "Nh" && moreNeu == false) { if(0.98 >= threshold) { return true; } }//   310. front1=begin after1=Nh moreNeu=false 2557 ==> self=true 2507    conf:(0.98)
      if(front1 == "Nb" && after1 == "VE" && moreNeu == false) { if(0.98 >= threshold) { return true; } }//   311. front1=Nb after1=VE moreNeu=false 612 ==> self=true 600    conf:(0.98)
      if(front1 == "VC" && moreNeu == true) { if(0.98 >= threshold) { return false; } }//   312. front1=VC moreNeu=true 1059 ==> self=false 1038    conf:(0.98)
      if(front1 == "Caa" && after1 == "Nd") { if(0.98 >= threshold) { return true; } }//   323. front1=Caa after1=Nd 567 ==> self=true 555    conf:(0.98)
      if(front1 == "Nd" && after1 == "Caa" && moreNeu == false) { if(0.98 >= threshold) { return true; } }//   325. front1=Nd after1=Caa moreNeu=false 847 ==> self=true 829    conf:(0.98)
      if(front1 == "notword" && after1 == "Nd" && moreNeu == false) { if(0.98 >= threshold) { return true; } }//   338. front1=notword after1=Nd moreNeu=false 895 ==> self=true 875    conf:(0.98)
      if(front1 == "Nd" && after1 == "Caa") { if(0.98 >= threshold) { return true; } }//   339. front1=Nd after1=Caa 850 ==> self=true 831    conf:(0.98)
      if(front1 == "VC" && after1 == "Na" && moreNeu == true) { if(0.98 >= threshold) { return false; } }//   340. front1=VC after1=Na moreNeu=true 402 ==> self=false 393    conf:(0.98)
      if(front1 == "Nc" && after1 == "VE" && moreNeu == false) { if(0.98 >= threshold) { return true; } }//   343. front1=Nc after1=VE moreNeu=false 532 ==> self=true 520    conf:(0.98)
      if(after1 == "VC" && moreNeu == true) { if(0.98 >= threshold) { return false; } }//   349. after1=VC moreNeu=true 719 ==> self=false 702    conf:(0.98)
      if(front1 == "Da" && after1 == "notword") { if(0.98 >= threshold) { return false; } }//   354. front1=Da after1=notword 1554 ==> self=false 1516    conf:(0.98)
      if(after1 == "Ng" && moreNeu == true) { if(0.98 >= threshold) { return false; } }//   355. after1=Ng moreNeu=true 611 ==> self=false 596    conf:(0.98)
      if(front1 == "begin" && after1 == "Nd" && moreNeu == false) { if(0.98 >= threshold) { return true; } }//   358. front1=begin after1=Nd moreNeu=false 5604 ==> self=true 5465    conf:(0.98)
      if(front1 == "V_2" && after1 == "Nc") { if(0.98 >= threshold) { return false; } }//   359. front1=V_2 after1=Nc 481 ==> self=false 469    conf:(0.98)
      if(front1 == "Da" && after1 == "DE") { if(0.98 >= threshold) { return false; } }//   361. front1=Da after1=DE 520 ==> self=false 507    conf:(0.98)
      if(front1 == "Da" && after1 == "notword" && moreNeu == false) { if(0.97 >= threshold) { return false; } }//   366. front1=Da after1=notword moreNeu=false 1455 ==> self=false 1418    conf:(0.97)
      if(front1 == "Da" && after1 == "DE" && moreNeu == false) { if(0.97 >= threshold) { return false; } }//   370. front1=Da after1=DE moreNeu=false 501 ==> self=false 488    conf:(0.97)
      if(front1 == "V_2" && after1 == "Nc" && moreNeu == false) { if(0.97 >= threshold) { return false; } }//   372. front1=V_2 after1=Nc moreNeu=false 460 ==> self=false 448    conf:(0.97)
      if(front1 == "P" && moreNeu == true) { if(0.97 >= threshold) { return false; } }//   374. front1=P moreNeu=true 965 ==> self=false 939    conf:(0.97)
      if(front1 == "Nb" && after1 == "VE") { if(0.97 >= threshold) { return true; } }//   382. front1=Nb after1=VE 617 ==> self=true 600    conf:(0.97)
      if(front1 == "notword" && after1 == "Nd") { if(0.97 >= threshold) { return true; } }//   402. front1=notword after1=Nd 903 ==> self=true 875    conf:(0.97)
      if(front1 == "begin" && after1 == "Cbb") { if(0.97 >= threshold) { return true; } }//   407. front1=begin after1=Cbb 957 ==> self=true 927    conf:(0.97)
      if(front1 == "Nes" && after1 == "Na") { if(0.97 >= threshold) { return false; } }//   409. front1=Nes after1=Na 3893 ==> self=false 3770    conf:(0.97)
      if(front1 == "Nes" && after1 == "Na" && moreNeu == false) { if(0.97 >= threshold) { return false; } }//   410. front1=Nes after1=Na moreNeu=false 3888 ==> self=false 3765    conf:(0.97)
      if(front1 == "Nc" && after1 == "VE") { if(0.97 >= threshold) { return true; } }//   411. front1=Nc after1=VE 537 ==> self=true 520    conf:(0.97)
      if(front1 == "Na" && after1 == "Nd") { if(0.97 >= threshold) { return true; } }//   417. front1=Na after1=Nd 682 ==> self=true 660    conf:(0.97)
      if(front1 == "V_2") { if(0.97 >= threshold) { return false; } }//   435. front1=V_2 11952 ==> self=false 11543    conf:(0.97)
      if(front1 == "V_2" && after1 == "notword") { if(0.97 >= threshold) { return false; } }//   436. front1=V_2 after1=notword 1825 ==> self=false 1762    conf:(0.97)
      if(front1 == "Cab" && after1 == "Na") { if(0.96 >= threshold) { return false; } }//   440. front1=Cab after1=Na 452 ==> self=false 436    conf:(0.96)
      if(front1 == "V_2" && moreNeu == false) { if(0.96 >= threshold) { return false; } }//   443. front1=V_2 moreNeu=false 11495 ==> self=false 11086    conf:(0.96)
      if(front1 == "V_2" && after1 == "notword" && moreNeu == false) { if(0.96 >= threshold) { return false; } }//   451. front1=V_2 after1=notword moreNeu=false 1684 ==> self=false 1621    conf:(0.96)
      if(front1 == "Cab" && after1 == "Na" && moreNeu == false) { if(0.96 >= threshold) { return false; } }//   453. front1=Cab after1=Na moreNeu=false 425 ==> self=false 409    conf:(0.96)
      if(front1 == "begin" && after1 == "Nd") { if(0.96 >= threshold) { return true; } }//   461. front1=begin after1=Nd 5684 ==> self=true 5465    conf:(0.96)
      if(after1 == "Nd" && moreNeu == false) { if(0.96 >= threshold) { return true; } }//   464. after1=Nd moreNeu=false 25014 ==> self=true 24049    conf:(0.96)
      if(front1 == "Nc" && after1 == "Nd" && moreNeu == false) { if(0.96 >= threshold) { return true; } }//   468. front1=Nc after1=Nd moreNeu=false 487 ==> self=true 468    conf:(0.96)
      if(front1 == "VE" && after1 == "Nd" && moreNeu == false) { if(0.96 >= threshold) { return true; } }//   478. front1=VE after1=Nd moreNeu=false 401 ==> self=true 385    conf:(0.96)
      if(front1 == "begin" && after1 == "Nh") { if(0.96 >= threshold) { return true; } }//   488. front1=begin after1=Nh 2615 ==> self=true 2507    conf:(0.96)
      if(front1 == "Ncd" && after1 == "Na") { if(0.96 >= threshold) { return false; } }//   493. front1=Ncd after1=Na 563 ==> self=false 539    conf:(0.96)
      if(front1 == "Nb" && after1 == "P" && moreNeu == false) { if(0.96 >= threshold) { return true; } }//   498. front1=Nb after1=P moreNeu=false 643 ==> self=true 615    conf:(0.96)
      if(front1 == "Di" && after1 == "VH") { if(0.95 >= threshold) { return false; } }//   511. front1=Di after1=VH 1370 ==> self=false 1307    conf:(0.95)
      if(front1 == "Ncd" && after1 == "Na" && moreNeu == false) { if(0.95 >= threshold) { return false; } }//   514. front1=Ncd after1=Na moreNeu=false 518 ==> self=false 494    conf:(0.95)
      if(front1 == "Di" && after1 == "VH" && moreNeu == false) { if(0.95 >= threshold) { return false; } }//   516. front1=Di after1=VH moreNeu=false 1350 ==> self=false 1287    conf:(0.95)
      if(after1 == "Nd") { if(0.95 >= threshold) { return true; } }//   520. after1=Nd 25235 ==> self=true 24050    conf:(0.95)
      if(front1 == "VE" && after1 == "Nd") { if(0.95 >= threshold) { return true; } }//   522. front1=VE after1=Nd 404 ==> self=true 385    conf:(0.95)
      if(front1 == "Nd" && after1 == "P" && moreNeu == false) { if(0.95 >= threshold) { return true; } }//   525. front1=Nd after1=P moreNeu=false 1733 ==> self=true 1651    conf:(0.95)
      if(front1 == "Nc" && after1 == "Nd") { if(0.95 >= threshold) { return true; } }//   560. front1=Nc after1=Nd 495 ==> self=true 468    conf:(0.95)
      if(front1 == "Nep" && after1 == "VH") { if(0.94 >= threshold) { return false; } }//   565. front1=Nep after1=VH 434 ==> self=false 410    conf:(0.94)
      if(front1 == "Nd" && after1 == "P") { if(0.94 >= threshold) { return true; } }//   566. front1=Nd after1=P 1749 ==> self=true 1652    conf:(0.94)
      if(front1 == "Nep" && after1 == "VH" && moreNeu == false) { if(0.94 >= threshold) { return false; } }//   568. front1=Nep after1=VH moreNeu=false 431 ==> self=false 407    conf:(0.94)
      if(front1 == "Di" && after1 == "Na") { if(0.94 >= threshold) { return false; } }//   569. front1=Di after1=Na 4454 ==> self=false 4205    conf:(0.94)
      if(front1 == "Da") { if(0.94 >= threshold) { return false; } }//   571. front1=Da 4072 ==> self=false 3843    conf:(0.94)
      if(front1 == "V_2" && after1 == "D") { if(0.94 >= threshold) { return false; } }//   573. front1=V_2 after1=D 438 ==> self=false 413    conf:(0.94)
      if(front1 == "Di" && after1 == "Na" && moreNeu == false) { if(0.94 >= threshold) { return false; } }//   576. front1=Di after1=Na moreNeu=false 4310 ==> self=false 4062    conf:(0.94)
      if(front1 == "V_2" && after1 == "D" && moreNeu == false) { if(0.94 >= threshold) { return false; } }//   578. front1=V_2 after1=D moreNeu=false 434 ==> self=false 409    conf:(0.94)
      if(front1 == "Da" && moreNeu == false) { if(0.94 >= threshold) { return false; } }//   581. front1=Da moreNeu=false 3896 ==> self=false 3669    conf:(0.94)
      if(front1 == "Nd" && after1 == "VC" && moreNeu == false) { if(0.94 >= threshold) { return true; } }//   588. front1=Nd after1=VC moreNeu=false 1704 ==> self=true 1601    conf:(0.94)
      if(front1 == "Nb" && after1 == "P") { if(0.94 >= threshold) { return true; } }//   591. front1=Nb after1=P 655 ==> self=true 615    conf:(0.94)
      if(front1 == "Nc" && after1 == "D" && moreNeu == false) { if(0.94 >= threshold) { return true; } }//   612. front1=Nc after1=D moreNeu=false 971 ==> self=true 908    conf:(0.94)
      if(front1 == "Nep") { if(0.93 >= threshold) { return false; } }//   613. front1=Nep 8006 ==> self=false 7484    conf:(0.93)
      if(front1 == "Nep" && moreNeu == false) { if(0.93 >= threshold) { return false; } }//   616. front1=Nep moreNeu=false 7896 ==> self=false 7374    conf:(0.93)
      if(front1 == "Cab") { if(0.93 >= threshold) { return false; } }//   633. front1=Cab 915 ==> self=false 850    conf:(0.93)
      if(front1 == "Nb" && after1 == "D" && moreNeu == false) { if(0.93 >= threshold) { return true; } }//   634. front1=Nb after1=D moreNeu=false 735 ==> self=true 682    conf:(0.93)
      if(front1 == "SHI" && after1 == "VH") { if(0.93 >= threshold) { return false; } }//   636. front1=SHI after1=VH 1887 ==> self=false 1750    conf:(0.93)
      if(front1 == "SHI" && after1 == "VH" && moreNeu == false) { if(0.93 >= threshold) { return false; } }//   639. front1=SHI after1=VH moreNeu=false 1879 ==> self=false 1742    conf:(0.93)
      if(front1 == "begin" && after1 == "Nb" && moreNeu == false) { if(0.93 >= threshold) { return true; } }//   642. front1=begin after1=Nb moreNeu=false 1648 ==> self=true 1527    conf:(0.93)
      if(front1 == "Nd" && after1 == "VC") { if(0.93 >= threshold) { return true; } }//   644. front1=Nd after1=VC 1732 ==> self=true 1603    conf:(0.93)
      if(front1 == "Cab" && moreNeu == false) { if(0.92 >= threshold) { return false; } }//   647. front1=Cab moreNeu=false 866 ==> self=false 801    conf:(0.92)
      if(front1 == "Nc" && after1 == "D") { if(0.92 >= threshold) { return true; } }//   648. front1=Nc after1=D 982 ==> self=true 908    conf:(0.92)
      if(front1 == "Nc" && after1 == "P" && moreNeu == false) { if(0.92 >= threshold) { return true; } }//   655. front1=Nc after1=P moreNeu=false 404 ==> self=true 373    conf:(0.92)
      if(front1 == "VG" && after1 == "notword") { if(0.92 >= threshold) { return false; } }//   656. front1=VG after1=notword 1980 ==> self=false 1828    conf:(0.92)
      if(front1 == "begin" && after1 == "Nes" && moreNeu == false) { if(0.92 >= threshold) { return true; } }//   665. front1=begin after1=Nes moreNeu=false 619 ==> self=true 570    conf:(0.92)
      if(front1 == "begin" && after1 == "Neqa" && moreNeu == false) { if(0.92 >= threshold) { return true; } }//   671. front1=begin after1=Neqa moreNeu=false 609 ==> self=true 560    conf:(0.92)
      if(front1 == "VG" && after1 == "notword" && moreNeu == false) { if(0.92 >= threshold) { return false; } }//   672. front1=VG after1=notword moreNeu=false 1867 ==> self=false 1715    conf:(0.92)
      if(front1 == "VD" && after1 == "Na") { if(0.92 >= threshold) { return false; } }//   673. front1=VD after1=Na 429 ==> self=false 394    conf:(0.92)
      if(front1 == "VD" && after1 == "Na" && moreNeu == false) { if(0.92 >= threshold) { return false; } }//   678. front1=VD after1=Na moreNeu=false 421 ==> self=false 386    conf:(0.92)
      if(front1 == "VA" && after1 == "Na") { if(0.92 >= threshold) { return false; } }//   679. front1=VA after1=Na 444 ==> self=false 407    conf:(0.92)
      if(front1 == "Nb" && after1 == "D") { if(0.92 >= threshold) { return true; } }//   683. front1=Nb after1=D 745 ==> self=true 682    conf:(0.92)
      if(front1 == "P" && after1 == "Nes" && moreNeu == false) { if(0.91 >= threshold) { return true; } }//   686. front1=P after1=Nes moreNeu=false 421 ==> self=true 385    conf:(0.91)
      if(front1 == "Cbb" && after1 == "DE" && moreNeu == false) { if(0.91 >= threshold) { return true; } }//   687. front1=Cbb after1=DE moreNeu=false 608 ==> self=true 556    conf:(0.91)
      if(front1 == "VA" && after1 == "Na" && moreNeu == false) { if(0.91 >= threshold) { return false; } }//   689. front1=VA after1=Na moreNeu=false 428 ==> self=false 391    conf:(0.91)
      if(front1 == "Nd" && after1 == "notword" && moreNeu == false) { if(0.91 >= threshold) { return true; } }//   691. front1=Nd after1=notword moreNeu=false 5019 ==> self=true 4584    conf:(0.91)
      if(front1 == "Nd" && after1 == "notword") { if(0.91 >= threshold) { return true; } }//   699. front1=Nd after1=notword 5052 ==> self=true 4596    conf:(0.91)
      if(front1 == "Nep" && after1 == "D") { if(0.91 >= threshold) { return false; } }//   701. front1=Nep after1=D 544 ==> self=false 494    conf:(0.91)
      if(front1 == "Nep" && after1 == "D" && moreNeu == false) { if(0.91 >= threshold) { return false; } }//   705. front1=Nep after1=D moreNeu=false 535 ==> self=false 485    conf:(0.91)
      if(front1 == "Nb" && after1 == "VC" && moreNeu == false) { if(0.91 >= threshold) { return true; } }//   710. front1=Nb after1=VC moreNeu=false 500 ==> self=true 453    conf:(0.91)
      if(front1 == "VD") { if(0.91 >= threshold) { return false; } }//   713. front1=VD 1288 ==> self=false 1166    conf:(0.91)
      if(front1 == "Cbb" && after1 == "D" && moreNeu == false) { if(0.9 >= threshold) { return true; } }//   715. front1=Cbb after1=D moreNeu=false 1071 ==> self=true 969    conf:(0.9)
      if(front1 == "VG" && after1 == "VH") { if(0.9 >= threshold) { return false; } }//   717. front1=VG after1=VH 828 ==> self=false 749    conf:(0.9)
      if(front1 == "begin" && after1 == "D" && moreNeu == false) { if(0.9 >= threshold) { return true; } }//   718. front1=begin after1=D moreNeu=false 9118 ==> self=true 8243    conf:(0.9)
      if(front1 == "VG" && after1 == "VH" && moreNeu == false) { if(0.9 >= threshold) { return false; } }//   720. front1=VG after1=VH moreNeu=false 819 ==> self=false 740    conf:(0.9)
      if(front1 == "begin" && after1 == "Nes") { if(0.9 >= threshold) { return true; } }//   722. front1=begin after1=Nes 631 ==> self=true 570    conf:(0.9)
      if(front1 == "VD" && moreNeu == false) { if(0.9 >= threshold) { return false; } }//   725. front1=VD moreNeu=false 1251 ==> self=false 1129    conf:(0.9)
      if(front1 == "begin" && after1 == "Neqa") { if(0.9 >= threshold) { return true; } }//   730. front1=begin after1=Neqa 622 ==> self=true 560    conf:(0.9)
      if(front1 == "Nd" && after1 == "VA" && moreNeu == false) { if(0.9 >= threshold) { return true; } }//   733. front1=Nd after1=VA moreNeu=false 528 ==> self=true 475    conf:(0.9)
      if(front1 == "Nc" && after1 == "VC" && moreNeu == false) { if(0.9 >= threshold) { return true; } }//   737. front1=Nc after1=VC moreNeu=false 937 ==> self=true 842    conf:(0.9)
      if(front1 == "VC" && after1 == "VH") { if(0.9 >= threshold) { return false; } }//   738. front1=VC after1=VH 2050 ==> self=false 1842    conf:(0.9)
      if(front1 == "VC" && after1 == "VH" && moreNeu == false) { if(0.9 >= threshold) { return false; } }//   739. front1=VC after1=VH moreNeu=false 2019 ==> self=false 1811    conf:(0.9)
      if(front1 == "Nd" && moreNeu == false) { if(0.9 >= threshold) { return true; } }//   740. front1=Nd moreNeu=false 26761 ==> self=true 23993    conf:(0.9)
      if(front1 == "begin" && after1 == "P" && moreNeu == false) { if(0.9 >= threshold) { return true; } }//   741. front1=begin after1=P moreNeu=false 3470 ==> self=true 3111    conf:(0.9)
      if(front1 == "Cbb" && after1 == "DE") { if(0.9 >= threshold) { return true; } }//   742. front1=Cbb after1=DE 621 ==> self=true 556    conf:(0.9)
      if(front1 == "begin" && after1 == "Nb") { if(0.9 >= threshold) { return true; } }//   744. front1=begin after1=Nb 1706 ==> self=true 1527    conf:(0.9)
      if(front1 == "begin" && after1 == "V_2" && moreNeu == false) { if(0.89 >= threshold) { return true; } }//   746. front1=begin after1=V_2 moreNeu=false 596 ==> self=true 533    conf:(0.89)
      if(front1 == "DE" && after1 == "VH") { if(0.89 >= threshold) { return false; } }//   749. front1=DE after1=VH 810 ==> self=false 723    conf:(0.89)
      if(front1 == "VH" && after1 == "Na") { if(0.89 >= threshold) { return false; } }//   752. front1=VH after1=Na 1104 ==> self=false 984    conf:(0.89)
      if(front1 == "P" && after1 == "Nes") { if(0.89 >= threshold) { return true; } }//   753. front1=P after1=Nes 432 ==> self=true 385    conf:(0.89)
      if(front1 == "VH" && after1 == "Na" && moreNeu == false) { if(0.89 >= threshold) { return false; } }//   758. front1=VH after1=Na moreNeu=false 1084 ==> self=false 964    conf:(0.89)
      if(front1 == "Cbb" && after1 == "D") { if(0.89 >= threshold) { return true; } }//   760. front1=Cbb after1=D 1091 ==> self=true 969    conf:(0.89)
      if(front1 == "Nd") { if(0.89 >= threshold) { return true; } }//   762. front1=Nd 27088 ==> self=true 24050    conf:(0.89)
      if(front1 == "DE" && after1 == "VH" && moreNeu == false) { if(0.89 >= threshold) { return false; } }//   763. front1=DE after1=VH moreNeu=false 772 ==> self=false 685    conf:(0.89)
      if(front1 == "Nep" && after1 == "notword") { if(0.89 >= threshold) { return false; } }//   765. front1=Nep after1=notword 1200 ==> self=false 1063    conf:(0.89)
      if(front1 == "begin" && after1 == "Nep" && moreNeu == false) { if(0.89 >= threshold) { return true; } }//   767. front1=begin after1=Nep moreNeu=false 420 ==> self=true 372    conf:(0.89)
      if(front1 == "Nep" && after1 == "notword" && moreNeu == false) { if(0.89 >= threshold) { return false; } }//   768. front1=Nep after1=notword moreNeu=false 1197 ==> self=false 1060    conf:(0.89)
      if(front1 == "begin" && after1 == "VE" && moreNeu == false) { if(0.88 >= threshold) { return true; } }//   772. front1=begin after1=VE moreNeu=false 840 ==> self=true 742    conf:(0.88)
      if(front1 == "SHI" && after1 == "Nd" && moreNeu == false) { if(0.88 >= threshold) { return true; } }//   773. front1=SHI after1=Nd moreNeu=false 437 ==> self=true 386    conf:(0.88)
      if(front1 == "Nd" && after1 == "VA") { if(0.88 >= threshold) { return true; } }//   774. front1=Nd after1=VA 540 ==> self=true 476    conf:(0.88)
      if(front1 == "VCL" && after1 == "Na") { if(0.88 >= threshold) { return false; } }//   775. front1=VCL after1=Na 588 ==> self=false 518    conf:(0.88)
      if(front1 == "begin" && after1 == "Nep") { if(0.88 >= threshold) { return true; } }//   778. front1=begin after1=Nep 423 ==> self=true 372    conf:(0.88)
      if(front1 == "begin" && after1 == "notword" && moreNeu == false) { if(0.88 >= threshold) { return true; } }//   780. front1=begin after1=notword moreNeu=false 5825 ==> self=true 5115    conf:(0.88)
      if(front1 == "Nh" && after1 == "Na") { if(0.88 >= threshold) { return false; } }//   782. front1=Nh after1=Na 1129 ==> self=false 991    conf:(0.88)
      if(front1 == "Nd" && after1 == "D" && moreNeu == false) { if(0.88 >= threshold) { return true; } }//   783. front1=Nd after1=D moreNeu=false 1273 ==> self=true 1117    conf:(0.88)
      if(front1 == "VJ" && after1 == "notword") { if(0.88 >= threshold) { return false; } }//   785. front1=VJ after1=notword 3285 ==> self=false 2882    conf:(0.88)
      if(front1 == "SHI" && after1 == "Nd") { if(0.88 >= threshold) { return true; } }//   786. front1=SHI after1=Nd 440 ==> self=true 386    conf:(0.88)
      if(front1 == "VC" && after1 == "Na") { if(0.88 >= threshold) { return false; } }//   790. front1=VC after1=Na 8433 ==> self=false 7384    conf:(0.88)
      if(front1 == "Nd" && after1 == "D") { if(0.88 >= threshold) { return true; } }//   792. front1=Nd after1=D 1283 ==> self=true 1123    conf:(0.88)
      if(front1 == "begin" && after1 == "D") { if(0.88 >= threshold) { return true; } }//   793. front1=begin after1=D 9421 ==> self=true 8246    conf:(0.88)
      if(front1 == "begin" && after1 == "V_2") { if(0.88 >= threshold) { return true; } }//   794. front1=begin after1=V_2 609 ==> self=true 533    conf:(0.88)
      if(front1 == "VCL" && after1 == "Na" && moreNeu == false) { if(0.88 >= threshold) { return false; } }//   796. front1=VCL after1=Na moreNeu=false 560 ==> self=false 490    conf:(0.88)
      if(front1 == "Nes" && after1 == "VH") { if(0.87 >= threshold) { return false; } }//   798. front1=Nes after1=VH 526 ==> self=false 460    conf:(0.87)
      if(front1 == "Nes" && after1 == "VH" && moreNeu == false) { if(0.87 >= threshold) { return false; } }//   799. front1=Nes after1=VH moreNeu=false 525 ==> self=false 459    conf:(0.87)
      if(front1 == "VG" && after1 == "Na") { if(0.87 >= threshold) { return false; } }//   800. front1=VG after1=Na 2118 ==> self=false 1851    conf:(0.87)
      if(front1 == "begin" && after1 == "Nc" && moreNeu == false) { if(0.87 >= threshold) { return true; } }//   801. front1=begin after1=Nc moreNeu=false 3766 ==> self=true 3291    conf:(0.87)
      if(front1 == "Na" && after1 == "notword") { if(0.87 >= threshold) { return false; } }//   805. front1=Na after1=notword 4587 ==> self=false 4001    conf:(0.87)
      if(front1 == "VG" && after1 == "Na" && moreNeu == false) { if(0.87 >= threshold) { return false; } }//   807. front1=VG after1=Na moreNeu=false 2087 ==> self=false 1820    conf:(0.87)
      if(front1 == "VJ" && after1 == "Na") { if(0.87 >= threshold) { return false; } }//   808. front1=VJ after1=Na 2435 ==> self=false 2123    conf:(0.87)
      if(front1 == "Nh" && after1 == "D" && moreNeu == false) { if(0.87 >= threshold) { return true; } }//   811. front1=Nh after1=D moreNeu=false 1407 ==> self=true 1225    conf:(0.87)
      if(front1 == "VC" && after1 == "Na" && moreNeu == false) { if(0.87 >= threshold) { return false; } }//   813. front1=VC after1=Na moreNeu=false 8031 ==> self=false 6991    conf:(0.87)
      if(front1 == "begin" && after1 == "P") { if(0.87 >= threshold) { return true; } }//   815. front1=begin after1=P 3576 ==> self=true 3111    conf:(0.87)
      if(front1 == "Nd" && after1 == "Ng" && moreNeu == false) { if(0.87 >= threshold) { return true; } }//   817. front1=Nd after1=Ng moreNeu=false 2637 ==> self=true 2294    conf:(0.87)
      if(front1 == "Nb" && after1 == "VC") { if(0.87 >= threshold) { return true; } }//   819. front1=Nb after1=VC 521 ==> self=true 453    conf:(0.87)
      if(front1 == "Nh" && after1 == "Na" && moreNeu == false) { if(0.87 >= threshold) { return false; } }//   823. front1=Nh after1=Na moreNeu=false 1056 ==> self=false 918    conf:(0.87)
      if(front1 == "VJ" && after1 == "notword" && moreNeu == false) { if(0.87 >= threshold) { return false; } }//   825. front1=VJ after1=notword moreNeu=false 3064 ==> self=false 2661    conf:(0.87)
      if(front1 == "D" && after1 == "Na") { if(0.87 >= threshold) { return false; } }//   826. front1=D after1=Na 1080 ==> self=false 937    conf:(0.87)
      if(front1 == "Na" && after1 == "P" && moreNeu == false) { if(0.87 >= threshold) { return true; } }//   830. front1=Na after1=P moreNeu=false 886 ==> self=true 768    conf:(0.87)
      if(front1 == "VJ" && after1 == "Na" && moreNeu == false) { if(0.87 >= threshold) { return false; } }//   833. front1=VJ after1=Na moreNeu=false 2328 ==> self=false 2016    conf:(0.87)
      if(front1 == "Nd" && after1 == "Ng") { if(0.87 >= threshold) { return true; } }//   834. front1=Nd after1=Ng 2661 ==> self=true 2303    conf:(0.87)
      if(front1 == "Nes") { if(0.87 >= threshold) { return false; } }//   835. front1=Nes 9882 ==> self=false 8548    conf:(0.87)
      if(front1 == "Nes" && moreNeu == false) { if(0.86 >= threshold) { return false; } }//   836. front1=Nes moreNeu=false 9861 ==> self=false 8527    conf:(0.86)
      if(front1 == "SHI" && after1 == "Dfa") { if(0.86 >= threshold) { return false; } }//   837. front1=SHI after1=Dfa 715 ==> self=false 618    conf:(0.86)
      if(front1 == "D" && after1 == "Na" && moreNeu == false) { if(0.86 >= threshold) { return false; } }//   838. front1=D after1=Na moreNeu=false 1054 ==> self=false 911    conf:(0.86)
      if(front1 == "SHI" && after1 == "Dfa" && moreNeu == false) { if(0.86 >= threshold) { return false; } }//   839. front1=SHI after1=Dfa moreNeu=false 714 ==> self=false 617    conf:(0.86)
      if(front1 == "Ncd") { if(0.86 >= threshold) { return false; } }//   840. front1=Ncd 1594 ==> self=false 1376    conf:(0.86)
      if(front1 == "begin" && after1 == "Da" && moreNeu == false) { if(0.86 >= threshold) { return true; } }//   841. front1=begin after1=Da moreNeu=false 716 ==> self=true 618    conf:(0.86)
      if(front1 == "Na" && after1 == "notword" && moreNeu == false) { if(0.86 >= threshold) { return false; } }//   846. front1=Na after1=notword moreNeu=false 4239 ==> self=false 3653    conf:(0.86)
      if(front1 == "Nc" && after1 == "P") { if(0.86 >= threshold) { return true; } }//   848. front1=Nc after1=P 434 ==> self=true 373    conf:(0.86)
      if(front1 == "Nes" && after1 == "DE") { if(0.86 >= threshold) { return false; } }//   853. front1=Nes after1=DE 494 ==> self=false 424    conf:(0.86)
      if(front1 == "Nes" && after1 == "DE" && moreNeu == false) { if(0.86 >= threshold) { return false; } }//   854. front1=Nes after1=DE moreNeu=false 494 ==> self=false 424    conf:(0.86)
      if(front1 == "VC" && after1 == "Nd" && moreNeu == false) { if(0.86 >= threshold) { return true; } }//   856. front1=VC after1=Nd moreNeu=false 562 ==> self=true 481    conf:(0.86)
      if(front1 == "P" && after1 == "D" && moreNeu == false) { if(0.85 >= threshold) { return true; } }//   858. front1=P after1=D moreNeu=false 2107 ==> self=true 1799    conf:(0.85)
      if(front1 == "Cbb" && after1 == "Nc" && moreNeu == false) { if(0.85 >= threshold) { return true; } }//   859. front1=Cbb after1=Nc moreNeu=false 492 ==> self=true 420    conf:(0.85)
      if(front1 == "Nd" && after1 == "VH" && moreNeu == false) { if(0.85 >= threshold) { return true; } }//   860. front1=Nd after1=VH moreNeu=false 721 ==> self=true 615    conf:(0.85)
      if(front1 == "SHI" && after1 == "Na") { if(0.85 >= threshold) { return false; } }//   861. front1=SHI after1=Na 3501 ==> self=false 2986    conf:(0.85)
      if(front1 == "Nd" && after1 == "Nc" && moreNeu == false) { if(0.85 >= threshold) { return true; } }//   862. front1=Nd after1=Nc moreNeu=false 712 ==> self=true 607    conf:(0.85)
      if(front1 == "Ncd" && moreNeu == false) { if(0.85 >= threshold) { return false; } }//   863. front1=Ncd moreNeu=false 1475 ==> self=false 1257    conf:(0.85)
      if(front1 == "Nh" && after1 == "D") { if(0.85 >= threshold) { return true; } }//   864. front1=Nh after1=D 1438 ==> self=true 1225    conf:(0.85)
      if(front1 == "P" && after1 == "DE" && moreNeu == false) { if(0.85 >= threshold) { return true; } }//   866. front1=P after1=DE moreNeu=false 3047 ==> self=true 2594    conf:(0.85)
      if(front1 == "DE" && after1 == "Nc") { if(0.85 >= threshold) { return false; } }//   867. front1=DE after1=Nc 601 ==> self=false 511    conf:(0.85)
      if(front1 == "SHI" && after1 == "Na" && moreNeu == false) { if(0.85 >= threshold) { return false; } }//   868. front1=SHI after1=Na moreNeu=false 3434 ==> self=false 2919    conf:(0.85)
      if(front1 == "VC" && after1 == "Nd") { if(0.85 >= threshold) { return true; } }//   869. front1=VC after1=Nd 566 ==> self=true 481    conf:(0.85)
      if(after1 == "Nes" && moreNeu == false) { if(0.85 >= threshold) { return true; } }//   871. after1=Nes moreNeu=false 1732 ==> self=true 1471    conf:(0.85)
      if(front1 == "begin" && after1 == "Da") { if(0.85 >= threshold) { return true; } }//   872. front1=begin after1=Da 729 ==> self=true 618    conf:(0.85)
      if(front1 == "Nd" && after1 == "VH") { if(0.84 >= threshold) { return true; } }//   876. front1=Nd after1=VH 733 ==> self=true 619    conf:(0.84)
      if(front1 == "DE" && after1 == "Na") { if(0.84 >= threshold) { return false; } }//   879. front1=DE after1=Na 6187 ==> self=false 5213    conf:(0.84)
      if(front1 == "Nc" && after1 == "VC") { if(0.84 >= threshold) { return true; } }//   880. front1=Nc after1=VC 1000 ==> self=true 842    conf:(0.84)
      if(front1 == "Cbb" && after1 == "Nc") { if(0.84 >= threshold) { return true; } }//   882. front1=Cbb after1=Nc 499 ==> self=true 420    conf:(0.84)
      if(front1 == "Nd" && after1 == "Nc") { if(0.84 >= threshold) { return true; } }//   884. front1=Nd after1=Nc 723 ==> self=true 608    conf:(0.84)
      if(front1 == "Na" && after1 == "D" && moreNeu == false) { if(0.84 >= threshold) { return true; } }//   885. front1=Na after1=D moreNeu=false 1787 ==> self=true 1501    conf:(0.84)
      if(front1 == "VJ" && after1 == "VH") { if(0.84 >= threshold) { return false; } }//   886. front1=VJ after1=VH 618 ==> self=false 519    conf:(0.84)
      if(front1 == "begin" && after1 == "Nc") { if(0.84 >= threshold) { return true; } }//   888. front1=begin after1=Nc 3920 ==> self=true 3291    conf:(0.84)
      if(front1 == "P" && after1 == "D") { if(0.84 >= threshold) { return true; } }//   890. front1=P after1=D 2143 ==> self=true 1799    conf:(0.84)
      if(front1 == "begin" && after1 == "Dfa" && moreNeu == false) { if(0.84 >= threshold) { return true; } }//   894. front1=begin after1=Dfa moreNeu=false 474 ==> self=true 397    conf:(0.84)
      if(front1 == "VJ" && after1 == "VH" && moreNeu == false) { if(0.84 >= threshold) { return false; } }//   895. front1=VJ after1=VH moreNeu=false 606 ==> self=false 507    conf:(0.84)
      if(front1 == "begin" && after1 == "notword") { if(0.84 >= threshold) { return true; } }//   896. front1=begin after1=notword 6128 ==> self=true 5117    conf:(0.84)
      if(front1 == "Na" && after1 == "P") { if(0.83 >= threshold) { return true; } }//   898. front1=Na after1=P 921 ==> self=true 768    conf:(0.83)
      if(after1 == "Cbb" && moreNeu == false) { if(0.83 >= threshold) { return true; } }//   902. after1=Cbb moreNeu=false 1808 ==> self=true 1506    conf:(0.83)
      if(front1 == "P" && after1 == "DE") { if(0.83 >= threshold) { return true; } }//   903. front1=P after1=DE 3125 ==> self=true 2602    conf:(0.83)
      if(front1 == "Di") { if(0.83 >= threshold) { return false; } }//   904. front1=Di 13046 ==> self=false 10846    conf:(0.83)
      if(front1 == "begin" && after1 == "DE" && moreNeu == false) { if(0.83 >= threshold) { return true; } }//   905. front1=begin after1=DE moreNeu=false 3306 ==> self=true 2745    conf:(0.83)
      if(front1 == "begin" && after1 == "Dfa") { if(0.83 >= threshold) { return true; } }//   909. front1=begin after1=Dfa 479 ==> self=true 397    conf:(0.83)
      if(front1 == "Nes" && after1 == "D") { if(0.83 >= threshold) { return false; } }//   912. front1=Nes after1=D 813 ==> self=false 673    conf:(0.83)
      if(front1 == "P" && after1 == "VC" && moreNeu == false) { if(0.83 >= threshold) { return true; } }//   913. front1=P after1=VC moreNeu=false 2131 ==> self=true 1764    conf:(0.83)
      if(front1 == "Nes" && after1 == "D" && moreNeu == false) { if(0.83 >= threshold) { return false; } }//   915. front1=Nes after1=D moreNeu=false 812 ==> self=false 672    conf:(0.83)
      if(front1 == "DE" && after1 == "Nc" && moreNeu == false) { if(0.83 >= threshold) { return false; } }//   916. front1=DE after1=Nc moreNeu=false 521 ==> self=false 431    conf:(0.83)
      if(front1 == "DE" && after1 == "Na" && moreNeu == false) { if(0.83 >= threshold) { return false; } }//   918. front1=DE after1=Na moreNeu=false 5590 ==> self=false 4616    conf:(0.83)
      if(front1 == "Di" && moreNeu == false) { if(0.83 >= threshold) { return false; } }//   919. front1=Di moreNeu=false 12603 ==> self=false 10404    conf:(0.83)
      if(front1 == "begin" && after1 == "VE") { if(0.83 >= threshold) { return true; } }//   920. front1=begin after1=VE 899 ==> self=true 742    conf:(0.83)
      if(front1 == "SHI" && after1 == "DE" && moreNeu == false) { if(0.82 >= threshold) { return true; } }//   922. front1=SHI after1=DE moreNeu=false 483 ==> self=true 398    conf:(0.82)
      if(after1 == "Nes") { if(0.82 >= threshold) { return true; } }//   923. after1=Nes 1787 ==> self=true 1472    conf:(0.82)
      if(front1 == "begin" && after1 == "VC" && moreNeu == false) { if(0.82 >= threshold) { return true; } }//   925. front1=begin after1=VC moreNeu=false 2787 ==> self=true 2289    conf:(0.82)
      if(after1 == "Cbb") { if(0.82 >= threshold) { return true; } }//   927. after1=Cbb 1837 ==> self=true 1506    conf:(0.82)
      if(after1 == "VE" && moreNeu == false) { if(0.82 >= threshold) { return true; } }//   929. after1=VE moreNeu=false 4455 ==> self=true 3648    conf:(0.82)
      if(front1 == "P" && after1 == "P" && moreNeu == false) { if(0.82 >= threshold) { return true; } }//   930. front1=P after1=P moreNeu=false 1098 ==> self=true 897    conf:(0.82)
      if(front1 == "VC" && after1 == "notword") { if(0.82 >= threshold) { return false; } }//   931. front1=VC after1=notword 4164 ==> self=false 3396    conf:(0.82)
      if(front1 == "Nh" && after1 == "DE" && moreNeu == false) { if(0.81 >= threshold) { return true; } }//   933. front1=Nh after1=DE moreNeu=false 597 ==> self=true 486    conf:(0.81)
      if(front1 == "begin" && after1 == "VCL" && moreNeu == false) { if(0.81 >= threshold) { return true; } }//   934. front1=begin after1=VCL moreNeu=false 632 ==> self=true 514    conf:(0.81)
      if(after1 == "Nh" && moreNeu == false) { if(0.81 >= threshold) { return true; } }//   937. after1=Nh moreNeu=false 5071 ==> self=true 4115    conf:(0.81)
      if(front1 == "VJ") { if(0.81 >= threshold) { return false; } }//   940. front1=VJ 10435 ==> self=false 8433    conf:(0.81)
      if(front1 == "Na" && after1 == "D") { if(0.81 >= threshold) { return true; } }//   942. front1=Na after1=D 1861 ==> self=true 1501    conf:(0.81)
      if(front1 == "VC" && after1 == "notword" && moreNeu == false) { if(0.81 >= threshold) { return false; } }//   944. front1=VC after1=notword moreNeu=false 3961 ==> self=false 3193    conf:(0.81)
      if(front1 == "Nc" && after1 == "Nc") { if(0.8 >= threshold) { return false; } }//   946. front1=Nc after1=Nc 540 ==> self=false 434    conf:(0.8)
      if(after1 == "Neqb") { if(0.8 >= threshold) { return false; } }//   948. after1=Neqb 2170 ==> self=false 1741    conf:(0.8)
      if(front1 == "P" && after1 == "VC") { if(0.8 >= threshold) { return true; } }//   949. front1=P after1=VC 2206 ==> self=true 1769    conf:(0.8)
      if(after1 == "Neqb" && moreNeu == false) { if(0.8 >= threshold) { return false; } }//   950. after1=Neqb moreNeu=false 2165 ==> self=false 1736    conf:(0.8)
      if(front1 == "P" && after1 == "P") { if(0.8 >= threshold) { return true; } }//   953. front1=P after1=P 1122 ==> self=true 897    conf:(0.8)
      if(front1 == "begin" && after1 == "DE") { if(0.8 >= threshold) { return true; } }//   955. front1=begin after1=DE 3440 ==> self=true 2748    conf:(0.8)
      if(front1 == "VJ" && moreNeu == false) { if(0.8 >= threshold) { return false; } }//   956. front1=VJ moreNeu=false 9938 ==> self=false 7937    conf:(0.8)
      if(front1 == "Nh" && after1 == "DE") { if(0.8 >= threshold) { return true; } }//   959. front1=Nh after1=DE 610 ==> self=true 486    conf:(0.8)
      if(front1 == "Nes" && after1 == "notword") { if(0.8 >= threshold) { return false; } }//   962. front1=Nes after1=notword 897 ==> self=false 714    conf:(0.8)
      if(after1 == "Nh") { if(0.8 >= threshold) { return true; } }//   963. after1=Nh 5170 ==> self=true 4115    conf:(0.8)
      if(front1 == "Nes" && after1 == "notword" && moreNeu == false) { if(0.8 >= threshold) { return false; } }//   965. front1=Nes after1=notword moreNeu=false 896 ==> self=false 713    conf:(0.8)
      if(front1 == "SHI" && after1 == "DE") { if(0.79 >= threshold) { return true; } }//   967. front1=SHI after1=DE 502 ==> self=true 398    conf:(0.79)
      if(front1 == "begin" && after1 == "VA" && moreNeu == false) { if(0.79 >= threshold) { return true; } }//   969. front1=begin after1=VA moreNeu=false 1033 ==> self=true 816    conf:(0.79)
      if(front1 == "VG") { if(0.79 >= threshold) { return false; } }//   970. front1=VG 7697 ==> self=false 6070    conf:(0.79)
      if(front1 == "P" && after1 == "Neu" && moreNeu == false) { if(0.79 >= threshold) { return true; } }//   972. front1=P after1=Neu moreNeu=false 993 ==> self=true 782    conf:(0.79)
      if(front1 == "Nf" && after1 == "DE") { if(0.79 >= threshold) { return false; } }//   973. front1=Nf after1=DE 1054 ==> self=false 829    conf:(0.79)
      if(after1 == "Na") { if(0.79 >= threshold) { return false; } }//   974. after1=Na 79043 ==> self=false 62084    conf:(0.79)
      if(front1 == "VC") { if(0.78 >= threshold) { return false; } }//   975. front1=VC 24180 ==> self=false 18946    conf:(0.78)
      if(front1 == "begin" && moreNeu == false) { if(0.78 >= threshold) { return true; } }//   976. front1=begin moreNeu=false 68206 ==> self=true 53350    conf:(0.78)
      if(front1 == "begin" && after1 == "VC") { if(0.78 >= threshold) { return true; } }//   977. front1=begin after1=VC 2928 ==> self=true 2289    conf:(0.78)
      if(front1 == "VG" && moreNeu == false) { if(0.78 >= threshold) { return false; } }//   979. front1=VG moreNeu=false 7452 ==> self=false 5825    conf:(0.78)
      if(after1 == "P" && moreNeu == false) { if(0.78 >= threshold) { return true; } }//   980. after1=P moreNeu=false 12050 ==> self=true 9418    conf:(0.78)
      if(front1 == "VA" && after1 == "notword") { if(0.78 >= threshold) { return false; } }//   981. front1=VA after1=notword 1341 ==> self=false 1048    conf:(0.78)
      if(front1 == "VA") { if(0.78 >= threshold) { return false; } }//   982. front1=VA 3037 ==> self=false 2372    conf:(0.78)
      if(after1 == "Na" && moreNeu == false) { if(0.77 >= threshold) { return false; } }//   984. after1=Na moreNeu=false 75214 ==> self=false 58281    conf:(0.77)
      if(front1 == "VC" && moreNeu == false) { if(0.77 >= threshold) { return false; } }//   985. front1=VC moreNeu=false 23121 ==> self=false 17908    conf:(0.77)
      if(after1 == "VE") { if(0.77 >= threshold) { return true; } }//   986. after1=VE 4713 ==> self=true 3649    conf:(0.77)
      if(front1 == "Nf" && after1 == "DE" && moreNeu == false) { if(0.77 >= threshold) { return false; } }//   988. front1=Nf after1=DE moreNeu=false 996 ==> self=false 771    conf:(0.77)
      if(front1 == "VA" && after1 == "notword" && moreNeu == false) { if(0.77 >= threshold) { return false; } }//   990. front1=VA after1=notword moreNeu=false 1296 ==> self=false 1003    conf:(0.77)
      if(front1 == "VH") { if(0.77 >= threshold) { return false; } }//   991. front1=VH 5123 ==> self=false 3962    conf:(0.77)
      if(front1 == "VA" && moreNeu == false) { if(0.77 >= threshold) { return false; } }//   992. front1=VA moreNeu=false 2926 ==> self=false 2261    conf:(0.77)
      if(front1 == "Nc" && after1 == "DE" && moreNeu == false) { if(0.77 >= threshold) { return true; } }//   993. front1=Nc after1=DE moreNeu=false 682 ==> self=true 527    conf:(0.77)
      if(after1 == "A") { if(0.77 >= threshold) { return false; } }//   994. after1=A 2506 ==> self=false 1935    conf:(0.77)
      if(front1 == "Cbb" && moreNeu == false) { if(0.77 >= threshold) { return true; } }//   995. front1=Cbb moreNeu=false 7792 ==> self=true 6008    conf:(0.77)
      if(after1 == "Nep" && moreNeu == false) { if(0.77 >= threshold) { return true; } }//   996. after1=Nep moreNeu=false 1196 ==> self=true 922    conf:(0.77)
      if(front1 == "P" && after1 == "VA" && moreNeu == false) { if(0.77 >= threshold) { return true; } }//   997. front1=P after1=VA moreNeu=false 679 ==> self=true 522    conf:(0.77)
      if(front1 == "VH" && after1 == "notword") { if(0.77 >= threshold) { return false; } }//   998. front1=VH after1=notword 1626 ==> self=false 1249    conf:(0.77)
      if(front1 == "VH" && moreNeu == false) { if(0.77 >= threshold) { return false; } }//   999. front1=VH moreNeu=false 4994 ==> self=false 3833    conf:(0.77)
      if(front1 == "Nd" && after1 == "DE" && moreNeu == false) { if(0.77 >= threshold) { return true; } }//  1001. front1=Nd after1=DE moreNeu=false 1036 ==> self=true 794    conf:(0.77)
      if(front1 == "Nb" && moreNeu == false) { if(0.77 >= threshold) { return true; } }//  1002. front1=Nb moreNeu=false 6203 ==> self=true 4749    conf:(0.77)
      if(front1 == "D" && after1 == "notword") { if(0.76 >= threshold) { return false; } }//  1003. front1=D after1=notword 690 ==> self=false 527    conf:(0.76)
      if(front1 == "P" && after1 == "Neu") { if(0.76 >= threshold) { return true; } }//  1004. front1=P after1=Neu 1025 ==> self=true 782    conf:(0.76)
      if(after1 == "Nep") { if(0.76 >= threshold) { return true; } }//  1006. after1=Nep 1210 ==> self=true 922    conf:(0.76)
      if(after1 == "A" && moreNeu == false) { if(0.76 >= threshold) { return false; } }//  1008. after1=A moreNeu=false 2395 ==> self=false 1824    conf:(0.76)
      if(front1 == "Na" && after1 == "Na") { if(0.76 >= threshold) { return false; } }//  1009. front1=Na after1=Na 2441 ==> self=false 1859    conf:(0.76)
      if(front1 == "VH" && after1 == "notword" && moreNeu == false) { if(0.76 >= threshold) { return false; } }//  1010. front1=VH after1=notword moreNeu=false 1578 ==> self=false 1201    conf:(0.76)
      if(front1 == "VH" && after1 == "DE") { if(0.76 >= threshold) { return false; } }//  1012. front1=VH after1=DE 598 ==> self=false 454    conf:(0.76)
      if(after1 == "P") { if(0.76 >= threshold) { return true; } }//  1014. after1=P 12436 ==> self=true 9421    conf:(0.76)
      if(after1 == "VF" && moreNeu == false) { if(0.76 >= threshold) { return true; } }//  1016. after1=VF moreNeu=false 708 ==> self=true 536    conf:(0.76)
      if(front1 == "Na" && after1 == "VC" && moreNeu == false) { if(0.75 >= threshold) { return true; } }//  1019. front1=Na after1=VC moreNeu=false 1101 ==> self=true 831    conf:(0.75)
      if(front1 == "VC" && after1 == "Ng") { if(0.75 >= threshold) { return false; } }//  1020. front1=VC after1=Ng 565 ==> self=false 426    conf:(0.75)
      if(front1 == "Cbb") { if(0.75 >= threshold) { return true; } }//  1021. front1=Cbb 7971 ==> self=true 6009    conf:(0.75)
      if(front1 == "Nd" && after1 == "DE") { if(0.75 >= threshold) { return true; } }//  1023. front1=Nd after1=DE 1059 ==> self=true 798    conf:(0.75)
      if(front1 == "VH" && after1 == "DE" && moreNeu == false) { if(0.75 >= threshold) { return false; } }//  1024. front1=VH after1=DE moreNeu=false 584 ==> self=false 440    conf:(0.75)
      if(front1 == "begin" && after1 == "VCL") { if(0.75 >= threshold) { return true; } }//  1025. front1=begin after1=VCL 684 ==> self=true 514    conf:(0.75)
      if(after1 == "D" && moreNeu == false) { if(0.75 >= threshold) { return true; } }//  1028. after1=D moreNeu=false 26763 ==> self=true 20057    conf:(0.75)
      if(front1 == "begin" && after1 == "VG" && moreNeu == false) { if(0.75 >= threshold) { return true; } }//  1029. front1=begin after1=VG moreNeu=false 550 ==> self=true 412    conf:(0.75)
      if(front1 == "begin") { if(0.75 >= threshold) { return true; } }//  1030. front1=begin 71317 ==> self=true 53370    conf:(0.75)
      if(front1 == "Neu" && moreNeu == false) { if(0.75 >= threshold) { return true; } }//  1033. front1=Neu moreNeu=false 2464 ==> self=true 1843    conf:(0.75)
      if(front1 == "Neu") { if(0.75 >= threshold) { return true; } }//  1035. front1=Neu 2474 ==> self=true 1846    conf:(0.75)
      if(front1 == "begin" && after1 == "VA") { if(0.74 >= threshold) { return true; } }//  1040. front1=begin after1=VA 1099 ==> self=true 818    conf:(0.74)
      if(front1 == "Caa" && after1 == "Na") { if(0.74 >= threshold) { return false; } }//  1041. front1=Caa after1=Na 1390 ==> self=false 1034    conf:(0.74)
      if(front1 == "D" && after1 == "notword" && moreNeu == false) { if(0.74 >= threshold) { return false; } }//  1044. front1=D after1=notword moreNeu=false 632 ==> self=false 469    conf:(0.74)
      if(front1 == "P" && after1 == "VA") { if(0.74 >= threshold) { return true; } }//  1045. front1=P after1=VA 704 ==> self=true 522    conf:(0.74)
      if(after1 == "Nb" && moreNeu == false) { if(0.74 >= threshold) { return true; } }//  1047. after1=Nb moreNeu=false 4061 ==> self=true 3010    conf:(0.74)
      if(front1 == "Nh" && after1 == "notword") { if(0.74 >= threshold) { return false; } }//  1049. front1=Nh after1=notword 620 ==> self=false 459    conf:(0.74)
      if(front1 == "Nb") { if(0.74 >= threshold) { return true; } }//  1050. front1=Nb 6422 ==> self=true 4750    conf:(0.74)
      if(front1 == "SHI" && after1 == "D") { if(0.74 >= threshold) { return false; } }//  1052. front1=SHI after1=D 582 ==> self=false 430    conf:(0.74)
      if(front1 == "VC" && after1 == "Ng" && moreNeu == false) { if(0.74 >= threshold) { return false; } }//  1053. front1=VC after1=Ng moreNeu=false 532 ==> self=false 393    conf:(0.74)
      if(front1 == "Nf" && after1 == "Na") { if(0.74 >= threshold) { return false; } }//  1056. front1=Nf after1=Na 873 ==> self=false 643    conf:(0.74)
      if(front1 == "Nh" && after1 == "notword" && moreNeu == false) { if(0.74 >= threshold) { return false; } }//  1057. front1=Nh after1=notword moreNeu=false 610 ==> self=false 449    conf:(0.74)
      if(front1 == "Caa" && after1 == "Na" && moreNeu == false) { if(0.73 >= threshold) { return false; } }//  1059. front1=Caa after1=Na moreNeu=false 1332 ==> self=false 978    conf:(0.73)
      if(front1 == "SHI") { if(0.73 >= threshold) { return false; } }//  1060. front1=SHI 12932 ==> self=false 9481    conf:(0.73)
      if(front1 == "Na" && after1 == "Na" && moreNeu == false) { if(0.73 >= threshold) { return false; } }//  1061. front1=Na after1=Na moreNeu=false 2177 ==> self=false 1596    conf:(0.73)
      if(front1 == "SHI" && after1 == "notword") { if(0.73 >= threshold) { return false; } }//  1062. front1=SHI after1=notword 1069 ==> self=false 782    conf:(0.73)
      if(front1 == "P" && moreNeu == false) { if(0.73 >= threshold) { return true; } }//  1064. front1=P moreNeu=false 38588 ==> self=true 28213    conf:(0.73)
      if(front1 == "SHI" && after1 == "D" && moreNeu == false) { if(0.73 >= threshold) { return false; } }//  1065. front1=SHI after1=D moreNeu=false 565 ==> self=false 413    conf:(0.73)
      if(after1 == "Nv") { if(0.73 >= threshold) { return false; } }//  1066. after1=Nv 2535 ==> self=false 1852    conf:(0.73)
      if(after1 == "D") { if(0.73 >= threshold) { return true; } }//  1067. after1=D 27470 ==> self=true 20066    conf:(0.73)
      if(after1 == "VH") { if(0.73 >= threshold) { return false; } }//  1068. after1=VH 20849 ==> self=false 15226    conf:(0.73)
      if(after1 == "VCL" && moreNeu == false) { if(0.73 >= threshold) { return true; } }//  1069. after1=VCL moreNeu=false 2476 ==> self=true 1808    conf:(0.73)
      if(front1 == "Nc" && after1 == "DE") { if(0.73 >= threshold) { return true; } }//  1071. front1=Nc after1=DE 724 ==> self=true 527    conf:(0.73)
      if(front1 == "VE" && after1 == "Na") { if(0.73 >= threshold) { return false; } }//  1074. front1=VE after1=Na 1641 ==> self=false 1194    conf:(0.73)
      if(after1 == "Da" && moreNeu == false) { if(0.73 >= threshold) { return true; } }//  1075. after1=Da moreNeu=false 2283 ==> self=true 1661    conf:(0.73)
      if(after1 == "VF") { if(0.73 >= threshold) { return true; } }//  1076. after1=VF 737 ==> self=true 536    conf:(0.73)
      if(front1 == "VC" && after1 == "D") { if(0.73 >= threshold) { return false; } }//  1078. front1=VC after1=D 719 ==> self=false 522    conf:(0.73)
      if(front1 == "VJ" && after1 == "DE") { if(0.73 >= threshold) { return false; } }//  1079. front1=VJ after1=DE 1319 ==> self=false 957    conf:(0.73)
      if(front1 == "begin" && after1 == "VJ" && moreNeu == false) { if(0.73 >= threshold) { return true; } }//  1080. front1=begin after1=VJ moreNeu=false 800 ==> self=true 580    conf:(0.73)
      if(after1 == "VH" && moreNeu == false) { if(0.72 >= threshold) { return false; } }//  1081. after1=VH moreNeu=false 20398 ==> self=false 14783    conf:(0.72)
      if(front1 == "SHI" && moreNeu == false) { if(0.72 >= threshold) { return false; } }//  1083. front1=SHI moreNeu=false 12490 ==> self=false 9039    conf:(0.72)
      if(front1 == "VC" && after1 == "Nc") { if(0.72 >= threshold) { return false; } }//  1084. front1=VC after1=Nc 912 ==> self=false 660    conf:(0.72)
      if(front1 == "VC" && after1 == "D" && moreNeu == false) { if(0.72 >= threshold) { return false; } }//  1085. front1=VC after1=D moreNeu=false 707 ==> self=false 510    conf:(0.72)
      if(front1 == "VE" && after1 == "Na" && moreNeu == false) { if(0.72 >= threshold) { return false; } }//  1086. front1=VE after1=Na moreNeu=false 1596 ==> self=false 1149    conf:(0.72)
      if(front1 == "Di" && after1 == "notword") { if(0.72 >= threshold) { return false; } }//  1087. front1=Di after1=notword 2942 ==> self=false 2117    conf:(0.72)
      if(after1 == "V_2" && moreNeu == false) { if(0.72 >= threshold) { return true; } }//  1088. after1=V_2 moreNeu=false 1594 ==> self=true 1147    conf:(0.72)
      if(after1 == "Nv" && moreNeu == false) { if(0.72 >= threshold) { return false; } }//  1090. after1=Nv moreNeu=false 2404 ==> self=false 1722    conf:(0.72)
      if(front1 == "P") { if(0.71 >= threshold) { return true; } }//  1091. front1=P 39553 ==> self=true 28239    conf:(0.71)
      if(after1 == "Da") { if(0.71 >= threshold) { return true; } }//  1092. after1=Da 2331 ==> self=true 1664    conf:(0.71)
      if(front1 == "VJ" && after1 == "DE" && moreNeu == false) { if(0.71 >= threshold) { return false; } }//  1095. front1=VJ after1=DE moreNeu=false 1253 ==> self=false 891    conf:(0.71)
      if(after1 == "Nb") { if(0.71 >= threshold) { return true; } }//  1096. after1=Nb 4234 ==> self=true 3010    conf:(0.71)
      if(front1 == "VHC") { if(0.71 >= threshold) { return false; } }//  1098. front1=VHC 729 ==> self=false 518    conf:(0.71)
      if(front1 == "SHI" && after1 == "VC") { if(0.71 >= threshold) { return false; } }//  1099. front1=SHI after1=VC 556 ==> self=false 395    conf:(0.71)
      if(front1 == "P" && after1 == "notword" && moreNeu == false) { if(0.71 >= threshold) { return true; } }//  1100. front1=P after1=notword moreNeu=false 3081 ==> self=true 2188    conf:(0.71)
      if(front1 == "Na" && after1 == "VC") { if(0.71 >= threshold) { return true; } }//  1103. front1=Na after1=VC 1173 ==> self=true 832    conf:(0.71)
      if(front1 == "Nf" && after1 == "Na" && moreNeu == false) { if(0.71 >= threshold) { return false; } }//  1105. front1=Nf after1=Na moreNeu=false 789 ==> self=false 559    conf:(0.71)
      if(front1 == "Nc" && after1 == "notword") { if(0.71 >= threshold) { return false; } }//  1107. front1=Nc after1=notword 816 ==> self=false 578    conf:(0.71)
      if(front1 == "Di" && after1 == "notword" && moreNeu == false) { if(0.71 >= threshold) { return false; } }//  1108. front1=Di after1=notword moreNeu=false 2821 ==> self=false 1996    conf:(0.71)
      if(after1 == "V_2") { if(0.7 >= threshold) { return true; } }//  1109. after1=V_2 1630 ==> self=true 1147    conf:(0.7)
      if(front1 == "VC" && after1 == "Nc" && moreNeu == false) { if(0.7 >= threshold) { return false; } }//  1112. front1=VC after1=Nc moreNeu=false 850 ==> self=false 598    conf:(0.7)
      if(front1 == "SHI" && after1 == "notword" && moreNeu == false) { if(0.7 >= threshold) { return false; } }//  1113. front1=SHI after1=notword moreNeu=false 965 ==> self=false 678    conf:(0.7)
      if(front1 == "VHC" && moreNeu == false) { if(0.7 >= threshold) { return false; } }//  1114. front1=VHC moreNeu=false 709 ==> self=false 498    conf:(0.7)
      if(front1 == "begin" && after1 == "VJ") { if(0.7 >= threshold) { return true; } }//  1117. front1=begin after1=VJ 829 ==> self=true 580    conf:(0.7)
      if(front1 == "notword" && after1 == "D" && moreNeu == false) { if(0.7 >= threshold) { return true; } }//  1120. front1=notword after1=D moreNeu=false 715 ==> self=true 499    conf:(0.7)
      if(after1 == "VC" && moreNeu == false) { if(0.69 >= threshold) { return true; } }//  1122. after1=VC moreNeu=false 15537 ==> self=true 10777    conf:(0.69)
      if(front1 == "notword" && after1 == "Na") { if(0.69 >= threshold) { return false; } }//  1123. front1=notword after1=Na 2757 ==> self=false 1910    conf:(0.69)
      if(front1 == "VK" && after1 == "Na") { if(0.69 >= threshold) { return false; } }//  1124. front1=VK after1=Na 820 ==> self=false 566    conf:(0.69)
      if(front1 == "Nb" && after1 == "Na") { if(0.69 >= threshold) { return false; } }//  1125. front1=Nb after1=Na 766 ==> self=false 528    conf:(0.69)
      if(after1 == "VCL") { if(0.69 >= threshold) { return true; } }//  1126. after1=VCL 2637 ==> self=true 1810    conf:(0.69)
      if(front1 == "P" && after1 == "notword") { if(0.69 >= threshold) { return true; } }//  1128. front1=P after1=notword 3193 ==> self=true 2189    conf:(0.69)
      if(after1 == "Neqa" && moreNeu == false) { if(0.69 >= threshold) { return true; } }//  1129. after1=Neqa moreNeu=false 1784 ==> self=true 1223    conf:(0.69)
      if(front1 == "VK" && after1 == "Na" && moreNeu == false) { if(0.68 >= threshold) { return false; } }//  1132. front1=VK after1=Na moreNeu=false 800 ==> self=false 546    conf:(0.68)
      if(front1 == "P" && after1 == "Ng" && moreNeu == false) { if(0.68 >= threshold) { return true; } }//  1134. front1=P after1=Ng moreNeu=false 4527 ==> self=true 3077    conf:(0.68)
      if(front1 == "VF") { if(0.68 >= threshold) { return false; } }//  1137. front1=VF 581 ==> self=false 394    conf:(0.68)
      if(front1 == "notword" && after1 == "D") { if(0.68 >= threshold) { return true; } }//  1138. front1=notword after1=D 736 ==> self=true 499    conf:(0.68)
      if(front1 == "begin" && after1 == "VG") { if(0.68 >= threshold) { return true; } }//  1140. front1=begin after1=VG 608 ==> self=true 412    conf:(0.68)
      if(front1 == "notword" && after1 == "Na" && moreNeu == false) { if(0.68 >= threshold) { return false; } }//  1142. front1=notword after1=Na moreNeu=false 2625 ==> self=false 1778    conf:(0.68)
      if(front1 == "VCL") { if(0.68 >= threshold) { return false; } }//  1143. front1=VCL 3015 ==> self=false 2041    conf:(0.68)
      if(after1 == "Caa" && moreNeu == false) { if(0.68 >= threshold) { return true; } }//  1146. after1=Caa moreNeu=false 3357 ==> self=true 2266    conf:(0.68)
      if(front1 == "begin" && after1 == "Neu" && moreNeu == false) { if(0.67 >= threshold) { return true; } }//  1147. front1=begin after1=Neu moreNeu=false 1714 ==> self=true 1155    conf:(0.67)
      if(front1 == "Nc" && after1 == "notword" && moreNeu == false) { if(0.67 >= threshold) { return false; } }//  1148. front1=Nc after1=notword moreNeu=false 727 ==> self=false 489    conf:(0.67)
      if(front1 == "Nb" && after1 == "Na" && moreNeu == false) { if(0.67 >= threshold) { return false; } }//  1149. front1=Nb after1=Na moreNeu=false 719 ==> self=false 482    conf:(0.67)
      if(after1 == "Neqa") { if(0.67 >= threshold) { return true; } }//  1150. after1=Neqa 1827 ==> self=true 1224    conf:(0.67)
      if(front1 == "P" && after1 == "Nc" && moreNeu == false) { if(0.67 >= threshold) { return true; } }//  1152. front1=P after1=Nc moreNeu=false 1263 ==> self=true 844    conf:(0.67)
      if(front1 == "Ng") { if(0.67 >= threshold) { return false; } }//  1153. front1=Ng 1262 ==> self=false 842    conf:(0.67)
      if(front1 == "VC" && after1 == "VC") { if(0.67 >= threshold) { return false; } }//  1154. front1=VC after1=VC 767 ==> self=false 511    conf:(0.67)
      if(front1 == "Nc" && moreNeu == false) { if(0.67 >= threshold) { return true; } }//  1155. front1=Nc moreNeu=false 9424 ==> self=true 6277    conf:(0.67)
      if(after1 == "VC") { if(0.66 >= threshold) { return true; } }//  1157. after1=VC 16256 ==> self=true 10794    conf:(0.66)
      if(front1 == "P" && after1 == "Ng") { if(0.66 >= threshold) { return true; } }//  1158. front1=P after1=Ng 4642 ==> self=true 3078    conf:(0.66)
      if(front1 == "begin" && after1 == "Neu") { if(0.66 >= threshold) { return true; } }//  1161. front1=begin after1=Neu 1745 ==> self=true 1155    conf:(0.66)
      if(front1 == "DE" && after1 == "notword" && moreNeu == false) { if(0.66 >= threshold) { return true; } }//  1163. front1=DE after1=notword moreNeu=false 4366 ==> self=true 2889    conf:(0.66)
      if(front1 == "VC" && after1 == "VC" && moreNeu == false) { if(0.66 >= threshold) { return false; } }//  1164. front1=VC after1=VC moreNeu=false 734 ==> self=false 484    conf:(0.66)
      if(front1 == "VCL" && moreNeu == false) { if(0.66 >= threshold) { return false; } }//  1165. front1=VCL moreNeu=false 2852 ==> self=false 1878    conf:(0.66)
      if(after1 == "Caa") { if(0.65 >= threshold) { return true; } }//  1168. after1=Caa 3472 ==> self=true 2270    conf:(0.65)
      if(after1 == "T") { if(0.65 >= threshold) { return false; } }//  1170. after1=T 1607 ==> self=false 1043    conf:(0.65)
      if(front1 == "begin" && after1 == "SHI" && moreNeu == false) { if(0.65 >= threshold) { return true; } }//  1171. front1=begin after1=SHI moreNeu=false 1019 ==> self=true 659    conf:(0.65)
      if(after1 == "VA" && moreNeu == false) { if(0.64 >= threshold) { return true; } }//  1173. after1=VA moreNeu=false 5793 ==> self=true 3722    conf:(0.64)
      if(front1 == "DE") { if(0.64 >= threshold) { return false; } }//  1174. front1=DE 15869 ==> self=false 10182    conf:(0.64)
      if(front1 == "notword" && after1 == "Nc" && moreNeu == false) { if(0.64 >= threshold) { return true; } }//  1175. front1=notword after1=Nc moreNeu=false 594 ==> self=true 381    conf:(0.64)
      if(front1 == "Di" && after1 == "DE") { if(0.64 >= threshold) { return false; } }//  1176. front1=Di after1=DE 707 ==> self=false 453    conf:(0.64)
      if(front1 == "Nc" && after1 == "Na") { if(0.64 >= threshold) { return false; } }//  1178. front1=Nc after1=Na 1995 ==> self=false 1275    conf:(0.64)
      if(front1 == "Nh" && moreNeu == false) { if(0.64 >= threshold) { return true; } }//  1179. front1=Nh moreNeu=false 6688 ==> self=true 4264    conf:(0.64)
      if(front1 == "Caa" && after1 == "Ng") { if(0.64 >= threshold) { return false; } }//  1180. front1=Caa after1=Ng 656 ==> self=false 417    conf:(0.64)
      if(front1 == "P" && after1 == "Nc") { if(0.64 >= threshold) { return true; } }//  1181. front1=P after1=Nc 1332 ==> self=true 846    conf:(0.64)
      if(front1 == "notword" && after1 == "Ng") { if(0.63 >= threshold) { return false; } }//  1184. front1=notword after1=Ng 832 ==> self=false 526    conf:(0.63)
      if(front1 == "Caa" && after1 == "Ng" && moreNeu == false) { if(0.63 >= threshold) { return false; } }//  1186. front1=Caa after1=Ng moreNeu=false 646 ==> self=false 407    conf:(0.63)
      if(front1 == "notword" && after1 == "Ng" && moreNeu == false) { if(0.63 >= threshold) { return false; } }//  1188. front1=notword after1=Ng moreNeu=false 821 ==> self=false 515    conf:(0.63)
      if(front1 == "P" && after1 == "Na") { if(0.63 >= threshold) { return false; } }//  1189. front1=P after1=Na 5496 ==> self=false 3446    conf:(0.63)
      if(after1 == "Dfa") { if(0.62 >= threshold) { return false; } }//  1193. after1=Dfa 3380 ==> self=false 2088    conf:(0.62)
      if(after1 == "T" && moreNeu == false) { if(0.62 >= threshold) { return false; } }//  1194. after1=T moreNeu=false 1467 ==> self=false 906    conf:(0.62)
      if(front1 == "Ng" && moreNeu == false) { if(0.62 >= threshold) { return false; } }//  1195. front1=Ng moreNeu=false 1098 ==> self=false 678    conf:(0.62)
      if(front1 == "Nc") { if(0.62 >= threshold) { return true; } }//  1199. front1=Nc 10199 ==> self=true 6277    conf:(0.62)
      if(after1 == "Dfa" && moreNeu == false) { if(0.61 >= threshold) { return false; } }//  1201. after1=Dfa moreNeu=false 3351 ==> self=false 2059    conf:(0.61)
      if(front1 == "DE" && after1 == "notword") { if(0.61 >= threshold) { return true; } }//  1202. front1=DE after1=notword 4711 ==> self=true 2894    conf:(0.61)
      if(front1 == "P" && after1 == "Na" && moreNeu == false) { if(0.61 >= threshold) { return false; } }//  1204. front1=P after1=Na moreNeu=false 5281 ==> self=false 3233    conf:(0.61)
      if(front1 == "Neqa") { if(0.61 >= threshold) { return false; } }//  1205. front1=Neqa 1146 ==> self=false 701    conf:(0.61)
      if(after1 == "VA") { if(0.61 >= threshold) { return true; } }//  1206. after1=VA 6109 ==> self=true 3727    conf:(0.61)
      if(front1 == "DE" && moreNeu == false) { if(0.61 >= threshold) { return false; } }//  1207. front1=DE moreNeu=false 14558 ==> self=false 8880    conf:(0.61)
      if(front1 == "Di" && after1 == "DE" && moreNeu == false) { if(0.61 >= threshold) { return false; } }//  1213. front1=Di after1=DE moreNeu=false 647 ==> self=false 393    conf:(0.61)
      if(front1 == "Nh") { if(0.61 >= threshold) { return true; } }//  1214. front1=Nh 7032 ==> self=true 4264    conf:(0.61)
      if(front1 == "D") { if(0.6 >= threshold) { return false; } }//  1216. front1=D 5458 ==> self=false 3302    conf:(0.6)
      if(front1 == "Neqa" && moreNeu == false) { if(0.6 >= threshold) { return false; } }//  1217. front1=Neqa moreNeu=false 1123 ==> self=false 678    conf:(0.6)
      if(front1 == "Nd" && after1 == "Na" && moreNeu == false) { if(0.6 >= threshold) { return true; } }//  1218. front1=Nd after1=Na moreNeu=false 1702 ==> self=true 1027    conf:(0.6)
      if(after1 == "DE" && moreNeu == false) { if(0.6 >= threshold) { return true; } }//  1219. after1=DE moreNeu=false 22490 ==> self=true 13551    conf:(0.6)
      if(after1 == "VL" && moreNeu == false) { if(0.6 >= threshold) { return true; } }//  1220. after1=VL moreNeu=false 1163 ==> self=true 697    conf:(0.6)
      if(front1 == "notword" && after1 == "Nc") { if(0.6 >= threshold) { return true; } }//  1222. front1=notword after1=Nc 637 ==> self=true 381    conf:(0.6)
      if(after1 == "SHI" && moreNeu == false) { if(0.6 >= threshold) { return true; } }//  1224. after1=SHI moreNeu=false 2440 ==> self=true 1454    conf:(0.6)
      if(front1 == "D" && moreNeu == false) { if(0.59 >= threshold) { return false; } }//  1226. front1=D moreNeu=false 5252 ==> self=false 3096    conf:(0.59)
      if(front1 == "Na" && after1 == "Ng") { if(0.59 >= threshold) { return false; } }//  1228. front1=Na after1=Ng 791 ==> self=false 464    conf:(0.59)
      if(after1 == "VK" && moreNeu == false) { if(0.58 >= threshold) { return true; } }//  1229. after1=VK moreNeu=false 1251 ==> self=true 729    conf:(0.58)
      if(after1 == "VL") { if(0.58 >= threshold) { return true; } }//  1230. after1=VL 1200 ==> self=true 699    conf:(0.58)
      if(after1 == "Neu" && moreNeu == false) { if(0.58 >= threshold) { return true; } }//  1231. after1=Neu moreNeu=false 7327 ==> self=true 4263    conf:(0.58)
      if(front1 == "notword" && after1 == "notword" && moreNeu == false) { if(0.58 >= threshold) { return true; } }//  1233. front1=notword after1=notword moreNeu=false 3455 ==> self=true 2006    conf:(0.58)
      if(front1 == "Nd" && after1 == "Na") { if(0.58 >= threshold) { return true; } }//  1234. front1=Nd after1=Na 1781 ==> self=true 1030    conf:(0.58)
      if(front1 == "Nc" && after1 == "Na" && moreNeu == false) { if(0.58 >= threshold) { return false; } }//  1235. front1=Nc after1=Na moreNeu=false 1707 ==> self=false 987    conf:(0.58)
      if(after1 == "notword") { if(0.58 >= threshold) { return false; } }//  1236. after1=notword 58974 ==> self=false 34071    conf:(0.58)
      if(after1 == "DE") { if(0.58 >= threshold) { return true; } }//  1237. after1=DE 23496 ==> self=true 13570    conf:(0.58)
      if(front1 == "notword" && moreNeu == false) { if(0.58 >= threshold) { return true; } }//  1240. front1=notword moreNeu=false 13898 ==> self=true 7999    conf:(0.58)
      if(front1 == "Na" && after1 == "DE") { if(0.57 >= threshold) { return false; } }//  1241. front1=Na after1=DE 1237 ==> self=false 711    conf:(0.57)
      if(front1 == "Na" && after1 == "Ng" && moreNeu == false) { if(0.57 >= threshold) { return false; } }//  1242. front1=Na after1=Ng moreNeu=false 759 ==> self=false 433    conf:(0.57)
      if(after1 == "VK") { if(0.56 >= threshold) { return true; } }//  1244. after1=VK 1293 ==> self=true 730    conf:(0.56)
      if(after1 == "Nc" && moreNeu == false) { if(0.56 >= threshold) { return true; } }//  1247. after1=Nc moreNeu=false 13311 ==> self=true 7477    conf:(0.56)
      if(after1 == "Neu") { if(0.56 >= threshold) { return true; } }//  1249. after1=Neu 7628 ==> self=true 4263    conf:(0.56)
      if(front1 == "VK" && moreNeu == false) { if(0.56 >= threshold) { return true; } }//  1251. front1=VK moreNeu=false 3039 ==> self=true 1696    conf:(0.56)
      if(front1 == "Caa" && after1 == "notword") { if(0.56 >= threshold) { return false; } }//  1252. front1=Caa after1=notword 1228 ==> self=false 685    conf:(0.56)
      if(front1 == "notword" && after1 == "DE" && moreNeu == false) { if(0.56 >= threshold) { return true; } }//  1253. front1=notword after1=DE moreNeu=false 935 ==> self=true 521    conf:(0.56)
      if(after1 == "notword" && moreNeu == false) { if(0.56 >= threshold) { return false; } }//  1254. after1=notword moreNeu=false 56154 ==> self=false 31278    conf:(0.56)
      if(front1 == "notword" && after1 == "notword") { if(0.56 >= threshold) { return true; } }//  1255. front1=notword after1=notword 3607 ==> self=true 2007    conf:(0.56)
      if(front1 == "Nf") { if(0.56 >= threshold) { return false; } }//  1258. front1=Nf 5901 ==> self=false 3279    conf:(0.56)
      if(after1 == "FW") { if(0.55 >= threshold) { return false; } }//  1259. after1=FW 931 ==> self=false 516    conf:(0.55)
      if(front1 == "notword") { if(0.55 >= threshold) { return true; } }//  1260. front1=notword 14511 ==> self=true 8003    conf:(0.55)
      if(front1 == "VE" && moreNeu == false) { if(0.55 >= threshold) { return true; } }//  1262. front1=VE moreNeu=false 6200 ==> self=true 3415    conf:(0.55)
      if(after1 == "VD" && moreNeu == false) { if(0.55 >= threshold) { return true; } }//  1263. after1=VD moreNeu=false 680 ==> self=true 373    conf:(0.55)
      if(front1 == "Na") { if(0.55 >= threshold) { return false; } }//  1264. front1=Na 18410 ==> self=false 10085    conf:(0.55)
      if(front1 == "Caa" && after1 == "notword" && moreNeu == false) { if(0.55 >= threshold) { return false; } }//  1266. front1=Caa after1=notword moreNeu=false 1197 ==> self=false 654    conf:(0.55)
      if(front1 == "notword" && after1 == "DE") { if(0.54 >= threshold) { return true; } }//  1267. front1=notword after1=DE 959 ==> self=true 522    conf:(0.54)
      if(front1 == "VK") { if(0.54 >= threshold) { return true; } }//  1268. front1=VK 3121 ==> self=true 1696    conf:(0.54)
      if(after1 == "VHC" && moreNeu == false) { if(0.54 >= threshold) { return true; } }//  1271. after1=VHC moreNeu=false 1101 ==> self=true 598    conf:(0.54)
      if(front1 == "VE") { if(0.54 >= threshold) { return true; } }//  1272. front1=VE 6339 ==> self=true 3417    conf:(0.54)
      if(front1 == "begin" && after1 == "Na") { if(0.54 >= threshold) { return false; } }//  1274. front1=begin after1=Na 10184 ==> self=false 5480    conf:(0.54)
      if(after1 == "Ng" && moreNeu == false) { if(0.54 >= threshold) { return true; } }//  1275. after1=Ng moreNeu=false 19816 ==> self=true 10655    conf:(0.54)
      if(after1 == "Nc") { if(0.54 >= threshold) { return true; } }//  1277. after1=Nc 13972 ==> self=true 7480    conf:(0.54)
      if(front1 == "VC" && after1 == "DE") { if(0.53 >= threshold) { return false; } }//  1279. front1=VC after1=DE 1579 ==> self=false 844    conf:(0.53)
      if(after1 == "FW" && moreNeu == false) { if(0.53 >= threshold) { return false; } }//  1280. after1=FW moreNeu=false 890 ==> self=false 475    conf:(0.53)
      if(after1 == "VD") { if(0.53 >= threshold) { return true; } }//  1282. after1=VD 705 ==> self=true 375    conf:(0.53)
      if(front1 == "Nf" && moreNeu == false) { if(0.53 >= threshold) { return false; } }//  1283. front1=Nf moreNeu=false 5594 ==> self=false 2972    conf:(0.53)
      if(after1 == "VG") { if(0.53 >= threshold) { return false; } }//  1284. after1=VG 2505 ==> self=false 1329    conf:(0.53)
      if(front1 == "begin" && after1 == "VH") { if(0.53 >= threshold) { return false; } }//  1286. front1=begin after1=VH 2408 ==> self=false 1277    conf:(0.53)
      if(front1 == "VL") { if(0.53 >= threshold) { return false; } }//  1287. front1=VL 769 ==> self=false 407    conf:(0.53)
      if(front1 == "Na" && after1 == "DE" && moreNeu == false) { if(0.53 >= threshold) { return false; } }//  1288. front1=Na after1=DE moreNeu=false 1115 ==> self=false 590    conf:(0.53)
      if(after1 == "VJ" && moreNeu == false) { if(0.53 >= threshold) { return true; } }//  1290. after1=VJ moreNeu=false 3810 ==> self=true 2014    conf:(0.53)
      if(front1 == "P" && after1 == "VH" && moreNeu == false) { if(0.53 >= threshold) { return true; } }//  1291. front1=P after1=VH moreNeu=false 2114 ==> self=true 1114    conf:(0.53)
      if(front1 == "begin" && after1 == "Ng" && moreNeu == false) { if(0.53 >= threshold) { return true; } }//  1292. front1=begin after1=Ng moreNeu=false 4303 ==> self=true 2267    conf:(0.53)
      if(front1 == "Cbb" && after1 == "Na" && moreNeu == false) { if(0.52 >= threshold) { return true; } }//  1293. front1=Cbb after1=Na moreNeu=false 1500 ==> self=true 787    conf:(0.52)
      if(after1 == "VHC") { if(0.52 >= threshold) { return true; } }//  1294. after1=VHC 1148 ==> self=true 602    conf:(0.52)
      if(after1 == "Ng") { if(0.52 >= threshold) { return true; } }//  1296. after1=Ng 20427 ==> self=true 10670    conf:(0.52)
      if(moreNeu == false) { if(0.52 >= threshold) { return true; } }//  1297. moreNeu=false 357927 ==> self=true 186704    conf:(0.52)
      if(front1 == "begin" && after1 == "SHI") { if(0.52 >= threshold) { return false; } }//  1302. front1=begin after1=SHI 1372 ==> self=false 713    conf:(0.52)
      if(front1 == "Caa") { if(0.52 >= threshold) { return false; } }//  1303. front1=Caa 6720 ==> self=false 3489    conf:(0.52)
      if(front1 == "Na" && after1 == "VH") { if(0.52 >= threshold) { return false; } }//  1304. front1=Na after1=VH 776 ==> self=false 402    conf:(0.52)
      if(front1 == "P" && after1 == "VH") { if(0.52 >= threshold) { return true; } }//  1305. front1=P after1=VH 2153 ==> self=true 1115    conf:(0.52)
      if(front1 == "VL" && moreNeu == false) { if(0.52 >= threshold) { return false; } }//  1307. front1=VL moreNeu=false 749 ==> self=false 387    conf:(0.52)
      if(front1 == "Nf" && after1 == "notword" && moreNeu == false) { if(0.52 >= threshold) { return true; } }//  1308. front1=Nf after1=notword moreNeu=false 1389 ==> self=true 717    conf:(0.52)
      if(front1 == "Na" && moreNeu == false) { if(0.51 >= threshold) { return false; } }//  1310. front1=Na moreNeu=false 17108 ==> self=false 8789    conf:(0.51)
      if(front1 == "begin" && after1 == "VH" && moreNeu == false) { if(0.51 >= threshold) { return false; } }//  1312. front1=begin after1=VH moreNeu=false 2319 ==> self=false 1189    conf:(0.51)
      if(front1 == "Caa" && after1 == "DE") { if(0.51 >= threshold) { return false; } }//  1315. front1=Caa after1=DE 733 ==> self=false 375    conf:(0.51)
      if(front1 == "VE" && after1 == "notword") { if(0.51 >= threshold) { return false; } }//  1316. front1=VE after1=notword 859 ==> self=false 439    conf:(0.51)
      if(front1 == "Cbb" && after1 == "Na") { if(0.51 >= threshold) { return true; } }//  1319. front1=Cbb after1=Na 1546 ==> self=true 788    conf:(0.51)
      if(after1 == "VG" && moreNeu == false) { if(0.51 >= threshold) { return false; } }//  1321. after1=VG moreNeu=false 2395 ==> self=false 1219    conf:(0.51)
      if(front1 == "begin" && after1 == "Na" && moreNeu == false) { if(0.51 >= threshold) { return false; } }//  1324. front1=begin after1=Na moreNeu=false 9559 ==> self=false 4860    conf:(0.51)
      if(front1 == "VC" && after1 == "DE" && moreNeu == false) { if(0.51 >= threshold) { return false; } }//  1325. front1=VC after1=DE moreNeu=false 1493 ==> self=false 759    conf:(0.51)
      if(after1 == "VJ") { if(0.51 >= threshold) { return true; } }//  1326. after1=VJ 3963 ==> self=true 2014    conf:(0.51)
      if(front1 == "Caa" && moreNeu == false) { if(0.51 >= threshold) { return false; } }//  1332. front1=Caa moreNeu=false 6538 ==> self=false 3310    conf:(0.51)
      if(front1 == "begin" && after1 == "Ng") { if(0.51 >= threshold) { return true; } }//  1333. front1=begin after1=Ng 4484 ==> self=true 2270    conf:(0.51)
      if(front1 == "VE" && after1 == "notword" && moreNeu == false) { if(0.51 >= threshold) { return false; } }//  1336. front1=VE after1=notword moreNeu=false 845 ==> self=false 427    conf:(0.51)
      if(front1 == "Nf" && after1 == "notword") { if(0.5 >= threshold) { return false; } }//  1342. front1=Nf after1=notword 1442 ==> self=false 725    conf:(0.5)
      if(front1 == "Na" && after1 == "VH" && moreNeu == false) { if(0.5 >= threshold) { return false; } }//  1343. front1=Na after1=VH moreNeu=false 750 ==> self=false 377    conf:(0.5)
      if(after1 == "SHI") { if(0.5 >= threshold) { return false; } }//  1344. after1=SHI 2912 ==> self=false 1458    conf:(0.5)
      return false;

    }
  }
}
