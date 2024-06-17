using Domain.IRepository;
using Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecordsController : Controller
    {
        private readonly IHttpClientFactory _http;
        private readonly IRecordsRepository _recordsRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly IUserRecordsRepository _userRecordsRepository;
        public RecordsController(IRecordsRepository recordsRepository, IUsersRepository usersRepository, IHttpClientFactory http, 
            IUserRecordsRepository userRecordsRepository)
        {
            _recordsRepository = recordsRepository;
            _usersRepository = usersRepository;
            _userRecordsRepository = userRecordsRepository;
            _http = http;
        }

        // Create
        [HttpPost]
        public async Task<IActionResult> CreateAsync(RecordsViewModel record)
        {
            try
            {
                // New Record obj saved to db
                RecordsViewModel newRecord = await _recordsRepository.CreateAsync(record);

                if (newRecord is not null)
                {
                    // Create new UserRecord
                    UsersViewModel user = await _usersRepository.ReadAsync(record.UserEmail);

                    UserRecordsViewModel newUserRecord = new()
                    {
                        RecordId = newRecord.Id,
                        UserId = user.Id,
                    };

                    newUserRecord = await _userRecordsRepository.CreateAsync(newUserRecord);

                    if (newUserRecord is not null)
                        return Ok(newRecord);

                    return BadRequest();
                }

                return BadRequest();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // Read
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<RecordsViewModel> allRecords = await _recordsRepository.ReadAllAsync();

            if (allRecords is not null)
                return Ok(allRecords);

            return NotFound();
        }

        [HttpGet("{recordId:int}")]
        public async Task<IActionResult> GetById(int recordId)
        {
            RecordsViewModel record = await _recordsRepository.ReadAsync(recordId);

            if (record is not null)
                return Ok(record);

            return NotFound();
        }

        //Update
    }
}