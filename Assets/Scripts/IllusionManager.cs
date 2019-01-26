using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IllusionManager : MonoBehaviour {

    public GameObject bounceIllusion, flashIllusion;
    public GameObject bounceUI, flashUI;

    public void ShowVibrateIllusion()
    {
        bounceIllusion.SetActive(false);
        flashIllusion.SetActive(true);

        bounceUI.SetActive(false);
        flashUI.SetActive(true);
    }

    public void ShowBounceIllusion()
    {
        bounceIllusion.SetActive(true);
        flashIllusion.SetActive(false);

        bounceUI.SetActive(true);
        flashUI.SetActive(false);
    }
}
