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
* Filename: ChatUIPopMenu
* Created:  2016/3/13 16:43:18
* Author:   HaYaShi ToShiTaKa
* Purpose:  聊天窗口弹出菜单
* ==============================================================================
*/
using System;
using System.Collections.Generic;
using UnityEngine;

public class ChatUIPopMenu : UIBase {
	
	#region member
	GameObject _btnEmoji;
	GameObject _btnItem;
	GameObject _btnPet;
	GameObject _btnTask;
	GameObject _btnHonor;
	GameObject _btnQuickTalk;
	GameObject _btnHistory;

	Transform _emojiPanel;
	UIGrid _gridEmoji;
	GameObject _emojiTemplate;
	List<string> _emojiNames;
	GridItemList<EmojiListItem> _emojiGridList;

	Transform _bagPanel;
	GameObject _btnAllItem;
	GameObject _btnPotionItem;
	UIGrid _gridBag;
	GameObject _itemTemplate;

	Transform _petPanel;
	UIGrid _gridPet;
	GameObject _petTemplate;

	Transform _taskPanel;
	UIGrid _gridTask;
	GameObject _taskTemplate;

	Transform _honorPanel;
	UIGrid _gridHonor;
	GameObject _honorTemplate;

	Transform _quickTalkPanel;
	UIGrid _gridTalk;
	GameObject _talkTemplate;
	List<string> _allTalks;
	List<Transform> talkTsList;
	List<ChatCommonTalkItem> talkCoList;
	ChatCommonTalkItem saveItem;

	Transform _historyPanel;
	UIGrid _gridHistory;
	GameObject _historyTemplate;
	GridItemList<ChatHistoryItem> _historyGridList;
	List<string> _historyList;
	ChatDetailView _parentUI;
	int _currentIndex = -1;
	#endregion

	#region virtual
	public override void OnLoadedUI(bool close3dTouch, object args) {
		base.OnLoadedUI(close3dTouch, args);
		_btnEmoji = transform.Find("L/botton1").gameObject;
		_btnItem = transform.Find("L/botton2").gameObject;
		_btnPet  = transform.Find("L/botton3").gameObject;
		_btnTask = transform.Find("L/botton4").gameObject;
		_btnHonor = transform.Find("L/botton5").gameObject;
		_btnQuickTalk = transform.Find("L/botton6").gameObject;
		_btnHistory = transform.Find("L/botton7").gameObject;

		_emojiPanel = transform.Find("R/Emoji");
		_gridEmoji = transform.Find("R/Emoji/Grid").GetComponent<UIGrid>();
		_emojiTemplate = transform.Find("R/Emoji/emoji_template").gameObject;
		_emojiTemplate.SetActive(false);
		_emojiGridList = new GridItemList<EmojiListItem>(null, this);

		_bagPanel = transform.Find("R/Bag");
		_btnAllItem = transform.Find("R/Bag/bottom1").gameObject;
		_btnPotionItem = transform.Find("R/Bag/bottom2").gameObject;
		_gridBag = transform.Find("R/Bag/Scroll View/Grid").GetComponent<UIGrid>();

		_petPanel = transform.Find("R/Pet");
		_gridPet = transform.Find("R/Pet/Grid").GetComponent<UIGrid>();
		_petTemplate = transform.Find("R/Pet/pet_template").gameObject;
		_petTemplate.SetActive(false);

		_taskPanel = transform.Find("R/quest");
		_gridTask = transform.Find("R/quest/Grid").GetComponent<UIGrid>();
		_taskTemplate = transform.Find("R/quest/quest_template").gameObject;
		_taskTemplate.SetActive(false);

		_honorPanel = transform.Find("R/chenghao");
		_gridHonor = transform.Find("R/chenghao/Grid").GetComponent<UIGrid>();
		_honorTemplate = transform.Find("R/chenghao/honor_template").gameObject;
		_honorTemplate.SetActive(false);
		

		_quickTalkPanel = transform.Find("R/massage");
		_gridTalk = transform.Find("R/massage/Grid").GetComponent<UIGrid>();
		_talkTemplate = transform.Find("R/massage/message_template").gameObject;

		_historyPanel = transform.Find("R/History");
		_gridHistory = transform.Find("R/History/Grid").GetComponent<UIGrid>();
		_historyTemplate = transform.Find("R/History/history_template").gameObject;
		_historyTemplate.SetActive(false);
		_historyGridList = new GridItemList<ChatHistoryItem>(null, this);
		_historyList = ChatDataManager.GetInstance().history;

		_parentUI = GetParentUI<ChatDetailView>();

		List<List<Transform>> contents = new List<List<Transform>>{
			new List<Transform>{_emojiPanel},new List<Transform>{_bagPanel},
			new List<Transform>{_petPanel},new List<Transform>{_taskPanel},
			new List<Transform>{_honorPanel},new List<Transform>{_quickTalkPanel},
			new List<Transform>{_historyPanel},
		};
		List<GameObject> activeButtons = new List<GameObject> {
			_btnEmoji,_btnItem,_btnPet,_btnTask,_btnHonor,
			_btnQuickTalk,_btnHistory,
		};
		_emojiNames = new List<string>();
		_emojiNames.Add("000_2");
		_emojiNames.Add("001_2");
		_emojiNames.Add("002_2");

		_gridEmoji.CreateScrollView<EmojiListItem>(_emojiTemplate, _emojiNames, _emojiGridList, GetParentUI<ChatDetailView>());

		UIWidgetTools.RegistUITapButton(activeButtons, contents);
		RegistUIButton(_btnHistory, (go) => {
			RefreshHistory();
		});

	}
	public override void OnOpenUI(object args) {
		base.OnOpenUI(args);
	}

	#endregion

	public void RefreshHistory() {
		if (_gridHistory.gameObject.activeSelf) {
			_gridHistory.CreateScrollView<ChatHistoryItem>(_historyTemplate, _historyList, _historyGridList, GetParentUI<ChatDetailView>());
		}
	}
}