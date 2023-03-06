using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement  ;

[CreateAssetMenu(fileName = "SceneSO", menuName = "ScriptableObject/SceneSO")]
public class SceneSO : ScriptableObject
{
	[SerializeField]
	private GameSceneType _gameSceneType;

	[SerializeField]
	private string _sceneName;

	public string GetSceneName()
	{
		return _sceneName;
	}

	public GameSceneType GetSceneType()
    {
		return _gameSceneType;  
    }

	public enum GameSceneType
	{
		Location, //SceneSelector tool will also load PersistentManagers and Gameplay
		Menu, //SceneSelector tool will also load 

		//Special scenes
		Initialisation,
		PersistentManagers,
		Gameplay,
 
	}
}
