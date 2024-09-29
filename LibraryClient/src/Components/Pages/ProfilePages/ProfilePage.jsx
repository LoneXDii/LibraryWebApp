import Header from "../../Common/Header"
import { AuthenticationService } from "../../../Services/AuthenticationService"
import { Navigate } from "react-router-dom"
import { Container } from "react-bootstrap"
import { BookService } from "../../../Services/BookService"
import { useCallback, useEffect, useState } from "react"
import TakenBookCard from "./Components/TakenBookCard"

export default function ProfilePage(){
    const [books, setBooks] = useState([])
    const [loading, setLoading] = useState(false)
    
    const fetchBooks = useCallback(async () => {
        setLoading(true)
        const data = await BookService.getTakenBooks()
        setBooks(data)
        setLoading(false)
    }, [])

    useEffect(()=>{
        fetchBooks()
    }, [fetchBooks])

    if (!AuthenticationService.userId){
        return (<Navigate to="/login"/>)
    }

    let cards = null
    if(loading){
        cards = <p>Loading...</p>
    }
    else{
        cards = books.map(book => <TakenBookCard key={book.id} {...book}/>)
    }

    return(
        <>
            <Header/>
            <Container>
                <h1>{AuthenticationService.userName}</h1>
                <h2>Taken books:</h2>
                <Container className="row row-cols-md-3 g-4">
                        {cards}
                </Container>
            </Container>
        </>
    )
}