using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

[Serializable]
public class Decayer
{
    public float decaySpeed;
    public float magnitude;
    public AnimationCurve curve;
    [NonSerialized] public float value;
    [NonSerialized] public float valueInv;
    [NonSerialized] public float t;

    public void SetT(float v) => t = v;

    public void Update()
    {
        t = Mathf.Max(0, (t - decaySpeed * Time.deltaTime));
        float tEval = curve.keys.Length > 0 ? curve.Evaluate(1f - t) : t;
        value = tEval * magnitude;
        valueInv = (1f - tEval) * magnitude;
    }
}