using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class test : MonoBehaviour

    
{
    public Vector3 nouvellePosition = Vector3.Zero;
    public GameObject repere ;
    public NavMeshAgent playerAgent;
    public Animator playerAnimator;
    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        playerAgent = GetComponent <NavMeshAgent> ();
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()  {
         if (Input.GetMouseButtonDown(0));
         RaycastHit choque;
         Ray rayon = Camera.main

    }
}
