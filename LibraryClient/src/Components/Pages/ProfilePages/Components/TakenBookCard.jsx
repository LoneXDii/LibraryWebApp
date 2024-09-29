export default function TakenBookCard({book, timeOfTake, timeToReturn}){
    const dateObj1 = new Date(timeOfTake)
    let take = dateObj1.toLocaleString().split(',')[0]
    const dateObj2 = new Date(timeToReturn)
    let ret = dateObj2.toLocaleString().split(',')[0]
    return(
        <>
            <div className="col-md-3 p-2">
            <a href={`/books/${book.id}`} style={{textDecoration: 'none'}}>
                <div className="card h-100">
                    <div className="image-container" style={{ width: '100%', height: '200px' }}>
                        <img 
                            src={book?.image === null ? "/noimage.jfif" : book.image} 
                            className="card-img-top mt-2"
                            style={{ width: '100%', height: '100%', objectFit: 'contain' }}
                            alt="Book Cover"
                        />
                    </div>
                    <div className="card-body d-flex flex-column">
                        <h4 className="card-title flex-grow-1">{book?.title}</h4>
                        <h5 className="card-text mb2 flex-grow-1">{book?.author}</h5>
                        <p className="card-text mb2 flex-grow-1">{book?.description}</p>
                    </div>
                    <div className="card-footer text-center">
                        <p>Was taken at: {take}</p>
                        <p>Must be returned before: {ret}</p>
                    </div>
                </div>
            </a>
        </div>
        </>
    )
}