// Writer：QooChaso

using UnityEngine;

public class PlayerController : SingletonMonoBehaviour<PlayerController>
{
    [SerializeField]
    private Player player1 = null;
    [SerializeField]
    private Player player2 = null;

    public void GetScores(out int _player1Score, out int _player2Score)
    {
        _player1Score = this.player1.Score;
        _player2Score = this.player2.Score;
    }

    private void Start()
    {
        FyUtility.NullCheck(this.player1, this.gameObject);
        FyUtility.NullCheck(this.player2, this.gameObject);
    }

    public void OnUpdate(float _timeLimit)
    {
        this.player1.OnUpdate();
        this.player2.OnUpdate();
    }
}