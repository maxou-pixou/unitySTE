using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class ARClickHandler : MonoBehaviour
{
    // Variable qui s'incr�mente � chaque clic
    public int clickCount = 0;

    // R�f�rences aux objets "Carte4", "Carte5" et "Carte6"
    public GameObject carte3;
    public GameObject carte4;
    public GameObject carte5;
    public GameObject carte6;

    // R�f�rence � l'AR Raycast Manager
    private ARRaycastManager raycastManager;

    // Liste des r�sultats de raycast
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Start()
    {
        // R�cup�re le ARRaycastManager attach� � l'AR Session Origin
        raycastManager = GetComponent<ARRaycastManager>();
    }


    void Update()
    {
        // Si l'utilisateur effectue un tap sur l'�cran (ou clic souris sur desktop)
        if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
        {
            // Effectuer un raycast � partir de la position du tap/clic
            Vector2 screenCenter = Input.touchCount > 0 ? Input.GetTouch(0).position : (Vector2)Input.mousePosition;

            // V�rifie si raycastManager est null
            if (raycastManager == null)
            {
                Debug.LogError("ARRaycastManager n'est pas assign� !");
                return;
            }

            // On effectue un raycast en 2D (cela permet de trouver un plan AR)
            if (raycastManager.Raycast(screenCenter, hits, TrackableType.PlaneWithinPolygon))
            {
                // V�rifie si le raycast a touch� un objet AR
                foreach (ARRaycastHit hit in hits)
                {
                    // Utilisation de hit.pose.position pour obtenir la position touch�e par le raycast
                    RaycastHit raycastHit;

                    // Essayons de r�cup�rer un objet avec un collider sous cette position
                    if (Physics.Raycast(hit.pose.position, Vector3.forward, out raycastHit))
                    {
                        GameObject hitObject = raycastHit.collider.gameObject;

                        // D�bogage : V�rifie si l'objet touch� est valide
                        if (hitObject == null)
                        {
                            Debug.LogError("L'objet touch� est null");
                            continue;
                        }

                        // V�rifie si l'objet touch� est un des objets � g�rer
                        if (hitObject.CompareTag("carte3") || hitObject.CompareTag("carte4") || hitObject.CompareTag("carte5") || hitObject.CompareTag("carte6"))
                        {
                            // Incr�mente la variable clickCount
                            clickCount++;
                            Debug.Log("Prefab cliqu� ! Nombre de clics : " + clickCount);

                            // Si le clickCount est �gal � z�ro, cache les objets Carte4, Carte5, Carte6
                            if (clickCount == 0)
                            {
                                HidePrefabs();
                                ShowCarte3();
                            }
                            else if (clickCount == 1)
                            {
                                ShowCarte6();
                                ShowCarte3();
                            }
                            else if (clickCount == 2)
                            {
                                ShowCarte3();
                                ShowCarte4();
                                ShowCarte6();
                            }
                            else if (clickCount == 3)
                            {
                                ShowCarte3();
                                ShowCarte5();
                                ShowCarte4();
                                ShowCarte6();
                            }
                        }
                    }
                }
            }
        }
    }


    // Fonction pour cacher les objets Carte4, Carte5 et Carte6
    void HidePrefabs()
    {
        // D�sactive Carte4
        if (carte4 != null)
        {
            carte4.SetActive(false);  // Cache l'objet en d�sactivant compl�tement
        }

        // D�sactive Carte5
        if (carte5 != null)
        {
            carte5.SetActive(false);  // Cache l'objet en d�sactivant compl�tement
        }

        // D�sactive Carte6
        if (carte6 != null)
        {
            carte6.SetActive(false);  // Cache l'objet en d�sactivant compl�tement
        }

        
    }

    // Affiche Carte6 si le clickCount est 1
    void ShowCarte6()
    {
        if (carte6 != null)
        {
            carte6.SetActive(true);  // R�active Carte6
            Debug.Log("Carte6 affich�e");
        }
    }

    // Affiche Carte4 si le clickCount est 2
    void ShowCarte4()
    {
        if (carte4 != null)
        {
            carte4.SetActive(true);  // R�active Carte4
            Debug.Log("Carte4 affich�e");
        }
    }

    // Affiche Carte5 si le clickCount est 3
    void ShowCarte5()
    {
        if (carte5 != null)
        {
            carte5.SetActive(true);  // R�active Carte5
            Debug.Log("Carte5 affich�e");
        }
    }
    void ShowCarte3()
    {
        if (carte3 != null)
        {
            carte3.SetActive(true);  // R�active Carte5
            Debug.Log("Carte3 affich�e");
        }
    }
}
