using ProjetoModeloDDD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoModeloDDD.Infra.Data.EntityConfig
{
    public class ClienteConfiguration : EntityTypeConfiguration<Cliente>
    {
        public ClienteConfiguration()
        {
            // set ClienteId como primaryKey
            HasKey(c => c.ClienteId);

            // set como not allow null e tamanho maximo de 150
            Property(c => c.Nome).IsRequired().HasMaxLength(150);
            // set como not allow null e tamanho maximo de 150
            Property(c => c.Sobrenome).IsRequired().HasMaxLength(150);
            // set como not allow null
            Property(c => c.Email).IsRequired();

        }
    }
}
