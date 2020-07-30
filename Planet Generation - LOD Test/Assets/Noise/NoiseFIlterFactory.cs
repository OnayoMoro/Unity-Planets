using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NoiseFilterFactory 
{
    public static INoiseFilter CreateNoiseFilter(NoiseFilter settings)
    {
        switch (settings.filterType)
        {
            case NoiseFilter.FilterType.Simple:
                return new SimpleNoiseFilter(settings.simpleNoiseSettings);
            case NoiseFilter.FilterType.Ridgid: 
                return new RidgidNoiseFilter(settings.ridgidNoiseSettings);
        }
        return null;
    }
}
