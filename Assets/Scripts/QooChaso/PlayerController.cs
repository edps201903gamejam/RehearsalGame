// Writer：QooChaso

using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private AudioClip mizuyaru;
    [SerializeField]
    private AudioClip mizukumu;
    private AudioSource audioSource;

    public WateringCan hoge;

    void Start()
    {
        hoge = new WateringCan();
        audioSource = gameObject.GetComponent<AudioSource>();
    }


    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "plant")
        {
            if (Input.GetKeyDown(KeyCode.Space) && hoge.isWaterd)
            {
                hoge.RemovedWater();
                other.GetComponent<Flower>().MinusWhtherPercentage();
                Debug.Log("水をあげた");

                audioSource.clip = mizuyaru;
                audioSource.Play();

            }
        }

        if (other.gameObject.tag == "water")
        {
            if (Input.GetKeyDown(KeyCode.Space) && !hoge.isWaterd)
            {
                hoge.ToMaxWaterGase();
                Debug.Log("水をくんだ");

                audioSource.clip = mizukumu;
                audioSource.Play();
            }
        }
    }

}