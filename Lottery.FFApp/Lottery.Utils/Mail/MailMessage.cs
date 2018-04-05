// Decompiled with JetBrains decompiler
// Type: Lottery.Utils.Mail.MailMessage
// Assembly: Lottery.Utils, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: E7A9C185-AF0A-4444-AE46-9A73782D0A74
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Utils.dll

using System;
using System.Collections;

namespace Lottery.Utils.Mail
{
  public class MailMessage
  {
    private int _MaxRecipientNum = 30;
    private string _Charset = "GB2312";
    private string _From;
    private string _FromName;
    private IList _Recipients;
    private MailAttachments _Attachments;
    private string _Body;
    private string _Subject;
    private MailFormat _BodyFormat;
    private MailPriority _Priority;

    public MailMessage()
    {
      this._Recipients = (IList) new ArrayList();
      this._Attachments = new MailAttachments();
      this._BodyFormat = MailFormat.HTML;
      this._Priority = MailPriority.Normal;
      this._Charset = "GB2312";
    }

    public string Charset
    {
      get
      {
        return this._Charset;
      }
      set
      {
        this._Charset = value;
      }
    }

    public int MaxRecipientNum
    {
      get
      {
        return this._MaxRecipientNum;
      }
      set
      {
        this._MaxRecipientNum = value;
      }
    }

    public string From
    {
      get
      {
        return this._From;
      }
      set
      {
        this._From = value;
      }
    }

    public string FromName
    {
      get
      {
        return this._FromName;
      }
      set
      {
        this._FromName = value;
      }
    }

    public string Body
    {
      get
      {
        return this._Body;
      }
      set
      {
        this._Body = value;
      }
    }

    public string Subject
    {
      get
      {
        return this._Subject;
      }
      set
      {
        this._Subject = value;
      }
    }

    public MailAttachments Attachments
    {
      get
      {
        return this._Attachments;
      }
      set
      {
        this._Attachments = value;
      }
    }

    public MailPriority Priority
    {
      get
      {
        return this._Priority;
      }
      set
      {
        this._Priority = value;
      }
    }

    public IList Recipients
    {
      get
      {
        return this._Recipients;
      }
    }

    public MailFormat BodyFormat
    {
      set
      {
        this._BodyFormat = value;
      }
      get
      {
        return this._BodyFormat;
      }
    }

    public void AddRecipients(string recipient)
    {
      if (this._Recipients.Count >= this.MaxRecipientNum)
        return;
      this._Recipients.Add((object) recipient);
    }

    public void AddRecipients(params string[] recipient)
    {
      if (recipient == null)
        throw new ArgumentException("收件人不能为空.");
      for (int index = 0; index < recipient.Length; ++index)
        this.AddRecipients(recipient[index]);
    }
  }
}
