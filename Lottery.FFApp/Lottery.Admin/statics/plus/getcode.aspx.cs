// Decompiled with JetBrains decompiler
// Type: Lottery.AdminFile.Plus._getcode
// Assembly: Lottery.Admin, Version=7.0.1.203, Culture=neutral, PublicKeyToken=null
// MVID: 838B9BD2-8091-4C2A-B624-E2A206486676
// Assembly location: F:\pros\tianheng\bf\admin\bin\Lottery.Admin.dll

using Lottery.DAL;
using Lottery.Utils;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Security.Cryptography;

namespace Lottery.AdminFile.Plus
{
  public partial class _getcode : AdminBasicPage
  {
    private static byte[] randb = new byte[4];
    private static RNGCryptoServiceProvider rand = new RNGCryptoServiceProvider();
    private int letterWidth = 30;
    private int letterHeight = 30;
    private int letterCount = 4;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.Response.ClearContent();
      this.Response.ContentType = "image/jpeg";
      this.letterHeight = this.Str2Int(this.q("h"), 30);
      this.letterWidth = this.letterHeight;
      this.CreateImage(ValidateCode.GetValidateCode(this.letterCount, true));
    }

    private static int Next(int max)
    {
      _getcode.rand.GetBytes(_getcode.randb);
      int num = BitConverter.ToInt32(_getcode.randb, 0) % (max + 1);
      if (num < 0)
        num = -num;
      return num;
    }

    private static int Next(int min, int max)
    {
      return _getcode.Next(max - min) + min;
    }

    public void CreateImage(string checkCode)
    {
      int num1 = this.letterHeight * 3 / 4 - 3;
      if (num1 < 12)
        num1 = 12;
      if (num1 > 30)
        num1 = 30;
      int max = 3;
      Font[] fontArray = new Font[4]
      {
        new Font(new FontFamily("Times New Roman"), (float) (num1 + _getcode.Next(max)), FontStyle.Italic),
        new Font(new FontFamily("Times New Roman"), (float) (num1 + _getcode.Next(max)), FontStyle.Regular),
        new Font(new FontFamily("Times New Roman"), (float) (num1 + _getcode.Next(max)), FontStyle.Regular),
        new Font(new FontFamily("Times New Roman"), (float) (num1 + _getcode.Next(max)), FontStyle.Italic)
      };
      Bitmap bitmap = new Bitmap(checkCode.Length * this.letterWidth, this.letterHeight);
      Graphics graphics = Graphics.FromImage((Image) bitmap);
      graphics.Clear(Color.White);
      for (int index = 0; index < 2; ++index)
      {
        int x1 = _getcode.Next(bitmap.Width - 1);
        int x2 = _getcode.Next(bitmap.Width - 1);
        int y1 = _getcode.Next(bitmap.Height - 1);
        int y2 = _getcode.Next(bitmap.Height - 1);
        graphics.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
      }
      int num2 = -num1 + 6;
      for (int startIndex = 0; startIndex < checkCode.Length; ++startIndex)
      {
        int x = num2 + _getcode.Next(num1 - 2, num1 + 10);
        num2 = x;
        int y = _getcode.Next(0, 3) - 3;
        string s = checkCode.Substring(startIndex, 1);
        Brush brush = (Brush) new SolidBrush(this.GetRandomColor());
        Point point = new Point(x, y);
        graphics.DrawString(s, fontArray[_getcode.Next(fontArray.Length - 1)], brush, (PointF) point);
      }
      for (int index = 0; index < 20; ++index)
      {
        int x = _getcode.Next(bitmap.Width - 1);
        int y = _getcode.Next(bitmap.Height - 1);
        bitmap.SetPixel(x, y, Color.FromArgb(_getcode.Next(0, (int) byte.MaxValue), _getcode.Next(0, (int) byte.MaxValue), _getcode.Next(0, (int) byte.MaxValue)));
      }
      MemoryStream memoryStream = new MemoryStream();
      bitmap.Save((Stream) memoryStream, ImageFormat.Png);
      this.Response.BinaryWrite(memoryStream.ToArray());
      graphics.Dispose();
      bitmap.Dispose();
    }

    public Bitmap TwistImage(Bitmap srcBmp, bool bXDir, double dMultValue, double dPhase)
    {
      double num1 = 2.0 * Math.PI;
      Bitmap bitmap = new Bitmap(srcBmp.Width, srcBmp.Height);
      Graphics graphics = Graphics.FromImage((Image) bitmap);
      graphics.FillRectangle((Brush) new SolidBrush(Color.White), 0, 0, bitmap.Width, bitmap.Height);
      graphics.Dispose();
      double num2 = bXDir ? (double) bitmap.Height : (double) bitmap.Width;
      for (int x1 = 0; x1 < bitmap.Width; ++x1)
      {
        for (int y1 = 0; y1 < bitmap.Height; ++y1)
        {
          double num3 = Math.Sin((bXDir ? num1 * (double) y1 / num2 : num1 * (double) x1 / num2) + dPhase);
          int x2 = bXDir ? x1 + (int) (num3 * dMultValue) : x1;
          int y2 = bXDir ? y1 : y1 + (int) (num3 * dMultValue);
          Color pixel = srcBmp.GetPixel(x1, y1);
          if (x2 >= 0 && x2 < bitmap.Width && y2 >= 0 && y2 < bitmap.Height)
            bitmap.SetPixel(x2, y2, pixel);
        }
      }
      srcBmp.Dispose();
      return bitmap;
    }

    public Color GetRandomColor()
    {
      return Color.FromArgb(0, 0, 0);
    }
  }
}
