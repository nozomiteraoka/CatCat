using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ExecuteUtage : MonoBehaviour
{
    //ここのシーンを通過したか否か　false
    public static bool isClear = false;

    // 他シーンのUtage呼び出し
    public void Execute(string utageSceneName, string label)
    {
        // 実行するラベル名を保存
        ExecuteScenario.Label = label;

        StartCoroutine(ExecuteScenarioCoroutine(utageSceneName));
    }

    // シーン読み込みが完了したか
    private bool IsSceneLoaded;

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // シーン読み込み完了イベント
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        IsSceneLoaded = true;
    }

    // シナリオの呼び出しCorotine
    private IEnumerator ExecuteScenarioCoroutine(string utageSceneName)
    {
        isClear = true;　//ここのシーンを通過したか否か　true

        IsSceneLoaded = false;
        SceneManager.LoadScene(utageSceneName);
        yield return new WaitUntil(() => IsSceneLoaded);
    }
}