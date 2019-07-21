using UnityEngine;

[DisallowMultipleComponent]
public sealed class UserAnswerValidation : MonoBehaviour {

	public SnapPosition2D[] snapPositions = new SnapPosition2D[0];

	public bool correct {
		get {
			for (int i = 0; i != snapPositions.Length; ++i) {
				bool wrong = true;
				for (int j = 0; j != snapPositions[i].correntAnswers.Length; ++j) {
					if (snapPositions[i].currentAnswer == snapPositions[i].correntAnswers[j]) {
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
	}
}