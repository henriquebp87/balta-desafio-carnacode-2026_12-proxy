using DesignPatternChallenge_Proxy.Models;

namespace DesignPatternChallenge_Proxy.Subject;

internal interface IDocumentRepository
{
    ConfidentialDocument GetDocument(string documentId);

    void UpdateDocument(string documentId, string newContent);
}