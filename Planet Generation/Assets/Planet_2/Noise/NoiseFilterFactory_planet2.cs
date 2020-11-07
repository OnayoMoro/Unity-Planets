using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NoiseFilterFactory_planet2 
{
    public static INoiseFilter_planet2 CreateNoiseFilter(NoiseSettings_planet2 settings)
    {
        switch (settings.filterType)
        {
            case NoiseSettings_planet2.FilterType.Simple:
                return new SimpleNoiseFilter_planet2(settings.simpleNoiseSettings);
            case NoiseSettings_planet2.FilterType.Ridgid: 
                return new RidgidNoiseFilter_planet2(settings.ridgidNoiseSettings);
        }
        return null;
    }
}
