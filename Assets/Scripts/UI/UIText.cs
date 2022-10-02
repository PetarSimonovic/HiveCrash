using TMPro;
using UnityEngine;


public class UIText : MonoBehaviour
{
    [SerializeField]
    private TMP_Text hivePollenUI;

    private int hivePollen;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hivePollenUI.SetText("100%");
    }
}
