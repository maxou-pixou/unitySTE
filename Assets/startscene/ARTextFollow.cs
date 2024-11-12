using UnityEngine;
using TMPro; // Si tu utilises TextMeshPro

public class ARTextFollow : MonoBehaviour
{
    public Camera arCamera; // La cam�ra AR (souvent la cam�ra principale)
    public TextMeshPro text; // Le composant TextMeshPro
    public Transform target; // Optionnel : un objet ou un point � suivre

    void Update()
    {
        // Si tu veux que le texte suive directement la cam�ra :
        if (arCamera != null)
        {
            transform.position = arCamera.transform.position + arCamera.transform.forward * 2f; // 2f : distance � la cam�ra
            transform.LookAt(arCamera.transform); // Faire en sorte que le texte regarde la cam�ra
            transform.Rotate(0f, 180f, 0f); // Si tu veux que le texte soit bien orient� (lecteur en bas)
        }

        // Si tu veux que le texte suive un autre objet (par exemple une position AR sp�cifique) :
        if (target != null)
        {
            transform.position = target.position; // Le texte suit la position de l'objet
            transform.LookAt(arCamera.transform); // Le texte regarde toujours la cam�ra
        }
    }
}
