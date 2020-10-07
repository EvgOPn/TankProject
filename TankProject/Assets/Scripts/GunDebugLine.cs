using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunDebugLine : MonoBehaviour
{
	private void Update()
	{
		Debug.DrawRay(transform.position, transform.rotation * Vector3.forward * 15f, Color.red);
	}
}
