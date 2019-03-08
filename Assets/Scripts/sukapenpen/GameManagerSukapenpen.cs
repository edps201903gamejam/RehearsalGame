using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
				
				if ((int) timeLimit < 0)
				{
					currentState = State.Result;
				}
				break;
			
			case State.Result:
				this.DoResult();
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
	}

	private void DoResult()
	{
		Debug.Log("リザルトの処理");
	}
}
