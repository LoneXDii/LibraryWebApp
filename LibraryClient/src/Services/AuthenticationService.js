import { apiService } from "./ApiService";
import ApiConfiguration from "./ApiConfiguration";
import { TokenAcessor } from "./TokenAcessor";

export class AuthenticationService{
    static userId = null
    static userName = null
    static userRole = null

    static async login(email, password){
        let uri = ApiConfiguration.identityApiBaseUri + 'connect/token'
        let response = await apiService.post(uri, {
            client_id: 'library',
            client_secret: 'secret',
            username: email,
            grant_type: 'password',
            password: password
        }, {
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded',
                'Access-Control-Allow-Origin': '*'
            }
        })
        const responseData = response.data
        TokenAcessor.accessToken = responseData.access_token
        TokenAcessor.refreshToken = responseData.refresh_token
        
        const arrayToken = TokenAcessor.accessToken.split('.')
        const tokenPayload = JSON.parse(atob(arrayToken[1]))
        this.userId = tokenPayload?.sub
        this.userName = tokenPayload?.name
        this.userRole = tokenPayload?.role

        console.log(this.userId)
        console.log(this.userName)
        console.log(this.userRole)
    }

    static logout(){
        this.userId = null
        this.userName = null
        this.userRole = null
        TokenAcessor.accessToken = null
        TokenAcessor.refreshToken = null
    }
}