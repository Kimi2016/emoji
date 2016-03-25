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
using UnityEngine;
public enum enumChatType {
	CHANEL_CURRENT = 0,  /// 当前频道
	CHANEL_WORLD,        ///世界频道
	CHANEL_GUILD,        /// 帮会频道
	CHANEL_PRIVATE,      /// 私聊频道
	CHANEL_TEAM,         /// 队伍频道
	CHANEL_SYSTEM,       /// 系统频道
};
public enum enumSysInfoType {
	INFO_TYPE_CHAT = 1,/// 系统信息、GM信息，在聊天窗口
	INFO_TYPE_GAME, /// 游戏信息，屏幕左上
};
public class ChatData {
	public readonly string playName;
	public readonly string text;
	public readonly enumChatType chatType;
	public readonly enumSysInfoType systemType;
	public readonly byte[] voiceData;
	public readonly float time;
	public readonly long userID = 0;
	public string chatTypeName {
		get {
			return GameConst.ChannelNameDict[chatType];
		}
	}
	public Color chatTypeColor {
		get {
			return GameConst.ChatColorDict.ContainsKey(chatType) ? GameConst.ChatColorDict[chatType] : Color.white;
		}
	}
	public ChatData(string playName, string text, enumChatType chatType, enumSysInfoType systemType, byte[] voiceData,float time,long userID) {
		this.playName = playName;
		this.text = SetTextColor(text, chatType);
		this.chatType = chatType;
		this.systemType = systemType;
		this.voiceData = voiceData;
		this.time = time;
		this.userID = userID;
	}
	private string SetTextColor(string text, enumChatType chatType){
		switch(chatType) {
			case enumChatType.CHANEL_WORLD:
				text = "[ff0000]" + text + "[-]";
			break;
		}
		return text;
	}
	public ChatData(string playName, string text, int chatType, int systemType)
		: this(playName, text, (enumChatType)chatType, (enumSysInfoType)systemType) {
	}
	public ChatData(string playName, string text, enumChatType chatType, enumSysInfoType systemType)
		: this(playName, text, chatType, systemType, null, 0, 0) {
	}
}
public class ChatDataManager {
	private const int MAX_CHAT_DATA_COUNT = 10000;
	public const int MAX_TEXT_LENGTH = 200;
	private static ChatDataManager _instance;
	private List<string> _history = new List<string>();
	private EventDispatcher mDispatcher;
	public static ChatDataManager GetInstance() {
		if (_instance == null) {
			_instance = new ChatDataManager();
		}

		return _instance;
	}
	private List<ChatData> mChatDataList;
	private Dictionary<enumChatType, List<ChatData>> mChatDataDict;
	public List<string> history {
		get {
			return _history;
		}
	}
	public void AddHistory(string text) {
		if (_history.Contains(text)) {
			_history.Remove(text);
		}
		_history.Add(text);
		
	}
	public List<ChatData> chatDataList {
		get { return mChatDataList; }
	}
	public List<ChatData> SortChatData(enumChatType chatType) {

		List<ChatData> result;
		if (!mChatDataDict.TryGetValue(chatType, out result)) {
			result = new List<ChatData>();
			for (int i = 0; i < mChatDataList.Count; i++) {
				if (mChatDataList[i].chatType == chatType) {
					result.Add(mChatDataList[i]);
				}
			}
			mChatDataDict[chatType] = result;
		}
		return result;
	}
	public ChatDataManager() {
		mChatDataList = new List<ChatData>();
		mChatDataDict = new Dictionary<enumChatType, List<ChatData>>();
		mDispatcher = Director.GetInstance().eventDispatcher;
	}
	public void ClearChatHistory() {
		mChatDataList.Clear();
		mDispatcher.Notify(EnumEventDispathcer.ChatViewRefreshHistory);
	}
	public void AddChatData(ChatData data) {
		AddChatData(data, true);
	}
	public void AddChatData(ChatData data,bool isRefresh) {
		if (mChatDataList.Count == MAX_CHAT_DATA_COUNT) {
			mChatDataList.RemoveAt(0);
		}
		mChatDataList.Add(data);
		List<ChatData> sortData;
		if (!mChatDataDict.TryGetValue(data.chatType, out sortData)) {
			sortData = new List<ChatData>();
			mChatDataDict[data.chatType] = sortData;
		}
		sortData.Add(data);
		if (isRefresh) {
			mDispatcher.Notify(EnumEventDispathcer.ChatViewRefreshPanel);
		}
	}
}