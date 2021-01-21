using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DNWS
{
  class Client_info : IPlugin
  {
    protected static Dictionary<String, int> statDictionary = null;
    public Client_info()
    {
      if (statDictionary == null)
      {
        statDictionary = new Dictionary<String, int>();

      }
    }

    public void PreProcessing(HTTPRequest request)
    {
      if (statDictionary.ContainsKey(request.Url))
      {
        statDictionary[request.Url] = (int)statDictionary[request.Url] + 1;
      }
      else
      {
        statDictionary[request.Url] = 1;
      }
    }
    public HTTPResponse GetResponse(HTTPRequest request)
    {
      HTTPResponse response = null;
      StringBuilder sb = new StringBuilder();

      String[] client = Regex.Split(request.getPropertyByKey("remoteendpoint"), ":");
      
      sb.Append("<html><body>");
      sb.Append("Client IP address: " + client[0] + "<br><br>");
      sb.Append("Client Port: " + client[1] + "<br><br>");
      sb.Append("Browser Information: " + request.getPropertyByKey("user-agent") + "<br><br>");
      sb.Append("Accept-Charset: " + request.getPropertyByKey("accept-language") + "<br><br>");
      sb.Append("Accept-Encoding: " + request.getPropertyByKey("accept-encoding"));
      sb.Append("</body></html>");
      response = new HTTPResponse(200);
      response.body = Encoding.UTF8.GetBytes(sb.ToString());
      return response;
    }

    public HTTPResponse PostProcessing(HTTPResponse response)
    {
      throw new NotImplementedException();
    }
  }
}