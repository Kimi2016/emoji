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
* Filename: ScrollViewItemList
* Created:  2016/2/1 18:06:43
* Author:   HaYaShi ToShiTaKa
* Purpose:  ScrollItem 的管理类
* ==============================================================================
*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollViewItemList<T> where T : ScrollViewBaseItem {
	public List<List<T>> items;
	public IList datas;
	public int itemCount;
	public UIGrid grid;
	private UIBase parentUI;
	public GameObject itemTemplate;
	public ScrollViewItemList(List<List<T>> items, IList datas, UIBase parentUI) {
		this.items = items;
		this.datas = datas;
		this.parentUI = parentUI;
	}
	public TYPE GetParentUI<TYPE>() where TYPE : UIBase {
		return parentUI as TYPE;
	}
	public int Length { get { return items.Count; } }
	private T this[int index] {
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
	public void AddItem<TYPE>(int index, TYPE data) {
		if (datas.Count >= itemCount) {
			datas.Insert(index, data);
			T item = this[index];
			item.UpdateItem();
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
		grid.CreateScrollView<T>(itemTemplate, datas, this, parentUI);
	}

}