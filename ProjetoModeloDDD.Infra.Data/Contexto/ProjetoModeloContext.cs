using System.Data.Entity;
using ProjetoModeloDDD.Domain.Entities;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System;
using ProjetoModeloDDD.Infra.Data.EntityConfig;

namespace ProjetoModeloDDD.Infra.Data.Contexto
{
    public class ProjetoModeloContext : DbContext
    {
        public ProjetoModeloContext() : base("ProjetoModeloDDD")
        {

        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Produto> Produtos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            // quando tiver atributo com xxxx + id set como primary key
            modelBuilder.Properties().Where(p => p.Name == p.ReflectedType.Name + "Id").Configure(p => p.IsKey());

            // toda propriedade string set no banco de dados como varchar (ocupa menos espaço que nvachar
            modelBuilder.Properties<string>().Configure(p => p.HasColumnType("varchar"));

            // se não informar tamanho da string coloca sempre 100
            modelBuilder.Properties<string>().Configure(p => p.HasMaxLength(100));

            modelBuilder.Configurations.Add(new ClienteConfiguration());
            modelBuilder.Configurations.Add(new ProdutoConfiguration());

            /*
             * DEPOIS DE CONFIGURAR NO VAI NO PACKAGE MANAGER CONSOLE E DIGITA:
             * Enable-Migrations ... Atualiza a migrantion pra AutomaticMigrationsEnabled = true; na classe Migrations/Configuration
             * E da o Update-Database -Verbose
            */
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
            {
                if(entry.State == EntityState.Added)
                {
                    entry.Property("DataCadastro").CurrentValue = DateTime.Now;
                }
                if(entry.State == EntityState.Modified)
                {
                    entry.Property("DataCadastro").IsModified = false;
                }
            }

            return base.SaveChanges();
        }
    }
}
