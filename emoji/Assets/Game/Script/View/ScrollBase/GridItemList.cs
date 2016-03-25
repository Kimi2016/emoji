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
* Filename: GridItemList
* Created:  2016/2/1 18:06:43
* Author:   HaYaShi ToShiTaKa
* Purpose:  ScrollItem Grid类型 的管理类
* ==============================================================================
*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridItemList<T> where T : GridBaseItem {
	public List<List<T>> items;
	public ICollection datas;
	public int itemCount;
	public UIGrid grid;
	private UIBase parentUI;
	public GameObject itemTemplate;
	private UIPanel _panel;
	public UIPanel panel {
		set {
			preScrollPos = value.transform.localPosition;
			preOffset = value.clipOffset;
			_panel = value;
		}
		get {
			return _panel;
		}
	}
	private Vector3 preScrollPos;
	private Vector2 preOffset;
	public GridItemList(List<List<T>> items, UIBase parentUI) {
		this.items = items;
		this.parentUI = parentUI;
	}
	public TYPE GetBaseUI<TYPE>() where TYPE : UIBase {
		return parentUI as TYPE;
	}
	public int Length { get { return items.Count; } }
	public T this[int index] {
		get {
			T item = null;
			for (int i = 0; i < Length; i++) {
				for (int j = 0; j < items[i].Count; j++) {
					if (items[i][j].index == index) {
						item = items[i][j];
					}
				}
			}
			return item;
		}
	}
	public void UpdateItem(int index) {
		T item = this[index];
		if (item != null) {
			item.UpdateItem();
		}
	}
	public void DeleteItem(int index) {
		T item = this[index];
		if (item != null) {
			item.DeleteItem<T>(this);
		}
	}
	public bool IsItemFull() {
		return items != null && datas.Count >= itemCount + 1;
	}
	public bool RefreshGrid() {
		bool result = false;
		if (IsItemFull()) {
			for (int i = 0; i < Length; i++) {
				for (int j = 0; j < items[i].Count; j++) {
					items[i][j].UpdateItem();
					result = true;
				}
			}
			panel.transform.localPosition = preScrollPos;
			panel.clipOffset = preOffset;
			if (panel.onClipMove != null) {
				panel.onClipMove(panel);
			}
		}
		
		return result;
	}
	public void ForceRefresh() {
		for (int i = 0; i < Length; i++) {
			for (int j = 0; j < items[i].Count; j++) {
				items[i][j].UpdateItem();
			}
		}
	}

}