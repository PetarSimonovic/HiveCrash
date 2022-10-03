using TMPro;
using UnityEngine;


public class UIText : MonoBehaviour
{
    private TMP_Text uiText;

    void Awake()
    {
        uiText  = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetText(string text)
    {
      uiText.SetText(text);
    }
}
