import axios from "axios";
import { TokenAcessor } from "./TokenAcessor";
import ApiConfiguration from "./ApiConfiguration";

export const apiService = axios.create()

apiService.interceptors.request.use(
    (cfg) => {
        cfg.headers.Authorization = `Bearer ${TokenAcessor.accessToken}`
        return cfg
    }
)

apiService.interceptors.response.use(
    (cfg) => {
        return cfg
    },

    async (error) => {
        const originalRequest = {...error.config};
        originalRequest._isRetry = true; 
        if ( error.response.status === 401 && error.config && !error.config._isRetry ){
            try{
                let uri = ApiConfiguration.identityApiBaseUri + 'connect/token'
                const response = await apiService.postpost(uri, {
                    client_id: 'library',
                    client_secret: 'secret',
                    grant_type: 'refresh_token',
                    refresh_token: TokenAcessor.refresh_token
                }, {
                    headers: {
                        'Content-Type': 'application/x-www-form-urlencoded',
                        'Access-Control-Allow-Origin': '*'
                    }
                })
                TokenAcessor.accessToken = response.data.access_token
                return apiService.request(originalRequest);
            } catch (error) {
                console.log("AUTH ERROR");
            }
        }
        throw error;
    }
)