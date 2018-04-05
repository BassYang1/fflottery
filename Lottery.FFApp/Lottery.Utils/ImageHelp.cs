// Decompiled with JetBrains decompiler
// Type: Lottery.Utils.ImageHelp
// Assembly: Lottery.Utils, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: E7A9C185-AF0A-4444-AE46-9A73782D0A74
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Utils.dll

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Net;

namespace Lottery.Utils
{
  public static class ImageHelp
  {
    public static ImageFormat ImgFormat(string _Photo)
    {
      string lower = _Photo.Substring(_Photo.LastIndexOf(".") + 1, _Photo.Length - _Photo.LastIndexOf(".") - 1).ToLower();
      ImageFormat jpeg = ImageFormat.Jpeg;
      ImageFormat imageFormat;
      switch (lower)
      {
        case "png":
          imageFormat = ImageFormat.Png;
          break;
        case "gif":
          imageFormat = ImageFormat.Gif;
          break;
        case "bmp":
          imageFormat = ImageFormat.Bmp;
          break;
        default:
          imageFormat = ImageFormat.Jpeg;
          break;
      }
      return imageFormat;
    }

    public static bool LocalImage2Thumbs(string originalImagePath, string thumbnailPath, int width, int height, string mode)
    {
      Image originalImage = Image.FromFile(originalImagePath);
      ImageHelp.Image2Thumbs(originalImage, thumbnailPath, width, height, mode);
      originalImage.Dispose();
      return true;
    }

    public static bool RemoteImage2Thumbs(string remoteImageUrl, string thumbnailPath, int width, int height, string mode)
    {
      try
      {
        WebRequest webRequest = WebRequest.Create(remoteImageUrl);
        webRequest.Timeout = 20000;
        Image originalImage = Image.FromStream(webRequest.GetResponse().GetResponseStream());
        ImageHelp.Image2Thumbs(originalImage, thumbnailPath, width, height, mode);
        originalImage.Dispose();
        return true;
      }
      catch
      {
        return false;
      }
    }

    public static void Image2Thumbs(Image originalImage, string thumbnailPath, int photoWidth, int photoHeight, string mode)
    {
      int width1 = photoWidth;
      int height1 = photoHeight;
      int width2 = photoWidth;
      int height2 = photoHeight;
      int x1 = 0;
      int y1 = 0;
      int width3 = originalImage.Width;
      int height3 = originalImage.Height;
      int x2 = 0;
      int y2 = 0;
      switch (mode.ToUpper())
      {
        case "FILL":
          height2 = photoHeight;
          width2 = height2 * width3 / height3;
          if (width2 > photoWidth)
          {
            height2 = height2 * photoWidth / width2;
            width2 = photoWidth;
          }
          x2 = (photoWidth - width2) / 2;
          y2 = (photoHeight - height2) / 2;
          break;
        case "W":
          height2 = height1 = originalImage.Height * photoWidth / originalImage.Width;
          break;
        case "H":
          width2 = width1 = originalImage.Width * photoHeight / originalImage.Height;
          break;
        case "CUT":
          if ((double) originalImage.Width / (double) originalImage.Height > (double) width1 / (double) height1)
          {
            height3 = originalImage.Height;
            width3 = originalImage.Height * width1 / height1;
            y1 = 0;
            x1 = (originalImage.Width - width3) / 2;
            break;
          }
          width3 = originalImage.Width;
          height3 = originalImage.Width * photoHeight / width1;
          x1 = 0;
          y1 = (originalImage.Height - height3) / 2;
          break;
      }
      Image image = (Image) new Bitmap(width1, height1);
      Graphics graphics = Graphics.FromImage(image);
      graphics.InterpolationMode = InterpolationMode.High;
      graphics.SmoothingMode = SmoothingMode.HighQuality;
      graphics.CompositingQuality = CompositingQuality.HighQuality;
      graphics.InterpolationMode = InterpolationMode.High;
      graphics.Clear(Color.White);
      graphics.DrawImage(originalImage, new Rectangle(x2, y2, width2, height2), new Rectangle(x1, y1, width3, height3), GraphicsUnit.Pixel);
      try
      {
        image.Save(thumbnailPath, ImageHelp.ImgFormat(thumbnailPath));
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        image.Dispose();
        graphics.Dispose();
      }
    }

    public static void MakeMyThumbs(string originalImagePath, string thumbnailPath, int toW, int toH, int X, int Y, int W, int H)
    {
      Image image1 = Image.FromFile(originalImagePath);
      int width1 = toW;
      int height1 = toH;
      int x = X;
      int y = Y;
      int width2 = W;
      int height2 = H;
      Image image2 = (Image) new Bitmap(width1, height1);
      Graphics graphics = Graphics.FromImage(image2);
      graphics.InterpolationMode = InterpolationMode.High;
      graphics.SmoothingMode = SmoothingMode.HighQuality;
      graphics.CompositingQuality = CompositingQuality.HighQuality;
      graphics.InterpolationMode = InterpolationMode.High;
      graphics.Clear(Color.Transparent);
      graphics.DrawImage(image1, new Rectangle(0, 0, width1, height1), new Rectangle(x, y, width2, height2), GraphicsUnit.Pixel);
      try
      {
        image2.Save(thumbnailPath, ImageHelp.ImgFormat(thumbnailPath));
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        image1.Dispose();
        image2.Dispose();
        graphics.Dispose();
      }
    }

