using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageTextData : MonoBehaviour
{
    public string Key;
    private bool updated;
    public void UpdateText(string _newLanguageText)
    {
        updated = true;
        GetComponent<TMPro.TextMeshProUGUI>().text = _newLanguageText;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (!updated)
            {
                Debug.LogError("gameObject name: " + transform.name + " does not have languageData with Key: " + Key);
            }
        }
    }
}
