using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;

public class MainBlock : MonoBehaviour
{
    [SerializeField] GameEvent mainBlockGE;

    // Start is called before the first frame update
    void Start()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            mainBlockGE.Raise();
        }
    }
}
