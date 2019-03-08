using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerSukapenpen : SingletonMonoBehaviour<GameManagerSukapenpen>
{
	public enum State
	{
		Initial,
		Play,
		Result,

		Length,
    }
    [SerializeField]
    private KeyCode actionKey1;
    [SerializeField]
    private KeyCode actionKey2;
    [SerializeField]
    private string titleSceneName = string.Empty;
    [SerializeField]
	private float timeLimit = 60;
	private float maxStartTime = 4;
	private State currentState = State.Length;
	
	private void Start ()
	{
		currentState = State.Initial;
	}
	
	private void Update ()
	{
		switch (currentState)
		{
			case State.Initial:
				maxStartTime -= Time.deltaTime;
				this.DoInitial(maxStartTime);
				if ((int) maxStartTime < 0)
				{
					currentState = State.Play;
				}
				break;
			
			case State.Play:
				timeLimit -= Time.deltaTime;
				this.DoPlay(timeLimit);
				
				if ((int) timeLimit < 0 || FlowerController.Instance.IsAllWithered())
				{
					currentState = State.Result;

                    timeLimit = 0;
                    PlayUIController.Instance.Close();
                    int one, two = 0;
                    PlayerController.Instance.GetScores(out one, out two);
                    var win = (one < two) ? 2 : 1;
                    ResultUIController.Instance.Open(win);
				}
				break;
			
			case State.Result:
                timeLimit -= Time.deltaTime;
                this.DoResult();

                if (timeLimit <= -3.0f && (Input.GetKeyDown(this.actionKey1) || Input.GetKeyDown(this.actionKey2)))
                {
                    SceneManager.LoadScene(this.titleSceneName);
                }
				break;
		}
	}

	private void DoInitial(float _maxStartTime)
	{
		GameSceneUIManager.Instance.OnUpdate(_maxStartTime);
	}

	private void DoPlay(float _timeLimit)
	{
		GameSceneUIManager.Instance.OnUpdate(_timeLimit);
		PlayerController.Instance.OnUpdate(_timeLimit);
        FlowerController.Instance.OnUpdate();
	}

	private void DoResult()
	{
        ResultUIController.Instance.OnUpdate();
	}
}
