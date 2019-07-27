using BestHTTP;
using BestHTTP.WebSocket;
using LuaFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaTingSocket : Manager {

    private float time = 0;

    WebSocket webSocket;
    //网络地址 通过lua赋值
    //是否开始连接
    private bool mIsStartSocket=false;

    public bool MIsStartSocket
    {
        get
        {
            return mIsStartSocket;
        }

        set
        {
            mIsStartSocket = value;
        }
    }


    void OnDestroy()
    {
        if (webSocket != null)
            webSocket.Close();
    }
    private void Update()
    {
        //开始连接
        if (webSocket == null &&mIsStartSocket)
        {
            webSocket = new WebSocket(new Uri(AppConst.DaTingSocketPort));

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
        if(webSocket!=null && webSocket.IsOpen)
        {
            time += Time.deltaTime;
            if (time >= 10)
            {
                time = 0;
                //TODO 发送心跳协议
                SendMsg("{msg:'heartbeat',data:{token:'" + AppConst.DaTingUserToken + "'}}");
            }
        }
    }

    public void SendMsg(string msg)
    {
        webSocket.Send(msg);
    }

    #region WebSocket Event Handlers

    /// <summary>
    /// 初始连接socket
    /// </summary>
    void OnOpen(WebSocket ws)
    {
        Debug.Log("网络开始");
    }

    /// <summary>
    /// 收到服务器消息
    /// </summary>
    void OnMessageReceived(WebSocket ws, string message)
    {
        Debug.Log("收到消息:"+ message);
        //处理消息
        Util.CallMethod("Game", "HandDaTingSocket", message);
    }

    /// <summary>
    /// 关闭websocket
    /// </summary>
    void OnClosed(WebSocket ws, UInt16 code, string message)
    {
        Debug.Log("websocket关闭:" + code+message);
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
        Util.CallMethod("Game", "HandDaTingSocket", ex!=null?ex.Message: "Unknown Error");

        Debug.Log(string.Format("websocket有错误:", (ex != null ? ex.Message : "Unknown Error " + errorMsg)));
        webSocket = null;
    }
    #endregion


}
