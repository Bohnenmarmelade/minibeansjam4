using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Meds
{

   


    public static string[] SUFFIX_EASY =  {
        "al", "el", "il", "ol", "ul",
        "an", "en", "in", "on", "un"
    };

    public static string[] SUFFIX_MEDIUM = {
        "astin",  "azin", "azol",
        "cain", "cillin", 
        "dipin", "fibrat", "kiren", "mab",
        "nitrat",  "pramin", 
        "pril", "rein"
    };

    public static string[] SUFFIX_HARD = {
        "azepam", "coxib", "cyclin","floxacin",
        "mycin", "oxetin", "sartan", "xetin",
        "zepam", "prazol"
    };

    public static string[] EASY =  {
        "Riz","Param","Gun","Ber","Maz","Frank","otto"
    };

    public static string[] MEDIUM =  {
        "Abroloz","Bananoz","Cholister", "Banana",
        "Lamsal", "Aspiz", "Ildiko"
    };

    public static string[] HARD =  {
        "Adrianotez","Macrogolcetylstear","Xenomorpho"
    };


    public static string getMed(string difficulty){
        string medString = "";



        if (difficulty.Equals(Difficulty.EASY)){
            string prefix = Meds.EASY[Random.Range(0, Meds.EASY.Length)];
            string suffix = Meds.SUFFIX_EASY[Random.Range(0, Meds.SUFFIX_EASY.Length)];
            medString = prefix + suffix;
        } else if(difficulty.Equals(Difficulty.MEDIUM)){
            string prefix = Meds.MEDIUM[Random.Range(0, Meds.MEDIUM.Length)];
            string suffix = Meds.SUFFIX_EASY[Random.Range(0, Meds.SUFFIX_EASY.Length)];
            medString = prefix + suffix;
        } else if(difficulty.Equals(Difficulty.HARD)){
            string prefix = Meds.MEDIUM[Random.Range(0, Meds.MEDIUM.Length)];
            string suffix = Meds.SUFFIX_MEDIUM[Random.Range(0, Meds.SUFFIX_MEDIUM.Length)];
            medString = prefix + suffix;
        }
        else if (difficulty.Equals(Difficulty.SUPERHARD)){
            string prefix = Meds.HARD[Random.Range(0, Meds.HARD.Length)];
            string suffix = Meds.SUFFIX_MEDIUM[Random.Range(0, Meds.SUFFIX_MEDIUM.Length)];
            medString = prefix + suffix;
        }
        else if (difficulty.Equals(Difficulty.MEGAHARD)){
            string prefix = Meds.HARD[Random.Range(0, Meds.HARD.Length)];
            string suffix = Meds.SUFFIX_HARD[Random.Range(0, Meds.SUFFIX_HARD.Length)];
            medString = prefix + suffix;
        }
        else if (difficulty.Equals(Difficulty.GIGAHARD)){
            string prefix1 = Meds.HARD[Random.Range(0, Meds.HARD.Length)];
            string suffix1 = Meds.SUFFIX_HARD[Random.Range(0, Meds.SUFFIX_HARD.Length)];
            string suffix2 = Meds.SUFFIX_MEDIUM[Random.Range(0, Meds.SUFFIX_MEDIUM.Length)];
            medString = prefix1 + suffix1 + suffix2;
        }
        else{//if (difficulty.Equals(Difficulty.NINETOUSANDANDONE))
            string prefix1 = Meds.HARD[Random.Range(0, Meds.HARD.Length)];
            string suffix1 = Meds.SUFFIX_HARD[Random.Range(0, Meds.SUFFIX_HARD.Length)];
            string suffix2 = Meds.SUFFIX_MEDIUM[Random.Range(0, Meds.SUFFIX_MEDIUM.Length)];
            string suffix3 = Meds.SUFFIX_HARD[Random.Range(0, Meds.SUFFIX_HARD.Length)];
            medString = prefix1 + suffix1 + suffix2;
        }

        return medString;

    }
};
