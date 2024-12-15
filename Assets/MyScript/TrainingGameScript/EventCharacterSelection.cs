using UnityEngine;

[RequireComponent(typeof(ExecuteUtage))]
public class EventCharacterSelection : MonoBehaviour
{
    private ExecuteUtage ExecuteUtageComponent;

    public GuiAnimation guiAnimation;//

    private void Start()
    {
        ExecuteUtageComponent = gameObject.GetComponent<ExecuteUtage>();
    }

    public void CharacterSelect(string label)
    {
        guiAnimation.SaveButtontran();

        ExecuteUtageComponent.Execute("NeatsGame", label);
    }





}