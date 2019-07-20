using System;
using UnityEngine;

[DisallowMultipleComponent]
public sealed class SnapPosition2D : MonoBehaviour {

	public Collider2D[] snapAreas = new Collider2D[0];
	public Dragger2D[] correntAnswers = new Dragger2D[0];
	[NonSerialized] public Dragger2D currentAnswer = null;
}