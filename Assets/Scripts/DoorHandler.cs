using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorHandler : MonoBehaviour
{
    public string toSceneName;
    public UnityEngine.Vector3 coords;

    public void Interact()
    {
        Debug.Log("Используем дверь, которая ведёт на сцену " + toSceneName);

        int sceneIndex = SceneUtility.GetBuildIndexByScenePath(toSceneName);

        if (sceneIndex != -1)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.LoadScene(sceneIndex);
        }
        else
        {
            Debug.LogError($"Сцена '{toSceneName}' не найдена в Build Settings! " + "Проверьте правильность имени или добавьте сцену в окно Build Settings.");
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == toSceneName)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.transform.position = coords;
            player.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}