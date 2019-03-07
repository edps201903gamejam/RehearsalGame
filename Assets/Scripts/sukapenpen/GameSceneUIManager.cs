using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneUIManager : SingletonMonoBehaviour<GameSceneUIManager>
{
	private enum GameUIState
	{
		Set,
		Start
	}

	[SerializeField]
	private Sprite[] startCountImages;
	
	[SerializeField]
	private float maxStartTime = 4;
	private float countTime;
	private GameUIState gameUIState;
	private Image startCountImage;
	
	private void Start ()
	{
		startCountImage = GameObject.Find("StartCounter").GetComponent<Image> ();
		this.countTime = 0;
		gameUIState = GameUIState.Set;
	}
	
	public void OnUpdate (float _timeLimit)
	{
		int startSec = (int) _timeLimit;
		switch (gameUIState)
		{
			case GameUIState.Set:
				if (startSec >= 0)
				{
					startCountImage.sprite = startCountImages[startSec];
				}
				else
				{
					startCountImage.enabled = false;
					gameUIState = GameUIState.Start;
				}

				break;
			
			case GameUIState.Start:
				Debug.Log("ゲーム中のUI処理");
				break;
		}
	}
}
