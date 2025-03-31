    namespace APIGestãoTarefas.Repository;
    using Microsoft.EntityFrameworkCore;
    using APIGestãoTarefas.Entities;
    
    public class GestaoTarefasContext : DbContext
    {
        public GestaoTarefasContext(DbContextOptions<GestaoTarefasContext> options)
                : base(options) { }
        // Define as tabelas do banco de dados
        public DbSet<Tarefa> Tarefas { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=gestao_tarefas;Username=postgres;Password=1313");
            }
        }
    }
