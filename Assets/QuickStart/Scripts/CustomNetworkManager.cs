using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;
using System;
using System.Collections;
using kcp2k;


public class CustomNetworkManager : NetworkManager
{
    public override void Start()
    {
        base.Start();

        //if (Application.isBatchMode)
        //{
        // Debug.Log("In BatchMode");
#if !UNITY_EDITOR
        StartCoroutine("StartHeadless");
#endif
       // }
    }
    
    IEnumerator StartHeadless()
    {
        yield return new WaitForSeconds(2.0f);

        string[] args = Environment.GetCommandLineArgs();

        if (args == null || args.Length <= 0)
        {
            Debug.Log(" - Type 0 for default - ");
            Debug.Log("Traffic 0=none   1=light (card game)  2=active (social game)  3=heavy (mmo)   4=frequent (fps)");
            Debug.Log("Client: - file - c - frameRate - hostIP - hostPort - traffic");
            Debug.Log("Server: - file - s - frameRate - hostIP - hostPort - traffic");
        }
        else
        {
            if (args.Length >= 3 && args[2] != "0")
            {
                serverTickRate = int.Parse(args[2]);
                Application.targetFrameRate = int.Parse(args[2]);
            }

            if (args.Length >= 4 && args[3] != "0")
            {
                networkAddress = args[3];
            }
        
            if (args.Length >= 5 && args[4] != "0")
            {
                //ignoring this section for whilst testing multiple transports
                
                //GetComponent<TelepathyTransport>().port = ushort.Parse(args[3]);
                //GetComponent<KcpTransport>().Port = ushort.Parse(args[4]);
                //((KcpTransport)Transport.activeTransport).Port = 1234;
            }

            if (args.Length >= 6 && args[5] != "0")
            {
                //static variable thats set in headless arugments, and then used in player script
                staticC.traffic = int.Parse(args[5]);
            }

            yield return new WaitForSeconds(2.0f);

            if (args[1] == "s")
            {
                StartServer();
            }
            else if (args[1] == "c")
            {
                if (args.Length >= 3 && args[2] == "0") { Application.targetFrameRate = 30; }
                StartClient();
            }
        }
    }


}
