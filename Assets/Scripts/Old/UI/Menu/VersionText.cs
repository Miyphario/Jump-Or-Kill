using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VersionText : MonoBehaviour
{
    public Text my_text;
    void Start()
    {
        my_text.text = "Version " + Application.version;
    }
}
