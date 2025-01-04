using UnityEngine;

[RequireComponent(typeof(ExecuteUtage))]
public class EventCharacterSelection : MonoBehaviour
{
    private ExecuteUtage ExecuteUtageComponent;

    public GuiAnimation guiAnimation;

    public string label;//labelの取得

    private void Start()
    {
        ExecuteUtageComponent = gameObject.GetComponent<ExecuteUtage>();
    }

    /*以前の記述　※消さないで！！！
    public void CharacterSelect(string label)
    {

        ExecuteUtageComponent.Execute("NeatsGame", label);
    }*/



    public void testen(){ //ここの関数名変えてね！！
        Debug.Log("1秒まってね！！！！！");
        guiAnimation.SaveButtontran();

        Invoke("CharacterSelect", 1);
    }



    public void CharacterSelect()
    {
        ExecuteUtageComponent.Execute("NeatsGame", label);
    }






}