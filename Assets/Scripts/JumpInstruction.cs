using TMPro;
using UnityEngine;

public class JumpInstruction : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite windowsSprite;
    [SerializeField] private Sprite androidSprite;
    [SerializeField] private TextMeshPro textLabel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        print("Plataforma: " + Application.platform);
        // Detectar la plataforma en la que se ejecuta el juego
        if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
        {
            Debug.Log("Juego ejecutándose en Windows.");
            spriteRenderer.sprite = windowsSprite;

            // Mostrar instrucciones para Windows (ej. tecla 'Espacio')
        } 
        else if (Application.platform == RuntimePlatform.Android)
        {
            Debug.Log("Juego ejecutándose en Android.");
            spriteRenderer.sprite = androidSprite;
            textLabel.text = "Tap the screen to jump.";
            // Mostrar instrucciones para Android (ej. tocar la pantalla)
        }
        else
        {
            Debug.Log("Plataforma no soportada.");
            textLabel.text = "Plataforma no soportada.";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
