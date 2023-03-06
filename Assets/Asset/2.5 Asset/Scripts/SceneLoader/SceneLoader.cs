using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    private SceneSO _gamePlaySceneSO; 

    [Header("Listen to")]
    [SerializeField]
    private SceneEventChannel _loadLocation ;
    [SerializeField]
    private SceneEventChannel _loadMenu ;

    private bool _containGameplayScene;
    private bool _isLoading;

    private string _currentLoadedScene;
    private string _sceneToLoad ;


    private void OnEnable()
    {
        _loadLocation._raiseEvent += LoadLocation;
        _loadMenu._raiseEvent += LoadMenu;
    }

    private void OnDisable()
    {
        _loadLocation._raiseEvent -= LoadLocation;
        _loadMenu._raiseEvent -= LoadMenu;
    }


    //Event Listener 
    private void LoadLocation(SceneSO sceneSO)
    {
        if (_isLoading)
            return; 

        _isLoading = true;
        _sceneToLoad = sceneSO.GetSceneName(); 

        if (! _containGameplayScene)
        {
            AsyncOperation asy = SceneManager.LoadSceneAsync(_gamePlaySceneSO.GetSceneName(), LoadSceneMode.Additive) ;
            asy.completed += OnCompleteLoadGameplay;
        }else
        {
            StartCoroutine(UnLoadPreviousScene()); 
        }
    }

    private void LoadMenu(SceneSO sceneSO)
    {
        if (_isLoading)
            return;

        _isLoading = true;
        _sceneToLoad = sceneSO.GetSceneName();

        if (_containGameplayScene)
        {
            AsyncOperation asy = SceneManager.UnloadSceneAsync(_gamePlaySceneSO.GetSceneName());
            asy.completed += OnCompleteLoadGameplay; 

        }
        else
        {
            StartCoroutine(UnLoadPreviousScene());
        }
    }

    private void LoadNewScene()
    {
        AsyncOperation _loadingOperationHandle = SceneManager.LoadSceneAsync(_sceneToLoad, LoadSceneMode.Additive);
        _loadingOperationHandle.completed += OnCompleteLoadedNewScene ;
    }

    private IEnumerator UnLoadPreviousScene()
    {
        if (_currentLoadedScene == null)
            goto unloadDone ;

        SceneManager.UnloadSceneAsync(_currentLoadedScene);

    unloadDone:
        LoadNewScene();

    yield return null;

    }

    private void OnCompleteLoadGameplay(AsyncOperation asy)
    {
        StartCoroutine(UnLoadPreviousScene()); 
    }

    private void OnCompleteLoadedNewScene(AsyncOperation asy)
    {
        _currentLoadedScene = _sceneToLoad;

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(_currentLoadedScene));

        _isLoading = false;
    }
}

