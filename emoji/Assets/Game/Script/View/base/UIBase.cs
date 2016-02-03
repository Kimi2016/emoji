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
* Filename: UIBase
* Created:  2016/2/3 14:56:02
* Author:   HaYaShi ToShiTaKa
* Purpose:  UI的基类
* ==============================================================================
*/
using System;
using System.Collections.Generic;
using UnityEngine;

public class UIBase : MonoBehaviour {
	private Dictionary<Type, UIBase> childUIs = null;
	private UIBase _parentUI = null;

	#region virtual
	//阶段函数
	public virtual void OnLoadedUI(bool close3dTouch, object args) {
		//在加载完UI资源后调用，一般用于获取UI节点跟注册一些点击函数，刷新函数
		childUIs = new Dictionary<Type, UIBase>();
		//判断如果是根节点就显示出那个gameObject
		if (GetParentUI<UIBase>() == null) {
			gameObject.SetActive(true);
		}
	}

	public virtual void OnOpenUI(object args) {
		//在打开UI之前调用，一般用于初始化刷新界面
		foreach (var item in childUIs) {
			item.Value.OnOpenUI(args);
		}
	}

	public virtual void OnCloseUI() {
		//在关闭的UI的之前调用，一般用于清理一些临时生成的UI资源
		foreach (var item in childUIs) {
			item.Value.OnCloseUI();
		}
	}

	public virtual void OnFreeUI() {
		//释放UI内存的时候调用
		foreach (var item in childUIs) {
			item.Value.OnFreeUI();
		}
		childUIs.Clear();
	}
	#endregion

	#region button
	protected void RegistUIButton(GameObject button, UIEventListener.VoidDelegate action) {
		UIEventListener listener = UIEventListener.Get(button);
		listener.onClick += (go) => {
			action(go);
		};
	}
	protected void UnRegistUIButton(GameObject button) {
		UIEventListener listener = UIEventListener.Get(button);
		listener.onClick = null;
	}

	protected void RegistOnTogglePress(GameObject button, EventDelegate.Callback action) {
		UIToggle toggle = button.GetComponent<UIToggle>();
		if (toggle != null) {
			EventDelegate.Add(button.GetComponent<UIToggle>().onChange, action);
		}
	}
	protected void RegistOnPress(GameObject button, UIEventListener.BoolDelegate action) {
		UIEventListener listener = UIEventListener.Get(button);
		listener.onPress += action;
	}
	#endregion

	/// <summary>
	/// 在父节点中获取子模板脚本
	/// </summary>
	/// <typeparam name="T">字模板类</typeparam>
	/// <returns>子模板脚本</returns>
	public T GetUIBase<T>() where T : UIBase {
		UIBase result;

		if (childUIs.TryGetValue(typeof(T), out result)) {
			return result as T;
		}
		return null;
	}
	/// <summary>
	/// 把子模板的脚本绑定在指定的Transform上
	/// </summary>
	/// <typeparam name="T">字模板类</typeparam>
	/// <param name="ts">需要绑定子模板的Transform</param>
	protected T RegistUIBase<T>(Transform ts) where T : UIBase {
		return RegistUIBase<T>(ts, null);
	}

	protected T RegistUIBase<T>(Transform ts, object args) where T : UIBase {
		T com = null;
		if (ts != null) {
			com = ts.gameObject.AddComponent<T>();
			AddChildUI<T>(com);
			com.OnLoadedUI(false, args);
		}
		else {
			Debug.LogWarning("获取控件为空，请检查");
		}
		return com;
	}

	protected void AddChildUI<T>(UIBase childUI) where T : UIBase {
		if (childUIs != null) {
			childUIs[typeof(T)] = childUI;
			childUI._parentUI = this;
		}
	}

	protected T GetParentUI<T>() where T : UIBase {
		return _parentUI as T;
	}
}