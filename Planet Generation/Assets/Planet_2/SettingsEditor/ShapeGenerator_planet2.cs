using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeGenerator_planet2
{
    ShapeSettings_planet2 settings;
    INoiseFilter_planet2[] noiseFilters;
    public MinMax_planet2 elevationMinMax;

    public void UpdateSettings(ShapeSettings_planet2 settings)
    {
        this.settings = settings;
        noiseFilters = new INoiseFilter_planet2[settings.noiseLayers.Length];
        for (int i = 0; i < noiseFilters.Length; i++)
        {
            noiseFilters[i] = NoiseFilterFactory_planet2.CreateNoiseFilter(settings.noiseLayers[i].noiseSettings);
        }    
        elevationMinMax = new MinMax_planet2();
    }

    public Vector3 CalculatePointOnPlanet(Vector3 pointOnUnitSphere)
    {
        float firstLayerValue = 0;
        float elevation = 0;

        if (noiseFilters.Length > 0)
        {
            firstLayerValue = noiseFilters[0].Evaluate(pointOnUnitSphere);
            if (settings.noiseLayers[0].enabled)
            {
                elevation = firstLayerValue;
            }
        }
        for (int i = 1; i < noiseFilters.Length; i++)
        {
            if (settings.noiseLayers[i].enabled)
            {
                float mask = (settings.noiseLayers[i].useFirstLayerAsMask) ? firstLayerValue : 1;
                elevation += noiseFilters[i].Evaluate(pointOnUnitSphere) * mask;
            }
        }
        elevation = settings.planetRadius * (1+elevation);
        elevationMinMax.AddValue(elevation);
        return pointOnUnitSphere * elevation;
    }
}
