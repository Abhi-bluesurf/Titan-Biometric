using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Titan_Biometric.EFCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Titan_Biometric.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly EF_DataContext _context;
        public SessionController(EF_DataContext context)
        {
            _context = context;
        }

        [HttpPost("new/activity")]
        public async Task<IActionResult> SaveActivity(SessionInfo sessionInfo)
        {
            try
            {
                SessionInfo session = new SessionInfo();
                session.Username = sessionInfo.Username;
                session.SessionId = sessionInfo.SessionId;
                session.SessionProtocol = sessionInfo.SessionProtocol;
                session.SensorLocation = sessionInfo.SensorLocation;
                session.StartWeight = sessionInfo.StartWeight;
                session.ActivityDate = sessionInfo.ActivityDate.ToUniversalTime();
                session.EndWeight = sessionInfo.EndWeight;
                session.SessionNote = sessionInfo.SessionNote;
                _context.Add(session);
                await _context.SaveChangesAsync();
                return Ok("Success");
            }
            catch(Exception ex)
            {
                return BadRequest("Bad request");
            }
        }

        [HttpPost("update/activity")]
        public async Task<IActionResult> UpdateActivity(SessionInfo sessionInfo)
        {
            try
            {
                var session = _context.SessionsInfo
                                      .FirstOrDefault
                                      (s => s.SessionId == sessionInfo.SessionId);
                if(session != null)
                {
                    session.EndWeight = sessionInfo.EndWeight;
                    session.SessionNote = sessionInfo.SessionNote;
                    await _context.SaveChangesAsync();
                    return Ok("Success");
                }

                return Ok("SessionId not present");
            }
            catch
            {
                return BadRequest("Bad request");
            }
        }

        [HttpPost("new/event")]
        public async Task<IActionResult> SaveEvent(Events events)
        {
            try
            {
                Events newEvent = new Events();
                newEvent.SessionId = events.SessionId;
                newEvent.EventName = events.EventName;
                newEvent.Username = events.Username;
                newEvent.Value = events.Value;
                newEvent.Unit = events.Unit;
                newEvent.EventDescription = events.EventDescription;
                newEvent.EventDate = events.EventDate.ToUniversalTime();
                newEvent.EventTime = events.EventTime;
                _context.Add(newEvent);
                await _context.SaveChangesAsync();
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest("Bad request");
            }
        }

        [HttpPost("sensor/data")]
        public async Task<IActionResult> SaveSensorData(List<SensorData> sensorData)
        {
            try
            {
                SensorData data;
                for (int i = 0; i < sensorData.Count; i++)
                {
                    data = new SensorData();
                    data.Username = sensorData[i].Username;
                    data.SessionId = sensorData[i].SessionId;
                    data.TimeStampMSB = sensorData[i].TimeStampMSB;
                    data.TimeStampLSB = sensorData[i].TimeStampLSB;
                    data.LedOne = sensorData[i].LedOne;
                    data.LedTwo = sensorData[i].LedTwo;
                    data.LedThree = sensorData[i].LedThree;
                    data.LedFour = sensorData[i].LedFour;
                    data.BatteryVoltage = sensorData[i].BatteryVoltage;
                    data.DateTime = sensorData[i].DateTime.ToUniversalTime();
                    TimeSpan t = TimeSpan.Parse(sensorData[i].DateTime.ToString().Split(' ')[1]);
                    _context.Add(data);
                    await _context.SaveChangesAsync();
                }
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest("Bad request: \r\n"+ex.ToString());
            }
        }
    }
}
