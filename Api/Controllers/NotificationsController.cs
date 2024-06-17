using Domain.IRepository;
using Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Json;
using WebPush;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : Controller
    {
        private readonly IHttpClientFactory _http;
        private readonly IConfiguration _configuration;
        private readonly IPushNotificationsRepository _notificationsRepository;
        public NotificationController(IHttpClientFactory http, IPushNotificationsRepository notificationsRepository, IConfiguration configuration)
        {
            _http = http;
            _notificationsRepository = notificationsRepository;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("SendPush")]
        public async Task<IActionResult> SendPushNotification()
        {
            List<PushNotificationsViewModel> pushNotifications = await _notificationsRepository.ReadAllAsync();
            if (pushNotifications is not null)
            {
                foreach (var pushNotification in pushNotifications)
                {
                    try
                    {
                        string pushEndpoint = pushNotification.Endpoint;
                        string p256dh = pushNotification.P256DH;
                        string auth = pushNotification.Auth;

                        string subject = "mailto:example@example.com";
#nullable disable
                        string publicKey = _configuration.GetValue<string>("Firebase:gcmPublicKey").ToString();
                        string privateKey = _configuration.GetValue<string>("Firebase:gcmPrivateKey").ToString();
#nullable enable

                        if (publicKey is null || privateKey is null)
                            throw new ArgumentNullException("Public or Private key argument null");

                        var payload = new
                        {
                            title = "Hello, World!",
                            body = "New record created!"
                        };

                        string payloadJson = JsonSerializer.Serialize(payload);

                        PushSubscription subscription = new PushSubscription(pushEndpoint, p256dh, auth);
                        VapidDetails vapidDetails = new VapidDetails(subject, publicKey, privateKey);

                        WebPushClient webPushClient = new WebPushClient();

                        await webPushClient.SendNotificationAsync(subscription, payloadJson, vapidDetails);
                    }
                    catch (ArgumentNullException n)
                    {
                        Console.WriteLine("Argument Null:" + n.Message);
                    }
                    catch (WebPushException exception)
                    {
                        Console.WriteLine("Http STATUS code" + exception.StatusCode);
                    }
                }

                return Ok();
            }

            return BadRequest();
        }
    }
}