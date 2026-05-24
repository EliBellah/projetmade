using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Portes")]
    public List<GameObject> Porte;
    public int PorteActives = 1;
    public int PorteMax = 6;

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
            Debug.Log("Correct ! Une porte de plus.");
            PorteActives++;

            if (PorteActives > PorteMax)
            {
                PorteActives = PorteMax;
                Debug.Log("Maximum atteint ! Victoire !");
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
            Debug.Log("Correct ! Anomalie trouvée.");
            PorteActives++;

            if (PorteActives > PorteMax)
            {
                PorteActives = PorteMax;
                Debug.Log("Maximum atteint ! Victoire !");
            }

            MettreAJourPorte();
        }
        else
        {
            Debug.Log("Mauvais choix ! Pas d'anomalie.");
            ResetPorte();
        }
    }

    void ResetPorte()
    {
        PorteActives = 1;
        MettreAJourPorte();

        foreach (GameObject porteObj in Porte)
        {
            GestionPorte gestion = porteObj.GetComponent<GestionPorte>();
            if (gestion != null) gestion.ResetPorte();
        }

        Debug.Log("Retour à 1 porte.");
    }

    void MettreAJourPorte()
    {
        for (int i = 0; i < Porte.Count; i++)
            Porte[i].SetActive(i < PorteActives);
    }
}