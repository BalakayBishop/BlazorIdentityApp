﻿using Domain.IRepository;
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
        private readonly IUserNotificationsRepository _userNotificationsRepository;
        private readonly IUsersRepository _usersRepository;
        public NotificationController(IHttpClientFactory http, IConfiguration configuration,
            IPushNotificationsRepository notificationsRepository, IUserNotificationsRepository userNotificationsRepository, IUsersRepository usersRepository)
        {
            _http = http;
            _configuration = configuration;
            _notificationsRepository = notificationsRepository;
            _userNotificationsRepository = userNotificationsRepository;
            _usersRepository = usersRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("SendPush")]
        public async Task<IActionResult> SendPushNotification()
        {
            // Get all PushNotification Records in db
            List<PushNotificationsViewModel> pushNotifications = await _notificationsRepository.ReadAllAsync();
            if (pushNotifications is not null)
            {
                try
                {
#nullable disable
                    // Get config strings
                    string publicKey = "BMboSCc9YRKnoNAf6xwlCwS5BHcIQY77Veqt1W8XTO33_1vycANd6SDH4qlRgJNT9V_l9Hje6n1EbBZiQ_dKad8";
                    string privateKey = "9znNMAxJZEVHiQbUQotgklWeGB1WyUUU28fQvul5enA";
#nullable enable

                    // If failed to get keys throw exception, end the method
                    if (publicKey is null || privateKey is null)
                        throw new ArgumentNullException("Public or Private key argument null");
                
                    foreach (var pushNotification in pushNotifications)
                    {
                        try
                        {
                            // Get the User Associated with this PushNotification record
                            UsersViewModel user = await _usersRepository.ReadAsync(pushNotification.UserId);

                            string pushEndpoint = pushNotification.Endpoint;
                            string p256dh = pushNotification.P256DH;
                            string auth = pushNotification.Auth;

                            string subject = "mailto:" + user.Email;

                            var payload = new
                            {
                                title = "Hello, World!",
                                body = "this is the body"
                            };

                            string payloadJson = JsonSerializer.Serialize(payload);

                            // Instanciate a new WebPush.PushSubcription, VapidDetails, & Client
                            //PushSubscription pushSubscription = new PushSubscription
                            //{
                            //    Endpoint = "https://wns2-by3p.notify.windows.com/w/?token=BQYAAADeTGUmLGJ7YvCWR34SicQTcE6i6MfOnro%2f8QDDfjdRt97mE2kixHZ0c1pBs2vaW7b3K1EOmjOWTaXfDTJaT4ysiWgj2jdvv9on9QQ9AW1MWQSIi766MdzuUFpuRL8zlCKUhqvv3%2f9JD0MfWQOmiKXAjK3InLEm2yCD39Ny1kbQNtWI2%2f2fhIC6Q2zXg22iwz9ZOp63xcQZwfYGx4XdrsmGCjAQmTYj8wUmmF6U0hhGbb7wpe6ED%2fe5H5xcqFELiW%2bRE6C3sGafLNT63EuSkTJ%2ftKrYMreK7kfJfAqyaiOrKVp63WEzxK%2fAd1hhXTh9RaE%3d",
                            //};

                            var vapidKeys = VapidHelper.GenerateVapidKeys();

                            // Initialize WebPush
                            PushSubscription subscription = new PushSubscription(pushEndpoint, p256dh, auth);
                            VapidDetails vapidDetails = new VapidDetails(subject, publicKey, privateKey);

                            WebPushClient webPushClient = new WebPushClient();

                            // Call WebPush API
                            await webPushClient.SendNotificationAsync(subscription, payloadJson, vapidDetails);

                            // New UserNotification obj
                            UserNotificationsViewModel newUNotification = new()
                            {
                                UserId = user.Id,
                                PushId = pushNotification.Id,
                                Title = payload.title,
                                Body = payload.body,
                            };

                            // Save new UserNotification obj to db to indicate notification sent 
                            UserNotificationsViewModel newUserNotification = await _userNotificationsRepository.CreateAsync(newUNotification);
                        }
                        catch (WebPushException exception)
                        {
                            Console.WriteLine("WebPush: Http STATUS code" + exception.StatusCode);
                            Console.WriteLine("WebPush: " + exception.Message);
                            continue;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Unexpected error: " + ex.Message);
                            continue;
                        }
                    }
                }
                catch (ArgumentNullException n)
                {
                    Console.WriteLine("Argument Null:" + n.Message);
                    return BadRequest();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Unexpected error: " + e.Message);
                    return BadRequest();
                }

                return Ok();
            }

            return BadRequest();
        }
    }
}