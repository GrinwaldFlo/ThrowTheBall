using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

public class test2dispenserScript : MonoBehaviour
{
	public GameObject goElem;
	public GameObject prefabElem;
	public float dropTimeMs = 5;
	public bool dropEnabled = false;

	float nextDrop = 0;

	// Use this for initialization
	void Start()
	{
		Test2logic.OnInit += Test2logic_OnInit;
	}

	private void Test2logic_OnInit()
	{
		for (int i = 0; i < goElem.transform.childCount; i++)
		{
			Destroy(goElem.transform.GetChild(i).gameObject);
		}
	}

	// Update is called once per frame
	void Update()
	{
		if(Test2Gvar.started)
		{
			if(Time.time > nextDrop)
			{
				GameObject newPlayer = Instantiate(prefabElem);
				newPlayer.transform.SetParent(goElem.transform);

				Collider col = this.GetComponent<Collider>();
				newPlayer.transform.localPosition = new Vector3(Random.Range(col.bounds.min.x, col.bounds.max.x), this.transform.localPosition.y, Random.Range(col.bounds.min.z, col.bounds.max.z));

				//newPlayer.transform.localPosition = this.transform.position;
				nextDrop = Time.time + dropTimeMs;
			}
		}
	}


}
