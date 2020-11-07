using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NoiseSettings_planet2
{
    [System.Serializable]
    public class SimpleNoiseSettings_planet2
    {
        public float strength = 1;
        [Range(1,8)]
        public int numLayers = 1;
        public float baseRoughness = 1;
        public float roughness = 2;
        public float persistence = .5f;
        public Vector3 center;
        public float minValue;
    }

    [System.Serializable]
    public class RidgidNoiseSettings_planet2 : SimpleNoiseSettings_planet2
    {
        public float weightMultiplier = .8f;
    }
    
    public enum FilterType { Simple, Ridgid };
    public FilterType filterType;

    [ConditionalHide("filterType", 0)]
    public SimpleNoiseSettings_planet2 simpleNoiseSettings;
    [ConditionalHide("filterType", 1)]
    public RidgidNoiseSettings_planet2 ridgidNoiseSettings;
}
