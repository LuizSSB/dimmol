using UnityEngine;
using System;
using System.Collections.Generic;

public class DoOnMainThread : MonoBehaviour {

//	public static DoOnMainThread GetFirstOne() {
//		var gameObject = GameObject.FindGameObjectWithTag("DoOnMainThread");
//		return gameObject.GetComponent<DoOnMainThread>();
//	}

	private static Queue<Action> sActionQueue = new Queue<Action>();

	public static void AddAction(Action action)
	{
		sActionQueue.Enqueue(action);
	}
	
	// Update is called once per frame
	void Update () {
		while (sActionQueue.Count > 0) {
			sActionQueue.Dequeue().Invoke();
		}
	}
}
