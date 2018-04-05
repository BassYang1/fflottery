// Decompiled with JetBrains decompiler
// Type: Lottery.DBUtility.DbKeyItem
// Assembly: Lottery.DBUtility, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: 41391965-66A5-4DE4-8203-13B298F4A572
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.DBUtility.dll

namespace Lottery.DBUtility
{
  public class DbKeyItem
  {
    public string fieldName;
    public string fieldValue;

    public DbKeyItem(string _fieldName, object _fieldValue)
    {
      this.fieldName = _fieldName;
      this.fieldValue = _fieldValue.ToString();
    }
  }
}
