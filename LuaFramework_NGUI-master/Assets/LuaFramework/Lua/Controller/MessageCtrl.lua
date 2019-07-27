
MessageCtrl = {};
local this = MessageCtrl;

local message;
local transform;
local gameObject;

--构建函数--
function MessageCtrl.New()
	logWarn("MessageCtrl.New--->>");
	return this;
end

function MessageCtrl.Awake()
	logWarn("MessageCtrl.Awake--->>");
	panelMgr:CreatePanel('Message', this.OnCreate);
end

--启动事件--
function MessageCtrl.OnCreate(obj)
    gameObject=obj;
    transform=obj.transform;
    this.CreateTable()
end
--创建表
function MessageCtrl.CreateTable()
    this.QuitGameQueDingBtn=ButtonBase:New(MessagePanel.QueDingBtn)
    this.QuitGameCanclBtn=ButtonBase:New(MessagePanel.CanclBtn)
    this.MessageQueDingBtn=ButtonBase:New(MessagePanel.MessageQueDingBtn)
    this.SetTable()
    this.SetOkFunc()
end

function MessageCtrl.SetTable()
    this.QuitGameQueDingBtn:SetRight(this.QuitGameCanclBtn)
    this.QuitGameCanclBtn:SetLeft(this.QuitGameQueDingBtn)
end

function MessageCtrl:SetOkFunc()
    this.QuitGameQueDingBtn.okFunc=function()
        UnityEngine.Application.Quit()
    end
    this.QuitGameCanclBtn.okFunc=function()
        MessagePanel.bg.gameObject:SetActive(false)
        MessagePanel.QuitGame.gameObject:SetActive(false)
        if currentGameType==1 then
            this.QuitGameQueDingBtn:SetStart()
        else
            CtrlManager.GetCtrl(CtrlNames.Game).SetTuiChuBtn()
        end
    end
end
---------------------------------------------------------内置函数---------------------------------------------------------------------------------------------------
--退出游戏
function MessageCtrl.QuitGame()
    MessagePanel.bg.gameObject:SetActive(true)
    MessagePanel.QuitGame.gameObject:SetActive(true)
    this.QuitGameQueDingBtn:SetStart()
end
----获得经验提示
function MessageCtrl.GetExe(num,isAddLevel,newLevelName)
    MessagePanel.bg.gameObject:SetActive(true)
    MessagePanel.Message.gameObject:SetActive(true)
    MessagePanel.Title.text="[b]恭喜您获得"..num.."经验"
    this.MessageQueDingBtn:SetStart()
    --升级 显示升级界面
    if isAddLevel then
        this.MessageQueDingBtn.okFunc=function()
            MessagePanel.Message.gameObject:SetActive(false)
            this.AddLevel(newLevelName)
        end
    else--不升级 隐藏界面
        this.MessageQueDingBtn.okFunc=function()
            MessagePanel.Message.gameObject:SetActive(false)
            MessagePanel.bg.gameObject:SetActive(false)
        end
    end
end
--升级 三秒后自动隐藏
function MessageCtrl.AddLevel(newLevelName)
    local rr=coroutine.create(function ()
        MessagePanel.Level.gameObject:SetActive(true)
        MessagePanel.NewLevel.text="[b]"..newLevelName
        coroutine.wait(3)
        MessagePanel.Level.gameObject:SetActive(false)
        MessagePanel.bg.gameObject:SetActive(false)
    end)
    coroutine.resume(rr)
end
--关闭事件--
function MessageCtrl.Close()
	panelMgr:ClosePanel(CloseNames.Message);
end