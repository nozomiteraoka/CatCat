using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaAnimation : MonoBehaviour
{

    private Animator anim;
    public GameManagerScript gameManagerScript;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        
    }

    /*食べる*/
    public void OnEatClick(){
        anim.SetBool("EatBool", true);
        gameManagerScript.ButtonOn();
        Invoke("EatAnim" ,2);
    }

    void EatAnim(){
        anim.SetBool("EatBool", false);
        gameManagerScript.ButtonOff();
        gameManagerScript.EatCount();

    }/*食べる-end*/


    /*掃除する*/
    public void OnCleanClick(){
        anim.SetBool("CleenBool", true);
        gameManagerScript.ButtonOn();
        Invoke("CleanAnim" ,2);
    }

    void CleanAnim(){
        anim.SetBool("CleenBool", false);
        gameManagerScript.ButtonOff();
        gameManagerScript.CleanCount();

    }/*掃除-end*/


    /*寝る*/
    public void OnSleepClick(){
        anim.SetBool("SleepBool", true);
        gameManagerScript.ButtonOn();
        Invoke("SleepAnim" ,2);
    }

    void SleepAnim(){
        anim.SetBool("SleepBool", false);
        gameManagerScript.ButtonOff();
        gameManagerScript.SleepCount();

    }/*寝る-end*/



    // Update is called once per frame
    void Update()
    {
        
    }
}
