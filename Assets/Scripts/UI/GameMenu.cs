using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenu : MonoBehaviour
{
    [SerializeField] GameObject panel;
    SelectCharacter selectCharacter;
    private void Awake()
    {
        selectCharacter = GetComponent<SelectCharacter>();
    }

    private void Update()
    {   
        if(selectCharacter.enabled == false) {return;}
        if(Input.GetMouseButtonDown(1)){
            panel.SetActive(true);
        }    
    }
}
