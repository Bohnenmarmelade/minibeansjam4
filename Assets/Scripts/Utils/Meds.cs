using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Meds
{

   


    public static string[] SUFFIX =  {
        "al", "el", "il", "ol", "ul",
        "an", "en", "in", "on", "un",
        "astin", "azepam", "azin", "azol",
        "cain", "cillin", "coxib", "cyclin",
        "dipin", "fibrat", "floxacin", "kiren", "mab",
        "mycin", "nitrat", "oxetin", "pramin", "prazol",
        "pril", "sartan", "xetin", "zepam"
    };

    public static string[] EASY =  {
        "Riz","Aspiz","Param","Gun","Ber","Maz"
    };

    public static string[] MEDIUM =  {
        "Abroloz","Bananoz","Cholister", "Banana"
    };

    public static string[] HARD =  {
        "Adrianotez","Macrogolcetylstear"
    };


    public static string getMed(string difficulty){
        int randSuffix = Random.Range(0, Meds.SUFFIX.Length);
        string suffix = Meds.SUFFIX[randSuffix];

        string prefix = "";
        if(difficulty.Equals(Difficulty.EASY)){
            prefix = Meds.EASY[Random.Range(0, Meds.EASY.Length)];
        } else if(difficulty.Equals(Difficulty.MEDIUM)){
            prefix = Meds.MEDIUM[Random.Range(0, Meds.MEDIUM.Length)];
        } else {
            prefix = Meds.HARD[Random.Range(0, Meds.HARD.Length)];
        }

        return prefix + suffix;

    }
};
