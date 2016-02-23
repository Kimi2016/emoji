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
* Filename: TableItemList
* Created:  2016/2/12 17:30:48
* Author:   HaYaShi ToShiTaKa
* Purpose:  ScrollItem Table类型 的管理类
* ==============================================================================
*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableItemList<T> where T : TableBaseItem {
	public List<T> items;
	public IList datas;
	public int itemCount;
	public UITable table;
	private UIBase parentUI;
	public GameObject itemTemplate;
	public TableItemList(List<T> items, UIBase parentUI) {
		this.items = items;
		this.parentUI = parentUI;
	}
	public TYPE GetBaseUI<TYPE>() where TYPE : UIBase {
		return parentUI as TYPE;
	}
	public int Length { get { return items.Count; } }
	private T this[int index] {
		get {
			T item = null;
			for (int i = 0; i < Length; i++) {
				if (items[i].index == index) {
					item = items[i];
				}
			}
			return item;
		}
	}
	public void AddItem<TYPE>(int index, TYPE data) {
		if (datas.Count >= itemCount) {
			datas.Insert(index, data);
			T item = this[index];
			item.UpdateItem();
			UIScrollView scrollView = NGUITools.FindInParents<UIScrollView>(table.gameObject);
			UIPanel panel = scrollView.GetComponent<UIPanel>();
			panel.transform.localPosition = items[items.Count - 1].transform.localPosition;
		}
		else {
			datas.Insert(index, data);
			RefreshGrid();
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
	public void RefreshGrid() {
		table.CreateScrollView<T>(itemTemplate, datas, this, parentUI);
	}

}