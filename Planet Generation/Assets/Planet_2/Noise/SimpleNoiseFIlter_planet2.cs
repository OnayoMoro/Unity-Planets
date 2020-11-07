using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleNoiseFilter_planet2 : INoiseFilter_planet2
{
    Noise noise = new Noise();
    NoiseSettings_planet2.SimpleNoiseSettings_planet2 settings;

    public SimpleNoiseFilter_planet2(NoiseSettings_planet2.SimpleNoiseSettings_planet2 settings)
    { 
        this.settings = settings;
    }

    public float Evaluate(Vector3 point)
    {
        float noiseValue = 0;
        float frequency = settings.baseRoughness;
        float amplitude = 1;

        for (int i = 0; i < settings.numLayers; i++)
        {
            float v = noise.Evaluate(point*frequency+settings.center);
            noiseValue += (v + 1) * .5f * amplitude;
            frequency *= settings.roughness;
            amplitude *= settings.persistence;
        }

        noiseValue = Mathf.Max(0, noiseValue-settings.minValue);
        return noiseValue * settings.strength; 
    }
}
