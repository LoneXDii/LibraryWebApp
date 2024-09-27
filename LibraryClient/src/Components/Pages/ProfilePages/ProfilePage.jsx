import Header from "../../Common/Header"
import { AuthenticationService } from "../../../Services/AuthenticationService"
import { Navigate } from "react-router-dom"

export default function ProfilePage(){
    if (!AuthenticationService.userId){
        return (<Navigate to="/login"/>)
    }
    return(
        <>
            <Header/>
            <h1>Profile page</h1>
        </>
    )
}