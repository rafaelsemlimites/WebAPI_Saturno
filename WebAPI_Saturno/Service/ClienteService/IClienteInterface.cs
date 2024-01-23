using WebAPI_Saturno.Models;

namespace WebAPI_Saturno.Service.ClienteService
{
    public interface IClienteInterface
    {
        Task<ServiceResponse<List<ClienteModel>>> GetClientes();
        Task<ServiceResponse<List<ClienteModel>>> CreateCliente(ClienteModel novoCliente);
        Task<ServiceResponse<ClienteModel>> GetClienteById(int id);
        Task<ServiceResponse<ClienteModel>> GetClienteByTelefone(string ddd, string numero);
        Task<ServiceResponse<ClienteModel>> InativaCliente(int id);
        Task<ServiceResponse<ClienteModel>> UpdateCliente(ClienteModel editadoCliente);
        Task<ServiceResponse<List<ClienteModel>>> DeleteCliente(string email);


    }
}
