using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class VibratingPointLight : MonoBehaviour
{
    public Light2D Light;
    public float VibrationAmount;
    public float VibrationTime;

    private float _targetMaxRadius;
    private float _targetMinRadius;

    void Start()
    {
        _targetMaxRadius = Light.pointLightOuterRadius + VibrationAmount;
        _targetMinRadius = Light.pointLightOuterRadius + VibrationAmount;
    }

    // Update is called once per frame
    void Update()
    {
        var t = Mathf.PingPong(Time.time * VibrationTime, VibrationAmount);
        var e = Easing.Bounce.InOut(t);
        Light.pointLightOuterRadius = e + _targetMinRadius;
    }
}
