using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

public class dispenserScript : MonoBehaviour
{
	public GameObject goBall;
	public GameObject prefabBall;
	public float dropTimeMaxMs = 0.3f;
	public float dropTimeMinMs = 0.2f;
	private int maxBall = 400;

	float nextDrop = 0;

	// Use this for initialization
	void Start()
	{
		logicScript.OnStateChange += LogicScript_OnStateChange;
	}

	private void LogicScript_OnStateChange()
	{
		//for (int i = 0; i < goBall.transform.childCount; i++)
		//{
		//	Destroy(goBall.transform.GetChild(i).gameObject);
		//}
	}


	// Update is called once per frame
	void Update()
	{
		if(Gvar.gameState == enGameState.Play || Gvar.gameState == enGameState.Intro)
		{
			if(Time.time > nextDrop && goBall.transform.childCount < maxBall)
			{
				GameObject newPlayer = Instantiate(prefabBall);
				newPlayer.transform.SetParent(goBall.transform);

				Collider col = this.GetComponent<Collider>();
				newPlayer.transform.localPosition = new Vector3(Random.Range(col.bounds.min.x, col.bounds.max.x), this.transform.localPosition.y, Random.Range(col.bounds.min.z, col.bounds.max.z));

				//newPlayer.transform.localPosition = this.transform.position;
				if(Gvar.gameState == enGameState.Play)
					nextDrop = Time.time + Mathf.Lerp(dropTimeMinMs, dropTimeMaxMs, goBall.transform.childCount / Gvar.scoreMax);
				else
					nextDrop = Time.time + 0.2f;
			}
		}
	}
}
