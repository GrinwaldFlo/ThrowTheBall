using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class playerBallScript : MonoBehaviour
{
	public List<GameObject> lstElem = new List<GameObject>();


	// Use this for initialization
	void Start()
	{
		logicScript.OnStateChange += LogicScript_OnStateChange;
	}

	private void LogicScript_OnStateChange()
	{
		if (Gvar.gameState == enGameState.Play)
			lstElem.Clear();
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void OnTriggerEnter(Collider other)
	{
		Debug.Log("Add ball " + other.gameObject.name);
		lstElem.Add(other.gameObject);
		other.gameObject.GetComponent<Highlight>().isSelected = true;
	}

	public void OnTriggerExit(Collider other)
	{
		Debug.Log("Del ball " + other.gameObject.name);
		lstElem.Remove(other.gameObject);
		other.gameObject.GetComponent<Highlight>().isSelected = false;
	}

	internal GameObject getElem()
	{
		if (lstElem.Count > 0)
			return lstElem[0];

		return null;
	}
}
