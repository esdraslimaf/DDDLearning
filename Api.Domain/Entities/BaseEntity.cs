using System;
using System.ComponentModel.DataAnnotations;
namespace Api.Domain.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        public Guid Id { get; set; } // Exemplo de guid: f47ac10b-58cc-4372-a567-0e02b2c3d479 Temos que gerar eles pelo Guid.NewGuid()
        private DateTime? _createAt;
        public DateTime? CreateAt //"Criado em" - Essa que vai pro banco de dados
        {
            get { return _createAt; }
            set { _createAt = (value==null ? DateTime.UtcNow : value); }
        }
        
        public DateTime? UpdateAt { get; set; } //"Atualizado em"
    }
}
