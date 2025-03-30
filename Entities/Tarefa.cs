     
    namespace APIGestãoTarefas.Entities;
    
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Diagnostics.CodeAnalysis;

public class Tarefa{
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string titulo { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string? descricao { get; set; }

        [Required]
        public bool concluida { get; set; } = false;

        public DateTime  horarioCriacao{ get; set; } = DateTime.UtcNow;
        public DateTime? prazo { get; set; }

        // Chave estrangeira para a categoria
        public int categoriaId { get; set; }
        public Categoria? Categoria { get; set; }

        // Chave estrangeira para o usuário (para futura implementar autenticação)
        //public int? IdUsuario { get; set; }
        //public Usuario? Usuario { get; set; } 
    }
