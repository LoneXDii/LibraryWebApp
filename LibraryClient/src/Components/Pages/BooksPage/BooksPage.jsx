import { useCallback, useEffect, useState } from "react";
import Header from "../../Common/Header";
import BookCard from "./BookPageComponents/BookCard";
import { Container } from "react-bootstrap";
import { BookService } from "../../../Services/BookService";
import GenreSelector from "./BookPageComponents/GenreSelector";

export default function BooksPage(){
    const [books, setBooks] = useState([])
    const [loading, setLoading] = useState(false)
    const [genre, setGenre] = useState(undefined)
    let cards = null

    const fetchBooks = useCallback(async () => {
        setLoading(true)
        const data = await BookService.getPaginatedBooks(genre)
        setBooks(data.items)
        setLoading(false)
    }, [genre])

    useEffect(()=>{
        fetchBooks()
    }, [fetchBooks, genre])

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
            <Container className="row">
                <Container className="col-2 border-2 md-3">
                    <GenreSelector setValue={setGenre}/>
                </Container>
                <div className="col-10">
                    <Container className="row row-cols-md-3 g-4">
                        {cards}
                    </Container>
                </div>
            </Container>
        </>
    )
}