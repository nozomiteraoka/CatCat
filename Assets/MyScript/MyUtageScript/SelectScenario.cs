using System.Collections;
using UnityEngine;
using Utage;


public class SelectScenario : MonoBehaviour
{
    [SerializeField] public AdvEngine Engine;
    [SerializeField] public UtageUguiTitle Title;
    [SerializeField] public UtageUguiMainGame MainGame;


    public static int utage_hp;
    public static int utage_money;
    public static int utage_heart;
    public static int utage_day;
    public static int utage_clean;

    public static string utage_player_name;



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

        Engine.Param.SetParameterInt("dayttime",ClockUI.dayttime);

        Engine.Param.SetParameterString("player_name",GameManagerScript.player_name);//name

    }

    //パラメーターを取得する ※宴のParameterを　外部というか飛ばしているstatic変数数字加える
    public void LoadCount(){

        Debug.Log("どうゆうこと");
        utage_hp = Engine.Param.GetParameterInt("hp");
        utage_money = Engine.Param.GetParameterInt("money");
        utage_heart = Engine.Param.GetParameterInt("heart");
        utage_day = Engine.Param.GetParameterInt("day");
        utage_clean = Engine.Param.GetParameterInt("clean");

        utage_player_name = Engine.Param.GetParameterString("player_name");


    }




    //セーブシーンに飛ばす
    public void SaveTransition(){

        if(ExecuteUtage.isClear == true){

            MainGame.OnTapSave();


        }else{
            
            Debug.Log("セーブしてません");

        }
    }

    public void Endflug(){

        ExecuteUtage.isClear = false;
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