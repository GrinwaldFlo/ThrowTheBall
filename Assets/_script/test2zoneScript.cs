using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class test2zoneScript : MonoBehaviour
{
	public int counter = 0;
	public Text scoreTxt;

	// Use this for initialization
	void Start()
	{
		Test2logic.OnInit += Test2logic_OnInit;
	}

	private void Test2logic_OnInit()
	{
		counter = 0;
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void OnTriggerEnter(Collider other)
	{
		counter++;
		scoreTxt.text = counter.ToString();
	}

	public void OnTriggerExit(Collider other)
	{
		counter--;
		scoreTxt.text = counter.ToString();
	}
}
