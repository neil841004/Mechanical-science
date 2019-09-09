using System;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public sealed class SnapPosition2D : MonoBehaviour {

	public Collider2D[] snapAreas = new Collider2D[0];
	public Dragger2D[] correntAnswers = new Dragger2D[0];
	public bool multipleAnswer = false;
	public bool correct {
		get {
			if (multipleAnswer) {
				for (int i = 0; i != correntAnswers.Length; ++i) {
					bool wrong = true;
					for (int j = 0; j != currentAnswers.Count; ++j) {
						if (correntAnswers[i] == currentAnswers[j]) {
							wrong = false;
							break;
						}
					}
					if (wrong) {
						return false;
					}
				}
				return true;
			}
			for (int i = 0; i != correntAnswers.Length; ++i) {
				if (correntAnswers[i] == currentAnswer) {
					return true;
				}
			}
			return false;
		}
	}
	[NonSerialized] public Dragger2D currentAnswer = null;
	[NonSerialized] public List<Dragger2D> currentAnswers = new List<Dragger2D>();
}