using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SingletonPattern.Four
{
    public class LogWriter : MonoBehaviour
    {
        public static LogWriter Instance { get; private set; }

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

        public void WriteToFile(string msg)
        {
            /*THIS WILL WRITE THE MSG TO A TEXT FILE*/
        }
    }

}

