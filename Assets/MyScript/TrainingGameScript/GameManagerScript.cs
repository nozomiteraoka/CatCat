using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManagerScript : MonoBehaviour
{

    public MiniTalkScript miniTalkScript;

    public static string player_name;

    public static int money;
    public static int hp;
    public static int heart;
    public static int day;
    public static int clean;

    public Slider moneyslider;
    public Slider heartslider;
    public Slider hpslider;
    public Slider cleanslider;


    public TextMeshProUGUI Moneytext;
    public TextMeshProUGUI Hptext;
    public TextMeshProUGUI Hearttext;
    public TextMeshProUGUI Daytext;
    public TextMeshProUGUI Cleantext;

    /*room-clean*/
    public GameObject BG01;
    public GameObject BG02;
    public GameObject BG03;


    /*回数制限*/
    public static int countlimit = 3;//回数
    public TextMeshProUGUI Countlimittext;

    /*回数制限により無効化したいボタン*/
    public GameObject EatBtn;
    public GameObject TalkBtn;
    public GameObject WorkBtn;
    public GameObject CleanBtn;

    /*Iphonスクリーン*/
    public GameObject IphonScreen;


    /*Popup*/
    public GameObject PopUpAll;
    public GameObject MoneyPopup;
    public GameObject MentalPopup;
    public GameObject HpPopup;
    public GameObject CleanPopup;
    public GameObject ClearPopup;

    /*Tutorial*/
    public GameObject TutorialPopup;


    /*オーディオ*/
    public AudioSource audioSource;

    public static int textTalk = 0;//８時前に寝ているか否か



    // Start is called before the first frame update
    void Start()
    {

        //Debug.Log(player_name);

        hp = SelectScenario.utage_hp;
        money = SelectScenario.utage_money;
        heart = SelectScenario.utage_heart;
        day = SelectScenario.utage_day;
        clean = SelectScenario.utage_clean;

        player_name = SelectScenario.utage_player_name;
        Debug.Log(player_name);


        if(ExecuteUtage.isClear == false){

            Debug.Log("チュートリアル出す");
            TutorialPopup.SetActive (true);


        }else{

             //

        }

        Moneytext.text = money.ToString();
        Hptext.text = hp.ToString();
        Hearttext.text = heart.ToString();
        Daytext.text = day.ToString();
        Cleantext.text = clean.ToString();


        /*回数制限*/
        Countlimittext.text = countlimit.ToString();
        /*Iphon*/
        IphonScreen.SetActive (false);


        /*room-clean　はじめの部屋は通常です*/
        if(clean == 200){
            BG01.SetActive (true);
            BG02.SetActive (false);
            BG03.SetActive (false);
        }

        /*初期設定
        money = 1000;
        heart = 100;
        hp = 500;
        day = 1;*/

        /*スライダー設定*/
        moneyslider.value = money;
        heartslider.value = heart;
        hpslider.value = hp;
        cleanslider.value = clean;


        /*ポップアップ*/
        PopUpAll.SetActive (false);
        MoneyPopup.SetActive (false);
        MentalPopup.SetActive (false);
        HpPopup.SetActive (false);
        CleanPopup.SetActive (false);
        ClearPopup.SetActive (false);
        
    }

    /*Tutorialを非表示にする*/
    public void TutorialOff(){
        TutorialPopup.SetActive (false);

    }

    /*食べるボタンを押すと*/
    public void EatCount(){
        money -= 100;
        heart += 100;
        clean -= 10;
        hp += 100;

        countlimit　-= 1;


        audioSource.Play();
    }

    /*寝るボタンを押すと*/
    public void SleepCount(){
        heart += 100;
        hp += 100;

        /*回数制限復活*/
        if(countlimit > 2){
            countlimit　= 3;
        }else{
            countlimit　+= 1;
        }

        if(ClockUI.lasttime == 1){
            textTalk = 1;//８時前に寝るフラグ：過ぎていく
            miniTalkScript.MorningTalkCO5();
        }


        audioSource.Play();
    }

    /*掃除ボタンを押すと*/
    public void CleanCount(){
        clean += 10;
        heart -= 100;
        hp -= 100;

        countlimit　-= 1;

        audioSource.Play();
    }

    /*1日おわり*/
    public void TodayPlus(){
        day++;
        textTalk = 0;//８時前に寝るフラグ：リセットする
    }

    /*ボタンのONOFF　外部から*/
    public void ButtonOn(){
        IphonScreen.SetActive (true);

    }
    public void ButtonOff(){
        IphonScreen.SetActive (false);

    }


    // Update is called once per frame
    void Update()
    {

        Moneytext.text = money.ToString();
        Hptext.text = hp.ToString();
        Hearttext.text = heart.ToString();
        Daytext.text = day.ToString();
        Cleantext.text = clean.ToString();

        /*スライダー設定*/
        moneyslider.value = money;
        heartslider.value = heart;
        hpslider.value = hp;
        cleanslider.value = clean;

        /*回数制限*/
        Countlimittext.text = countlimit.ToString();

        /*掃除ー部屋表示*/
        if(clean > 80){
            BG01.SetActive (true);
            BG02.SetActive (false);
            BG03.SetActive (false);

        }
        if(clean < 50)
        {
            BG01.SetActive (false);
            BG02.SetActive (true);
            BG03.SetActive (false);
        }
        if(clean < 20)
        {
            BG01.SetActive (false);
            BG02.SetActive (false);
            BG03.SetActive (true);
        }


        /*もし回数制限が0になったら*/
        if(countlimit == 0){

           // ボタンを非活性に
            EatBtn.GetComponent<Button>().interactable = false;
            TalkBtn.GetComponent<Button>().interactable = false;
            WorkBtn.GetComponent<Button>().interactable = false;
            CleanBtn.GetComponent<Button>().interactable = false;
        }
        else if(countlimit > 0)
        {
            EatBtn.GetComponent<Button>().interactable = true;
            TalkBtn.GetComponent<Button>().interactable = true;
            WorkBtn.GetComponent<Button>().interactable = true;
            CleanBtn.GetComponent<Button>().interactable = true;
        }



        /*もし、0以下だったら = 0になる*/
        if(money < 0){
            money = 0;
        }
        else if(money > 900){
            money = 1000;
        }

        if(clean < 0){
            clean = 0;
        }else if(clean > 90){
            clean = 100;
        }

        if(hp < 0){
            hp = 0;
        }
        else if(hp > 900){
            hp = 1000;
        }

        if(heart < 0){
            heart = 0;
        }
        else if(heart > 900){
            heart = 1000;
        }

        if(day < 0){
            day = 0;
        }

        /*掃除ストッパ*/
        if(clean < -100){
            clean = -100;
        }else if(clean > 300){
            clean = 200;
        }


        /*ポップアップ*/
        if(money < 200){

            PopUpAll.SetActive (true);
            MoneyPopup.SetActive (true);

        }

        if(heart < 200){

            PopUpAll.SetActive (true);
            MentalPopup.SetActive (true);

        }


        if(clean < 20){

            PopUpAll.SetActive (true);
            CleanPopup.SetActive (true);

        }

        if(hp < 200){

            PopUpAll.SetActive (true);
            HpPopup.SetActive (true);

        }

        if(hp > 700 && heart > 700 && clean > 80){

            PopUpAll.SetActive (true);
            ClearPopup.SetActive (true);

        }

        
    }/*Update-End*/
}
