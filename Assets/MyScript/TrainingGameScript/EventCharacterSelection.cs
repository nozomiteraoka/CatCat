using UnityEngine;

[RequireComponent(typeof(ExecuteUtage))]
public class EventCharacterSelection : MonoBehaviour
{
    private ExecuteUtage ExecuteUtageComponent;

    private void Start()
    {
        ExecuteUtageComponent = gameObject.GetComponent<ExecuteUtage>();
    }

    public void CharacterSelect(string label)
    {

        ExecuteUtageComponent.Execute("NeatsGame", label);
    }





}