// Writer：QooChaso

using UnityEngine;

public class PlayerController : SingletonMonoBehaviour<PlayerController>
{
    [SerializeField]
    private GameObject player1;
    [SerializeField]
    private GameObject player2;

    private void Start()
    {
        player1 = Instantiate(player1, new Vector3(3.0f, 0.35f, 0.0f), Quaternion.Euler(0, 0, 0));
        player2 = Instantiate(player2, new Vector3(-3.0f, 0.0f, 0.0f), Quaternion.Euler(0, 0, 0));
    }

    public void OnUpdate(float _timeLimit)
    {
        player1.GetComponent<Player>().OnUpdate();
        player2.GetComponent<Player>().OnUpdate();
    }
}