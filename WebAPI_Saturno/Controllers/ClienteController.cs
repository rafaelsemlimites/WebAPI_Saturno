using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI_Saturno.Models;
using WebAPI_Saturno.Service.ClienteService;

namespace WebAPI_Saturno.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteInterface _clienteInterface;

        public ClienteController(IClienteInterface clienteInterface)
        {
            _clienteInterface = clienteInterface;
        }


        /// <summary>
        /// Obtém a lista de clientes.
        /// </summary>
        /// <returns>Uma resposta contendo a lista de clientes.</returns>
        /// <response code="200">Sucesso</response>
        /// ///  <response code="404">Não Encontrado</response>
        [HttpGet]
        [ProducesResponseType(typeof(ServiceResponse<List<ClienteModel>>), 200)]
        [Produces("application/json")]
        public async Task<ActionResult<ServiceResponse<List<ClienteModel>>>> GetClientes()
        {
            return Ok(await _clienteInterface.GetClientes());
        }

        /// <summary>
        /// Obtém um cliente por Id.
        /// </summary>
        /// <param name="id">Id do cliente.</param>
        /// <returns>Uma resposta contendo o cliente correspondente ao ID.</returns>
        /// /// <response code="200">Sucesso</response>
        /// /// <response code="404">Não Encontrado</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ServiceResponse<ClienteModel>), 200)]
        [ProducesResponseType(404)]
        [Produces("application/json")]
        public async Task<ActionResult<ServiceResponse<ClienteModel>>> GetClienteById(int id)
        {
            var serviceResponse = await _clienteInterface.GetClienteById(id);

            if (serviceResponse.Sucesso)
            {
                return Ok(serviceResponse);
            }
            else
            {
                return NotFound(serviceResponse); // Retorna 404 se não encontrar o cliente
            }
        }


        /// <summary>
        /// Obtém um cliente por número de telefone.
        /// </summary>
        /// <param name="ddd">DDD do telefone.</param>
        /// <param name="numero">Número do telefone.</param>
        /// <returns>Uma resposta contendo o cliente correspondente ao ID.</returns>
        /// /// <response code="200">Sucesso</response>
        /// /// <response code="404">Não Encontrado</response>
        [HttpGet("{ddd}/{numero}")]
        [ProducesResponseType(typeof(ServiceResponse<ClienteModel>), 200)]
        [ProducesResponseType(404)]
        [Produces("application/json")]
        public async Task<ActionResult<ServiceResponse<ClienteModel>>> GetClienteByTelefone(string ddd, string numero)
        {
            var serviceResponse = await _clienteInterface.GetClienteByTelefone(ddd, numero);

            if (serviceResponse.Sucesso)
            {
                return Ok(serviceResponse);
            }
            else
            {
                return NotFound(serviceResponse); // Retorna 404 se não encontrar o cliente
            }
        }

        /// <summary>
        /// Cria um novo cliente.
        /// </summary>
        /// <remarks>
        ///  {id=0nome=stringemail=stringtelefones=[{ddd=stringnumero=stringtipo=Fixo}]tipoCliente=Ouroativo=truedataDeAlteracao=2024-01-16T21:37:50.884Z}
        /// </remarks>
        /// <param name="novoCliente">Os detalhes do novo cliente.</param>
        /// <returns>Uma resposta contendo a lista atualizada de clientes.</returns>
        /// <response code="201">Criado com Sucesso</response>
        /// <response code="400">Erro ao recuperar a lista de clientes</response>
        [HttpPost]
        [ProducesResponseType(typeof(ServiceResponse<List<ClienteModel>>), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [Produces("application/json")]
        public async Task<ActionResult<ServiceResponse<List<ClienteModel>>>> CreateCliente(ClienteModel novoCliente)
        {
            var serviceResponse = await _clienteInterface.CreateCliente(novoCliente);

            if (serviceResponse.Sucesso)
            {
                return CreatedAtAction(nameof(GetClienteById), new { id = novoCliente.Id }, serviceResponse);
            }
            else if (serviceResponse.Mensagem == "Cliente não encontrado!")
            {
                return NotFound(serviceResponse); // Retorna 404 Not Found se o recurso não foi encontrado
            }
            else
            {
                return BadRequest(serviceResponse); // Retorna 400 se a solicitação for inválida
            }
        }

        /// <summary>
        /// Inativa um cliente por ID.
        /// </summary>
        /// <param name="id">O ID do cliente a ser inativado.</param>
        /// <returns>Uma resposta contendo o cliente inativado.</returns>
        /// <response code="204">Atualizado com Sucesso</response>
        /// <response code="404">Não Encontrado</response>
        [HttpPut("InativarCliente/{id}")]
        [ProducesResponseType(typeof(ServiceResponse<ClienteModel>), 204)]
        [ProducesResponseType(404)]
        [Produces("application/json")]
        public async Task<ActionResult<ServiceResponse<ClienteModel>>> InativaCliente(int id)
        {
            var serviceResponse = await _clienteInterface.InativaCliente(id);

            if (serviceResponse.Sucesso)
            {
                if (serviceResponse.Dados != null)
                {
                    return Ok(serviceResponse); // Retorna 200 OK se a operação foi bem-sucedida
                }
                else
                {
                    return NoContent(); // Retorna 204 No Content se o recurso foi atualizado, mas não há dados para retornar
                }
            }
            else
            {
                return NotFound(serviceResponse); // Retorna 404 Not Found se o recurso não foi encontrado
            }
        }


        /// <summary>
        /// Atualiza as informações de um cliente.
        /// </summary>
        /// <param name="editadoCliente">Os detalhes do cliente editado.</param>
        /// <returns>Uma resposta contendo o cliente atualizado.</returns>
        /// <response code="204">Inativado(atualizado) com Sucesso</response>
        /// <response code="404">Não Encontrado</response>
        [HttpPut]
        [ProducesResponseType(typeof(ServiceResponse<ClienteModel>), 204)]
        [ProducesResponseType(404)]
        [Produces("application/json")]
        public async Task<ActionResult<ServiceResponse<ClienteModel>>> UpdateCliente(ClienteModel editadoCliente)
        {
            var serviceResponse = await _clienteInterface.UpdateCliente(editadoCliente);

            if (serviceResponse.Sucesso)
            {
                if (serviceResponse.Dados != null)
                {
                    return Ok(serviceResponse); // Retorna 200 OK se a operação foi bem-sucedida
                }
                else
                {
                    return NoContent(); // Retorna 204 No Content se o recurso foi atualizado, mas não há dados para retornar
                }
            }
            else
            {
                return NotFound(serviceResponse); // Retorna 404 Not Found se o recurso não foi encontrado
            }
        }


        /// <summary>
        /// Exclui um cliente por Email.
        /// </summary>
        /// <param name="email">O Email do cliente a ser excluído.</param>
        /// <returns>Uma resposta contendo a lista atualizada de clientes após a exclusão.</returns>
        /// <response code="204">Excluído Sucesso</response>
        /// <response code="404">Não Encontrado</response>
        [HttpDelete("{email}")]
        [ProducesResponseType(typeof(ServiceResponse<List<ClienteModel>>), 200)]
        [ProducesResponseType(404)]
        [Produces("application/json")]
        public async Task<ActionResult<ServiceResponse<List<ClienteModel>>>> DeleteCliente(string email)
        {
            var serviceResponse = await _clienteInterface.DeleteCliente(email);

            if (serviceResponse.Sucesso)
            {
                if (serviceResponse.Dados != null && serviceResponse.Dados.Any())
                {
                    return Ok(serviceResponse); // Retorna 200 OK se a operação foi bem-sucedida e há dados para retornar
                }
                else
                {
                    return NoContent(); // Retorna 204 No Content se a operação foi bem-sucedida, mas não há dados para retornar
                }
            }
            else
            {
                return NotFound(serviceResponse); // Retorna 404 Not Found se o recurso não foi encontrado
            }
        }


    }
}
