using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.SceneManagement; 


public class SceneInitializer : MonoBehaviour
{
    [SerializeField]
    private SceneSO _firstSceneToLoad  ;

    [SerializeField]
    private SceneSO _persistantScene ;

    [Header("Broadcast to")]
    [SerializeField]
    private SceneEventChannel _loadLocation;
    [SerializeField]
    private SceneEventChannel _loadMenu;

    private void Start()
    {
        SceneManager.LoadSceneAsync(_persistantScene.GetSceneName(), LoadSceneMode.Additive).completed += OnLoadAsync ;
    }

    private void OnLoadAsync(AsyncOperation asy)
    {
        if (_firstSceneToLoad.GetSceneType() == SceneSO.GameSceneType.Menu)
        {
            _loadMenu.RaiseEvent(_firstSceneToLoad); 
        }
        else if (_firstSceneToLoad.GetSceneType() == SceneSO.GameSceneType.Location)
        {
            _loadLocation.RaiseEvent(_firstSceneToLoad); 
        }
    }


}
