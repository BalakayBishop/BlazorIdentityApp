using Domain.IRepository;
using Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PushNotificationsController : Controller
    {
        private readonly IPushNotificationsRepository _notificationsRepository;
        public PushNotificationsController(IPushNotificationsRepository notificationsRepository)
        {
            _notificationsRepository = notificationsRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<PushNotificationsViewModel> pushNotifications = await _notificationsRepository.ReadAllAsync();
            return Ok(pushNotifications);
        }

        [HttpPost("CreateOrUpdate")]
        public async Task<IActionResult> CreatePushNotification([FromBody] string json)
        {
            if (json is not "null")
            {
                try
                {
                    PushSubViewModel pushSubObj = JsonConvert.DeserializeObject<PushSubViewModel>(json) ?? throw new NullReferenceException();
                    PushNotificationsViewModel dbPushNotification = await _notificationsRepository.ReadAsync(1);
                    if (dbPushNotification is not null)
                    {
                        // Record exists, update with new subscription info
                        dbPushNotification.Endpoint = pushSubObj.Endpoint;
                        dbPushNotification.P256DH = pushSubObj.Keys.P256dh;
                        dbPushNotification.Auth = pushSubObj.Keys.Auth;

                        PushNotificationsViewModel pushNotification = await _notificationsRepository.UpdateAsync(dbPushNotification);
                        return Ok();
                    }
                    else
                    {
                        // Record does not exist, create new 
                        PushNotificationsViewModel newPushNotification = new()
                        {
                            Endpoint = pushSubObj.Endpoint,
                            P256DH = pushSubObj.Keys.P256dh,
                            Auth = pushSubObj.Keys.Auth,
                        };
                        newPushNotification = await _notificationsRepository.CreateAsync(newPushNotification);
                        return Ok();
                    }
                }
                catch (NullReferenceException n)
                {
                    Console.WriteLine("Null reference caught");
                    return BadRequest();
                }
                catch (Exception ex)
                {
                    return BadRequest();
                }
            }

            return Ok();
        }
    }
}