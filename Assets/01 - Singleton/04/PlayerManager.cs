using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SingletonPattern.Four
{
    public class PlayerManager : MonoBehaviour
    {
       /*IF YOU ACCESS THE LOG WRITER INSTANCE THIS WAY THEY ARE TIGHTLY COUPLED. IN THE FUTURE
        IF YOU WANT TO USE DIFFRENT LOGWRITER HERE YOU HAVE TO CHANGE EVERY REFERENCE. IN A LARGE CODEBASE
        THIS WILL BE A PROBLEM.*/
        private void WritePlayerData(string msg)
        {
            LogWriter.Instance.WriteToFile(msg);
        }

        /*HERE IS THE WAY TO MINIMIZE THE DAMAGE. IN THIS WAY IF YOU WANT TO CHANGE THE LOG WRITER INSTANCE YOU 
         ONLY NEED TO CHANGE ONE PLACE ONLY*/
        Action<string> logWriterFn;
        
        private void Start()
        {
            logWriterFn = LogWriter.Instance.WriteToFile;
        }

        private void WritePlayerDataTwo(string msg)
        {
            logWriterFn(msg);
        }
    }
}
    
