using UnityEngine;
using Newtonsoft.Json;
using System.Collections.Generic;

public class NewtonSoftJsonExample : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    //    JsonTestClass jsonTest1 = new JsonTestClass();
    //    string jsonString = JsonConvert.SerializeObject(jsonTest1);
    //    Debug.Log(jsonString);
    //    JsonTestClass jsonTest2 = JsonConvert.DeserializeObject<JsonTestClass>(jsonString);
    //    jsonTest2.Print();
        //GameObject obj = new GameObject();
        //obj.AddComponent<NewtonSoftJsonExample>();
        //Debug.Log(JsonConvert.SerializeObject(obj.GetComponent<TestMono>()));

    }

    // Update is called once per frame
    void Update()
    {

    }

    public class JsonTestClass
    {
        public int i;
        public string s;
        public float f;
        public bool b;
        public int[] arr;
        public List<int> list = new List<int>();
        public Dictionary<string, int> dict = new Dictionary<string, int>();

        public IntVector2 vec2;

        public JsonTestClass()
        {
            i = 10;
            s = "Hello, World!";
            f = 3.14f;
            b = true;
            arr = new int[] { 1, 2, 3, 4, 5 };
            list.Add(10);
            list.Add(20);
            list.Add(30);
            dict.Add("one", 1);
            dict.Add("two", 2);
            dict.Add("three", 3);
            vec2 = new IntVector2(5, 10);
        }

        public void Print()
        {
            Debug.Log($"i: {i}");
            Debug.Log($"s: {s}");
            Debug.Log($"f: {f}");
            Debug.Log($"b: {b}");
            Debug.Log($"arr: {string.Join(", ", arr)}");
            Debug.Log($"list: {string.Join(", ", list)}");
            foreach (var kvp in dict)
            {
                Debug.Log($"dict[{kvp.Key}]: {kvp.Value}");
            }
            Debug.Log($"vec2: ({vec2.x}, {vec2.y})");
        }
        [System.Serializable]

        public class IntVector2
        {
            public int x;
            public int y;

            public IntVector2(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }
    }

}
