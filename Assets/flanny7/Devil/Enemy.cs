using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Enemy : MonoBehaviour
{
    private const string PLAYER_TAG = "player";
    private static readonly float FLOAT_MAX = float.MaxValue;

    // serialize
    [SerializeField]
    private float moveSpeed = 1.0f;
    [SerializeField]
    private float moveSpeedEaseOffset = 0.25f;
    [SerializeField]
    private float survivalTime = 1.0f;

    // private member
    private float elapsedTime = 0;
    private float deathTime = FLOAT_MAX;

    // cache
    private Transform transform_ = null;
    private float deltaTime = 0;
    
    private bool deathFlg { get { return (deathTime < this.elapsedTime) || (this.survivalTime <= this.elapsedTime); } }

    private void Start ()
    {
        // init member
        this.elapsedTime = 0;
        this.deathTime = FLOAT_MAX;

        // cache
        this.transform_ = this.transform;
	}
	
	private void Update ()
    {
        this.deltaTime = Time.deltaTime;
        this.elapsedTime += this.deltaTime;

        this.Move();
        
        if (this.deathFlg) { this.Die(); }
	}

    private void OnTriggerEnter(Collider _other)
    {
        var target = _other;

        if (_other.tag == PLAYER_TAG)
        {
            this.deathTime = this.elapsedTime;
        }
    }

    private void Move()
    {
        this.transform_.position += this.transform_.forward * this.moveSpeed * this.deltaTime;
    }

    private void Die()
    {
        Destroy(this.gameObject);
    }
}
