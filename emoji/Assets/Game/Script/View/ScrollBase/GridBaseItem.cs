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
* Filename: GridBaseItem
* Created:  2016/1/31 11:37:44
* Author:   HaYaShi ToShiTaKa
* Purpose:  Scroll View Grid 类型 的item基类
* ==============================================================================
*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBaseItem : UIBase {
	private IList _datas;
	private int _index;
	private UIScrollView.Movement _moveType;
	private UIScrollView _scrollView;
	protected UIGrid _grid;
	[HideInInspector]
	public int itemCount;//grid填满的数量
	[HideInInspector]
	public UIBase parentUI;
	public T GetBaseUI<T>() where T : UIBase {
		return parentUI as T;
	}
	public int index { get { return _index; } }
	public UIGrid grid {
		set {
			_grid = value;
			_scrollView = _grid.transform.parent.GetComponent<UIScrollView>();
			_moveType = _scrollView.movement;
		}
		get { return _grid; }
	}
	public virtual void FindItem() {
	}
	public virtual void FillItem(IList datas, int index) {
		_datas = datas;
		_index = index;
		int gridIndex = _grid.maxPerLine == 0 ? index : Mathf.FloorToInt((float)index / (float)_grid.maxPerLine);
		int lineIndex = _grid.maxPerLine == 0 ? 0 : index % _grid.maxPerLine;

		if (_moveType == UIScrollView.Movement.Horizontal) {
			transform.localPosition = new Vector3(_grid.cellWidth * gridIndex, -_grid.cellHeight * lineIndex, 0);
		}
		else if (_moveType == UIScrollView.Movement.Vertical) {
			transform.localPosition = new Vector3(_grid.cellWidth * lineIndex, -_grid.cellHeight * gridIndex, 0);
		}
		UnRegistUIButton(gameObject);
	}
	public void UpdateItem() {
		if (_datas == null) return;
		if (_datas.Count <= 0) return;
		FillItem(_datas, _index);
	}
	public void DeleteItem<T>(GridItemList<T> items) where T : GridBaseItem {
		if (_datas == null) return;
		if (_datas.Count <= 0) return;

		//没满就重写构造这个grid
		if (_datas.Count < itemCount) {
			_grid.CreateScrollView<T>(gameObject, _datas, items, parentUI);
		}
		else {
			UpdateItem();
		}
	}
}