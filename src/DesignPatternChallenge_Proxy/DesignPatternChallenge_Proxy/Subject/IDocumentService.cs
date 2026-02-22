using DesignPatternChallenge_Proxy.Models;

namespace DesignPatternChallenge_Proxy.Subject;

internal interface IDocumentService
{
    ConfidentialDocument ViewDocument(string documentId);

    void EditDocument(string documentId, string newContent);
}