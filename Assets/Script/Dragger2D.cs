﻿using System;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Dragger2D : MonoBehaviour {

	public Camera inputCamera;
	public Collider2D[] inputReceivers = new Collider2D[0];
	public SnapPosition2D[] snapPositions = new SnapPosition2D[0];

	public virtual void OnTouchBegan() {

	}

	public virtual void OnTouchMove() {

	}

	public virtual void OnTouchEnd() {

	}

	public virtual void OnEnable() {
		m_startingPosition = transform.position;
		m_draggers.Add(this);
	}

	public virtual void OnDisable() {
		m_draggers.Remove(this);
	}

	public virtual void Update() {
		if (m_touchHit) {
			if (Input.touchSupported) {
				for (int i = 0; i != Input.touchCount; ++i) {
					Touch touch = Input.GetTouch(i);
					if (touch.fingerId == m_touch.fingerId) {
						switch (touch.phase) {
						case TouchPhase.Canceled:
						case TouchPhase.Ended:
							Release();
							break;
						case TouchPhase.Moved:
							Drag(m_touch.position);
							break;
						}
						break;
					}
				}
			} else if (Input.GetMouseButtonUp(0)) {
				Release();
			} else if (Input.GetMouseButton(0)) {
				Drag(Input.mousePosition);
			}
		} else if (Input.touchSupported) {
			for (int i = 0; i != Input.touchCount; ++i) {
				Touch touch = Input.GetTouch(i);
				if (touch.phase == TouchPhase.Began && (m_touchHit = InputHitReceiver(touch.position))) {
					m_touch = touch;
					Press();
					break;
				}
			}
		} else if (Input.GetMouseButtonDown(0) && (m_touchHit = InputHitReceiver(Input.mousePosition))) {
			Press();
		}
	}

	void Press() {
		if (m_touchSprite) {
			int deltaSortingOrder = 0;
			for (int i = 0; i != m_draggers.Count; ++i) {
				if (m_draggers[i] == this) {
					continue;
				}
				SpriteRenderer[] sprites = m_draggers[i].GetComponentsInChildren<SpriteRenderer>();
				for (int j = 0; j != sprites.Length; ++j) {
					if (sprites[j].sortingLayerID == m_touchSprite.sortingLayerID &&
						sprites[j].sortingOrder > m_touchSprite.sortingOrder) {
						--sprites[j].sortingOrder;
						++deltaSortingOrder;
					}
				}
			}
			m_touchSprite.sortingOrder += deltaSortingOrder;
		}
		m_touchDownPosition = m_touchHit.point;
		if (m_snapPosition) {
			m_snapPosition.currentAnswer = null;
			m_snapPosition = null;
		}
		OnTouchBegan();
	}

	void Drag(Vector2 screenPoint) {
		float enterDist;
		Ray inputRay = inputCamera.ScreenPointToRay(screenPoint);
		if (new Plane(Vector3.forward, m_touchHit.collider.transform.position).Raycast(inputRay, out enterDist)) {
			Vector2 enterPoint = inputRay.GetPoint(enterDist);
			transform.position += (Vector3)(enterPoint - m_touchDownPosition);
			m_touchDownPosition = enterPoint;
		}
		OnTouchMove();
	}

	void Release() {
		for (int i = 0; i != snapPositions.Length; ++i) {
			if (snapPositions[i].currentAnswer) {
				continue;
			}
			Collider2D[] snapAreas = snapPositions[i].snapAreas;
			for (int j = 0; j != snapAreas.Length; ++j) {
				if (snapAreas[j].OverlapPoint(transform.position)) {
					transform.position = snapAreas[j].transform.position;
					(m_snapPosition = snapPositions[i]).currentAnswer = this;
					break;
				}
			}
		}
		if (!m_snapPosition) {
			transform.position = m_startingPosition;
		}
		m_touchHit = default(RaycastHit2D);
		OnTouchEnd();
	}

	RaycastHit2D InputHitReceiver(Vector2 screenPoint) {
		RaycastHit2D[] hits = Physics2D.GetRayIntersectionAll(inputCamera.ScreenPointToRay(screenPoint));
		if (hits.Length != 0) {
			RaycastHit2D hit = hits[0];
			if (m_touchSprite = hit.collider.GetComponentInParent<SpriteRenderer>()) {
				for (int i = 1; i != hits.Length; ++i) {
					SpriteRenderer sprite = hits[i].collider.GetComponentInParent<SpriteRenderer>();
					if (sprite) {
						if (CompareSpriteSortingOrder(sprite, m_touchSprite) > 0) {
							hit = hits[i];
							m_touchSprite = sprite;
						}
					} else {
						hit = hits[i];
						break;
					}
				}
			}
			for (int i = 0; i != inputReceivers.Length; ++i) {
				if (hit.collider == inputReceivers[i]) {
					return hit;
				}
			}
		}
		return default(RaycastHit2D);
	}

	int CompareSpriteSortingOrder(SpriteRenderer a, SpriteRenderer b) {
		int aVal = -1;
		int bVal = -1;
		SortingLayer[] layers = SortingLayer.layers;
		for (int i = 0; i != layers.Length; ++i) {
			if (layers[i].id == a.sortingLayerID) {
				aVal = layers[i].value;
			}
			if (layers[i].id == b.sortingLayerID) {
				bVal = layers[i].value;
			}
		}
		if (aVal > bVal) {
			return 1;
		}
		if (aVal < bVal) {
			return -1;
		}
		if (a.sortingOrder > b.sortingOrder) {
			return 1;
		}
		if (a.sortingOrder < b.sortingOrder) {
			return -1;
		}
		return 0;
	}

	bool ScreenToTransformPoint(Vector2 screenPoint, out Vector2 transformPoint) {
		float enterDist;
		Ray inputRay = inputCamera.ScreenPointToRay(screenPoint);
		if (new Plane(Vector3.forward, m_touchHit.collider.transform.position).Raycast(inputRay, out enterDist)) {
			transformPoint = inputRay.GetPoint(enterDist);
			return true;
		}
		transformPoint = default(Vector2);
		return false;
	}

	[NonSerialized] RaycastHit2D m_touchHit = default(RaycastHit2D);
	[NonSerialized] SpriteRenderer m_touchSprite;
	[NonSerialized] Touch m_touch;
	[NonSerialized] Vector2 m_touchDownPosition;
	[NonSerialized] SnapPosition2D m_snapPosition;
	[NonSerialized] Vector2 m_startingPosition;

	static List<Dragger2D> m_draggers = new List<Dragger2D>();
}