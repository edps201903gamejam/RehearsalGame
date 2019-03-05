using UnityEngine;

public class DevilSpawnTester : MonoBehaviour
{
    [SerializeField]
    private DevilSpawner spawner = null;
    [SerializeField]
    private float intervalTime = 1.0f;

    private float elapsedTime = 0.0f;

	private void Start ()
    {
        FyUtility.NullCheck(this.spawner, this.gameObject);
        this.elapsedTime = 0.0f;
	}
	
	private void Update ()
    {
        this.elapsedTime += Time.deltaTime;

        if (this.intervalTime <= this.elapsedTime)
        {
            this.elapsedTime = 0.0f;
            this.spawner.Spawn();
        }
	}
}
