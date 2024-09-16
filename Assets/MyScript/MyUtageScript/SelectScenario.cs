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


    //型指定済みの設定方法　※宴のパラメーターに加える
    public void SaveCount(){

        Engine.Param.SetParameterInt("money",GameManagerScript.money);
        Engine.Param.SetParameterInt("hp",GameManagerScript.hp);
        Engine.Param.SetParameterInt("heart",GameManagerScript.heart);
        Engine.Param.SetParameterInt("day",GameManagerScript.day);
        Engine.Param.SetParameterInt("clean",GameManagerScript.clean);
        Engine.Param.SetParameterInt("countlimit",GameManagerScript.countlimit);

    }

    //パラメーターを取得する
    public void LoadCount(){

        if(ExecuteUtage.isClear == true){

            //ボタンを介していたら取得しない
            Debug.Log(GameManagerScript.hp);

        }else{
            //パラメーターの取得　※宴から取ってくるときはこちら
            GameManagerScript.money = Engine.Param.GetParameterInt("money");
            GameManagerScript.hp = Engine.Param.GetParameterInt("hp");
            GameManagerScript.heart = Engine.Param.GetParameterInt("heart");
            GameManagerScript.day = Engine.Param.GetParameterInt("day");
            GameManagerScript.clean = Engine.Param.GetParameterInt("clean");
            GameManagerScript.countlimit = Engine.Param.GetParameterInt("countlimit");

        }
    }




    //セーブシーンに飛ばす
    public void SaveTransition(){

        if(ExecuteUtage.isClear == true){

            MainGame.OnTapSave();


        }else{
            
            Debug.Log("セーブしてません");

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