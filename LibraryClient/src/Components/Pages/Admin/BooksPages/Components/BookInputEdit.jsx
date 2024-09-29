import { useState } from "react";
import GenreSelector from "./GenreSelector";
import AuthorSelector from "./AuthorSelector";
import { BookService } from "../../../../../Services/BookService";

export default function BookInputEdit({id, isbn, title, genre, author, quantity, image, description, genreId, authorId}){
    
    const [imageFile, setImageFile] = useState(null)
    const [book, setBook] = useState({
        id: id ? id : 0,
        isbn: isbn ? isbn : '',
        title: title ? title : '',
        description: description ? description : '',
        genreId: genreId ? genreId : 0,
        authorId: authorId ? authorId : 0,
        quantity: quantity ? quantity : 0,
        image: image ? image : 0
    });

    const handleChange = (e) => {
        const { name, value } = e.target;
        setBook({ ...book, [name]: value });
        console.log(book)
    };

    function changeGenre(genreId){
        setBook({ ...book, ['genreId']: genreId });
    }

    function changeAuthor(authorId){
        setBook({ ...book, ['authorId']: authorId });
    }

    function handleImageSelect(event){
        setImageFile(event.target.files[0])
    }

    async function handleSubmit()
    {
        let response = await BookService.updateBook(book, imageFile)
    }

    return(
        <>
            <div>
                <label>Title:</label>
                <input type="text" name="title" className="form-control"
                       value={book.title} onChange={handleChange} 
                />
            </div>
            <div>
                <label>ISBN:</label>
                <input type="text" name="isbn" className="form-control" 
                       value={book.isbn} onChange={handleChange} 
                />
            </div>
            <div>
                <label>Description:</label>
                <textarea name="description" className="form-control" 
                          value={book.description} onChange={handleChange}
                />
            </div>
            <div>
                <GenreSelector onChange={changeGenre} genreId={genreId}/>
            </div>
            <div>
                <AuthorSelector onChange={changeAuthor} authorId={authorId}/>
            </div>
            <div>
                <label>Quantity:</label>
                <input type="number" name="quantity" className="form-control"
                       value={book.quantity} onChange={handleChange} 
                />
            </div>
            <div>
                <label>Image:</label>
                <input type="file" name="image" className="form-control"
                       onChange={handleImageSelect} 
                />
            </div>
            <button className="btn btn-success" onClick={handleSubmit}>Edit</button>
        </>
    )
}