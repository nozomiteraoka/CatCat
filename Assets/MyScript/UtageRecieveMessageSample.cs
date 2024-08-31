using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Utage;
using TMPro;

/// <summary>
/// ADV用SendMessageコマンドから送られたメッセージを受け取る処理のサンプル
/// </summary>
[AddComponentMenu("Utage/ADV/Examples/UtageRecieveMessageSample")]
public class UtageRecieveMessageSample : MonoBehaviour
{
    public AdvEngine engine;            //Advエンジン本体
    public TMP_InputField inputFiled;       //テキスト入力用のオブジェクト
    public TextMeshProUGUI NameExplanation; //名前入力してくださいの説明
    public string scenarioLabel;

    void Awake()
    {
        inputFiled.gameObject.SetActive(false);
    }

    //SendMessageコマンドが実行されたタイミング
    void OnDoCommand(AdvCommandSendMessage command)
    {
        switch (command.Name)
        {
            case "DebugLog":
                DebugLog(command);
                break;
            case "InputFileld":
                InputFileld(command);
                break;
            default:
                Debug.Log("Unknown Message:" + command.Name );
                break;
        }
    }

    //SendMessageコマンドの処理待ちタイミング
    void OnWait(AdvCommandSendMessage command)
    {
        switch (command.Name)
        {
            case "InputFileld":
                //インプットフィールドが有効な間は待機
                command.IsWait = inputFiled.gameObject.activeSelf;
                break;
            default:
                command.IsWait = false;
                break;
        }
    }

    //デバッグログを出力
    void DebugLog(AdvCommandSendMessage command)
    {
        Debug.Log(command.Arg2);
    }

    //設定された入力フィールドを有効化
    void InputFileld(AdvCommandSendMessage command)
    {
        //NameExplanation
        NameExplanation.text =　"名前を入力ください";


        //テキスト入力用のScript
        inputFiled.gameObject.SetActive(true);
        inputFiled.onEndEdit.RemoveAllListeners();
        inputFiled.onEndEdit.AddListener((string text) => OnEndEditInputFiled(command.Arg2, text));
    }

    //入力終了。入力されたテキストを宴のパラメーターに設定する
    void OnEndEditInputFiled(string paramName, string text)
    {
        if (!engine.Param.TrySetParameter(paramName, text))
        {
            Debug.LogError(paramName + "is not found");
        }
        inputFiled.gameObject.SetActive(false);
    }


    public void OKButton()
    {
        StartCoroutine( CoTalk() );

    }

    //Name_Outというラベルに飛ぶ
    IEnumerator CoTalk()
    {
        //「宴」のシナリオを呼び出す
        engine.JumpScenario( scenarioLabel );

        //「宴」のシナリオ終了待ち
        while(!engine.IsEndScenario)
        {
            yield return null;
        }
    }
}
