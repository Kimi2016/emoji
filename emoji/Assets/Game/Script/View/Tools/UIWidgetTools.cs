using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

static public class UIWidgetTools {
	public delegate void FillItemFun(Transform fillItem, object data);
	public delegate void CSharpFun(object[] args);
	/// <summary>
	/// 向gird中填充内容，
	/// 注意这个grid parent必须是scroll view,templateItem必须带有 UIDragView这个component。
	/// 具体可以参照PetView,prefab的做法可以参照pet_ui的Popup_Levelup
	/// </summary>
	/// <param name="grid">UIGrid的控件</param>
	/// <param name="templateItem">填充的单位元件</param>
	/// <param name="datas">填充的逻辑数据</param>
	/// <param name="fillAction">填充函数</param>
	static public List<Transform> CreateScrollView(this UIGrid grid, GameObject templateItem, IList datas, FillItemFun fillAction) {

		// 删除UI项目
		List<Transform> result = grid.GetChildList();
		grid.transform.DestroyChildren();

		for (int i = 0; i < datas.Count; i++) {
			GameObject go = NGUITools.AddChild(grid.gameObject, templateItem);
			result.Add(go.transform);
			go.SetActive(true);
			fillAction(go.transform, datas[i]);
		}
		grid.Reposition();

		UIScrollView scrollView = grid.transform.parent.GetComponent<UIScrollView>();
		if (scrollView != null) {
			SetScrollViewDragNoFit(scrollView, grid);
		}

		return result;

	}
	static public void CreateScrollView<T>(this UIGrid grid, GameObject templateItem, IList datas, GridItemList<T> scrollItemList, UIBase parentUI)
		where T : GridBaseItem {
		if (scrollItemList == null) {
			scrollItemList = new GridItemList<T>(null, parentUI);
		}
		if (scrollItemList.RefreshGrid())
			return;
		if (scrollItemList.items == null) {
			scrollItemList.items = new List<List<T>>();
		}
		List<List<T>> scrollItems = scrollItemList.items;
		scrollItems.Clear();
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
		panel.onClipMove = null;
		UIScrollView.Movement moveType = scrollView.movement;
		if (moveType == UIScrollView.Movement.Vertical) {
			fillCount = (int)(panel.height / grid.cellHeight);
		}
		else if (moveType == UIScrollView.Movement.Horizontal) {
			fillCount = (int)(panel.width / grid.cellWidth);
		}
		scrollView.onMomentumMove = null;
		// 面板没被占满拖拽回滚
		if (!scrollView.disableDragIfFits) {
			if (itemCount <= fillCount) {
				lastPos = panel.transform.localPosition;
				scrollView.onMomentumMove = () => {
					SpringPanel.Begin(panel.gameObject, lastPos, 13f).strength = 8f;
				};
			}
		}

		// 如果item数量大于填满显示面板的数量做优化
		if (itemCount >= fillCount + cacheNum) {
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
				if (distance > 0 && moveType == UIScrollView.Movement.Vertical) return;
				if (distance < 0 && moveType == UIScrollView.Movement.Horizontal) return;

				distance = Mathf.Abs(distance);

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

		scrollItemList.itemCount = (fillCount + cacheNum) * maxPerLine;
		scrollItemList.grid = grid;
		scrollItemList.itemTemplate = templateItem;
		scrollItemList.datas = datas;
		scrollItemList.panel = panel;
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
			scrollItemList = new TableItemList<T>(null, parentUI);
		}
		if (scrollItemList.items == null) {
			scrollItemList.items = new List<T>();
		}
		List<T> scrollItems = scrollItemList.items;
		scrollItems.Clear();
		UIScrollView scrollView = NGUITools.FindInParents<UIScrollView>(table.gameObject);
		UIScrollView.Movement moveType = table.columns == 0 ? UIScrollView.Movement.Horizontal : UIScrollView.Movement.Vertical;
		UITable.Direction direction = table.direction;
		int itemCount = datas.Count;//grid的行(列)数量
		UIWidget tmpWidget = templateItem.GetComponent<UIWidget>();
		int cellHeight = tmpWidget.GetComponent<UIWidget>().height;
		int cellWidth = tmpWidget.GetComponent<UIWidget>().width;
		int fillCount = 0; //当前scrollView被填满的格子数
		int cacheNum = 3; //多出来的缓存格子
		Vector3 lastPos = Vector3.zero;
		UIPanel panel = scrollView.GetComponent<UIPanel>();
		panel.onClipMove = null;
		// 删除UI项目
		table.transform.DestroyChildren();
		if (moveType == UIScrollView.Movement.Vertical) {
			fillCount = Mathf.CeilToInt(panel.height / cellHeight);
		}
		else if (moveType == UIScrollView.Movement.Horizontal) {
			fillCount = Mathf.CeilToInt(panel.width / cellWidth);
		}

		// 如果item数量大于填满显示面板的数量做优化
		if (itemCount >= fillCount + cacheNum) {
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
				if (distance > 0 && direction == UITable.Direction.Down) {
					return; 
				}
				if (distance < 0 && direction == UITable.Direction.Up) {
					return;
				}
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
					int targetNum = direction == UITable.Direction.Down ? cacheNum - 1 : cacheNum - 2;
					while ((forwardCacheNum < targetNum && index >= cacheNum - 1) || (forwardCacheNum < 0 && index < cacheNum - 1)) {
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
			scrollItems[i].FillItem(datas, i);
		}
		scrollItemList.itemCount = fillCount + cacheNum;
		scrollItemList.table = table;
		scrollItemList.itemTemplate = templateItem;
		scrollItemList.datas = datas;
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
	static public void MoveTableItem<T>(
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

	static private void SetScrollViewDragNoFit(UIScrollView scrollView, UIGrid grid) {
		if (scrollView.disableDragIfFits) { return; }
		int childCount = Mathf.FloorToInt((float)grid.GetChildList().Count / (float)grid.maxPerLine);
		int fillCount = 0; //当前scrollView被填满的格子数
		UIPanel panel = scrollView.GetComponent<UIPanel>();
		if (scrollView.movement == UIScrollView.Movement.Vertical) {
			fillCount = Mathf.RoundToInt(panel.height / grid.cellHeight);
		}
		else if (scrollView.movement == UIScrollView.Movement.Horizontal) {
			fillCount = Mathf.RoundToInt(panel.width / grid.cellWidth);
		}
		if (childCount < fillCount) {
			Vector3 lastPos = panel.transform.localPosition;
			scrollView.onMomentumMove = () => {
				SpringPanel.Begin(panel.gameObject, lastPos, 13f).strength = 8f;
			};
		}

	}

	/// <summary>
	/// Reset transform
	/// </summary>
	static public void ResetTransform(GameObject go) {
		ResetTransform(go.transform);
	}

	/// <summary>
	/// Reset transform
	/// </summary>
	static public void ResetTransform(Transform ts) {
		ts.localPosition = Vector3.zero;
		ts.localRotation = Quaternion.identity;
		ts.localScale = Vector3.one;
	}

	/// <summary>
	/// 注意三个List的长度必须等长，且activeButtons的第一个GameObject就是加载完UI后处于活跃状态的，具体可以参照PetView
	/// </summary>
	/// <param name="activeButons">处于活跃状态的按钮</param>
	/// <param name="dactiveButtons">处于不活跃状态的按钮</param>
	/// <param name="contents">根据按钮活跃状态显示的内容</param>
	static public void RegistUITapButton(List<GameObject> activeButtons, List<GameObject> dactiveButtons, List<List<Transform>> contents) {
		// 判断Tap控件数量是否相等，不相等就不用处理了。
		// TODO锁定状态
		if (activeButtons.Count != dactiveButtons.Count) {
			return;
		}
		if (activeButtons.Count != contents.Count) {
			return;
		}
		for (int i = 0; i < dactiveButtons.Count; i++) {
			if (i == 0) {
				dactiveButtons[i].SetActive(false);
				activeButtons[i].SetActive(true);
				for (int k = 0; k < contents[i].Count; k++)
					contents[i][k].gameObject.SetActive(true);
			}
			RegistUIButton(dactiveButtons[i], (sender) => {
				// 设置到当前状态
				int index = dactiveButtons.IndexOf(sender);
				if (contents[index] == null) {
					return;
				}
				else {
					// 初始化状态
					for (int j = 0; j < activeButtons.Count; j++) {
						if (contents[j] != null) {
							activeButtons[j].SetActive(false);
							dactiveButtons[j].SetActive(true);
							for (int k = 0; k < contents[j].Count; k++)
								contents[j][k].gameObject.SetActive(false);
						}
					}
					dactiveButtons[index].SetActive(false);
					activeButtons[index].SetActive(true);
					for (int k = 0; k < contents[index].Count; k++)
						contents[index][k].gameObject.SetActive(true);
				}

			});
			RegistUIButton(activeButtons[i], (sender) => {
				// 设置到当前状态
				int index = activeButtons.IndexOf(sender);
				if (contents[index] == null) {
					return;
				}
				else {
					// 初始化状态
					for (int j = 0; j < activeButtons.Count; j++) {
						if (contents[j] != null) {
							activeButtons[j].SetActive(false);
							dactiveButtons[j].SetActive(true);
							for (int k = 0; k < contents[j].Count; k++)
								contents[j][k].gameObject.SetActive(false);
						}
					}
					dactiveButtons[index].SetActive(false);
					activeButtons[index].SetActive(true);
					for (int k = 0; k < contents[index].Count; k++)
						contents[index][k].gameObject.SetActive(true);
				}

			});
		}
	}
	static public void RegistUITapButton(List<GameObject> activeButtons, List<List<Transform>> contents) {
		if (activeButtons.Count != contents.Count) {
			return;
		}
		for (int i = 0; i < activeButtons.Count; i++) {
			if (i == 0) {
				for (int k = 0; k < contents[i].Count; k++)
					contents[i][k].gameObject.SetActive(true);
			}
			else {
				for (int k = 0; k < contents[i].Count; k++)
					contents[i][k].gameObject.SetActive(false);
			}
			
			RegistUIButton(activeButtons[i], (sender) => {
				// 设置到当前状态
				int index = activeButtons.IndexOf(sender);
				if (contents[index] == null) {
					return;
				}
				else {
					// 初始化状态
					for (int j = 0; j < activeButtons.Count; j++) {
						if (contents[j] != null) {
							for (int k = 0; k < contents[j].Count; k++)
								contents[j][k].gameObject.SetActive(false);
						}
					}
					for (int k = 0; k < contents[index].Count; k++)
						contents[index][k].gameObject.SetActive(true);
				}

			});
		}
	}

	/// <summary>
	/// 注册左右切换模型数据显示的按钮控件，具体可以参照PetView的写法
	/// </summary>
	/// <param name="leftButton">向左的按钮</param>
	/// <param name="rightButton">向右的按钮</param>
	/// <param name="datas">显示内容的逻辑数据数组</param>
	/// <param name="action">刷新界面的函数</param>
	public static void RegistUILeftRightButton(GameObject leftButton, GameObject rightButton, IList datas, int curKey, CSharpFun action) {
		// 初始化
		RefreshLeftRight(leftButton, rightButton, action, datas, curKey);

		RegistUIButton(leftButton, (sender) => {
			curKey--;
			curKey = Mathf.Max(0, curKey);
			RefreshLeftRight(leftButton, rightButton, action, datas, curKey);
		});

		RegistUIButton(rightButton, (sender) => {
			curKey++;
			curKey = Mathf.Min(curKey, datas.Count - 1);
			RefreshLeftRight(leftButton, rightButton, action, datas, curKey);
		});

	}

	private static void RefreshLeftRight(GameObject leftButton, GameObject rightButton, CSharpFun action, IList datas, int curKey) {
		leftButton.GetComponent<Collider>().enabled = true;
		leftButton.GetComponent<UISprite>().color = Color.white;
		rightButton.GetComponent<Collider>().enabled = true;
		rightButton.GetComponent<UISprite>().color = Color.white;


		if (curKey <= 0) {
			leftButton.GetComponent<Collider>().enabled = false;
			leftButton.GetComponent<UISprite>().color = Color.gray;
			curKey = 0;
		}
		if (curKey >= datas.Count - 1) {
			rightButton.GetComponent<Collider>().enabled = false;
			rightButton.GetComponent<UISprite>().color = Color.gray;
			curKey = datas.Count - 1;
		}
		if (datas.Count != 0) {
			action(new object[] { datas[curKey] });
		}
		else {
			action(null);
		}

	}
	/// <summary>
	/// Regists the user interface button.
	/// </summary>
	/// <param name="button">Button.</param>
	/// <param name="action">Action.</param>
	private static void RegistUIButton(GameObject button, UIEventListener.VoidDelegate action) {
		UIEventListener listener = UIEventListener.Get(button);
		listener.onClick += (go) => {
			action(go);
		};
	}
}
