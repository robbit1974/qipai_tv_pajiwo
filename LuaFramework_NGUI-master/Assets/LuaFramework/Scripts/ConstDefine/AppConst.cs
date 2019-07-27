using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine.Networking;

namespace LuaFramework {
    public class AppConst{
        public const bool DebugMode = false;                        //调试模式-用于内部测试

        /// <summary>
        /// 如果想删掉框架自带的例子，那这个例子模式必须要
        /// 关闭，否则会出现一些错误。
        /// </summary>
        public const bool ExampleMode = true;                       //例子模式 

        /// <summary>
        /// 如果开启更新模式，前提必须启动框架自带服务器端。
        /// 否则就需要自己将StreamingAssets里面的所有内容
        /// 复制到自己的Webserver上面，并修改下面的WebUrl。
        /// </summary>
        public const bool UpdateMode = false;                      //更新模式-默认关闭 
        public const bool LuaByteMode = false;                     //Lua字节码模式-默认关闭 
        public const bool LuaBundleMode = true;                   //Lua代码AssetBundle模式-默认关闭 

        public const int TimerInterval = 1;
        public const int GameFrameRate = 30;                       //游戏帧频

        public const string AppName = "LuaFramework";               //应用程序名称
        public const string LuaTempDir = "Lua/";                    //临时目录
        public const string ExtName = ".unity3d";                   //资源扩展名
        public const string AppPrefix = AppName + "_";              //应用程序前缀
        public const string WebUrl = "http://localhost:6688/";      //测试更新地址

        public static string Mac = "";
        public static string UserId = string.Empty;                 //用户ID
        public static string DaTingSocketPort = "";                 //大厅webSocket服务器
        public static string DaTingUserToken = "";                  //用户大厅token值  在lua中赋值 

        public static string GameSocketPort = "";                 //游戏webSocket服务器
        public static string GameUserToken = "";                  //游戏用户token值 在lua中赋值 



        public static string FrameworkRoot {
            get {
                return Application.dataPath + "/" + AppName;
            }
        }

        public static byte[] GetByte(string s)
        {
            return Encoding.UTF8.GetBytes(s);
        }
      
    }
}