using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;

public class BallFlasher : MonoBehaviour {

    public GameObject ball;

    public AudioSource sound;

    public Hand controllerA, controllerB;

    public InputField flashTimeField;
    public InputField flashEndTimeField;
    public InputField soundTimeField;
    public InputField vibrateTimeField;

    public Toggle doVibrateToggle, doSoundToggle;

    private float flashTime, flashEndTime, soundTime, vibrateTime;

    private float timer;

    private bool didFlash, didSound, didVibrate, didEndFlash;

	void Start () {
        flashTimeField.text = soundTimeField.text = vibrateTimeField.text = "" + 1;
        flashTime = soundTime = vibrateTime = 1f;

        flashEndTime = 1.1f;
        flashEndTimeField.text = "" + 1.1f;

        didFlash = didSound = didVibrate = didEndFlash = true;

        timer = 9999999f;
	}
	
	void Update () {
        timer += Time.deltaTime;

        if (!float.TryParse(flashTimeField.text, out flashTime))
        {
            flashTime = 1f;
            flashTimeField.text = "" + 1;
        }
        if (!float.TryParse(flashEndTimeField.text, out flashEndTime))
        {
            flashEndTime = 1.1f;
            flashEndTimeField.text = "" + 1.1f;
        }
        if (!float.TryParse(soundTimeField.text, out soundTime))
        {
            soundTime = 1f;
            soundTimeField.text = "" + 1;
        }
        if (!float.TryParse(vibrateTimeField.text, out vibrateTime))
        {
            vibrateTime = 1f;
            vibrateTimeField.text = "" + 1;
        }

        if (timer >= flashTime && !didFlash)
        {
            ball.SetActive(true);

            didFlash = true;
        }

        if(timer >= flashEndTime && !didEndFlash)
        {
            ball.SetActive(false);

            didEndFlash = true;
        }

        if(timer >= soundTime && !didSound)
        {
            if (doSoundToggle.isOn)
            {
                sound.Play();
            }

            didSound = true;
        }

        if(timer >= vibrateTime && !didVibrate)
        {
            if (doVibrateToggle.isOn)
            {
                controllerA.TriggerHapticPulse(50000);
                controllerB.TriggerHapticPulse(50000);
            }

            didVibrate = true;
        }
	}

    public void Play()
    {
        timer = 0f;
        didFlash = didSound = didVibrate = didEndFlash = false;
    }

}
