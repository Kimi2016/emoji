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
	/* panel被填满的数量 */
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
	
	public bool RefreshTable() {
		bool result = false;
		if (table == null) {
			return false;
		}
		UIScrollView scrollView = NGUITools.FindInParents<UIScrollView>(table.gameObject);
		
		UIPanel panel = scrollView.GetComponent<UIPanel>();
		Vector3 pretablePos = table.transform.localPosition;
		Vector3 preScrollPos = scrollView.transform.localPosition;
		Vector2 preOffset = panel.clipOffset;
		
		if (items != null && datas.Count >= itemCount + 1) {
			for (int i = 0; i < items.Count; i++) {
				items[i].UpdateItem();
				result = true;
			}
			table.Reposition();
			table.transform.localPosition = pretablePos;
			scrollView.transform.localPosition = preScrollPos;
		}

		return result;
	}
	public bool MoveUpTable() {
		bool result = false;
		if (table == null) {
			return false;
		}
		if (items != null && datas.Count >= itemCount + 1) {
			if (items.Count > 0) {
				MoveUpItem();
				result = true;
			}
		}

		return result;
	}
	private void MoveUpItem(){
		int sign = table.direction == UITable.Direction.Up ? 1 : -1;
		T targetItem = items[0];
		Vector3 targetPos = targetItem.transform.localPosition;
		T moveItem = items[items.Count - 1];
		moveItem.FillItem(datas, 0);
		UIWidget tmpWidget = moveItem.GetComponent<UIWidget>();
		int height = tmpWidget.height;
		targetPos.y -= targetItem.GetComponent<UIWidget>().height;

		for (int i = 0; i < items.Count; i++) {
			Vector3 tmpPos = items[i].transform.localPosition;
			tmpPos.y += sign * height;
			items[i].transform.localPosition = tmpPos;
			items[i].index += 1;
		}

		items.Remove(moveItem);
		items.Insert(0, moveItem);
		targetPos.y += height;
		moveItem.transform.localPosition = targetPos;
	}

}