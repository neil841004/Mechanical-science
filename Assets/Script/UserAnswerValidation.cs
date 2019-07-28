using UnityEngine;
using UnityEngine.Events;

[DisallowMultipleComponent]
public sealed class UserAnswerValidation : MonoBehaviour {

	public SnapPosition2D[] snapPositions = new SnapPosition2D[0];
	public UnityEvent onComplete = new UnityEvent();
	public UnityEvent onIncomplete = new UnityEvent();
	public UnityEvent onValidationSuccess = new UnityEvent();
	public UnityEvent onValidationFailure = new UnityEvent();

	public void ValidateUserAnswers() {
		if (correct) {
			if (onValidationSuccess != null) {
				onValidationSuccess.Invoke();
				GameObject.FindWithTag("SE").SendMessage("CorrectSE");
			}
		} else {
			if (onValidationFailure != null) {
				onValidationFailure.Invoke();
				GameObject.FindWithTag("SE").SendMessage("ErrorSE");
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

	public bool complete {
		get {
			for (int i = 0; i != snapPositions.Length; ++i) {
				for (int j = 0; j != snapPositions[i].correntAnswers.Length; ++j) {
					if (!snapPositions[i].currentAnswer) {
						return false;
					}
				}
			}
			return true;
		}
	}

	void Update() {
		bool complete = this.complete;
		if (complete == m_complete) {
			return;
		}
		if (m_complete = complete) {
			onComplete.Invoke();
		} else {
			onIncomplete.Invoke();
		}
	}
	bool m_complete = false;
}