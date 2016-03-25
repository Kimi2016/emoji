using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputUtility
{
	/// <summary>
	/// 使用的时候parents一定要用成员变量，不要临时声明,造成无所谓的GC者，拖出去爆菊1个小时
	/// </summary>
	/// <param name="button">鼠标的左键右键</param>
	/// <param name="parents">在NGUI层上需要关闭点击容器的父节点，比如一个grid里面要屏蔽掉
	/// 所有item的点击的话，请使用把grid的transform设置在parent中
	/// </param>
	/// <returns></returns>
	public static bool GetMouseButtonDown(int button, List<Transform> parents) {
		if (Input.GetMouseButtonDown(button)) {
			if (!UICamera.isOverUI) return true;
			if (Input.touchCount > 0 && UICamera.Raycast(Input.GetTouch(0).position)) return false;

			Ray ray = UICamera.mainCamera.ScreenPointToRay(Input.mousePosition);
			RaycastHit[] hits = Physics.RaycastAll(ray, Mathf.Infinity, 1 << GameConst.Layer.UI);
			for (int i = 0; i < hits.Length; i++) {
				if (CheckHasParent(hits[i].collider.transform, parents)) return false;
			}
			return true;
		}

		return false;
	}
	public static bool CheckHitsHasParent(List<Transform> parents) {
		return CheckHitsHasParent(parents, (1 << GameConst.Layer.UI) | ~(1 << GameConst.Layer.UI));
	}
	public static bool CheckHitsHasParent(List<Transform> parents, int layer) {
		bool ret = false;
		Ray ray = UICamera.mainCamera.ScreenPointToRay(Input.mousePosition);
		RaycastHit[] hits = Physics.RaycastAll(ray, Mathf.Infinity, layer);
		for (int i = 0; i < hits.Length; i++) {
			if (InputUtility.CheckHasParent(hits[i].collider.transform, parents)) {
				ret = true;
				break;
			}
		}
		return ret;
	}
	public static bool CheckHasParent(Transform current, List<Transform> parents) {
		if (current == null) {
			return false;
		}
		for (int i = 0; i < parents.Count; i++) {
			if (current == parents[i]) {
				return true;
			}
		}
		return CheckHasParent(current.parent, parents);
	}
	public static bool GetMouseButtonDown(int button) {
		if (UICamera.isOverUI) return false;
		if (Input.touchCount > 0 && UICamera.Raycast(Input.GetTouch(0).position)) return false;
		return Input.GetMouseButtonDown(button);
	}

	public static bool GetMouseButtonUp(int button) {
		if (UICamera.isOverUI)	return false;
		if (Input.touchCount > 0 && UICamera.Raycast(Input.GetTouch(0).position))	return false;
		return Input.GetMouseButtonUp(button);
	}


	public static  bool IsPointInsideCamera(Vector3 kPoint, Camera kCam)
	{
		Plane[] planes = GeometryUtility.CalculateFrustumPlanes(kCam);
		
		bool bRet = true;
		
		for (int i = 0; i < planes.Length; ++i) 
		{
			bRet &= planes[i].GetSide(kPoint);
		}
		
		return bRet;
	}
}

