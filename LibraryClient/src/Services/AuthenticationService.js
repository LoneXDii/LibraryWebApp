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
            }
        })
        const responseData = response.data
        TokenAcessor.accessToken = responseData.access_token
        TokenAcessor.refreshToken = responseData.refresh_token
        localStorage.setItem('accessToken', TokenAcessor.accessToken)
        localStorage.setItem('refreshToken', TokenAcessor.refreshToken)


        const arrayToken = TokenAcessor.accessToken.split('.')
        const tokenPayload = JSON.parse(atob(arrayToken[1]))
        this.userId = tokenPayload?.sub
        this.userName = tokenPayload?.name
        this.userRole = tokenPayload?.role

        localStorage.setItem('userId', this.userId);
        localStorage.setItem('userName', this.userName);
        localStorage.setItem('userRole', this.userRole);
    }

    static async register(name, surname, email, password, phone){
        let uri = ApiConfiguration.identityApiBaseUri + 'api/users/register'
        let response = await apiService.post(uri, {
            name: name,
            surname: surname,
            email: email,
            phone: phone,
            password: password
        }, {
            headers: {
                'Content-Type': 'application/json',
            }
        })

        await this.login(email, password)
    }

    static logout(){
        this.userId = null
        this.userName = null
        this.userRole = null
        TokenAcessor.accessToken = null
        TokenAcessor.refreshToken = null

        localStorage.removeItem('accessToken')
        localStorage.removeItem('refreshToken')
        localStorage.removeItem('userId');
        localStorage.removeItem('userName');
        localStorage.removeItem('userRole');
    }

    static loadUserFromStorage() {
        this.userId = localStorage.getItem('userId');
        this.userName = localStorage.getItem('userName');
        this.userRole = localStorage.getItem('userRole');
        TokenAcessor.accessToken = localStorage.getItem('accessToken');
        TokenAcessor.refreshToken = localStorage.getItem('refreshToken');
    }
}