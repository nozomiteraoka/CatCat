using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuiAnimation : MonoBehaviour
{

    private Animation BlackAinm;

    // Start is called before the first frame update
    void Start()
    {
        BlackAinm = gameObject.GetComponent<Animation>();
        BlackInAnimation(BlackAinm);
        
    }

    void BlackInAnimation (Animation BlackAinm)
    {
        BlackAinm.Play("BlackIn");
    }

    public void SaveButtontran(){
        BlackOutAnimation(BlackAinm);
    }

    void BlackOutAnimation (Animation BlackAinm)
    {
        BlackAinm.Play("BlackOut");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
