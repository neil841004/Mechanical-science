using UnityEngine;
using UnityEngine.Events;

[DisallowMultipleComponent]
public sealed class UserAnswerValidation : MonoBehaviour {

	public SnapPosition2D[] snapPositions = new SnapPosition2D[0];
	public Dragger2D[] draggers = new Dragger2D[0];
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
			if (snapPositions != null && snapPositions.Length != 0) {
				for (int i = 0; i != snapPositions.Length; ++i) {
					if (!snapPositions[i].correct) {
						return false;
					}
				}
			}
			if (draggers != null && draggers.Length != 0) {
				for (int i = 0; i != draggers.Length; ++i) {
					if (!draggers[i].snapPosition || !draggers[i].snapPosition.correct) {
						return false;
					}
				}
			}
			return true;
		}
	}

	public bool complete {
		get {
			if (snapPositions != null && snapPositions.Length != 0) {
				for (int i = 0; i != snapPositions.Length; ++i) {
					if (!snapPositions[i].multipleAnswer && !snapPositions[i].currentAnswer) {
						return false;
					}
				}
			}
			if (draggers != null && draggers.Length != 0) {
				for (int i = 0; i != draggers.Length; ++i) {
					if (!draggers[i].snapPosition) {
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