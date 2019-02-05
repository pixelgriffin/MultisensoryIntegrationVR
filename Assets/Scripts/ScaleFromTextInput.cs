using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScaleFromTextInput : MonoBehaviour {

    public InputField scaleInput;

	void Update () {
        float scale = 1f;
        if (!float.TryParse(scaleInput.text, out scale))
        {
            scale = 1f;
            scaleInput.text = "" + 1;
        }

        this.transform.localScale = Vector3.one * scale;
    }
}
