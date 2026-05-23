using UnityEngine;

public class UIChoix : MonoBehaviour
{
    public GameObject Canvas;
    public void BoutonNon() // ya pas d'anomalie
    {
        GameManager.Instance.JoueurDitNon();
        Canvas.SetActive(false);
    }

    public void BoutonOui() // ya une anomalie
    {
        GameManager.Instance.JoueurDitOui();
         Canvas.SetActive(false);
    }
}