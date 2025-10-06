using UnityEngine;

public class Test : MonoBehaviour
{
    public ActSystem actSystem;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        actSystem.SetAct();
    }
}
