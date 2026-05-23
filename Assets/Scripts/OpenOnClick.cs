using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class OpenOnClick : MonoBehaviour
{
   public GameObject porte;
   public GameObject fenetre;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

       
    }

        void OnMouseDown()
    {
        Instantiate(fenetre);
    }
}
