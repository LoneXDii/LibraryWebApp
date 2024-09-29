import { Container } from "react-bootstrap";
import Header from "../../../../Common/Header";
import BookCreate from "../Components/BookCreate";

export default function CreateBookPage(){
    return(
        <>
            <Header/>
            <Container>
                <h1>Create book</h1>
                <BookCreate/>
            </Container>
        </>
    )
}