    namespace APIGestãoTarefas.Services;
    using APIGestãoTarefas.Entities;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ITarefaService
    {
        Task<IEnumerable<Tarefa>> GetAllAsync(); // Obter todas as tarefas
        Task<Tarefa> GetByIdAsync(int id); // Obter tarefa por ID
        Task<Tarefa> CreateAsync(Tarefa tarefa); // Criar nova tarefa
        Task<Tarefa> UpdateAsync(int id, Tarefa tarefa); // Atualizar tarefa existente
        Task<bool> DeleteAsync(int id); // Deletar tarefa
    }