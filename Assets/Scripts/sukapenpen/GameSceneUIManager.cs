using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneUIManager : SingletonMonoBehaviour<GameSceneUIManager>
{
	private enum GameUIState
	{
		Set,
		Start,
		Result
	}

	[SerializeField] private Sprite[] startStopCountImages;
	[SerializeField]
	private Sprite startImage;
	[SerializeField]
	private Sprite stopImage;
	
	[SerializeField]
	private float maxStartTime = 4;
	private float countTime;
	private GameUIState gameUIState;
	private Image startCountImage;
	
	private void Start ()
	{
		startCountImage = GameObject.Find("StartStopCounter").GetComponent<Image> ();
		startStopCountImages[0] = startImage;
		this.countTime = 0;
		gameUIState = GameUIState.Set;
	}
	
	public void OnUpdate (float _timeLimit)
	{
		int sec = (int) _timeLimit;
		switch (gameUIState)
		{
			case GameUIState.Set:
				if (sec >= 0)
				{
					startCountImage.sprite = startStopCountImages[sec];
				}
				else
				{
					startCountImage.enabled = false;
					gameUIState = GameUIState.Start;
					startStopCountImages[0] = stopImage;
				}

				break;
			
			case GameUIState.Start:
				Debug.Log("ゲーム中のUI処理" + sec);
				if (sec < 0)
				{
					startCountImage.enabled = false;
					gameUIState = GameUIState.Result;
				}
				else if(sec < 6)
				{
					startCountImage.enabled = true;
					startCountImage.sprite = startStopCountImages[sec];
				}
				break;
			
			case GameUIState.Result:
				Debug.Log("リザルトの処理");
				break;
		}
	}
}
