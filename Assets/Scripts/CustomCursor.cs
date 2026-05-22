using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    public GameObject CursorObject; 
    void Start()
    {
        // Cache le curseur système
        Cursor.visible = false;
    }

    void Update()
    {
        Cursor.visible = false;

        // Déplace l'objet à la position de la souris
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f; // distance devant la caméra = nb elevé = reduis taille curseur,
        //max 10 sinon derriere l'ecran = voit pas
        CursorObject.transform.position = 
            Camera.main.ScreenToWorldPoint(mousePos);
    }

    void OnApplicationQuit() // remet le curseur systeme visible quand je quitte le jeu
{
    Cursor.visible = true;
}
}