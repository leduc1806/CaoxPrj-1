using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;

public class NormBlock : MonoBehaviour
{
    // Normal Components

    // Variables for data container from Scriptable Object
    [SerializeField] private NormalBlock data;
    [SerializeField] private bool cur_status;
    [SerializeField] private bool init_status;

    // For interacting with main block
    [SerializeField] private GameEvent mainBlockGE;
    [SerializeField] private List<GameObject> allChildren;


    // Start is called before the first frame update
    void Awake()
    {
        // Load children to allChildren, so they can interact with main block
        foreach (Transform child in transform)
        {
            allChildren.Add(child.gameObject);
        }

        LoadDataFromScriptableObject(data); // Load data from SO

        // Set init state with init_status from SO
        if (init_status == false)
        {
            foreach (GameObject child in allChildren)
            {
                child.SetActive(false);
            }
        }
        else
        {
            foreach (GameObject child in allChildren)
            {
                child.SetActive(true);
            }
        }
    }

    private void OnEnable()
    {
        mainBlockGE.AddListener(ChangeStatus); // Listener to interact with main block
    }

    private void OnDisable()
    {
        mainBlockGE.RemoveListener(ChangeStatus);
    }

    void LoadDataFromScriptableObject(NormalBlock _data)
    {
        cur_status = _data.curStatus;
        init_status = _data.initStatus;
    }

    // Change state of children when interact with main block, listen by mainBlockGE
    void ChangeStatus()
    {
        cur_status = !cur_status;
        if (cur_status)
        {
            foreach (GameObject child in allChildren)
            {
                child.SetActive(true);
            }
        }
        else
        {
            foreach (GameObject child in allChildren)
            {
                child.SetActive(false);
            }
        }
    }
}
