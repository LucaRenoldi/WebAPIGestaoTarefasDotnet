namespace APIGestãoTarefas.Entities; 
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
     
        public class Categoria
        {
            [Key]
            public int Id { get; set; }

            [Required]
            [MaxLength(100)]
            public string Name { get; set; } = String.Empty;


            // Relação com tarefas
            public ICollection<Tarefa> Tarefas { get; set; } = [];
        }


