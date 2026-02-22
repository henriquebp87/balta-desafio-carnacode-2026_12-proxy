using DesignPatternChallenge_Proxy.Models;
using DesignPatternChallenge_Proxy.Subject;

namespace DesignPatternChallenge_Proxy.RealSubject;

internal class DocumentService(IDocumentRepository repository) : IDocumentService
{
    public ConfidentialDocument ViewDocument(string documentId)
    {
        return repository.GetDocument(documentId);
    }

    public void EditDocument(string documentId, string newContent)
    {
        repository.UpdateDocument(documentId, newContent);
    }
}