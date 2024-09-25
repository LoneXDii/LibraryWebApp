import axios from "axios";
import ApiConfiguration from "./ApiConfiguration";

export class GenreServcie{
    static async getGenres(){
        let uri = ApiConfiguration.apiBaseUri + 'api/genres'
        let response = await axios.get(uri,{
            headers :{
                'Access-Control-Allow-Origin': '*'
            }
        })
        const responseData = response.data
        return responseData
    }
}