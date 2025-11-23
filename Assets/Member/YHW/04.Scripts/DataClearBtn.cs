using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DataClearBtn : MonoBehaviour
{
    [SerializeField] Image textImage;


    public void ClearData()
    {
        PlayerPrefs.DeleteAll();
        StartCoroutine(ShowText());
    }

    private IEnumerator ShowText()
    {
        textImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        textImage.gameObject.SetActive(false);
    }
}
