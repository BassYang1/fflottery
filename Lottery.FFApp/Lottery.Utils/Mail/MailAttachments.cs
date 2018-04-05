// Decompiled with JetBrains decompiler
// Type: Lottery.Utils.Mail.MailAttachments
// Assembly: Lottery.Utils, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: E7A9C185-AF0A-4444-AE46-9A73782D0A74
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Utils.dll

using System;
using System.Collections;
using System.IO;

namespace Lottery.Utils.Mail
{
  public class MailAttachments
  {
    private const int MaxAttachmentNum = 10;
    private IList _Attachments;

    public MailAttachments()
    {
      this._Attachments = (IList) new ArrayList();
    }

    public string this[int index]
    {
      get
      {
        return (string) this._Attachments[index];
      }
    }

    public void Add(params string[] filePath)
    {
      if (filePath == null)
        throw new ArgumentNullException("非法的附件");
      for (int index = 0; index < filePath.Length; ++index)
        this.Add(filePath[index]);
    }

    public void Add(string filePath)
    {
      if (!File.Exists(filePath) || this._Attachments.Count >= 10)
        return;
      this._Attachments.Add((object) filePath);
    }

    public void Clear()
    {
      this._Attachments.Clear();
    }

    public int Count
    {
      get
      {
        return this._Attachments.Count;
      }
    }
  }
}
