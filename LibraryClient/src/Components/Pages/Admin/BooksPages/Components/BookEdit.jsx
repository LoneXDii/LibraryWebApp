import { useState } from "react";
import GenreSelector from "./GenreSelector";
import AuthorSelector from "./AuthorSelector";
import { BookService } from "../../../../../Services/BookService";
import { useNavigate } from "react-router-dom";

export default function BookEdit({id, isbn, title, genre, author, quantity, image, description, genreId, authorId}){
    const navigate = useNavigate()

    const [imageFile, setImageFile] = useState(null)
    const [book, setBook] = useState({
        id: id ? id : 0,
        isbn: isbn ? isbn : '',
        title: title ? title : '',
        description: description ? description : '',
        genreId: genreId ? genreId : 0,
        authorId: authorId ? authorId : 0,
        quantity: quantity ? quantity : 0, 
        image: image ? image : null
    });

    const [titleError, setTitleError] = useState(false)
    const [isbnError, setIsbnError] = useState(false)
    const [descriptionError, setDestriptionError] = useState(false)
    const [quantityError, setQuantityError] = useState(false)

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

    let isbnPattern = /^:?\x20*(?=.{17}$)97(?:8|9)([ -])\d{1,5}\1\d{1,7}\1\d{1,6}\1\d$/

    async function handleSubmit()
    {
        let isTitleOk = book.title.length >= 1
        let isDescriptionOk = book.description.length >= 1
        let isQuantityOk = book.quantity >= 0
        let isIsbnOk = isbnPattern.test(book.isbn)

        setTitleError(!isTitleOk)
        setDestriptionError(!isDescriptionOk)
        setQuantityError(!isQuantityOk)
        setIsbnError(!isIsbnOk)

        if(!(isDescriptionOk && isIsbnOk && isQuantityOk && isTitleOk)){
            return
        }

        let response = await BookService.updateBook(book, imageFile)
        handleGoBack()
    }

    function handleGoBack(){
        navigate(-1)
    }

    return(
        <>
            <div>
                <label>Title:</label>
                <input type="text" name="title" className="form-control"
                       value={book.title} onChange={handleChange}
                       style={{border: titleError ? '2px solid red' : null}} 
                />
            </div>
            <div>
                <label>ISBN:</label>
                <input type="text" name="isbn" className="form-control" 
                       value={book.isbn} onChange={handleChange}
                       style={{border: isbnError ? '2px solid red' : null}} 
                />
            </div>
            <div>
                <label>Description:</label>
                <textarea name="description" className="form-control" 
                          value={book.description} onChange={handleChange}
                          style={{border: descriptionError ? '2px solid red' : null}}
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
                       style={{border: quantityError ? '2px solid red' : null}} 
                />
            </div>
            <div>
                <label>Image:</label>
                <input type="file" name="image" className="form-control"
                       accept="image/*" onChange={handleImageSelect} 
                />
            </div>
            <button className="btn btn-success my-2 mx-1" onClick={handleSubmit}>Edit</button>
            <button className="btn btn-primary my-2 mx-1" onClick={handleGoBack}>Cancel</button>
        </>
    )
}