    public static void AddWater(string Path, string Path_sy, string addText)
    {
      Image image = Image.FromFile(Path);
      Graphics graphics = Graphics.FromImage(image);
      graphics.DrawImage(image, 0, 0, image.Width, image.Height);
      Font font = new Font("Verdana", 60f);
      Brush brush = (Brush) new SolidBrush(Color.Green);
      graphics.DrawString(addText, font, brush, 35f, 35f);
      graphics.Dispose();
      image.Save(Path_sy);
      image.Dispose();
    }

    public static void AddImageSignPic(string Path, string filename, string watermarkFilename, int watermarkStatus, int quality, int watermarkTransparency)
    {
      Image image1 = Image.FromFile(Path);
      Graphics graphics = Graphics.FromImage(image1);
      Image image2 = (Image) new Bitmap(watermarkFilename);
      if (image2.Height >= image1.Height || image2.Width >= image1.Width)
        return;
      ImageAttributes imageAttr = new ImageAttributes();
      ColorMap[] map = new ColorMap[1]
      {
        new ColorMap()
        {
          OldColor = Color.FromArgb((int) byte.MaxValue, 0, (int) byte.MaxValue, 0),
          NewColor = Color.FromArgb(0, 0, 0, 0)
        }
      };
      imageAttr.SetRemapTable(map, ColorAdjustType.Bitmap);
      float num = 0.5f;
      if (watermarkTransparency >= 1 && watermarkTransparency <= 10)
        num = (float) watermarkTransparency / 10f;
      ColorMatrix newColorMatrix = new ColorMatrix(new float[5][]
      {
        new float[5]{ 1f, 0.0f, 0.0f, 0.0f, 0.0f },
        new float[5]{ 0.0f, 1f, 0.0f, 0.0f, 0.0f },
        new float[5]{ 0.0f, 0.0f, 1f, 0.0f, 0.0f },
        new float[5]{ 0.0f, 0.0f, 0.0f, num, 0.0f },
        new float[5]{ 0.0f, 0.0f, 0.0f, 0.0f, 1f }
      });
      imageAttr.SetColorMatrix(newColorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
      int x = 0;
      int y = 0;
      switch (watermarkStatus)
      {
        case 1:
          x = (int) ((double) image1.Width * 0.00999999977648258);
          y = (int) ((double) image1.Height * 0.00999999977648258);
          break;
        case 2:
          x = (int) ((double) image1.Width * 0.5 - (double) (image2.Width / 2));
          y = (int) ((double) image1.Height * 0.00999999977648258);
          break;
        case 3:
          x = (int) ((double) image1.Width * 0.990000009536743 - (double) image2.Width);
          y = (int) ((double) image1.Height * 0.00999999977648258);
          break;
        case 4:
          x = (int) ((double) image1.Width * 0.00999999977648258);
          y = (int) ((double) image1.Height * 0.5 - (double) (image2.Height / 2));
          break;
        case 5:
          x = (int) ((double) image1.Width * 0.5 - (double) (image2.Width / 2));
          y = (int) ((double) image1.Height * 0.5 - (double) (image2.Height / 2));
          break;
        case 6:
          x = (int) ((double) image1.Width * 0.990000009536743 - (double) image2.Width);
          y = (int) ((double) image1.Height * 0.5 - (double) (image2.Height / 2));
          break;
        case 7:
          x = (int) ((double) image1.Width * 0.00999999977648258);
          y = (int) ((double) image1.Height * 0.990000009536743 - (double) image2.Height);
          break;
        case 8:
          x = (int) ((double) image1.Width * 0.5 - (double) (image2.Width / 2));
          y = (int) ((double) image1.Height * 0.990000009536743 - (double) image2.Height);
          break;
        case 9:
          x = (int) ((double) image1.Width * 0.990000009536743 - (double) image2.Width);
          y = (int) ((double) image1.Height * 0.990000009536743 - (double) image2.Height);
          break;
      }
      graphics.DrawImage(image2, new Rectangle(x, y, image2.Width, image2.Height), 0, 0, image2.Width, image2.Height, GraphicsUnit.Pixel, imageAttr);
      ImageCodecInfo[] imageEncoders = ImageCodecInfo.GetImageEncoders();
      ImageCodecInfo encoder = (ImageCodecInfo) null;
      foreach (ImageCodecInfo imageCodecInfo in imageEncoders)
      {
        if (imageCodecInfo.MimeType.Contains("jpeg"))
          encoder = imageCodecInfo;
      }
      EncoderParameters encoderParams = new EncoderParameters();
      long[] numArray = new long[1];
      if (quality < 0 || quality > 100)
        quality = 80;
      numArray[0] = (long) quality;
      EncoderParameter encoderParameter = new EncoderParameter(Encoder.Quality, numArray);
      encoderParams.Param[0] = encoderParameter;
      if (encoder != null)
        image1.Save(filename, encoder, encoderParams);
      else
        image1.Save(filename);
      graphics.Dispose();
      image1.Dispose();
      image2.Dispose();
      imageAttr.Dispose();
    }

    public static void AddWaterPic(string Path, string Path_syp, string Path_sypf)
    {
      Image image1 = Image.FromFile(Path);
      Image image2 = Image.FromFile(Path_sypf);
      Graphics graphics = Graphics.FromImage(image1);
      graphics.DrawImage(image2, new Rectangle(image1.Width - image2.Width, image1.Height - image2.Height, image2.Width, image2.Height), 0, 0, image2.Width, image2.Height, GraphicsUnit.Pixel);
      graphics.Dispose();
      image1.Save(Path_syp);
      image1.Dispose();
    }
  }
}
