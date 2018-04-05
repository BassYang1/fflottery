// Decompiled with JetBrains decompiler
// Type: Lottery.WebApp.Plus._getcode
// Assembly: Lottery.FFApp, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: CD5F1C7F-2EB9-4806-9452-C9F3634A8986
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.FFApp.dll

using Lottery.DAL;
using Lottery.Utils;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;

namespace Lottery.WebApp.Plus
{
  public class _getcode : BasicPage
  {
    private int letterWidth = 20;
    private int letterHeight = 32;
    private int letterCount = 4;
    private char[] chars = "0123456789QWERTYUIOPASDFGHJKLZXCVBNM".ToCharArray();
    private string[] fonts = new string[2]
    {
      "Arial",
      "Georgia"
    };
    private const double PI = 3.14159265358979;
    private const double PI2 = 6.28318530717959;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.Response.Expires = 0;
      this.Response.Buffer = true;
      this.Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1.0);
      this.Response.AddHeader("pragma", "no-cache");
      this.Response.CacheControl = "no-cache";
      this.CreateImage(ValidateCode.GetValidateCode(this.letterCount, true));
    }

    public void CreateImage(string checkCode)
    {
      int width = checkCode.Length * this.letterWidth;
      Random random1 = new Random();
      Bitmap bitmap = new Bitmap(width, this.letterHeight);
      Graphics graphics = Graphics.FromImage((Image) bitmap);
      Random random2 = new Random();
      graphics.Clear(Color.White);
      for (int index = 0; index < 10; ++index)
      {
        int x1 = random2.Next(bitmap.Width);
        int x2 = random2.Next(bitmap.Width);
        int y1 = random2.Next(bitmap.Height);
        int y2 = random2.Next(bitmap.Height);
        graphics.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
      }
      for (int index = 0; index < 10; ++index)
      {
        int x = random2.Next(bitmap.Width);
        int y = random2.Next(bitmap.Height);
        bitmap.SetPixel(x, y, Color.FromArgb(random2.Next()));
      }
      for (int startIndex = 0; startIndex < checkCode.Length; ++startIndex)
      {
        int index = random1.Next(this.fonts.Length - 1);
        string s = checkCode.Substring(startIndex, 1);
        Brush brush = (Brush) new SolidBrush(this.GetRandomColor());
        Point point = new Point(startIndex * this.letterWidth + 1 + random1.Next(3), 1 + random1.Next(3));
        graphics.DrawString(s, new Font(this.fonts[index], 14f, FontStyle.Bold), brush, (PointF) point);
      }
      graphics.DrawRectangle(new Pen(Color.LightGray, 1f), 0, 0, width - 1, this.letterHeight - 1);
      MemoryStream memoryStream = new MemoryStream();
      bitmap.Save((Stream) memoryStream, ImageFormat.Png);
      this.Response.ClearContent();
      this.Response.ContentType = "image/Png";
      this.Response.BinaryWrite(memoryStream.ToArray());
      graphics.Dispose();
      bitmap.Dispose();
    }

    public Bitmap TwistImage(Bitmap srcBmp, bool bXDir, double dMultValue, double dPhase)
    {
      Bitmap bitmap = new Bitmap(srcBmp.Width, srcBmp.Height);
      Graphics graphics = Graphics.FromImage((Image) bitmap);
      graphics.FillRectangle((Brush) new SolidBrush(Color.White), 0, 0, bitmap.Width, bitmap.Height);
      graphics.Dispose();
      double num1 = bXDir ? (double) bitmap.Height : (double) bitmap.Width;
      for (int x1 = 0; x1 < bitmap.Width; ++x1)
      {
        for (int y1 = 0; y1 < bitmap.Height; ++y1)
        {
          double num2 = Math.Sin((bXDir ? 2.0 * Math.PI * (double) y1 / num1 : 2.0 * Math.PI * (double) x1 / num1) + dPhase);
          int x2 = bXDir ? x1 + (int) (num2 * dMultValue) : x1;
          int y2 = bXDir ? y1 : y1 + (int) (num2 * dMultValue);
          Color pixel = srcBmp.GetPixel(x1, y1);
          if (x2 >= 0 && x2 < bitmap.Width && y2 >= 0 && y2 < bitmap.Height)
            bitmap.SetPixel(x2, y2, pixel);
        }
      }
      return bitmap;
    }

    public Color GetRandomColor()
    {
      Random random1 = new Random((int) DateTime.Now.Ticks);
      Thread.Sleep(random1.Next(50));
      Random random2 = new Random((int) DateTime.Now.Ticks);
      int red = random1.Next(210);
      int green = random2.Next(180);
      int num = red + green > 300 ? 0 : 400 - red - green;
      int blue = num > (int) byte.MaxValue ? (int) byte.MaxValue : num;
      return Color.FromArgb(red, green, blue);
    }
  }
}
