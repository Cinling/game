using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

public class SocketLong {

    Socket socket;
    byte[] buffer = new byte[1024];

    public void Init() {
        try {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect("127.0.0.1", 6001);

            socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveMessage), socket);
        } catch (Exception e) {
            Debug.LogError(e.GetBaseException());
        }
    }

    private void ReceiveMessage(IAsyncResult ar) {
        try {

            var socket = ar.AsyncState as Socket;

            var length = socket.EndReceive(ar);
            socket.Send(System.Text.Encoding.UTF8.GetBytes("recv"));

            byte[] reallData = new byte[length];

            Array.Copy(buffer, reallData, length);

            string recv = System.Text.Encoding.UTF8.GetString(reallData);

            Debug.Log(recv);
        } catch (Exception ex) {
            Debug.LogError(ex.GetBaseException());
        }

        finally {

            socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveMessage), socket);

        }
    }
}
