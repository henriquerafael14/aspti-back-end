using Aspti.Domain.Interface.Service;
using System;
using Microsoft.AspNetCore.Identity;
using Aspti.Infra.CrossCutting.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;

namespace Aspti.Domain.Entidades
{
	public class Usuario : IdentityUser<Guid>, IEntidadeBase
	{
		public Usuario()
		{

		}

        public Usuario(Guid id, string userName, string nome, string sobrenome,
        string email, UserStatusEnum status, string cpfCnpj, DateTime dataNascimento,
        string phoneNumber, string passwordHash, string securityStamp, string concurrencyStamp)
        {
            Id = id;
            UserName = userName;
            Nome = nome;
            Sobrenome = sobrenome;
            NormalizedUserName = userName.ToUpper();
            Email = email;
            NormalizedEmail = email.ToUpper();
            EmailConfirmed = true;
			Status = status;
			CPFCNPJ = cpfCnpj;
            DataNascimento = dataNascimento;
            PhoneNumber = phoneNumber;
            PasswordHash = passwordHash;
            SecurityStamp = securityStamp;
            ConcurrencyStamp = concurrencyStamp;
        }


        public string Nome { get; private set; }
		public string Sobrenome { get; private set; }
		public string CPFCNPJ { get; private set; }
		public DateTime? DataNascimento { get; private set; }
		public Endereco Endereco { get; set; }
		public virtual UserStatusEnum Status { get; set; } = UserStatusEnum.Inativo;
		public DateTime DataCriacao { get; set; } = DateTime.Now;
		public DateTime DataAtualizacao { get; set; } = DateTime.Now;
        public virtual ICollection<UsuarioPerfil> UsuarioPerfis { get; protected set; } = new Collection<UsuarioPerfil>();

        public void Ativar() => Status = UserStatusEnum.Ativo;
		public void Inativar() => Status = UserStatusEnum.Inativo;

        public void AtualizarPerfis(List<Guid> perfisId)
        {
            UsuarioPerfis ??= new List<UsuarioPerfil>();
            UsuarioPerfis.Clear();

            var usuarioPerfis = perfisId.Select(p => new UsuarioPerfil()
            {
                UserId = Id,
                RoleId = p
            }).ToList();

            UsuarioPerfis = usuarioPerfis;
        }

        public void AlterarPasswordHash(string passwordHash)
        {
            PasswordHash = passwordHash;
        }

        public void DefinirNomeCompleto()
        {
            UserName = $"{Nome} {Sobrenome}";
        }

        public void AlterarNome(string nome)
        {
            Nome = nome;
        }

        public void AlterarSobrenome(string sobrenome)
        {
            Sobrenome = sobrenome;
        }

        public void AlterarCPFCNPJ(string cpfcnpj)
        {
            CPFCNPJ = cpfcnpj;
        }

        public void AlterarEmail(string email)
        {
            Email = email;
        }

        public void AlterarTelefone(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }

        public void AlterarDataNascimento(DateTime? dataNascimento)
        {
            DataNascimento = dataNascimento;
        }

        //public void AlterarEndereco(Endereco endereco)
        //{
        //    Endereco = endereco;
        //}

        public void AtualizarUsuario(string nome, string sobrenome, string cpfCnpj, string email, string telefone, DateTime? dataNascimento)
        {
            AlterarNome(nome);
            AlterarSobrenome(sobrenome);
            AlterarCPFCNPJ(cpfCnpj);
            AlterarEmail(email);
            AlterarTelefone(telefone);
            AlterarDataNascimento(dataNascimento);
        }
    }
}
