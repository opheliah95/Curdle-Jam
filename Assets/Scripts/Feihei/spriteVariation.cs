using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spriteVariation : MonoBehaviour
{

    public Sprite[] sprites;
    // Start is called before the first frame update
    void Awake()
    {
        GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];
    }


}
