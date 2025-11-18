using UnityEngine;
using static NewtonSoftJsonExample;

public class JsonUtilityExample : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        JsonTestClass jsonTest1 = new JsonTestClass();
        string jsonString = JsonUtility.ToJson(jsonTest1);
        Debug.Log(jsonString);

        JsonTestClass jsonTest2 = JsonUtility.FromJson<JsonTestClass>(jsonString);
        jsonTest2.Print();
    }

    // Update is called once per frame
    
}
