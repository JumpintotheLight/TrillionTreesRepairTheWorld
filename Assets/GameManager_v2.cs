using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum tree_types_v2
{
    Seed1 = 0,
    Seed2 = 1,
    Seed3 = 2,
    Seed4 = 3,

}


public class GameManager_v2 : MonoBehaviour
{
    private static GameManager_v2 _instance;

    //public GameObject prefab; 
    public int treeTotal = 0; //tree_types.Spruce + tree_types.Fur;
    public float percent = 0f;

    public GameObject Timer;
    public GameObject Month;
    public GameObject YearGO;

    public GameObject WarningFire;
    public GameObject WarningDiversity;
    public GameObject WarningTornado;
    public GameObject WarningWater;


    public Text SeedStatsText1;
    public Text SeedStatsText2;
    public Text SeedStatsText3;
    public Text SeedStatsText4;

    public int SeedStats1 = 0;
    public int SeedStats2 = 0;
    public int SeedStats3 = 0;
    public int SeedStats4 = 0;

    public Text PlagueStats;
    public Text FireStats;
    public Text TornadoStats;
    public Text SunStats;



    private Text timerText;
    private Text monthText;
    private Text yearText;

    public string currentTime;

    public int totalObjects = 3;
    public int minT = 2;
    public int maxT = 10;
    public int calculatedTime;

    public static GameManager_v2 Instance { get { return _instance; } }

    //public List<tree_types> treeCounter;
    public Dictionary<tree_types_v2, int> treeDictionary;
    public string[] Months = new string[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sept", "Oct", "Nov", "Dec" };
    public int Year = 2020;


    // TreePainter will broadcast and GameManager will listen. 


    public delegate void OnButtonClick();
    public static event OnButtonClick onButtonClick;

    public void BroadcastOnButtonClick()
    {
        if (onButtonClick != null)
        {
            onButtonClick();

        }
    }

    public void UpdateScore1()
    {
        SeedStats1++;
        SeedStatsText1.text = SeedStats1.ToString(); 
    }

    public void UpdateScore2()
    {
        SeedStats2++;
        SeedStatsText2.text = SeedStats2.ToString();
    }

    public void UpdateScore3()
    {
        SeedStats3++;
        SeedStatsText3.text = SeedStats3.ToString();
    }

    public void UpdateScore4()
    {
        SeedStats4++;
        SeedStatsText4.text = SeedStats4.ToString();
    }

    private void Start()
    {

        TreePainter.onAddSeed1 += UpdateScore1;
        TreePainter.onAddSeed2 += UpdateScore2;
        TreePainter.onAddSeed3 += UpdateScore3;
        TreePainter.onAddSeed4 += UpdateScore4;




        Invoke("spawnOB", 4);
        Debug.Log("Launched");
    }
    void spawnOB()
    {
        CancelInvoke(); // Stop the timer (I don't think you need it, try without)

          int randomNum = Random.Range(0, totalObjects);
        //Object objTemp = Resources.Load("Assets/" + randomNum);
       // Instantiate(objTemp);
        //Instantiate(prefab, new Vector3(Random.Range(-5, 5), 0f, Random.Range(-5, 5)), Quaternion.identity);
        // Start a new timer for the next random spawn
        Invoke("spawnOB",Random.Range(2f, 4f));
        Debug.Log("Random Tree appeared");
    } 
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        timerText = Timer.GetComponent<Text>();
        monthText = Month.GetComponent<Text>();
        yearText = YearGO.GetComponent<Text>();

        if (timerText == null) Debug.Log("Error!");

        treeDictionary = new Dictionary<tree_types_v2, int>();
        treeDictionary.Add(tree_types_v2.Seed1, 0);
        treeDictionary.Add(tree_types_v2.Seed2, 0);
        treeDictionary.Add(tree_types_v2.Seed3, 0);
        treeDictionary.Add(tree_types_v2.Seed4, 0);


    }

    private void Update()
    {
        UpdateTime();
        calculatedTime = Random.Range(minT, maxT);

        // Obtain the current time.
        //currentTime = Time.time.ToString("0:00");
        //currentTime = "Time is: " + currentTime + " sec.";
        string month = "Jan";
        string year = "2020";

        int minutes = Mathf.FloorToInt(Time.time / 60);

        int seconds = Mathf.FloorToInt(Time.time % 60);

        //Debug.Log("Minutes : Seconds " + minutes + " " + seconds);

        int monthIndex = (int)seconds / 5;
        if (monthIndex >= 0 && monthIndex < Months.Length)
        {
            month = Months[monthIndex];
            year = (minutes + 2020 ).ToString();
        }
        //timerText.text = month + " " +  year; 
        timerText.text = "Time : " + currentTime;
        monthText.text = "Month : " +  month;
        yearText.text = "Year : " + year; 

       // if(seconds > Random.Range())

    }
    private void UpdateTime()
    {
        int minutes = Mathf.FloorToInt(Time.time / 60);

        int seconds = Mathf.FloorToInt(Time.time % 60);


        currentTime = string.Format("{0:0}:{1:00}", minutes, seconds);
    }

    public void AddSeed1()
    {
        
        AddTreeOfType(tree_types_v2.Seed1);
    }
    public void AddSeed2()
    {
        AddTreeOfType(tree_types_v2.Seed2);
    }
    public void AddSeed3()
    {
        AddTreeOfType(tree_types_v2.Seed3);
    }
    public void AddSeed4()
    {
        AddTreeOfType(tree_types_v2.Seed4);
    }

    public void AddTreeOfType(tree_types_v2 treetype)
    {
        treeDictionary[treetype] += 1;
        Debug.Log("Number of trees of type : " + treetype + " is " + treeDictionary[treetype]);

        UpdateTotalTrees();
        UpdateTreePercent(treetype);
    }
    public void UpdateTotalTrees()
    {
        treeTotal = treeDictionary[tree_types_v2.Seed1] + treeDictionary[tree_types_v2.Seed2] + treeDictionary[tree_types_v2.Seed3] + +treeDictionary[tree_types_v2.Seed4];
        Debug.Log("Total Number of trees: " + treeTotal);
    }

    public void UpdateTreePercent(tree_types_v2 treetype)
    {
            percent = (float) treeDictionary[treetype] / treeTotal * 100;
            Debug.Log("Percentage of " + treetype + " of total:"  + percent + "%");

        float seed1percent;
        float seed2percent;
        float seed3percent;
        float seed4percent;


        if (treetype == tree_types_v2.Seed1)
        {
            seed1percent = percent;
        }
       else if (treetype == tree_types_v2.Seed2)
        {
            seed2percent = percent; 
        }
        else if (treetype == tree_types_v2.Seed3)
        {
            seed3percent = percent;
        }
        else if (treetype == tree_types_v2.Seed4)
        {
            seed4percent = percent;
        }
        // float grass = percent;

        if (percent >= 30.0f && treeTotal > 5)
        {
            Debug.Log("You have planted too many " + treetype + " Trees!");
            // Invoke Warning Here about diversity!!
            WarningDiversity.SetActive(true);
        }

    }






}