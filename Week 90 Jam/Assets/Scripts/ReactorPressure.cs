using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactorPressure : MonoBehaviour
{
    [Range(0, 100)]
    [SerializeField]
    private int _pressure;
    // Set this property from other scripts to alter pressure
    public int pressure {
        get {
            return _pressure;
                }
        set {
            // Ensure value is always 0 - 100
            //This wasn't working, but I don't want to touch it either. I don't know enough about get and set.
            _pressure = Mathf.Clamp(value, 0, 100);
            isDecaying = false;
        }
    }

    [Header("Pressure decays 1/sec")]
    public float decayRateInSeconds = 1.0f;
    public float delayBeforeDecayStartsInSeconds = 6.0f;

    //private bool decayCountdownRuning = true;
    private bool isPlaying; //This bool is to make sure the coroutine runs at all times, so we can avoid using update.
    public bool isDecaying;


    void Start()
    {
        isDecaying = false;
        isPlaying = true;
        pressure = 5;
        StartCoroutine(Decay());
    }

    IEnumerator Decay()
    {
        while (isPlaying)
        {
            while (pressure != 0 && isDecaying == true)
            {
                _pressure--; // Sets backing field directly to avoid starting the delay all over again
                yield return new WaitForSecondsRealtime(decayRateInSeconds);
            }
            while (pressure != 0 && isDecaying == false)
            {
                yield return new WaitForSecondsRealtime(delayBeforeDecayStartsInSeconds);
                isDecaying = true;
            }
            while (pressure == 0)
            {
                yield return new WaitForSecondsRealtime(1.0f);
            }
        }
    }
}
