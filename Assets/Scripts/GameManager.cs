using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Portes")]
    public List<GameObject> Porte;
    public int PorteActives = 1;
    public int PorteMax = 6;

    //   [Header("Fenetres")] jsp pq ca marche pas comme les portes, dcp jai fait des prefab variants
    // public List<GameObject> Fenetre;
    // public int FenetreActives = 1;
    // public int FenetreMax = 6;

    [Header("Anomalie")]
    public bool anomaliePresente = false;

    [Header("Difficulte par nombre de portes")]
    public int seuilMoyen = 3;
    public int seuilDifficile = 4;

    void Awake() => Instance = this;

    void Start() => MettreAJourPorte();

    public string GetNiveauDifficulte()
    {
        if (PorteActives >= seuilDifficile) return "difficile";
        if (PorteActives >= seuilMoyen) return "moyen";
        return "facile";
    }

    public void JoueurDitNon()
    {
        Debug.Log("anomaliePresente = " + anomaliePresente);
        if (!anomaliePresente)
        {
            Debug.Log("Correct ");
            PorteActives++;

            if (PorteActives > PorteMax)
            {
                PorteActives = PorteMax;
                Debug.Log("Maximum atteint");
            }

            MettreAJourPorte();
        }
        else
        {
            Debug.Log("Mauvais choix ! Anomalie manquée.");
            ResetPorte();
        }
    }

    public void JoueurDitOui()
    {
        Debug.Log("anomaliePresente = " + anomaliePresente);
        if (anomaliePresente)
        {
            Debug.Log("Anomalie!");
            PorteActives++;

            if (PorteActives > PorteMax)
            {
                PorteActives = PorteMax;
                Debug.Log("Maximum atteint ");
            }

            MettreAJourPorte();
        }
        else
        {
            Debug.Log("Mauvais choix ! Anomalie manquée.");
            ResetPorte();
        }
    }

    void ResetPorte() // game over si mauvaix choix joueur : retour a une porte
    {
        PorteActives = 1;
        MettreAJourPorte();

        foreach (GameObject porteObj in Porte)
        {
            GestionPorte gestion = porteObj.GetComponent<GestionPorte>();
            if (gestion != null) gestion.ResetPorte();
        }

        Debug.Log("Game over");
    }

    void MettreAJourPorte()
    {
        for (int i = 0; i < Porte.Count; i++)
            Porte[i].SetActive(i < PorteActives);
    }
}