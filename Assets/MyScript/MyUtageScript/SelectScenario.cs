using System.Collections;
using UnityEngine;
using Utage;


public class SelectScenario : MonoBehaviour
{
    [SerializeField] public AdvEngine Engine;
    [SerializeField] public UtageUguiTitle Title;
    [SerializeField] public UtageUguiMainGame MainGame;



    private　void Start() {
        
        //育成ゲームを通過していればシーンラベルを飛ばすのを実行する
        if(ExecuteUtage.isClear == true){
            
            StartCoroutine(Execute());
            Title.Close();

        }else{
            
            Debug.Log("育成ゲームを通過していないよ");
        }

    }


    // シナリオを呼び出す
    private IEnumerator Execute()
    {
        // Utage Engine の起動を待つ
        yield return new WaitUntil(() => !Engine.IsLoading);
        // 機能呼び出し
        yield return StartCoroutine(ExecuteLabel());
    }

    // 機能呼び出し
    private IEnumerator ExecuteLabel()
    {
        // 起動ラベルのラベル名を取得
        string label = ExecuteScenario.Label;

        // 機能呼び出し
        Title.Close();
        MainGame.OpenStartLabel(label);

        yield return null;
    }
}