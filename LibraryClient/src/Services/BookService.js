import axios from "axios";
import ApiConfiguration from "./ApiConfiguration";

export class BookService{
    static async getPaginatedBooks(genre, pageNo = 1, pageSize = 8 ){
        let uri = ApiConfiguration.apiBaseUri + 'api/books/'
        console.log(genre)
        uri += genre === undefined ? '' : genre
        uri += `?pageNo=${pageNo}&pageSize=${pageSize}`
        console.log(uri)
        let response = await axios.get(uri,{
            headers :{
                'Access-Control-Allow-Origin': '*'
            }
        })
        const responseData = response.data
        return responseData
    }
}