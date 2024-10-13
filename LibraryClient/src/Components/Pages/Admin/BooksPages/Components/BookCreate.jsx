import { useState } from "react";
import GenreSelector from "./GenreSelector";
import AuthorSelector from "./AuthorSelector";
import { BookService } from "../../../../../Services/BookService";
import { useNavigate } from "react-router-dom";

export default function BookCreate(){
    const navigate = useNavigate()

    const [imageFile, setImageFile] = useState(null)
    const [book, setBook] = useState({
        isbn: '',
        title: '',
        description: '',
        genreId: 1,
        authorId: 1,
        quantity: 0, 
        image: null
    });

    const [titleError, setTitleError] = useState(false)
    const [isbnError, setIsbnError] = useState(false)
    const [descriptionError, setDestriptionError] = useState(false)
    const [quantityError, setQuantityError] = useState(false)
    const [isbnErrorMessage, setIsbnErrorMessage] = useState((<></>))

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
        setIsbnErrorMessage((<></>))

        if(!(isDescriptionOk && isIsbnOk && isQuantityOk && isTitleOk)){
            return
        }

        let error = await BookService.CreateBook(book, imageFile)
        if (!error){
            handleGoBack()
        }
        else{
            let errMessage = ""
            for (const key in error.response.data.errors) {
                if (error.response.data.errors.hasOwnProperty(key)) {
                  errMessage += error.response.data.errors[key]
                }
            }
            console.log(errMessage)
            setIsbnErrorMessage((<div className="alert alert-danger" role="alert">
            {errMessage}
        </div>))
        }
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
                {isbnErrorMessage}
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
                <GenreSelector onChange={changeGenre}/>
            </div>
            <div>
                <AuthorSelector onChange={changeAuthor}/>
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
            <button className="btn btn-success my-2 mx-1" onClick={handleSubmit}>Create</button>
            <button className="btn btn-primary my-2 mx-1" onClick={handleGoBack}>Cancel</button>
        </>
    )
}