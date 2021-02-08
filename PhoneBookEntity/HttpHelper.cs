using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Text;
using Newtonsoft.Json;

namespace Common.Service
{
  // ----------------------------------------------------------------------------------------------------------------------------
  public class SiteBase
  {
    // request
    public string UserSessionId { get; set; }
    public int RequestId { get; set; }

    // result
    public SiteError Error { get; set; }
  }

  public class SiteList : SiteBase
  {
    public object list;
  }

  public class SiteError
  {
    public int Code { get; set; }
    public string Message { get; set; }
  }

  public class BaseFilter : SiteBase
  {
    public int SiteId { get; set; }
  }

  // ----------------------------------------------------------------------------------------------------------------------------
  public enum enAPIRequest
  {
    _null,

    getlist,
    put,

    _last
  }

  // ----------------------------------------------------------------------------------------------------------------------------
  public class HttpHelper 
  {
    public string _controller { get; set; }
    public string _url { get; set; }

    public HttpHelper(string url)
    {
      _url = url;
    }

    public async Task<HttpResponseMessage> PostAsync(object obj, int methodid, string controller = "", string action = "")
    {
      try
      {
        using (HttpClient _client = new HttpClient())
        {
          var json = JsonConvert.SerializeObject(obj);
          var content = new StringContent(json, Encoding.UTF8, "application/json");
          string url = String.Format("api/{0}", String.IsNullOrEmpty(controller) ? _controller : controller);
          if (!string.IsNullOrEmpty(action))
            url += String.Format("/{0}", action);
          url += String.Format("/{0}", methodid.Equals((int)enAPIRequest.getlist) ? string.Empty : methodid.ToString());

          _client.BaseAddress = new Uri(_url);
          _client.DefaultRequestHeaders.Accept.Clear();
          _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
          return await _client.PostAsync(url, content);
        }
      }
      catch (Exception ex)
      {
        HttpResponseMessage httpmessage = new HttpResponseMessage();
        httpmessage.StatusCode = System.Net.HttpStatusCode.NotFound;
        httpmessage.ReasonPhrase = "AsyncPost failed. Web API may not be available. " + ex.Message;
        return httpmessage;
      }
    }

    public async Task<HttpResponseMessage> PostTextAsync(MultipartFormDataContent form, string page)
    {
      try
      {
        using (HttpClient _client = new HttpClient())
        {
          //var content = new StringContent(form.ToString(), Encoding.UTF8, "application/text");
          string url = String.Format("/{0}", page);

          _client.BaseAddress = new Uri(_url);
          _client.DefaultRequestHeaders.Accept.Clear();
          _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/text"));
          return await _client.PostAsync(url, form);
        }
      }
      catch (Exception ex)
      {
        HttpResponseMessage httpmessage = new HttpResponseMessage();
        httpmessage.StatusCode = System.Net.HttpStatusCode.NotFound;
        httpmessage.ReasonPhrase = "AsyncPost failed. " + ex.Message;
        return httpmessage;
      }
    }
  }
}
