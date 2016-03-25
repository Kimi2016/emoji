
using UnityEngine;

/// <summary>
/// Tween the object's rotation.
/// </summary>

public class TweenRotationOne : UITweener
{
	public enum POS{
		X,
		Y,
		Z
	};

	public POS pos;
	public float from;
	public float to;
	
	Transform mTrans;
	
	public Transform cachedTransform { get { if (mTrans == null) mTrans = transform; return mTrans; } }
	
	[System.Obsolete("Use 'value' instead")]
	public float rotation { get { return this.value; } set { this.value = value; } }
	
	/// <summary>
	/// Tween's current value.
	/// </summary>
	
	public float value { 
		get {
			switch ( this.pos ){
			case POS.X:
				return cachedTransform.localRotation.eulerAngles.x;
			case POS.Y:
				return cachedTransform.localRotation.eulerAngles.y;
			case POS.Z:
				return cachedTransform.localRotation.eulerAngles.z;
			}
			return 0;
		} 
		set {
			Vector3 angles = cachedTransform.localRotation.eulerAngles;
			switch ( this.pos ){
			case POS.X:
				angles.x = value;
				break;
			case POS.Y:
				angles.y = value;
				break;
			case POS.Z:
				angles.z = value;
				break;
			}
			cachedTransform.localRotation = Quaternion.Euler( angles );
		} 
	}
	
	/// <summary>
	/// Tween the value.
	/// </summary>
	
	protected override void OnUpdate (float factor, bool isFinished)
	{
		value = Mathf.Lerp(from, to, factor);
	}
	
	/// <summary>
	/// Start the tweening operation.
	/// </summary>
	
	static public TweenRotationOne Begin (GameObject go, float duration, POS pos , float angle)
	{
		TweenRotationOne comp = UITweener.Begin<TweenRotationOne>(go, duration);
		comp.pos = pos;
		comp.from = comp.value;
		comp.to = angle;
		
		if (duration <= 0f)
		{
			comp.Sample(1f, true);
			comp.enabled = false;
		}
		return comp;
	}
	
	[ContextMenu("Set 'From' to current value")]
	public override void SetStartToCurrentValue () { from = value; }
	
	[ContextMenu("Set 'To' to current value")]
	public override void SetEndToCurrentValue () { to = value; }
	
	[ContextMenu("Assume value of 'From'")]
	void SetCurrentValueToStart () { value = from; }
	
	[ContextMenu("Assume value of 'To'")]
	void SetCurrentValueToEnd () { value = to; }
}
