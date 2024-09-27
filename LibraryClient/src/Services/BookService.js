import { apiService } from "./ApiService";
import ApiConfiguration from "./ApiConfiguration";

export class BookService{ 
    static async getPaginatedBooks(genre, pageNo = 1, pageSize = 8 ){
        let uri = ApiConfiguration.apiBaseUri + 'api/books/'
        uri += genre === undefined ? '' : genre
        uri += `?pageNo=${pageNo}&pageSize=${pageSize}`
        let response = await apiService.get(uri)
        const responseData = response.data
        return responseData
    }
}