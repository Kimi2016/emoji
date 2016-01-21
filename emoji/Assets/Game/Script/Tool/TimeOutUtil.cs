using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class TimeOutUtil:MonoBehaviour
{
	private static TimeOutUtil _instance = null;
	public static TimeOutUtil getInstance ()
	{
		if (_instance == null) {
			_instance = (TimeOutUtil)FindObjectOfType(typeof(TimeOutUtil));
		}
		return _instance;
	}
	void Awake() {
		_keyBuffers = new List<int>();
	}

	#region scheduler
	// ��ʱ������
	private int _uniqueKey = 0;
	private class SchedulerCSharpFun {
		public Action fun;
		public float firstTime;
		public float deltaTime;
		public float realTime;
		public float eclipseTime;
		public int callTime;
		public object args;
	}
	private Dictionary<int, SchedulerCSharpFun> _schedulerCSharpFuns = new Dictionary<int, SchedulerCSharpFun>();
	private List<int> _keyBuffers;
	private int GetUniqueKey() {
		return _uniqueKey++;
	}
	/// <summary>
	/// �������Լ��ñհ�����class�ĳ�Ա����
	/// �ǵ�Ҫ�÷���ֵ��UnSchedulerCSFun��ȡ�������ʱ��
	/// </summary>
	/// <param name="fun">��ʱִ�еĺ���</param>
	/// <param name="fistTime">��һ��ִ�к�����ʱ��</param>
	/// <param name="deltaTime">�����ִ��һ�κ���</param>
	/// <returns></returns>
	public int SchedulerCSFun(Action fun, float fistTime, float deltaTime, object args) {
		int key = -1;

		SchedulerCSharpFun csFun = new SchedulerCSharpFun();
		csFun.fun = fun;
		csFun.firstTime = fistTime;
		csFun.deltaTime = deltaTime;
		csFun.realTime = 0;
		csFun.callTime = 0;
		csFun.eclipseTime = 0;
		csFun.args = args;
		key = GetUniqueKey();
		_schedulerCSharpFuns.Add(key, csFun);
		_keyBuffers = new List<int>(_schedulerCSharpFuns.Keys);
		return key;
	}
	public int SchedulerCSFun(Action fun, float fistTime, float deltaTime) {
		return SchedulerCSFun(fun, fistTime, deltaTime, null);
	}
	// ��������߳�ִ�е�ʱ��
	public float UnSchedulerCSFun(int key) {
		float result = 0f;

		if (_schedulerCSharpFuns.ContainsKey(key)) {
			result = _schedulerCSharpFuns[key].eclipseTime;
			_schedulerCSharpFuns.Remove(key);
			_keyBuffers.Remove(key);
		}
		return result;
	}
	void Update() {
		if (_schedulerCSharpFuns.Count <= 0) return;

		int key = 0;
		for (int i = 0; i < _keyBuffers.Count; i++) {
			key = _keyBuffers[i];
			SchedulerCSharpFun funObj;
			if (_schedulerCSharpFuns.TryGetValue(key, out funObj)) {
				funObj.realTime = funObj.realTime + Time.deltaTime;
				funObj.eclipseTime = funObj.eclipseTime + Time.deltaTime;
				if (funObj.realTime >= funObj.firstTime && funObj.callTime == 0) {
					funObj.fun();
					funObj.realTime = funObj.realTime - funObj.firstTime;
					funObj.callTime++;
					break;
				}
				if (funObj.realTime >= funObj.deltaTime && funObj.callTime > 0) {
					funObj.fun();
					funObj.realTime = funObj.realTime - funObj.deltaTime;
					funObj.callTime++;
				}
			}
		}
	}
	#endregion
	
	public void setTimeOut (float delay, Action action)
	{
		if(delay <= 0f){
			if (action != null) {
				action();
			}
			return;
		}
		StartCoroutine(timeOutExcute(delay, action));
	}

	public void ExecuteCoroutine(IEnumerator coroutineMethod)
	{
		StartCoroutine(coroutineMethod);
	}

	private IEnumerator timeOutExcute (float delay, Action action)
	{
		yield return new WaitForSeconds (delay);
		if (action != null) {
			action();
		}
	}
}

