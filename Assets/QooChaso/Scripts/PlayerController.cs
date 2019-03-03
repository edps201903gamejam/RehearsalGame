using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public WateringCan hoge;

    void Start(){ hoge = new WateringCan(); }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "plant")
        {
            if (Input.GetKeyDown(KeyCode.Space) && hoge.isWaterd)
            {
                hoge.RemovedWater();
                other.GetComponent<Flower>().minusWhtherPercentage();
                Debug.Log("水をあげた");
            }
        }

        if (other.gameObject.tag == "water")
        {
            if (Input.GetKeyDown(KeyCode.Space) && !hoge.isWaterd)
            {
                hoge.ToMaxWaterGase();
                Debug.Log("水をくんだ");
            }
        }
    }

}