using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Data.Mapping
{
    public class UserMap : IEntityTypeConfiguration<UserEntity>
    { //Para fazermos o mapeamento antes de criarmos a tabela UserEntity no banco de dados.
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
           builder.ToTable("User");
           builder.HasKey(u=>u.Id);
           builder.HasIndex(u=>u.Email).IsUnique();
           builder.Property(u=>u.Name).IsRequired().HasMaxLength(50);
           builder.Property(u=>u.Email).HasMaxLength(70);
        }
    }
}
