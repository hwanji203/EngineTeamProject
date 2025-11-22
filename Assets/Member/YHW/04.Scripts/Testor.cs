using Member.Kimyongmin._02.Code.Agent;
using UnityEngine;
using UnityEngine.InputSystem;

public class Testor : MonoBehaviour
{
    //HealthSystem healthSystem;
    private void Awake()
    {
       // healthSystem = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthSystem>();
    }

    private void Start()
    {
        //healthSystem.SetHealth(100f);
    }
    void Update()
    {
        
        if (Keyboard.current.mKey.wasPressedThisFrame)
        {
            //healthSystem.GetDamage(10f);
            //Debug.Log(healthSystem.Health);

        }
        if (Keyboard.current.nKey.wasPressedThisFrame)
        {
            UIManager.Instance.OpenUI(UIType.ClearUI);
        }

        if(Keyboard.current.aKey.isPressed)
        {
            transform.position += Vector3.left * 5 * Time.deltaTime;
        }
        if(Keyboard.current.dKey.isPressed)
        {
            transform.position += Vector3.right * 5 * Time.deltaTime;

        }

    }
}
