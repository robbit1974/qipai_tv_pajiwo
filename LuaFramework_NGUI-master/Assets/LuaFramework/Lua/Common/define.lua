
CtrlNames = {
	Prompt = "PromptCtrl",
	Message = "MessageCtrl",
	SignIn="SignInCtrl",
	Record="RecordCtrl",
	Rule="RuleCtrl",
	Setting="SettingCtrl",
	QianDao="QianDaoCtrl",
	Game="GameCtrl",
	GameExpressioAndWitty="GameExpressioAndWittyCtrl",
	GameMoreSetting="GameMoreSettingCtrl",
}
CloseNames = {
	Prompt = "Prompt",
	Message = "Message",
	SignIn="SignIn",
	Record="Record",
	Rule="Rule",
	Setting="Setting",
	QianDao="QianDao",
	Game="Game",
	GameExpressioAndWitty="GameExpressioAndWitty",
	GameMoreSetting="GameMoreSetting",
}

PanelNames = {
	"PromptPanel",
	"MessagePanel",
	"SignInPanel",
	"RecordPanel",
	"RulePanel",
	"SettingPanel",
	"QianDaoPanel",
	"GamePanel",
	"GameExpressioAndWittyPanel",
	"GameMoreSettingPanel",
}
EventType={
	DaTingChuShiAnNiu = '1001';
}
--协议类型--
ProtocalType = {
	BINARY = 0,
	PB_LUA = 1,
	PBC = 2,
	SPROTO = 3,
}
--当前使用的协议类型--
TestProtoType = ProtocalType.BINARY;

Util = LuaFramework.Util;
AppConst = LuaFramework.AppConst;
LuaHelper = LuaFramework.LuaHelper;
ByteBuffer = LuaFramework.ByteBuffer;

resMgr = LuaHelper.GetResManager();
panelMgr = LuaHelper.GetPanelManager();
soundMgr = LuaHelper.GetSoundManager();
daTingSocket = LuaHelper.GetDaTingWebSocketManager();
gameWebSocket = LuaHelper.GetGameWebSocketManager();
objPoolMgr= LuaHelper.GetObjectPooltManager();
setWeChatHead=LuaHelper.GetSetWechatHeadManager();
--WWW 协议
WWW = UnityEngine.WWW;
GameObject = UnityEngine.GameObject;
-------------网络以及个人基础信息----------------------------------
LocalHallHttpPort= "http://192.168.0.80"
LocalGameHttpPort=""
--大厅和游戏里的服务器不同,会有两个信息,不过MemberId唯一
PlayerInfo={
	GameToken=nil,--大厅token
	MyHeadUrl=nil,--头像URl
    MyName=nil,--名字
    MemberId=nil,--大厅玩家id
    --PlayerId=nil,--用户id
}

GameInfo={
    GameId=nil,--游戏id
    GameToken=nil,--游戏token
    GameType=nil,--游戏类型
}

----------游戏状态--------------------
GameProcess={
	DaTing=1,--大厅
	Game=2,--游戏准备状态
	--GameStart=3,--开始游戏
	GamePlaying=4,
	GameTuoGuan=5,
	GameCaoZuo=6,
	GameHu=7,
}
--初始值,判断游戏所在的状态
currentGameType=GameProcess.DaTing

--牌权重
PokerWeight={
	["GuiPai"]=0,
	["yiwan"]=1,
	["erwan"]=2,
	["sanwan"]=3,
	["siwan"]=4,
	["wuwan"]=5,
	["liuwan"]=6,
	["qiwan"]=7,
	["bawan"]=8,
	["jiuwan"]=9,
	["yitong"]=10,
	["ertong"]=11,
	["santong"]=12,
	["sitong"]=13,
	["wutong"]=14,
	["liutong"]=15,
	["qitong"]=16,
	["batong"]=17,
	["jiutong"]=18,
	["yitiao"]=19,
	["ertiao"]=20,
	["santiao"]=21,
	["sitiao"]=22,
	["wutiao"]=23,
	["liutiao"]=24,
	["qitiao"]=25,
	["batiao"]=26,
	["jiutiao"]=27,
	["dongfeng"]=28,
	["xifeng"]=29,
	["nanfeng"]=30,
	["beifeng"]=31,
	["hongzhong"]=32,
	["facai"]=33,
	["baiban"]=34,
}
--通过数字得到牌名字
GetNameByNum={
	[0]="GuiPai",
	[1]="yiwan",
	[2] ="erwan",
	[3]="sanwan",
	[4]="siwan",
	[5]="wuwan",
	[6]="liuwan",
	[7]="qiwan",
	[8]="bawan",
	[9]="jiuwan",
	[10]="yitong",
	[11]="ertong",
	[12]="santong",
	[13]="sitong",
	[14]="wutong",
	[15]="liutong",
	[16]="qitong",
	[17]="batong",
	[18]="jiutong",
	[19]="yitiao",
	[20]="ertiao",
	[21]="santiao",
	[22]="sitiao",
	[23]="wutiao",
	[24]="liutiao",
	[25]="qitiao",
	[26]="batiao",
	[27]="jiutiao",
	[28]="dongfeng",
	[29]="xifeng",
	[30]="nanfeng",
	[31]="beifeng",
	[32]="hongzhong",
	[33]="facai",
	[34]="baiban",
}
