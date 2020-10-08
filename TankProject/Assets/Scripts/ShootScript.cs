using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootScript : MonoBehaviour
{
	[SerializeField] private Transform _shellSpawnPoint = null;
	[SerializeField] private GameObject _shellPrefab = null;

	[SerializeField] private float _shellForce = 10f;

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			GameObject spawnedShell = Instantiate(_shellPrefab, _shellSpawnPoint.position, _shellSpawnPoint.rotation);
			spawnedShell.GetComponent<Rigidbody>()?.AddForce(_shellSpawnPoint.forward * _shellForce);
			Destroy(spawnedShell, 6f);
		}
	}
}
