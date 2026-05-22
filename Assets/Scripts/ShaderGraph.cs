using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Reflection;

public class ShaderGraph : MonoBehaviour
{
    public string featureName = "FullScreenPassRendererFeature";
    public bool enableFeature = true; // true = Scène 1, false = Scène 2

    void Start()
    {
        UniversalRenderPipelineAsset urpAsset =
            (UniversalRenderPipelineAsset)QualitySettings.renderPipeline;

        var renderer = urpAsset.GetRenderer(0);

        // Accès forcé via réflexion
        var property = typeof(ScriptableRenderer).GetProperty(
            "rendererFeatures",
            BindingFlags.NonPublic | BindingFlags.Instance
        );

        var features = (System.Collections.Generic.List<ScriptableRendererFeature>)
            property.GetValue(renderer);

        foreach (var feature in features)
        {
            if (feature.name == featureName)
            {
                feature.SetActive(enableFeature);
                return;
            }
        }

        Debug.LogWarning("Renderer Feature introuvable : " + featureName);
    }
}