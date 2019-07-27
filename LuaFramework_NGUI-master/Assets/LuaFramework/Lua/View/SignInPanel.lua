---
--- Generated by EmmyLua(https://github.com/EmmyLua)
--- Created by Administrator.
--- DateTime: 2019/6/24 14:54
---
local gameobject
local transform
SignInPanel={}
local this=SignInPanel
function  SignInPanel.Awake(obj)
    logWarn("SignInPanel.Awake--->>");
    gameobject=obj
    transform=obj.transform
    this.InitPanel()
end
function  SignInPanel.Start()
    logWarn("SignInPanel.Satrt--->>");
end
---查找初始UI
function SignInPanel.InitPanel()
    --郑州麻将
    this.ZhengZhowMaJiang=transform:Find("CenterBtn/ZhengZhouMaJiangBtn")
    --快速开始
    this.KuaiSuKaiShi=transform:Find("CenterBtn/QuickStartBtn")
    --签到
    this.QianDao=transform:Find("RightBtn/QianDao")
    --战绩
    this.Record=transform:Find("RightBtn/Record")
    --规则
    this.Rule=transform:Find("RightBtn/Rule")
    --设置
    this.Setting=transform:Find("RightBtn/Setting")
    --退出
    this.QuitGame=transform:Find("RightBtn/Quit")
    --头像
    this.HeadTex=transform:Find("PlayerInfo/InfoBg/Head/HeadTex"):GetComponent("UITexture")
    --id文本
    this.IdLabelInfo=transform:Find("PlayerInfo/InfoBg/IDLabel/IdLabelInfo"):GetComponent("UILabel")
    --等级文本
    this.LevelLabelInfo=transform:Find("PlayerInfo/InfoBg/LevelBg/LevelLabelInfo"):GetComponent("UILabel")
    --经验值文本
    this.ExperienceBgInfo=transform:Find("PlayerInfo/InfoBg/ExperienceBg/ExperienceBgInfo"):GetComponent("UILabel")
    --Slider
    this.ExeSlider=transform:Find("PlayerInfo/InfoBg/Slider/ExperienceSlider"):GetComponent("UISlider")
    --当前等级文本
    this.CurrentLabel=transform:Find("PlayerInfo/InfoBg/Slider/CurrentLabel"):GetComponent("UILabel")
    --下一等级文本
    this.NextLabel=transform:Find("PlayerInfo/InfoBg/Slider/NextLabel"):GetComponent("UILabel")
    --下一等级经验数量
    this.AllExperience=transform:Find("PlayerInfo/InfoBg/Slider/AllExperience"):GetComponent("UILabel")
    --距离下一等级经验数量
    this.NextTitleInfo=transform:Find("PlayerInfo/InfoBg/NextTitleBg/NextTitleInfo"):GetComponent("UILabel")
end
