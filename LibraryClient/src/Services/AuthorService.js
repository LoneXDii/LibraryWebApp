import ApiConfiguration from "./ApiConfiguration";
import { apiService } from "./ApiService";

export class AuthorService{
    static async getAuthors(){
        let uri = ApiConfiguration.apiBaseUri + 'api/authors'
        let response = await apiService.get(uri,{
            headers :{
                'Access-Control-Allow-Origin': '*'
            }
        })
        const responseData = response.data
        return responseData
    }
}