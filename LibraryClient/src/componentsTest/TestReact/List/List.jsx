import ListObject from "../ListObject/ListObject"


export default function List({list}){
    let listElements = list.map(elem => <ListObject key={elem.description} {...elem}/>)

    return(
        <section>
            <ul>
                {listElements}
            </ul>
        </section>
    )
}