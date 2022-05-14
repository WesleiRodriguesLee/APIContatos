﻿using ApiContatos.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
namespace ApiContatos.Controllers
{
    public class ContatosController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetTodosContatos(bool incluirEndereco = false)
        {
            IList<Contato> contatos = null;
            using (var ctx = new AppDbContext())
            {
                contatos = ctx.Contatos.Include("Endereco").ToList()
                            .Select(s => new Contato()
                            {
                                ContatoId = s.ContatoId,
                                Nome = s.Nome,
                                Email = s.Email,
                                Telefone = s.Telefone,
                                Endereco = s.Endereco == null || incluirEndereco == false ? null : new Endereco()
                                {
                                    EnderecoId = s.Endereco.EnderecoId,
                                    Local = s.Endereco.Local,
                                    Cidade = s.Endereco.Cidade,
                                    Estado = s.Endereco.Estado
                                }
                            }).ToList();
            }
            if (contatos.Count == 0)
            {
                return NotFound();
            }
            return Ok(contatos);
        }
        public IHttpActionResult GetContatoPorId(int? id)
        {
            if (id == null)
                return BadRequest("O Id do contato é inválido");
            Contato contato = null;
            using (var ctx = new AppDbContext())
            {
                contato = ctx.Contatos.Include("Endereco").ToList()
                    .Where(c => c.ContatoId == id)
                    .Select(c => new Contato()
                    {
                        ContatoId = c.ContatoId,
                        Nome = c.Nome,
                        Email = c.Email,
                        Telefone = c.Telefone,
                        Endereco = c.Endereco == null ? null : new Endereco()
                        {
                            EnderecoId = c.Endereco.EnderecoId,
                            Local = c.Endereco.Local,
                            Cidade = c.Endereco.Cidade,
                            Estado = c.Endereco.Estado
                        }
                    }).FirstOrDefault<Contato>();
            }
            if (contato == null)
            {
                return NotFound();
            }
            return Ok(contato);
        }
        public IHttpActionResult GetContatoPorNome(string nome)
        {
            if (nome == null)
                return BadRequest("Nome Inválido");
            IList<Contato> students = null;
            using(var ctx = new AppDbContext())
            {
                students = ctx.Contatos.Include("Endereco").ToList()
                    .Where(s => s.Nome.ToLower() == nome.ToLower())
                    .Select(s => new Contato()
                    {
                        ContatoId = s.ContatoId,
                        Nome = s.Nome,
                        Email = s.Email,
                        Telefone = s.Telefone,
                        Endereco = s.Endereco == null ? null : new Endereco()
                        {
                            EnderecoId = s.Endereco.EnderecoId,
                            Local = s.Endereco.Local,
                            Cidade = s.Endereco.Cidade,
                            Estado = s.Endereco.Estado
                        }
                    }).ToList();
            }
            if (students.Count == 0)
            {
                return NotFound();
            }
            return Ok(students);
        }
    }
}