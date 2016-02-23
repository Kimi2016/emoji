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
	static public void CreateScrollView<T>(this UIGrid grid, GameObject templateItem, IList datas, GridItemList<T> scrollItemList, UIBase parentUI)
		where T : GridBaseItem {
		if (scrollItemList == null) {
			scrollItemList = new GridItemList<T>(new List<List<T>>(), parentUI);
		}

		List<List<T>> scrollItems = scrollItemList.items;
		scrollItems = new List<List<T>>();
		scrollItemList.datas = datas;

		// 删除UI项目
		grid.transform.DestroyChildren();
		UIScrollView scrollView = NGUITools.FindInParents<UIScrollView>(grid.gameObject);

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
			fillCount = Mathf.CeilToInt(panel.height / grid.cellHeight);
		}
		else if (moveType == UIScrollView.Movement.Horizontal) {
			fillCount = Mathf.CeilToInt(panel.width / grid.cellWidth);
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

				realItemCount = Mathf.CeilToInt((float)datas.Count / (float)maxPerLine);

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
							MoveGridItem<T>(scrollItems, moveType, datas, maxPerLine, ref minIndex, ref maxIndex, ref forwardCacheNum, true, true);
						}

					}
					// 滑到底的时候，把上部缓存的那一个item移动到下部
					if ((forwardCacheNum > 0 && index + itemCount == realItemCount)) {
						//上（左）移动到下（右）
						MoveGridItem<T>(scrollItems, moveType, datas, maxPerLine, ref minIndex, ref maxIndex, ref forwardCacheNum, true, true);
					}

					for (int i = 1; i <= offset; i++) {
						//上（左）移动到下（右）
						MoveGridItem<T>(scrollItems, moveType, datas, maxPerLine, ref minIndex, ref maxIndex, ref forwardCacheNum, true, false);
					}

				}
				else {
					forwardCacheNum = forwardCacheNum - offset;
					//缓存数量
					while ((forwardCacheNum < cacheNum - 1 && index >= cacheNum - 1)
						|| (forwardCacheNum < 0 && index < cacheNum - 1)) {
						// 下（右）移动到上（左）
						MoveGridItem<T>(scrollItems, moveType, datas, maxPerLine, ref minIndex, ref maxIndex, ref forwardCacheNum, false, true);
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
				if (i * maxPerLine + j >= datas.Count) { break; }
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

		scrollItemList.itemCount = itemCount * maxPerLine;
		scrollItemList.grid = grid;
		scrollItemList.itemTemplate = templateItem;
		lastPos = panel.transform.localPosition;

		grid.Reposition();
	}

	static private void MoveGridItem<T>(
		List<List<T>> scrollItems, UIScrollView.Movement moveType, IList datas, int maxPerLine, ref int minIndex, ref int maxIndex,
		ref int forwardCacheNum, bool isTopToBottom, bool isMoveForward) where T : GridBaseItem {

		List<T> items;
		// 判断是否是 上（左）移动到下（右)
		int curIndex;
		int itemIndex;
		int sign;
		if (isTopToBottom) {
			curIndex = maxIndex + 1;
			itemIndex = 0;
			sign = 1;
		}
		else {
			curIndex = minIndex - 1;
			itemIndex = scrollItems.Count - 1;
			sign = -1;
		}
		items = scrollItems[itemIndex];

		int targetIndex = itemIndex == 0 ? scrollItems.Count - 1 : 0;

		scrollItems.Remove(items);
		scrollItems.Insert(targetIndex, items);

		for (int i = 0; i < items.Count; i++) {
			if (!isMoveForward && curIndex * maxPerLine + i > datas.Count - 1) {
				break;
			}
			T item = items[i];
			item.FillItem(datas, curIndex * maxPerLine + i);
		}

		minIndex += sign;
		maxIndex += sign;
		if (isMoveForward) {
			forwardCacheNum -= sign;
		}

	}

	static public void CreateScrollView<T>(this UITable table, GameObject templateItem, IList datas, TableItemList<T> scrollItemList, UIBase parentUI)
		where T : TableBaseItem {
		if (scrollItemList == null) {
			scrollItemList = new TableItemList<T>(new List<T>(), parentUI);
		}

		List<T> scrollItems = scrollItemList.items;
		UIScrollView scrollView = NGUITools.FindInParents<UIScrollView>(table.gameObject);
		UIScrollView.Movement moveType = table.columns == 0 ? UIScrollView.Movement.Horizontal : UIScrollView.Movement.Vertical;
		UITable.Direction direction = table.direction;
		int itemCount;//grid的行(列)数量
		UIWidget tmpWidget = templateItem.GetComponent<UIWidget>();
		int cellHeight = tmpWidget.GetComponent<UIWidget>().height;
		int cellWidth = tmpWidget.GetComponent<UIWidget>().width;
		int fillCount = 0; //当前scrollView被填满的格子数
		int cacheNum = 3; //多出来的缓存格子
		Vector3 lastPos = Vector3.zero;
		UIPanel panel = scrollView.GetComponent<UIPanel>();

		scrollItems = new List<T>();
		scrollItemList.datas = datas;
		if (scrollItems == null) { scrollItems = new List<T>(); }
		else { scrollItems.Clear(); }

		// 删除UI项目
		table.transform.DestroyChildren();
		itemCount = datas.Count;
		if (moveType == UIScrollView.Movement.Vertical) {
			fillCount = Mathf.CeilToInt(panel.height / cellHeight);
		}
		else if (moveType == UIScrollView.Movement.Horizontal) {
			fillCount = Mathf.CeilToInt(panel.width / cellWidth);
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
				int index = 0;//当前显示出来的第一个格子，在grid数据中的index
				float curItemDistance = 0;

				distance = delata.y != 0 ? delata.y : delata.x;
				// 满的时候向上滑不管它
				if (distance > 0 && direction == UITable.Direction.Down) return;
				if (distance < 0 && direction == UITable.Direction.Up) return;

				distance = Mathf.Abs(distance);

				curItemDistance = CalItemDistance(moveType, scrollItems[scrollItems.Count - 1].transform.localPosition);
				if (curItemDistance < distance) {
					index = Mathf.Min(scrollItems[scrollItems.Count - 1].index + 1, datas.Count - 1);
				}
				else {
					for (int i = 0; i < scrollItems.Count; i++) {
						curItemDistance = CalItemDistance(moveType, scrollItems[i].transform.localPosition);
						if (curItemDistance >= distance) {
							index = Mathf.Max(scrollItems[i].index - 1, 0);
							break;
						}
					}
				}

				// 拖拽不满一个单元格
				if (index == lastIndex) return;

				// 拉到底了
				if (index + itemCount > datas.Count) {
					if (lastIndex + itemCount == datas.Count) {
						return;
					}
					else {
						index = datas.Count - itemCount;
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
							MoveTableItem<T>(scrollItems, moveType, datas, ref minIndex, ref maxIndex, ref forwardCacheNum, true, true, direction);
						}

					}
					// 滑到底的时候，把上部缓存的那一个item移动到下部
					if ((forwardCacheNum > 0 && index + itemCount == datas.Count)) {
						//上（左）移动到下（右）
						MoveTableItem<T>(scrollItems, moveType, datas, ref minIndex, ref maxIndex, ref forwardCacheNum, true, true, direction);
					}

					for (int i = 1; i <= offset; i++) {
						//上（左）移动到下（右）
						MoveTableItem<T>(scrollItems, moveType, datas, ref minIndex, ref maxIndex, ref forwardCacheNum, true, false, direction);
					}

				}
				else {
					forwardCacheNum = forwardCacheNum - offset;
					//缓存数量
					while ((forwardCacheNum < cacheNum - 1 && index >= cacheNum - 1)
						|| (forwardCacheNum < 0 && index < cacheNum - 1)) {
						// 下（右）移动到上（左)
						MoveTableItem<T>(scrollItems, moveType, datas, ref minIndex, ref maxIndex, ref forwardCacheNum, false, true, direction);
					}
				}

				lastIndex = index;

			};

		}

		// 添加能填满UI数量的button
		for (int i = 0; i < itemCount; i++) {
			GameObject go = NGUITools.AddChild(table.gameObject, templateItem);
			go.SetActive(true);
			T item = go.AddComponent<T>();
			item.table = table;
			item.itemCount = itemCount;
			item.parentUI = parentUI;
			scrollItems.Add(item);
			item.FindItem();
			item.FillItem(datas, i);
		}

		scrollItemList.itemCount = itemCount;
		scrollItemList.table = table;
		scrollItemList.itemTemplate = templateItem;
		lastPos = panel.transform.localPosition;
		table.Reposition();
	}
	static public void CreateScrollView<T>(this UITable table, GameObject templateItem, IList datas, UIBase parentUI) where T : TableBaseItem {
		// 删除UI项目
		int itemCount;//grid的行(列)数量

		table.transform.DestroyChildren();
		itemCount = datas.Count;

		// 添加能填满UI数量的button
		for (int i = 0; i < itemCount; i++) {
			GameObject go = NGUITools.AddChild(table.gameObject, templateItem);
			go.SetActive(true);
			T item = go.AddComponent<T>();
			item.table = table;
			item.itemCount = itemCount;
			item.parentUI = parentUI;
			item.FindItem();
			item.FillItem(datas, i);
		}
		table.Reposition();

	}
	static private float CalItemDistance(UIScrollView.Movement moveType, Vector3 postion) {
		float moveLength = 0;
		if (moveType == UIScrollView.Movement.Horizontal) {
			moveLength = Math.Abs(postion.x);
		}
		else {
			moveLength = Math.Abs(postion.y);
		}
		return moveLength;
	}
	static private void MoveTableItem<T>(
		List<T> scrollItems, UIScrollView.Movement moveType, IList datas, ref int minIndex, ref int maxIndex,
		ref int forwardCacheNum, bool isTopToBottom, bool isMoveForward, UITable.Direction direction) where T : TableBaseItem {
		T item;
		// 判断是否是 上（左）移动到下（右)
		int curIndex;
		int itemIndex;
		int sign;
		if (isTopToBottom) {
			curIndex = maxIndex + 1;
			itemIndex = 0;
			sign = 1;
		}
		else {
			curIndex = minIndex - 1;
			itemIndex = scrollItems.Count - 1;
			sign = -1;
		}
		item = scrollItems[itemIndex];

		int targetIndex = itemIndex == 0 ? scrollItems.Count - 1 : 0;
		T targetItem = scrollItems[targetIndex];
		Vector3 targetPos = targetItem.transform.localPosition;

		scrollItems.Remove(item);
		scrollItems.Insert(targetIndex, item);

		item.FillItem(datas, curIndex);
		UIWidget tmpWidget;

		if (direction == UITable.Direction.Down) {
			if (isTopToBottom) {
				tmpWidget = targetItem.GetComponent<UIWidget>();
			}
			else {
				tmpWidget = item.GetComponent<UIWidget>();
			}
		}
		else {
			if (!isTopToBottom) {
				tmpWidget = targetItem.GetComponent<UIWidget>();
			}
			else {
				tmpWidget = item.GetComponent<UIWidget>();
			}
		}

		int cellHeight = tmpWidget.GetComponent<UIWidget>().height;
		int cellWidth = tmpWidget.GetComponent<UIWidget>().width;

		ReSetCellPostion<T>(item, moveType, targetPos, isTopToBottom, cellWidth, cellHeight, direction);


		minIndex += sign;
		maxIndex += sign;
		if (isMoveForward) {
			forwardCacheNum -= sign;
		}

	}
	static private void ReSetCellPostion<T>(T item, UIScrollView.Movement moveType, Vector3 pos, bool isTopToBottom, int cellWidth, int cellHeight, UITable.Direction direction) where T : TableBaseItem {
		int sign = 1;
		if (direction == UITable.Direction.Down) {
			if (isTopToBottom) {
				sign = -1;
			}
			else {
				sign = 1;
			}
		}
		else {
			if (!isTopToBottom) {
				sign = -1;
			}
			else {
				sign = 1;
			}
		}

		if (moveType == UIScrollView.Movement.Horizontal) {
			item.transform.localPosition = new Vector3(pos.x + sign * cellWidth, pos.y, 0);
		}
		else if (moveType == UIScrollView.Movement.Vertical) {
			item.transform.localPosition = new Vector3(pos.x, pos.y + sign * cellHeight, 0);
		}
	}
}