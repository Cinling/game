
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using System.Threading;
using System.IO;
using System;
using System.Collections;
using System.Collections.Generic;

public class SocketTcp {

    private static string host = "";
    private static int port = 0;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="host"></param>
    /// <param name="port"></param>
    public static void Init(string host, int port) {
        SocketTcp.host = host;
        SocketTcp.port = port;
    }

    /// <summary>
    /// 发送并接收数据
    /// </summary>
    /// <param name="send">需要发送的字符串</param>
    /// <returns></returns>
    public static string Send(string send) {
        ThreadTool.GetInstance().MainThread_RunOnWorldSceneLambda();
        string recv = "";

        TcpClient tcpClient = new TcpClient();
        tcpClient.Connect(host, port);

        if (tcpClient != null) {
            NetworkStream networkStream = tcpClient.GetStream();
            BinaryReader br = new BinaryReader(networkStream);
            BinaryWriter bw = new BinaryWriter(networkStream);

            try {
                bw.Write(System.Text.Encoding.UTF8.GetBytes(send));
                recv = System.Text.Encoding.UTF8.GetString(br.ReadBytes(100));
            } catch (Exception e) {
                Debug.LogError(e.StackTrace);
                recv = "";
            }
        }

        tcpClient.Close();

        return recv;
    }

    public static void Run() {
        TcpClient tcpClient;
        tcpClient = new TcpClient();  //创建一个TcpClient对象，自动分配主机IP地址和端口号  
        tcpClient.Connect("127.0.0.1", 6000);   //连接服务器，其IP和端口号为127.0.0.1和51888  
        if (tcpClient != null) {
            Debug.Log("连接服务器成功");

            NetworkStream networkStream = tcpClient.GetStream();
            BinaryReader br = new BinaryReader(networkStream);
            BinaryWriter bw = new BinaryWriter(networkStream);

            while (true) {
                try {
                    bw.Write(System.Text.Encoding.UTF8.GetBytes("Client = 中文"));  //向服务器发送字符串  
                    //接收服务器发送的数据  
                    string brString = System.Text.Encoding.UTF8.GetString(br.ReadBytes(100));
                    if (brString != null) {
                        Debug.Log(brString);
                        break;
                    }
                } catch (Exception e) {
                    Debug.Log(e.StackTrace);
                    break;        //接收过程中如果出现异常，将推出循环  
                }
            }
        }

        tcpClient.Close();
        Debug.Log("END");
    }
}



