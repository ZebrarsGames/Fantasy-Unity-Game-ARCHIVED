using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorHandler : MonoBehaviour
{
    public string toSceneName;

    public void Interact()
    {
        Debug.Log("Используем дверь, которая ведёт на сцену " + toSceneName);

        int sceneIndex = SceneUtility.GetBuildIndexByScenePath(toSceneName);

        if (sceneIndex != -1)
        {
            SceneManager.LoadScene(toSceneName);
        }
        else
        {
            Debug.LogError($"Сцена '{toSceneName}' не найдена в Build Settings! " + "Проверьте правильность имени или добавьте сцену в окно Build Settings.");
        }
    }
}