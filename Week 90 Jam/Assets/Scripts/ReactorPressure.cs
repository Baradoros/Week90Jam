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
            _pressure = Mathf.Clamp(value, 0, 100);
            isDecaying = false;

            // If Delay() is already running, stop the current one before starting a new one
            if (decayCountdownRuning) {
                StopCoroutine(Delay());
                decayCountdownRuning = false;
            }
            StartCoroutine(Delay());
        }
    }

    [Header("Pressure decays 1/sec")]
    public float decayRateInSeconds = 1.0f;
    public float delayBeforeDecayStartsInSeconds = 6.0f;

    private bool decayCountdownRuning = true;
    public bool isDecaying { get; private set; }


    void Start()
    {
        isDecaying = false;
        pressure = 50;
    }

    void Update()
    {
        if (isDecaying) {
            StartCoroutine(Decay());
        } else {
            StopCoroutine(Decay());
        }
        
    }

    IEnumerator Decay() {
        _pressure--; // Sets backing field directly to avoid starting the delay all over again
        yield return new WaitForSeconds(decayRateInSeconds);
        Decay();
    }

    IEnumerator Delay() {
        decayCountdownRuning = true;
        yield return new WaitForSeconds(delayBeforeDecayStartsInSeconds);
        decayCountdownRuning = false;
        isDecaying = true;
    }
}
