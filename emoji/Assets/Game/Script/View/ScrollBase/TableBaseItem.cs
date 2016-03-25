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
* Filename: TableBaseItem
* Created:  2016/2/12 17:33:06
* Author:   HaYaShi ToShiTaKa
* Purpose:  Scroll View Table 类型 的item基类
* ==============================================================================
*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableBaseItem : UIBase {
	private IList _datas;
	private int _index;
	protected UITable _table;
	[HideInInspector]
	public int itemCount;//grid填满的数量
	[HideInInspector]
	public UIBase parentUI;
	public T GetBaseUI<T>() where T : UIBase {
		return parentUI as T;
	}
	public int index { 
		get { 
			return _index; 
		}
		set {
			_index = value;
		}
	}
	public UITable table {
		set {
			_table = value;
		}
		get { return _table; }
	}
	public virtual void FindItem() {
	}
	public virtual void FillItem(IList datas, int index) {
		_datas = datas;
		this.index = index;
		UnRegistUIButton(gameObject);
	}
	public void UpdateItem() {
		if (_datas == null) return;
		if (_datas.Count <= 0) return;
		FillItem(_datas, _index);
	}
	public void DeleteItem<T>(TableItemList<T> items) where T : TableBaseItem {
		if (_datas == null) return;
		if (_datas.Count <= 0) return;

		//没满就重写构造这个grid
		if (_datas.Count < itemCount) {
			_table.CreateScrollView<T>(gameObject, _datas, items, parentUI);
		}
		else {
			UpdateItem();
		}
	}
}