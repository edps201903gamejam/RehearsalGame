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

}