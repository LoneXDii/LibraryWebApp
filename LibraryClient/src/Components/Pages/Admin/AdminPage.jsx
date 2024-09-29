import { Navigate } from "react-router-dom";
import { AuthenticationService } from "../../../Services/AuthenticationService";
import Header from "../../Common/Header";
import AdminBooksSection from "./BooksPages/AdminBooksSection";

export default function AdminPage(){
    if (AuthenticationService.userRole !== "admin"){
        return(
            <Navigate to={"/home"}/>
        )
    }

    return(
        <>
            <Header/>
            <h1>Admin page</h1>
            <AdminBooksSection/>
        </>
    )
}