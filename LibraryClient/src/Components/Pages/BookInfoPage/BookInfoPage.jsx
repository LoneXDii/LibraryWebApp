import { Container } from "react-bootstrap";
import Header from "../../Common/Header";
import { useParams } from "react-router-dom";
import { BookService } from "../../../Services/BookService";
import { useCallback, useEffect, useState } from "react";
import BookInfoComponent from "./Components/BookInfoComponent";
import { AuthenticationService } from "../../../Services/AuthenticationService";
import DeleteBookModal from "../Admin/BooksPages/DeleteBookModal";

export default function BookInfoPage(){
    const { id } = useParams()
    const [book, setBook] = useState(null)
    const [loading, setLoading] = useState(false)

    const [show, setShow] = useState(false)

    const handleClose = () => setShow(false)
    const handleShow = () => setShow(true)

    const fetchBook = useCallback(async () => {
        setLoading(true)
        let book = null
        try{
            book = await BookService.getBookById(id)
        }
        catch(error){
        }
        setBook(book)
        setLoading(false)
    }, [])

    useEffect(()=>{
        fetchBook()
    }, [fetchBook])

    let bookInfo = null
    if(loading){
        bookInfo = <p>Loading...</p>
    }
    else{
        bookInfo = <BookInfoComponent {... book}/>
    }

    async function handleTakeClick(){
        await BookService.takeBook(id)
    }

    return (
        <>
            <Header/>
            <h1>Book info</h1>
            <Container>
                {bookInfo}
                <button className="btn btn-primary my-2 mx-1" disabled={book?.quantity === 0}
                        onClick={handleTakeClick}
                >
                    Take
                </button>
                {AuthenticationService.userRole === 'admin' && (<>
                    <a className="btn btn-success my-2 mx-1" href={`/books/edit/${id}`}>Edit</a>
                    <button className="btn btn-danger my-2 mx-1" onClick={handleShow}>Delete</button>
                </>)}
            </Container> 
            <DeleteBookModal id={id} show={show} handleClose={handleClose}/>
        </>
    )
}