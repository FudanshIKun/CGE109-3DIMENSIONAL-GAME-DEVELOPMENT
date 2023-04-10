using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Firestore;

namespace Wonderland.API
{
    public static class FirestoreAPI
    {
        public static async Task PostToFirestore(DocumentReference docRef, Dictionary<string, object> dictionary) =>
            await docRef.SetAsync(dictionary);

        public static async Task UpdateAllToFirestore(DocumentReference docRef, Dictionary<string, object> dictionary) =>
            await docRef.SetAsync(dictionary, SetOptions.MergeAll);

        public static async Task UpdateTargetToFireStore(DocumentReference docRef, string[] targetFields,
            Dictionary<string, object> dictionary) =>
            await docRef.SetAsync(dictionary, SetOptions.MergeFields(targetFields));

        public static async Task<Dictionary<string, object>> RetrieveFromFirestore(DocumentReference docRef) =>
            await docRef.GetSnapshotAsync().ContinueWith(
                getDocTask =>
                {
                    DocumentSnapshot snapshot = getDocTask.Result;
                    if (snapshot.Exists)
                    {
                        Dictionary<string, object> result = snapshot.ToDictionary();
                        return result;
                    }
                    
                    return null;
                });
    }
}