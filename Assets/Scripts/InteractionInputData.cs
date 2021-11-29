using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "InteractionInputData", menuName = "InteractionSystem/InputData")]
public class InteractionInputData : ScriptableObject{
    private bool m_interactClicked;
    private bool m_interactRelease;

    public bool InteractClicked{
        get => m_interactClicked;
        set => m_interactClicked = value;
    }

    public bool InteractRelease{
        get => m_interactRelease;
        set => m_interactRelease = value;
    }

    public void Reset(){
        m_interactClicked = false;
        m_interactRelease = false;
    }
}
