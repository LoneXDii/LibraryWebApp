import ApiConfiguration from "./ApiConfiguration";
import { apiService } from "./ApiService";

export class GenreServcie{
    static async getGenres(){
        let uri = ApiConfiguration.apiBaseUri + 'api/genres'
        let response = await apiService.get(uri,{
            headers :{
                'Access-Control-Allow-Origin': '*'
            }
        })
        const responseData = response.data
        return responseData
    }
}