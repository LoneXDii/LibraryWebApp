export default function BookInfoComponent({title, isbn, description, author, genre, quantity, image}){
    return(
        <div className="row">
            <div className="col-sm-2">
                <img src={image} style={{maxWidth: '200px'}}/>
            </div>
            <div className="col-sm-10">
                <h1>{title}</h1>
                <h3>{author}</h3>
                <h5>{genre}</h5>
                <p>{isbn}</p>
                <p>{description}</p>
                <h5>{quantity} book left</h5>
            </div>
        </div>
    )
}