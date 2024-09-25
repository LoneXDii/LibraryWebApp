import { useCallback, useEffect, useState } from "react";
import Header from "../../Common/Header";
import { BookService } from "../../../Services/BookService";
import BookCard from "./BookPageComponents/BookCard";
import { Container } from "react-bootstrap";

export default function BooksPage(){
    const [books, setBooks] = useState([])
    const [loading, setLoading] = useState(false)
    let cards = null

    const fetchBooks = useCallback(async () => {
        setLoading(true)
        const data = await BookService.getPaginatedBooks()
        setBooks(data.items)
        console.log(data)
        setLoading(false)
    }, [])

    useEffect(()=>{
        fetchBooks()
    }, [fetchBooks])

    if(loading){
        cards = <p>Loading...</p>
    }
    else{
        cards = books.map(book => <BookCard key={book.id} {...book}/>)
    }

    return(
        <>
            <Header/>
            <h1>Books page</h1>
            <Container className="row row-cols-md-3 g-4">
                {cards}
            </Container>
        </>
    )
}