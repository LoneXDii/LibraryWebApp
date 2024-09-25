import axios from "axios";
import ApiConfiguration from "./ApiConfiguration";

export class BookService{
    static async getPaginatedBooks(genre, pageNo = 1, pageSize = 8 ){
        let uri = ApiConfiguration.apiBaseUri + 'Api/books/'
        if (genre === undefined){
            uri += genre === undefined ? '' : genre
        }
        uri += `?pageNo=${pageNo}&pageSize=${pageSize}`
        let response = await axios.get(uri,{
            headers :{
                'Access-Control-Allow-Origin': '*'
            }
        })
        const responseData = response.data
        return responseData
    }
}