namespace APIGestãoTarefas.Services;

    using APIGestãoTarefas.Entities;
    using APIGestãoTarefas.Repository;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class TarefaService : ITarefaService
    {
        private readonly GestaoTarefasContext _context;

        public TarefaService(GestaoTarefasContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tarefa>> GetAllAsync()
        {
            return await _context.Tarefas.Include(t => t.Categoria).ToListAsync(); // Incluindo categoria associada
        }

        public async Task<Tarefa> GetByIdAsync(int id)
        {
            var tarefa = await _context.Tarefas.Include(t => t.Categoria)
                                                .FirstOrDefaultAsync(t => t.Id == id);
            if (tarefa == null)
            {
                throw new KeyNotFoundException($"Tarefa com ID {id} não foi encontrada.");
            }
            return tarefa;
        }

        public async Task<Tarefa> CreateAsync(Tarefa tarefa)
        {
            _context.Tarefas.Add(tarefa);
            await _context.SaveChangesAsync();
            return tarefa;
        }

        public async Task<Tarefa> UpdateAsync(int id, Tarefa tarefa)
        {
            var tarefaExistente = await _context.Tarefas.FindAsync(id);

            if (tarefaExistente == null) {
                throw new ArgumentException("A tarefa selecionada não existe");
            }

            if (string.IsNullOrEmpty(tarefa.titulo))
            {
                throw new ArgumentException("O título não pode ser vazio.");
            }

            tarefaExistente.titulo = tarefa.titulo;
            tarefaExistente.descricao = tarefa.descricao;
            tarefaExistente.concluida = tarefa.concluida;
            tarefaExistente.prazo = tarefa.prazo;
            tarefaExistente.categoriaId = tarefa.categoriaId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // Lidar com erro de banco de dados
                throw new Exception("Erro ao salvar a tarefa no banco de dados", ex);
            }

            return tarefaExistente;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var tarefa = await _context.Tarefas.FindAsync(id);
            if (tarefa == null) return false;

            _context.Tarefas.Remove(tarefa);
            await _context.SaveChangesAsync();
            return true;
        }
    }

