using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.VFX;

public class WeatherSystem : MonoBehaviour
{
    UpdateSky updateSky;

    [Header("Global")]
    public Material globalMaterial;
    public Light sunLight;
    public Material skyboxMaterial;

    [Header("Winter Assets")]
    public ParticleSystem winterParticleSystem;
    [SerializeField] VisualEffect snowVFX;
    public Volume winterVolume;

    [SerializeField] Color winterSky;
    [SerializeField] Color winterSunColor;
    [SerializeField] Color winterGround;
    [SerializeField] Color winterFogColour;
    [SerializeField] float winterSkyboxExposure;

    [Header("Rain Assets")]
    public ParticleSystem rainParticleSystem;
    public Volume rainVolume;

    [SerializeField] Color rainSky;
    [SerializeField] Color rainSunColor;
    [SerializeField] Color rainGround;
    [SerializeField] Color rainFogColour;
    [SerializeField] float rainSkyboxExposure;

    [Header("Autumn Assets")]
    public ParticleSystem autumnParticleSystem;
    public Volume autumnVolume;

    [SerializeField] Color autumnSky;
    [SerializeField] Color autumnSunColor;
    [SerializeField] Color autumnGround;
    [SerializeField] Color autumnFogColour;
    [SerializeField] float autumnSkyboxExposure;

    [Header("Summer Assets")]
    public ParticleSystem summerParticleSystem;
    public Volume summerVolume;
    [SerializeField] Color summerSky;
    [SerializeField] Color summerSunColor;
    [SerializeField] Color summerGround;
    [SerializeField] Color summerFogColour;
    [SerializeField] float summerSkyboxExposure;

    private void Start()
    {
        updateSky = GetComponent<UpdateSky>();
        Summer();
    }

    public void Winter()
    {
        VolumeSetter(winterVolume);
        
        snowVFX.Play();
        rainParticleSystem.Stop();

        // enable snow
        globalMaterial.SetFloat("_SnowFade", 1);
        skyboxMaterial.SetFloat("_SunSize", 0);
        skyboxMaterial.SetColor("_SkyTint", winterSky);
        skyboxMaterial.SetColor("_GroundColor", winterGround);

        //sets sunlight to winter sun color
        sunLight.color = winterSunColor;

        //Sets winter fog settings
        RenderSettings.fog = true;
        RenderSettings.fogColor = winterFogColour;
        RenderSettings.fogDensity = 0.03f;


        // set directional light rotation

        // set snow volume parameters

        StartCoroutine(updateSky.UpdateEnvironment());
    }

    public void Rain()
    {
        VolumeSetter(rainVolume);

        snowVFX.Stop();
        rainParticleSystem.Play();


        globalMaterial.SetFloat("_SnowFade", 0);
        sunLight.transform.rotation = new Quaternion(0.359757036f, 0.561913073f, -0.0250049084f, 0.744448364f);
        sunLight.color = rainSunColor;

        RenderSettings.fog = true;
        RenderSettings.fogColor = rainFogColour;
        RenderSettings.fogDensity = 0.01f;

        skyboxMaterial.SetFloat("_SunSize", 0);
        skyboxMaterial.SetColor("_SkyTint", rainSky);
        skyboxMaterial.SetColor("_GroundColor", rainGround);
        skyboxMaterial.SetFloat("_Exposure", rainSkyboxExposure);

        StartCoroutine(updateSky.UpdateEnvironment());
    }

    public void Autumn()
    {

        VolumeSetter(autumnVolume);
 
        snowVFX.Stop();
        rainParticleSystem.Stop();

        //turn off snow
        globalMaterial.SetFloat("_SnowFade", 0);

        // set directional light rotation
        sunLight.transform.rotation = new Quaternion(0.484988034f, -0.49216494f, -0.255326688f, 0.676290333f);

        sunLight.color = autumnSunColor;

        //Set Skybox
        skyboxMaterial.SetFloat("_SunSize", .05f);
        skyboxMaterial.SetColor("_SkyTint", autumnSky);
        skyboxMaterial.SetColor("_GroundColor", autumnGround);
        skyboxMaterial.SetFloat("_Exposure", autumnSkyboxExposure);

        RenderSettings.fog = true;
        RenderSettings.fogColor = autumnFogColour;
        RenderSettings.fogDensity = 0.015f;

        // enable leaves blowing vfx

        StartCoroutine(updateSky.UpdateEnvironment());
    }

    public void Summer()
    {
        VolumeSetter(summerVolume);
       
        snowVFX.Stop();
        rainParticleSystem.Stop();

        sunLight.transform.rotation = new Quaternion(0.641770482f, -0.25606823f, 0.0376434587f, 0.721902192f);

        sunLight.color = summerSunColor;

        // turn off snow
        globalMaterial.SetFloat("_SnowFade", 0);
        skyboxMaterial.SetFloat("_SunSize", 0.03f);

        skyboxMaterial.SetColor("_SkyTint", summerSky);
        skyboxMaterial.SetColor("_GroundColor", summerGround);
        skyboxMaterial.SetFloat("_Exposure", summerSkyboxExposure);

        RenderSettings.fog = true;
        RenderSettings.fogColor = summerFogColour;
        RenderSettings.fogDensity = 0.01f;

        

        StartCoroutine(updateSky.UpdateEnvironment());
    }
    void VolumeSetter(Volume myVolume)
    {
        
        Volume[] volumes = { summerVolume, winterVolume, autumnVolume, rainVolume };

        foreach (Volume _volume in volumes)
        {
            if (_volume == myVolume) { _volume.weight = 1; }
            else { _volume.weight = 0; }
        }
    }


}
