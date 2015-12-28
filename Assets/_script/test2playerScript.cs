using UnityEngine;
using System.Collections;

public class test2playerScript : MonoBehaviour
{
	internal int deviceId;
	internal int player;
	internal int side;

	internal Rigidbody rbody;
	private test2PlayerElemScript elemScript;

	private Vector3 curForce;

	private bool joyActive = false;

	// Use this for initialization
	void Start()
	{
		rbody = GetComponent<Rigidbody>();

		elemScript = GetComponentInChildren<test2PlayerElemScript>();

		MeshRenderer m = GetComponent<MeshRenderer>();
		m.material.color = Random.ColorHSV();
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void FixedUpdate()
	{
		if (joyActive && rbody.velocity.magnitude < Test2Gvar.movePlayerVmax)
			rbody.AddRelativeForce(curForce, ForceMode.Acceleration);
	}

	internal void move(float x1, float y1)
	{
		curForce = new Vector3(x1, 0f, -y1) * Test2Gvar.movePlayerFactor;
		joyActive = true;
	}

	internal void stopMove()
	{
		joyActive = false;
	}

	internal void askThrow(float angle)
	{
		GameObject elem = elemScript.getElem();
		if (elem == null)
			return;

		Vector3 v = new Vector3(1, 1, 0).normalized;
		v = Quaternion.Euler(0, angle, 0) * v;

		elem.GetComponent<Rigidbody>().AddForce(v * Test2Gvar.throwElemFactor, ForceMode.Impulse);
	}
}
