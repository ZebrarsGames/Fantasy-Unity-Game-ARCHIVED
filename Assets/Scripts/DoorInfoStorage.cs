using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorInfoStorage : MonoBehaviour
{
    
    public string toSceneName;

    public void Interact()
    {
        Debug.Log("Используем дверь, которая ведёт на сцену " + toSceneName);
        if(SceneManager.GetSceneByName(toSceneName) == null)
        {
            Debug.LogWarning("Не найдена сцена с именем " + toSceneName);
        } else
        {
            SceneManager.LoadScene(toSceneName);
        }
        
    }

}
