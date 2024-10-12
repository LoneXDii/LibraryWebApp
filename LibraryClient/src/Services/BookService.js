import { apiService } from "./ApiService";
import ApiConfiguration from "./ApiConfiguration";
import { AuthenticationService } from "./AuthenticationService";
import { TokenAcessor } from "./TokenAcessor";

export class BookService{ 
    static async getPaginatedBooks(genre, pageNo = 1, pageSize = 8 ){
        let uri = ApiConfiguration.apiBaseUri + 'api/books/'
        uri += genre === undefined ? '' : genre
        uri += `?pageNo=${pageNo}&pageSize=${pageSize}`
        let response = await apiService.get(uri)
        const responseData = response.data
        return responseData
    }

    static async getBookById(id){
        let uri = ApiConfiguration.apiBaseUri + `api/books/${id}`
        let response = await apiService.get(uri)
        return response.data
    }

    static async updateBook(book, image){
        const formData = new FormData();
    
        formData.append('book.Book.Id', book.id)
        formData.append('book.Book.ISBN', book.isbn);
        formData.append('book.Book.Title', book.title);
        formData.append('book.Book.Description', book.description);
        formData.append('book.Book.GenreId', book.genreId);
        formData.append('book.Book.Genre', null);
        formData.append('book.Book.AuthorId', book.authorId);
        formData.append('book.Book.Author', null);
        formData.append('book.Book.Quantity', book.quantity);
        if (book.image){
            formData.append('book.Book.Image', book.image)
        }
        
        if (image) {
            formData.append('book.ImageFile', image);
        }

        let updateUri = ApiConfiguration.apiBaseUri + `api/books/${book.id}`

        console.log(updateUri)
        try{
            const response = await apiService.put(updateUri, formData, {
                headers:{
                    'Content-Type': 'multipart/form-data',
                }
            })
            console.log('book updated sucessfully')
        }
        catch(error){
            console.log('Error in updating book')
            console.log(error)
        }
        
    }

    static async CreateBook(book, image){
        const formData = new FormData();
    
        formData.append('book.Book.ISBN', book.isbn);
        formData.append('book.Book.Title', book.title);
        formData.append('book.Book.Description', book.description);
        formData.append('book.Book.GenreId', book.genreId);
        formData.append('book.Book.Genre', null);
        formData.append('book.Book.AuthorId', book.authorId);
        formData.append('book.Book.Author', null);
        formData.append('book.Book.Quantity', book.quantity);
        formData.append('book.Book.Image', null)
        
        if (image) {
            formData.append('book.ImageFile', image);
        }

        for (let [key, value] of formData.entries()) {
            console.log(key, value);
        }

        let uri = ApiConfiguration.apiBaseUri + 'api/books'
        try{
            const response = await apiService.post(uri, formData, {
                headers:{
                    'Content-Type': 'multipart/form-data',
                }
            })
            console.log('book saved sucessfully')
        }
        catch(error){
            console.log('Error in saving book')
            console.log(error)
        }
    }

    static async deleteBook(id){
        let uri = ApiConfiguration.apiBaseUri + `api/books/${id}`
        let response = await apiService.delete(uri)
    }

    static async takeBook(bookId){
        let uri = ApiConfiguration.apiBaseUri + 'api/library/take'
        console.log(TokenAcessor.accessToken)

        let response = await apiService.post(uri, {
            bookId: bookId,
            userId: AuthenticationService.userId
        }, {
            headers: {
                'Content-Type': 'application/json',
            }
        })
    }

    static async getTakenBooks(){
        let uri = ApiConfiguration.apiBaseUri + `api/library/user-books/${AuthenticationService.userId}`
        let response = await apiService.get(uri)
        return response.data
    }
}