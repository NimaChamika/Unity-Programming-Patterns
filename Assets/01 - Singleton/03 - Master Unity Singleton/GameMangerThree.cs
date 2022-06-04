using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SingletonPattern.Three
{
    /*THIS DOESNT PREVENT THE PROBLEM CASUED BY SINGLETON. BUT THIS ADDS LAYER OF ABSTRACTION
    BETWEEN THE SINGLETON SYSTEM AND THE OBJECT THAT MIGHT WANT TI ACCESS THEM. IT'S BEST TO KEEP
    THE STATIC REFERNCE COUNT TO A MINIMUM*/

    public class GameMangerThree : MonoBehaviour
    {
        public static GameMangerThree Instance { get; private set; }
        public PlayerManager PlayerManager { get; }
        public  AudioManager AudioManager { get; }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(this.gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
        }
    }
}

    
