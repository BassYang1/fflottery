// Decompiled with JetBrains decompiler
// Type: Lottery.Utils.YouLeTcpServerEngine
// Assembly: Lottery.Utils, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: E7A9C185-AF0A-4444-AE46-9A73782D0A74
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Utils.dll

using StriveEngine;
using StriveEngine.Core;
using StriveEngine.Tcp.Server;
using System;
using System.Net;
using System.Text;

namespace Lottery.Utils
{
  public class YouLeTcpServerEngine
  {
    private ITcpServerEngine tcpServerEngine;
    private bool hasTcpServerEngineInitialized;

    public void TcpConnection(int port)
    {
      try
      {
        if (this.tcpServerEngine == null)
          this.tcpServerEngine = NetworkEngineFactory.CreateTextTcpServerEngine(port, (ITextContractHelper) new DefaultTextContractHelper(new string[1]
          {
            "\0"
          }));
        if (this.hasTcpServerEngineInitialized)
          this.tcpServerEngine.ChangeListenerState(true);
        else
          this.InitializeTcpServerEngine();
      }
      catch (Exception ex)
      {
        ex.ToString();
      }
    }

    private void InitializeTcpServerEngine()
    {
      this.tcpServerEngine.ClientCountChanged += new CbDelegate<int>(this.tcpServerEngine_ClientCountChanged);
      this.tcpServerEngine.ClientConnected += new CbDelegate<IPEndPoint>(this.tcpServerEngine_ClientConnected);
      this.tcpServerEngine.ClientDisconnected += new CbDelegate<IPEndPoint>(this.tcpServerEngine_ClientDisconnected);
      this.tcpServerEngine.MessageReceived += new CbDelegate<IPEndPoint, byte[]>(this.tcpServerEngine_MessageReceived);
      this.tcpServerEngine.Initialize();
      this.hasTcpServerEngineInitialized = true;
    }

    private void tcpServerEngine_MessageReceived(IPEndPoint client, byte[] bMsg)
    {
      string str = Encoding.UTF8.GetString(bMsg);
      str.Substring(0, str.Length - 1);
    }

    private void tcpServerEngine_ClientDisconnected(IPEndPoint ipe)
    {
      string.Format("{0} 下线", (object) ipe);
    }

    private void tcpServerEngine_ClientConnected(IPEndPoint ipe)
    {
      string.Format("{0} 上线", (object) ipe);
    }

    private void tcpServerEngine_ClientCountChanged(int count)
    {
    }

    public void SendMessage(string msg)
    {
      try
      {
        foreach (IPEndPoint client in this.tcpServerEngine.GetClientList())
          this.tcpServerEngine.SendMessageToClient(client, Encoding.UTF8.GetBytes(msg));
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }
}
