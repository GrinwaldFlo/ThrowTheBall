using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class playerBallScript : MonoBehaviour
{
	private List<GameObject> lstElem = new List<GameObject>();


	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	public void OnTriggerEnter(Collider other)
	{
		lstElem.Add(other.gameObject);
		other.gameObject.GetComponent<Highlight>().isSelected = true;
	}

	public void OnTriggerExit(Collider other)
	{
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
