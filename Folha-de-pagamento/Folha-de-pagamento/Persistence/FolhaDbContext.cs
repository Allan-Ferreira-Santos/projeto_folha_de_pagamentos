using Folha_de_pagamento.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Folha_de_pagamento.Persistence
{
    public class FolhaDbContext : DbContext
    {
        public FolhaDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<FuncionarioModel> Funcionario { get; set; }
        public DbSet<FolhaModel> Folha { get; set; }

    }

}
