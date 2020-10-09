using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class ShellExploseScript : MonoBehaviour
{
	[SerializeField] private GameObject _exploseEffect = null;
	[SerializeField] private Transform _shellTransform = null;

	private float _timeToDestroyPrefabs = 5f;
	private float _exploseImpulsePower = 4f;
	private float _exploseImpulseRadius = 2f;
	private float _exploseImpulseUpForce = 1f;

	private void OnCollisionEnter(Collision collision)
	{
		SpawnExploseEffect(_shellTransform.position);
		SpawnExploseImpulse(_shellTransform.position);
		DestroyShell();
	}

	private void SpawnExploseEffect(Vector3 position)
	{
		GameObject spawnedEffect = Instantiate(_exploseEffect, position, Quaternion.identity);
		Destroy(spawnedEffect, _timeToDestroyPrefabs);
	}

	private void SpawnExploseImpulse(Vector3 position)
	{
		Collider[] collidersForImpulse = Physics.OverlapSphere(position, _exploseImpulseRadius);
		foreach (Collider hitCollider in collidersForImpulse)
		{
			Rigidbody hitRigidbody = hitCollider.GetComponent<Rigidbody>();
			if (hitRigidbody != null)
			{
				hitRigidbody.AddExplosionForce(_exploseImpulsePower, position, _exploseImpulseRadius, _exploseImpulseUpForce, ForceMode.Impulse);
			}
		}
	}

	private void DestroyShell()
	{
		Destroy(_shellTransform.gameObject, 0.05f);
	}
}
