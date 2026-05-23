using System.Collections;
using UnityEngine;

public class GestionPorte : MonoBehaviour
{
    [Header("Textures")]
    public Texture[] textures = new Texture[3]; // 0=Fermee 1=SemiOuverte 2=Ouverte

    [Header("Composants")]
    public Renderer meshRenderer;

    [Header("Prefabs et Canvas")]
    public GameObject fenetrePrefab;
    public GameObject FenetreChoixJoueur;

    [Header("Timers")]
    public float delaiAvantChoix = 2f;
    public float tempsLimiteOuverte = 5f;

    private int clicCount = 0;
    private int fermeturesCount = 0;
    private GameObject fenetreInstance;
    private Coroutine timerPorte;
    private bool porteVerrouilee = false;

    void Start() => AppliquerTexture(0);

    void OnMouseEnter()
    {
        if (!porteVerrouilee && clicCount % 2 == 0)
            AppliquerTexture(1);
    }

    void OnMouseExit()
    {
        if (!porteVerrouilee && clicCount % 2 == 0)
            AppliquerTexture(0);
    }

    void OnMouseDown()
    {
        if (porteVerrouilee || FenetreChoixJoueur.activeSelf) return;

        clicCount++;

        if (clicCount % 2 == 1) // impair = ouvrir
        {
            AppliquerTexture(2);
            fenetreInstance = Instantiate(fenetrePrefab);
            timerPorte = StartCoroutine(FermerAuto());
        }
        else // pair = fermer
        {
            if (timerPorte != null) StopCoroutine(timerPorte);
            FermerPorte();
        }
    }

    IEnumerator FermerAuto()
    {
        yield return new WaitForSeconds(tempsLimiteOuverte);
        clicCount++;
        FermerPorte();
    }

    void FermerPorte()
    {
        AppliquerTexture(0);
        if (fenetreInstance != null) Destroy(fenetreInstance);

        if (++fermeturesCount >= 2)
        {
            porteVerrouilee = true;
            StartCoroutine(OuvrirChoixApresDelai());
        }
    }

    IEnumerator OuvrirChoixApresDelai()
    {
        yield return new WaitForSeconds(delaiAvantChoix);
        FenetreChoixJoueur.SetActive(true);
    }

    void AppliquerTexture(int index) => meshRenderer.material.mainTexture = textures[index];
}