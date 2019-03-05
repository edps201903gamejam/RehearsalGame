using UnityEngine;

public sealed class Timer : MonoBehaviour
{
    private float elapsedTime = 0.0f;
    public float ElapsedTime
    {
        get { return this.elapsedTime; }
        private set { this.elapsedTime = value; }
    }

    private void Start()
    {
        this.ElapsedTime = 0.0f;
    }

    private void Update()
    {
        this.ElapsedTime += Time.deltaTime;
    }
}