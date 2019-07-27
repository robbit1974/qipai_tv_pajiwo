local transform;
local gameObject;

MessagePanel = {};
local this = MessagePanel;

--启动事件--
function MessagePanel.Awake(obj)
	gameObject = obj;
	transform = obj.transform;

	this.InitPanel();
	logWarn("Awake lua--->>"..gameObject.name);
end

--初始化面板--
function MessagePanel.InitPanel()
	this.bg=transform:Find("Bg")
	this.QuitGame=transform:Find("QuitGame")
	this.QueDingBtn=this.QuitGame:Find("QueDingBtn")
	this.CanclBtn=this.QuitGame:Find("CanclBtn")

	this.Message=transform:Find("Message")
	this.MessageQueDingBtn=this.Message:Find("MessageQueDingBtn")
	this.Title=this.Message:Find("Title"):GetComponent("UILabel")

	this.Level=transform:Find("Level")
	this.NewLevel=this.Level:Find("NewLevel"):GetComponent("UILabel")
end

--单击事件--
function MessagePanel.OnDestroy()
	logWarn("OnDestroy---->>>");
end

