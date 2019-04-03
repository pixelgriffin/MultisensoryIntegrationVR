using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;

[System.Serializable]
public class BallFlashUI
{
    public float defaultFlashTime;
    public float defaultFlashEndTime;

    public InputField flashTimeField, flashTimeEndField, soundTimeField, vibrateTimeField;
    public Toggle vibrateToggle;
    public Toggle soundToggle;

    private float flashTime, flashEndTime, soundTime, vibrateTime;
    private bool didFlash, didSound, didVibrate, didEndFlash;

    public BallFlashUI()
    {
        flashTime = soundTime = vibrateTime = defaultFlashTime;
        flashEndTime = defaultFlashEndTime;

        didFlash = didSound = didVibrate = didEndFlash = false;
    }

    public bool Update(float timer, GameObject ball, AudioSource sound, Hand controllerA, Hand controllerB)
    {
        if (timer >= flashTime && !didFlash)
        {
            ball.SetActive(true);

            didFlash = true;
        }

        if (timer >= flashEndTime && !didEndFlash)
        {
            ball.SetActive(false);

            didEndFlash = true;
        }

        if (timer >= soundTime && !didSound)
        {
            if (soundToggle.isOn)
            {
                sound.Play();
            }

            didSound = true;
        }

        if (timer >= vibrateTime && !didVibrate)
        {
            if (vibrateToggle.isOn)
            {
                //controllerA.TriggerHapticPulse(50000);
                //controllerB.TriggerHapticPulse(50000);
                didVibrate = true;

                return true;
            }
        }

        return false;
    }

    public void ValidateValues()
    {
        if (!float.TryParse(flashTimeField.text, out flashTime))
        {
            flashTime = defaultFlashTime;
            flashTimeField.text = "" + defaultFlashTime;
        }
        if (!float.TryParse(flashTimeEndField.text, out flashEndTime))
        {
            flashEndTime = defaultFlashEndTime;
            flashTimeEndField.text = "" + defaultFlashEndTime;
        }
        if (!float.TryParse(soundTimeField.text, out soundTime))
        {
            soundTime = defaultFlashTime;
            soundTimeField.text = "" + defaultFlashTime;
        }
        if (!float.TryParse(vibrateTimeField.text, out vibrateTime))
        {
            vibrateTime = defaultFlashTime;
            vibrateTimeField.text = "" + defaultFlashTime;
        }
    }

    public void Reset()
    {
        didFlash = didSound = didVibrate = didEndFlash = false;
    }
}

public class BallFlasher : MonoBehaviour {

    public GameObject ball;

    public AudioSource sound;

    public Hand controllerA, controllerB;

    public List<BallFlashUI> flashes;

    private float timer;

	void Start () {

        foreach (BallFlashUI flash in flashes)
            flash.ValidateValues();

        timer = 9999999f;
	}
	
	void Update () {
        timer += Time.deltaTime;

        foreach (BallFlashUI flash in flashes)
            flash.ValidateValues();

        foreach (BallFlashUI flash in flashes)
        {
            if(flash.Update(timer, ball, sound, controllerA, controllerB))
            {
                StartCoroutine(LongVibration(controllerA, 0.15f, 1f));
                StartCoroutine(LongVibration(controllerB, 0.15f, 1f));
            }
        }
    }

    public void Play()
    {
        timer = 0f;

        foreach (BallFlashUI flash in flashes)
            flash.Reset();
    }

    //length is how long the vibration should go for
    //strength is vibration strength from 0-1
    IEnumerator LongVibration(Hand hand, float length, float strength) {
        for(float i = 0; i < length; i += Time.deltaTime) {
            hand.TriggerHapticPulse((ushort)Mathf.Lerp(0, 3999, strength));
            yield return null;
        }
    }
}
