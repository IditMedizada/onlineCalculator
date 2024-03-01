using System;
using System.Data;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Microsoft.AspNetCore.Routing.Template;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

public class Server
{
    
    public void Start(){
        TcpListener? server = null;
        int bytesRead, port = 8888;
        byte[] buffer = new byte[1024];

        try{
            // Set up the server to listen on the specified IP address and port
            IPAddress localAddr = IPAddress.Parse("127.0.0.1");
            server = new TcpListener(localAddr, port);
            server.Start();
            // Listening for incoming connections
            while (true)
            {
                TcpClient client = server.AcceptTcpClient();
                // Get a stream object for reading and writing
                NetworkStream stream = client.GetStream();
                // Read the incoming message
                StringBuilder requestBuilder = new();
                do{
                    bytesRead = stream.Read(buffer, 0, buffer.Length);
                    requestBuilder.Append(Encoding.ASCII.GetString(buffer, 0, bytesRead));
                }while(stream.DataAvailable);

                string expression = requestBuilder.ToString();
                expression = GetExpressionFromRequest(expression);

                if (!string.IsNullOrEmpty(expression))
                {
                    // Calculate the result
                    double result = CalculateMathExpression(expression);

                    // Construct the HTTP response with the result
                    string httpResponse = $"HTTP/1.1 200 OK\r\nContent-Length: {result.ToString().Length}\r\nAccess-Control-Allow-Origin: http://localhost:4200\r\nAccess-Control-Allow-Credentials: true\r\n\r\n{result}";

                    // Convert the HTTP response to bytes and send it back to the client
                    byte[] responseBytes = Encoding.ASCII.GetBytes(httpResponse);
                    stream.Write(responseBytes, 0, responseBytes.Length);

                }
                // Clean up the connection
                stream.Close();
                client.Close();
            
            }
        } catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
        }
     
    }  

    // Helper method to extract the mathematical expression from the client request
    private static string GetExpressionFromRequest(string request)
    {
        string[] lines = request.Split('\n');

        // Check if there are any elements in lines before using LastOrDefault
        if (lines.Any())
        {
            string? lastLine = lines.LastOrDefault()?.Trim();
            if (!string.IsNullOrEmpty(lastLine))
            {
                // Extract the expression from the last line
                return lastLine;
            }
        }

        return string.Empty;
    }

    // Method to calculate the result of a mathematical expression
    public static double CalculateMathExpression(string expression){
        double tempResult,result = 0;
        bool firstTime = true;
        // Split the expression into parts based on addition and subtraction
        string[] parts = expression.Split(new char[] { '+', '-' });
        // Use Regex to find all instances of addition and subtraction in the original expression
        MatchCollection addSub = Regex.Matches(expression, @"[+\-]");
        // Iterate through each part of the expression
        for(int i=0; i<parts.Length; i++){
            // Check if the part contains multiplication (*) or division (/)
            if(parts[i].Contains('*') || parts[i].Contains('/')){
                // Split the part into factors based on multiplication and division
                string[] mulDiv = parts[i].Split(new char[] { '*', '/' });
                // Initialize tempResult with the first factor
                tempResult = double.Parse(mulDiv[0]);
                // Use Regex to find all instances of multiplication and division in the current part
                MatchCollection matches = Regex.Matches(parts[i], @"[*\/]");
                // Iterate through each factor and perform the corresponding operation
                for(int j=1; j<mulDiv.Length; j++){
                    if(matches[j-1].Value == "*"){
                        tempResult *= double.Parse(mulDiv[j]);
                    }else{
                        tempResult /= double.Parse(mulDiv[j]);
                    }
                } 
                // Check if it's the first iteration and assign tempResult to result, or perform addition/subtraction 
                if(firstTime){
                    result = tempResult;
                    firstTime = false;
                }else{
                    if(addSub[i-1].Value == "+"){result += tempResult;}
                    else{result -= tempResult;}
                }
            }else{
                // Handle the case where the part does not contain multiplication or division
                if(firstTime){
                    result = double.Parse(parts[i]);
                    firstTime = false;
                }else{
                    if(addSub[i-1].Value == "+"){result += double.Parse(parts[i]);}
                    else{result -= double.Parse(parts[i]);}
                }
            }
           
        }
        return result;
    }
  
}