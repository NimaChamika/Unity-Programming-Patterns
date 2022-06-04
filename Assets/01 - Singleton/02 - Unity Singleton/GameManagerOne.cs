using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SingletonPattern.One
{
    public class GameManagerOne : MonoBehaviour
    {
        //A static variable which holds a reference to the single created instance
        private static GameManagerOne instance = null;

        //A public static means of getting the reference to the single created instance, creating one if necessary
        public static GameManagerOne Instance
        {
            get
            {
                if (instance == null)
                {
                    // Find singleton of this type in the scene
                    var instance = GameObject.FindObjectOfType<GameManagerOne>();

                    // If there is no singleton object in the scene, we have to add one
                    if (instance == null)
                    {
                        GameObject obj = new GameObject("Game Manger One");
                        instance = obj.AddComponent<GameManagerOne>();


                        // The singleton object shouldn't be destroyed when we switch between scenes
                        DontDestroyOnLoad(obj);
                    }
                }

                return instance;
            }
        }


        void Awake()
        {
            if (instance == null)
            {
                instance = this;

                // The singleton object shouldn't be destroyed when we switch between scenes
                DontDestroyOnLoad(this.gameObject);
            }
            // because we inherit from MonoBehaviour whem might have accidentally added several of them to the scene,
            // which will cause trouble, so we have to make sure we have just one!
            else
            {
                Destroy(gameObject);
            }
        }


        //For testing
        public void TestSingleton()
        {
            Debug.Log($"Hello this is Singleton");
        }



    }
}
