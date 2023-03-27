using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Firestore;

namespace Wonderland.API
{
    public static class FirestoreAPI
    {
        private static readonly FirebaseFirestore Firestore = FirebaseFirestore.DefaultInstance;

        public static readonly DocumentReference UserInfoDocRef =
            Firestore.Collection("Users").Document(AuthAPI.GetCurrentAuthUser().Email);

        public static async Task PostToFirestoreRequest(DocumentReference docRef, Dictionary<string, object> dictionary) =>
            await docRef.SetAsync(dictionary);

        public static async Task RepostToFirestoreRequest(DocumentReference docRef, Dictionary<string, object> dictionary) =>
            await docRef.SetAsync(dictionary, SetOptions.MergeAll);

        public static async Task UpdateToFireStoreRequest(DocumentReference docRef, string[] targetFields,
            Dictionary<string, object> dictionary) =>
            await docRef.SetAsync(dictionary, SetOptions.MergeFields(targetFields));

        public static async Task<Dictionary<string, object>> RetrieveFromFirestoreRequest(DocumentReference docRef) =>
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
