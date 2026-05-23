using System.Collections;
using System.Collections.Generic;
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
    public GameObject FenetreChoixJoueur;

    [Header("Delai")]
    public float delaiAvantChoix = 2f;
    public float tempsLimiteOuverte = 5f; // ← temps max porte ouverte

    private int clicCount = 0;
    private int fermeturesCount = 0;
    private int fermeturesMax = 2;
    private GameObject fenetreInstance;
    private Coroutine timerPorte; // ← référence au timer

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
        if (FenetreChoixJoueur.activeSelf) return;

        clicCount++;

        if (clicCount == 1 || clicCount == 3)
        {
            SetEtat(EtatPorte.Ouverte);
            fenetreInstance = Instantiate(fenetrePrefab);

            // Lance le timer — ferme auto si pas cliqué
            timerPorte = StartCoroutine(FermerAuto());
        }
        else if (clicCount == 2 || clicCount == 4)
        {
            // Annule le timer si le joueur ferme manuellement
            if (timerPorte != null)
            {
                StopCoroutine(timerPorte);
                timerPorte = null;
            }

            FermerPorte();
        }
    }

    IEnumerator FermerAuto()
    {
        yield return new WaitForSeconds(tempsLimiteOuverte);
        Debug.Log("Temps écoulé ! Fermeture automatique.");
        clicCount++; // simule un clic de fermeture
        FermerPorte();
    }

    void FermerPorte()
    {
        fermeturesCount++;
        SetEtat(EtatPorte.Fermee);

        if (fenetreInstance != null)
        {
            Destroy(fenetreInstance);
            fenetreInstance = null;
        }

        if (fermeturesCount >= fermeturesMax)
        {
            StartCoroutine(OuvrirChoixApresDelai());
            clicCount = 0;
            fermeturesCount = 0;
        }
    }

    IEnumerator OuvrirChoixApresDelai()
    {
        yield return new WaitForSeconds(delaiAvantChoix);
        FenetreChoixJoueur.SetActive(true);
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