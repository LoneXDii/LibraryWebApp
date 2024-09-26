import { useCallback, useEffect, useState } from "react";
import Header from "../../Common/Header";
import BookCard from "./BookPageComponents/BookCard";
import { Container } from "react-bootstrap";
import { BookService } from "../../../Services/BookService";
import GenreSelector from "./BookPageComponents/GenreSelector";
import CustomPagination from "../../Common/CustomPagination";

export default function BooksPage(){
    const [books, setBooks] = useState([])
    const [totalPages, setTotalPages] = useState(1)
    const [currentPage, setCurrentPage] = useState(1)
    const [loading, setLoading] = useState(false)
    const [genre, setGenre] = useState(undefined)
    let cards = null

    const fetchBooks = useCallback(async () => {
        setLoading(true)
        const data = await BookService.getPaginatedBooks(genre, currentPage)
        setBooks(data.items)
        setTotalPages(data.totalPages)
        setLoading(false)
    }, [genre, currentPage])

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
            <Container className="row">
                <Container className="col-2 border-2 md-3">
                    <GenreSelector setValue={setGenre} resetPage={setCurrentPage}/>
                </Container>
                <div className="col-10">
                    <Container className="row row-cols-md-3 g-4">
                        {cards}
                    </Container>
                </div>
            </Container>
            <nav className="offset-sm-2 justify-content-center">
                <CustomPagination totalPages={totalPages} currentPage={currentPage} changeValue={setCurrentPage}/>
            </nav>
        </>
    )
}