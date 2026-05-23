using UnityEngine;

public class GestionPorte : MonoBehaviour
{
    [Header("Textures")]
    public Texture porteFermee;
    public Texture porteSemiOuverte;
    public Texture porteOuverte;

    [Header("Composants")]
    public Renderer meshRenderer;

    [Header("Prefabs et Canvas")]
    public GameObject fenetrePrefab;
    public GameObject canvas;

    private int clicCount = 0;
    private int fermeturesCount = 0;
    private int fermeturesMax = 2;
    private GameObject fenetreInstance; // ← référence à la fenetre active

    public enum EtatPorte { Fermee, SemiOuverte, Ouverte }
    public EtatPorte etatActuel = EtatPorte.Fermee;

    void Start()
    {
        SetEtat(EtatPorte.Fermee);
    }

    void OnMouseEnter()
    {
        if (etatActuel == EtatPorte.Fermee)
            meshRenderer.material.mainTexture = porteSemiOuverte;
    }

    void OnMouseExit()
    {
        if (etatActuel == EtatPorte.Fermee)
            meshRenderer.material.mainTexture = porteFermee;
    }

    void OnMouseDown()
    {
        if (canvas.activeSelf) return;

        clicCount++;

        if (clicCount == 1 || clicCount == 3)
        {
            SetEtat(EtatPorte.Ouverte);
            fenetreInstance = Instantiate(fenetrePrefab); // ← on garde la référence
        }
        else if (clicCount == 2 || clicCount == 4)
        {
            fermeturesCount++;
            SetEtat(EtatPorte.Fermee);

            // Détruit la fenetre quand la porte se ferme
            if (fenetreInstance != null)
            {
                Destroy(fenetreInstance);
                fenetreInstance = null;
            }

            if (fermeturesCount >= fermeturesMax)
            {
                canvas.SetActive(true);
                clicCount = 0;
                fermeturesCount = 0;
            }
        }
    }

    public void SetEtat(EtatPorte etat)
    {
        etatActuel = etat;

        switch (etat)
        {
            case EtatPorte.Fermee:
                meshRenderer.material.mainTexture = porteFermee;
                break;
            case EtatPorte.SemiOuverte:
                meshRenderer.material.mainTexture = porteSemiOuverte;
                break;
            case EtatPorte.Ouverte:
                meshRenderer.material.mainTexture = porteOuverte;
                break;
        }
    }
}