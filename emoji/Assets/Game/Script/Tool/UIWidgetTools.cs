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
	static public void CreateScrollView<T>(this UIGrid grid, GameObject templateItem, IList datas, ScrollViewItemList<T> scrollItemList, UIBase parentUI) where T : ScrollViewBaseItem {
		if (scrollItemList == null) {
			scrollItemList = new ScrollViewItemList<T>(new List<List<T>>(), datas, parentUI);
		}
		List<List<T>> scrollItems = scrollItemList.items;
		// 删除UI项目
		grid.transform.DestroyChildren();
		UIScrollView scrollView = grid.transform.parent.GetComponent<UIScrollView>();

		if (scrollItems == null) { scrollItems = new List<List<T>>(); }
		else { scrollItems.Clear(); }

		int maxPerLine = grid.maxPerLine == 0 ? 1 : grid.maxPerLine;
		int itemCount;//grid的行(列)数量
		int realItemCount;//原本grid的行(列)数量
		itemCount = Mathf.CeilToInt((float)datas.Count / (float)maxPerLine);
		realItemCount = itemCount;

		int fillCount = 0; //当前scrollView被填满的格子数
		int cacheNum = 3; //多出来的缓存格子
		Vector3 lastPos = Vector3.zero;
		UIPanel panel = scrollView.GetComponent<UIPanel>();
		UIScrollView.Movement moveType = scrollView.movement;
		if (moveType == UIScrollView.Movement.Vertical) {
			fillCount = Mathf.RoundToInt(panel.height / grid.cellHeight);
		}
		else if (moveType == UIScrollView.Movement.Horizontal) {
			fillCount = Mathf.RoundToInt(panel.width / grid.cellWidth);
		}

		// 面板没被占满拖拽回滚
		if (!scrollView.disableDragIfFits) {
			if (itemCount <= fillCount) {
				lastPos = panel.transform.localPosition;
				scrollView.onMomentumMove = () => { };
				scrollView.onMomentumMove = () => {
					SpringPanel.Begin(panel.gameObject, lastPos, 13f).strength = 8f;
				};
			}
		}

		// 如果item数量大于填满显示面板的数量做优化
		if (itemCount > fillCount + cacheNum) {
			itemCount = fillCount + cacheNum;
			int lastIndex = 0; //上次显示出来的第一个格子，在grid数据中的index
			int maxIndex = itemCount - 1;
			int minIndex = 0;
			int forwardCacheNum = 0;//用于缓存向指定方向滑动，预加载的格子数
			// 拖拽刷新面板
			panel.onClipMove = (uiPanel) => {
				Vector3 delata = lastPos - panel.transform.localPosition;
				float distance = -1;
				int index;//当前显示出来的第一个格子，在grid数据中的index
				distance = delata.y != 0 ? delata.y : delata.x;
				// 满的时候向上滑不管它
				if (distance > 0) return;

				distance = -distance;

				if (moveType == UIScrollView.Movement.Horizontal) {
					index = Mathf.FloorToInt(distance / grid.cellWidth);
				}
				else {
					index = Mathf.FloorToInt(distance / grid.cellHeight);
				}


				// 拖拽不满一个单元格
				if (index == lastIndex) return;

				// 拉到底了
				if (index + itemCount > realItemCount) {
					if (lastIndex + itemCount == realItemCount) {
						return;
					}
					else {
						index = realItemCount - itemCount;
					}
				}
				// 重刷
				int offset = Math.Abs(index - lastIndex);
				// 判断要把最上（左）的item移动到最下（右）,还是相反
				if (lastIndex < index) {
					//如果有上一次的缓存数量，就清掉
					if (forwardCacheNum > 0) {
						while (forwardCacheNum > 1) {
							//上（左）移动到下（右）
							int curIndex = maxIndex + 1;
							List<T> items = scrollItems[0];
							scrollItems.Remove(items);
							scrollItems.Add(items);

							for (int i = 0; i < items.Count; i++) {
								T item = items[i];
								item.FillItem(datas, curIndex * maxPerLine + i);
							}

							minIndex++;
							maxIndex++;
							forwardCacheNum--;
						}

					}
					// 滑到底的时候，把上部缓存的那一个item移动到下部
					if ((forwardCacheNum > 0 && index + itemCount == realItemCount)) {
						//上（左）移动到下（右）
						int curIndex = maxIndex + 1;
						List<T> items = scrollItems[0];
						scrollItems.Remove(items);
						scrollItems.Add(items);

						for (int i = 0; i < items.Count; i++) {
							T item = items[i];
							item.FillItem(datas, curIndex * maxPerLine + i);
						}

						minIndex++;
						maxIndex++;
						forwardCacheNum--;
					}

					for (int i = 1; i <= offset; i++) {
						//上（左）移动到下（右）
						int curIndex = maxIndex + 1;
						List<T> items = scrollItems[0];
						scrollItems.Remove(items);
						scrollItems.Add(items);

						for (int j = 0; j < items.Count; j++) {
							if (curIndex * maxPerLine + j > datas.Count - 1) {
								lastIndex = index;
								minIndex++;
								maxIndex++;
								return;
							}
							T item = items[j];
							item.FillItem(datas, curIndex * maxPerLine + j);
						}
						minIndex++;
						maxIndex++;
					}

				}
				else {
					forwardCacheNum = forwardCacheNum - offset;
					//缓存数量
					while ((forwardCacheNum < cacheNum - 1 && index >= cacheNum - 1)
						|| (forwardCacheNum < 0 && index < cacheNum - 1)) {
						// 下（右）移动到上（左）
						int curIndex = minIndex - 1;
						List<T> items = scrollItems[scrollItems.Count - 1];
						scrollItems.Remove(items);
						scrollItems.Insert(0, items);

						for (int i = 0; i < items.Count; i++) {
							T item = items[i];
							//if (curIndex < 0) {
							//	lastIndex = index;
							//	return;
							//}
							item.FillItem(datas, curIndex * maxPerLine + i);
						}

						minIndex--;
						maxIndex--;
						forwardCacheNum++;
					}
				}
				lastIndex = index;

			};

			//如果这个函数BUG了，请把下面解开下面代码的封印，强行不BUG

			//scrollView.onMomentumMove = () => { };
			//scrollView.onMomentumMove = () => {
			//	for (int i = lastIndex; i < itemCount + lastIndex; i++) {
			//		for (int j = 0; j < scrollItems[i - lastIndex].Count; j++) {
			//			scrollItems[i - lastIndex][j].FillItem(datas, i * maxPerLine + j);
			//		}
			//	}
			//};
		}

		// 添加能填满UI数量的button
		for (int i = 0; i < itemCount; i++) {
			for (int j = 0; j < maxPerLine; j++) {
				GameObject go = NGUITools.AddChild(grid.gameObject, templateItem);
				go.SetActive(true);
				T item = go.AddComponent<T>();
				item.grid = grid;
				item.itemCount = itemCount;
				item.parentUI = parentUI;
				if (scrollItems.Count - 1 < i) {
					scrollItems.Add(new List<T>());
				}
				scrollItems[i].Add(item);
				item.FindItem();
				item.FillItem(datas, i * maxPerLine + j);
			}
		}

		scrollItemList.itemCount = itemCount * grid.maxPerLine;
		scrollItemList.grid = grid;
		scrollItemList.itemTemplate = templateItem;
		lastPos = panel.transform.localPosition;

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