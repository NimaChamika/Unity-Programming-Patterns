using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SingletonPattern.Two
{
    public class GameMangerTwo : MonoBehaviour
    {
        //ONE SCENE SINGLETON
        //A static variable which holds a reference to the single created instance
        private static GameMangerTwo instance = null;

        //A public static means of getting the reference to the single created instance, creating one if necessary
        public static GameMangerTwo Instance
        {
            get
            {
                if (instance == null)
                {
                    // Find singleton of this type in the scene
                    var instance = GameObject.FindObjectOfType<GameMangerTwo>();

                    // If there is no singleton object in the scene, we have to add one
                    if (instance == null)
                    {
                        GameObject obj = new GameObject("Game Manger One");
                        instance = obj.AddComponent<GameMangerTwo>();
                    }
                }

                return instance;
            }
        }


        void Awake()
        {
            //HERE I WANT THIS SINGLETON ONLY IN ONE SCENE. I DONT WANT IT TO PERSIST BETWEEN SCENES.
            if (instance == null)
            {
                instance = this;
            }
        }


        //For testing
        public void TestSingleton()
        {
            Debug.Log($"Hello this is Singleton");
        }

        //IF WE EXPLICITLY DON'T MAKE IT NULL IT WILL BE THERE FOREVER. SINCE IT IS STATIC.
        //THIS SINGLETON IS ONLY USED IN ONE SCENE. WHEN SWITCHING SCENES GAME OBJECT WILL BE DESTROYED.
        //AFTER THAT WE EXPECT EVERYTHING TO BE CLEARED FROM MEMORY. BUT THAT'S NOT HOW UNITY WORKS.
        private void OnDestroy()
        {
            instance = null;
        }
    }
}
