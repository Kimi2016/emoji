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
* Filename: ChatDataManager
* Created:  2016/1/28 18:26:40
* Author:   HaYaShi ToShiTaKa
* Purpose:  聊天数据客户端缓存
* ==============================================================================
*/
using System;
using System.Collections.Generic;
public enum enumChatType {
	CHAT_TYPE_PRIVATE = 1,/// 私聊频道
	CHAT_TYPE_NINE,/// 轻聊频道
	CHAT_TYPE_TEAM,         /// 队伍频道
	CHAT_TYPE_FRIEND,       /// 好友频道
	CHAT_TYPE_GM,           /// GM聊频道
	CHAT_TYPE_SYSTEM,       /// 系统频道
	CHAT_TYPE_UNION,        /// 帮会频道
	CHAT_TYPE_POP,          /// 弹出式系统提示
	CHAT_TYPE_PERSON,       /// 个人频道
	CHAT_TYPE_WHISPER,      ///悄悄话
	CHAT_TYPE_WHISPERTOME,///悄悄话
	CHAT_TYPE_COUNTRY,      /// 国家频道
	CHAT_TYPE_AREA,///区域频道
	CHAT_TYPE_FAMILY,       /// 家族频道

	CHAT_TYPE_FRIEND_AFFICHE,/// 好友公告
	CHAT_TYPE_UNION_AFFICHE,/// 帮会公告
	CHAT_TYPE_OVERMAN_AFFICHE,/// 师门公告
	CHAT_TYPE_FAMILY_AFFICHE,/// 家族公告

	CHAT_TYPE_FRIEND_PRIVATE,/// 好友私聊
	CHAT_TYPE_UNION_PRIVATE,/// 帮会私聊
	CHAT_TYPE_OVERMAN_PRIVATE,/// 师门私聊
	CHAT_TYPE_FAMILY_PRIVATE,/// 家族私聊

	CHAT_TYPE_NPC,///npc说话

	CHAT_TYPE_EMOTION,///表情
	CHAT_TYPE_SHOPADV,///摆摊广告
	CHAT_TYPE_WORLD,///世界频道
	CHAT_TYPE_OVERMAN,/// 师门频道
	CHAT_TYPE_AUTO,/// 自动回复
	CHAT_TYPE_COUNTRY_PK,/// 外国人入侵PK消息
	CHAT_TYPE_BLESS_MSG,/// 个人祝福消息
	CHAT_TYPE_COUNTRY_MARRY,/// 结婚消息广播
	CHAT_TYPE_ERROR_GM,///发送到GM工具的警告信息
	CHAT_TYPE_MINIGAME  /// 玩小游戏聊天
};
public enum enumSysInfoType {
	INFO_TYPE_SYS = 1,/// 系统信息、GM信息，在聊天窗口
	INFO_TYPE_GAME, /// 游戏信息，屏幕左上
	INFO_TYPE_STATE,        /// 状态转换，屏幕左上
	INFO_TYPE_FAIL,         /// 失败信息，屏幕左上
	INFO_TYPE_EXP,  /// 特殊信息,获得经验、物品，在人物头上
	INFO_TYPE_MSG,  /// 弹出用户确认框的系统消息
	INFO_TYPE_KING, /// 国王发出的聊天消息
	INFO_TYPE_CASTELLAN,/// 城主发出的聊天消息
	INFO_TYPE_EMPEROR,/// 皇帝发出的聊天消息
	INFO_TYPE_SCROLL,/// 屏幕上方滚动的系统信息
};
public class ChatData {
	public readonly string playName;
	public readonly string text;
	public readonly enumChatType chatType;
	public readonly enumSysInfoType systemType;
	public readonly byte[] voiceData;
	public readonly float time;
	public ChatData(string playName, string text, enumChatType chatType, enumSysInfoType systemType, byte[] voiceData,float time) {
		this.playName = playName;
		this.text = text;
		this.chatType = chatType;
		this.systemType = systemType;
		this.voiceData = voiceData;
		this.time = time;
	}
}
public class ChatDataManager {
	private const int MAX_CHAT_DATA_COUNT = 1000;
	private static ChatDataManager _instance;
	public static ChatDataManager GetInstance() {
		if (_instance == null) {
			_instance = new ChatDataManager();
		}

		return _instance;
	}
	private List<ChatData> _chatDataList;
	public List<ChatData> chatDataList {
		get { return _chatDataList; }
	}

	public ChatDataManager() {
		_chatDataList = new List<ChatData>();
	}
	public void AddChatData(ChatData data) {
		if (_chatDataList.Count == MAX_CHAT_DATA_COUNT) {
			_chatDataList.RemoveAt(_chatDataList.Count - 1);
		}
		_chatDataList.Insert(0, data);
	}
}