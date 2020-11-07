using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ShapeSettings_planet2 : ScriptableObject
{
    public float planetRadius = 1;
    public NoiseLayer_planet2[] noiseLayers;

    [System.Serializable]
    public class NoiseLayer_planet2
    {
        public bool enabled = true;
        public bool useFirstLayerAsMask;
        public NoiseSettings_planet2 noiseSettings;
    }
}
