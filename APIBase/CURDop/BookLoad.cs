using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace APIBase.CURDop
{
    public class BookLoad
    {
       private static readonly string url = "https://crudcrud.com/api/99fc93de67f8454a9cc9c14d367293de/user";
       

        /// <summary>
        /// Get the value
        /// </summary> 
        /// <returns>
        /// return the data, status code and Reason
        /// </returns>
        
         public static async Task<(List<BookModel>, int statusCode, string Reason)> LoadBook()
         {
              using (HttpResponseMessage reponse = await APIHelper.httpClient.GetAsync(url))
              { 
                if (reponse.IsSuccessStatusCode)
                {
                     List<BookModel> book = await reponse.Content.ReadAsAsync<List<BookModel>>();

                     return (book,(int) reponse.StatusCode, reponse.ReasonPhrase);
                }
                else
                {
                     throw new Exception(reponse.ReasonPhrase);
                }
                
            }
        }
        /// <summary>
        /// create and update the data 
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static async Task<(BookModel addedBook, int statusCode, string reason)> AddBookAsync(BookModel book)
        {
            if (book == null) throw new ArgumentNullException(nameof(book));

            // Ignore Id to prevent sending _id
            string json = JsonConvert.SerializeObject(book, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await APIHelper.httpClient.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                string respJson = await response.Content.ReadAsStringAsync();
                BookModel addedBook = JsonConvert.DeserializeObject<BookModel>(respJson);
                return (addedBook, (int)response.StatusCode, response.ReasonPhrase);
            }
            else
            {
                string err = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error: {response.StatusCode} - {response.ReasonPhrase} \nServer Response: {err}");
            }
        }

        /// <summary>
        /// now this block will method for delete
        /// </summary>
        /// <param name="bookId"> that is "_id"</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static async Task<(bool success, int statusCode, string reason)> DeleteBook(string bookId)
        {
            if (string.IsNullOrEmpty(bookId))
                throw new ArgumentException("Book ID cannot be null or empty.", nameof(bookId));

            // Build URL with the _id
            string deleteUrl = $"{url}/{bookId}";

            try
            {
                HttpResponseMessage response = await APIHelper.httpClient.DeleteAsync(deleteUrl);

                if (response.IsSuccessStatusCode)
                {
                    return (true, (int)response.StatusCode, response.ReasonPhrase);
                }
                else
                {
                    return (false, (int)response.StatusCode, response.ReasonPhrase);
                }
            }
            catch (Exception ex)
            {
                // Return failure if an exception occurs
                return (false, 0, ex.Message);
            }
        }

        ///Update the book details
        ///
        public static async Task<(BookModel updatedBook, int status, string response)> UpdateBookAsync(string id, BookModel updatedData)
        {
            string updateUrl = $"{url}/{id}"; // Include the record ID in the URL
            var obj = new { StudentID = updatedData.StudentID, StudentName = updatedData.StudentName, BookISBN = updatedData.BookISBN };
           //i just remove the 
            string json = JsonConvert.SerializeObject(obj);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await APIHelper.httpClient.PutAsync(updateUrl, content);

            if (response.IsSuccessStatusCode)
            {
                return (updatedData, (int)response.StatusCode, response.ReasonPhrase);
            }
            else
            {
                throw new Exception($"Error: {response.StatusCode} - {response.ReasonPhrase}");
            }
        }





    }
}
