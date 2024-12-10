using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManagerScript : MonoBehaviour
{
    public static int money = 1000;
    public static int hp = 100;
    public static int heart = 500;
    public static int day = 1;
    public static int clean = 200;

    public Slider moneyslider;
    public Slider heartslider;
    public Slider hpslider;


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
    public static int countlimit = 3;
    public TextMeshProUGUI Countlimittext;

    /*回数制限により無効化したいボタン*/
    public GameObject EatBtn;
    public GameObject WorkBtn;
    public GameObject CleanBtn;


    // Start is called before the first frame update
    void Start()
    {
        Moneytext.text = money.ToString();
        Hptext.text = hp.ToString();
        Hearttext.text = heart.ToString();
        Daytext.text = day.ToString();
        Cleantext.text = clean.ToString();

        /*回数制限*/
        Countlimittext.text = countlimit.ToString();


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
        
    }

    /*食べるボタンを押すと*/
    public void EatCount(){
        money -= 100;
        heart += 100;
        clean -= 100;
        hp += 100;

        countlimit　-= 1;
    }

    /*寝るボタンを押すと*/
    public void SleepCount(){
        heart += 100;
        hp += 100;
        day += 1;

        /*回数制限復活*/
        if(countlimit < 3){
            countlimit　= 3;
        }
    }

    /*掃除ボタンを押すと*/
    public void CleanCount(){
        clean += 100;
        heart -= 100;
        hp -= 100;


        countlimit　-= 1;
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

        /*回数制限*/
        Countlimittext.text = countlimit.ToString();

        /*掃除ー部屋表示*/
        if(clean == 200){
            BG01.SetActive (true);
            BG02.SetActive (false);
            BG03.SetActive (false);

        }
        if(clean < 100)
        {
            BG01.SetActive (false);
            BG02.SetActive (true);
            BG03.SetActive (false);
        }
        if(clean < 0)
        {
            BG01.SetActive (false);
            BG02.SetActive (false);
            BG03.SetActive (true);
        }


        /*もし回数制限が0になったら*/
        if(countlimit == 0){

           // ボタンを非活性に
            EatBtn.GetComponent<Button>().interactable = false;
            WorkBtn.GetComponent<Button>().interactable = false;
            CleanBtn.GetComponent<Button>().interactable = false;
        }
        else if(countlimit > 0)
        {
            EatBtn.GetComponent<Button>().interactable = true;
            WorkBtn.GetComponent<Button>().interactable = true;
            CleanBtn.GetComponent<Button>().interactable = true;
        }



        /*もし、0以下だったら = 0になる*/
        if(money < 0){
            money = 0;
        }
        else if(money > 1000){
            money = 1000;
        }

        if(hp < 0){
            hp = 0;
        }
        else if(hp > 1000){
            hp = 1000;
        }

        if(heart < 0){
            heart = 0;
        }
        else if(heart > 1000){
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

        
    }
}
