using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using System.Net;
using System.Windows.Forms;


/**
 * Setup a simple http server to respond to client requests
 */
namespace SimpleHttpServer
{
    class MyServer
    {
        private string _server_addr;
        private string[] prefixes = new string[1];

        public MyServer(string server_addr)
        {
            _server_addr = server_addr;
            prefixes[0] = _server_addr;

            if (!HttpListener.IsSupported)
            {
                Console.WriteLine("Windows XP SP2 or Server 2003 is required to use the HttpListener class.");
                return;
            }
            // URI prefixes are required, 
            // for example "http://localhost.com:8080/test/".
            if (prefixes == null || prefixes.Length == 0)
                throw new ArgumentException("prefixes");

            createListener();
        }

        /**
         * Create a listenr to waiting request from client
         */
        public void createListener()
        {
            // Create a listener.
            HttpListener listener = new HttpListener();
            // Add the prefixes. 
            foreach (string s in prefixes)
            {
                listener.Prefixes.Add(s);
            }

            listener.Start();

            while (true)
            {
                Console.WriteLine("Listening...");
                // Note: The GetContext method blocks while waiting for a request. 
                HttpListenerContext context = listener.GetContext();
                string method = "";
                readRequest(context, method);
                sendResponse(context, method);
            }
            //listener.Stop();
        }

        /**
         * Reading request from client
         */
        public void readRequest(HttpListenerContext context, string method)
        {
            HttpListenerRequest req = context.Request;
            method = req.HttpMethod;
            Console.WriteLine("Request contentType: " + method);
        }

        /**
         * Sending response back to client
         */
        public void sendResponse(HttpListenerContext context, string method)
        {
            Console.WriteLine("Sending Response");
            // Obtain a response object.
            HttpListenerResponse response = context.Response;
            // Construct a response. 
            String c = null;
            c = Clipboard.GetText(TextDataFormat.Text);
            Console.WriteLine("Clipboard: {0}", c);
            string responseString = "{\"response\":true, \"clipboard\":\"" + c + "\"}";

            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
            // Get a response stream and write the response to it.
            response.ContentLength64 = buffer.Length;
            response.ContentType = "text/json";

            System.IO.Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            // Close the output stream.
            output.Close();
        }
    }
}
