using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hint : MonoBehaviour
{
    //While only key not mouse or joystick
    public KeyCode KeyCodeBase;
    private SpriteRenderer spriteRenderer;
    public Sprite[] Sprites = new Sprite[30];
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Switch(KeyCodeBase);
    }
    void Switch(KeyCode key)
    {
        switch(key)
        {
            case KeyCode.A:
                spriteRenderer.sprite = Sprites[0];
            break;
            case KeyCode.B:
                spriteRenderer.sprite = Sprites[1];
            break;
            case KeyCode.C:
                spriteRenderer.sprite = Sprites[2];
            break;
            case KeyCode.D:
                spriteRenderer.sprite = Sprites[3];
                break;
            case KeyCode.E:
                spriteRenderer.sprite = Sprites[4];
                break;
            case KeyCode.F:
                spriteRenderer.sprite = Sprites[5];
                break;
            case KeyCode.G:
                spriteRenderer.sprite = Sprites[6];
                break;
            case KeyCode.H:
                spriteRenderer.sprite = Sprites[7];
                break;
            case KeyCode.I:
                spriteRenderer.sprite = Sprites[8];
                break;
            case KeyCode.J:
                spriteRenderer.sprite = Sprites[9];
                break;
            case KeyCode.K:
                spriteRenderer.sprite = Sprites[10];
                break;
            case KeyCode.L:
                spriteRenderer.sprite = Sprites[11];
                break;
            case KeyCode.M:
                spriteRenderer.sprite = Sprites[12];
                break;
            case KeyCode.N:
                spriteRenderer.sprite = Sprites[13];
                break;
            case KeyCode.O:
                spriteRenderer.sprite = Sprites[14];
                break;
            case KeyCode.P:
                spriteRenderer.sprite = Sprites[15];
                break;
            case KeyCode.Q:
                spriteRenderer.sprite = Sprites[16];
                break;
            case KeyCode.R:
                spriteRenderer.sprite = Sprites[17];
                break;
            case KeyCode.S:
                spriteRenderer.sprite = Sprites[18];
                break;
            case KeyCode.T:
                spriteRenderer.sprite = Sprites[19];
                break;
            case KeyCode.U:
                spriteRenderer.sprite = Sprites[20];
                break;
            case KeyCode.V:
                spriteRenderer.sprite = Sprites[21];
                break;
            case KeyCode.W:
                spriteRenderer.sprite = Sprites[22];
                break;
            case KeyCode.X:
                spriteRenderer.sprite = Sprites[23];
                break;
            case KeyCode.Y:
                spriteRenderer.sprite = Sprites[24];
                break;
            case KeyCode.Z:
                spriteRenderer.sprite = Sprites[25];
                break;
            case KeyCode.LeftControl | KeyCode.RightControl:
                spriteRenderer.sprite = Sprites[26];
            break;
            case KeyCode.LeftShift | KeyCode.RightShift:
                spriteRenderer.sprite = Sprites[27];
            break;
            case KeyCode.KeypadEnter:
                spriteRenderer.sprite = Sprites[28];
            break;
            case KeyCode.Tab:
                spriteRenderer.sprite = Sprites[29];
            break;


        }
    }
}
