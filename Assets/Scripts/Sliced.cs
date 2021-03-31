using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sliced : MonoBehaviour
{
	public GameObject slicedPrefab;
	public float startForce = 15f;

	Rigidbody rb;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
		rb.AddForce(transform.up * startForce, ForceMode.Impulse);
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Blade")
		{
			Vector3 direction = (col.transform.position - transform.position).normalized;

			Quaternion rotation = Quaternion.LookRotation(direction);

			GameObject slicedObs = Instantiate(slicedPrefab, transform.position, rotation);
			Destroy(slicedObs, 3f);
			Destroy(gameObject);
			FindObjectOfType<GameManager>().highscore();
		}
		/*
		else if (col.tag == "Handle") 
		{
			FindObjectOfType<PlayerControl>().hit();
		}
		*/
	}
}
