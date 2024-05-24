using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using reports.win.Module.BusinessObjects;
using static DevExpress.Data.Helpers.ExpressiveSortInfo;

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
        [HttpPost("InsertarSolicitud")]
        public IActionResult InsertarSolicitud([FromBody] SolicitudRequest solicitudRequest)
        {

            try
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    var persona_guardar = uow.Query<Persona>().FirstOrDefault(c => c.Usuario.Username == solicitudRequest.Usuario);

                    if (persona_guardar != null)
                    {
                        var identificacionPersona = persona_guardar.Identificaciones.FirstOrDefault();

                        if (identificacionPersona != null)
                        {
                            // Aquí accedes al número de identificación
                            var numeroIdentificacion = identificacionPersona.NumeroIdentificacion;

                            // Resto de tu lógica
                            Solicitudes solicitud = new Solicitudes(uow);
                            Solicitudes_Reportes solicitud_Reportes = new Solicitudes_Reportes(uow);
                            var carrera = uow.Query<CatalogoCarrera>().FirstOrDefault(c => c.Carrera == solicitudRequest.Carrera);
                            var ubicacion = uow.Query<CatalogoUbicacion>().FirstOrDefault(c => c.Ubicacion == solicitudRequest.UbicacionPeticion);

                            solicitud_Reportes.Estado = Solicitudes_Reportes.EstadoSolicitud.Publicado;
                            solicitud_Reportes.Descripcion = solicitudRequest.Descripcion;
                            solicitud_Reportes.Carrera = carrera;
                            solicitud_Reportes.UbicacionDePeticion = ubicacion;
                            solicitud_Reportes.FechaRegistro = DateTime.Now;
                            solicitud_Reportes.Persona = persona_guardar;

                            // Buscar el objeto PersonaIdentificaciones por el número de identificación
                            var identificacion = uow.Query<PersonaIdentificaciones>().FirstOrDefault(c => c.NumeroIdentificacion == numeroIdentificacion);
                            if (identificacion != null)
                            {
                                solicitud_Reportes.Identificacion = identificacion;
                            }
                            else
                            {
                                return NotFound(new { Message = "No se encontró ninguna identificación con el número proporcionado." });
                            }

                            solicitud_Reportes.Save();

                            persona_guardar.Solicitude.Add(solicitud);
                            uow.CommitChanges();

                            return Ok(new { Message = "Solicitud insertada correctamente." });
                        }
                        else
                        {
                            return NotFound(new { Message = "La persona no tiene ninguna identificación asociada." });
                        }
                    }
                    else
                    {
                        return NotFound(new { Message = "No se encontró ninguna persona con el nombre proporcionado." });
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Error en el servidor al insertar la solicitud.", Error = ex.Message });
            }
        }



    }

    public class LoginRequest
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }

    public class SolicitudRequest
    {

        //public string NombrePersona { get; set; }
        public string Carrera { get; set; }
        public string Descripcion { get; set; }
        public string UbicacionPeticion { get; set; }
        public string Usuario { get; set; }

        

        public SolicitudRequest()
        {
            //NombrePersona = string.Empty;
            Carrera = string.Empty;
            Descripcion = string.Empty;
            UbicacionPeticion = string.Empty;
            Usuario = string.Empty;
        }

    }
}
