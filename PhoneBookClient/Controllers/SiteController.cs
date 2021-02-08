using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Common.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net.Http;

namespace PhoneBookClient.Controllers
{
  public class SiteController : ControllerBase
  {
    private readonly ILogger<SiteController> _logger;
    protected IConfiguration _config;
    protected static HttpHelper _api { get; set; }
    public SiteController(ILogger<SiteController> logger, IConfiguration config)
    {
      _config = config;
      _logger = logger;
      if (_api == null)
        _api = new HttpHelper(config["API:Url"]);
    }

    protected async Task<string> PostAsync2(object obj, int methodid, string controller = "", string action = "")
    {
      HttpResponseMessage responseMessage = await _api.PostAsync(obj, methodid, controller, action);
      if ((responseMessage == null) || (responseMessage.Content == null))
      {
        _logger.LogError(methodid, "responseMessage == null");
        return null;
      }
      string responsedata = responseMessage.Content.ReadAsStringAsync().Result;
      if (responseMessage.IsSuccessStatusCode)
      {
        return responsedata;
      }

      SiteError error = JsonConvert.DeserializeObject<SiteError>(responsedata);
      if (error == null)
      {
        _logger.LogError(methodid, "error == null");
        return null;
      }
      _logger.LogError(methodid, string.Format("{0}; {1}", error.Code, error.Message));
      return null;
    }
  }
}
