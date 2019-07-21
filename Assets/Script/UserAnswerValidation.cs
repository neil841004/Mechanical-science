using UnityEngine;
using UnityEngine.Events;

[DisallowMultipleComponent]
public sealed class UserAnswerValidation : MonoBehaviour {

	public SnapPosition2D[] snapPositions = new SnapPosition2D[0];
	public UnityEvent onValidationSuccess = new UnityEvent();
	public UnityEvent onValidationFailure = new UnityEvent();

	public void ValidateUserAnswers() {
		if (correct) {
			if (onValidationSuccess != null) {
				onValidationSuccess.Invoke();
			}
		} else {
			if (onValidationFailure != null) {
				onValidationFailure.Invoke();
			}
		}
	}

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