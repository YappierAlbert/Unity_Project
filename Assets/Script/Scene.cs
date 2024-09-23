using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
    public void PlayGame(){
        SceneManager.LoadSceneAsync(1);
    }

    public void Credit(){
        SceneManager.LoadSceneAsync(3);
    }

    public void Menu(){
        SceneManager.LoadSceneAsync(0);
    }

}
