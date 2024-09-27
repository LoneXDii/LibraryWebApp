import { Navigate } from "react-router-dom";
import { AuthenticationService } from "../../../Services/AuthenticationService";

export default function LogoutPage(){
    AuthenticationService.logout()
    return(
        <Navigate to={"/login"}/>
    )
}