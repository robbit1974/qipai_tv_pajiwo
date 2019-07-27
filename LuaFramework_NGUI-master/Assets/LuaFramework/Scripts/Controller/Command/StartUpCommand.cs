using UnityEngine;
using System.Collections;
using LuaInterface;
using LuaFramework;

public class StartUpCommand : ControllerCommand {

    public override void Execute(IMessage message) {
        if (!Util.CheckEnvironment()) return;


        this.InitLoading();

        //GameObject gameMgr = GameObject.Find("GlobalGenerator");
        //if (gameMgr != null) {
        //    AppView appView = gameMgr.AddComponent<AppView>();
        //}
        //-----------------关联命令-----------------------
        AppFacade.Instance.RegisterCommand(NotiConst.DISPATCH_MESSAGE, typeof(SocketCommand));
        AppFacade.Instance.RegisterCommand(NotiConst.GAMEDISPATCH_MESSAGE, typeof(GameSocketCommand));
        //-----------------初始化管理器-----------------------
        AppFacade.Instance.AddManager<LuaManager>(ManagerName.Lua);
        AppFacade.Instance.AddManager<PanelManager>(ManagerName.Panel);
        AppFacade.Instance.AddManager<SoundManager>(ManagerName.Sound);
        AppFacade.Instance.AddManager<TimerManager>(ManagerName.Timer);
        AppFacade.Instance.AddManager<DaTingSocket>(ManagerName.DaTingSocket);
        AppFacade.Instance.AddManager<GameSocket>(ManagerName.GameSocket);
        AppFacade.Instance.AddManager<ResourceManager>(ManagerName.Resource);
        AppFacade.Instance.AddManager<ThreadManager>(ManagerName.Thread);
        AppFacade.Instance.AddManager<ObjectPoolManager>(ManagerName.ObjectPool);
        AppFacade.Instance.AddManager<SetWechatHead>(ManagerName.SetWeChatHead);
        AppFacade.Instance.AddManager<GameManager>(ManagerName.Game);
    }

    private void InitLoading() {
        string name = "LoadingUI";
        GameObject gui = GameObject.Find(name);
        if (gui != null) return;

        GameObject prefab = Util.LoadPrefab(name);
        gui = Object.Instantiate(prefab) as GameObject;
        gui.name = name;
        AppView appView = gui.AddComponent<AppView>();
    }
}