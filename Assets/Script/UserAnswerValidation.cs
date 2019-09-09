using System;
using UnityEngine;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

[DisallowMultipleComponent]
public sealed class UserAnswerValidation : MonoBehaviour {

#if UNITY_EDITOR
	[UnityEditor.CustomEditor(typeof(UserAnswerValidation)), CanEditMultipleObjects]
	class CustomEditor : Editor {

		SerializedProperty validationModeProperty;
		SerializedProperty snapPositionsProperty;
		SerializedProperty draggersProperty;
		SerializedProperty onCompleteProperty;
		SerializedProperty onIncompleteProperty;
		SerializedProperty onValidationSuccessProperty;
		SerializedProperty onValidationFailureProperty;

		void OnEnable() {
			validationModeProperty = serializedObject.FindProperty("validationMode");
			snapPositionsProperty = serializedObject.FindProperty("snapPositions");
			draggersProperty = serializedObject.FindProperty("draggers");
			onCompleteProperty = serializedObject.FindProperty("onComplete");
			onIncompleteProperty = serializedObject.FindProperty("onIncomplete");
			onValidationSuccessProperty = serializedObject.FindProperty("onValidationSuccess");
			onValidationFailureProperty = serializedObject.FindProperty("onValidationFailure");
		}

		public override void OnInspectorGUI() {
			EditorGUI.BeginDisabledGroup(true);
			EditorGUILayout.ObjectField("Script", MonoScript.FromMonoBehaviour(target as MonoBehaviour), typeof(MonoScript), false);
			EditorGUI.EndDisabledGroup();
			EditorGUILayout.PropertyField(validationModeProperty);
			bool validationModePropertySingleValue = !validationModeProperty.hasMultipleDifferentValues;
			ValidationMode validationMode = (ValidationMode)validationModeProperty.enumValueIndex;
			++EditorGUI.indentLevel;
			EditorGUI.BeginDisabledGroup(validationModePropertySingleValue && validationMode != ValidationMode.SnapPosition);
			EditorGUILayout.PropertyField(snapPositionsProperty, true);
			EditorGUI.EndDisabledGroup();
			EditorGUI.BeginDisabledGroup(validationModePropertySingleValue && validationMode != ValidationMode.Dragger);
			EditorGUILayout.PropertyField(draggersProperty, true);
			EditorGUI.EndDisabledGroup();
			--EditorGUI.indentLevel;
			EditorGUILayout.PropertyField(onCompleteProperty);
			EditorGUILayout.PropertyField(onIncompleteProperty);
			EditorGUILayout.PropertyField(onValidationSuccessProperty);
			EditorGUILayout.PropertyField(onValidationFailureProperty);
			serializedObject.ApplyModifiedProperties();
		}
	}
#endif

	public enum ValidationMode {
		SnapPosition, Dragger
	}
	public ValidationMode validationMode;
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
			switch (validationMode) {
			case ValidationMode.SnapPosition:
				for (int i = 0; i != snapPositions.Length; ++i) {
					if (!snapPositions[i].correct) {
						return false;
					}
				}
				return true;
			case ValidationMode.Dragger:
				for (int i = 0; i != draggers.Length; ++i) {
					if (!draggers[i].snapPosition || !draggers[i].snapPosition.correct) {
						return false;
					}
				}
				return true;
			}
			throw new IndexOutOfRangeException();
		}
	}

	public bool complete {
		get {
			switch (validationMode) {
			case ValidationMode.SnapPosition:
				for (int i = 0; i != snapPositions.Length; ++i) {
					if (!snapPositions[i].currentAnswer) {
						return false;
					}
				}
				return true;
			case ValidationMode.Dragger:
				for (int i = 0; i != draggers.Length; ++i) {
					if (!draggers[i].snapPosition) {
						return false;
					}
				}
				return true;
			}
			throw new IndexOutOfRangeException();
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