import { Container } from "react-bootstrap";
import { BookService } from "../../Services/BookService";
import Header from "../Common/Header";

export default function HomePage(){
    return(
        <>
            <Header/>
            <Container>
                <h1>Home page</h1>
            </Container>
        </>
    )
}