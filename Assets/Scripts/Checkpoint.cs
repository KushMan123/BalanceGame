using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Material emmisiveMaterial;
    public Renderer[] emissionObjects;
    public float intensity= 10809.41f;
    public Transform spawnLocation;

    private void Start()
    {

        foreach(Renderer symbol in emissionObjects)
        {
            emmisiveMaterial = symbol.GetComponent<Renderer>().material;
            emmisiveMaterial.SetFloat("_EmissiveIntensity", 1f);
            UpdateEmissiveColorFromIntensityAndEmissiveColorLDR(emmisiveMaterial);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CheckpointManager.cm.UpdateCheckpoint(spawnLocation.position);
            foreach (Renderer symbol in emissionObjects)
            {
                emmisiveMaterial = symbol.GetComponent<Renderer>().material;
                emmisiveMaterial.SetFloat("_EmissiveIntensity", intensity);
                UpdateEmissiveColorFromIntensityAndEmissiveColorLDR(emmisiveMaterial);
            }
        }
    }

    //Changing the Emmisive intensity of HDRP Lit Shader
    public static void UpdateEmissiveColorFromIntensityAndEmissiveColorLDR(Material material)
    {
        const string kEmissiveColorLDR = "_EmissiveColorLDR";
        const string kEmissiveColor = "_EmissiveColor";
        const string kEmissiveIntensity = "_EmissiveIntensity";

        if (material.HasProperty(kEmissiveColorLDR) && material.HasProperty(kEmissiveIntensity) && material.HasProperty(kEmissiveColor))
        {
            // Important: The color picker for kEmissiveColorLDR is LDR and in sRGB color space but Unity don't perform any color space conversion in the color
            // picker BUT only when sending the color data to the shader... So as we are doing our own calculation here in C#, we must do the conversion ourselves.
            Color emissiveColorLDR = material.GetColor(kEmissiveColorLDR);
            Color emissiveColorLDRLinear = new Color(Mathf.GammaToLinearSpace(emissiveColorLDR.r), Mathf.GammaToLinearSpace(emissiveColorLDR.g), Mathf.GammaToLinearSpace(emissiveColorLDR.b));
            material.SetColor(kEmissiveColor, emissiveColorLDRLinear * material.GetFloat(kEmissiveIntensity));
        }
    }
}
