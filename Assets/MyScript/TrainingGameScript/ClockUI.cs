using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockUI : MonoBehaviour
{


    private const float REAL_TIME_DAY = 120f;//~fで1日設定できるところ

    private Transform clockHourHandTransform;
    private Transform clockMinuteTransform;
    private Text clocktext;

    private float dayt;　//日数
    public static bool isTalk = true;
    public static int lasttime = 0;
    public static int dayttime = 0; //0朝　１昼　2夜

    private static int start_timeflug;//開始有無

    public MiniTalkScript miniTalkScript;
    public GameManagerScript gameManagerScript;


    void Awake(){
        clockHourHandTransform = transform.Find("clockHand");//時計の針のオブジェクトを取得
        clockMinuteTransform = transform.Find("minuteHand");//時計の針のオブジェクトを取得
        clocktext = transform.Find("ClockText").GetComponent<Text>();//テキスト取得

    }

    // Start is called before the first frame update
    void Start()
    {
        //start_timeflug = 0;

        if(ExecuteUtage.isClear == false){

            start_timeflug = 0;

        }else{

            start_timeflug = 1;

        }

        if(dayttime == 0){
            dayt　= 0.33f; // 午前8ジ/24ｆで割る
            gameManagerScript.mornings();

        }
        if(dayttime == 1){
            dayt　= 0.5f;
            gameManagerScript.noons();

        }
        if(dayttime == 2){
            dayt　= 0.8f;
            gameManagerScript.nights();

        }

        
        if(ExecuteUtage.isClear == false){

            Debug.Log("false");

        }else{

             Debug.Log("true");

        }


    }

    public void timeflug_on(){
        dayt　= 0.33f;
        start_timeflug = 0;
        lasttime = 0;
        
    }

    public void timeflug_off(){
        start_timeflug = 1;
        isTalk = false;

    }


    /*talkを見たか否か*/
    public void TodayTalkFlug(){
        isTalk = true;//朝

    }
    public void TodayTalkFlug2(){
        isTalk = false;//昼

    }

    /*ポップアップ*/
    public void timeflug00(){
        start_timeflug = 0;
    }


    // Update is called once per frame
    void Update()
    {
        

        if(start_timeflug == 1){

            dayt += Time.deltaTime / REAL_TIME_DAY *2;

        }else if(start_timeflug == 0){


        }

            float dayNormalized = dayt % 1f;
            float rotationDegreesPerDay = 360f;

            clockHourHandTransform.eulerAngles = new Vector3(0, 0, -dayNormalized * rotationDegreesPerDay *2);//eulerAngles=角度を取得

            float hoursPerDay = 12f;
            clockMinuteTransform.eulerAngles = new Vector3(0, 0, -dayNormalized * rotationDegreesPerDay * hoursPerDay *2);//eulerAngles=角度を取得



            string hoursString = Mathf.Floor(dayNormalized * hoursPerDay *2).ToString("00");

            float minutesPerHour = 60f;
            string minutesString = (((dayNormalized * hoursPerDay *2) % 1f) * minutesPerHour).ToString("00");

            clocktext.text = hoursString + ":" + minutesString;


        /*時間*/
        if(dayt > 0.33f && dayt < 0.5f){
            dayttime = 0;
        }
        if(dayt > 0.5f && dayt < 0.8f){
            dayttime = 1;
        }
        if(dayt > 0.8f && dayt < 1f){
            dayttime = 2;
        }



        /*朝昼晩　フラグ*/
        if(dayt > 0.34f && dayt < 0.4f){

            //午前中は
            if(isTalk == false){

                GameManagerScript.countlimit = 3;
                miniTalkScript.MorningTalkCO();

            }else{

            }

            gameManagerScript.mornings();



        }

        if(dayt > 0.5f && dayt < 0.6f){

            //12時以降は
            if(isTalk == true){

                if(GameManagerScript.countlimit > 3){
                    GameManagerScript.countlimit = 3;

                }else if(GameManagerScript.countlimit < 3){
                    GameManagerScript.countlimit = 3;
                }

                miniTalkScript.MorningTalkCO2();
                gameManagerScript.noons();
            }

        }

        if(dayt > 0.8f && dayt < 0.9f){
            lasttime = 1;//夜8じ以降を過ぎているか

            GameManagerScript.countlimit = 0;

            //20時以降は
            if(isTalk == false){
                miniTalkScript.MorningTalkCO3();
                gameManagerScript.nights();
            }

        }

        if(dayt > 1f　&& dayt < 1.1f){

            //20時以降は
            if(isTalk == true　&& GameManagerScript.textTalk == 0){
                miniTalkScript.MorningTalkCO4();
            }else{

            }
            gameManagerScript.nights();

        }


        if(GameManagerScript.money < 200){
            timeflug00();
        }
        if(GameManagerScript.heart < 200){
            timeflug00();
        }
        if(GameManagerScript.clean < 20){
            timeflug00();
        }
        if(GameManagerScript.hp < 20){
            timeflug00();
        }

        if(GameManagerScript.hp > 700 && GameManagerScript.heart > 700 && GameManagerScript.clean > 80){
            timeflug00();
        }

    }
}
