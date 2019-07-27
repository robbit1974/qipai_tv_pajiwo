---
--- Generated by EmmyLua(https://github.com/EmmyLua)
--- Created by Administrator.
--- DateTime: 2019/6/24 13:40
---
------------------------------------------这里是面向对象类的封装,此类过于臃肿建议优化,最好做到继承----------------------------------------------------------------
require "Logic/TV_KeyCode"
ButtonBase={}
local this=ButtonBase
--按钮类
function ButtonBase:New(go)
    local NewButton={}
    setmetatable(NewButton,{__index=self})
    NewButton.gameObject=go.gameObject
    --上下左右四个按钮
    NewButton.left=nil
    NewButton.right=nil
    NewButton.up=nil
    NewButton.down=nil
    --点检ok按钮函数
    NewButton.okFunc=nil
    --点击ok按钮是否恢复按钮原状,有的按钮点击确定制之后不需要回复原装,例如设置音乐与音效的按钮
    NewButton.isReverse=true
    --上下左右方法函数,有的按钮需要执行不同的方法,例如规则
    NewButton.leftFunc=nil
    NewButton.rightFunc=nil
    NewButton.upFunc=nil
    NewButton.downFunc=nil
    --刚开始选择按钮函数,比如刚选择签到按钮,签到按钮需要高亮
    --之前SetStartFunc函数的代码都实现在startFunc函数中,后来发现重写startFunc函数时,需要将 SetStartFunc函数重写一遍,为了不必要的代码重写,所以定义了startFunc函数
    --具体可参考RuleCtrl.SetRuleBtnStartFun()函数中的重写   注意理解SetStartFunc() 与 SetStart()函数的区别
    NewButton.startFunc=function()
        NewButton:SetStartFunc()
    end
    --当不在选择该按钮时,恢复按钮原状
    NewButton.EndFunc=function()
        NewButton:SetEndFunc()
    end
    --对象池的名字,回收go
    NewButton.PoolName=nil
    return NewButton
end
----- 按钮高亮   (注意不要与SetStart()方法搞混) 此方法可以应对大多数情况
function ButtonBase:SetStartFunc()
    --是否选择该按钮,一种是带高亮边框,另一种是改变按钮大小并修改颜色值,另一种测试用
    if self.gameObject:GetComponent("TweenScale") then
        self.gameObject:GetComponent("TweenScale"):PlayForward()
        self.gameObject:GetComponent("UISprite").color=Color.New(0.5,0.5,0.5,1)
    elseif self.gameObject.transform:Find("GaoLiang") then
        self.gameObject.transform:Find("GaoLiang").gameObject:SetActive(true)
    elseif self.gameObject:GetComponent("UILabel") then
        self.gameObject:GetComponent("UILabel").color=Color.New(1,0,0,1)
    elseif self.gameObject:GetComponent("UISprite") then
        self.gameObject:GetComponent("UISprite").color=Color.New(0.5,0.5,0.5,1)
    end
end
---回复按钮
function ButtonBase:SetEndFunc()
    --是否选择该按钮,一种是带高亮边框,另一种是改变按钮大小并修改颜色值,另一种测试用
    if self.gameObject:GetComponent("TweenScale") then
        self.gameObject:GetComponent("TweenScale"):PlayReverse()
        self.gameObject:GetComponent("UISprite").color=Color.New(1,1,1,1)
    elseif self.gameObject.transform:Find("GaoLiang") then
        self.gameObject.transform:Find("GaoLiang").gameObject:SetActive(false)
    elseif self.gameObject:GetComponent("UILabel") then
        self.gameObject:GetComponent("UILabel").color=Color.New(0,0,0,1)
    elseif self.gameObject:GetComponent("UISprite") then
        self.gameObject:GetComponent("UISprite").color=Color.New(1,1,1,1)
    end
end
--改变图片
function ButtonBase:SetSprite(spritename)
    local sprite=self.gameObject:GetComponent("UISprite")
    if sprite then
        sprite.spriteName=spritename
    else
        log(self.gameObject.name.."不存在UISprite")
    end
end
--设置位置
function ButtonBase:SetPos(vec)
   self.gameObject.transform.localPosition=vec
end
--设置大小
function ButtonBase:SetScale(vec)
    self.gameObject.transform.localScale=vec
end
--左侧表
function ButtonBase:SetLeft(left)
    self.left=left
end
--右侧表
function ButtonBase:SetRight(right)
    self.right=right
end
--上册表
function ButtonBase:SetUp(up)
    self.up=up
end
--下册表
function ButtonBase:SetDown(down)
    self.down=down
end
function ButtonBase:SetAllTable(left,right,up,down)
    self.left=left
    self.right=right
    self.up=up
    self.down=down
end
--开始选择
function ButtonBase:SetStart()
    if self.startFunc then
        TV_KeyCode.CurrentTable=self
        self.startFunc()
    end
end
--开始选择
function ButtonBase:SetEnd()
    if self.EndFunc then
        self.EndFunc()
    end
end
--确定按钮
function ButtonBase:SetOk()
    if self.okFunc then
        if self.isReverse then
            self.EndFunc()
        end
        self:okFunc()
        --有些点击ok之后不需要执行结束函数,例如音效设置
    end
    BaseHelper.PlaySound("ui_click")
end
----------------------------------------此处运用了大量的if 语法,在底层编译时会产生较多垃圾语句,建议使用 三元运算 例如        local x=self.left and (self.left:SetStart()) or nil
--当点击遥控器左侧按钮
function ButtonBase:LeftBtn()
    if self.left then
        -- TV_KeyCode.CurrentTable 表示当前所选择的按钮
        TV_KeyCode.CurrentTable=self.left
        self.left:SetStart()
        self.EndFunc()
    end
    if self.leftFunc then
        self.leftFunc()
    end
    BaseHelper.PlaySound("ui_click")
end
--当点击遥控器右侧按钮
function ButtonBase:RightBtn()
    if self.right then
        TV_KeyCode.CurrentTable=self.right
        self.right:SetStart()
        self.EndFunc()
    end
    if self.rightFunc then
        self.rightFunc()
    end
    BaseHelper.PlaySound("ui_click")
end
--当点击遥控器上侧按钮
function ButtonBase:UpBtn()
    if self.up then
        TV_KeyCode.CurrentTable=self.up
        self.up:SetStart()
        self.EndFunc()
    end
    if self.upFunc then
        self.upFunc()
    end
    BaseHelper.PlaySound("ui_click")
end
--当点击遥控器下侧按钮
function ButtonBase:DownBtn()
    if self.down then
        TV_KeyCode.CurrentTable=self.down
        self.down:SetStart()
        self.EndFunc()
    end
    if self.downFunc then
        self.downFunc()
    end
    BaseHelper.PlaySound("ui_click")
end

--对象池回收
function ButtonBase:ReleaseGo()
    objPoolMgr:Release(self.PoolName,self.gameObject)
    self=nil
end
