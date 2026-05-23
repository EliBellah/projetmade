using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Porte")]
    public List<GameObject> Porte; // glisse tes 6 Porte ici dans l'Inspector
    public int PorteActives = 1;   // commence avec 1 porte
    public int PorteMax = 6;

    [Header("Anomalie")]
    public bool anomaliePresente = false; // tu gères ça selon ta scène

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        MettreAJourPorte();
    }

    // Le joueur dit "non"
    public void JoueurDitNon()
    {
        if (anomaliePresente)
        {
            // Joueur a tort — anomalie manquée
            Debug.Log("Mauvais choix ! Anomalie manquée.");
            ResetPorte();
        }
        else
        {
            // Joueur a raison — on avance
            Debug.Log("Correct ! Une porte de plus.");
            PorteActives++;

            if (PorteActives > PorteMax)
            {
                PorteActives = PorteMax;
                Debug.Log("Maximum atteint !");
            }

            MettreAJourPorte();
        }
    }

    // Le joueur dit "Il y a une anomalie"
    public void JoueurDitOui()
    {
        if (anomaliePresente)
        {
            // Joueur a raison
            Debug.Log("Correct ! Anomalie trouvée.");
            MettreAJourPorte(); // on garde le même nombre
        }
        else
        {
            // Joueur a tort
            Debug.Log("Mauvais choix ! Pas d'anomalie.");
            ResetPorte();
        }
    }

    // Remet à 1 porte si erreur
    void ResetPorte()
    {
        PorteActives = 1;
        MettreAJourPorte();
        Debug.Log("Retour à 1 porte.");
    }

    // Active/désactive les Porte selon le nombre
    void MettreAJourPorte()
    {
        for (int i = 0; i < Porte.Count; i++)
        {
            Porte[i].SetActive(i < PorteActives);
        }
    }
}