using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System.Data.Common;
using WebAPI_Saturno.DataContext;
using WebAPI_Saturno.Models;

namespace WebAPI_Saturno.Service.ClienteService
{
    public class ClienteService : IClienteInterface
    {
        private readonly ApplicationDbContext  _context;
        public ClienteService(ApplicationDbContext context) 
        {
        
            _context = context;
        }


        public async Task<ServiceResponse<List<ClienteModel>>> GetClientes()
        {

            ServiceResponse<List<ClienteModel>> serviceResponse = new ServiceResponse<List<ClienteModel>>();

            try
            {
                // Recupera a lista atualizada de clientes (incluindo telefones)
                serviceResponse.Dados = _context.Clientes
                    .Include(c => c.Telefones) // Inclui os telefones na consulta
                    .ToList();

                if (serviceResponse.Dados.Count == 0)
                {
                    serviceResponse.Mensagem = "Nenhum dado encontrado!";
                    serviceResponse.Sucesso = true;
                }
            }
            catch (DbException ex)
            {
                // Logar a exceção ou tratar conforme necessário
                serviceResponse.Mensagem = "Erro ao acessar o banco de dados.";
                serviceResponse.Sucesso = false;

                // Imprimir detalhes da exceção interna
                if (ex.InnerException != null)
                {
                    Console.WriteLine("### Inner Exception Message: " + ex.InnerException.Message);
                    Console.WriteLine("@@@ Inner Exception StackTrace: " + ex.InnerException.StackTrace);
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;

                // Imprimir detalhes da exceção interna
                if (ex.InnerException != null)
                {
                    Console.WriteLine("### Inner Exception Message: " + ex.InnerException.Message);
                    Console.WriteLine("@@@ Inner Exception StackTrace: " + ex.InnerException.StackTrace);
                }
            }

            return serviceResponse;
        }
        
        public async Task<ServiceResponse<ClienteModel>> GetClienteByTelefone(string ddd, string numero)
        {
            ServiceResponse<ClienteModel> serviceResponse = new ServiceResponse<ClienteModel>();

            try
            {
                ClienteModel cliente = _context.Clientes
                                        .Include(c => c.Telefones)
                                        .FirstOrDefault(c => c.Telefones.Any(t => t.DDD == ddd && t.Numero == numero));

                if (cliente == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Cliente não encontrado!";
                    serviceResponse.Sucesso = false;
                }

                serviceResponse.Dados = cliente;
            }
            catch (DbException ex)
            {
                // Logar a exceção ou tratar conforme necessário
                serviceResponse.Mensagem = "Erro ao acessar o banco de dados.";
                serviceResponse.Sucesso = false;

                // Imprimir detalhes da exceção interna
                if (ex.InnerException != null)
                {
                    Console.WriteLine("### Inner Exception Message: " + ex.InnerException.Message);
                    Console.WriteLine("@@@ Inner Exception StackTrace: " + ex.InnerException.StackTrace);
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;

                // Imprimir detalhes da exceção interna
                if (ex.InnerException != null)
                {
                    Console.WriteLine("### Inner Exception Message: " + ex.InnerException.Message);
                    Console.WriteLine("@@@ Inner Exception StackTrace: " + ex.InnerException.StackTrace);
                }
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<ClienteModel>> GetClienteById(int id)
        {
            ServiceResponse<ClienteModel> serviceResponse = new ServiceResponse<ClienteModel>();

            try
            {
                ClienteModel cliente = _context.Clientes
                                        .Include(c => c.Telefones)
                                        .FirstOrDefault(c => c.Id == id);

                if (cliente == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Cliente não encontrado!";
                    serviceResponse.Sucesso = false;
                }

                serviceResponse.Dados = cliente;
            }
            catch (DbException ex)
            {
                // Logar a exceção ou tratar conforme necessário
                serviceResponse.Mensagem = "Erro ao acessar o banco de dados.";
                serviceResponse.Sucesso = false;

                // Imprimir detalhes da exceção interna
                if (ex.InnerException != null)
                {
                    Console.WriteLine("### Inner Exception Message: " + ex.InnerException.Message);
                    Console.WriteLine("@@@ Inner Exception StackTrace: " + ex.InnerException.StackTrace);
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;

                // Imprimir detalhes da exceção interna
                if (ex.InnerException != null)
                {
                    Console.WriteLine("### Inner Exception Message: " + ex.InnerException.Message);
                    Console.WriteLine("@@@ Inner Exception StackTrace: " + ex.InnerException.StackTrace);
                }
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<ClienteModel>>> CreateCliente(ClienteModel novoCliente)
        {
            ServiceResponse<List<ClienteModel>> serviceResponse = new ServiceResponse<List<ClienteModel>>();

            try
            {
                if (novoCliente == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Informe os dados para registrar";
                    serviceResponse.Sucesso = false;

                    return serviceResponse;
                }

                // Adiciona o novo cliente
                novoCliente.Id = 0;
                _context.Add(novoCliente);
                await _context.SaveChangesAsync();

                // Recupera a lista atualizada de clientes (incluindo telefones)
                serviceResponse.Dados = _context.Clientes
                    .Include(c => c.Telefones)
                    .ToList();
            }
            catch (DbException ex)
            {
                // Logar a exceção ou tratar conforme necessário
                serviceResponse.Mensagem = "Erro ao acessar o banco de dados.";
                serviceResponse.Sucesso = false;

                // Imprimir detalhes da exceção interna
                if (ex.InnerException != null)
                {
                    Console.WriteLine("### Inner Exception Message: " + ex.InnerException.Message);
                    Console.WriteLine("@@@ Inner Exception StackTrace: " + ex.InnerException.StackTrace);
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;

                // Imprimir detalhes da exceção interna
                if (ex.InnerException != null)
                {
                    Console.WriteLine("### Inner Exception Message: " + ex.InnerException.Message);
                    Console.WriteLine("@@@ Inner Exception StackTrace: " + ex.InnerException.StackTrace);
                }
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<ClienteModel>> InativaCliente(int id)
        {
           ServiceResponse<ClienteModel> serviceResponse = new ServiceResponse<ClienteModel>();

            try
            {
                ClienteModel cliente = _context.Clientes
                        .FirstOrDefault(c => c.Id == id);

                if (cliente == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Cliente não encontrado!";
                    serviceResponse.Sucesso = false;
                }

                cliente.Ativo = false;
                cliente.DataDeAlteracao = DateTime.Now.ToLocalTime();

                _context.Clientes.Update(cliente);
                await _context.SaveChangesAsync();


                ClienteModel clienteAtualizado = _context.Clientes
                                        .Include(c => c.Telefones) // Carregar a lista de telefones
                                        .FirstOrDefault(c => c.Id == id);

                serviceResponse.Dados = clienteAtualizado;
                serviceResponse.Mensagem = "Cliente Inativado com sucesso!";


            }
            catch (DbException ex)
            {
                // Logar a exceção ou tratar conforme necessário
                serviceResponse.Mensagem = "Erro ao acessar o banco de dados.";
                serviceResponse.Sucesso = false;

                // Imprimir detalhes da exceção interna
                if (ex.InnerException != null)
                {
                    Console.WriteLine("### Inner Exception Message: " + ex.InnerException.Message);
                    Console.WriteLine("@@@ Inner Exception StackTrace: " + ex.InnerException.StackTrace);
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;

                // Imprimir detalhes da exceção interna
                if (ex.InnerException != null)
                {
                    Console.WriteLine("### Inner Exception Message: " + ex.InnerException.Message);
                    Console.WriteLine("@@@ Inner Exception StackTrace: " + ex.InnerException.StackTrace);
                }
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<ClienteModel>> UpdateCliente(ClienteModel editadoCliente)
        {
            ServiceResponse<ClienteModel> serviceResponse = new ServiceResponse<ClienteModel>();

            try
            {
                ClienteModel cliente = _context.Clientes.AsNoTracking()
                                        .Include(c => c.Telefones)
                                        .FirstOrDefault(c => c.Id == editadoCliente.Id);

                if (cliente == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Cliente não encontrado!";
                    serviceResponse.Sucesso = false;
                }

                // Atualizar informações do cliente
                cliente.Nome = editadoCliente.Nome;
                cliente.Email = editadoCliente.Email;
                cliente.TipoCliente = editadoCliente.TipoCliente;
                cliente.Ativo = editadoCliente.Ativo;
                cliente.DataDeAlteracao = DateTime.Now.ToLocalTime();

                // Excluir telefones existentes
                foreach (var telefoneExistente in cliente.Telefones.ToList())
                {
                    _context.Telefones.Remove(telefoneExistente);
                }

                // Adicionar novos telefones
                foreach (var telefone in editadoCliente.Telefones)
                {
                    cliente.Telefones.Add(telefone);
                }


                // Salvar as alterações
                _context.Clientes.Update(cliente);
                await _context.SaveChangesAsync();

                // Recarregar o cliente atualizado
                ClienteModel clienteAtualizado = _context.Clientes
                                        .Include(c => c.Telefones) // Carregar a lista de telefones
                                        .FirstOrDefault(c => c.Id == editadoCliente.Id);

                serviceResponse.Dados = clienteAtualizado;
                serviceResponse.Mensagem = "Cliente Editado com sucesso!";



            }
            catch (DbException ex)
            {
                // Logar a exceção ou tratar conforme necessário
                serviceResponse.Mensagem = "Erro ao acessar o banco de dados.";
                serviceResponse.Sucesso = false;

                // Imprimir detalhes da exceção interna
                if (ex.InnerException != null)
                {
                    Console.WriteLine("### Inner Exception Message: " + ex.InnerException.Message);
                    Console.WriteLine("@@@ Inner Exception StackTrace: " + ex.InnerException.StackTrace);
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;

                // Imprimir detalhes da exceção interna
                if (ex.InnerException != null)
                {
                    Console.WriteLine("### Inner Exception Message: " + ex.InnerException.Message);
                    Console.WriteLine("@@@ Inner Exception StackTrace: " + ex.InnerException.StackTrace);
                }
            }

            return serviceResponse;


        }

        public async Task<ServiceResponse<List<ClienteModel>>> DeleteCliente(string email)
        {

            ServiceResponse<List<ClienteModel>> serviceResponse = new ServiceResponse<List<ClienteModel>>();

            try
            {
                ClienteModel cliente = _context.Clientes
                                        .Include(c => c.Telefones)
                                        .FirstOrDefault(c => c.Email == email);

                if (cliente == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Cliente não encontrado!";
                    serviceResponse.Sucesso = false;
                }

                _context.Clientes.Remove(cliente);
                await _context.SaveChangesAsync();

                serviceResponse.Dados = _context.Clientes
                                        .Include(c => c.Telefones) // Inclui os telefones na consulta
                                        .ToList();
                serviceResponse.Mensagem = "Cliente Removido com sucesso!";



            }
            catch (DbException ex)
            {
                // Logar a exceção ou tratar conforme necessário
                serviceResponse.Mensagem = "Erro ao acessar o banco de dados.";
                serviceResponse.Sucesso = false;

                // Imprimir detalhes da exceção interna
                if (ex.InnerException != null)
                {
                    Console.WriteLine("### Inner Exception Message: " + ex.InnerException.Message);
                    Console.WriteLine("@@@ Inner Exception StackTrace: " + ex.InnerException.StackTrace);
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;

                // Imprimir detalhes da exceção interna
                if (ex.InnerException != null)
                {
                    Console.WriteLine("### Inner Exception Message: " + ex.InnerException.Message);
                    Console.WriteLine("@@@ Inner Exception StackTrace: " + ex.InnerException.StackTrace);
                }
            }

            return serviceResponse;


        }








    }
}
