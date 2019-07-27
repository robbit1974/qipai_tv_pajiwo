
using BestHTTP;
using BestHTTP.WebSocket;
using LuaFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSocket : Manager
{

    private float time = 0;

    public WebSocket webSocket;

    //是否开始连接
    private bool mIsStartSocket = false;

    public bool MIsStartSocket
    {
        set
        {
            mIsStartSocket = value;
        }
    }

    public bool MIsSendHeatBeat
    {
        set
        {
            mIsSendHeatBeat = value;
        }
    }

    private bool mIsSendHeatBeat = false;


    void OnDestroy()
    {
        if (webSocket != null)
            webSocket.Close();
    }
    private void Update()
    {
        if (webSocket == null && mIsStartSocket)
        {
            webSocket = new WebSocket(new Uri(AppConst.GameSocketPort));

#if !BESTHTTP_DISABLE_PROXY && !UNITY_WEBGL
            if (HTTPManager.Proxy != null)
                webSocket.InternalRequest.Proxy = new HTTPProxy(HTTPManager.Proxy.Address, HTTPManager.Proxy.Credentials, false);
#endif
            webSocket.OnOpen += OnOpen;
            webSocket.OnMessage += OnMessageReceived;
            webSocket.OnClosed += OnClosed;
            webSocket.OnError += OnError;

            webSocket.Open();
        }
        if (webSocket != null && webSocket.IsOpen&&mIsSendHeatBeat)
        {
            time += Time.deltaTime;
            if (time >= 10)
            {
                time = 0;
                //TODO 发送心跳协议
                SendMsg("{msg:'heartbeat',data:{token:'" +AppConst.GameUserToken + "'}}");
            }
        }
    }

    public void SendMsg(string msg)
    {
        webSocket.Send(msg);
    }

    public void CloseWeb()
    {
        mIsStartSocket = false;
        if (webSocket != null)
            webSocket.Close();
        webSocket = null;
    }

    public void BindPlayer(string GameToken,string GameId)
    {
        SendMsg("{msg:'bindPlayer',data:{token:'" + GameToken + "',gameId:'" + GameId + "'}}");
    }
    #region WebSocket Event Handlers

    /// <summary>
    /// 初始连接socket
    /// </summary>
    void OnOpen(WebSocket ws)
    {
        Debug.Log("开始网络");
    }

    /// <summary>
    /// 收到服务器消息
    /// </summary>
    void OnMessageReceived(WebSocket ws, string message)
    {
        //处理消息
        Util.CallMethod("Game", "HandGameSocket", message);
    }

    /// <summary>
    /// 关闭websocket
    /// </summary>
    void OnClosed(WebSocket ws, UInt16 code, string message)
    {
        Debug.Log("webSocket关闭:" + code + message);
        webSocket = null;
    }

    /// <summary>
    /// websocket出错
    /// </summary>
    void OnError(WebSocket ws, Exception ex)
    {
        string errorMsg = string.Empty;
#if !UNITY_WEBGL || UNITY_EDITOR
        if (ws.InternalRequest.Response != null)
            errorMsg = string.Format("websocket有错误:", ws.InternalRequest.Response.StatusCode, ws.InternalRequest.Response.Message);
#endif
        Util.CallMethod("Game", "HandGameSocketError", ex != null ? ex.Message : "Unknown Error");

        Debug.Log(string.Format("websocket有错误:", (ex != null ? ex.Message : "Unknown Error " + errorMsg)));
        webSocket = null;
    }
    #endregion
}

