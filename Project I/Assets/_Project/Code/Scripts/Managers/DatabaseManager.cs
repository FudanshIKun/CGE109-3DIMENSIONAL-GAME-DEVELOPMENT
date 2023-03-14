using UnityEngine;

// APIs
using Firebase;
using Firebase.Database;

namespace Wonderland
{
    public class DatabaseManager : MonoBehaviour
    {
        private string databaseURL = "https://project-id.firebaseio.com/users"; 
        private string AuthKey = "secret ;D";
        
        public void SubmitToDatabase()
        {
            //TODO: PostToDatabase();   
        }
        
        private void PostToDatabase()
        {
            //TODO: Post Any Input Data To Database
        }

        private void RetrieveFromDatabase()
        {
            //TODO: Retrieve Any Required Data From Database
        }
    }
}
