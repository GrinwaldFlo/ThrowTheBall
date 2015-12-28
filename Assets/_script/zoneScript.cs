using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class zoneScript : MonoBehaviour
{
	public int counter = 0;
	public Text scoreTxt;
	public RectTransform jauge;
	public float jaugeFactor = 200f;
	public int zoneId;

	// Use this for initialization
	void Start()
	{
		logicScript.OnStateChange += LogicScript_OnStateChange;
	}

	private void LogicScript_OnStateChange()
	{
		if(Gvar.gameState == enGameState.Play)
		{
			counter = 0;
			updateScore();
		}
	}


	// Update is called once per frame
	void Update()
	{
	}

	public void OnTriggerEnter(Collider other)
	{
		counter++;
		updateScore();
	}

	public void OnTriggerExit(Collider other)
	{
		counter--;
		updateScore();
	}

	private void updateScore()
	{
		Gvar.score[zoneId] = counter;
		scoreTxt.text = counter.ToString();

		float jaugeSize = 1f - (float)counter / (float)Gvar.scoreMax;

		jauge.sizeDelta = new Vector2(jaugeSize * jaugeFactor, jauge.sizeDelta.y);
	}
}
