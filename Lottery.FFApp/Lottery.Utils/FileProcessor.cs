// Decompiled with JetBrains decompiler
// Type: Lottery.Utils.FileProcessor
// Assembly: Lottery.Utils, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: E7A9C185-AF0A-4444-AE46-9A73782D0A74
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Utils.dll

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;

namespace Lottery.Utils
{
  public class FileProcessor : IDisposable
  {
    private string _currentFilePath = "";
    private string _formPostID = "";
    private string _fieldSeperator = "";
    public string _currentFileName = Guid.NewGuid().ToString() + ".bin";
    private long _startIndexBufferID = -1;
    private int _startLocationInBufferID = -1;
    private long _endIndexBufferID = -1;
    private int _endLocationInBufferID = -1;
    private Dictionary<long, byte[]> _bufferHistory = new Dictionary<long, byte[]>();
    private List<string> _finishedFiles = new List<string>();
    private long _currentBufferIndex;
    private bool _startFound;
    private bool _endFound;
    private FileStream _fileStream;

    public FileProcessor(string uploadLocation)
    {
      this._currentFilePath = uploadLocation;
    }

    public List<string> FinishedFiles
    {
      get
      {
        return this._finishedFiles;
      }
    }

    public void ProcessBuffer(ref byte[] bufferData, bool addToBufferHistory)
    {
      int num = -1;
      if (!this._startFound)
      {
        num = this.GetStartBytePosition(ref bufferData);
        if (num != -1)
        {
          this._startIndexBufferID = this._currentBufferIndex + 1L;
          this._startLocationInBufferID = num;
          this._startFound = true;
        }
      }
      if (this._startFound)
      {
        int offset = 0;
        if (num != -1)
          offset = num;
        int count = bufferData.Length - offset;
        int endBytePosition = this.GetEndBytePosition(ref bufferData);
        if (endBytePosition != -1)
        {
          count = endBytePosition - offset;
          this._endFound = true;
          this._endIndexBufferID = this._currentBufferIndex + 1L;
          this._endLocationInBufferID = endBytePosition;
        }
        if (count > 0)
        {
          if (this._fileStream == null)
          {
            this._fileStream = new FileStream(this._currentFilePath + this._currentFileName, FileMode.OpenOrCreate);
            Timer timer = new Timer(new TimerCallback(FileProcessor.DeleteFile), (object) (this._currentFilePath + this._currentFileName), 3600 * 1000, 0);
          }
          this._fileStream.Write(bufferData, offset, count);
          this._fileStream.Flush();
        }
      }
      if (this._endFound)
      {
        this.CloseStreams();
        this._startFound = false;
        this._endFound = false;
        this.ProcessBuffer(ref bufferData, false);
      }
      if (!addToBufferHistory)
        return;
      this._bufferHistory.Add(this._currentBufferIndex, bufferData);
      ++this._currentBufferIndex;
      this.RemoveOldBufferData();
    }

    private void RemoveOldBufferData()
    {
      for (long key = this._currentBufferIndex; key >= 0L; --key)
      {
        if (key <= this._currentBufferIndex - 3L)
        {
          if (this._bufferHistory.ContainsKey(key))
            this._bufferHistory.Remove(key);
          else
            key = 0L;
        }
      }
      GC.Collect();
    }

    public void CloseStreams()
    {
      if (this._fileStream == null)
        return;
      this._fileStream.Dispose();
      this._fileStream.Close();
      this._fileStream = (FileStream) null;
      this._finishedFiles.Add(this._currentFileName);
      this._currentFileName = Guid.NewGuid().ToString() + ".bin";
    }

    public void GetFieldSeperators(ref byte[] bufferData)
    {
      try
      {
        this._formPostID = Encoding.UTF8.GetString(bufferData).Substring(29, 13);
        this._fieldSeperator = "-----------------------------" + this._formPostID;
      }
      catch (Exception ex)
      {
        Debug.WriteLine("Error in GetFieldSeperators(): " + ex.Message);
      }
    }

