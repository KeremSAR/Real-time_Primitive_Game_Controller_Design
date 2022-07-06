using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class networkHost1 : MonoBehaviour
{
    public string ip;
    public IPAddress hostaddress;
 
    public int port;
    public IPEndPoint ipep;
    public IPEndPoint sender;
    public UdpClient newSock;
    public Thread networkThread;
 
    public byte[] receivedData;
    public string dataString;
    public string[] EulerAngles;
    public InputField ip_text;
    public InputField port_text;
    private Quaternion quatern;
    private Quaternion inverseQt;
    void Start()
    {
        inverseQt = Quaternion.identity;
        networkThread = new Thread(ReceiveData);   //  UDP network thread
        networkThread.Start();
        
        receivedData = new byte[0];
        
        ipep = new IPEndPoint(IPAddress.Parse(ip), port);
        newSock = new UdpClient(ipep);
       // sender = new IPEndPoint(IPAddress.Any, 9998);*/
    }

    public void IPtext()
    {
        ip = ip_text.text; 
    }

    public void PortText()
    {
        
        port = int.Parse(port_text.text);
        Debug.Log("port num %d" + port);
        
    }
    void Update()
    {
        
        ReceiveData();
       // StartCoroutine(Delay());
        
    } 
  
    private string[] parseval(string data_string)
    {
        
        string[] data_values = data_string.Replace(".",",").Split(' ');
        foreach (string val in data_values)
        {
            Debug.Log("value parsed " + val);
        }
        return data_values;
    }
    public void ReceiveData()
    {
        try
        {
            if (newSock.Available > 0)
            {
                receivedData = newSock.Receive(ref sender);

                dataString = Encoding.ASCII.GetString(receivedData);
                EulerAngles = dataString.Replace(".",",").Split(' ');;
                
               // quatern = new Quaternion(float.Parse(EulerAngles[0]), 0, 0, float.Parse(EulerAngles[3]));
               // quatern = new Quaternion(0, float.Parse(EulerAngles[1]), 0, float.Parse(EulerAngles[3]));
               //quatern = new Quaternion(0, 0,float.Parse(EulerAngles[2]) , float.Parse(EulerAngles[3]));
                quatern = new Quaternion(float.Parse(EulerAngles[0]), -1*float.Parse(EulerAngles[1]), float.Parse(EulerAngles[2]), float.Parse(EulerAngles[3]));
              
                
                this.transform.rotation = quatern * inverseQt;//Quaternion.Euler(/*-1f*float.Parse(EulerAngles[0].Replace(".",","))*/0,/*float.Parse(EulerAngles[1].Replace(".",","))*/0,float.Parse(EulerAngles[1]));
                

            }
            else
            {
                // print("No data to receive");
            }
        }
        catch (Exception e)
        {
            Debug.Log("waiting the socket connetion");
        }
    }
    public void resetRotation()
    {
        inverseQt = Quaternion.Inverse(quatern);
    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.1f);
        ReceiveData();
    }
}