import { useCallback, useEffect, useState } from "react"
import { BookService } from "../../../../Services/BookService"
import BookListObject from "./Components/BookListObject"
import { Container } from "react-bootstrap"
import CustomPagination from "../../../Common/CustomPagination"

export default function AdminBooksSection(){
    const [books, setBooks] = useState([])
    const [totalPages, setTotalPages] = useState(1)
    const [currentPage, setCurrentPage] = useState(1)
    const [loading, setLoading] = useState(false)
    let items = null

    const fetchBooks = useCallback(async () => {
        setLoading(true)
        const data = await BookService.getPaginatedBooks("", currentPage)
        setBooks(data.items)
        setTotalPages(data.totalPages)
        setLoading(false)
    }, [currentPage])

    useEffect(()=>{
        fetchBooks()
    }, [fetchBooks])

    if(loading){
        items = <p>Loading...</p>
    }
    else{
        items = books.map(book => <BookListObject key={book.id} {...book}/>)
    }

    return(
        <Container>
            <table className="table">
                <thead>
                    <tr>
                        <th></th>
                        <th>Title</th>
                        <th>ISBN</th>
                        <th>Genre</th>
                        <th>Author</th>
                        <th>Quantity</th>
                    </tr>
                </thead>
                <tbody>
                    {items}
                </tbody>
            </table>
            <nav className="justify-content-center">
                <CustomPagination totalPages={totalPages} currentPage={currentPage} changeValue={setCurrentPage}/>
            </nav>
        </Container>
    )
}