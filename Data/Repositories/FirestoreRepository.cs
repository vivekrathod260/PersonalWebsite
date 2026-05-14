using Data.Configuration;
using Google.Cloud.Firestore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Data.Repositories;

public interface IFirestoreRepository
{
    Task<T?> GetDocumentAsync<T>(string collection, string documentId) where T : class;
    Task<List<T>> GetCollectionAsync<T>(string collection) where T : class;
    Task<List<T>> GetCollectionOrderedAsync<T>(string collection, string orderByField) where T : class;
    Task AddDocumentAsync<T>(string collection, T data) where T : class;
}

public class FirestoreRepository : IFirestoreRepository
{
    private readonly FirestoreDb _db;
    private readonly ILogger<FirestoreRepository> _logger;

    public FirestoreRepository(IOptions<FirebaseSettings> settings, ILogger<FirestoreRepository> logger)
    {
        _logger = logger;
        var firebaseSettings = settings.Value;

        if (!string.IsNullOrEmpty(firebaseSettings.CredentialPath))
        {
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", firebaseSettings.CredentialPath);
        }

        _db = FirestoreDb.Create(firebaseSettings.ProjectId);
    }

    public async Task<T?> GetDocumentAsync<T>(string collection, string documentId) where T : class
    {
        try
        {
            var docRef = _db.Collection(collection).Document(documentId);
            var snapshot = await docRef.GetSnapshotAsync();
            return snapshot.Exists ? snapshot.ConvertTo<T>() : null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching document {DocumentId} from {Collection}", documentId, collection);
            throw;
        }
    }

    public async Task<List<T>> GetCollectionAsync<T>(string collection) where T : class
    {
        try
        {
            var snapshot = await _db.Collection(collection).GetSnapshotAsync();
            return snapshot.Documents.Select(doc =>
            {
                var item = doc.ConvertTo<T>();
                SetIdProperty(item, doc.Id);
                return item;
            }).ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching collection {Collection}", collection);
            throw;
        }
    }

    public async Task<List<T>> GetCollectionOrderedAsync<T>(string collection, string orderByField) where T : class
    {
        try
        {
            var snapshot = await _db.Collection(collection).OrderBy(orderByField).GetSnapshotAsync();
            return snapshot.Documents.Select(doc =>
            {
                var item = doc.ConvertTo<T>();
                SetIdProperty(item, doc.Id);
                return item;
            }).ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching ordered collection {Collection}", collection);
            throw;
        }
    }

    public async Task AddDocumentAsync<T>(string collection, T data) where T : class
    {
        try
        {
            await _db.Collection(collection).AddAsync(data);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding document to {Collection}", collection);
            throw;
        }
    }

    private static void SetIdProperty<T>(T item, string id)
    {
        var idProp = typeof(T).GetProperty("Id");
        if (idProp != null && idProp.PropertyType == typeof(string))
        {
            idProp.SetValue(item, id);
        }
    }
}
