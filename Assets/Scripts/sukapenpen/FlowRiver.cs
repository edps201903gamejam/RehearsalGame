// Writer：sukapenpen

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowRiver : MonoBehaviour
{
    [SerializeField]
    private float riverSpeed = -0.05f;
    private Vector3 initPos;

    private Transform transform_;

    private void Start()
    {
        this.transform_ = this.transform;
        this.initPos = this.transform_.position;
    }

    private void Update ()
	{
        this.transform_.Translate (0, riverSpeed, 0);
		if (this.transform_.position.z < -6.5f)
		{
            this.transform_.position = this.initPos;
		}
	}
}
