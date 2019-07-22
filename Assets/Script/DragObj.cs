using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObj : MonoBehaviour {
	public int objAmount = 0;
	
	public void ResetDragObj(){
		for (int i = 0; i < objAmount; i++)
		{
			this.transform.GetChild(i).GetComponent<Dragger2D>().ResetPosition();
		}
	}
}
