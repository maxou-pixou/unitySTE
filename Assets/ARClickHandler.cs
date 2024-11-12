using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class ARClickHandler : MonoBehaviour
{
    // Variable qui s'incrémente à chaque clic
    public int clickCount = 0;

    // Références aux objets "Carte4", "Carte5" et "Carte6"
    public GameObject carte3;
    public GameObject carte4;
    public GameObject carte5;
    public GameObject carte6;

    // Référence à l'AR Raycast Manager
    private ARRaycastManager raycastManager;

    // Liste des résultats de raycast
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Start()
    {
        // Récupère le ARRaycastManager attaché à l'AR Session Origin
        raycastManager = GetComponent<ARRaycastManager>();
    }


    void Update()
    {
        // Si l'utilisateur effectue un tap sur l'écran (ou clic souris sur desktop)
        if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
        {
            // Effectuer un raycast à partir de la position du tap/clic
            Vector2 screenCenter = Input.touchCount > 0 ? Input.GetTouch(0).position : (Vector2)Input.mousePosition;

            // Vérifie si raycastManager est null
            if (raycastManager == null)
            {
                Debug.LogError("ARRaycastManager n'est pas assigné !");
                return;
            }

            // On effectue un raycast en 2D (cela permet de trouver un plan AR)
            if (raycastManager.Raycast(screenCenter, hits, TrackableType.PlaneWithinPolygon))
            {
                // Vérifie si le raycast a touché un objet AR
                foreach (ARRaycastHit hit in hits)
                {
                    // Utilisation de hit.pose.position pour obtenir la position touchée par le raycast
                    RaycastHit raycastHit;

                    // Essayons de récupérer un objet avec un collider sous cette position
                    if (Physics.Raycast(hit.pose.position, Vector3.forward, out raycastHit))
                    {
                        GameObject hitObject = raycastHit.collider.gameObject;

                        // Débogage : Vérifie si l'objet touché est valide
                        if (hitObject == null)
                        {
                            Debug.LogError("L'objet touché est null");
                            continue;
                        }

                        // Vérifie si l'objet touché est un des objets à gérer
                        if (hitObject.CompareTag("carte3") || hitObject.CompareTag("carte4") || hitObject.CompareTag("carte5") || hitObject.CompareTag("carte6"))
                        {
                            // Incrémente la variable clickCount
                            clickCount++;
                            Debug.Log("Prefab cliqué ! Nombre de clics : " + clickCount);

                            // Si le clickCount est égal à zéro, cache les objets Carte4, Carte5, Carte6
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
        // Désactive Carte4
        if (carte4 != null)
        {
            carte4.SetActive(false);  // Cache l'objet en désactivant complètement
        }

        // Désactive Carte5
        if (carte5 != null)
        {
            carte5.SetActive(false);  // Cache l'objet en désactivant complètement
        }

        // Désactive Carte6
        if (carte6 != null)
        {
            carte6.SetActive(false);  // Cache l'objet en désactivant complètement
        }

        
    }

    // Affiche Carte6 si le clickCount est 1
    void ShowCarte6()
    {
        if (carte6 != null)
        {
            carte6.SetActive(true);  // Réactive Carte6
            Debug.Log("Carte6 affichée");
        }
    }

    // Affiche Carte4 si le clickCount est 2
    void ShowCarte4()
    {
        if (carte4 != null)
        {
            carte4.SetActive(true);  // Réactive Carte4
            Debug.Log("Carte4 affichée");
        }
    }

    // Affiche Carte5 si le clickCount est 3
    void ShowCarte5()
    {
        if (carte5 != null)
        {
            carte5.SetActive(true);  // Réactive Carte5
            Debug.Log("Carte5 affichée");
        }
    }
    void ShowCarte3()
    {
        if (carte3 != null)
        {
            carte3.SetActive(true);  // Réactive Carte5
            Debug.Log("Carte3 affichée");
        }
    }
}
