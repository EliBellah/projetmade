using System.Collections;
using UnityEngine;

public class GestionPorte : MonoBehaviour
{
    [Header("Textures")]
    public Texture[] textures = new Texture[3]; // 0=Fermee 1=Semi 2=Ouverte

    [Header("Composants")]
    public Renderer meshRenderer;

    [Header("Prefabs et Canvas")]
    public GameObject Fenetre;
    public GameObject FenetreChoixJoueur;

    [Header("Timers")] // temps avant affichage automatique de la fenetre a choix 
    public float delaiAvantChoix = 20f;   //2f et 5f
    public float tempsLimiteOuverte = 50f;

    private int clicCount = 0;
    private int fermeturesCount = 0;
    private GameObject FenetreInstance;
    private Coroutine timerPorte;
    private bool porteVerrouillee = false;
    private ImageFenetre imageChoisie = null;
    private bool estAnomalieChoisie = false;
    private int dernierIndex = -1;

    void Start() => AppliquerTexture(0); //texture de porte avec comportement curseur

    void OnMouseEnter()
    {
        if (!porteVerrouillee && clicCount % 2 == 0)
            AppliquerTexture(1);
    }

    void OnMouseExit()
    {
        if (!porteVerrouillee && clicCount % 2 == 0)
            AppliquerTexture(0);
    }

    void OnMouseDown()
    {
        if (porteVerrouillee || FenetreChoixJoueur.activeSelf) return;

        clicCount++;

        if (clicCount % 2 == 1)
        {
            AppliquerTexture(2);
            FenetreInstance = Instantiate(Fenetre);

            GestionFenetre gf = FenetreInstance.GetComponent<GestionFenetre>(); // ici?

            if (imageChoisie == null)
            {
                var (image, estAnomalie) = gf.GetImageAleatoire(ref dernierIndex);
                imageChoisie = image;
                estAnomalieChoisie = estAnomalie;
            }

            gf.Initialiser(imageChoisie, estAnomalieChoisie);
            timerPorte = StartCoroutine(FermerAuto());
        }
        else
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
        if (FenetreInstance != null) Destroy(FenetreInstance);

        if (++fermeturesCount >= 2)
        {
            porteVerrouillee = true;
            StartCoroutine(OuvrirChoixApresDelai());
        }
    }

    IEnumerator OuvrirChoixApresDelai()
    {
        yield return new WaitForSeconds(delaiAvantChoix);
        FenetreChoixJoueur.SetActive(true);
    }

    public void ResetPorte()
    {
        if (timerPorte != null) StopCoroutine(timerPorte);
        StopAllCoroutines();
        if (FenetreInstance != null) Destroy(FenetreInstance);

        clicCount = 0;
        fermeturesCount = 0;
        porteVerrouillee = false;
        imageChoisie = null;
        estAnomalieChoisie = false;
        dernierIndex = -1;

        AppliquerTexture(0);
    }

    void AppliquerTexture(int index) => meshRenderer.material.mainTexture = textures[index];
}