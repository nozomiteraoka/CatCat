using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialPopup : MonoBehaviour
{
    public const int t_popup1 = 1;
    public const int t_popup2 = 2;
    public const int t_popup3 = 3;
    public const int t_popup4 = 4;

    private int t_no;

    public GameObject t_popupImages;

    // Start is called before the first frame update
    void Start()
    {
        t_no ++;
    }

    public void PushButtonRight(){
        t_no++;
        if(t_no > t_popup4){
            t_no = t_popup1;
        }

        t_popupAll();
    }

    public void PushButtonLeft(){
        t_no--;
        if(t_no < t_popup1){
            t_no = t_popup4;
        }

        t_popupAll();
    }

    void t_popupAll(){
        switch(t_no){

            case t_popup1:
            t_popupImages.transform.localPosition = new Vector3(1920.0f, 0.0f, 0.0f);
            break;

            case t_popup2:
            t_popupImages.transform.localPosition = new Vector3(640.0f, 0.0f, 0.0f);
            break;

            case t_popup3:
            t_popupImages.transform.localPosition = new Vector3(-640.0f, 0.0f, 0.0f);
            break;

            case t_popup4:
            t_popupImages.transform.localPosition = new Vector3(-1920.0f, 0.0f, 0.0f);
            break;

        }
    }

    // Update is called once per frame
    void Update()
    {
                 
    }
}
