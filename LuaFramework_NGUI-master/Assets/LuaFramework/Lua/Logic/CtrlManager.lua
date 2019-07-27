require "Common/define"
require "Controller/PromptCtrl"
require "Controller/MessageCtrl"
require "Controller/SignInCtrl"
require "Controller/RecordCtrl"
require "Controller/RuleCtrl"
require "Controller/SettingCtrl"
require "Controller/QianDaoCtrl"
require "Controller/GameCtrl"
require "Controller/GameMoreSettingCtrl"
require "Controller/GameExpressioAndWittyCtrl"
require "Http_Helper"
require "BaseHelper"
require "ButtonBase"
CtrlManager = {};
local this = CtrlManager;
local ctrlList = {};	--控制器列表--

function CtrlManager.Init()
	logWarn("CtrlManager.Init----->>>");
	ctrlList[CtrlNames.Prompt] = PromptCtrl.New();
	ctrlList[CtrlNames.Message] = MessageCtrl.New();
	ctrlList[CtrlNames.SignIn] = SignInCtrl.New();
	ctrlList[CtrlNames.Record] = RecordCtrl.New();
	ctrlList[CtrlNames.Rule] = RuleCtrl.New();
	ctrlList[CtrlNames.Setting] = SettingCtrl.New();
	ctrlList[CtrlNames.QianDao] = QianDaoCtrl.New();
	ctrlList[CtrlNames.Game] = GameCtrl.New();
	ctrlList[CtrlNames.GameMoreSetting] = GameMoreSettingCtrl.New();
	ctrlList[CtrlNames.GameExpressioAndWitty] = GameExpressioAndWittyCtrl.New();
	return this;
end

--添加控制器--
function CtrlManager.AddCtrl(ctrlName, ctrlObj)
	ctrlList[ctrlName] = ctrlObj;
end

--获取控制器--
function CtrlManager.GetCtrl(ctrlName)
	return ctrlList[ctrlName];
end

--移除控制器--
function CtrlManager.RemoveCtrl(ctrlName)
	ctrlList[ctrlName] = nil;
end

--关闭控制器--
function CtrlManager.Close()
	logWarn('CtrlManager.Close---->>>');
end