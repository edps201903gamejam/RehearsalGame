using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowRiver : MonoBehaviour
{
	private float riverSpeed = -0.05f;
	
	private void Update ()
	{
		transform.Translate (0, riverSpeed, 0);
		if (transform.position.z < -6.5f)
		{
			transform.position = new Vector3 (0, 0, 0);
		}
	}
}
