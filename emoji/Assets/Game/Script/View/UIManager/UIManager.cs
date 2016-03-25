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
* Filename: UIManager
* Created:  2016/3/24 16:40:04
* Author:   HaYaShi ToShiTaKa
* Purpose:  UI管理器
* ==============================================================================
*/
using System;
using System.Collections.Generic;
using UnityEngine;

public class UIManager {
	private static UIManager mInstance;
	public static UIManager MakeInstance() { 
		if(mInstance == null){
			mInstance = new UIManager();
		}
		return mInstance;
	}

	List<EnumUIName> mCachedUINameList;
	private Dictionary<EnumUIName,UIBase> mCachedUIDict; //存储在内存的UI
	private Transform mUIRoot;
	UIManager() {
		mCachedUIDict = new Dictionary<EnumUIName, UIBase>();
		mCachedUINameList = new List<EnumUIName>();
		UICamera camera = GameObject.FindObjectOfType<UICamera>();
		if (camera != null) {
			mUIRoot = camera.transform;
		}
		else {
			GameObject go = Resources.Load<GameObject>(GameConst.ResourceUrl.UI_ROOT);
			mUIRoot = GameObject.Instantiate<GameObject>(go).transform;
			mUIRoot.name = go.name;
		}
	}
	
	#region open close
	public void OpenView(EnumUIName uiName) {
		OpenView(uiName, null, true);
	}
	public void OpenView(EnumUIName uiName, object args) {
		OpenView(uiName, args, true);
	}
	public void OpenView(EnumUIName uiName, object args, bool closeOtherUI) {
		
		if (closeOtherUI) {
			CloseAllExitView();
		}

		bool isUIOpen = CheckExitView(uiName);
		UIBase uiBase = isUIOpen ? mCachedUIDict[uiName] : LoadUISource(uiName, args);

		if (isUIOpen && uiBase.isActive) { 
			return; 
		}
		uiBase.OnOpenUI(args);
	}

	public void CloseView(EnumUIName uiName) {
		CloseView(uiName, false);
	}
	public void CloseView(EnumUIName uiName, bool needFree) {
		if (!CheckExitView(uiName)) {
			return;
		}
		UIBase uiBase = mCachedUIDict[uiName];
		if (!uiBase.isActive) {
			uiBase.OnCloseUI();
		}
		if (needFree) {
			FreeView(uiName);
		}
	}
	#endregion

	#region load and free
	private UIBase LoadUISource(EnumUIName uiName, object args) {
		string name = uiName.ToString();
		GameObject ui = Resources.Load<GameObject>(GameConst.ResourceUrl.UI_URL + name);
		ui = GameObject.Instantiate(ui);
		ui.transform.parent = mUIRoot;
		ui.transform.localScale = Vector3.one;
		ui.transform.localPosition = Vector3.zero;
		ui.name = name;

		UIBase uiBase = ui.GetComponent<UIBase>();
		if (uiBase == null) {
			uiBase = ui.AddComponent(Type.GetType(name)) as UIBase;
		}
		mCachedUINameList.Add(uiName);
		mCachedUIDict[uiName] = uiBase;
		uiBase.OnLoadedUI(true, args);
		return uiBase;
	}
	private void FreeView(EnumUIName uiName) {
		UIBase uiBase = mCachedUIDict[uiName];
		mCachedUINameList.Remove(uiName);
		mCachedUIDict.Remove(uiName);
		GameObject.Destroy(uiBase.gameObject);
	}

	private bool CheckExitView(EnumUIName name) {
		return mCachedUINameList.Contains(name);
	}
	#endregion

	private void CloseAllExitView() {
		for (int i = 0; i < mCachedUINameList.Count; i++) { 
			CloseView(mCachedUINameList[i]);
		}
	}
}