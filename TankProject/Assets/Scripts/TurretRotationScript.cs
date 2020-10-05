using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretRotationScript : MonoBehaviour
{
	[SerializeField] private Transform _cameraPivotTransform = null;
	[SerializeField] private Transform _turretPivotTransform = null;

	[SerializeField] private float _rotationDampening = 10f;

	private void Update()
	{
		Quaternion targetRotation = _cameraPivotTransform.rotation;
		targetRotation.x = 0f;
		targetRotation.z = 0f;
		_turretPivotTransform.rotation = Quaternion.Lerp(_turretPivotTransform.rotation, targetRotation, Time.deltaTime * _rotationDampening);
	}
}
