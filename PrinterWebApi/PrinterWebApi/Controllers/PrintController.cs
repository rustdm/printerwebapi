using Microsoft.AspNetCore.Mvc;
using PrinterWebApi.Models;
using System.Diagnostics;
using System.Reflection;
using ThermalTalk;

namespace PrinterWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrintController : ControllerBase
    {
        public static readonly string portName = "/dev/ttyACM0";
        //public static readonly string portName = "COM4";



        // GET api/print
        [HttpGet()]
        public string Get()
        {
            using var printer = new ThermalTalk.ReliancePrinter(portName);
            {
                return printer.GetStatus(StatusTypes.FullStatus).ToJSON(true);
            }
        }


        // POST api/print/bytearray
        [HttpPost("bytearray")]
        public int Post([FromBody] PrintPayload payload)
        {
            using var printer = new ThermalTalk.ReliancePrinter(portName);
            try
            {
                byte[] myByteArray = payload.data.Select(i => (byte)i).ToArray();
                printer.SendRaw(myByteArray);

                if (printer.FormFeed() == ReturnCode.Success)
                    return StatusCodes.Status200OK;
                else
                    return StatusCodes.Status500InternalServerError;
            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return StatusCodes.Status400BadRequest;
            }
        }
    }
}