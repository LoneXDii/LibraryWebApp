import { useState } from "react"
import DeleteBookModal from "../DeleteBookModal"

export default function BookListObject({id, isbn, title, genre, author, quantity, image}){
    const [show, setShow] = useState(false)

    const handleClose = () => setShow(false)
    const handleShow = () => setShow(true)

    return(
        <>
            <tr>
                <td>
                    <img src={image === null ? "/noimage.jfif" : image} style={{maxHeight: '150px'}}/>    
                </td>
                <td>
                    {title}
                </td>
                <td>
                    {isbn}
                </td>
                <td>
                    {genre}
                </td>
                <td>
                    {author}
                </td>
                <td>
                    {quantity}
                </td>
                <td>
                    <a className="btn btn-success mx-1" href={`/books/edit/${id}`}>
                        Edit
                    </a>

                    <a className="btn btn-info mx-1" href={`/books/${id}`}>
                        Info
                    </a>

                    <button className="btn btn-danger mx-1" onClick={handleShow}>
                        Delete
                    </button>
                </td>
            </tr>
            <DeleteBookModal id={id} show={show} handleClose={handleClose}/>
        </>
    )
}