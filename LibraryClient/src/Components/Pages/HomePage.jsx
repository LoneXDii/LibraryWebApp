import { BookService } from "../../Services/BookService";
import HttpSection from "../../componentsTest/TestReact/httpSection";
import Header from "../Common/Header";

export default function HomePage(){
    return(
        <>
            <Header/>
            <h1>Home page</h1>
            <HttpSection/>
        </>
    )
}