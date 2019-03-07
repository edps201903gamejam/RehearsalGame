// Writer：QooChaso

using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private GameObject player1;
    [SerializeField]
    private GameObject player2;

    void Start()
    {
        var a = Instantiate(player1, new Vector3(3.0f, 0.35f, 0.0f), Quaternion.Euler(0, 0, 0));
        var b = Instantiate(player2, new Vector3(-3.0f, 0.0f, 0.0f), Quaternion.Euler(0, 0, 0));
    }

}