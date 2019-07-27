using UnityEngine;
using LuaFramework;
using System.Collections.Generic;
using System.Net.NetworkInformation;

public class AppView : View {
    private string message;

    private UISlider mSlider;
    private UILabel mLabel;

    private float UISlider
    {
        set
        {
            if (mSlider!=null)
            mSlider.value = value;
        }
    }

    private string MLabel
    {
        set
        {
            if (mLabel != null)
                mLabel.text = value;
        }
    }


    ///<summary>
    /// 监听的消息
    ///</summary>
    List<string> MessageList {
        get {
            return new List<string>()
            { 
                NotiConst.UPDATE_MESSAGE,
                NotiConst.UPDATE_EXTRACT,
                NotiConst.UPDATE_DOWNLOAD,
                NotiConst.UPDATE_PROGRESS,
                NotiConst.UPDATE_SLIDER,
            };
        }
    }

    void Awake() {
        RemoveMessage(this, MessageList);
        RegisterMessage(this, MessageList);
        mSlider = transform.Find("Camera/Loading/Slider").GetComponent<UISlider>();
        mLabel = transform.Find("Camera/Loading/MessageLable").GetComponent<UILabel>();
    }

    /// <summary>
    /// 处理View消息
    /// </summary>
    /// <param name="message"></param>
    public override void OnMessage(IMessage message) {
        string name = message.Name;
        object body = message.Body;
        switch (name) {
            case NotiConst.UPDATE_MESSAGE:      //更新消息
                UpdateMessage(body.ToString());
            break;
            case NotiConst.UPDATE_EXTRACT:      //更新解压
                UpdateExtract(body.ToString());
            break;
            case NotiConst.UPDATE_DOWNLOAD:     //更新下载
                UpdateDownload(body.ToString());
            break;
            case NotiConst.UPDATE_PROGRESS:     //更新下载进度
                UpdateProgress(body.ToString());
                break;
            case NotiConst.UPDATE_SLIDER:
                UpdateSlider(body.ToString());
            break;
        }
    }

    public void UpdateMessage(string data) {
        MLabel = data;
    }

    public void UpdateSlider(string data)
    {
        UISlider = float.Parse(data);
    }

    public void UpdateExtract(string data) {
        MLabel = data;
    }

    public void UpdateDownload(string data) {
        MLabel = data;
    }

    public void UpdateProgress(string data) {
        MLabel = data;
    }

    //void OnGUI() {
    //    GUI.Label(new Rect(10, 120, 960, 50), message);

    //    GUI.Label(new Rect(10, 0, 500, 50), "(1) 单击 \"Lua/Gen Lua Wrap Files\"。");
    //    GUI.Label(new Rect(10, 20, 500, 50), "(2) 运行Unity游戏");
    //    GUI.Label(new Rect(10, 40, 500, 50), "PS: 清除缓存，单击\"Lua/Clear LuaBinder File + Wrap Files\"。");
    //    GUI.Label(new Rect(10, 60, 900, 50), "PS: 若运行到真机，请设置Const.DebugMode=false，本地调试请设置Const.DebugMode=true");
    //    GUI.Label(new Rect(10, 80, 500, 50), "PS: 加Unity+ulua技术讨论群：>>341746602");
    //}
}
