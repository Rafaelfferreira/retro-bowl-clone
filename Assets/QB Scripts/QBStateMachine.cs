using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QBStateMachine : MonoBehaviour
{
    // MARK: - States
    #region States
    public PlayerPreSnapState preSnapState { get; private set; }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        preSnapState = new PlayerPreSnapState("preSnap");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
