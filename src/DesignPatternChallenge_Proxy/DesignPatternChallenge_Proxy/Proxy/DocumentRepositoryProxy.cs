using DesignPatternChallenge_Proxy.Models;
using DesignPatternChallenge_Proxy.RealSubject;
using DesignPatternChallenge_Proxy.Subject;

namespace DesignPatternChallenge_Proxy.Proxy;

internal class DocumentRepositoryProxy : IDocumentRepository
{
    private readonly IDictionary<string, ConfidentialDocument> _cache = new Dictionary<string, ConfidentialDocument>();
    private IDocumentRepository _documentRepository;

    public ConfidentialDocument GetDocument(string documentId)
    {
        if (_cache.TryGetValue(documentId, out var document))
        {
            Console.WriteLine($"[Cache] Documento {documentId} encontrado no cache");
            return document;
        }

        _documentRepository ??= new DocumentRepository();
        document = _documentRepository.GetDocument(documentId);
        _cache[documentId] = document;

        return document;
    }

    public void UpdateDocument(string documentId, string newContent)
    {
        _documentRepository ??= new DocumentRepository();
        _documentRepository.UpdateDocument(documentId, newContent);

        if (_cache.ContainsKey(documentId))
        {
            _cache.Remove(documentId);
        }
    }
}