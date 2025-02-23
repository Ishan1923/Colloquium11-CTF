using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButtonDeactivating : MonoBehaviour
{
    public GameObject pauseButton,winPanel;
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        if(winPanel.activeInHierarchy)
        {
            pauseButton.SetActive(false);
        }

    }
}
