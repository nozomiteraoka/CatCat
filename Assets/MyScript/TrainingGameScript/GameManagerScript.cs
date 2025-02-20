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

    /*hp_icon*/
    public GameObject hp_icon_01;
    public GameObject hp_icon_02;
    public GameObject hp_icon_03;
    public GameObject hp_icon_04;
    /*hp_color*/
    public Image hp_pram;

    /*room-clean*/
    public GameObject BG01;
    public GameObject BG02;
    public GameObject BG03;

    /*朝・昼・晩*/
    public GameObject morning;
    public GameObject noon;
    public GameObject night;


    /*回数制限-Image*/
    //public int countlth;
    public int countlOfcount;

    public Image[] countss;
    public Sprite fullcount;
    public Sprite emptycount;


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

    public GameObject ADVPopup;

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
        if(SelectScenario.utage_clean > 60 && SelectScenario.utage_clean == 60){
            BG01.SetActive (true);
            BG02.SetActive (false);
            BG03.SetActive (false);

        }
        if(SelectScenario.utage_clean > 40 && SelectScenario.utage_clean < 60 && SelectScenario.utage_clean == 40)
        {
            BG01.SetActive (false);
            BG02.SetActive (true);
            BG03.SetActive (false);
        }
        if(SelectScenario.utage_clean > 0 && SelectScenario.utage_clean < 40)
        {
            BG01.SetActive (false);
            BG02.SetActive (false);
            BG03.SetActive (true);
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

        ADVPopup.SetActive (true);


        /*hp_icon設定*/
        if(hp > 90){
            hp_icon_01.SetActive (true);
            hp_icon_02.SetActive (false);
            hp_icon_03.SetActive (false);
            hp_icon_04.SetActive (false);

            hp_pram.color = new Color32 (130, 246, 152, 255);
        }
        else if(hp > 40 && hp < 90){
            hp_icon_02.SetActive (true);
            hp_icon_01.SetActive (false);
            hp_icon_03.SetActive (false);
            hp_icon_04.SetActive (false);

            hp_pram.color = new Color32 (252, 225, 131, 255);
        }
        else if(hp > 30 && hp < 40){
            hp_icon_03.SetActive (true);
            hp_icon_01.SetActive (false);
            hp_icon_02.SetActive (false);
            hp_icon_04.SetActive (false);

            hp_pram.color = new Color32 (189, 237, 244, 255);
        }
        else if(hp > 0 && hp < 30){
            hp_icon_04.SetActive (true);
            hp_icon_01.SetActive (false);
            hp_icon_02.SetActive (false);
            hp_icon_03.SetActive (false);

            hp_pram.color = new Color32 (239, 145, 152, 255);
        }
        
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
        hp += 10;

        countlimit　-= 1;


        audioSource.Play();
    }

    /*寝るボタンを押すと*/
    public void SleepCount(){
        heart += 100;
        hp += 10;

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
        hp -= 10;

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

    /*朝・昼・晩*/
    public void mornings(){
        morning.SetActive (true);
        noon.SetActive (false);
        night.SetActive (false);

    }
    public void noons(){
        noon.SetActive (true);
        morning.SetActive (false);
        night.SetActive (false);

    }
    public void nights(){
        night.SetActive (true);
        morning.SetActive (false);
        noon.SetActive (false);

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

        /*回数制限-Image*/
        for(int i = 0; i < countss.Length; i++){

            if(countlimit > countlOfcount){
                countlimit = countlOfcount;
            }

            if(i < countlimit){
                countss[i].sprite = fullcount;
            }else{
                countss[i].sprite = emptycount;
            }

            if(i < countlOfcount){
                countss[i].enabled = true;
            }else{
                countss[i].enabled = false;
            }

        }

        /*回数制限*/
        Countlimittext.text = countlimit.ToString();

        /*掃除ー部屋表示*/
        if(clean > 70){
            BG01.SetActive (true);
            BG02.SetActive (false);
            BG03.SetActive (false);

        }
        if(clean > 40 && clean < 70)
        {
            BG01.SetActive (false);
            BG02.SetActive (true);
            BG03.SetActive (false);
        }
        if(clean > 0 && clean < 40)
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
        else if(hp > 90){
            hp = 100;
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


        /*ポップアップ*/
        if(money < 200){

            PopUpAll.SetActive (true);
            MoneyPopup.SetActive (true);

            ADVPopup.SetActive (false);

        }

        if(heart < 200){

            PopUpAll.SetActive (true);
            MentalPopup.SetActive (true);

            ADVPopup.SetActive (false);

        }


        if(clean < 20){

            PopUpAll.SetActive (true);
            CleanPopup.SetActive (true);

            ADVPopup.SetActive (false);

        }

        if(hp < 20){

            PopUpAll.SetActive (true);
            HpPopup.SetActive (true);

            ADVPopup.SetActive (false);

        }

        if(hp > 70 && heart > 700 && clean > 80){

            PopUpAll.SetActive (true);
            ClearPopup.SetActive (true);

            ADVPopup.SetActive (false);

        }


        /*hp_icon設定*/
        if(hp > 90){
            hp_icon_01.SetActive (true);
            hp_icon_02.SetActive (false);
            hp_icon_03.SetActive (false);
            hp_icon_04.SetActive (false);

            hp_pram.color = new Color32 (130, 246, 152, 255);
        }
        else if(hp > 40 && hp < 90){
            hp_icon_02.SetActive (true);
            hp_icon_01.SetActive (false);
            hp_icon_03.SetActive (false);
            hp_icon_04.SetActive (false);

            hp_pram.color = new Color32 (252, 225, 131, 255);
        }
        else if(hp > 30 && hp < 40){
            hp_icon_03.SetActive (true);
            hp_icon_01.SetActive (false);
            hp_icon_02.SetActive (false);
            hp_icon_04.SetActive (false);

            hp_pram.color = new Color32 (189, 237, 244, 255);
        }
        else if(hp > 0 && hp < 30){
            hp_icon_04.SetActive (true);
            hp_icon_01.SetActive (false);
            hp_icon_02.SetActive (false);
            hp_icon_03.SetActive (false);

            hp_pram.color = new Color32 (239, 145, 152, 255);
        }

        
    }/*Update-End*/
}
