using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using reports.win.Module.BusinessObjects;

namespace UdeMReports.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CallAndroid : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public CallAndroid(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost("VerificarUsuario")]
        public IActionResult VerificarUsuario([FromBody] LoginRequest request)
        {
            try
            {
                Usuario usuario = _unitOfWork.FindObject<Usuario>(CriteriaOperator.Parse("Username = ?", request.Username));

                if (usuario != null && usuario.Password == request.Password && usuario.Estado == Usuario.EstadoUsuario.Activo)
                {
                    return Ok(new { Message = "Autorizado" });
                }
                else
                {
                    return Unauthorized(new { Message = "Credenciales inválidas o usuario inactivo." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Error en el servidor al verificar el usuario.", Error = ex.Message });
            }
        }
    }

    public class LoginRequest
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}
