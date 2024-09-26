import axios from "axios";
import ApiConfiguration from "./ApiConfiguration";

import { AuthenticationService } from "./AuthenticationService";

export class BookService{ 
    static async getPaginatedBooks(genre, pageNo = 1, pageSize = 8 ){
        let uri = ApiConfiguration.apiBaseUri + 'api/books/'
        uri += genre === undefined ? '' : genre
        uri += `?pageNo=${pageNo}&pageSize=${pageSize}`
        let response = await axios.get(uri,{
            headers :{
                'Access-Control-Allow-Origin': '*'
            }
        })
        const responseData = response.data

        //debug
        await AuthenticationService.login('admin1@gmail.com', 'Admin123*')

        return responseData
    }
}