// Decompiled with JetBrains decompiler
// Type: Lottery.DAL.LotteryUtils
// Assembly: Lottery.DAL, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: 7C79BA5B-21B3-40F1-B96A-84E656E22E29
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.DAL.dll

namespace Lottery.DAL
{
  public class LotteryUtils
  {
    public static string LotteryTitle(int Id)
    {
      switch (Id)
      {
        case 1001:
          return "重庆时时彩";
        case 1002:
          return "江西时时彩";
        case 1003:
          return "新疆时时彩";
        case 1004:
          return "纽约30秒彩";
        case 1005:
          return "腾讯分分彩";
        case 1006:
          return "优乐秒秒彩";
        case 1007:
          return "天津时时彩";
        case 1008:
          return "云南时时彩";
        case 1009:
          return "泰国一分彩";
        case 1010:
          return "韩国1.5分";
        case 1011:
          return "新德里1.5分彩";
        case 1012:
          return "新加坡2分彩";
        case 1013:
          return "台湾5分彩";
        case 1014:
          return "老东京1.5分彩";
        case 1015:
          return "菲律宾1.5分彩";
        case 1016:
          return "东京1.5分彩";
        case 1017:
          return "老韩国1.5分";
        case 1018:
          return "新加坡30秒";
        case 1019:
          return "台湾45秒";
        case 1020:
          return "首尔60秒";
        case 2001:
          return "山东11选5";
        case 2002:
          return "广东11选5";
        case 2003:
          return "上海11选5";
        case 2004:
          return "江西11选5";
        case 2005:
          return "纽约30秒11选5";
        case 2006:
          return "韩国1.5分11选5";
        case 3001:
          return "时时乐";
        case 3002:
          return "福彩3D";
        case 3003:
          return "体彩P3";
        case 3004:
          return "韩国90秒3D";
        case 3005:
          return "纽约30秒3D";
        case 3006:
          return "一分3d";
        case 4001:
          return "北京PK10";
        case 4002:
          return "英国30秒PK10";
        case 4003:
          return "英国60秒 ";
        case 4004:
          return "英国120秒赛车";
        case 5005:
          return "广西快3";
        case 6001:
          return "六合彩";
        default:
          return "未知彩票";
      }
    }
  }
}
