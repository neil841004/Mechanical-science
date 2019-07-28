using UnityEngine;

[DisallowMultipleComponent]
public sealed class SnapPositionRetriever : MonoBehaviour {

	public Dragger2D dragger;
	public SnapPosition2D[] snapPositions = new SnapPosition2D[0];
	public RockEvent rockEvent;

	void Update() {
		for (int i = 0; i != snapPositions.Length; ++i) {
			if (snapPositions[i].currentAnswer == dragger) {
				rockEvent.pivotNumber = i + 1;
				break;
			}
		}
	}
}