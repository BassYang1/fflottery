// Decompiled with JetBrains decompiler
// Type: Lottery.Utils.IPSearchHelp.Location
// Assembly: Lottery.Utils, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: E7A9C185-AF0A-4444-AE46-9A73782D0A74
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Utils.dll

namespace Lottery.Utils.IPSearchHelp
{
  public class Location
  {
    private string[] _capital0 = new string[23]
    {
      "黑龙江",
      "吉林",
      "辽宁",
      "河北",
      "山西",
      "陕西",
      "山东",
      "青海",
      "甘肃",
      "宁夏",
      "河南",
      "江苏",
      "湖北",
      "浙江",
      "安徽",
      "福建",
      "江西",
      "湖南",
      "贵州",
      "四川",
      "广东",
      "云南",
      "海南"
    };
    private string[] _capital1 = new string[4]
    {
      "北京",
      "上海",
      "天津",
      "重庆"
    };
    private string[] _capital2 = new string[5]
    {
      "内蒙古",
      "新疆",
      "西藏",
      "宁夏",
      "广西"
    };
    private string[] _capital3 = new string[3]
    {
      "香港",
      "澳门",
      "台湾"
    };
    private int _areatype = 4;
    private string _captical = "";
    private string _city = "";

    public Location(string _area)
    {
      if (_area.Contains("省") && _area.Contains("市"))
      {
        this._areatype = 0;
        this._captical = _area.Substring(0, _area.IndexOf('省'));
        this._city = _area.Substring(_area.IndexOf('省') + 1, _area.IndexOf('市') - _area.IndexOf('省') - 1);
      }
      if (this._areatype == 4)
      {
        for (int index = 0; index < this._capital1.Length; ++index)
        {
          if (_area.StartsWith(this._capital1[index]))
          {
            this._areatype = 1;
            this._captical = this._capital1[index];
            if (_area.Length > this._capital1[index].Length + 1)
              this._city = _area.Substring(this._capital1[index].Length + 1).Replace("区", "");
          }
        }
      }
      if (this._areatype == 4)
      {
        for (int index = 0; index < this._capital2.Length; ++index)
        {
          if (_area.StartsWith(this._capital2[index]))
          {
            this._areatype = 2;
            this._captical = this._capital2[index];
            if (_area.Length > this._capital2[index].Length)
              this._city = _area.Substring(this._capital2[index].Length + 1).Replace("市", "");
          }
        }
      }
      if (this._areatype != 4)
        return;
      for (int index = 0; index < this._capital3.Length; ++index)
      {
        if (_area.StartsWith(this._capital3[index]))
        {
          this._areatype = 3;
          this._captical = this._capital3[index];
          if (_area.Length > this._capital3[index].Length)
            this._city = _area.Substring(this._capital3[index].Length);
        }
      }
    }

    public int AreaType
    {
      set
      {
        this._areatype = value;
      }
      get
      {
        return this._areatype;
      }
    }

    public string Captical
    {
      set
      {
        this._captical = value;
      }
      get
      {
        return this._captical;
      }
    }

    public string City
    {
      set
      {
        this._city = value;
      }
      get
      {
        return this._city;
      }
    }
  }
}
