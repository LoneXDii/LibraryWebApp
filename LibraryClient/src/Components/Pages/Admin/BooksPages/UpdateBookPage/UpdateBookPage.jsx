import { useCallback, useEffect, useState } from "react"
import { Navigate, useParams } from "react-router-dom"
import { AuthenticationService } from "../../../../../Services/AuthenticationService"
import Header from "../../../../Common/Header"
import { Container } from "react-bootstrap"
import { BookService } from "../../../../../Services/BookService"
import BookEdit from "../Components/BookEdit"

export default function UpdateBookPage(){

    const { id } = useParams()
    const [book, setBook] = useState()
    const [loading, setLoading] = useState(false)

    const fetchBook = useCallback(async () => {
        setLoading(true)
        let book = null
        try{
            book = await BookService.getBookById(id)
            console.log(book)
        }
        catch(error){
            console.log('errr')
        }
        setBook(book)
        setLoading(false)
    }, [])

    useEffect(()=>{
        fetchBook()
    }, [fetchBook])

    if (AuthenticationService.userRole !== 'admin'){
        return(<Navigate to={"/home"}/>)
    }

    let bookInput = null
    if(loading){
        bookInput = <p>Loading...</p>
    }
    else{
        console.log(book)
        bookInput = <BookEdit {... book}/>
    }

    return(
        <>
            <Header/>
            <h1>Update book</h1>
            <Container>
                {bookInput}
            </Container>
        </>
    )
}