    private int GetStartBytePosition(ref byte[] bufferData)
    {
      int startAtIndex = 0;
      if (this._startIndexBufferID == this._currentBufferIndex + 1L)
        startAtIndex = this._startLocationInBufferID;
      if (this._endIndexBufferID == this._currentBufferIndex + 1L)
        startAtIndex = this._endLocationInBufferID;
      byte[] bytes1 = Encoding.UTF8.GetBytes("Content-Type: ");
      int bytePattern1 = FileProcessor.FindBytePattern(ref bufferData, ref bytes1, startAtIndex);
      if (bytePattern1 != -1)
      {
        byte[] bytes2 = Encoding.UTF8.GetBytes("\r\n\r\n");
        int bytePattern2 = FileProcessor.FindBytePattern(ref bufferData, ref bytes2, bytePattern1);
        if (bytePattern2 != -1)
          return bytePattern2 + 4;
      }
      else
      {
        if (startAtIndex - bytes1.Length > 0 || this._currentBufferIndex <= 0L)
          return -1;
        byte[] arrayOne = this._bufferHistory[this._currentBufferIndex - 1L];
        byte[] containerBytes = FileProcessor.MergeArrays(ref arrayOne, ref bufferData);
        byte[] bytes2 = Encoding.UTF8.GetBytes("Content-Type: ");
        if (FileProcessor.FindBytePattern(ref containerBytes, ref bytes2, arrayOne.Length - bytes2.Length) != -1)
        {
          byte[] bytes3 = Encoding.UTF8.GetBytes("Content-Type: ");
          int bytePattern2 = FileProcessor.FindBytePattern(ref containerBytes, ref bytes3, arrayOne.Length - bytes3.Length);
          if (bytePattern2 != -1)
          {
            if (bytePattern2 > arrayOne.Length)
              return bytePattern2 - arrayOne.Length;
            return 0;
          }
        }
      }
      return -1;
    }

    private int GetEndBytePosition(ref byte[] bufferData)
    {
      int startAtIndex = 0;
      if (this._startIndexBufferID == this._currentBufferIndex + 1L)
        startAtIndex = this._startLocationInBufferID;
      byte[] bytes1 = Encoding.UTF8.GetBytes(this._fieldSeperator);
      int bytePattern1 = FileProcessor.FindBytePattern(ref bufferData, ref bytes1, startAtIndex);
      if (bytePattern1 != -1)
      {
        if (bytePattern1 - 2 >= 0)
          return bytePattern1 - 2;
      }
      else
      {
        if (startAtIndex - bytes1.Length > 0 || this._currentBufferIndex <= 0L)
          return -1;
        byte[] arrayOne = this._bufferHistory[this._currentBufferIndex - 1L];
        byte[] containerBytes = FileProcessor.MergeArrays(ref arrayOne, ref bufferData);
        byte[] bytes2 = Encoding.UTF8.GetBytes(this._fieldSeperator);
        int bytePattern2 = FileProcessor.FindBytePattern(ref containerBytes, ref bytes2, arrayOne.Length - bytes2.Length + startAtIndex);
        if (bytePattern2 != -1)
        {
          bytes2 = Encoding.UTF8.GetBytes("\r\n\r\n");
          int bytePattern3 = FileProcessor.FindBytePattern(ref containerBytes, ref bytes2, bytePattern2);
          if (bytePattern3 != -1 && bytePattern3 > arrayOne.Length)
            return bytePattern3 - arrayOne.Length;
        }
      }
      return -1;
    }

    private static int FindBytePattern(ref byte[] containerBytes, ref byte[] searchBytes, int startAtIndex)
    {
      int num = -1;
      for (int index1 = startAtIndex; index1 < containerBytes.Length; ++index1)
      {
        if (index1 + searchBytes.Length > containerBytes.Length)
          return -1;
        if ((int) containerBytes[index1] == (int) searchBytes[0])
        {
          bool flag = true;
          int index2 = index1;
          for (int index3 = 1; index3 < searchBytes.Length; ++index3)
          {
            ++index2;
            if ((int) searchBytes[index3] != (int) containerBytes[index2])
            {
              flag = false;
              break;
            }
          }
          if (flag)
            return index1;
        }
      }
      return num;
    }

    private static byte[] MergeArrays(ref byte[] arrayOne, ref byte[] arrayTwo)
    {
      arrayOne.GetType().GetElementType();
      byte[] numArray = new byte[arrayOne.Length + arrayTwo.Length];
      arrayOne.CopyTo((Array) numArray, 0);
      arrayTwo.CopyTo((Array) numArray, arrayOne.Length);
      return numArray;
    }

    private static void DeleteFile(object filePath)
    {
      try
      {
        if (!File.Exists((string) filePath))
          return;
        File.Delete((string) filePath);
      }
      catch
      {
      }
    }

    public void Dispose()
    {
      this._bufferHistory.Clear();
      GC.Collect();
    }
  }
}
