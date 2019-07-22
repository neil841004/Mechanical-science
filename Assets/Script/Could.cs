using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Could : MonoBehaviour {
	public enum Direction
	{
		right,
		left
	}
	public Direction direction; //雲往哪個方向飄
	public float speed = 0.05f; //雲飄動的速度
	Vector3 position; //當前位置
	// Use this for initialization
	void Start () {
		position = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(direction == Direction.right){
			position.x += speed;
			if(position.x>=10){
				position.x = -10;
			}
			this.transform.position = position;
		}
		if(direction == Direction.left){
			position.x -= speed;
			if(position.x <=-10){
				position.x = 10;
			}
			this.transform.position = position;
		}
		
	}
}
