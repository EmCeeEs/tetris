using UnityEngine;

public class SpaceStuff : MonoBehaviour
{
	public GameObject[] randomStuff;
	public Transform[] randomStuffSpawnLocations;
	public Transform[] randomStuffRoationGO;

	float delta;

	void Start()
	{
		SpawnRandomStuff();
	}

	void FixedUpdate()
	{
		delta = Time.deltaTime;
		RotateRandomStuff(delta);
	}

	public void SpawnRandomStuff()
	{
		for (int i = 0; i < randomStuffSpawnLocations.Length; i++)
		{
			Instantiate(randomStuff[Random.Range(0, randomStuff.Length)], randomStuffSpawnLocations[i]);
			randomStuffRoationGO[i].transform.Rotate(Vector3.up, Random.Range(0, 360));
		}
	}

	public void RotateRandomStuff(float delta)
	{
		int direction = 1;
		for (int i = 0; i < randomStuffRoationGO.Length; i++)
		{
			randomStuffRoationGO[i].transform.Rotate(Vector3.up, delta * direction);
			direction *= -1;
		}
	}
}
