using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
	private static volatile T instance;

	public static T Instance
	{
		get
		{
			if (instance != null)
			{
				return instance;
			}
			
			instance = FindObjectOfType<T>() as T;
			
			if (instance == null)
			{
				Debug.Log("どこにもないよ");
				Debug.Break();
			}
			
			return instance;
		}
		protected set { instance = value; }
	}

	private void Awake()
	{
		if (this != Instance)
		{
			Destroy(this);
		}
	}
}
