using UnityEngine;

[System.Serializable]
public class ImageFenetre
{
    public Texture texture;
}

public class GestionFenetre : MonoBehaviour
{
    [Header("Images normales")]
    public ImageFenetre[] imagesNormales;

    [Header("Anomalies faciles")]
    public ImageFenetre[] anomaliesFaciles;

    [Header("Anomalies moyennes")]
    public ImageFenetre[] anomaliesMoyennes;

    [Header("Anomalies difficiles")]
    public ImageFenetre[] anomaliesDifficiles;

    [Header("Composants")]
    public Renderer meshRenderer;
   // public GameObject 

    public void Initialiser(ImageFenetre image, bool estAnomalie)
    {
        meshRenderer.material.mainTexture = image.texture;
        GameManager.Instance.anomaliePresente = estAnomalie;
        Debug.Log(estAnomalie ? "Anomalie " : "Normal");
    }

    public (ImageFenetre, bool) GetImageAleatoire(ref int dernierIndex)
    {
        bool choisirAnomalie = Random.value > 0.5f;

        if (!choisirAnomalie)
            return (GetSansRepetition(imagesNormales, ref dernierIndex), false);

        string niveau = GameManager.Instance.GetNiveauDifficulte();

        return niveau switch
        {
            "facile"    => (GetSansRepetition(anomaliesFaciles, ref dernierIndex), true),
            "moyen"     => (GetSansRepetition(anomaliesMoyennes, ref dernierIndex), true),
            "difficile" => (GetSansRepetition(anomaliesDifficiles, ref dernierIndex), true),
            _           => (GetSansRepetition(anomaliesFaciles, ref dernierIndex), true)
        };
    }

    ImageFenetre GetSansRepetition(ImageFenetre[] tableau, ref int dernierIndex)
    {
        int index;
        do { index = Random.Range(0, tableau.Length); }
        while (index == dernierIndex && tableau.Length > 1);
        dernierIndex = index;
        return tableau[index];
    }
}