function ListObject({title, description}){
    return(
        <li>
            <p>
                <strong>{title}</strong> {description}
            </p>
        </li>
    )
}

export default ListObject;