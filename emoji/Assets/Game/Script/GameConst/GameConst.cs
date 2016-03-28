/*
               #########                       
              ############                     
              #############                    
             ##  ###########                   
            ###  ###### #####                  
            ### #######   ####                 
           ###  ########## ####                
          ####  ########### ####               
         ####   ###########  #####             
        #####   ### ########   #####           
       #####   ###   ########   ######         
      ######   ###  ###########   ######       
     ######   #### ##############  ######      
    #######  #####################  ######     
    #######  ######################  ######    
   #######  ###### #################  ######   
   #######  ###### ###### #########   ######   
   #######    ##  ######   ######     ######   
   #######        ######    #####     #####    
    ######        #####     #####     ####     
     #####        ####      #####     ###      
      #####       ###        ###      #        
        ###       ###        ###              
         ##       ###        ###               
__________#_______####_______####______________

                我们的未来没有BUG              
* ==============================================================================
* Filename: GameConst
* Created:  2016/3/24 17:22:49
* Author:   HaYaShi ToShiTaKa
* Purpose:  游戏中与逻辑无关的常量
* ==============================================================================
*/
using System;
using System.Collections.Generic;
using UnityEngine;

public class GameConst {
	static public class ResourceUrl {
		public const string UI_URL = "UI/";
		public const string UI_ROOT = "UI/UI Root";
	}
	static public class LuaClass {
		public const string DIRECTORY_URL = "lua/Director.lua";
	}
	public static Dictionary<enumChatType, string> ChannelNameDict = new Dictionary<enumChatType, string> {
		{enumChatType.CHANEL_CURRENT,"当前"},
		{enumChatType.CHANEL_WORLD,"世界"},
		{enumChatType.CHANEL_GUILD,"帮会"},
		{enumChatType.CHANEL_PRIVATE,"私聊"},
		{enumChatType.CHANEL_TEAM,"队伍"},
		{enumChatType.CHANEL_SYSTEM,"系统"},
	};
	public static Dictionary<enumChatType, Color> ChatColorDict = new Dictionary<enumChatType, Color> {
		{enumChatType.CHANEL_SYSTEM, new Color(0.96f,0.23f,0.23f)},
		{enumChatType.CHANEL_TEAM, new Color(1.0f,1.0f,1.0f)},
		{enumChatType.CHANEL_WORLD, new Color(0.03f,0.73f,0.29f)},
		{enumChatType.CHANEL_CURRENT, new Color(0.96f,0.79f,0.14f)},
		{enumChatType.CHANEL_GUILD, new Color(1.0f,0.96f,0.09f)},
	};
	public readonly static List<string> ChanelList = new List<string> {
		"当前","世界",
		"帮会","私聊",
		"队伍","系统"
	};
	public static class Layer {
		public static int Default = LayerMask.NameToLayer("Default");
		public static int UI = LayerMask.NameToLayer("UI");
		public static int Character = LayerMask.NameToLayer("Character");
		public static int Ground = LayerMask.NameToLayer("Ground");
	} 
	public static Dictionary<string, AnimationVO> animationConfig = new Dictionary<string, AnimationVO> { 
		{"000",new AnimationVO(2,false,6,false,3)},
		{"001",new AnimationVO(6,false,4,false,2)},
		{"002",new AnimationVO(2,false,2,false,1)},
		{"995",new AnimationVO(6,true,2,false,1)},//宠物经验图标
		{"996",new AnimationVO(6,true,2,false,1)},//钻石图标
		{"997",new AnimationVO(6,true,2,false,1)},//金币图标
		{"998",new AnimationVO(6,true,2,false,1)},//人物经验图标
		{"999",new AnimationVO(6,true,2,true,1)},//声音的播放
	};
	public static AnimationVO GetAnimationConfigByIndex(string index) {
		AnimationVO result = null;
		animationConfig.TryGetValue(index, out result);
		return result;
	}
}
public class AnimationVO {
	public readonly int rate;
	public readonly bool isNotPlay;
	public readonly int spaceNum;
	public readonly bool isAudio;
	public readonly int rowNum;
	public AnimationVO(int rate, bool isNotPlay, int spaceNum, bool isAudio, int rowNum) {
		this.rate = rate;
		this.isNotPlay = isNotPlay;
		this.spaceNum = spaceNum;
		this.isAudio = isAudio;
		this.rowNum = rowNum;
	}
	public AnimationVO() {
	}
}