var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var tarefas = new List<Tarefa>();

// GET (listar tarefas)
app.MapGet("/tarefas", () => tarefas);

// POST (adicionar tarefas)
app.MapPost("/tarefas", (Tarefa novaTarefa) =>
{
    tarefas.Add(novaTarefa);
    return Results.Created($"/tarefas/{novaTarefa.Id}", novaTarefa);
});

//DELETE (excluir tarefa pelo ID)
app.MapDelete("/tarefas/{id}", (int id) => {
    var tarefa = tarefas.Find(t => t.Id == id);
    if (tarefa is null) 
    return Results.NotFound("Tarefa não encontrada");

    tarefas.Remove(tarefa);

    return Results.Ok($"Tarefa {id} removida com sucesso!");
});

app.Run();

//definição da variável Tarefa (com t maiusculo)
public record Tarefa (int Id, string Nome, bool Concluída);