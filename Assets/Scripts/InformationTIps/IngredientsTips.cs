using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class IngredientsTips
{
    [SerializeField] string[] informationTips = {
        "Eggs are an excellent source of minerals including vitamin D, zinc, selenium, and vitamin E, which the body need for proper immune function. Eggs contain all the nutrients that the body needs to produce energy. And it contains protein to make make your muscles strong.",
        "Cabbage is an excellent source of the vitamin C and maintaining immune system. Cabbage is high in fiber and contains zero fat.It will help the people who want to lose weight.",
        "Papayas contain high levels of antioxidants vitamin A, vitamin C, and vitamin E. Diets high in antioxidants may reduce the risk of heart disease.",
        "Onions are packed with immune-boosting nutrients like selenium, sulfur compounds, zinc, and vitamin C. Eating onions may help control blood sugar, which is especially significant for people with diabetes or prediabetes.",
        "Spinach is one of the most nutritionally dense vegetables. It’s loaded with vitamins, minerals and antioxidants, and is incredibly low in calories. It has two antioxidants which is lutein and zeaxanthin that will help you protect your eyes from damaging UV light and other harmful light wavelengths.",
        "Chicken is high in vitamin B-6. Vitamin B-6 is an important player in many of the chemical reactions that happen in the body. It’s also vital to the formation of new and healthy red blood cells.",
        "Red meat such as beef and lamb provide iron, zinc, vitamins B12 and B6 that all play an important role in keeping your immune system in check, not to mention zinc helps with wound healing and protein aids in tissue building and repair after an injury.",
        "Pechay, like all types of cabbage, is a good source of protein, vitamin A, vitamin C, Vitamin B6, calcium, iron, potassium, vitamin K, and more. It may help to lower your risk of developing heart disease",
        "Garlic produces a chemical called allicin. Allicin also makes garlic smell, and it helps to protect you against atherosclerosis and stroke.",
        "Ginger may have anti-inflammatory, antibacterial, antiviral, and other healthful properties. Ginger contains chemicals that may reduce nausea and inflammation.",
        "Potato is a moderate source of iron, and its high vitamin C content promotes iron absorption. It is a good source of vitamins B1, B3 and B6 and minerals such as potassium, phosphorus and magnesium, and contains folate, pantothenic acid and riboflavin.Potatoes are a good source of fiber, which can help you lose weight.",
        "Carrots are rich in vitamins, minerals, and fiber. They are also a good source of antioxidants. Carrots is rich in beta-carotene, a compound your body changes into vitamin A, which helps keep your eyes healthy.",
        "Radish are a good source of antioxidants like catechin, pyrogallol, vanillic acid, and other phenolic compounds. These root vegetables also have a good amount of vitamin C, which acts as an antioxidant to protect your cells from damage.",
        "Eggplant has antioxidants like vitamins A and C, which help protect your cells against damage. It's also high in natural plant chemicals called polyphenols, which may help cells do a better job of processing sugar if you have diabetes.",
        "Okra is low in calories but packed full of nutrients. The vitamin C in okra helps support healthy immune function. Okra is also rich in vitamin K, which helps your body clot blood.",
        "Spinach contains minerals such as potassium, magnesium, copper, zinc, and manganese. These help the body regulate body fluids, cell functions, heart rate, and blood pressure. Spinach is particularly beneficial in treating iron deficiency (anemia) because it is rich in this micronutrient.",
        "Green beans are high in vitamin K, and they also contain a decent amount of calcium. These nutrients are important for maintaining strong, healthy bones and reducing your risk of fractures.",
        "Lettuce is also a good source of fiber, iron, folate, and vitamin C. Lettuce is a source of vitamin A too, which plays a role in eye health.",
        "Fish is rich in calcium and phosphorus and a great source of minerals, such as iron, zinc, iodine, magnesium, and potassium. Fish is packed with protein, vitamins, and nutrients that can lower blood pressure and help reduce the risk of a heart attack or stroke.",
        "Lemons contain a high amount of vitamin C, soluble fiber, and plant compounds that give them a number of health benefits. Lemons may aid weight loss and reduce your risk of heart disease, anemia, kidney stones, digestive issues, and cancer."
    };


    public string[] getTipsArray
    {
        get { return informationTips; }
    }

    public string getTip
    {
        get { return informationTips[UnityEngine.Random.Range(0,informationTips.Length-1)]; }
    }
}
