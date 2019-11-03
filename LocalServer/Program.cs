namespace LocalServer
{
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;

    public class Program
    {
        private const string newLine = "\r\n";
        public static void Main()
        {
            TcpListener tcpListener = new TcpListener(IPAddress.Loopback, 80);

            tcpListener.Start();

            while (true)
            {
                TcpClient client = tcpListener.AcceptTcpClient();

                using (NetworkStream stream = client.GetStream())
                {
                    byte[] requestBytes = new byte[100000];

                    int readBytes = stream.Read(requestBytes, 0, requestBytes.Length);

                    string stringRequest = Encoding.UTF8.GetString(requestBytes, 0, readBytes);

                    Console.WriteLine(new string('=', 91));

                    Console.WriteLine(stringRequest);

                    string responseBody = "<form method='post'><input type='text' name='tweet' placeholder='Enter tweet'><input name='name' /> <input type='submit'/ > </form> ";

                    string response = "Http/1.0 200 OK" + newLine +
                     "Server: MyCustomServer/1.0" + newLine +
                     "Content-Type:text/html" + newLine +
                     "Content-Disposition :attachment; filename=firs.html" + newLine +
                     $"Content-Length: {responseBody.Length}" + newLine + newLine +
                     responseBody;

                    byte[] responseBytes = Encoding.UTF8.GetBytes(response);

                    stream.Write(responseBytes, 0, responseBytes.Length);
                }
            }
        }
    }
}
