// DESAFIO: Sistema de Acesso a Documentos Confidenciais
// PROBLEMA: Uma aplicação corporativa precisa controlar acesso a documentos sensíveis,
// fazer cache de documentos pesados e registrar todas as operações.
// O código anterior misturava lógica de negócio com controle de acesso, cache e logging.

using DesignPatternChallenge_Proxy.Models;
using DesignPatternChallenge_Proxy.Proxy;

namespace DesignPatternChallenge_Proxy;

internal class Program
{
    // Contexto: Sistema que gerencia documentos confidenciais com requisitos de:
    // - Controle de acesso baseado em permissões
    // - Cache para documentos pesados
    // - Auditoria de todas as operações

    static void Main(string[] args)
    {
        Console.WriteLine("=== Sistema de Documentos Confidenciais ===\n");

        var documentRepository = new DocumentRepositoryProxy();

        var manager = new User("joao.silva", 5);
        var managerDocService = new DocumentServiceProxy(manager, documentRepository);

        var employee = new User("maria.santos", 2);
        var employeeDocService = new DocumentServiceProxy(employee, documentRepository);

        Console.WriteLine("\n--- Gerente acessando documento de alto nível ---");
        managerDocService.ViewDocument("DOC002");

        Console.WriteLine("\n--- Funcionário tentando acessar mesmo documento ---");
        employeeDocService.ViewDocument("DOC002");

        Console.WriteLine("\n--- Gerente acessando novamente (deveria usar cache) ---");
        managerDocService.ViewDocument("DOC002");

        Console.WriteLine("\n--- Funcionário acessando documento permitido ---");
        employeeDocService.ViewDocument("DOC003");

        Console.WriteLine("\n--- Funcionário editando documento não permitido ---");
        employeeDocService.EditDocument("DOC002", "New content for document 002");

        Console.WriteLine("\n--- Funcionário editando documento permitido ---");
        employeeDocService.EditDocument("DOC003", "New content for document 003");

        Console.WriteLine("\n--- Gerente tentando acessar documento inválido ---");
        managerDocService.ViewDocument("DOC004");

        managerDocService.ShowAuditLog();
        employeeDocService.ShowAuditLog();
    }
}