using UnityEngine;
using TMPro; // Si tu utilises TextMeshPro

public class ARTextFollow : MonoBehaviour
{
    public Camera arCamera; // La caméra AR (souvent la caméra principale)
    public TextMeshPro text; // Le composant TextMeshPro
    public Transform target; // Optionnel : un objet ou un point à suivre

    void Update()
    {
        // Si tu veux que le texte suive directement la caméra :
        if (arCamera != null)
        {
            transform.position = arCamera.transform.position + arCamera.transform.forward * 2f; // 2f : distance à la caméra
            transform.LookAt(arCamera.transform); // Faire en sorte que le texte regarde la caméra
            transform.Rotate(0f, 180f, 0f); // Si tu veux que le texte soit bien orienté (lecteur en bas)
        }

        // Si tu veux que le texte suive un autre objet (par exemple une position AR spécifique) :
        if (target != null)
        {
            transform.position = target.position; // Le texte suit la position de l'objet
            transform.LookAt(arCamera.transform); // Le texte regarde toujours la caméra
        }
    }
}
