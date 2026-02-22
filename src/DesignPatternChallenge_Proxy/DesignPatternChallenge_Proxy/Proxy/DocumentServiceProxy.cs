using DesignPatternChallenge_Proxy.Models;
using DesignPatternChallenge_Proxy.RealSubject;
using DesignPatternChallenge_Proxy.Subject;

namespace DesignPatternChallenge_Proxy.Proxy;

internal class DocumentServiceProxy(User user, IDocumentRepository documentRepository) : IDocumentService
{
    private readonly IDocumentService _documentService = new DocumentService(documentRepository);
    private readonly List<string> _auditLog = [];

    public ConfidentialDocument ViewDocument(string documentId)
    {
        AddAuditLog($"[{DateTime.Now:HH:mm:ss}] {user.Username} tentou acessar {documentId}");

        var doc = _documentService.ViewDocument(documentId);

        if (doc == null)
        {
            Console.WriteLine($"❌ Documento {documentId} não encontrado");
            return null;
        }

        if (user.ClearanceLevel < doc.SecurityLevel)
        {
            Console.WriteLine($"❌ Acesso negado! Nível {user.ClearanceLevel} < Requerido {doc.SecurityLevel}");
            AddAuditLog($"[{DateTime.Now:HH:mm:ss}] ACESSO NEGADO para {user.Username}");

            return null;
        }

        Console.WriteLine($"✅ Acesso permitido ao documento: {doc.Title}");
        return doc;
    }

    public void EditDocument(string documentId, string newContent)
    {
        AddAuditLog($"[{DateTime.Now:HH:mm:ss}] {user.Username} tentou editar {documentId}");

        var doc = ViewDocument(documentId);

        if (doc == null)
        {
            return;
        }

        _documentService.EditDocument(documentId, newContent);

        Console.WriteLine($"✅ Documento atualizado");
    }

    public void ShowAuditLog()
    {
        Console.WriteLine("\n=== Log de Auditoria ===");
        foreach (var entry in _auditLog)
        {
            Console.WriteLine(entry);
        }
    }

    private void AddAuditLog(string logEntry)
    {
        _auditLog.Add(logEntry);
        Console.WriteLine($"[Audit] {logEntry}");
    }
}