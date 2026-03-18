using UnityEngine;

public class DoorInfoStorage : MonoBehaviour
{
    
    public string toSceneName;

    public void Interact()
    {
        Debug.Log("Используем дверь, которая ведёт на сцену " + toSceneName);
    }

}
