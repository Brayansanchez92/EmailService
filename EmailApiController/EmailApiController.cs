using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sabio.Models.AppSettings;
using Sabio.Models.Requests.EmailRequest;
using Sabio.Services;
using Sabio.Services.Interfaces.Email;
using Sabio.Web.Controllers;
using Sabio.Web.Models.Responses;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace Sabio.Web.Api.Controllers.EmailApiController
{
    [Route("api/emails")]
    [ApiController]
    public class EmailApiController : BaseApiController
    {
        
        private IMailService _service = null;
        private IAuthenticationService<int> _authService = null;
       

        public EmailApiController(IMailService service, ILogger<EmailApiController> logger
            , IAuthenticationService<int> authService) : base(logger)
        {
            _service = service;
            _authService = authService;

           
        }

      [HttpPost]
       public async Task<IActionResult> TestSend(EmailSendRequest email)
       {
            int iCode = 200;
            BaseResponse response = null;
            try
            {
                await _service.TestSend(email);
                if (response == null)
                {
                    response = new SuccessResponse();
                    iCode = 201;

                }
            }
            catch (Exception ex)
            {

                iCode = 500;
                base.Logger.LogError(ex.ToString());
                response = new ErrorResponse($"Generic Error: {ex.Message}");
            }
            return StatusCode(iCode, response);
        }
       
    }
 }



    

