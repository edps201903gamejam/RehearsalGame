using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 20;

    private Rigidbody rb;
    private WateringCan zyouro;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        zyouro = new WateringCan();

    }

    void Update()
    {
        moveCOntrol();

    }

    private void moveCOntrol(){
        // カーソルキーの入力を取得
        var moveHorizontal = Input.GetAxis("Horizontal");
        var moveVertical = Input.GetAxis("Vertical");

        // カーソルキーの入力に合わせて移動方向を設定
        var movement = new Vector3(moveHorizontal, 0, moveVertical);

        // Ridigbody に力を与えて玉を動かす
        rb.AddForce(movement * speed);
    }

    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == "plant")
        {
            Destroy(other.gameObject);
        }
    }
}