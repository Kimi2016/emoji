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
* Filename: UIWidgetTools
* Created:  2016/1/20 16:46:18
* Author:   HaYaShi ToShiTaKa
* Purpose:  扩展NGUI的功能
* ==============================================================================
*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public delegate void FillItemFun(Transform fillItem, object data);
static public class UIWidgetTools {
	static public void DestroyChildren(this Transform t) {
		bool isPlaying = Application.isPlaying;

		while (t.childCount != 0) {
			Transform child = t.GetChild(0);

			if (isPlaying) {
				child.parent = null;
				UnityEngine.Object.Destroy(child.gameObject);
			}
			else UnityEngine.Object.DestroyImmediate(child.gameObject);
		}
	}
	/// <summary>
	/// 向gird中填充内容，
	/// 注意这个grid parent必须是scroll view,templateItem必须带有 UIDragView这个component。
	/// 具体可以参照PetView,prefab的做法可以参照pet_ui的Popup_Levelup
	/// </summary>
	/// <param name="grid">UIGrid的控件</param>
	/// <param name="templateItem">填充的单位元件</param>
	/// <param name="datas">填充的逻辑数据</param>
	/// <param name="fillAction">填充函数</param>
	static public void CreateScrollView(this UIGrid grid, GameObject templateItem, IList datas, FillItemFun fillAction) {
		// 删除UI项目
		grid.transform.DestroyChildren();

		for (int i = 0; i < datas.Count; i++) {
			GameObject go = NGUITools.AddChild(grid.gameObject, templateItem);
			go.SetActive(true);
			fillAction(go.transform, datas[i]);
		}
		grid.Reposition();
	}
	static public void AddScrollViewChild(this UIGrid grid, GameObject templateItem, object data, FillItemFun fillAction) {

		int fillCount = 0; //当前scrollView被填满的格子数	
		GameObject go = NGUITools.AddChild(grid.gameObject, templateItem);
		go.SetActive(true);
		grid.Reposition();
		int childCount = grid.GetChildList().Count;
		// TODO 暂时定为20，以后根据实际情况调整
		if (childCount > 20) {
			Transform t = grid.GetChildList()[0];
			grid.RemoveChild(t);
			GameObject.Destroy(t.gameObject);
		}
		else {
			UIScrollView scrollView = grid.transform.parent.GetComponent<UIScrollView>();

			if (scrollView.movement == UIScrollView.Movement.Vertical) {
				fillCount = Mathf.RoundToInt(scrollView.GetComponent<UIPanel>().height / grid.cellHeight);
			}
			else if (scrollView.movement == UIScrollView.Movement.Horizontal) {
				fillCount = Mathf.RoundToInt(scrollView.GetComponent<UIPanel>().width / grid.cellWidth);
			}
			if (childCount > fillCount) {
				scrollView.MoveRelative(new Vector3(0, grid.cellHeight, 0));
			}
		}
		fillAction(go.transform, data);
	}
}