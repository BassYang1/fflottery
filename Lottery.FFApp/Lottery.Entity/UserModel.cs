// Decompiled with JetBrains decompiler
// Type: Lottery.Entity.UserBetModel
// Assembly: Lottery.Entity, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: 46AD28DD-7094-42F3-8479-C39F629CA84C
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Entity.dll

using System;

namespace Lottery.Entity
{
    [Serializable]
    public class UserModel {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int UserGroup { get; set; }
        public string UserGroupName { get; set; }

        public int ParentId { get; set; }
    }
}
