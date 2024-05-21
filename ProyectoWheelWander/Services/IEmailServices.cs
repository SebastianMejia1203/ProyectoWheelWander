using ProyectoWheelWander.Models;

namespace ProyectoWheelWander.Services
{
    public interface IEmailServices
    {
        void enviarEmail(EmailDTO request);
    }
}
