namespace APIGestãoTarefas.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using APIGestãoTarefas.Entities;
using APIGestãoTarefas.Services;
using Microsoft.EntityFrameworkCore;
using APIGestãoTarefas.Repository;

[Route("api/[controller]")]
    [ApiController]
    public class TarefaController : ControllerBase
    {
      
        private readonly ITarefaService _tarefaService;
        private readonly GestaoTarefasContext _context; 

    // Injeção de dependência do DbContext no construtor
    public TarefaController(GestaoTarefasContext context, ITarefaService tarefaService)
    {
        _context = context; 
        _tarefaService = tarefaService;
    }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tarefa>>> GetTarefas()
        {
            var tarefas = await _tarefaService.GetAllAsync();
            return Ok(tarefas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Tarefa>> GetTarefa(int id)
        {
            var tarefa = await _tarefaService.GetByIdAsync(id);
            if (tarefa == null)
                return NotFound();

            return Ok(tarefa);
        }

    [HttpPost]
    public async Task<IActionResult> CreateTarefa([FromBody] Tarefa tarefa)
    {
        // Verificar se a categoria existe
        var categoria = await _context.Categorias
            .FirstOrDefaultAsync(c => c.Id == tarefa.categoriaId);

        if (categoria == null)
        {
            // Se não existir, retornar erro ou criar a categoria
            return BadRequest("Categoria não encontrada.");
        }

        // Se a categoria existe, criar a tarefa
        _context.Tarefas.Add(tarefa);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTarefa), new { id = tarefa.Id }, tarefa);
    }
    // 🔹 Atualizar uma tarefa
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTarefa(int id, [FromBody] Tarefa tarefa)
    {
        if (tarefa == null)
        {
            return BadRequest("Tarefa não pode ser nula.");
        }

        var tarefaAtualizada = await _tarefaService.UpdateAsync(id, tarefa);
        if (tarefaAtualizada == null)
        {
            return NotFound();
        }

        return NoContent();
    }

    // 🔹 Deletar uma tarefa
    [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTarefa(int id)
        {
            var sucesso = await _tarefaService.DeleteAsync(id);
            if (!sucesso)
                return NotFound();

            return NoContent();
        }
    }

