using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

public class Test2logic : MonoBehaviour
{
	public const int nbPlayerMax = 20;
	public delegate void InitAction();
	public static event InitAction OnInit;

	public GameObject goPlayer;

	public GameObject prefabPlayer;
	public float movePlayerFactor = 2f;
	public float movePlayerVmax = 1f;
	public float throwElemFactor = 10f;
	public Text[] playerTxt;

	public GameObject[] zone;

	private test2playerScript[] lstPlayer = new test2playerScript[nbPlayerMax];

	public int[] nbPlayer = new int[2];

	void Awake()
	{
		init();
		AirConsole.instance.onConnect += Instance_onConnect;
		AirConsole.instance.onCustomDeviceStateChange += Instance_onCustomDeviceStateChange;
		AirConsole.instance.onDeviceStateChange += Instance_onDeviceStateChange;
		AirConsole.instance.onDisconnect += Instance_onDisconnect;
		AirConsole.instance.onMessage += Instance_onMessage;
		AirConsole.instance.onReady += Instance_onReady;
	}

	private void Instance_onReady(string code)
	{
		Debug.Log("Ready " + code);
		init();
		Test2Gvar.started = true;
	}

	private void init()
	{
		if (OnInit != null)
			OnInit();

		for (int i = 0; i < goPlayer.transform.childCount ; i++)
		{
			Destroy(goPlayer.transform.GetChild(i).gameObject);
		}
		updNbPlayer();
	}

	private void updNbPlayer()
	{
		for (int i = 0; i < nbPlayer.Length; i++)
		{
			nbPlayer[i] = 0;
		}

		for (int i = 0; i < playerTxt.Length; i++)
		{
			playerTxt[i].text = "";
		}

		int side;
		for (int i = 0; i < lstPlayer.Length; i++)
		{
			if(lstPlayer[i] != null)
			{
				side = lstPlayer[i].GetComponent<test2playerScript>().side;
				nbPlayer[side]++;
				playerTxt[side].text += AirConsole.instance.GetNickname(lstPlayer[i].deviceId) + "\r\n";
			}
		}
	}

	private void Instance_onMessage(int from, JToken data)
	{
		int numPlayer = AirConsole.instance.ConvertDeviceIdToPlayerNumber(from);

		if(lstPlayer[numPlayer] == null)
		{
			Debug.Log("Player " + numPlayer + " doesn't exists");
			return;
		}

		if (data["swipeanalog-right"] != null)
		{
			if (data["swipeanalog-right"].Value<bool>("pressed") == true)
			{
				//				float x = data["swipeanalog-right"]["message"].Value<float>("x");
				//				float y = data["swipeanalog-right"]["message"].Value<float>("y");
				//				double a = data["swipeanalog-right"]["message"].Value<double>("angle");
				float d = data["swipeanalog-right"]["message"].Value<float>("degree");

				//Debug.Log("Swipe " + x + " " + y + " " + a + " " + d);
				lstPlayer[numPlayer].askThrow(d);
			}
		}
		else if (data["joystick-left"] != null)
		{
			if (data["joystick-left"].Value<bool>("pressed") == true)
			{
				float x1 = data["joystick-left"]["message"].Value<float>("x");
				float y1 = data["joystick-left"]["message"].Value<float>("y");

				lstPlayer[numPlayer].move(x1, y1);
				//Debug.Log("Joy " + x1 + " " + y1 + " ");
			}
			else
			{
				lstPlayer[numPlayer].stopMove();
			}
		}
		else
		{
			//Debug.Log("Message from " + numPlayer + " " + data.ToString());

		}

		/*
Message from 2 {
  "swipeanalog-left": {
    "pressed": true,
    "message": {
      "x": -0.058722021951470346,
      "y": 0.99827437317499579,
      "angle": 1.6295521495106193,
      "degree": 93
    }
  }
}

		Message from 2 {
  "joystick-left": {
    "pressed": true,
    "message": {
      "x": -0.98657623292578622,
      "y": 0.16330136749569837
    }
  }
}
*/



		//foreach (JToken item in data.Values())
		//{
		//	Debug.Log(item);
		//}






	}

	private void Instance_onDisconnect(int device_id)
	{
		Debug.Log("Disconnected " + device_id);

		deletePlayer(device_id);
	}

	private void deletePlayer(int device_id)
	{
		int numPlayer = AirConsole.instance.ConvertDeviceIdToPlayerNumber(device_id);

		if (lstPlayer[numPlayer] != null)
		{
			Destroy(lstPlayer[numPlayer]);
			lstPlayer[numPlayer] = null;
		}

		updNbPlayer();
	}

	private void Instance_onDeviceStateChange(int device_id, JToken user_data)
	{
		//	Debug.Log("State change " + device_id + " " + user_data.ToString());
	}

	private void Instance_onCustomDeviceStateChange(int device_id, JToken custom_device_data)
	{
		Debug.Log("Custom device state change " + device_id + " " + custom_device_data.ToString());
	}

	private void Instance_onConnect(int device_id)
	{
		AirConsole.instance.SetActivePlayers(nbPlayerMax);

		Debug.Log("Connected " + device_id);

		addPlayer(device_id);
	}

	private void addPlayer(int device_id)
	{
		int numPlayer = AirConsole.instance.ConvertDeviceIdToPlayerNumber(device_id);

		GameObject newPlayer = Instantiate(prefabPlayer);
		newPlayer.transform.SetParent(goPlayer.transform);

		test2playerScript newScript = newPlayer.GetComponent<test2playerScript>();
		newScript.deviceId = device_id;
		newScript.player = numPlayer;

		lstPlayer[numPlayer] = newScript;

		int selZone = 0;
		for (int i = 1; i < nbPlayer.Length; i++)
		{
			if(nbPlayer[i] < nbPlayer[selZone])
			{
				selZone = i;
			}
		}
		newScript.side = selZone;

		Collider col = zone[selZone].GetComponent<Collider>();
		newPlayer.transform.localPosition = new Vector3(Random.Range(col.bounds.min.x, col.bounds.max.x), newPlayer.transform.localPosition.y, Random.Range(col.bounds.min.z, col.bounds.max.z));
		updNbPlayer();
	}


	// Use this for initialization
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		Test2Gvar.movePlayerFactor = movePlayerFactor;
		Test2Gvar.throwElemFactor = throwElemFactor;
		Test2Gvar.movePlayerVmax = movePlayerVmax;
	}
}
