using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Firestore;

namespace Wonderland.API
{
    public static class FirestoreAPI
    {
        public static async Task Post(DocumentReference docRef, Dictionary<string, object> dictionary) =>
            await docRef.SetAsync(dictionary);

        public static async Task UpdateAll(DocumentReference docRef, Dictionary<string, object> dictionary) =>
            await docRef.SetAsync(dictionary, SetOptions.MergeAll);

        public static async Task UpdateField(DocumentReference docRef, string[] fields,
            Dictionary<string, object> dictionary) =>
            await docRef.SetAsync(dictionary, SetOptions.MergeFields(fields));

        public static async Task<Dictionary<string, object>> Retrieve(DocumentReference docRef) =>
            await docRef.GetSnapshotAsync().ContinueWith(
                getDocTask =>
                {
                    var snapshot = getDocTask.Result;
                    if (!snapshot.Exists) return null;
                    
                    var result = snapshot.ToDictionary();
                    return result;

                });
    }
}