export default function BookListObject({id, isbn, title, genre, author, quantity, image}){
    return(
        <tr>
            <td>
                <img src={image} style={{maxHeight: '150px'}}/>    
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

                <a className="btn btn-danger mx-1">
                    Delete
                </a>
            </td>
        </tr>
    )
}