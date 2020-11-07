using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RidgidNoiseFilter_planet2 : INoiseFilter_planet2
{
    Noise noise = new Noise();
    NoiseSettings_planet2.RidgidNoiseSettings_planet2 settings;

    public RidgidNoiseFilter_planet2(NoiseSettings_planet2.RidgidNoiseSettings_planet2 settings)
    { 
        this.settings = settings;
    }

    public float Evaluate(Vector3 point)
    {
        float noiseValue = 0;
        float frequency = settings.baseRoughness;
        float amplitude = 1;
        float weight = 1;

        for (int i = 0; i < settings.numLayers; i++)
        {
            float v = 1-Mathf.Abs(noise.Evaluate(point*frequency+settings.center));
            v *= v;
            v *= weight;
            weight = v * settings.weightMultiplier;
            noiseValue += v * amplitude;
            frequency *= settings.roughness;
            amplitude *= settings.persistence;
        }

        noiseValue = Mathf.Max(0, noiseValue-settings.minValue);
        return noiseValue * settings.strength;
    }
}
