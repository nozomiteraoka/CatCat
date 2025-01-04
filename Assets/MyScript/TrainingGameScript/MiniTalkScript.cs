using System;
using System.Collections;
using UnityEngine;
using Utage;
using UtageExtensions;

    public class MiniTalkScript : MonoBehaviour
    {
        // ADVエンジン
        public AdvEngine AdvEngine { get { return advEngine; } }
        [SerializeField]
        protected AdvEngine advEngine;



        public string ScenarioLabel_01;

        public string Moneytalk_down;
        public string Hptalk_down;
        public string Mentaltalk_down;
        public string Cleantalk_down;

        public string Moneytalk_up;
        public string Hptalk_up;
        public string Mentaltalk_up;
        public string Cleantalk_up;

        public string AnotherTalk;

        public string Morning_talk;
        public string Noon_talk;
        public string Night_talk;

        public string Last_talk;
        public string Second_talk;

        public string Support_talk;




        public void JumpADV(){

            //GameManagerScript.countlimit　-= 1;


            if(GameManagerScript.money < 300){

                JumpScenario(Moneytalk_down);

            }

            else if(GameManagerScript.hp < 300){

                JumpScenario(Hptalk_down);

            }

            else if(GameManagerScript.heart < 300){

                JumpScenario(Mentaltalk_down);

            }

            else if(GameManagerScript.clean < 0){

                JumpScenario(Cleantalk_down);

            }

            //

            else if(GameManagerScript.money > 800){

                JumpScenario(Moneytalk_up);
            }

            else if(GameManagerScript.hp > 800){

                JumpScenario(Hptalk_up);
            }

            else if(GameManagerScript.heart > 800){

                JumpScenario(Mentaltalk_up);
            }

            else if(GameManagerScript.clean > 100){

                JumpScenario(Cleantalk_up);
            }

            else {

                JumpScenario(AnotherTalk);
            }

            //JumpScenario(AnotherTalk);


        }


        void Start(){



            /*
            if(ExecuteUtage.isClear == false){

                JumpScenario(ScenarioLabel_01);

            }else{

                 Debug.Log("どないしよう");

            }*/
        }
 

        public void MorningTalkCO(){

            JumpScenario(Morning_talk);

        }

        public void MorningTalkCO2(){

            JumpScenario(Noon_talk);

        }

        public void MorningTalkCO3(){

            JumpScenario(Night_talk);

        }

        public void MorningTalkCO4(){

            JumpScenario(Last_talk);

        }

        public void MorningTalkCO5(){

            JumpScenario(Second_talk);

        }

        public void MorningTalkCO6(){

            JumpScenario(Support_talk);

        }








        //再生中かどうか
        public bool IsPlaying { get; private set; }

        float defaultSpeed = -1;

        //指定のラベルのシナリオを再生する
        public void JumpScenario(string label)
        {
            StartCoroutine(JumpScenarioAsync(label, null));
        }

        //指定のラベルのシナリオを再生する
        //終了した時にonCompleteが呼ばれる
        public void JumpScenario(string label, Action onComplete)
        {
            StartCoroutine(JumpScenarioAsync(label, onComplete));
        }

        IEnumerator JumpScenarioAsync(string label, Action onComplete)
        {
            IsPlaying = true;
            AdvEngine.JumpScenario(label);
            while (!AdvEngine.IsEndOrPauseScenario)
            {
                yield return null;
            }
            IsPlaying = false;
            if(onComplete !=null) onComplete();
        }

        //指定のラベルのシナリオを再生する
        //ラベルがなかった場合を想定
        public void JumpScenario(string label, Action onComplete, Action onFailed)
        {
            JumpScenario(label, null, onComplete, onFailed);
        }

        //指定のラベルのシナリオを再生する
        //ラベルがなかった場合を想定
        public void JumpScenario(string label, Action onStart, Action onComplete, Action onFailed)
        {
            if (string.IsNullOrEmpty(label))
            {
                if(onFailed!=null)onFailed();
                Debug.LogErrorFormat("シナリオラベルが空です");
                return;
            }
            if (label[0] == '*')
            {
                label = label.Substring(1);
            }
            if (AdvEngine.DataManager.FindScenarioData(label) == null)
            {
                if(onFailed!=null)onFailed();
                Debug.LogErrorFormat("{0}はまだロードされていないか、存在しないシナリオです", label);
                return;
            }

            if (onStart != null) onStart();
            JumpScenario(
                label,
                onComplete);
        }

        //シナリオの呼び出し以外に、
        //AdvEngineを操作する処理をまとめておくと、便利
        //何が必要かはプロジェクトによるので、場合によって増やしていく

        //以下、メッセージウィンドのテキスト表示速度を操作する処理のサンプル

        //メッセージウィンドのテキスト表示の速度を指定のスピードに
        public void ChangeMessageSpeed( float  speed)
        {
            if (defaultSpeed < 0 )
            {
                defaultSpeed = AdvEngine.Config.MessageSpeed;
            }
            AdvEngine.Config.MessageSpeed = speed;
        }
        //メッセージウィンドのテキスト表示の速度を元に戻す
        public void ResetMessageSpeed()
        {
            if (defaultSpeed >= 0)
            {
                AdvEngine.Config.MessageSpeed = defaultSpeed;
            }
        }
    }