using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public BatEngine BatEngine;
    void Start()
    {
        if (Manager.MapNumber != 0)
        {
            BatEngine.enabled = true;
            gameObject.SetActive(false);
        }
    }
}
