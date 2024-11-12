using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using gameServer.networking;


string ipAddress = "127.0.0.1"; // localhost
int port = 6969;

new SimpleTcpServer(ipAddress, port);