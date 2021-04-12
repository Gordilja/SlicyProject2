using UnityEngine;

public class Sliced : MonoBehaviour
{
	public GameObject slicedPrefab;
	public GameObject cube;
	public float startForce = 15f;

	Rigidbody rb;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
		rb.AddForce(transform.up * startForce, ForceMode.Impulse);
		if(FindObjectOfType<SaveData>().score < 1)
			FindObjectOfType<SaveData>().score = 1;
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Blade")
		{
			Vector3 direction = (col.transform.position - transform.position).normalized;

			Quaternion rotation = Quaternion.LookRotation(direction);

			GameObject slicedObs = Instantiate(slicedPrefab, transform.position, rotation);
			Destroy(slicedObs, 3f);
			Destroy(cube);
			FindObjectOfType<SaveData>().scoreUp();
		}
	}
}
